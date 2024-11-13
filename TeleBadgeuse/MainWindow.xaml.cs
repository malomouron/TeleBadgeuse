using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using MaterialDesignThemes.Wpf;
using TeleBadgeuse.Data;
using TeleBadgeuse.Model;
using TeleBadgeuse.ViewModel;
using TeleBadgeuse.Ressources;

namespace TeleBadgeuse
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Attributs

        /// <summary>
        /// ViewModel principal de l'application.
        /// </summary>
        private static MainViewModel _viewModel = null!;
        
        private SettingsViewModel _settingsViewModel = null!;
        
        private const string dbPath = "Data Source=./data.db";

        /// <summary>
        /// Indique si le bouton de navigation est activé.
        /// </summary>
        private bool _isToggled = false;

        #endregion

        #region Propriétés

        /// <summary>
        /// Obtient le ViewModel principal de l'application.
        /// </summary>
        public static MainViewModel ViewModel
        {
            get => _viewModel;
            private set => _viewModel = value;
        }
        
        public SettingsViewModel SettingsViewModel
        {
            get => _settingsViewModel;
            private set => _settingsViewModel = value;
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="MainWindow"/>.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            
            SettingsViewModel = new SettingsViewModel(new SettingsService(dbPath));
            
            ViewModel = new MainViewModel();
            DataContext = ViewModel;
            

            InitSettings();
            InitMainWindow();
        }

        #endregion

        #region Méthodes
        
        private void InitMainWindow()
        {
            this.SetBinding(IconProperty, new Binding("Icon") { Source = ViewModel });

            ResourceDictionaryManager.SetCulture("fr");
            ResourceDictionaryManager.SetTheme(SettingsViewModel.SelectSettingsKey("Theme").First().Value);
            MainFrame.Source = new Uri(SettingsViewModel.SelectSettingsKey("DefaultPage").First().Value, UriKind.Relative);
        }
        
        private void InitSettings()
        {
            if (SettingsViewModel.SelectSettingsKey("Theme").Count() == 0)
            {
                SettingsViewModel.AddSettingsCommand.Execute(("Theme", "DarkTheme"));
            }
            if (SettingsViewModel.SelectSettingsKey("DefaultPage").Count() == 0)
            {
                SettingsViewModel.AddSettingsCommand.Execute(("DefaultPage", "/View/Page/HomePage.xaml"));
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