using JuniorDevOps.Net.Http.Requests;
using Prosthetics.ApiModels;

namespace Prosthetics.Api.Commands.AdditionalWorks
{
    public class UpdateAdditionalWorksCommand : IHttpPutRequest<UpdateAdditionalWorksCommand>
    {
        public int OrderId { get; set; }
        public IEnumerable<EditedAdditionalCountWorkDto> AdditionalCountWorks { get; set; } = new List<EditedAdditionalCountWorkDto>();

        public string Url => "asdditonal-works";

        public UpdateAdditionalWorksCommand Body => this;
    }
}
