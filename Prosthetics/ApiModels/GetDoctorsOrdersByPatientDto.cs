using JuniorDevOps.Net.Http.Requests;

namespace Prosthetics.ApiModels
{
    public class GetDoctorsOrdersByPatientDto : IHttpPostRequest<GetDoctorsOrdersByPatientDto, IEnumerable<DoctorOrdersByPatientDto>>
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public string Url => "admin/doctors-orders";

        public GetDoctorsOrdersByPatientDto Body => this;
    }
}
