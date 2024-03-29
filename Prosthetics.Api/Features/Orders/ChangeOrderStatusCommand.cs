﻿using Carter;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Api.Models;
using Prosthetics.Api.Persistance;
using Microsoft.AspNetCore.Mvc;

namespace Prosthetics.Features.Orders
{
    public class ChangeOrderStatusEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("orders/change-status", async ([FromBody] ChangeOrderStatusCommand command,
                [FromServices] IMediator mediator) => await mediator.Send(command));
        }
    }

    public class ChangeOrderStatusCommand : IRequest<Unit>
    {
        public int OrderId { get; set; }
        public int NewOrderStatusId { get; set; }
    }

    public class ChangeOrderStatusCommandHandler : IRequestHandler<ChangeOrderStatusCommand, Unit>
    {
        private readonly ProstheticsDbContext _dbContext;

        public ChangeOrderStatusCommandHandler(ProstheticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = await _dbContext.Orders.FirstOrDefaultAsync(_ => _.Id == request.OrderId);

            if (orderEntity != null)
            {
                orderEntity.Status = (OrderStatus)request.NewOrderStatusId;
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
