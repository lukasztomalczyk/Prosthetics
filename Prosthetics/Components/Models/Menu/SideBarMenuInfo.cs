namespace Prosthetics.Components.Models.Menu
{
    public class SideBarMenuInfo
    {
        public required string Text { get; init; }
        public required string Icon { get; init; }
        public string? PageUrl { get; set; }
        public bool Multiple { get; set; }
        public IEnumerable<SideBarMenuInfo> Items { get; set; } = Array.Empty<SideBarMenuInfo>();
    }
}
