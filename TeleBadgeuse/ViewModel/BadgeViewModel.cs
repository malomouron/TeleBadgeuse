using System;
using System.Linq;
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
        private readonly ProfilePage _profilePage;
        private bool _isViewListSelected;
        private bool _isViewGridSelected;
        private bool _isViewModuleSelected;
        private bool _isTimelineSelected;
        
        public bool IsViewListSelected
        {
            get => _isViewListSelected;
            set
            {
                _isViewListSelected = value;
                OnPropertyChanged();
                UpdateView();
            }
        }

        public bool IsViewGridSelected
        {
            get => _isViewGridSelected;
            set
            {
                _isViewGridSelected = value;
                OnPropertyChanged();
                UpdateView();
            }
        }

        public bool IsViewModuleSelected
        {
            get => _isViewModuleSelected;
            set
            {
                _isViewModuleSelected = value;
                OnPropertyChanged();
                UpdateView();
            }
        }

        public bool IsTimelineSelected
        {
            get => _isTimelineSelected;
            set
            {
                _isTimelineSelected = value;
                OnPropertyChanged();
                UpdateView();
            }
        }
        
        private void UpdateView()
        {
            // Update the ItemsSource of the DataGrid based on the selected view
            if (IsViewListSelected)
            {
                // Set ItemsSource for list view
            }
            else if (IsViewGridSelected)
            {
                // Set ItemsSource for grid view
            }
            else if (IsViewModuleSelected)
            {
                // Set ItemsSource for module view
            }
            else if (IsTimelineSelected)
            {
                // Set ItemsSource for timeline view
            }
        }


        public BadgeViewModel(IBadgeService badgeService, ProfilePage profilePage = null)
        {
            _badgeService = badgeService;
            _currentDate = DateTime.Now;
            LoadBadges();
            IsAllSelected = true; // Default selection
            IsViewListSelected = true; // Default selection
            _profilePage = profilePage;
            
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
                if (_profilePage != null)
                {
                    if (_isAllSelected)
                    {
                        _profilePage.TurnOff();
                    }
                }
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
                {
                    _currentDate = DateTime.Now;
                }
                if (_profilePage != null)
                {
                    if (_isYearsSelected)
                    {
                        _profilePage.TurnOn();
                    }
                }
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
                {
                    _currentDate = DateTime.Now;
                }
                if (_profilePage != null)
                {
                    if (_isMonthsSelected)
                    {
                        _profilePage.TurnOn();
                    }
                }
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
                if (_isDaysSelected)
                {
                    _currentDate = DateTime.Now;
                }
                if (_profilePage != null)
                {
                    if (_isDaysSelected)
                    {
                        _profilePage.TurnOn();
                    }
                }
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
        
        public ObservableCollection<Badge> BadgesIsStart
        {
            get => new ObservableCollection<Badge>(Badges.Where(b => b.IsStart));
            
        }
        
        public ObservableCollection<Badge> BadgesIsEnd
        {
            get => new ObservableCollection<Badge>(Badges.Where(b => b.IsEnd));
        }

        public ObservableCollection<BadgePair> MergedBadges
        {
            get
            {
                ObservableCollection<BadgePair> mergedBadges = new ObservableCollection<BadgePair>();
                int maxCount = Math.Max(BadgesIsStart.Count, BadgesIsEnd.Count);
                for (int i = 0; i < maxCount; i++)
                {
                    mergedBadges.Add(new BadgePair
                    {
                        StartBadge = i < BadgesIsStart.Count ? BadgesIsStart[i] : null,
                        EndBadge = i < BadgesIsEnd.Count ? BadgesIsEnd[i] : null
                    });
                }
                return mergedBadges;
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