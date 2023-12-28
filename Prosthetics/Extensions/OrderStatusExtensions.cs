using Prosthetics.Models;

namespace Prosthetics.Extensions
{
    public static class OrderStatusExtensions
    {
        public static string GetStatus(this OrderStatus status)
        {
            switch (status)
            {
                case OrderStatus.New:
                    return "Nowe";
                case OrderStatus.InProgress:
                    return "W przygotowaniu";
                case OrderStatus.Canceled:
                    return "Anulowane";
                case OrderStatus.Sent:
                    return "Wysłane";
                default:
                    return "Nowe";
            }
        }
    }
}
