using Microsoft.AspNetCore.Components;

namespace Prosthetics.Components.Models.Grid
{
    public class ColumnInfo<TData>
    {
        public required string Title { get; set; }
        public required string Property { get; set; }
        public bool Hide { get; set; }
        public Func<TData, string>? Display { get; set; }
        public Func<TData, RenderFragment>? Template { get; set; }
    }
}
