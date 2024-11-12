using System.Windows;
using System.Windows.Controls;

namespace TeleBadgeuse
{
    public partial class LeftNavBar : UserControl
    {
        public LeftNavBar()
        {
            InitializeComponent();
        }
        
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Source = new Uri("/View/Page/HomePage.xaml", UriKind.Relative);
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Source = new Uri("/View/Page/ProfilePage.xaml", UriKind.Relative);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Source = new Uri("/View/Page/SettingsPage.xaml", UriKind.Relative);
        }
        
    }
}

