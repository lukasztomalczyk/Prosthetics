using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Features.AdditionalWorks;
using Prosthetics.Models;
using Prosthetics.Persistance;
using Prosthetics.Persistance.Entities;

namespace Prosthetics.Features.Orders
{
    public class GetOrdersQuery : IRequest<IEnumerable<OrderDto>>
    {
        public int DoctorId { get; set; }
    }

    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>
    {
        private readonly ProstheticsDbContext _dbContext;

        public GetOrdersQueryHandler(ProstheticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Orders
                .Include(_ => _.AdditionalWorks).Include(_ => _.Patient).Include(_ => _.OrderType)
                .Where(_ => _.DoctorId == request.DoctorId).ToListAsync();

            return result.Adapt<IEnumerable<OrderDto>>();
        }
    }

    public class OrderDto : IRegister
    {
        public int Id { get; set; }
        public string? PatientFullName { get; set; }
        public string? OrderDate { get; set; }
        public string? DeadLine { get; set; }
        public string? Type { get; set; }
        public List<AdditionalWorkDto> AdditionalWorks { get; set; } = new List<AdditionalWorkDto>();
        public int AdditionalWorksCount { get; set; }
        public string? Comments { get; set; }
        public string? ShortComment { get; set; }
        public string? Status { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Order, OrderDto>()
                .Map(dest => dest.PatientFullName, src => src.Patient == null ? string.Empty : $"{src.Patient.LastName} {src.Patient.FirstName}")
                .Map(dest => dest.OrderDate, src => src.InsertedDate.ToString("dd-MM-yyyy"))
                .Map(dest => dest.DeadLine, src => src.DeadLine.ToString("dd-MM-yyyy"))
                .Map(dest => dest.Status, src => MapStatus(src.Status))
                .Map(dest => dest.Type, src => src.OrderType != null ? src.OrderType.Name : string.Empty);
        }

        public static string MapStatus(OrderStatus status)
        {
            switch (status)
            {
                case OrderStatus.New:
                    return "Nowe";
                case OrderStatus.InProgress:
                    return "W przygotowaniu";
                case OrderStatus.Canceled:
                    return "Anulowane";
                case OrderStatus.Sent:
                    return "Wysłane";
                default:
                    return "Nowe";
            }
        }
    }
}
