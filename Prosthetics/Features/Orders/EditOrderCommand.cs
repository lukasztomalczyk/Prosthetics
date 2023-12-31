﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Persistance;

namespace Prosthetics.Features.Orders
{
    public class EditOrderCommand
        : IRequest 
    {
        public string? Comments { get; set; }
        public int OrderId { get; set; }
    }

    public class EditCommentCommandHandler : IRequestHandler<EditOrderCommand, Unit>
    {
        private readonly ProstheticsDbContext _dbContext;

        public EditCommentCommandHandler(ProstheticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(EditOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(_ => _.Id == request.OrderId);

            if (order != null && request.Comments != null)
            {
                order.Comments = request.Comments;

                await _dbContext.SaveChangesAsync();
            }

            return Unit.Value;
        }
    }
}
