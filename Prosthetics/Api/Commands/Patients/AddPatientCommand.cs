using JuniorDevOps.Net.Http.Requests;

namespace Prosthetics.Api.Commands.Patients
{
    public class AddPatientCommand : IHttpPostRequest<AddPatientCommand, int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Url => "patients";

        public AddPatientCommand Body => this;
    }
}
