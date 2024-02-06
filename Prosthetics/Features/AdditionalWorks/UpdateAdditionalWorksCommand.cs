using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Persistance;
using Prosthetics.Persistance.Entities;

namespace Prosthetics.Features.AdditionalWorks
{
    public class UpdateAdditionalWorksCommand : IRequest<Unit>
    {
        public int OrderId { get; set; }
        public IEnumerable<EditedAdditionalCountWorkDto> AdditionalCountWorks { get; set; } = new List<EditedAdditionalCountWorkDto>();
    }

    public class UpdateAdditionalWorksCommandHandler : IRequestHandler<UpdateAdditionalWorksCommand, Unit>
    {
        private readonly ProstheticsDbContext _dbContext;

        public UpdateAdditionalWorksCommandHandler(ProstheticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateAdditionalWorksCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.Include(_ => _.AdditionalWorkCounts)
                .ThenInclude(_ => _.AdditionalWork)
                .Where(_ => _.Id == request.OrderId).FirstAsync(cancellationToken);

            var ids = request.AdditionalCountWorks.Select(_ => _.AdditionalWorkId);
            var exceptToDelete = order.AdditionalWorkCounts.Select(_ => _.Id).Except(ids);

            // usuwa te które zostały usunięte z UI
            foreach (var entityId in exceptToDelete)
            {
                _dbContext.AdditionalWorkCounts.RemoveRange(
                    order.AdditionalWorkCounts.Where(_ => exceptToDelete.Contains(_.Id)));
            }

            var exceptIdsToBeAdded = request.AdditionalCountWorks.Select(_ => _.AdditionalWorkId).Except(order.AdditionalWorkCounts.Select(_ => _.Id));
            var toBeAdded = request.AdditionalCountWorks.Where(_ => exceptIdsToBeAdded.Contains(_.AdditionalWorkId));

            // dodaje nowe
            foreach (var work in toBeAdded)
            {
                order.AdditionalWorkCounts.Add(new AdditionalWorkCount()
                {
                    Count = work.Count,
                    AdditionalWorkId = work.AdditionalWorkId
                });
            }

            // aktualizuje istniejące
            var toDeUpdated = request.AdditionalCountWorks.Select(_ => _.AdditionalWorkId).Except(exceptIdsToBeAdded);
            foreach (var id in toDeUpdated)
            {
                order.AdditionalWorkCounts.First(_ => _.Id == id).Count = request.AdditionalCountWorks.First(_ => _.AdditionalWorkId == id).Count;
            }

            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }

    public class EditedAdditionalCountWorkDto : IRegister
    {
        public int AdditionalWorkId { get; set; }
        public int Count { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AdditionalWorkCountDto, EditedAdditionalCountWorkDto>()
                .Map(dest => dest.Count, src => int.Parse(src.Count))
                .Map(dest => dest.AdditionalWorkId, src => src.Id);
        }
    }
}
