using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Persistance;
using Prosthetics.Persistance.Entities;

namespace Prosthetics.Features.Admin
{
    public class GetDoctorsOrdersByPatientQuery : IRequest<IEnumerable<DoctorOrdersByPatientDto>>
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }

    public class GetDoctorsOrdersByPatientEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("admin/doctors-orders", async ([FromBody] GetDoctorsOrdersByPatientQuery query,
                [FromServices] IMediator mediator) => await mediator.Send(query));
        }
    }

    public class GetDoctorsOrdersByPatientQueryHandler : IRequestHandler<GetDoctorsOrdersByPatientQuery, IEnumerable<DoctorOrdersByPatientDto>>
    {
        private readonly ProstheticsDbContext _dbContext;

        public GetDoctorsOrdersByPatientQueryHandler(ProstheticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<DoctorOrdersByPatientDto>> Handle(GetDoctorsOrdersByPatientQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Orders
                .Include(_ => _.AdditionalWorkCounts).ThenInclude(_ => _.AdditionalWork).Include(_ => _.Doctor).Include(_ => _.OrderType).Include(_ => _.Patient)
                .Where(_ => _.InsertedDate >= request.From && _.InsertedDate <= request.To)
                .ToListAsync();

            var ordersByPatient = result.Select(_ =>
            {
                var orders = _.AdditionalWorkCounts.Select(x => new OrderCountDto { OrderName = x.AdditionalWork.Name, Count = x.Count }).ToList();
                orders.Add(new OrderCountDto() { OrderName = _.OrderType.Name, Count = 1 });

                return new DoctorPatientAndOrdersDto()
                {
                    Doctor = $"{_.Doctor.LastName} {_.Doctor.FirstName}",
                    PatientFullName = $"{_.Patient.LastName} {_.Patient.FirstName}",
                    Orders = orders
                };
            });

            var groupByDoctor = ordersByPatient.GroupBy(_ => _.Doctor, _ => new PatientOrdersDto()
            {
                PatientFullName = _.PatientFullName,
                Orders = _.Orders
            })

            .ToDictionary(_ => _.Key, _ => _.ToList())
            .Select(_ => new DoctorOrdersByPatientDto()
            {
                DoctorFullName = _.Key,
                OrdersByPatients = _.Value

            }).ToList();

            foreach (var doctorOrders in groupByDoctor)
            {
                doctorOrders.Summary.AddRange(doctorOrders.OrdersByPatients.SelectMany(_ => _.Orders).GroupBy(_ => _.OrderName, _ => _)
                   .ToDictionary(_ => _.Key, _ => _.Sum(x => x.Count)).Select(_ => new OrderCountDto()
                   {
                       OrderName = _.Key,
                       Count = _.Value
                   }));
            }

            return groupByDoctor;
        }
    }

    public class PatientOrdersDto
    {
        public required string PatientFullName { get; init; }
        public IEnumerable<OrderCountDto> Orders { get; set; } = new List<OrderCountDto>();
    }

    internal class DoctorPatientAndOrdersDto : PatientOrdersDto
    {
        public string Doctor { get; set; }

    }

    public class DoctorOrdersByPatientDto
    {
        public required string DoctorFullName { get; init; }
        public IEnumerable<PatientOrdersDto> OrdersByPatients { get; set; } = new List<PatientOrdersDto>();
        public List<OrderCountDto> Summary { get; set; } = new List<OrderCountDto>();
    }

    public class OrdersSummaryDto
    {
        public IEnumerable<OrderCountDto> SummaryPerOrderNames { get; set; } = new List<OrderCountDto>();
    }

    public class OrderCountDto
    {
        public required string OrderName { get; init; }
        public int Count { get; set; }
    }

    public class AdditionalWorkByDateDto : IRegister
    {
        public string? Name { get; set; }
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AdditionalWork, AdditionalWorkByDateDto>()
                .PreserveReference(true);
        }
    }
}
