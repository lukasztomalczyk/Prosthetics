using MediatR;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Models;
using Prosthetics.Persistance;

namespace Prosthetics.Features.Orders
{
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
            }

            return Unit.Value;
        }
    }
}
