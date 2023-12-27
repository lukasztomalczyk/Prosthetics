using Microsoft.AspNetCore.Components;
using Prosthetics.Components.Models.Dialog;
using Radzen;

namespace Prosthetics.Common
{
    public interface IDialogService
    {
        void Setup(DialogService dialogService);
        Task OpenAsync<TComponent>(Type comopenntType, DialogConfig config) where TComponent : ComponentBase;
        void Close(Type comopenntType);
    }

    public class WebDialogService : IDialogService
    {
        private DialogService _dialogService;
        private IDictionary<Type, dynamic?> _dialogList = new Dictionary<Type, dynamic?>();

        public void Setup(DialogService dialogService) => _dialogService = dialogService;
        public void Close(Type componentType)
        {
            if (_dialogList.TryGetValue(componentType, out dynamic? value))
                _dialogService.Close(value);
        }

        public async Task OpenAsync<TComponent>(Type componentType, DialogConfig config)
            where TComponent : ComponentBase
        {
            var result = await _dialogService.OpenAsync<TComponent>(config.Title, config.ViewParameters);

            _dialogList.Add(componentType, result);
        }
    }
}
