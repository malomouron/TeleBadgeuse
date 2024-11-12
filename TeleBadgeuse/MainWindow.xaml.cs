using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using TeleBadgeuse.ViewModel;
using TeleBadgeuse.Ressources;

namespace TeleBadgeuse
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static MainViewModel ViewModel { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            this.Icon = new BitmapImage(new Uri("pack://application:,,,/Ressources/Icons/IconLight.ico"));

            MainWindow.ViewModel = new MainViewModel();
            DataContext = ViewModel;
            
            
            ResourceDictionaryManager.SetCulture("fr");
            ResourceDictionaryManager.SetTheme("LightTheme");
            
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            // Clear the navigation history to remove the back button
            if (MainFrame.CanGoBack)
            {
                MainFrame.RemoveBackEntry();
            }
        }
    }
}