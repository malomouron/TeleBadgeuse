using System.Windows.Controls;
using TeleBadgeuse;
using TeleBadgeuse.Model;
using TeleBadgeuse.Ressources;
using Xunit;

namespace TeleBadgeuseTestUnit.View
{
    /// <summary>
    /// Tests unitaires pour la classe SettingsPage.
    /// </summary>
    public class SettingsPageTests
    {
        #region Constructeur Tests

        [Fact(Skip = "Test désactivé")]
        public void SettingsPage_Constructor_InitializesCorrectly()
        {
            // Act
            var settingsPage = new SettingsPage();

            // Assert
            Assert.NotNull(settingsPage);
            Assert.NotNull(settingsPage.DataContext);
            Assert.IsType<SettingsViewModel>(settingsPage.DataContext);
        }

        #endregion

        #region LanguageComboBox_SelectionChanged Tests

        [Fact(Skip = "Test désactivé")]
        public void LanguageComboBox_SelectionChanged_SetsCultureToFrench()
        {
            // Arrange
            var settingsPage = new SettingsPage();
            var comboBox = new ComboBox();
            var comboBoxItem = new ComboBoxItem { Content = settingsPage.FindResource("LangFr") };
            comboBox.Items.Add(comboBoxItem);
            comboBox.SelectedItem = comboBoxItem;

            // Act
            settingsPage.LanguageComboBox_SelectionChanged(comboBox, null);

            // Assert
            Assert.Equal("fr", SettingsPage.SettingsViewModel.SelectSettingsKey("Language").Value);
        }

        [Fact(Skip = "Test désactivé")]
        public void LanguageComboBox_SelectionChanged_SetsCultureToEnglish()
        {
            // Arrange
            var settingsPage = new SettingsPage();
            var comboBox = new ComboBox();
            var comboBoxItem = new ComboBoxItem { Content = settingsPage.FindResource("LangEn") };
            comboBox.Items.Add(comboBoxItem);
            comboBox.SelectedItem = comboBoxItem;

            // Act
            settingsPage.LanguageComboBox_SelectionChanged(comboBox, null);

            // Assert
            Assert.Equal("en", SettingsPage.SettingsViewModel.SelectSettingsKey("Language").Value);
        }

        [Fact(Skip = "Test désactivé")]
        public void LanguageComboBox_SelectionChanged_SetsCultureToDefault()
        {
            // Arrange
            var settingsPage = new SettingsPage();
            var comboBox = new ComboBox();
            var comboBoxItem = new ComboBoxItem { Content = "Unknown" };
            comboBox.Items.Add(comboBoxItem);
            comboBox.SelectedItem = comboBoxItem;

            // Act
            settingsPage.LanguageComboBox_SelectionChanged(comboBox, null);

            // Assert
            Assert.Equal("en", SettingsPage.SettingsViewModel.SelectSettingsKey("Language").Value);
        }

        #endregion
    }
}
