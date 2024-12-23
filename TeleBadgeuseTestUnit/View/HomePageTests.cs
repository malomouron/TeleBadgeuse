using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.Input;
using TeleBadgeuse;
using TeleBadgeuse.Data;
using TeleBadgeuse.Model;
using Xunit;

namespace TeleBadgeuseTestUnit.View
{
    /// <summary>
    /// Tests unitaires pour la classe HomePage.
    /// </summary>
    [Collection(WpfTestCollection.Name)]
    public class HomePageTests
    {
        private readonly WpfTestHost _wpfTestHost;

        public HomePageTests(WpfTestHost wpfTestHost)
        {
            _wpfTestHost = wpfTestHost;
        }

        #region Constructor Tests

        [Fact]
        public void HomePageConstructor_WithDefaultSetup_SetsBadgeViewModelAsDataContext()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange

                // Act
                var homePage = new HomePage();

                // Assert
                Assert.IsType<BadgeViewModel>(homePage.DataContext);
            });
        }

        #endregion

        #region StartClock Tests

        [Fact]
        public void StartClock_WhenInvoked_StartsTimerWithOneSecondInterval()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var homePage = new HomePage();
                var timerField = typeof(HomePage).GetField("_timer", BindingFlags.NonPublic | BindingFlags.Instance)!;

                // Act
                homePage.StartClock();

                // Assert
                var timer = Assert.IsType<DispatcherTimer>(timerField.GetValue(homePage));
                Assert.Equal(TimeSpan.FromSeconds(1), timer.Interval);
                Assert.True(timer.IsEnabled);
            });
        }

        #endregion

        #region Start Tests

        [Fact]
        public void Start_WhenInvoked_AddsStartBadge()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var homePage = new HomePage();
                var badgeViewModel = Assert.IsType<BadgeViewModel>(homePage.DataContext);

                // Act
                homePage.Start();

                // Assert
                Assert.True(Assert.Single(badgeViewModel.Badges).IsStart);
            });
        }

        #endregion

        #region End Tests

        [Fact]
        public void End_WhenInvoked_AddsEndBadge()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var homePage = new HomePage();
                var badgeViewModel = Assert.IsType<BadgeViewModel>(homePage.DataContext);
                badgeViewModel.AddBadgeCommand.Execute(true);

                // Act
                homePage.End();

                // Assert
                Assert.Equal(2, badgeViewModel.Badges.Count);
                Assert.False(badgeViewModel.Badges.Last().IsStart);
            });
        }

        #endregion

        #region Popup Tests

        [Fact]
        public void ShowConfirmationPopup_WithMessageAndCommands_DisplaysPopup()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var homePage = new HomePage();
                var message = "Test message";
                var yesCommand = new RelayCommand(() => { });
                var noCommand = new RelayCommand(() => { });

                // Act
                homePage.ShowConfirmationPopup(message, yesCommand, noCommand);

                // Assert
                var popupContentControl = Assert.IsType<ContentControl>(homePage.FindName("PopupContentControl"));
                Assert.Equal(Visibility.Visible, popupContentControl.Visibility);
                var popupViewModel = Assert.IsType<ConfirmationPopupViewModel>(Assert.IsType<Popup>(popupContentControl.Content).DataContext);
                Assert.Equal(message, popupViewModel.Message);
            });
        }

        [Fact]
        public void ShowErrorPopup_WithMessageAndCommand_DisplaysPopup()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var homePage = new HomePage();
                var message = "Test error message";
                var okayCommand = new RelayCommand(() => { });

                // Act
                homePage.ShowErrorPopup(message, okayCommand);

                // Assert
                var popupContentControl = Assert.IsType<ContentControl>(homePage.FindName("PopupContentControl"));
                Assert.Equal(Visibility.Visible, popupContentControl.Visibility);
                var popupViewModel = Assert.IsType<ErrorPopupViewModel>(Assert.IsType<PopupError>(popupContentControl.Content).DataContext);
                Assert.Equal(message, popupViewModel.Message);
            });
        }

        [Fact]
        public void HideConfirmationPopup_WhenVisible_CollapsesAndClearsPopup()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var homePage = new HomePage();
                var popupContentControl = Assert.IsType<ContentControl>(homePage.FindName("PopupContentControl"));
                popupContentControl.Visibility = Visibility.Visible;
                popupContentControl.Content = new object();

                // Act
                homePage.HideConfirmationPopup();

                // Assert
                Assert.Equal(Visibility.Collapsed, popupContentControl.Visibility);
                Assert.Null(popupContentControl.Content);
            });
        }

        #endregion

        #region StartClick Tests

        [Fact]
        public void StartClick_WhenAlreadyStarted_DisplaysErrorPopup()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var homePage = new HomePage();
                var badgeViewModel = Assert.IsType<BadgeViewModel>(homePage.DataContext);
                badgeViewModel.Badges.Add(new Badge { IsStart = true });

                // Act
                homePage.Start_Click(null, null);

                // Assert
                var popupContentControl = Assert.IsType<ContentControl>(homePage.FindName("PopupContentControl"));
                Assert.Equal(Visibility.Visible, popupContentControl.Visibility);
                var errorPopupViewModel = Assert.IsType<ErrorPopupViewModel>(Assert.IsType<PopupError>(popupContentControl.Content).DataContext);
                Assert.Equal((string)Application.Current.Resources["AlreadyStart"], errorPopupViewModel.Message);
            });
        }

        [Fact]
        public void StartClick_WhenNotStarted_DisplaysConfirmationPopup()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var homePage = new HomePage();

                // Act
                homePage.Start_Click(null, null);

                // Assert
                var popupContentControl = Assert.IsType<ContentControl>(homePage.FindName("PopupContentControl"));
                Assert.Equal(Visibility.Visible, popupContentControl.Visibility);
                var confirmationPopupViewModel = Assert.IsType<ConfirmationPopupViewModel>(Assert.IsType<Popup>(popupContentControl.Content).DataContext);
                Assert.Equal((string)Application.Current.Resources["StartQuestion"], confirmationPopupViewModel.Message);
            });
        }

        #endregion

        #region EndClick Tests

        [Fact]
        public void EndClick_WhenAlreadyEnded_DisplaysErrorPopup()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var homePage = new HomePage();
                var badgeViewModel = Assert.IsType<BadgeViewModel>(homePage.DataContext);
                badgeViewModel.Badges.Add(new Badge { IsStart = false });

                // Act
                homePage.End_Click(null, null);

                // Assert
                var popupContentControl = Assert.IsType<ContentControl>(homePage.FindName("PopupContentControl"));
                Assert.Equal(Visibility.Visible, popupContentControl.Visibility);
                var errorPopupViewModel = Assert.IsType<ErrorPopupViewModel>(Assert.IsType<PopupError>(popupContentControl.Content).DataContext);
                Assert.Equal((string)Application.Current.Resources["AlreadyEnd"], errorPopupViewModel.Message);
            });
        }

        [Fact]
        public void EndClick_WhenNotEnded_DisplaysConfirmationPopup()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var homePage = new HomePage();

                // Act
                homePage.End_Click(null, null);

                // Assert
                var popupContentControl = Assert.IsType<ContentControl>(homePage.FindName("PopupContentControl"));
                Assert.Equal(Visibility.Visible, popupContentControl.Visibility);
                var confirmationPopupViewModel = Assert.IsType<ConfirmationPopupViewModel>(Assert.IsType<Popup>(popupContentControl.Content).DataContext);
                Assert.Equal((string)Application.Current.Resources["EndQuestion"], confirmationPopupViewModel.Message);
            });
        }

        #endregion
    }
}
