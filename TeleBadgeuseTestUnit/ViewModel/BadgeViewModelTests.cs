using Moq;
using System;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using TeleBadgeuse;
using TeleBadgeuse.Data;
using TeleBadgeuse.Model;
using Xunit;

namespace TeleBadgeuseTestUnit.ViewModel
{
    /// <summary>
    /// Tests unitaires pour la classe BadgeViewModel.
    /// </summary>
    public class BadgeViewModelTests
    {
        private readonly BadgeViewModel _viewModel;
        private readonly BadgeService _badgeService;
        private readonly string _connectionString = "Data Source=./dataVmTest.db";

        public BadgeViewModelTests()
        {
            _badgeService = new BadgeService(_connectionString);
            _viewModel = new BadgeViewModel(_badgeService, null, true);
        }


        public void Dispose()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DROP TABLE IF EXISTS Badges";
                command.ExecuteNonQuery();
            }
        }

        #region IsViewListSelected Tests

        [Fact]
        public void IsViewListSelected_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            _badgeService.InitializeDatabase();
            bool expectedValue = true;

            // Act
            _viewModel.IsViewListSelected = expectedValue;
            bool actualValue = _viewModel.IsViewListSelected;

            // Assert
            Assert.Equal(expectedValue, actualValue);
            Dispose();
        }

        #endregion

        #region IsViewGridSelected Tests

        [Fact]
        public void IsViewGridSelected_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            _badgeService.InitializeDatabase();
            bool expectedValue = true;

            // Act
            _viewModel.IsViewGridSelected = expectedValue;
            bool actualValue = _viewModel.IsViewGridSelected;

            // Assert
            Assert.Equal(expectedValue, actualValue);
            Dispose();
        }

        #endregion

        #region IsAllSelected Tests

        [Fact]
        public void IsAllSelected_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            _badgeService.InitializeDatabase();
            bool expectedValue = true;

            // Act
            _viewModel.IsAllSelected = expectedValue;
            bool actualValue = _viewModel.IsAllSelected;

            // Assert
            Assert.Equal(expectedValue, actualValue);
            Dispose();
        }

        #endregion

        #region IsYearsSelected Tests

        [Fact]
        public void IsYearsSelected_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            _badgeService.InitializeDatabase();
            bool expectedValue = true;

            // Act
            _viewModel.IsYearsSelected = expectedValue;
            bool actualValue = _viewModel.IsYearsSelected;

            // Assert
            Assert.Equal(expectedValue, actualValue);
            Dispose();
        }

        #endregion

        #region IsMonthsSelected Tests

        [Fact]
        public void IsMonthsSelected_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            _badgeService.InitializeDatabase();
            bool expectedValue = true;

            // Act
            _viewModel.IsMonthsSelected = expectedValue;
            bool actualValue = _viewModel.IsMonthsSelected;

            // Assert
            Assert.Equal(expectedValue, actualValue);
            Dispose();
        }

        #endregion

        #region IsDaysSelected Tests

        [Fact]
        public void IsDaysSelected_SetAndGet_ReturnsCorrectValue()
        {
            _badgeService.InitializeDatabase();
            // Arrange
            bool expectedValue = true;

            // Act
            _viewModel.IsDaysSelected = expectedValue;
            bool actualValue = _viewModel.IsDaysSelected;

            // Assert
            Assert.Equal(expectedValue, actualValue);

            Dispose();
        }

        #endregion

        #region IsInfoDisplay Tests

        [Fact]
        public void IsInfoDisplay_Get_ReturnsCorrectValue()
        {
            // Arrange
            _badgeService.InitializeDatabase();
            _viewModel.IsDaysSelected = true;

            // Act
            bool actualValue = _viewModel.IsInfoDisplay;

            // Assert
            Assert.True(actualValue);
            Dispose();
        }

        #endregion

        #region Badges Tests

        [Fact]
        public void Badges_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            _badgeService.InitializeDatabase();
            var expectedBadges = new ObservableCollection<Badge>
            {
                new Badge { Id = 1, Date = DateTime.Now, IsStart = true },
                new Badge { Id = 2, Date = DateTime.Now, IsStart = false }
            };

            // Act
            _viewModel.Badges = expectedBadges;
            var actualBadges = _viewModel.Badges;

            // Assert
            Assert.Equal(expectedBadges, actualBadges);
            Dispose();
        }

        #endregion

        #region BadgePairs Tests

        [Fact]
        public void BadgePairs_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            _badgeService.InitializeDatabase();
            var expectedBadgePairs = new ObservableCollection<BadgePair>
            {
                new BadgePair { StartBadge = new Badge { Id = 1, Date = DateTime.Now, IsStart = true }, EndBadge = new Badge { Id = 2, Date = DateTime.Now, IsStart = false } }
            };

            // Act
            _viewModel.BadgePairs = expectedBadgePairs;
            var actualBadgePairs = _viewModel.BadgePairs;

            // Assert
            Assert.Equal(expectedBadgePairs, actualBadgePairs);
            Dispose();
        }

        #endregion

        #region SelectedDatePreview Tests

        [Fact]
        public void SelectedDatePreview_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            _badgeService.InitializeDatabase();
            string expectedValue = "2023-10-01";

            // Act
            _viewModel.SelectedDatePreview = expectedValue;
            string actualValue = _viewModel.SelectedDatePreview;

            // Assert
            Assert.Equal(expectedValue, actualValue);
            Dispose();
        }

        #endregion

        #region TotalHours Tests

        [Fact]
        public void TotalHours_Get_ReturnsCorrectValue()
        {
            // Arrange
            _badgeService.InitializeDatabase();
            var badgePairs = new ObservableCollection<BadgePair>
            {
                new BadgePair { StartBadge = new Badge { Date = new DateTime(2023, 10, 1, 8, 0, 0) }, EndBadge = new Badge { Date = new DateTime(2023, 10, 1, 17, 0, 0) } }
            };
            _viewModel.BadgePairs = badgePairs;
            double expectedValue = 9;

            // Act
            double actualValue = _viewModel.TotalHours;

            // Assert
            Assert.Equal(expectedValue, actualValue);
            Dispose();
        }

        #endregion

        #region RemainingHours Tests

        [Fact]
        public void RemainingHours_Get_ReturnsCorrectValue()
        {
            // Arrange
            _badgeService.InitializeDatabase();
            var badgePairs = new ObservableCollection<BadgePair>
            {
                new BadgePair { StartBadge = new Badge { Date = new DateTime(2023, 10, 1, 8, 0, 0) }, EndBadge = new Badge { Date = new DateTime(2023, 10, 1, 17, 0, 0) } }
            };
            _viewModel.BadgePairs = badgePairs;
            double expectedValue = 0;

            // Act
            double actualValue = _viewModel.RemainingHours;

            // Assert
            Assert.Equal(expectedValue, actualValue);
            Dispose();
        }

        #endregion

        #region ExtraHours Tests

        [Fact]
        public void ExtraHours_Get_ReturnsCorrectValue()
        {
            // Arrange
            _badgeService.InitializeDatabase();
            var badge = new ObservableCollection<Badge> { new Badge { Id = 1, Date = new DateTime(2023, 10, 1, 8, 0, 0), IsStart = true }, new Badge { Id = 2,  Date = new DateTime(2023, 10, 1, 17, 0, 0), IsStart = false } };
            _viewModel.Badges = badge;
            _viewModel.LoadBadgesPair();
            double expectedValue = 2;

            // Act
            double actualValue = _viewModel.ExtraHours;

            // Assert
            Assert.Equal(expectedValue, actualValue);
            Dispose();
        }

        #endregion


        #region HoursMinToLeave Tests

        [Fact]
        public void HoursMinToLeave_Get_ReturnsCorrectValue()
        {
            // Arrange
            _badgeService.InitializeDatabase();
            var badge = new ObservableCollection<Badge> { new Badge { Id = 1, Date = DateTime.Now, IsStart = true } };
            _viewModel.Badges = badge;
            _viewModel.LoadBadgesPair();
            string expectedValue = DateTime.Now.AddHours(_viewModel.RemainingHours).ToString("HH:mm");

            // Act
            string actualValue = _viewModel.HoursMinToLeave;

            // Assert
            Assert.Equal(expectedValue, actualValue);
            Dispose();
        }

        #endregion

        #region AddBadgeCommand Tests

        [Fact]
        public void AddBadgeCommand_Execute_AddsBadge()
        {
            _badgeService.InitializeDatabase();
            // Arrange
            bool isStart = true;
            int initialCount = _viewModel.Badges.Count;

            // Act
            _viewModel.AddBadgeCommand.Execute(isStart);

            // Assert
            Assert.Equal(initialCount + 1, _viewModel.Badges.Count);
            
            Dispose();
        }

        #endregion

        #region UpdateBadgeCommand Tests

        [Fact]
        public void UpdateBadgeCommand_Execute_UpdatesBadge()
        {
            _badgeService.InitializeDatabase();
            // Arrange
            _viewModel.AddBadgeCommand.Execute(true);
            var updatedBadge = new Badge { Id = 1, Date = DateTime.Now.AddHours(1), IsStart = true };

            // Act
            _viewModel.UpdateBadgeCommand.Execute((updatedBadge.Id, updatedBadge.Date, updatedBadge.IsStart));

            // Assert
            var actualBadge = _viewModel.Badges.FirstOrDefault(b => b.Id == updatedBadge.Id);
            Assert.Equal(updatedBadge.Date, actualBadge.Date);

            Dispose();
        }

        #endregion

        #region DeleteBadgeCommand Tests

        [Fact]
        public void DeleteBadgeCommand_Execute_DeletesBadge()
        {
            _badgeService.InitializeDatabase();
            // Arrange
            var badge = new Badge { Id = 1, Date = DateTime.Now, IsStart = true };
            _viewModel.Badges.Add(badge);
            int initialCount = _viewModel.Badges.Count;

            // Act
            _viewModel.DeleteBadgeCommand.Execute(badge.Id);

            // Assert
            Assert.Equal(initialCount - 1, _viewModel.Badges.Count);

            Dispose();
        }

        #endregion

        #region IncrementCommand Tests

        [Fact]
        public void IncrementCommand_Execute_IncrementsDate()
        {
            _badgeService.InitializeDatabase();
            // Arrange
            string initialDate = _viewModel.SelectedDatePreview;

            // Act
            _viewModel.DecrementCommand.Execute(null);

            // Assert
            Assert.Equal(DateTime.Parse(initialDate).AddDays(-1).ToString("yyyy-MM-dd"), _viewModel.SelectedDatePreview);

            Dispose();
        }

        #endregion

        #region DecrementCommand Tests

        [Fact]
        public void DecrementCommand_Execute_DecrementsDate()
        {
            // Arrange
            _badgeService.InitializeDatabase();
            string initialDate = _viewModel.SelectedDatePreview;

            // Act
            _viewModel.DecrementCommand.Execute(null);

            // Assert
            Assert.Equal(DateTime.Parse(initialDate).AddDays(-1).ToString("yyyy-MM-dd"), _viewModel.SelectedDatePreview);
            Dispose();
        }

        #endregion
    }
}
