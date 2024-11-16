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
            _badgeViewModel = new BadgeViewModel(new BadgeService(MainWindow.dbPath), this);
            DataContext = _badgeViewModel;
            _badgeViewModel.IsDaysSelected = true;
        }

        public void TurnOff()
        {
            AddTime.Visibility = System.Windows.Visibility.Hidden;
        }

        public void TurnOn()
        {
            AddTime.Visibility = System.Windows.Visibility.Visible;
        }
    }
}