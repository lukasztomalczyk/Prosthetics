using Microsoft.AspNetCore.Components;
using Prosthetics.Components.Models.Dialog;
using Radzen;

namespace Prosthetics.Common
{
    public interface IDialogService
    {
        void Setup(DialogService dialogService);
        Task OpenAsync<TComponent>(DialogConfig config) where TComponent : ComponentBase;
        void Close();
    }

    public class WebDialogService : IDialogService
    {
        private DialogService _dialogService;

        public void Setup(DialogService dialogService) => _dialogService = dialogService;
        public void Close()
        {
            _dialogService.Close();
        }

        public async Task OpenAsync<TComponent>(DialogConfig config)
            where TComponent : ComponentBase
        {
            await _dialogService.OpenAsync<TComponent>(config.Title, config.ViewParameters);
        }
    }
}
