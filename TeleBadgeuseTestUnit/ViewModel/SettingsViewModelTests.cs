using Moq;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using TeleBadgeuse;
using TeleBadgeuse.Data;
using TeleBadgeuse.Model;
using Xunit;

namespace TeleBadgeuseTestUnit.ViewModel
{
    /// <summary>
    /// Tests unitaires pour la classe SettingsViewModel.
    /// </summary>
    public class SettingsViewModelTests
    {
        private readonly Mock<ISettingsService> _mockSettingsService;
        private readonly SettingsViewModel _viewModel;

        public SettingsViewModelTests()
        {
            _mockSettingsService = new Mock<ISettingsService>();
            _viewModel = new SettingsViewModel(_mockSettingsService.Object, true);
        }

        #region LoadSettings Tests

        [Fact]
        public void LoadSettings_ShouldLoadAllSettings()
        {
            // Arrange
            var settingsList = new List<Settings>
            {
                new Settings { Key = "Theme", Value = "DarkTheme" },
                new Settings { Key = "MinHours", Value = "8" }
            };
            _mockSettingsService.Setup(s => s.GetAllSettings()).Returns(settingsList);

            // Act
            _viewModel.LoadSettings();

            // Assert
            Assert.Equal(settingsList.Count, _viewModel.Settings.Count);
        }

        #endregion

        #region AddSettings Tests

        [Fact]
        public void AddSettings_ShouldAddNewSetting()
        {
            // Arrange
            var key = "NewSetting";
            var value = "NewValue";

            // Act
            _viewModel.AddSettingsCommand.Execute((key, value));

            // Assert
            _mockSettingsService.Verify(s => s.AddSettings(It.Is<Settings>(st => st.Key == key && st.Value == value)), Times.Once);
        }

        #endregion

        #region UpdateSettings Tests

        [Fact]
        public void UpdateSettings_ShouldUpdateExistingSetting()
        {
            // Arrange
            var key = "Theme";
            var value = "LightTheme";

            // Act
            _viewModel.UpdateSettingsCommand.Execute((key, value));

            // Assert
            _mockSettingsService.Verify(s => s.UpdateSettings(It.Is<Settings>(st => st.Key == key && st.Value == value)), Times.Once);
        }

        #endregion

        #region DeleteSettings Tests

        [Fact]
        public void DeleteSettings_ShouldDeleteSetting()
        {
            // Arrange
            var key = "Theme";

            // Act
            _viewModel.DeleteSettingsCommand.Execute(key);

            // Assert
            _mockSettingsService.Verify(s => s.DeleteSettings(key), Times.Once);
        }

        #endregion

        #region SelectSettingsKey Tests

        [Fact]
        public void SelectSettingsKey_ShouldReturnCorrectSetting()
        {
            // Arrange
            var key = "Theme";
            var expectedSetting = new Settings { Key = key, Value = "DarkTheme" };
            _mockSettingsService.Setup(s => s.GetSettings(key)).Returns(expectedSetting);

            // Act
            var actualSetting = _viewModel.SelectSettingsKey(key);

            // Assert
            Assert.Equal(expectedSetting, actualSetting);
        }

        #endregion

        #region SelectSettingsValue Tests

        [Fact]
        public void SelectSettingsValue_ShouldReturnCorrectSettings()
        {
            // Arrange
            var value = "DarkTheme";
            var expectedSettings = new List<Settings>
            {
                new Settings { Key = "Theme", Value = value }
            };
            _mockSettingsService.Setup(s => s.GetSettingsByValue(value)).Returns(expectedSettings);

            // Act
            var actualSettings = _viewModel.SelectSettingsValue(value);

            // Assert
            Assert.Equal(expectedSettings.Count, actualSettings.Count());
        }

        #endregion

        #region DarkTheme Tests

        [Fact]
        public void DarkTheme_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var expectedValue = true;

            // Act
            _viewModel.DarkTheme = expectedValue;
            var actualValue = _viewModel.DarkTheme;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region NbrHoursDays Tests

        [Fact]
        public void NbrHoursDays_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var expectedValue = 8;

            // Act
            _viewModel.NbrHoursDays = expectedValue;
            var actualValue = _viewModel.NbrHoursDays;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

        #region ImageSource Tests

        [Fact]
        public void ImageSource_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var expectedValue = "/Ressources/Icons/IconDark.png";

            // Act
            _viewModel.ImageSource = expectedValue;
            var actualValue = _viewModel.ImageSource;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        #endregion

    }
}
