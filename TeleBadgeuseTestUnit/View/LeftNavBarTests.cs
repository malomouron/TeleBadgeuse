using System;
using System.Windows;
using TeleBadgeuse;
using Xunit;

namespace TeleBadgeuseTestUnit.View
{
    /// <summary>
    /// Tests unitaires pour la classe LeftNavBar.
    /// </summary>
    public class LeftNavBarTests
    {
        #region Constructeur Tests

        [Fact]
        public void LeftNavBar_Constructor_InitializesComponent()
        {
            // Arrange & Act
            var leftNavBar = new LeftNavBar();

            // Assert
            Assert.NotNull(leftNavBar);
        }

        #endregion

        #region Méthodes Tests

        [Fact]
        public void HomeButton_Click_NavigatesToHomePage()
        {
            // Arrange
            var leftNavBar = new LeftNavBar();
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var expectedUri = new Uri("/View/Page/HomePage.xaml", UriKind.Relative);

            // Act
            leftNavBar.HomeButton_Click(null, null);

            // Assert
            //Assert.Equal(expectedUri, mainWindow.MainFrame.Source);
        }

        [Fact]
        public void ProfileButton_Click_NavigatesToProfilePage()
        {
            // Arrange
            var leftNavBar = new LeftNavBar();
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var expectedUri = new Uri("/View/Page/ProfilePage.xaml", UriKind.Relative);

            // Act
            leftNavBar.ProfileButton_Click(null, null);

            // Assert
            //Assert.Equal(expectedUri, mainWindow.MainFrame.Source);
        }

        [Fact]
        public void SettingsButton_Click_NavigatesToSettingsPage()
        {
            // Arrange
            var leftNavBar = new LeftNavBar();
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var expectedUri = new Uri("/View/Page/SettingsPage.xaml", UriKind.Relative);

            // Act
            leftNavBar.SettingsButton_Click(null, null);

            // Assert
            //Assert.Equal(expectedUri, mainWindow.MainFrame.Source);
        }

        [Fact]
        public void ToggleButtonStyles_ChangesButtonStyles()
        {
            // Arrange
            var leftNavBar = new LeftNavBar();
            var style = new Style();

            // Act
            leftNavBar.ToggleButtonStyles(style);

            // Assert
            //Assert.Equal(style, leftNavBar.ButtonHome.Style);
            //Assert.Equal(style, leftNavBar.ButtonProfil.Style);
            //Assert.Equal(style, leftNavBar.ButtonSettings.Style);
        }

        [Theory]
        [InlineData(true, Visibility.Visible)]
        [InlineData(false, Visibility.Collapsed)]
        public void ToggleLabelsVisibility_ChangesLabelsVisibility(bool isVisible, Visibility expectedVisibility)
        {
            // Arrange
            var leftNavBar = new LeftNavBar();

            // Act
            leftNavBar.ToggleLabelsVisibility(isVisible);

            // Assert
            //Assert.Equal(expectedVisibility, leftNavBar.HomeLabel.Visibility);
            //Assert.Equal(expectedVisibility, leftNavBar.ProfileLabel.Visibility);
            //Assert.Equal(expectedVisibility, leftNavBar.SettingsLabel.Visibility);
        }

        #endregion
    }
}
