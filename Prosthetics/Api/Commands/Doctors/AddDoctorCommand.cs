﻿using JuniorDevOps.Net.Http.Requests;
using System.Text.Json.Serialization;

namespace Prosthetics.Api.Commands.Doctors
{
    public class AddDoctorCommand : IHttpPostRequest<AddDoctorCommand>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Url => "doctors";
        public AddDoctorCommand Body => this;
    }
}
