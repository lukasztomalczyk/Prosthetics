﻿namespace Prosthetics.Persistance.Entities
{
    public class Doctor : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Order> Orders { get; set; }
    }
}
