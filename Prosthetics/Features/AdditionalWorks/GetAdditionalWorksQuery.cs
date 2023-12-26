using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Persistance;
using Prosthetics.Persistance.Entities;

namespace Prosthetics.Features.AdditionalWorks
{
    public class GetAdditionalWorksQuery : IRequest<IEnumerable<AdditionalWorkDto>>
    {
    }

    public class GetAdditionalWorksQueryHandler : IRequestHandler<GetAdditionalWorksQuery, IEnumerable<AdditionalWorkDto>>
    {
        private readonly ProstheticsDbContext _dbContext;

        public GetAdditionalWorksQueryHandler(ProstheticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<AdditionalWorkDto>> Handle(GetAdditionalWorksQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.AdditionalWorks.ToListAsync(cancellationToken);

            return result.Adapt<IEnumerable<AdditionalWorkDto>>();
        }
    }

    public class AdditionalWorkDto : IRegister
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AdditionalWork, AdditionalWorkDto>();
        }
    }
}
