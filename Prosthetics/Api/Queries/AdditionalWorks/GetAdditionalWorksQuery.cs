using JuniorDevOps.Net.Http.Requests;
using Prosthetics.ApiModels;

namespace Prosthetics.Api.Queries.AdditionalWorks
{
    public class GetAdditionalWorksQuery : IHttpGetRequest<IEnumerable<AdditionalWorkCountDto>>
    {
        public string Url => "additional-works";
    }
}
