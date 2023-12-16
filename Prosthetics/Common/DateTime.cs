namespace Prosthetics.Common
{
    public interface IDateTime
    {
        DateTime Now();
    }

    public class DateTimeService : IDateTime
    {
        public DateTime Now() => DateTime.Now;
    }
}
