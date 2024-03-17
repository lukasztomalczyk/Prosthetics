using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Api.Persistance;
using Prosthetics.Api.Persistance.Entities;

namespace Prosthetics.Features.Orders
{
    public class GetOrderTypesQuery : IRequest<IEnumerable<OrderTypeDto>>
    {
    }

    public class GetOrderTypesQueryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("orders/types", async ([FromServices] IMediator mediator) => await mediator.Send(new GetOrderTypesQuery()));
        }
    }

    public class GetQueryTypesQueryHandler : IRequestHandler<GetOrderTypesQuery, IEnumerable<OrderTypeDto>>
    {
        private readonly ProstheticsDbContext _dbContext;

        public GetQueryTypesQueryHandler(ProstheticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<OrderTypeDto>> Handle(GetOrderTypesQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.OrderTypes.ToListAsync();

            return result.Adapt<IEnumerable<OrderTypeDto>>();
        }
    }

    public class OrderTypeDto : IRegister
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<OrderType, OrderTypeDto>();
        }
    }
}
