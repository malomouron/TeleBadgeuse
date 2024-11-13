using System.Windows;

namespace TeleBadgeuse
{
    /// <summary>
    /// Barre de navigation gauche de l'application.
    /// </summary>
    public partial class LeftNavBar
    {
        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="LeftNavBar"/>.
        /// </summary>
        public LeftNavBar()
        {
            InitializeComponent();
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Gère l'événement de clic du bouton d'accueil.
        /// </summary>
        /// <param name="sender">L'objet source de l'événement.</param>
        /// <param name="e">Les données d'événement.</param>
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Source = new Uri("/View/Page/HomePage.xaml", UriKind.Relative);
        }

        /// <summary>
        /// Gère l'événement de clic du bouton de profil.
        /// </summary>
        /// <param name="sender">L'objet source de l'événement.</param>
        /// <param name="e">Les données d'événement.</param>
        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Source = new Uri("/View/Page/ProfilePage.xaml", UriKind.Relative);
        }

        /// <summary>
        /// Gère l'événement de clic du bouton des paramètres.
        /// </summary>
        /// <param name="sender">L'objet source de l'événement.</param>
        /// <param name="e">Les données d'événement.</param>
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Source = new Uri("/View/Page/SettingsPage.xaml", UriKind.Relative);
        }

        public void ToggleButtonStyles(Style style)
        {
            ButtonHome.Style = style;
            ButtonProfil.Style = style;
            ButtonSettings.Style = style;
        }
        public void ToggleLabelsVisibility(bool isVisible)
        {
            var visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
            HomeLabel.Visibility = visibility;
            ProfileLabel.Visibility = visibility;
            SettingsLabel.Visibility = visibility;
        }
        #endregion
    }
}