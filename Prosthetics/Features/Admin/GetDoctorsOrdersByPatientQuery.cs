using Mapster;
using MediatR;
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
                .Include(_ => _.AdditionalWorks).Include(_ => _.Doctor).Include(_ => _.OrderType).Include(_ => _.Patient)
                .Where(_ => _.InsertedDate >= request.From && _.InsertedDate <= request.To)
                .ToListAsync();

            var ordersByPatient = result.Select(_ =>
            {
                var orders = _.AdditionalWorks.Select(x => x.Name).ToList();
                orders.Add(_.OrderType.Name);

                return (Doctor: $"{_.Doctor.LastName} {_.Doctor.FirstName}", Patient: $"{_.Patient.LastName} {_.Patient.FirstName}", Orders: orders);
            });

            var groupByDoctor = ordersByPatient.GroupBy(_ => _.Doctor, _ => (Patient: _.Patient, Orders: _.Orders)).ToList();

            var groupedByDoctorAndByPatient = groupByDoctor.GroupBy(_ => _.Key, _ => _.GroupBy(x => x.Patient, y => y.Orders)
                .ToDictionary(z => z.Key, z => z.ToList())).ToDictionary(_ => _.Key, _ => _.SelectMany(_ => _).ToList());

            var mappedResult = groupedByDoctorAndByPatient.Select(_ => new DoctorOrdersByPatientDto
            {
                DoctorFullName = _.Key,
                OrdersByPatients = _.Value.Select(x => new ParientOrdersDto()
                { 
                    PatientFullName = x.Key,
                    Orders = x.Value.SelectMany(y => y).GroupBy(y => y).ToDictionary(y => y.Key, y => y.Count()).Select(_ => new OrderCountDto()
                    { 
                        OrderName = _.Key,
                        Count = _.Value
                    })
                })

            });

            return mappedResult;
        }
    }

    public class ParientOrdersDto
    {
        public required string PatientFullName { get; init; }
        public IEnumerable<OrderCountDto> Orders { get; set; } = new List<OrderCountDto>();
    }

    public class DoctorOrdersByPatientDto
    {
        public required string DoctorFullName { get; init; }
        public IEnumerable<ParientOrdersDto> OrdersByPatients { get; set; } = new List<ParientOrdersDto>();
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
