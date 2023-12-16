using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var result = await _dbContext.Orders.Where(_ => _.DoctorId == request.DoctorId).ToListAsync();

            return result.Adapt<IEnumerable<OrderDto>>();
        }
    }

    public class OrderDto : IRegister
    {
        public string Type { get; set; }
        public string OrderDate { get; set; }
        public string DeadLine { get; set; }
        public string Status { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Order, OrderDto>()
                .Map(dest => dest.OrderDate, src => src.InsertedDate.ToString("dd-MM-yyyy"))
                .Map(dest => dest.DeadLine, src => src.DeadLine.ToString("dd-MM-yyyy"))
                .Map(dest => dest.Status, src => MapStatus(src.Status));
        }

        public static string MapStatus(int status)
        {
            switch (status)
            {
                case 1:
                    return "nowe";
                case 2:
                    return "w przygotowaniu";
                case 3:
                    return "wycofane";
                default:
                    return "nowe";
            }
        }
    }
}
