namespace Prosthetics.Components.Models.Dialog
{
    public class DialogConfig
    {
        public DialogConfig(string title)
        {
            Title = title;
        }

        public Dictionary<string, object> ViewParameters { get; set; } = new();
        public string Title { get; set; }
        public bool ShowDialog { get; set; }

        public DialogConfig Show()
        {
            ShowDialog = true;

            return this;
        }

        public DialogConfig Hide()
        {
            ShowDialog = false;

            return this;
        }

        public DialogConfig ClearViewParameters()
        {
            ViewParameters.Clear();

            return this;
        }

        public DialogConfig AddViewParameter(string key, object value)
        {
            ViewParameters.TryAdd(key, value);

            return this;
        }
    }
}
