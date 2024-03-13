using JuniorDevOps.Net.Common.Time;
using Mapster;
using Prosthetics.Api.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Features.AdditionalWorks;
using Prosthetics.Api.Persistance;
using Prosthetics.Api.Persistance.Entities;
using Carter;
using Microsoft.AspNetCore.Mvc;

namespace Prosthetics.Features.Orders
{
    public class AddOrderCommand : IRequest<int>, IRegister
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int OrderTypeId { get; set; }
        public List<AdditionalWorkCountDto> AdditionalWorks { get; init; } = new();
        public DateTime DeadLine { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddOrderCommand, Order>()
                .Map(dest => dest.Status, src => OrderStatus.New)
                // TODO REMOVE
                .PreserveReference(true);
        }
    }

    public class AddOrderCommandEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("orders", async ([FromBody] AddOrderCommand command,
                [FromServices] IMediator mediator) => await mediator.Send(command));
        }
    }

    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, int>
    {
        private readonly IDateTime _dateTime;
        private readonly ProstheticsDbContext _dbContext;

        public AddOrderCommandHandler(IDateTime dateTime, ProstheticsDbContext dbContext)
        {
            _dateTime = dateTime;
            _dbContext = dbContext;
        }

        public async Task<int> Handle(AddOrderCommand request, CancellationToken cancellationToken)
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

            return entity.Entity.Id;
            }
            catch (Exception ex)
            {

                throw;
            }


        }
    }
}
