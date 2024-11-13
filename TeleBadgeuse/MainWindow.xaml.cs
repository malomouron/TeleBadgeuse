using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using MaterialDesignThemes.Wpf;
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

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="MainWindow"/>.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.Icon = new BitmapImage(new Uri("pack://application:,,,/Ressources/Icons/IconLight.ico"));

            ViewModel = new MainViewModel();
            DataContext = ViewModel;

            ResourceDictionaryManager.SetCulture("fr");
            ResourceDictionaryManager.SetTheme("LightTheme");
            MainFrame.Source = new Uri("/View/Page/HomePage.xaml", UriKind.Relative);
        }

        #endregion

        #region Méthodes

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
            if (_isToggled)
            {
                // Revert to initial state
                MainFrame.SetValue(Grid.ColumnSpanProperty, 2);
                MainFrame.SetValue(Grid.ColumnProperty, 1);
                LeftNavBar.SetValue(Grid.ColumnSpanProperty, 1);
                BigNav.SetValue(Grid.ColumnProperty, 1);
                IconChevron.Kind = (PackIconKind)Enum
                    .Parse(typeof(PackIconKind), "ChevronRight");

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
            }

            _isToggled = !_isToggled;
        }
        
        #endregion

    }
}