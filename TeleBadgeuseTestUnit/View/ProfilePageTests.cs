using System.Windows;
using System.Windows.Controls;
using TeleBadgeuse;
using Xunit;

namespace TeleBadgeuseTestUnit.View
{
    /// <summary>
    /// Tests unitaires pour la classe ProfilePage.
    /// </summary>
    public class ProfilePageTests
    {
        #region TurnOff Tests

        [Fact]
        public void TurnOff_SetsAddTimeVisibilityToHidden()
        {
            // Arrange
            var profilePage = new ProfilePage();
            profilePage.GetType().GetProperty("AddTime", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(profilePage, new StackPanel { Visibility = Visibility.Visible });

            // Act
            profilePage.TurnOff();

            // Assert
            var addTime = (StackPanel)profilePage.GetType().GetProperty("AddTime", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(profilePage);
            Assert.Equal(Visibility.Hidden, addTime.Visibility);
        }

        #endregion

        #region TurnOn Tests

        [Fact]
        public void TurnOn_SetsAddTimeVisibilityToVisible()
        {
            // Arrange
            var profilePage = new ProfilePage();
            profilePage.GetType().GetProperty("AddTime", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(profilePage, new StackPanel { Visibility = Visibility.Hidden });

            // Act
            profilePage.TurnOn();

            // Assert
            var addTime = (StackPanel)profilePage.GetType().GetProperty("AddTime", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(profilePage);
            Assert.Equal(Visibility.Visible, addTime.Visibility);
        }

        #endregion
    }
}
