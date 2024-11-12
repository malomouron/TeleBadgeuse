using System.Windows.Controls;
using TeleBadgeuse.Ressources;
using TeleBadgeuse.ViewModel;

namespace TeleBadgeuse;

public partial class SettingsPage : Page
{
    public SettingsPage()
    {
        InitializeComponent();

        DataContext = MainWindow.ViewModel;
    }
    

    private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ComboBox comboBox && comboBox.SelectedItem is ComboBoxItem selectedItem)
        {
            string? langFrValue = this.FindResource("LangFr") as string;
            string? langEnValue = this.FindResource("LangEn") as string;
            string newCulture = selectedItem.Content.ToString() switch
            {
                var content when content == langFrValue => "fr",
                var content when content == langEnValue => "en",
                _ => "en"
            };

            ResourceDictionaryManager.SetCulture(newCulture);
        }
    }
}