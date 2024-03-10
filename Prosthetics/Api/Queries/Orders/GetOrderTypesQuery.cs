using JuniorDevOps.Net.Http.Requests;
using Prosthetics.ApiModels;

namespace Prosthetics.Api.Queries
{
    public class GetOrderTypesQuery : IHttpGetRequest<IEnumerable<OrderTypeDto>>
    {
        public string Url => "orders/orders-type";
    }
}
