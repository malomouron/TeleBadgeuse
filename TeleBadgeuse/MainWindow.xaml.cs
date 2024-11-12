using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TeleBadgeuse.ViewModel;
using TeleBadgeuse.Ressources;

namespace TeleBadgeuse
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainViewModel ViewModel { get; }

        public MainWindow()
        {
            InitializeComponent();
            this.Icon = new BitmapImage(new Uri("pack://application:,,,/Ressources/Icons/IconLight.ico"));

            this.ViewModel = new MainViewModel();
            DataContext = ViewModel;
            
            
            ResourceDictionaryManager.SetCulture("fr");
            ResourceDictionaryManager.SetTheme("LightTheme");
            
            SetDefaultLanguage();
        }
        

        private void SetDefaultLanguage()
        {
            string? langFrValue = this.FindResource("LangFr") as string;
            string? langEnValue = this.FindResource("LangEn") as string;
            string currentCulture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            string? defaultLanguage = currentCulture switch
            {
                var culture when culture == "fr" => langFrValue,
                var culture when culture == "en" => langEnValue,
                _ => langEnValue
            };

            foreach (ComboBoxItem item in LanguageComboBox.Items)
            {
                if (item.Content.ToString() == defaultLanguage)
                {
                    LanguageComboBox.SelectedItem = item;
                    break;
                }
            }
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
}