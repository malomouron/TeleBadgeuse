using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TeleBadgeuse.ViewModel;

namespace TeleBadgeuse
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; }

        public MainWindow()
        {
            InitializeComponent();
            this.Icon = new BitmapImage(new Uri("pack://application:,,,/Ressources/Icons/IconDark.ico"));

            this.ViewModel = new MainViewModel();
            DataContext = ViewModel;
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Minimized;

        private void Maximize_Click(object sender, RoutedEventArgs e) =>
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

    }
}