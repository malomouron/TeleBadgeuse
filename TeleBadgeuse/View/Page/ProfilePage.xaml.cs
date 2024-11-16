using System.Windows.Controls;
using TeleBadgeuse.Model;

namespace TeleBadgeuse
{
    public partial class ProfilePage : Page
    {
        private BadgeViewModel _badgeViewModel;

        public ProfilePage()
        {
            InitializeComponent();
            _badgeViewModel = new BadgeViewModel(new BadgeService(MainWindow.dbPath));
            DataContext = _badgeViewModel;
        }
    }
}