using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
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
    public class HomePageTests
    {
        #region Constructeur Tests

        [Fact]
        public void HomePage_Constructor_InitializesCorrectly()
        {
            // Act
            var homePage = new HomePage();

            // Assert
            Assert.NotNull(homePage);
            Assert.NotNull(homePage.DataContext);
            Assert.IsType<BadgeViewModel>(homePage.DataContext);
        }

        #endregion

        #region StartClock Tests

        [Fact]
        public void StartClock_InitializesTimerCorrectly()
        {
            // Arrange
            var homePage = new HomePage();
            var timerField = typeof(HomePage).GetField("_timer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            // Act
            homePage.StartClock();

            // Assert
            var timer = (DispatcherTimer)timerField.GetValue(homePage);
            Assert.NotNull(timer);
            Assert.Equal(TimeSpan.FromSeconds(1), timer.Interval);
        }

        #endregion

        #region Start Tests

        [Fact]
        public void Start_ExecutesAddBadgeCommandWithTrue()
        {
            // Arrange
            var homePage = new HomePage();
            var badgeViewModel = (BadgeViewModel)homePage.DataContext;

            // Act
            homePage.Start();

            // Assert
            Assert.True(badgeViewModel.AddBadgeCommand.CanExecute(true));
        }

        #endregion

        #region End Tests

        [Fact]
        public void End_ExecutesAddBadgeCommandWithFalse()
        {
            // Arrange
            var homePage = new HomePage();
            var badgeViewModel = (BadgeViewModel)homePage.DataContext;

            // Act
            homePage.End();

            // Assert
            Assert.True(badgeViewModel.AddBadgeCommand.CanExecute(false));
        }

        #endregion

        #region ShowConfirmationPopup Tests

        [Fact]
        public void ShowConfirmationPopup_DisplaysPopupWithCorrectMessage()
        {
            // Arrange
            var homePage = new HomePage();
            var message = "Test message";
            var yesCommand = new RelayCommand(() => { });
            var noCommand = new RelayCommand(() => { });

            // Act
            homePage.ShowConfirmationPopup(message, yesCommand, noCommand);

            // Assert
            var popupContentControl = (ContentControl)homePage.FindName("PopupContentControl");
            Assert.NotNull(popupContentControl);
            Assert.Equal(Visibility.Visible, popupContentControl.Visibility);
            var popupViewModel = (ConfirmationPopupViewModel)popupContentControl.DataContext;
            Assert.Equal(message, popupViewModel.Message);
        }

        #endregion

        #region ShowErrorPopup Tests

        [Fact]
        public void ShowErrorPopup_DisplaysPopupWithCorrectMessage()
        {
            // Arrange
            var homePage = new HomePage();
            var message = "Test error message";
            var okayCommand = new RelayCommand(() => { });

            // Act
            homePage.ShowErrorPopup(message, okayCommand);

            // Assert
            var popupContentControl = (ContentControl)homePage.FindName("PopupContentControl");
            Assert.NotNull(popupContentControl);
            Assert.Equal(Visibility.Visible, popupContentControl.Visibility);
            var errorPopupViewModel = (ErrorPopupViewModel)popupContentControl.DataContext;
            Assert.Equal(message, errorPopupViewModel.Message);
        }

        #endregion

        #region HideConfirmationPopup Tests

        [Fact]
        public void HideConfirmationPopup_HidesPopupCorrectly()
        {
            // Arrange
            var homePage = new HomePage();
            var popupContentControl = (ContentControl)homePage.FindName("PopupContentControl");
            popupContentControl.Visibility = Visibility.Visible;

            // Act
            homePage.HideConfirmationPopup();

            // Assert
            Assert.Equal(Visibility.Collapsed, popupContentControl.Visibility);
            Assert.Null(popupContentControl.Content);
        }

        #endregion

        #region Start_Click Tests

        [Fact]
        public void Start_Click_ShowsErrorPopupIfAlreadyStarted()
        {
            // Arrange
            var homePage = new HomePage();
            var badgeViewModel = (BadgeViewModel)homePage.DataContext;
            badgeViewModel.Badges.Add(new Badge { IsStart = true });

            // Act
            homePage.Start_Click(null, null);

            // Assert
            var popupContentControl = (ContentControl)homePage.FindName("PopupContentControl");
            Assert.Equal(Visibility.Visible, popupContentControl.Visibility);
            var errorPopupViewModel = (ErrorPopupViewModel)popupContentControl.DataContext;
            Assert.Equal((string)Application.Current.Resources["AlreadyStart"], errorPopupViewModel.Message);
        }

        [Fact]
        public void Start_Click_ShowsConfirmationPopupIfNotStarted()
        {
            // Arrange
            var homePage = new HomePage();
            var badgeViewModel = (BadgeViewModel)homePage.DataContext;

            // Act
            homePage.Start_Click(null, null);

            // Assert
            var popupContentControl = (ContentControl)homePage.FindName("PopupContentControl");
            Assert.Equal(Visibility.Visible, popupContentControl.Visibility);
            var confirmationPopupViewModel = (ConfirmationPopupViewModel)popupContentControl.DataContext;
            Assert.Equal((string)Application.Current.Resources["StartQuestion"], confirmationPopupViewModel.Message);
        }

        #endregion

        #region End_Click Tests

        [Fact]
        public void End_Click_ShowsErrorPopupIfAlreadyEnded()
        {
            // Arrange
            var homePage = new HomePage();
            var badgeViewModel = (BadgeViewModel)homePage.DataContext;
            badgeViewModel.Badges.Add(new Badge { IsStart = false });

            // Act
            homePage.End_Click(null, null);

            // Assert
            var popupContentControl = (ContentControl)homePage.FindName("PopupContentControl");
            Assert.Equal(Visibility.Visible, popupContentControl.Visibility);
            var errorPopupViewModel = (ErrorPopupViewModel)popupContentControl.DataContext;
            Assert.Equal((string)Application.Current.Resources["AlreadyEnd"], errorPopupViewModel.Message);
        }

        [Fact]
        public void End_Click_ShowsConfirmationPopupIfNotEnded()
        {
            // Arrange
            var homePage = new HomePage();
            var badgeViewModel = (BadgeViewModel)homePage.DataContext;

            // Act
            homePage.End_Click(null, null);

            // Assert
            var popupContentControl = (ContentControl)homePage.FindName("PopupContentControl");
            Assert.Equal(Visibility.Visible, popupContentControl.Visibility);
            var confirmationPopupViewModel = (ConfirmationPopupViewModel)popupContentControl.DataContext;
            Assert.Equal((string)Application.Current.Resources["EndQuestion"], confirmationPopupViewModel.Message);
        }

        #endregion
    }
}
