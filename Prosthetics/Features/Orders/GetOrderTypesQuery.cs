using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Persistance;
using Prosthetics.Persistance.Entities;

namespace Prosthetics.Features.Orders
{
    public class GetOrderTypesQuery : IRequest<IEnumerable<OrderTypeDto>>
    {
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
