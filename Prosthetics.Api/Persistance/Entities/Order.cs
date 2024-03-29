﻿using Prosthetics.Api.Models;

namespace Prosthetics.Api.Persistance.Entities
{
    public class Order : BaseEntity
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int OrderTypeId { get; set; }
        public OrderType OrderType { get; set; }
        //[NotMapped]
        //public List<AdditionalWorkOrder>? AdditionalWorkEntityOrders { get; } = new();
        public List<AdditionalWorkCount> AdditionalWorkCounts { get; set; } = new();
        public DateTime InsertedDate { get; set; }
        public DateTime DeadLine { get; set; }
        public string? Comments { get; set; }
        public OrderStatus Status { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
