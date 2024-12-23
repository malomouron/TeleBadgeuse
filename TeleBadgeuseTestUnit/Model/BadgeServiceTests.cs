using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TeleBadgeuse.Data;
using TeleBadgeuse.Model;
using Xunit;

namespace TeleBadgeuseTestUnit.Model
{
    /// <summary>
    /// Tests unitaires pour la classe BadgeService.
    /// </summary>
    public class BadgeServiceTests
    {
        private readonly string _databaseDirectory;
        private readonly string _connectionString;
        private readonly BadgeService _badgeService;

        public BadgeServiceTests()
        {
            _databaseDirectory = Path.Combine(Path.GetTempPath(), $"TeleBadgeuseBadgeServiceTests_{Guid.NewGuid():N}");
            Directory.CreateDirectory(_databaseDirectory);
            _connectionString = $"Data Source={Path.Combine(_databaseDirectory, "data.db")};Pooling=False";
            _badgeService = new BadgeService(_connectionString);
        }

        #region AddBadge Tests

        [Fact]
        public void AddBadge_ValidBadge_AddsBadgeToDatabase()
        {
            // Arrange
            var badge = new Badge { Date = DateTime.Now, IsStart = true };

            // Act
            _badgeService.AddBadge(badge);

            // Assert
            var retrievedBadge = _badgeService.GetBadgeById(1);
            Assert.NotNull(retrievedBadge);
            Assert.Equal(badge.Date, retrievedBadge.Date);
            Assert.Equal(badge.IsStart, retrievedBadge.IsStart);
        }

        #endregion

        #region GetBadgeById Tests

        [Fact]
        public void GetBadgeById_ExistingId_ReturnsCorrectBadge()
        {
            // Arrange
            var badge = new Badge { Date = DateTime.Now, IsStart = true };
            _badgeService.AddBadge(badge);

            // Act
            var retrievedBadge = _badgeService.GetBadgeById(1);

            // Assert
            Assert.NotNull(retrievedBadge);
            Assert.Equal(badge.Date, retrievedBadge.Date);
            Assert.Equal(badge.IsStart, retrievedBadge.IsStart);
        }

        [Fact]
        public void GetBadgeById_NonExistingId_ReturnsNull()
        {
            // Act
            var retrievedBadge = _badgeService.GetBadgeById(999);

            // Assert
            Assert.Null(retrievedBadge);
        }

        #endregion

        #region GetAllBadges Tests

        [Fact]
        public void GetAllBadges_NoBadges_ReturnsEmptyCollection()
        {
            // Act
            var badges = _badgeService.GetAllBadges();

            // Assert
            Assert.Empty(badges);
        }

        [Fact]
        public void GetAllBadges_WithBadges_ReturnsAllBadges()
        {
            // Arrange
            var badge1 = new Badge { Date = DateTime.Now, IsStart = true };
            var badge2 = new Badge { Date = DateTime.Now, IsStart = false };
            _badgeService.AddBadge(badge1);
            _badgeService.AddBadge(badge2);

            // Act
            var badges = _badgeService.GetAllBadges();

            // Assert
            Assert.Equal(2, badges.Count());
        }

        #endregion

        #region getBadgesByYear Tests

        [Fact]
        public void getBadgesByYear_ValidYear_ReturnsCorrectBadges()
        {
            // Arrange
            var badge1 = new Badge { Date = new DateTime(2023, 1, 1), IsStart = true };
            var badge2 = new Badge { Date = new DateTime(2023, 2, 1), IsStart = false };
            var badge3 = new Badge { Date = new DateTime(2022, 1, 1), IsStart = true };
            _badgeService.AddBadge(badge1);
            _badgeService.AddBadge(badge2);
            _badgeService.AddBadge(badge3);

            // Act
            var badges = _badgeService.getBadgesByYear(2023);

            // Assert
            Assert.Equal(2, badges.Count());
        }

        [Fact]
        public void getBadgesByYear_InvalidYear_ReturnsEmptyCollection()
        {
            // Act
            var badges = _badgeService.getBadgesByYear(9999);

            // Assert
            Assert.Empty(badges);
        }

        #endregion

        #region getBadgesByMonthYear Tests

        [Fact]
        public void getBadgesByMonthYear_ValidMonthYear_ReturnsCorrectBadges()
        {
            // Arrange
            var badge1 = new Badge { Date = new DateTime(2023, 1, 1), IsStart = true };
            var badge2 = new Badge { Date = new DateTime(2023, 1, 2), IsStart = false };
            var badge3 = new Badge { Date = new DateTime(2023, 2, 1), IsStart = true };
            _badgeService.AddBadge(badge1);
            _badgeService.AddBadge(badge2);
            _badgeService.AddBadge(badge3);

            // Act
            var badges = _badgeService.getBadgesByMonthYear(1, 2023);

            // Assert
            Assert.Equal(2, badges.Count());
        }

        [Fact]
        public void getBadgesByMonthYear_InvalidMonthYear_ReturnsEmptyCollection()
        {
            // Act
            var badges = _badgeService.getBadgesByMonthYear(13, 9999);

            // Assert
            Assert.Empty(badges);
        }

        #endregion

        #region getBadgesByDayMonthYear Tests

        [Fact]
        public void getBadgesByDayMonthYear_ValidDayMonthYear_ReturnsCorrectBadges()
        {
            // Arrange
            var badge1 = new Badge { Date = new DateTime(2023, 1, 1), IsStart = true };
            var badge2 = new Badge { Date = new DateTime(2023, 1, 1), IsStart = false };
            var badge3 = new Badge { Date = new DateTime(2023, 1, 2), IsStart = true };
            _badgeService.AddBadge(badge1);
            _badgeService.AddBadge(badge2);
            _badgeService.AddBadge(badge3);

            // Act
            var badges = _badgeService.getBadgesByDayMonthYear(1, 1, 2023);

            // Assert
            Assert.Equal(2, badges.Count());
        }

        [Fact]
        public void getBadgesByDayMonthYear_InvalidDayMonthYear_ReturnsEmptyCollection()
        {
            // Act
            var badges = _badgeService.getBadgesByDayMonthYear(32, 13, 9999);

            // Assert
            Assert.Empty(badges);
        }

        #endregion

        #region UpdateBadge Tests

        [Fact]
        public void UpdateBadge_ValidBadge_UpdatesBadgeInDatabase()
        {
            // Arrange
            var badge = new Badge { Date = DateTime.Now, IsStart = true };
            _badgeService.AddBadge(badge);
            var updatedBadge = new Badge { Id = 1, Date = DateTime.Now.AddHours(1), IsStart = false };

            // Act
            _badgeService.UpdateBadge(updatedBadge);

            // Assert
            var retrievedBadge = _badgeService.GetBadgeById(1);
            Assert.NotNull(retrievedBadge);
            Assert.Equal(updatedBadge.Date, retrievedBadge.Date);
            Assert.Equal(updatedBadge.IsStart, retrievedBadge.IsStart);
        }

        #endregion

        #region DeleteBadge Tests

        [Fact]
        public void DeleteBadge_ExistingId_DeletesBadgeFromDatabase()
        {
            // Arrange
            var badge = new Badge { Date = DateTime.Now, IsStart = true };
            _badgeService.AddBadge(badge);

            // Act
            _badgeService.DeleteBadge(1);

            // Assert
            var retrievedBadge = _badgeService.GetBadgeById(1);
            Assert.Null(retrievedBadge);
        }

        [Fact]
        public void DeleteBadge_NonExistingId_DoesNothing()
        {
            // Act
            _badgeService.DeleteBadge(999);

            // Assert
            // No exception should be thrown
        }

        #endregion
    }
}
