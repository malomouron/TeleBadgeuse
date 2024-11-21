using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeleBadgeuse.Data;
using TeleBadgeuse.Model;

namespace TeleBadgeuse.ViewModel
{
    internal class DailyBadgeViewModel
    {
        private readonly IBadgeService _badgeService;
        private ObservableCollection<Badge> _badges;
        private ObservableCollection<BadgePair> _badgePairs;
        private DateTime _currentDate;
        private string _selectedDatePreview;

        public ObservableCollection<Badge> Badges
        {
            get => _badges;
            set
            {
                _badges = value;
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

        public string SelectedDatePreview
        {
            get => _selectedDatePreview;
            set
            {
                _selectedDatePreview = value;
                OnPropertyChanged();
            }
        }

        public DailyBadgeViewModel(IBadgeService badgeService)
        {
            _badgeService = badgeService;
            _currentDate = DateTime.Now;
            SelectedDatePreview = _currentDate.ToString("dd/MM/yyyy");
            LoadBadges();
            LoadBadgesPair();
        }

        public ICommand IncrementCommand => new RelayCommand(IncrementDate);
        public ICommand DecrementCommand => new RelayCommand(DecrementDate);

        private void LoadBadges()
        {
            Badges = new ObservableCollection<Badge>(_badgeService.getBadgesByDayMonthYear(_currentDate.Day, _currentDate.Month, _currentDate.Year));
        }

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

        }

        private void IncrementDate()
        {
            _currentDate = _currentDate.AddDays(1);
            LoadBadges();
            LoadBadgesPair();
            SelectedDatePreview = _currentDate.ToString("dd/MM/yyyy");
            Console.WriteLine("IncrementDate");
        }

        private void DecrementDate()
        {
            _currentDate = _currentDate.AddDays(-1);
            SelectedDatePreview = _currentDate.ToString("dd/MM/yyyy");
            LoadBadges();
            LoadBadgesPair();
            Console.WriteLine("DecrementDate");
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
