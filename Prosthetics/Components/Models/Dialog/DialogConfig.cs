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

        public DialogConfig ClearViewParameters()
        {
            ViewParameters.Clear();

            return this;
        }

        public DialogConfig AddViewParameter(string key, object value)
        {
            if (ViewParameters.TryGetValue(key, out object _value))
                _value = value;
            else
                ViewParameters.TryAdd(key, value);

            return this;
        }
    }
}
