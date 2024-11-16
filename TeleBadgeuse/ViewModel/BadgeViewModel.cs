using System;
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
        private DateTime _currentDate;
        private string _selectedDatePreview;

        public BadgeViewModel(IBadgeService badgeService)
        {
            _badgeService = badgeService;
            _currentDate = DateTime.Now;
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

        public string SelectedDatePreview
        {
            get => _selectedDatePreview;
            set
            {
                _selectedDatePreview = value;
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
        public ICommand IncrementCommand => new RelayCommand(IncrementDate);
        public ICommand DecrementCommand => new RelayCommand(DecrementDate);

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
            if (IsAllSelected)
            {
                Badges = new ObservableCollection<Badge>(_badgeService.GetAllBadges());
                SelectedDatePreview = string.Empty;
            }
            else if (IsYearsSelected)
            {
                Badges = new ObservableCollection<Badge>(_badgeService.getBadgesByYear(_currentDate.Year));
                SelectedDatePreview = _currentDate.ToString("yyyy");
            }
            else if (IsMonthsSelected)
            {
                Badges = new ObservableCollection<Badge>(_badgeService.getBadgesByMonthYear(_currentDate.Month, _currentDate.Year));
                SelectedDatePreview = _currentDate.ToString("yyyy-MM");
            }
            else if (IsDaysSelected)
            {
                Badges = new ObservableCollection<Badge>(_badgeService.getBadgesByDayMonthYear(_currentDate.Day, _currentDate.Month, _currentDate.Year));
                SelectedDatePreview = _currentDate.ToString("yyyy-MM-dd");
            }
        }

        private void IncrementDate()
        {
            if (IsYearsSelected)
            {
                _currentDate = _currentDate.AddYears(1);
            }
            else if (IsMonthsSelected)
            {
                _currentDate = _currentDate.AddMonths(1);
            }
            else if (IsDaysSelected)
            {
                _currentDate = _currentDate.AddDays(1);
            }
            FilterBadges();
        }

        private void DecrementDate()
        {
            if (IsYearsSelected)
            {
                _currentDate = _currentDate.AddYears(-1);
            }
            else if (IsMonthsSelected)
            {
                _currentDate = _currentDate.AddMonths(-1);
            }
            else if (IsDaysSelected)
            {
                _currentDate = _currentDate.AddDays(-1);
            }
            FilterBadges();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}