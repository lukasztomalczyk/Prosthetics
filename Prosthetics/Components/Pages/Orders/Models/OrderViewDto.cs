using Microsoft.AspNetCore.Components;
using Prosthetics.Components.Layout.Abstractions;
using Prosthetics.Features.Orders;

namespace Prosthetics.Components.Pages.Orders.Models
{
    public class OrderViewDto : OrderDto, IGridData
    {
        public RenderFragment Actions { get; set; }
        public string? ClassRow { get; set; }
    }
}
