﻿using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Persistance;
using Prosthetics.Persistance.Entities;

namespace Prosthetics.Features.Doctors
{
    public class AddDoctorCommand : IRequest<int>, IRegister
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddDoctorCommand, Doctor>();
        }
    }

    public class AddDoctorCommandHandler : IRequestHandler<AddDoctorCommand, int>
    {
        private readonly ProstheticsDbContext _dbContext;

        public AddDoctorCommandHandler(ProstheticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(AddDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctorEntity = await _dbContext.Doctors
                .FirstOrDefaultAsync(_ => _.FirstName == request.FirstName && _.LastName == request.LastName);

            if (doctorEntity == null)
                doctorEntity = request.Adapt<Doctor>();

            await _dbContext.Doctors.AddAsync(doctorEntity);
            await _dbContext.SaveChangesAsync();

            return doctorEntity.Id;
        }
    }
}
