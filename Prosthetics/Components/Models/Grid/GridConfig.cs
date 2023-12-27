namespace Prosthetics.Components.Models.Grid
{
    public abstract class GridConfig<TData>
    {
        public ColumnInfo<TData>[] Data { get; private set; }

        public GridConfig(ColumnInfo<TData>[] data)
        {
            Data = data;
        }
    }
}
