namespace Prosthetics.Common
{
    public interface IDateTime
    {
        DateTime Now();
        bool IsLessThen(DateTime from, DateTime to, int days);
    }

    public class DateTimeService : IDateTime
    {
        public DateTime Now() => DateTime.Now;
        public bool IsLessThen(DateTime from, DateTime to, int days)
        {
            var diffResult = from.Subtract(to);

            var test = days > diffResult.Days;
            return days > diffResult.Days;
        }
    }
}
