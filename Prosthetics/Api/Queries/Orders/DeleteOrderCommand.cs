using JuniorDevOps.Net.Http.Builders;
using JuniorDevOps.Net.Http.Requests;

namespace Prosthetics.Api.Queries.Orders
{
    public class DeleteOrderCommand : IHttpDeleteQueryRequest
    {
        public int OrderId { get; set; }

        public string Url => "orders";

        public Action<RequestQueryBuilder> Query => _ => _.AppendParameter(this, _ => _.OrderId);
    }
}
