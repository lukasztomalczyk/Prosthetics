using JuniorDevOps.Net.Http.Builders;
using JuniorDevOps.Net.Http.Requests;

namespace Prosthetics.Api.Commands.Doctors
{
    public class DeleteDoctorCommand : IHttpDeleteQueryRequest
    {
        public int DoctorId { get; set; }

        public string Url => "doctors";

        public Action<RequestQueryBuilder> Query => _ => _.AppendParameter(this, _ => _.DoctorId);
    }
}
