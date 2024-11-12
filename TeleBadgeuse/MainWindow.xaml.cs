using System.Windows.Media.Imaging;
using System.Windows.Navigation;
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

        #endregion
    }
}