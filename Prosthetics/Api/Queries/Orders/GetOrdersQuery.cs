using JuniorDevOps.Net.Http.Builders;
using JuniorDevOps.Net.Http.Requests;
using Prosthetics.ApiModels;

namespace Prosthetics.Api.Queries.Orders
{
    public class GetOrdersQuery : IHttpGetQueryRequest<IEnumerable<OrderDto>>
    {
        public int DoctorId { get; set; }

        public string Url => "orders";

        public Action<RequestQueryBuilder> Query => _ => _.AppendParameter(this, _ => _.DoctorId);
    }
}
