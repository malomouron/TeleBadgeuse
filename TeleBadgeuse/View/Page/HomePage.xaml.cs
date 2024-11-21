using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.Input;
using TeleBadgeuse.Model;

namespace TeleBadgeuse;

public partial class HomePage
{
    private DispatcherTimer _timer;
    private BadgeViewModel _badgeViewModel;

    public HomePage()
    {
        InitializeComponent();
        StartClock();
        _badgeViewModel = new BadgeViewModel(new BadgeService(MainWindow.dbPath));
        DataContext = _badgeViewModel;
    }

    private void StartClock()
    {
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += (sender, args) =>
        {
            CurrentTimeTextBlock.Text = DateTime.Now.ToString("HH:mm:ss");
        };
        _timer.Start();
    }

    private void Start()
    {
        _badgeViewModel.AddBadgeCommand.Execute(true);
    }

    private void End()
    {
        _badgeViewModel.AddBadgeCommand.Execute(false);
    }

    private void ShowConfirmationPopup(string message, ICommand yesCommand, ICommand noCommand)
    {
        var popupViewModel = new ConfirmationPopupViewModel(message, yesCommand, noCommand);
        var popup = new Popup { DataContext = popupViewModel };
        PopupContentControl.Content = popup;
        PopupContentControl.Visibility = Visibility.Visible;
    }

    private void HideConfirmationPopup()
    {
        PopupContentControl.Visibility = Visibility.Collapsed;
        PopupContentControl.Content = null;
    }

    private void Start_Click(object sender, RoutedEventArgs e)
    {
        ShowConfirmationPopup((string)Application.Current.Resources["StartQuestion"], new RelayCommand(() =>
        {
            HideConfirmationPopup();
            Start();
        }), new RelayCommand(HideConfirmationPopup));
    }

    private void End_Click(object sender, RoutedEventArgs e)
    {
        ShowConfirmationPopup((string)Application.Current.Resources["EndQuestion"], new RelayCommand(() =>
        {
            HideConfirmationPopup();
            End();
        }), new RelayCommand(HideConfirmationPopup));
    }
}