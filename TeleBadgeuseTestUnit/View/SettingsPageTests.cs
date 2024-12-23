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
    [Collection(WpfTestCollection.Name)]
    public class SettingsPageTests
    {
        private readonly WpfTestHost _wpfTestHost;

        public SettingsPageTests(WpfTestHost wpfTestHost)
        {
            _wpfTestHost = wpfTestHost;
        }

        #region Constructeur Tests

        [Fact]
        public void SettingsPageConstructor_WithDefaultSetup_UsesSettingsViewModelAsDataContext()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange

                // Act
                var settingsPage = new SettingsPage();

                // Assert
                Assert.Same(SettingsPage.SettingsViewModel, settingsPage.DataContext);
                Assert.IsType<SettingsViewModel>(settingsPage.DataContext);
            });
        }

        #endregion

        #region LanguageComboBox_SelectionChanged Tests

        [Fact]
        public void LanguageComboBoxSelectionChanged_WhenFrenchSelected_SavesFrenchCulture()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var settingsPage = new SettingsPage();
                SettingsPage.SettingsViewModel.AddSettingsCommand.Execute(("Language", "fr"));
                var comboBox = new ComboBox();
                var comboBoxItem = new ComboBoxItem { Content = settingsPage.FindResource("LangFr") };
                comboBox.Items.Add(comboBoxItem);
                comboBox.SelectedItem = comboBoxItem;

                // Act
                settingsPage.LanguageComboBox_SelectionChanged(comboBox, null);

                // Assert
                Assert.Equal("fr", SettingsPage.SettingsViewModel.SelectSettingsKey("Language").Value);
            });
        }

        [Fact]
        public void LanguageComboBoxSelectionChanged_WhenEnglishSelected_SavesEnglishCulture()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var settingsPage = new SettingsPage();
                SettingsPage.SettingsViewModel.AddSettingsCommand.Execute(("Language", "fr"));
                var comboBox = new ComboBox();
                var comboBoxItem = new ComboBoxItem { Content = settingsPage.FindResource("LangEn") };
                comboBox.Items.Add(comboBoxItem);
                comboBox.SelectedItem = comboBoxItem;

                // Act
                settingsPage.LanguageComboBox_SelectionChanged(comboBox, null);

                // Assert
                Assert.Equal("en", SettingsPage.SettingsViewModel.SelectSettingsKey("Language").Value);
            });
        }

        [Fact]
        public void LanguageComboBoxSelectionChanged_WhenUnknownLanguageSelected_UsesEnglishCulture()
        {
            _wpfTestHost.Run(() =>
            {
                // Arrange
                var settingsPage = new SettingsPage();
                SettingsPage.SettingsViewModel.AddSettingsCommand.Execute(("Language", "fr"));
                var comboBox = new ComboBox();
                var comboBoxItem = new ComboBoxItem { Content = "Unknown" };
                comboBox.Items.Add(comboBoxItem);
                comboBox.SelectedItem = comboBoxItem;

                // Act
                settingsPage.LanguageComboBox_SelectionChanged(comboBox, null);

                // Assert
                Assert.Equal("en", SettingsPage.SettingsViewModel.SelectSettingsKey("Language").Value);
            });
        }

        #endregion
    }
}
