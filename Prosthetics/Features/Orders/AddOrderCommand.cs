using Mapster;
using MediatR;
using Prosthetics.Common;
using Prosthetics.Persistance;
using Prosthetics.Persistance.Entities;

namespace Prosthetics.Features.Orders
{
    public class AddOrderCommand : IRequest, IRegister
    {
        public int DoctorId { get; set; }
        public string Type { get; set; }
        public DateTime DeadLine { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddOrderCommand, OrderEntity>()
                .Map(dest => dest.Status, src => 1);
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
            var order = request.Adapt<OrderEntity>();

            order.InsertedDate = _dateTime.Now();

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
