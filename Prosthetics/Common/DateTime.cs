namespace Prosthetics.Common
{
    public interface IDateTime
    {
        DateTime Now();
        bool IsLessOrEqual(DateTime from, DateTime to, int days);
        int CalculateDaysDifference(DateTime from, DateTime to);
    }

    public class DateTimeService : IDateTime
    {
        public DateTime Now() => DateTime.Now;
        public bool IsLessOrEqual(DateTime from, DateTime to, int days)
        {
            var diffResult = to.Subtract(from);

            return days >= diffResult.Days;
        }

        public int CalculateDaysDifference(DateTime from, DateTime to)
        {
            return to.Subtract(from).Days;
        }
    }
}
