using System;
using System.Windows;
using System.Windows.Controls;
using TeleBadgeuse;
using Xunit;

namespace TeleBadgeuseTestUnit.View
{
    /// <summary>
    /// Tests unitaires pour la classe LeftNavBar.
    /// </summary>
    [Collection(WpfTestCollection.Name)]
    public class LeftNavBarTests
    {
        private readonly WpfTestHost _wpfTestHost;

        public LeftNavBarTests(WpfTestHost wpfTestHost)
        {
            _wpfTestHost = wpfTestHost;
        }

        #region Constructeur Tests

        [Fact]
        public void LeftNavBarConstructor_WithDefaultSetup_InitializesNavigationButtons()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange

                // Act
                var leftNavBar = new LeftNavBar();

                // Assert
                Assert.NotNull(leftNavBar.FindName("ButtonHome"));
                Assert.NotNull(leftNavBar.FindName("ButtonProfil"));
                Assert.NotNull(leftNavBar.FindName("ButtonSettings"));
            });
        }

        #endregion

        #region Méthodes Tests

        [Fact]
        public void HomeButtonClick_WhenInvoked_NavigatesToHomePage()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var leftNavBar = new LeftNavBar();
                var mainWindow = new MainWindow();
                var mainFrame = Assert.IsType<Frame>(mainWindow.FindName("MainFrame"));
                mainFrame.NavigationFailed += (_, args) => args.Handled = true;
                Application.Current.MainWindow = mainWindow;
                var expectedUri = new Uri("View/Page/HomePage.xaml", UriKind.Relative);

                // Act
                leftNavBar.HomeButton_Click(null, null);

                // Assert
                Assert.Equal(expectedUri, mainFrame.Source);
            });
        }

        [Fact]
        public void ProfileButtonClick_WhenInvoked_NavigatesToProfilePage()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var leftNavBar = new LeftNavBar();
                var mainWindow = new MainWindow();
                var mainFrame = Assert.IsType<Frame>(mainWindow.FindName("MainFrame"));
                mainFrame.NavigationFailed += (_, args) => args.Handled = true;
                Application.Current.MainWindow = mainWindow;
                var expectedUri = new Uri("View/Page/ProfilePage.xaml", UriKind.Relative);

                // Act
                leftNavBar.ProfileButton_Click(null, null);

                // Assert
                Assert.Equal(expectedUri, mainFrame.Source);
            });
        }

        [Fact]
        public void SettingsButtonClick_WhenInvoked_NavigatesToSettingsPage()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var leftNavBar = new LeftNavBar();
                var mainWindow = new MainWindow();
                var mainFrame = Assert.IsType<Frame>(mainWindow.FindName("MainFrame"));
                mainFrame.NavigationFailed += (_, args) => args.Handled = true;
                Application.Current.MainWindow = mainWindow;
                var expectedUri = new Uri("View/Page/SettingsPage.xaml", UriKind.Relative);

                // Act
                leftNavBar.SettingsButton_Click(null, null);

                // Assert
                Assert.Equal(expectedUri, mainFrame.Source);
            });
        }

        [Fact]
        public void ToggleButtonStyles_WithStyle_AppliesStyleToAllButtons()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var leftNavBar = new LeftNavBar();
                var style = new Style();
                var homeButton = Assert.IsType<Button>(leftNavBar.FindName("ButtonHome"));
                var profileButton = Assert.IsType<Button>(leftNavBar.FindName("ButtonProfil"));
                var settingsButton = Assert.IsType<Button>(leftNavBar.FindName("ButtonSettings"));

                // Act
                leftNavBar.ToggleButtonStyles(style);

                // Assert
                Assert.Same(style, homeButton.Style);
                Assert.Same(style, profileButton.Style);
                Assert.Same(style, settingsButton.Style);
            });
        }

        [Theory]
        [InlineData(true, Visibility.Visible)]
        [InlineData(false, Visibility.Collapsed)]
        public void ToggleLabelsVisibility_WithVisibilitySetting_UpdatesAllLabels(bool isVisible, Visibility expectedVisibility)
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var leftNavBar = new LeftNavBar();
                var homeLabel = Assert.IsType<Label>(leftNavBar.FindName("HomeLabel"));
                var profileLabel = Assert.IsType<Label>(leftNavBar.FindName("ProfileLabel"));
                var settingsLabel = Assert.IsType<Label>(leftNavBar.FindName("SettingsLabel"));

                // Act
                leftNavBar.ToggleLabelsVisibility(isVisible);

                // Assert
                Assert.Equal(expectedVisibility, homeLabel.Visibility);
                Assert.Equal(expectedVisibility, profileLabel.Visibility);
                Assert.Equal(expectedVisibility, settingsLabel.Visibility);
            });
        }

        #endregion
    }
}
