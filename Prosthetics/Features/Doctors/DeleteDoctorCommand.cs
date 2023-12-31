﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Persistance;
using Prosthetics.Persistance.Entities;

namespace Prosthetics.Features.Doctors
{
    public class DeleteDoctorCommand : IRequest<Unit>
    {
        public int DoctorId { get; set; }
    }

    public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand, Unit>
    {
        private readonly ProstheticsDbContext _dbContext;

        public DeleteDoctorCommandHandler(ProstheticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctorEntityToBeDeleted = new Doctor()
            { 
                Id = request.DoctorId
            };

            _dbContext.Attach(doctorEntityToBeDeleted).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
