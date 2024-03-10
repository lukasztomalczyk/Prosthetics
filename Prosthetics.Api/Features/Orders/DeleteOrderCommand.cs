using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Persistance;
using Prosthetics.Persistance.Entities;

namespace Prosthetics.Features.Orders
{
    public class DeleteOrderCommand : IRequest<Unit>
    {
        public int OrderId { get; set; }
    }

    public class DeleteOrderCommandEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("orders/edit-order", async ([AsParameters] EditOrderCommand command,
                [FromServices] IMediator mediator) => await mediator.Send(command));
        }
    }

    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
    {
        private readonly ProstheticsDbContext _dbContext;

        public DeleteOrderCommandHandler(ProstheticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var entityToBeDelete = new Order() { Id = request.OrderId };

            var attachedEntity = _dbContext.Attach(entityToBeDelete);
            attachedEntity.State = EntityState.Deleted;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
