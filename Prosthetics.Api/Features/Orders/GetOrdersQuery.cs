using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Features.AdditionalWorks;
using Prosthetics.Api.Persistance;
using Prosthetics.Api.Persistance.Entities;
using Carter;
using Microsoft.AspNetCore.Mvc;

namespace Prosthetics.Features.Orders
{
    public class GetOrdersQuery : IRequest<IEnumerable<OrderDto>>
    {
        public int DoctorId { get; set; }
    }

    public class GetOrdersQueryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("orders", async ([FromServices] IMediator mediator) => await mediator.Send(new GetOrdersQuery()));
        }
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
                .Include(_ => _.AdditionalWorkCounts).ThenInclude(_ => _.AdditionalWork).Include(_ => _.Patient).Include(_ => _.OrderType)
                .AsNoTracking()
                .Where(_ => _.DoctorId == request.DoctorId).ToListAsync();

            return result.Adapt<IEnumerable<OrderDto>>();
        }
    }

    public class OrderDto : IRegister
    {
        public int Id { get; set; }
        public required string PatientFullName { get; set; }
        public required DateTime OrderDate { get; set; }
        public required DateTime DeadLine { get; set; }
        public required string Type { get; set; }
        public List<AdditionalWorkCountDto> AdditionalWorksCounts { get; set; } = new List<AdditionalWorkCountDto>();
        public int AdditionalWorksCount { get; set; }
        public string? Comments { get; set; }
        public required string ShortComment { get; set; }
        public required int Status { get; set; }
        public int OrderStatusId { get; set; }


        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Order, OrderDto>()
                .Map(dest => dest.PatientFullName, src => src.Patient == null ? string.Empty : $"{src.Patient.LastName} {src.Patient.FirstName}")
                .Map(dest => dest.OrderDate, src => src.InsertedDate)
                .Map(dest => dest.DeadLine, src => src.DeadLine)
                .Map(dest => dest.Status, src => src.Status)
                .Map(dest => dest.Type, src => src.OrderType != null ? src.OrderType.Name : string.Empty)
                .Map(dest => dest.OrderStatusId, src => (int)src.Status)
                .Map(dest => dest.AdditionalWorksCounts, src => src.AdditionalWorkCounts)
                .PreserveReference(true);
        }
    }
}
