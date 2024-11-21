using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using TeleBadgeuse.Data;
using TeleBadgeuse.Model;
using System.Windows;

namespace TeleBadgeuse
{
    public class BadgeViewModel : INotifyPropertyChanged
    {
        private readonly IBadgeService _badgeService;
        private ObservableCollection<Badge> _badges;
        private ObservableCollection<BadgePair> _badgePairs;
        private bool _isAllSelected;
        private bool _isYearsSelected;
        private bool _isMonthsSelected;
        private bool _isDaysSelected;
        private DateTime _currentDate;
        private string _selectedDatePreview;
        private readonly ProfilePage _profilePage;
        private bool _isViewListSelected;
        private bool _isViewGridSelected;

        private bool _status;
        
        public bool IsInfoDisplay
        {
            get => IsDaysSelected;
        }
        public bool IsViewListSelected
        {
            get => _isViewListSelected;
            set
            {
                if (value)
                {
                    SettingsPage.SettingsViewModel.UpdateSettingsCommand.Execute(("ViewSelected", "ViewList"));
                }
                _isViewListSelected = value;
                OnPropertyChanged();
            }
        }

        public bool IsViewGridSelected
        {
            get => _isViewGridSelected;
            set
            {
                if (value) {
                    SettingsPage.SettingsViewModel.UpdateSettingsCommand.Execute(("ViewSelected", "ViewGrid"));
                }
                _isViewGridSelected = value;
                OnPropertyChanged();
            }
        }

        public BadgeViewModel(IBadgeService badgeService, ProfilePage profilePage = null)
        {
            _badgeService = badgeService;
            _currentDate = DateTime.Now;
            IsDaysSelected = true; // Default selection

            switch (SettingsPage.SettingsViewModel.SelectSettingsKey("ViewSelected")?.Value)
            {
                case "ViewGrid":
                    IsViewGridSelected = true;
                    break;
                case "ViewList":
                    IsViewListSelected = true;
                    break;
                default:
                    IsViewListSelected = true;
                    break;
            }
            LoadBadges();
            LoadBadgesPair();
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
                LoadBadges();
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
                LoadBadges();
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
                LoadBadges();
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
                OnPropertyChanged(nameof(IsInfoDisplay));
                LoadBadges();
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
        
        public ObservableCollection<BadgePair> BadgePairs
        {
            get
            {
                return _badgePairs;
            }
            set
            {
                _badgePairs = value;
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

        private void LoadBadgesPair()
        {
            var badgesIsStart = Badges.Where(b => b.IsStart).ToList();
            var badgesIsEnd = Badges.Where(b => b.IsEnd).ToList();
            var maxCount = Math.Max(badgesIsStart.Count, badgesIsEnd.Count);
            var mergedBadges = new ObservableCollection<BadgePair>(Enumerable.Range(0, maxCount).Select(i => new BadgePair
            {
                StartBadge = i < badgesIsStart.Count ? badgesIsStart[i] : null,
                EndBadge = i < badgesIsEnd.Count ? badgesIsEnd[i] : null
            }));
            BadgePairs = mergedBadges;
            OnPropertyChanged(nameof(TotalHours));
            OnPropertyChanged(nameof(RemainingHours));
            UpdateStatus();
            VerifData();
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

        public double TotalHours
        {
            get
            {
                double hours = 0;
                foreach (var badge in BadgePairs)
                {
                    if (badge.StartBadge != null && badge.EndBadge != null)
                    {
                        hours += badge.HoursDifference;
                    }
                    if (badge.StartBadge != null && badge.EndBadge == null)
                    {
                        hours += (DateTime.Now - badge.StartBadge?.Date)?.TotalHours ?? 0;
                    }
                }
                return Math.Round(hours, 2);
            }
        }

        public double RemainingHours
        {
            get
            {
                if (double.TryParse(SettingsPage.SettingsViewModel.SelectSettingsKey("MinHours")?.Value, out double minHours))
                {
                    return Math.Round(Math.Max(minHours - TotalHours , 0), 2);
                }
                return Math.Round(TotalHours, 2);
            }
        }

        public double ExtraHours
        {
            get
            {
                if (double.TryParse(SettingsPage.SettingsViewModel.SelectSettingsKey("MinHours")?.Value, out double minHours))
                {
                    return Math.Round(Math.Max(TotalHours - minHours, 0), 2);
                }
                return 0;
            }
        }


        public string Status
        {
            get { return _status ? (string)Application.Current.Resources["Work"] : (string)Application.Current.Resources["Break"]; }
        }

        public string HoursMinToLeave
        {
            get {
                if (_status)
                {
                    int heure = (int)Math.Round(RemainingHours, 0) + DateTime.Now.Hour;
                    int minute = (int)Math.Round((RemainingHours - Math.Floor(RemainingHours)) * 60, 0) + DateTime.Now.Minute;
                    if (minute >= 60)
                    {
                        minute -= 60;
                        heure += 1;
                    }
                    return new DateTime(1, 1, 1, heure, minute, 0).ToString("HH:mm");
                }
                else
                {
                    return (string)Application.Current.Resources["Unknown"];
                }
            }
        }

        public void VerifData()
        {
            foreach (var badge in BadgePairs)
            {
                if (badge.StartBadge == null && badge.EndBadge != null)
                {
                    throw new Exception("End badge without start badge");
                }
                if (badge.StartBadge != null && badge.EndBadge != null && badge.StartBadge.Date > badge.EndBadge.Date)
                {
                    throw new Exception("Start badge after end badge");
                }
            }
        }


        public void UpdateStatus()
        {
            var lastBadge = BadgePairs.LastOrDefault();
            _status = lastBadge != null && lastBadge.StartBadge != null && lastBadge.EndBadge == null;
            OnPropertyChanged(nameof(Status));
        }


        private void LoadBadges()
        {
            if (IsAllSelected)
            {
                Badges = new ObservableCollection<Badge>(_badgeService.GetAllBadges().OrderBy(b => b.Date));
                LoadBadgesPair();
                SelectedDatePreview = string.Empty;
            }
            else if (IsYearsSelected)
            {
                Badges = new ObservableCollection<Badge>(_badgeService.getBadgesByYear(_currentDate.Year).OrderBy(b => b.Date));
                LoadBadgesPair();
                SelectedDatePreview = _currentDate.ToString("yyyy");
            }
            else if (IsMonthsSelected)
            {
                Badges = new ObservableCollection<Badge>(_badgeService.getBadgesByMonthYear(_currentDate.Month, _currentDate.Year).OrderBy(b => b.Date));
                LoadBadgesPair();
                SelectedDatePreview = _currentDate.ToString("yyyy-MM");
            }
            else if (IsDaysSelected)
            {
                Badges = new ObservableCollection<Badge>(_badgeService.getBadgesByDayMonthYear(_currentDate.Day, _currentDate.Month, _currentDate.Year).OrderBy(b => b.Date));
                LoadBadgesPair();
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
            LoadBadges();
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
            LoadBadges();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}