using Microsoft.AspNetCore.Components;
using Prosthetics.Features.Orders;

namespace Prosthetics.Components.Pages.Orders.Models
{
    public class OrderViewDto : OrderDto
    {
        public RenderFragment Actions { get; set; }
    }
}
