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
    public class TitleBarTests
    {
        #region Constructeur Tests

        [Fact(Skip = "Test désactivé")]
        public void TitleBar_Constructor_InitializesDataContext()
        {
            // Act
            var titleBar = new TitleBar();

            // Assert
            Assert.Equal(SettingsPage.SettingsViewModel, titleBar.DataContext);
        }

        #endregion

        #region Méthodes Tests

        [Fact(Skip = "Test désactivé")]
        public void TitleBar_MouseDown_LeftButton_DragsWindow()
        {
            // Arrange
            var titleBar = new TitleBar();
            var window = new Window { Content = titleBar };
            window.Show();
            var mouseButtonEventArgs = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left)
            {
                RoutedEvent = UIElement.MouseDownEvent
            };

            // Act
            titleBar.RaiseEvent(mouseButtonEventArgs);

            // Assert
            // No exception means the method executed successfully
        }

        [Fact(Skip = "Test désactivé")]
        public void Minimize_Click_MinimizesWindow()
        {
            // Arrange
            var titleBar = new TitleBar();
            var window = new Window { Content = titleBar };
            window.Show();
            var routedEventArgs = new RoutedEventArgs(Button.ClickEvent);

            // Act
            titleBar.Minimize_Click(titleBar, routedEventArgs);

            // Assert
            Assert.Equal(WindowState.Minimized, window.WindowState);
        }

        [Fact(Skip = "Test désactivé")]
        public void Maximize_Click_TogglesWindowState()
        {
            // Arrange
            var titleBar = new TitleBar();
            var window = new Window { Content = titleBar };
            window.Show();
            var routedEventArgs = new RoutedEventArgs(Button.ClickEvent);

            // Act
            titleBar.Maximize_Click(titleBar, routedEventArgs);

            // Assert
            Assert.Equal(WindowState.Maximized, window.WindowState);

            // Act
            titleBar.Maximize_Click(titleBar, routedEventArgs);

            // Assert
            Assert.Equal(WindowState.Normal, window.WindowState);
        }

        [Fact(Skip = "Test désactivé")]
        public void Close_Click_ClosesWindow()
        {
            // Arrange
            var titleBar = new TitleBar();
            var window = new Window { Content = titleBar };
            window.Show();
            var routedEventArgs = new RoutedEventArgs(Button.ClickEvent);

            // Act
            titleBar.Close_Click(titleBar, routedEventArgs);

            // Assert
            Assert.False(window.IsVisible);
        }

        #endregion
    }
}
