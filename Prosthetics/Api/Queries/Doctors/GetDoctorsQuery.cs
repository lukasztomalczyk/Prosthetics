using JuniorDevOps.Net.Http.Requests;
using Prosthetics.ApiModels;

namespace Prosthetics.Api.Queries.Doctors
{
    public class GetDoctorsQuery : IHttpGetRequest<IEnumerable<DoctorDto>>
    {
        public string Url => "doctors";
    }
}
