using JuniorDevOps.Net.Http.Requests;
using Prosthetics.ApiModels;

namespace Prosthetics.Api.Commands.Orders
{
    public class AddOrderCommand : IHttpPostRequest<AddOrderCommand>
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int OrderTypeId { get; set; }
        public required List<AdditionalWorkCountDto> AdditionalWorks { get; init; }
        public DateTime DeadLine { get; set; }

        public string Url => "orders/add";

        public AddOrderCommand Body => this;
    }
}
