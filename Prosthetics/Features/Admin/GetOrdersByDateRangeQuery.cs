using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Persistance;
using Prosthetics.Persistance.Entities;

namespace Prosthetics.Features.Admin
{
    public class GetOrdersByDateRangeQuery : IRequest<IEnumerable<OrderByRangeDto>>
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }

    public class GetOrdersByDateRangeQueryHandler : IRequestHandler<GetOrdersByDateRangeQuery, IEnumerable<OrderByRangeDto>>
    {
        private readonly ProstheticsDbContext _dbContext;

        public GetOrdersByDateRangeQueryHandler(ProstheticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<OrderByRangeDto>> Handle(GetOrdersByDateRangeQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Orders
                .Include(_ => _.AdditionalWorks).Include(_ => _.OrderType).Include(_ => _.Patient)
                .Where(_ => _.InsertedDate <= request.From && _.InsertedDate <= request.To).ToListAsync();

            return result.Adapt<IEnumerable<OrderByRangeDto>>();
        }
    }

    public class OrderByRangeDto : IRegister
    {
        public string? PatientFullName { get; set; }
        public string? Type { get; set; }
        public List<AdditionalWorkByDateDto> AdditionalWorks { get; set; } = new();

        public void Register(TypeAdapterConfig config)
        {
             config.NewConfig<Order, OrderByRangeDto>()
                .Map(dest => dest.PatientFullName, src => src.Patient != null 
                    ? $"{src.Patient.LastName} {src.Patient.FirstName}" : string.Empty)
                .Map(dest => dest.AdditionalWorks, src => src.AdditionalWorks)
                .PreserveReference(true);
        }
    }

    public class AdditionalWorkByDateDto : IRegister
    {
        public string Name { get; set; }
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AdditionalWork, AdditionalWorkByDateDto>()
                .PreserveReference(true);
        }
    }
}
