using Microsoft.AspNetCore.Components;
using Prosthetics.Components.Pages.Doctors.DoctorsViews;
using Prosthetics.Components.Pages.Orders.Views;

namespace Prosthetics.Common
{
    public static class RenderHelper
    {
        public static RenderFragment Generate<TComponent, TType>(params RenderComponentData[] componentParams)
           where TComponent : ComponentBase
            {
                return BuildComponent(typeof(TComponent), componentParams);
            }

        public static RenderFragment Generate<TComponet>(params RenderComponentData[] componentParams)
            where TComponet : ComponentBase 
                => BuildComponent(typeof(TComponet), componentParams);

        private static RenderFragment BuildComponent(Type type, params RenderComponentData[] componentParams) =>
            builder =>
            {
                var index = 1;
                builder.OpenComponent(index, type);

                foreach (var param in componentParams)
                    builder.AddAttribute(++index, param.ParameterName, param.Value);

                builder.CloseComponent();
            };

    }

    public record RenderComponentData(string ParameterName, object Value);
}
