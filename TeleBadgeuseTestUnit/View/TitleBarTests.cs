using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TeleBadgeuse;
using Xunit;

namespace TeleBadgeuseTestUnit.View
{
    /// <summary>
    /// Tests unitaires pour la classe TitleBar.
    /// </summary>
    [Collection(WpfTestCollection.Name)]
    public class TitleBarTests
    {
        private readonly WpfTestHost _wpfTestHost;

        public TitleBarTests(WpfTestHost wpfTestHost)
        {
            _wpfTestHost = wpfTestHost;
        }

        #region Constructeur Tests

        [Fact]
        public void TitleBarConstructor_WithDefaultSetup_UsesSettingsViewModelAsDataContext()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange

                // Act
                var titleBar = new TitleBar();

                // Assert
                Assert.Same(SettingsPage.SettingsViewModel, titleBar.DataContext);
            });
        }

        #endregion

        #region Méthodes Tests

        [Fact]
        public void TitleBarMouseDown_WhenDetachedAndLeftButton_DoesNotThrow()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var titleBar = new TitleBar();
                var mouseButtonEventArgs = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left)
                {
                    RoutedEvent = UIElement.MouseDownEvent
                };

                // Act
                var exception = Record.Exception(() => titleBar.TitleBar_MouseDown(titleBar, mouseButtonEventArgs));

                // Assert
                Assert.Null(exception);
            });
        }

        [Fact]
        public void MinimizeClick_WhenInvoked_SetsWindowStateToMinimized()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var titleBar = new TitleBar();
                var window = new Window { Content = titleBar };
                var routedEventArgs = new RoutedEventArgs(Button.ClickEvent);

                // Act
                titleBar.Minimize_Click(titleBar, routedEventArgs);

                // Assert
                Assert.Equal(WindowState.Minimized, window.WindowState);
            });
        }

        [Fact]
        public void MaximizeClick_WhenInvoked_TogglesWindowState()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var titleBar = new TitleBar();
                var window = new Window { Content = titleBar };
                var routedEventArgs = new RoutedEventArgs(Button.ClickEvent);

                // Act
                titleBar.Maximize_Click(titleBar, routedEventArgs);

                // Assert
                Assert.Equal(WindowState.Maximized, window.WindowState);

                // Act
                titleBar.Maximize_Click(titleBar, routedEventArgs);

                // Assert
                Assert.Equal(WindowState.Normal, window.WindowState);
            });
        }

        [Fact]
        public void CloseClick_WhenInvoked_RaisesClosedEvent()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var titleBar = new TitleBar();
                var window = new Window { Content = titleBar };
                var routedEventArgs = new RoutedEventArgs(Button.ClickEvent);
                var closed = false;
                window.Closed += (_, _) => closed = true;

                // Act
                titleBar.Close_Click(titleBar, routedEventArgs);

                // Assert
                Assert.True(closed);
            });
        }

        #endregion
    }
}
