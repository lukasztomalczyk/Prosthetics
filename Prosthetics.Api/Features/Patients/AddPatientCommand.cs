﻿using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Api.Persistance;
using Prosthetics.Api.Persistance.Entities;

namespace Prosthetics.Features.Patients
{
    public class AddPatientCommand : IRequest<int>, IRegister
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddPatientCommand, Patient>();
        }
    }

    public class AddPatientCommandEnpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("patients", async ([FromBody] AddPatientCommand command,
                [FromServices] IMediator mediator) => await mediator.Send(command));
        }
    }

    public class AddPatientCommandEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("patients", async ([FromBody] AddPatientCommand command,
                [FromServices] IMediator mediator) => await mediator.Send(command));
        }
    }

    public class AddPatientCommandHandler : IRequestHandler<AddPatientCommand, int>
    {
        private readonly ProstheticsDbContext _dbContext;

        public AddPatientCommandHandler(ProstheticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(AddPatientCommand request, CancellationToken cancellationToken)
        {
            var patientEntity = await _dbContext.Patients
                .FirstOrDefaultAsync(_ => _.FirstName == request.FirstName && _.LastName == request.LastName);

            if (patientEntity == null)
            {
                patientEntity = request.Adapt<Patient>();

                await _dbContext.Patients.AddAsync(patientEntity);
                await _dbContext.SaveChangesAsync();
            }

            return patientEntity.Id;
        }
    }

    public class PatientDto : IRegister
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Patient, PatientDto>();
        }
    }
}
