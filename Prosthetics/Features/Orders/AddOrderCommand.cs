using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Common;
using Prosthetics.Components.Pages.Orders.Models;
using Prosthetics.Features.AdditionalWorks;
using Prosthetics.Models;
using Prosthetics.Persistance;
using Prosthetics.Persistance.Entities;

namespace Prosthetics.Features.Orders
{
    public class AddOrderCommand : IRequest, IRegister
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int OrderTypeId { get; set; }
        public required List<AdditionalWorkCountDto> AdditionalWorks { get; init; }
        public DateTime DeadLine { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddOrderCommand, Order>()
                .Map(dest => dest.Status, src => OrderStatus.New)
                // TODO REMOVE
                .PreserveReference(true);
        }
    }

    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand>
    {
        private readonly IDateTime _dateTime;
        private readonly ProstheticsDbContext _dbContext;

        public AddOrderCommandHandler(IDateTime dateTime, ProstheticsDbContext dbContext)
        {
            _dateTime = dateTime;
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            Order order;
            try
            {
                order = request.Adapt<Order>();

                order.InsertedDate = _dateTime.Now();
                //order.AdditionalWorks.ForEach(_ =>
                //{
                //    _.Count = request.Ad
                //});
                order.AdditionalWorkCounts = request.AdditionalWorks.Select(_ => new AdditionalWorkCount() 
                {
                    Count = int.Parse(request.AdditionalWorks.First(x => x.Id == _.Id).Count),
                    AdditionalWorkId = request.AdditionalWorks.First(x => x.Id == _.Id).Id
                })
                .ToList();

                var entity = await _dbContext.Orders.AddAsync(order);
                await _dbContext.SaveChangesAsync();
                entity.State = EntityState.Detached;
                await _dbContext.SaveChangesAsync();

            return Unit.Value;
            }
            catch (Exception ex)
            {

                throw;
            }


        }
    }
}
