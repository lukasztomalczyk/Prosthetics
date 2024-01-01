using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Persistance;
using Prosthetics.Persistance.Entities;

namespace Prosthetics.Features.AdditionalWorks
{
    public class GetAdditionalWorksQuery : IRequest<IEnumerable<AdditionalWorkCountDto>>
    {
    }

    public class GetAdditionalWorksQueryHandler : IRequestHandler<GetAdditionalWorksQuery, IEnumerable<AdditionalWorkCountDto>>
    {
        private readonly ProstheticsDbContext _dbContext;

        public GetAdditionalWorksQueryHandler(ProstheticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<AdditionalWorkCountDto>> Handle(GetAdditionalWorksQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.AdditionalWorks.AsNoTracking().ToListAsync(cancellationToken);

            return result.Adapt<IEnumerable<AdditionalWorkCountDto>>();
        }
    }

    public class AdditionalWorkCountDto : IRegister
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Count { get; set; } = "0";

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AdditionalWorkCount, AdditionalWorkCountDto>()
                .Map(dest => dest.Count, src => src.Count.ToString())
                .Map(dest => dest.Name, src => src.AdditionalWork != null ? src.AdditionalWork.Name : string.Empty)
                .Map(dest => dest.Id, src => src.AdditionalWork != null ? src.AdditionalWork.Id : 0)
                .PreserveReference(true);
        }
    }
}
