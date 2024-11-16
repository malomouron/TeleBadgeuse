using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using TeleBadgeuse.Data;
using TeleBadgeuse.Model;

namespace TeleBadgeuse;

public class BadgeViewModel : INotifyPropertyChanged
{
    private readonly IBadgeService _badgeService;
    private ObservableCollection<Badge> _badges;
    public BadgeViewModel(IBadgeService badgeService)
    {
        _badgeService = badgeService;
        LoadBadges();
    }

    public ObservableCollection<Badge> Badges
    {
        get => _badges;
        set
        {
            _badges = value;
            OnPropertyChanged();
        }
    }


    public ICommand AddBadgeCommand => new RelayCommand<bool>(AddBadge);
    public ICommand UpdateBadgeCommand => new RelayCommand<object>((param) => 
    {
        var (id, date, isStart) = ((int, DateTime, bool))param;
        UpdateBadge(id, date, isStart);
    });
    public ICommand DeleteBadgeCommand => new RelayCommand<int>(DeleteBadge);

    private void LoadBadges()
    {
        Badges = new ObservableCollection<Badge>(_badgeService.GetAllBadges());
    }

    private void AddBadge(bool isStart)
    {
        var newBadge = new Badge { Date = DateTime.Now, IsStart = isStart };
        _badgeService.AddBadge(newBadge);
        LoadBadges();
    }

    private void UpdateBadge(int id, DateTime date, bool isStart)
    {
        var badge = new Badge { Id = id, Date = date, IsStart = isStart };
        _badgeService.UpdateBadge(badge);
        LoadBadges();
    }

    private void DeleteBadge(int id)
    {
        _badgeService.DeleteBadge(id);
        LoadBadges();
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler PropertyChanged;
}