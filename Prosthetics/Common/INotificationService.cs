using Radzen;

namespace Prosthetics.Common
{
    public interface INotificationService
    {
        void Success(string message, int duration = 4000);
        void Info(string message, int duration = 4000);
        void Error(string message, int duration = 4000);
    }

    public class CommonNotificationService : INotificationService
    {
        private readonly NotificationService _notificationService;

        public CommonNotificationService(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        private void Setup(NotificationSeverity type, string message, int duration)
        {
            _notificationService.Notify(new NotificationMessage()
            {
                Severity = type,
                Summary = message,
                Duration = duration
            });
        }

        public void Error(string message, int duration = 4000)
        {
            Setup(NotificationSeverity.Error, message, duration);
        }

        public void Info(string message, int duration = 4000)
        {
            Setup(NotificationSeverity.Info, message, duration);
        }

        public void Success(string message, int duration = 4000)
        {
            Setup(NotificationSeverity.Success, message, duration);
        }
    }
}
