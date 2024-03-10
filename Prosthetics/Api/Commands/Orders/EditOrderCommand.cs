using JuniorDevOps.Net.Http.Requests;

namespace Prosthetics.Api.Commands.Orders
{
    public class EditOrderCommand : IHttpPostRequest<EditOrderCommand>
    {
        public string? Comments { get; set; }
        public int OrderId { get; set; }

        public string Url => "orders/edit-order";

        public EditOrderCommand Body => this;
    }
}
