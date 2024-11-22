using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.Input;
using TeleBadgeuse.Model;

namespace TeleBadgeuse;

/// <summary>
/// Logique d'interaction pour HomePage.xaml
/// </summary>
public partial class HomePage
{
    #region Attributs

    /// <summary>
    /// Timer pour mettre à jour l'heure actuelle.
    /// </summary>
    private DispatcherTimer _timer;

    /// <summary>
    /// ViewModel pour la gestion des badges.
    /// </summary>
    private BadgeViewModel _badgeViewModel;

    #endregion

    #region Constructeur

    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="HomePage"/>.
    /// </summary>
    public HomePage()
    {
        InitializeComponent();
        StartClock();
        _badgeViewModel = new BadgeViewModel(new BadgeService(MainWindow.dbPath));
        DataContext = _badgeViewModel;
    }

    #endregion

    #region Méthodes

    /// <summary>
    /// Démarre l'horloge pour mettre à jour l'heure actuelle chaque seconde.
    /// </summary>
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
        // Met à jour l'heure immédiatement avant de démarrer le timer
        CurrentTimeTextBlock.Text = DateTime.Now.ToString("HH:mm:ss");
        _timer.Start();
    }

    /// <summary>
    /// Démarre un nouveau badge.
    /// </summary>
    private void Start()
    {
        _badgeViewModel.AddBadgeCommand.Execute(true);
    }

    /// <summary>
    /// Termine le badge en cours.
    /// </summary>
    private void End()
    {
        _badgeViewModel.AddBadgeCommand.Execute(false);
    }

    /// <summary>
    /// Affiche une popup de confirmation.
    /// </summary>
    /// <param name="message">Le message à afficher.</param>
    /// <param name="yesCommand">Commande à exécuter si l'utilisateur confirme.</param>
    /// <param name="noCommand">Commande à exécuter si l'utilisateur annule.</param>
    private void ShowConfirmationPopup(string message, ICommand yesCommand, ICommand noCommand)
    {
        var popupViewModel = new ConfirmationPopupViewModel(message, yesCommand, noCommand);
        var popup = new Popup { DataContext = popupViewModel };
        PopupContentControl.Content = popup;
        PopupContentControl.Visibility = Visibility.Visible;
    }

    /// <summary>
    /// Affiche une popup d'erreur.
    /// </summary>
    /// <param name="message">Le message d'erreur à afficher.</param>
    /// <param name="okayCommand">Commande à exécuter si l'utilisateur confirme.</param>
    private void ShowErrorPopup(string message, ICommand okayCommand)
    {
        var errorPopupViewModel = new ErrorPopupViewModel(message, okayCommand);
        var errorPopup = new PopupError { DataContext = errorPopupViewModel };
        PopupContentControl.Content = errorPopup;
        PopupContentControl.Visibility = Visibility.Visible;
    }

    /// <summary>
    /// Cache la popup de confirmation.
    /// </summary>
    private void HideConfirmationPopup()
    {
        PopupContentControl.Visibility = Visibility.Collapsed;
        PopupContentControl.Content = null;
    }

    #endregion

    #region Événements

    /// <summary>
    /// Gère l'événement de clic sur le bouton de démarrage.
    /// </summary>
    /// <param name="sender">L'objet source de l'événement.</param>
    /// <param name="e">Les données d'événement de clic.</param>
    private void Start_Click(object sender, RoutedEventArgs e)
    {
        var lastBadge = _badgeViewModel.Badges.LastOrDefault();
        if (lastBadge != null && lastBadge.IsStart)
        {
            ShowErrorPopup((string)Application.Current.Resources["AlreadyStart"], new RelayCommand(HideConfirmationPopup));
            return;
        }

        ShowConfirmationPopup((string)Application.Current.Resources["StartQuestion"], new RelayCommand(() =>
        {
            HideConfirmationPopup();
            Start();
        }), new RelayCommand(HideConfirmationPopup));
    }

    /// <summary>
    /// Gère l'événement de clic sur le bouton de fin.
    /// </summary>
    /// <param name="sender">L'objet source de l'événement.</param>
    /// <param name="e">Les données d'événement de clic.</param>
    private void End_Click(object sender, RoutedEventArgs e)
    {
        var lastBadge = _badgeViewModel.Badges.LastOrDefault();
        if (lastBadge != null && !lastBadge.IsStart)
        {
            ShowErrorPopup((string)Application.Current.Resources["AlreadyEnd"], new RelayCommand(HideConfirmationPopup));
            return;
        }

        ShowConfirmationPopup((string)Application.Current.Resources["EndQuestion"], new RelayCommand(() =>
        {
            HideConfirmationPopup();
            End();
        }), new RelayCommand(HideConfirmationPopup));
    }

    #endregion
}