using DocumentFormat.OpenXml.Bibliography;
using FluentAssertions;
using Prosthetics.Common;

namespace Prosthetics.Tests.Common
{
    public class DateTimeServiceTests
    {
        [Theory]
        [InlineData("2024-01-01", "2024-01-02", 7, true)]
        [InlineData("2024-01-01", "2024-01-09", 7, false)]
        [InlineData("2024-01-01", "2024-01-08", 7, true)]
        public void IsLessThen_Date(string left, string right, int days, bool expected)
        {
            // Arrange
            var leftDate = DateTime.Parse(left);
            var rightDate = DateTime.Parse(right);
            var timeService = new DateTimeService();

            // Act
            var result = timeService.IsLessOrEqual(leftDate, rightDate, days);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("2024-01-01", "2024-01-02", 1)]
        [InlineData("2024-01-01", "2024-01-09", 8)]
        [InlineData("2024-01-01", "2024-01-08", 7)]
        public void CalculateDaysDifference(string left, string right, int expectedDays)
        {
            // Arrange
            var leftDate = DateTime.Parse(left);
            var rightDate = DateTime.Parse(right);
            var timeService = new DateTimeService();

            // Act
            var result = timeService.CalculateDaysDifference(leftDate, rightDate);

            // Assert
            result.Should().Be(expectedDays);
        }
    }
}
