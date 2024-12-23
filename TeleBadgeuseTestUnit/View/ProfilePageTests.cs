using System.Windows;
using System.Windows.Controls;
using TeleBadgeuse;
using Xunit;

namespace TeleBadgeuseTestUnit.View
{
    /// <summary>
    /// Tests unitaires pour la classe ProfilePage.
    /// </summary>
    [Collection(WpfTestCollection.Name)]
    public class ProfilePageTests
    {
        private readonly WpfTestHost _wpfTestHost;

        public ProfilePageTests(WpfTestHost wpfTestHost)
        {
            _wpfTestHost = wpfTestHost;
        }

        #region TurnOff Tests

        [Fact]
        public void TurnOff_WhenInvoked_SetsAddTimeVisibilityToHidden()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var profilePage = new ProfilePage();
                var addTime = Assert.IsType<StackPanel>(profilePage.FindName("AddTime"));
                addTime.Visibility = Visibility.Visible;

                // Act
                profilePage.TurnOff();

                // Assert
                Assert.Equal(Visibility.Hidden, addTime.Visibility);
            });
        }

        #endregion

        #region TurnOn Tests

        [Fact]
        public void TurnOn_WhenInvoked_SetsAddTimeVisibilityToVisible()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var profilePage = new ProfilePage();
                var addTime = Assert.IsType<StackPanel>(profilePage.FindName("AddTime"));
                addTime.Visibility = Visibility.Hidden;

                // Act
                profilePage.TurnOn();

                // Assert
                Assert.Equal(Visibility.Visible, addTime.Visibility);
            });
        }

        #endregion
    }
}
