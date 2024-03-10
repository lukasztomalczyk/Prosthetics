using JuniorDevOps.Net.Http.Requests;

namespace Prosthetics.Api.Commands.Orders
{
    public class ChangeOrderStatusCommand : IHttpPutRequest<ChangeOrderStatusCommand>
    {
        public int OrderId { get; set; }
        public int NewOrderStatusId { get; set; }

        public string Url => "orders/change-status";

        public ChangeOrderStatusCommand Body => this;
    }
}
