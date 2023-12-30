using Microsoft.AspNetCore.Components;
using Prosthetics.Features.Orders;

namespace Prosthetics.Components.Pages.Orders.ViewModels
{
    public class OrderView : OrderDto
    {
        public required RenderFragment Actions { get; init; }
    }
}
