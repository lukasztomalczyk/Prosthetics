namespace Prosthetics.Components.Models.Grid
{
    public class ColumnInfo<TData>
    {
        public string? Title { get; set; }
        public string? Property { get; set; }
        public bool Hide { get; set; }
        public Func<TData, string>? Display { get; set; }
    }
}
