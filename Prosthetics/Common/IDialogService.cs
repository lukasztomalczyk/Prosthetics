using Microsoft.AspNetCore.Components;
using Prosthetics.Components.Models.Dialog;
using Radzen;

namespace Prosthetics.Common
{
    public interface IDialogService
    {
        void Setup(DialogService dialogService);
        Task OpenAsync<TComponent>(DialogConfig config) where TComponent : ComponentBase;
        void Close(dynamic? result = null);
        Task ConfirmAsync(string message, string title, Action onYes, Action<Task> onNo = null);
    }

    public class WebDialogService : IDialogService
    {
        private DialogService _dialogService;

        public void Setup(DialogService dialogService) => _dialogService = dialogService;
        public void Close(dynamic? result = null)
        {
            _dialogService.Close(result);
        }

        public async Task OpenAsync<TComponent>(DialogConfig config)
            where TComponent : ComponentBase
        {
            await _dialogService.OpenAsync<TComponent>(config.Title, config.ViewParameters);
        }

        public async Task ConfirmAsync(string message, string title, Action onYes, Action<Task> onNo = null)
        {
            var confirmResult = await _dialogService.Confirm(message, title, new ConfirmOptions() { OkButtonText = "Tak", CancelButtonText = "Nie" });

            if (confirmResult.HasValue && confirmResult.Value)
            {
                onYes.Invoke();
            }
        }
    }
}
