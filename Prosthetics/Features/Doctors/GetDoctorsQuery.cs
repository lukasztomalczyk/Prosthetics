using JuniorDevOps.Net.SqlLite;
using Mapster;
using MediatR;
using Prosthetics.Persistance.Entities;
using ServiceStack.Data;

namespace Prosthetics.Features.Doctors
{
    public class GetDoctorsQuery : IRequest<IEnumerable<DoctorDto>>
    {
    }

    public class GetDoctorsQueryHandler : IRequestHandler<GetDoctorsQuery, IEnumerable<DoctorDto>>
    {
        private readonly ISqlLiteRepository<Doctor> sqlLiteRepository;

        public GetDoctorsQueryHandler(ISqlLiteRepository<Doctor> sqlLiteRepository)
        {
            Console.Write("ss");
            this.sqlLiteRepository = sqlLiteRepository;
        }

        public async Task<IEnumerable<DoctorDto>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
        {
            // TODO do usuniecia
            var result = await sqlLiteRepository.GetListAsync();

            return result.Adapt<IEnumerable<DoctorDto>>();
        }

    }

    public class DoctorDto : IRegister
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Doctor, DoctorDto>()
                .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}");
        }
    }
}
