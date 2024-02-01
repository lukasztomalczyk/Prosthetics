using FluentAssertions;
using Prosthetics.Common;

namespace Prosthetics.Tests.Common
{
    public class DateTimeServiceTests
    {
        [Theory]
        [InlineData("2024-01-01", "2024-01-02", 7, true)]
        [InlineData("2024-01-01", "2024-01-09", 7, false)]
        public void IsLessThen_Date(string left, string right, int days, bool expected)
        {
            // Arrange
            var leftDate = DateTime.Parse(left);
            var rightDate = DateTime.Parse(right);
            var timeService = new DateTimeService();

            // Act
            var result = timeService.IsLessThen(leftDate, rightDate, days);

            // Assert
            result.Should().Be(expected);
        }
    }
}
