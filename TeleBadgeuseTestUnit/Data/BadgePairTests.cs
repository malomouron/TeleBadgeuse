using System;
using TeleBadgeuse.Data;
using Xunit;

namespace TeleBadgeuseTestUnit.Data
{
    /// <summary>
    /// Tests unitaires pour la classe BadgePair.
    /// </summary>
    public class BadgePairTests
    {
        #region StartBadge Tests

        [Fact]
        public void StartBadge_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var badgePair = new BadgePair();
            var expectedBadge = new Badge { Id = 1, Date = DateTime.Now, IsStart = true };

            // Act
            badgePair.StartBadge = expectedBadge;
            var actualBadge = badgePair.StartBadge;

            // Assert
            Assert.Equal(expectedBadge, actualBadge);
        }

        #endregion

        #region EndBadge Tests

        [Fact]
        public void EndBadge_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var badgePair = new BadgePair();
            var expectedBadge = new Badge { Id = 2, Date = DateTime.Now, IsStart = false };

            // Act
            badgePair.EndBadge = expectedBadge;
            var actualBadge = badgePair.EndBadge;

            // Assert
            Assert.Equal(expectedBadge, actualBadge);
        }

        #endregion

        #region HoursDifference Tests

        [Fact]
        public void HoursDifference_WithValidBadges_ReturnsCorrectValue()
        {
            // Arrange
            var startBadge = new Badge { Date = new DateTime(2023, 10, 1, 8, 0, 0) };
            var endBadge = new Badge { Date = new DateTime(2023, 10, 1, 17, 0, 0) };
            var badgePair = new BadgePair { StartBadge = startBadge, EndBadge = endBadge };
            double expectedHoursDifference = 9;

            // Act
            double actualHoursDifference = badgePair.HoursDifference;

            // Assert
            Assert.Equal(expectedHoursDifference, actualHoursDifference);
        }

        [Fact]
        public void HoursDifference_WithNullStartBadge_ReturnsZero()
        {
            // Arrange
            var endBadge = new Badge { Date = new DateTime(2023, 10, 1, 17, 0, 0) };
            var badgePair = new BadgePair { StartBadge = null, EndBadge = endBadge };

            // Act
            double actualHoursDifference = badgePair.HoursDifference;

            // Assert
            Assert.Equal(0, actualHoursDifference);
        }

        [Fact]
        public void HoursDifference_WithNullEndBadge_ReturnsZero()
        {
            // Arrange
            var startBadge = new Badge { Date = new DateTime(2023, 10, 1, 8, 0, 0) };
            var badgePair = new BadgePair { StartBadge = startBadge, EndBadge = null };

            // Act
            double actualHoursDifference = badgePair.HoursDifference;

            // Assert
            Assert.Equal(0, actualHoursDifference);
        }

        [Fact]
        public void HoursDifference_WithNullBadges_ReturnsZero()
        {
            // Arrange
            var badgePair = new BadgePair { StartBadge = null, EndBadge = null };

            // Act
            double actualHoursDifference = badgePair.HoursDifference;

            // Assert
            Assert.Equal(0, actualHoursDifference);
        }

        #endregion
    }
}
