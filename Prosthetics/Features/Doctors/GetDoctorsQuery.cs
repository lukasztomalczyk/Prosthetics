using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Persistance;
using Prosthetics.Persistance.Entities;

namespace Prosthetics.Features.Doctors
{
    public class GetDoctorsQuery : IRequest<IEnumerable<DoctorDto>>
    {
    }

    public class GetDoctorsQueryHandler : IRequestHandler<GetDoctorsQuery, IEnumerable<DoctorDto>>
    {
        private readonly ProstheticsDbContext _dbContext;

        public GetDoctorsQueryHandler(ProstheticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<DoctorDto>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
        {
            // TODO do usuniecia
            _dbContext.Database.EnsureCreated();
            var result = await _dbContext.Doctors.ToListAsync();

            return result.Adapt<IEnumerable<DoctorDto>>();
        }
    }

    public class DoctorDto : IRegister
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<DoctorEntity, DoctorDto>()
                .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}");
        }
    }
}
