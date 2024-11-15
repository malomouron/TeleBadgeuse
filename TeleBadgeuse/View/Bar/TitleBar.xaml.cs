using System.Windows;
using System.Windows.Input;

namespace TeleBadgeuse
{
    /// <summary>
    /// Barre de titre de l'application.
    /// </summary>
    public partial class TitleBar
    {
        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="TitleBar"/>.
        /// </summary>
        public TitleBar()
        {
            InitializeComponent();
            
            // Définit le DataContext de la barre de titre sur le ViewModel principal.
            DataContext = SettingsPage.SettingsViewModel;
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Gère l'événement de clic de la souris sur la barre de titre pour permettre le déplacement de la fenêtre.
        /// </summary>
        /// <param name="sender">L'objet source de l'événement.</param>
        /// <param name="e">Les données d'événement de la souris.</param>
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Window.GetWindow(this)?.DragMove();
            }
        }

        /// <summary>
        /// Gère l'événement de clic pour minimiser la fenêtre.
        /// </summary>
        /// <param name="sender">L'objet source de l'événement.</param>
        /// <param name="e">Les données d'événement.</param>
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window != null) window.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Gère l'événement de clic pour maximiser ou restaurer la fenêtre.
        /// </summary>
        /// <param name="sender">L'objet source de l'événement.</param>
        /// <param name="e">Les données d'événement.</param>
        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window != null)
            {
                window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            }
        }

        /// <summary>
        /// Gère l'événement de clic pour fermer la fenêtre.
        /// </summary>
        /// <param name="sender">L'objet source de l'événement.</param>
        /// <param name="e">Les données d'événement.</param>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }

        #endregion
    }
}