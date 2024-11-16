using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using TeleBadgeuse.Data;
using TeleBadgeuse.Model;

namespace TeleBadgeuse
{
    public class BadgeViewModel : INotifyPropertyChanged
    {
        private readonly IBadgeService _badgeService;
        private ObservableCollection<Badge> _badges;
        private bool _isAllSelected;
        private bool _isYearsSelected;
        private bool _isMonthsSelected;
        private bool _isDaysSelected;

        public BadgeViewModel(IBadgeService badgeService)
        {
            _badgeService = badgeService;
            LoadBadges();
            IsAllSelected = true; // Default selection
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

        public bool IsAllSelected
        {
            get => _isAllSelected;
            set
            {
                _isAllSelected = value;
                OnPropertyChanged();
                FilterBadges();
            }
        }

        public bool IsYearsSelected
        {
            get => _isYearsSelected;
            set
            {
                _isYearsSelected = value;
                OnPropertyChanged();
                FilterBadges();
            }
        }

        public bool IsMonthsSelected
        {
            get => _isMonthsSelected;
            set
            {
                _isMonthsSelected = value;
                OnPropertyChanged();
                FilterBadges();
            }
        }

        public bool IsDaysSelected
        {
            get => _isDaysSelected;
            set
            {
                _isDaysSelected = value;
                OnPropertyChanged();
                FilterBadges();
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

        private void FilterBadges()
        {
            // Implement filtering logic based on the selected radio button
            // This is a placeholder for the actual filtering logic
            if (IsAllSelected)
            {
                Badges = new ObservableCollection<Badge>(_badgeService.GetAllBadges());
            }
            else if (IsYearsSelected)
            {
                // Filter by years
                Badges = new ObservableCollection<Badge>(_badgeService.getBadgesByYear(DateTime.Now.Year));
                Console.WriteLine("Filter by years");
                
            }
            else if (IsMonthsSelected)
            {
                // Filter by months
                Badges = new ObservableCollection<Badge>(_badgeService.getBadgesByMonthYear(DateTime.Now.Month, DateTime.Now.Year));
            }
            else if (IsDaysSelected)
            {
                // Filter by days
                Badges = new ObservableCollection<Badge>(_badgeService.getBadgesByDayMonthYear(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}