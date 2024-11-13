using System.Windows.Threading;

namespace TeleBadgeuse;

public partial class HomePage
{
    private DispatcherTimer _timer;

    public HomePage()
    {
        InitializeComponent();
        StartClock();
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
    
}