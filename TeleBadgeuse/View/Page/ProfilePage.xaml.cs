using System.IO;
using System.Windows;
using System.Windows.Controls;
using TeleBadgeuse.Model;

namespace TeleBadgeuse
{
    /// <summary>
    /// Interaction logique pour ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        #region Champs privés

        /// <summary>
        /// Modèle de vue pour les badges
        /// </summary>
        private BadgeViewModel _badgeViewModel;

        #endregion

        #region Constructeurs

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ProfilePage"/>.
        /// </summary>
        public ProfilePage()
        {
            InitializeComponent();
            _badgeViewModel = new BadgeViewModel(new BadgeService(MainWindow.dbPath), this);
            DataContext = _badgeViewModel;
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Masque l'élément AddTime.
        /// </summary>
        public void TurnOff()
        {
            AddTime.Visibility = System.Windows.Visibility.Hidden;
        }

        /// <summary>
        /// Affiche l'élément AddTime.
        /// </summary>
        public void TurnOn()
        {
            AddTime.Visibility = System.Windows.Visibility.Visible;
        }

        #endregion
    }
}