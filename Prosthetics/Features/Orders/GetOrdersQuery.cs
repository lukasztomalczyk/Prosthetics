﻿using Mapster;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Prosthetics.Extensions;
using Prosthetics.Features.AdditionalWorks;
using Prosthetics.Models;
using Prosthetics.Persistance;
using Prosthetics.Persistance.Entities;

namespace Prosthetics.Features.Orders
{
    public class GetOrdersQuery : IRequest<IEnumerable<OrderDto>>
    {
        public int DoctorId { get; set; }
    }

    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>
    {
        private readonly ProstheticsDbContext _dbContext;

        public GetOrdersQueryHandler(ProstheticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Orders
                .Include(_ => _.AdditionalWorks).Include(_ => _.Patient).Include(_ => _.OrderType)
                .AsNoTracking()
                .Where(_ => _.DoctorId == request.DoctorId).ToListAsync();

            return result.Adapt<IEnumerable<OrderDto>>();
        }
    }

    public class OrderDto : IRegister
    {
        public int Id { get; set; }
        public required string PatientFullName { get; set; }
        public required string OrderDate { get; set; }
        public required string DeadLine { get; set; }
        public required string Type { get; set; }
        public List<AdditionalWorkDto> AdditionalWorks { get; set; } = new List<AdditionalWorkDto>();
        public int AdditionalWorksCount { get; set; }
        public string? Comments { get; set; }
        public required string ShortComment { get; set; }
        public required string Status { get; set; }
        public int OrderStatusId { get; set; }


        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Order, OrderDto>()
                .Map(dest => dest.PatientFullName, src => src.Patient == null ? string.Empty : $"{src.Patient.LastName} {src.Patient.FirstName}")
                .Map(dest => dest.OrderDate, src => src.InsertedDate.ToString("dd-MM-yyyy"))
                .Map(dest => dest.DeadLine, src => src.DeadLine.ToString("dd-MM-yyyy"))
                .Map(dest => dest.Status, src => src.Status.GetStatus())
                .Map(dest => dest.Type, src => src.OrderType != null ? src.OrderType.Name : string.Empty)
                .Map(dest => dest.OrderStatusId, src => (int)src.Status);
        }
    }
}
