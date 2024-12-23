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
    /// <summary>
    /// ViewModel pour la gestion des badges.
    /// </summary>
    public class BadgeViewModel : INotifyPropertyChanged
    {
        #region Champs privés

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
        private bool test;
        private SettingsViewModel svm = null;

        #endregion

        #region Propriétés

        /// <summary>
        /// Indique si la vue liste est sélectionnée.
        /// </summary>
        public bool IsViewListSelected
        {
            get => _isViewListSelected;
            set
            {
                if (value)
                {
                    if (svm != null)
                    {
                        svm.UpdateSettingsCommand.Execute(("ViewSelected", "ViewList"));
                    }
                    else
                    {
                        SettingsPage.SettingsViewModel.UpdateSettingsCommand.Execute(("ViewSelected", "ViewList"));
                    }
                }
                _isViewListSelected = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Indique si la vue grille est sélectionnée.
        /// </summary>
        public bool IsViewGridSelected
        {
            get => _isViewGridSelected;
            set
            {
                if (value) {
                    if (svm != null)
                    {
                        svm.UpdateSettingsCommand.Execute(("ViewSelected", "ViewGrid"));
                    }
                    else
                    {
                        SettingsPage.SettingsViewModel.UpdateSettingsCommand.Execute(("ViewSelected", "ViewGrid"));
                    }
                }
                _isViewGridSelected = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Indique si tous les badges sont sélectionnés.
        /// </summary>
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

        /// <summary>
        /// Indique si les badges de l'année sont sélectionnés.
        /// </summary>
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

        /// <summary>
        /// Indique si les badges du mois sont sélectionnés.
        /// </summary>
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

        /// <summary>
        /// Indique si les badges du jour sont sélectionnés.
        /// </summary>
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

        /// <summary>
        /// Indique si les informations sont affichées.
        /// </summary>
        public bool IsInfoDisplay
        {
            get => IsDaysSelected;
        }

        /// <summary>
        /// Collection des badges.
        /// </summary>
        public ObservableCollection<Badge> Badges
        {
            get => _badges;
            set
            {
                _badges = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Collection des paires de badges.
        /// </summary>
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

        /// <summary>
        /// Aperçu de la date sélectionnée.
        /// </summary>
        public string SelectedDatePreview
        {
            get => _selectedDatePreview;
            set
            {
                _selectedDatePreview = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Total des heures calculées.
        /// </summary>
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

        /// <summary>
        /// Heures restantes à travailler.
        /// </summary>
        public double RemainingHours
        {
            get
            {
                var settingsViewModel = svm ?? SettingsPage.SettingsViewModel;
                if (double.TryParse(settingsViewModel.SelectSettingsKey("MinHours")?.Value, out double minHours))
                {
                    return Math.Round(Math.Max(minHours - TotalHours, 0), 2);
                }
                return Math.Round(TotalHours, 2);
            }
        }

        /// <summary>
        /// Heures supplémentaires travaillées.
        /// </summary>
        public double ExtraHours
        {
            get
            {
                var settingsViewModel = svm ?? SettingsPage.SettingsViewModel;
                if (double.TryParse(settingsViewModel.SelectSettingsKey("MinHours")?.Value, out double minHours))
                {
                    return Math.Round(Math.Max(TotalHours - minHours, 0), 2);
                }
                return 0;
            }
        }

        /// <summary>
        /// Statut actuel (travail ou pause).
        /// </summary>
        public string Status
        {
            get { return _status ? (string)Application.Current.Resources["Work"] : (string)Application.Current.Resources["Break"]; }
        }

        /// <summary>
        /// Heure estimée de départ.
        /// </summary>
        public string HoursMinToLeave
        {
            get
            {
                if (_status)
                {
                    int heure = (int)Math.Round(RemainingHours, 0) + DateTime.Now.Hour;
                    int minute = (int)Math.Round((RemainingHours - Math.Floor(RemainingHours)) * 60, 0) + DateTime.Now.Minute;
                    if (minute >= 60)
                    {
                        minute -= 60;
                        heure++;
                    }
                    if (heure >= 24)
                    {
                        heure -= 24;
                    }
                    return new DateTime(1, 1, 1, heure, minute, 0).ToString("HH:mm");
                }
                else
                {
                    return (string)Application.Current.Resources["Unknown"];
                }
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur de la classe BadgeViewModel.
        /// </summary>
        /// <param name="badgeService">Service de gestion des badges.</param>
        /// <param name="profilePage">Page de profil (optionnelle).</param>
        public BadgeViewModel(IBadgeService badgeService, ProfilePage profilePage = null, bool test = false)
        {
            _badgeService = badgeService;
            _currentDate = DateTime.Now;
            IsDaysSelected = true; // Sélection par défaut
            this.test = test;
            if (test)
            {
                svm = new SettingsViewModel(new SettingsService(MainWindow.dbPath), test);

                if (svm.SelectSettingsKey("Theme") == null)
                {
                    svm.AddSettingsCommand.Execute(("Theme", "DarkTheme"));
                }
                if (svm.SelectSettingsKey("DefaultPage") == null)
                {
                    svm.AddSettingsCommand.Execute(("DefaultPage", "/View/Page/HomePage.xaml"));
                }
                if (svm.SelectSettingsKey("Language") == null)
                {
                    svm.AddSettingsCommand.Execute(("Language", "fr"));
                }
                if (svm.SelectSettingsKey("MinHours") == null)
                {
                    svm.AddSettingsCommand.Execute(("MinHours", "7"));
                }
                if (svm.SelectSettingsKey("ViewSelected") == null)
                {
                    svm.AddSettingsCommand.Execute(("ViewSelected", "ViewGrid"));
                }
            }

            if (svm == null)
            {
                switch (SettingsPage.SettingsViewModel.SelectSettingsKey("ViewSelected")?.Value)
                {
                    case "ViewGrid":
                        IsViewGridSelected = true;
                        break;
                    case "ViewList":
                    default:
                        IsViewListSelected = true;
                        break;
                }
            }
            else if (svm.SelectSettingsKey("ViewSelected") != null)
            {
                switch (svm.SelectSettingsKey("ViewSelected")?.Value)
                {
                    case "ViewGrid":
                        IsViewGridSelected = true;
                        break;
                    case "ViewList":
                    default:
                        IsViewListSelected = true;
                        break;
                }
            }

            LoadBadges();
            LoadBadgesPair();
            _profilePage = profilePage;
        }

        #endregion

        #region Commandes

        /// <summary>
        /// Commande pour ajouter un badge.
        /// </summary>
        public ICommand AddBadgeCommand => new RelayCommand<bool>(AddBadge);

        /// <summary>
        /// Commande pour mettre à jour un badge.
        /// </summary>
        public ICommand UpdateBadgeCommand => new RelayCommand<object>((param) =>
        {
            var (id, date, isStart) = ((int, DateTime, bool))param;
            UpdateBadge(id, date, isStart);
        });

        /// <summary>
        /// Commande pour supprimer un badge.
        /// </summary>
        public ICommand DeleteBadgeCommand => new RelayCommand<int>(DeleteBadge);

        /// <summary>
        /// Commande pour incrémenter la date.
        /// </summary>
        public ICommand IncrementCommand => new RelayCommand(IncrementDate);

        /// <summary>
        /// Commande pour décrémenter la date.
        /// </summary>
        public ICommand DecrementCommand => new RelayCommand(DecrementDate);

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Charge les paires de badges.
        /// </summary>
        public void LoadBadgesPair()
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

        /// <summary>
        /// Ajoute un nouveau badge.
        /// </summary>
        /// <param name="isStart">Indique si le badge est un badge de début.</param>
        private void AddBadge(bool isStart)
        {
            var newBadge = new Badge { Date = DateTime.Now, IsStart = isStart };
            _badgeService.AddBadge(newBadge);
            LoadBadges();
        }

        /// <summary>
        /// Met à jour un badge existant.
        /// </summary>
        /// <param name="id">Identifiant du badge.</param>
        /// <param name="date">Date du badge.</param>
        /// <param name="isStart">Indique si le badge est un badge de début.</param>
        private void UpdateBadge(int id, DateTime date, bool isStart)
        {
            var badge = new Badge { Id = id, Date = date, IsStart = isStart };
            _badgeService.UpdateBadge(badge);
            LoadBadges();
        }

        /// <summary>
        /// Supprime un badge existant.
        /// </summary>
        /// <param name="id">Identifiant du badge.</param>
        private void DeleteBadge(int id)
        {
            _badgeService.DeleteBadge(id);
            LoadBadges();
        }

        /// <summary>
        /// Vérifie les données des paires de badges.
        /// </summary>
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

        /// <summary>
        /// Met à jour le statut actuel.
        /// </summary>
        public void UpdateStatus()
        {
            var lastBadge = BadgePairs.LastOrDefault();
            _status = lastBadge != null && lastBadge.StartBadge != null && lastBadge.EndBadge == null;
            OnPropertyChanged(nameof(Status));
        }

        /// <summary>
        /// Charge les badges en fonction de la sélection.
        /// </summary>
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

        /// <summary>
        /// Incrémente la date sélectionnée.
        /// </summary>
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

        /// <summary>
        /// Décrémente la date sélectionnée.
        /// </summary>
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

        #endregion

        #region Événements

        /// <summary>
        /// Événement déclenché lorsqu'une propriété change.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Déclenche l'événement PropertyChanged.
        /// </summary>
        /// <param name="propertyName">Nom de la propriété.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}