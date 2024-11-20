using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using MaterialDesignThemes.Wpf;
using TeleBadgeuse.Data;
using TeleBadgeuse.Ressources;

namespace TeleBadgeuse
{
    
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public const string dbPath = "Data Source=./data.db";
        
        #region Attributs
        /// <summary>
        /// Indique si le bouton de navigation est activé.
        /// </summary>
        private bool _isToggled = false;

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="MainWindow"/>.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            new SettingsPage(); // Initialize settings
            

            InitSettings();
            InitMainWindow();

            DataContext = SettingsPage.SettingsViewModel;
            
            //display settings
            foreach (Settings settings in SettingsPage.SettingsViewModel.Settings)
            {
                Console.WriteLine(settings.Key + " : " + settings.Value);
            }
        }

        #endregion

        #region Méthodes
        
        private void InitMainWindow()
        {
            this.SetBinding(IconProperty, new Binding("Icon") { Source = SettingsPage.SettingsViewModel });

            ResourceDictionaryManager.SetCulture(SettingsPage.SettingsViewModel.SelectSettingsKey("Language").Value);
            ResourceDictionaryManager.SetTheme(SettingsPage.SettingsViewModel.SelectSettingsKey("Theme").Value);
            MainFrame.Source = new Uri(SettingsPage.SettingsViewModel.SelectSettingsKey("DefaultPage").Value, UriKind.Relative);
        }
        
        private void InitSettings()
        {
            if (SettingsPage.SettingsViewModel.SelectSettingsKey("Theme") == null)
            {
                SettingsPage.SettingsViewModel.AddSettingsCommand.Execute(("Theme", "DarkTheme"));
            }
            if (SettingsPage.SettingsViewModel.SelectSettingsKey("DefaultPage") == null)
            {
                SettingsPage.SettingsViewModel.AddSettingsCommand.Execute(("DefaultPage", "/View/Page/HomePage.xaml"));
            }
            if (SettingsPage.SettingsViewModel.SelectSettingsKey("Language") == null)
            {
                SettingsPage.SettingsViewModel.AddSettingsCommand.Execute(("Language", "fr"));
            }
            if (SettingsPage.SettingsViewModel.SelectSettingsKey("MinHours") == null)
            {
                SettingsPage.SettingsViewModel.AddSettingsCommand.Execute(("MinHours", "7"));
            }
        }

        /// <summary>
        /// Gère l'événement de navigation du MainFrame.
        /// </summary>
        /// <param name="sender">L'objet source de l'événement.</param>
        /// <param name="e">Les données d'événement de navigation.</param>
        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            // Efface l'historique de navigation pour supprimer le bouton de retour
            if (MainFrame.CanGoBack)
            {
                MainFrame.RemoveBackEntry();
            }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ResourceDictionary resource = new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/Ressources/Style.xaml")
            };
            if (_isToggled)
            {
                // Revert to initial state
                MainFrame.SetValue(Grid.ColumnSpanProperty, 2);
                MainFrame.SetValue(Grid.ColumnProperty, 1);
                LeftNavBar.SetValue(Grid.ColumnSpanProperty, 1);
                BigNav.SetValue(Grid.ColumnProperty, 1);
                IconChevron.Kind = (PackIconKind)Enum
                    .Parse(typeof(PackIconKind), "ChevronRight");
                LeftNavBar.ToggleLabelsVisibility(false);
                LeftNavBar.ToggleButtonStyles((Style)resource["ButtonLeftNavBarTiny"]);
            }
            else
            {
                // Toggle to new state
                MainFrame.SetValue(Grid.ColumnSpanProperty, 1);
                MainFrame.SetValue(Grid.ColumnProperty, 2);
                LeftNavBar.SetValue(Grid.ColumnSpanProperty, 2);
                BigNav.SetValue(Grid.ColumnProperty, 2);
                IconChevron.Kind = (PackIconKind)Enum
                    .Parse(typeof(PackIconKind), "ChevronLeft");
                LeftNavBar.ToggleLabelsVisibility(true);
                LeftNavBar.ToggleButtonStyles((Style)resource["ButtonLeftNavBarBig"]);
            }

            _isToggled = !_isToggled;
        }
        
        #endregion

    }
}