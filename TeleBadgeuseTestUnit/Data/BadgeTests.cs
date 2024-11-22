using System;
using TeleBadgeuse.Data;
using Xunit;

namespace TeleBadgeuseTestUnit.Data
{
    /// <summary>
    /// Tests unitaires pour la classe Badge.
    /// </summary>
    public class BadgeTests
    {
        #region Id Tests

        [Fact]
        public void Id_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var badge = new Badge();
            int expectedId = 123;

            // Act
            badge.Id = expectedId;
            int actualId = badge.Id;

            // Assert
            Assert.Equal(expectedId, actualId);
        }

        #endregion

        #region Date Tests

        [Fact]
        public void Date_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var badge = new Badge();
            DateTime expectedDate = new DateTime(2023, 10, 1);

            // Act
            badge.Date = expectedDate;
            DateTime actualDate = badge.Date;

            // Assert
            Assert.Equal(expectedDate, actualDate);
        }

        #endregion

        #region IsStart Tests

        [Fact]
        public void IsStart_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var badge = new Badge();
            bool expectedIsStart = true;

            // Act
            badge.IsStart = expectedIsStart;
            bool actualIsStart = badge.IsStart;

            // Assert
            Assert.Equal(expectedIsStart, actualIsStart);
        }

        #endregion

        #region IsEnd Tests

        [Fact]
        public void IsEnd_SetToTrue_SetsIsStartToFalse()
        {
            // Arrange
            var badge = new Badge();

            // Act
            badge.IsEnd = true;

            // Assert
            Assert.False(badge.IsStart);
        }

        [Fact]
        public void IsEnd_SetToFalse_SetsIsStartToTrue()
        {
            // Arrange
            var badge = new Badge();

            // Act
            badge.IsEnd = false;

            // Assert
            Assert.True(badge.IsStart);
        }

        [Fact]
        public void IsEnd_Get_ReturnsCorrectValue()
        {
            // Arrange
            var badge = new Badge { IsStart = false };

            // Act
            bool isEnd = badge.IsEnd;

            // Assert
            Assert.True(isEnd);
        }

        #endregion
    }
}
