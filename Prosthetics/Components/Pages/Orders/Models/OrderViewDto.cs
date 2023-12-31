using Microsoft.AspNetCore.Components;
using Prosthetics.Features.Orders;

namespace Prosthetics.Components.Pages.Orders.Models
{
    public class OrderViewDto : OrderDto
    {
        public required RenderFragment Actions { get; init; }
    }
}
