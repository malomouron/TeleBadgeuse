using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.Input;
using TeleBadgeuse.Data;
using TeleBadgeuse.Model;
using TeleBadgeuse.Ressources;

namespace TeleBadgeuse;

/// <summary>
/// ViewModel pour la gestion des paramètres.
/// </summary>
public class SettingsViewModel : INotifyPropertyChanged
{
    #region Attributs

    private readonly ISettingsService _settingsService;
    private ObservableCollection<Settings> _settings;
    private bool _darkTheme;
    private string _imageSource = null!;
    private BitmapImage _icon;
    private int _nbrHoursDays;
    private bool test;

    #endregion

    #region Constructeur

    /// <summary>
    /// Constructeur de la classe SettingsViewModel.
    /// </summary>
    /// <param name="settingsService">Service de gestion des paramètres.</param>
    public SettingsViewModel(ISettingsService settingsService, bool test = false)
    {
        _settingsService = settingsService;
        LoadSettings();

        if (_settingsService.GetSettings("Theme") == null)
        {
            _darkTheme = false;
        }
        else
        {
            _darkTheme = SelectSettingsKey("Theme").Value == "DarkTheme";
        }

        if (_settingsService.GetSettings("MinHours") == null)
        {
            NbrHoursDays = 0;
        }
        else
        {
            NbrHoursDays = int.Parse(SelectSettingsKey("MinHours").Value);
        }
        this.test = test;

        UpdateImageSource();
        UpdateIcon();
    }

    #endregion

    #region Propriétés

    /// <summary>
    /// Collection des paramètres.
    /// </summary>
    public ObservableCollection<Settings> Settings
    {
        get => _settings;
        set
        {
            _settings = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Indique si le thème sombre est activé.
    /// </summary>
    public bool DarkTheme
    {
        get => _darkTheme;
        set
        {
            if (_darkTheme != value)
            {
                _darkTheme = value;
                OnPropertyChanged();
                UpdateTheme();
                UpdateImageSource();
                UpdateIcon();
                UpdateSettingsCommand.Execute(("Theme", _darkTheme ? "DarkTheme" : "LightTheme"));
            }
        }
    }

    /// <summary>
    /// Nombre d'heures par jour.
    /// </summary>
    public int NbrHoursDays
    {
        get => _nbrHoursDays;
        set
        {
            if (_nbrHoursDays != value)
            {
                _nbrHoursDays = value;
                OnPropertyChanged();
                UpdateSettingsCommand.Execute(("MinHours", _nbrHoursDays.ToString()));
            }
        }
    }

    /// <summary>
    /// Source de l'image.
    /// </summary>
    public string ImageSource
    {
        get => _imageSource;
        set
        {
            if (_imageSource != value)
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Icône.
    /// </summary>
    public BitmapImage Icon
    {
        get => _icon;
        set
        {
            if (_icon != value)
            {
                _icon = value;
                OnPropertyChanged();
            }
        }
    }

    #endregion

    #region Commandes

    /// <summary>
    /// Commande pour ajouter un paramètre.
    /// </summary>
    public ICommand AddSettingsCommand => new RelayCommand<object>((param) => 
    {
        var (key, value) = ((string, string))param;
        AddSettings(key, value);
    });

    /// <summary>
    /// Commande pour mettre à jour un paramètre.
    /// </summary>
    public ICommand UpdateSettingsCommand => new RelayCommand<object>((param) =>
    {
        var (key, value) = ((string, string))param;
        UpdateSettings(key, value);
    });

    /// <summary>
    /// Commande pour supprimer un paramètre.
    /// </summary>
    public ICommand DeleteSettingsCommand => new RelayCommand<string>(DeleteSettings);

    /// <summary>
    /// Commande pour modifier la langue.
    /// </summary>
    public ICommand ModifLanguageCommand => new RelayCommand<string>(lang =>
    {
        ResourceDictionaryManager.SetCulture(lang);
        UpdateSettingsCommand.Execute(("Language", lang));
    });

    #endregion

    #region Méthodes

    /// <summary>
    /// Sélectionne un paramètre par clé.
    /// </summary>
    /// <param name="key">Clé du paramètre.</param>
    /// <returns>Le paramètre correspondant à la clé.</returns>
    public Settings SelectSettingsKey(string key) => _settingsService.GetSettings(key);
    
    /// <summary>
    /// Sélectionne les paramètres par valeur.
    /// </summary>
    /// <param name="value">Valeur du paramètre.</param>
    /// <returns>Les paramètres correspondant à la valeur.</returns>
    public IEnumerable<Settings> SelectSettingsValue(string value) => new ObservableCollection<Settings>(_settingsService.GetSettingsByValue(value));

    /// <summary>
    /// Charge les paramètres.
    /// </summary>
    public void LoadSettings()
    {
        Settings = new ObservableCollection<Settings>(_settingsService.GetAllSettings());
    }

    /// <summary>
    /// Ajoute un paramètre.
    /// </summary>
    /// <param name="key">Clé du paramètre.</param>
    /// <param name="value">Valeur du paramètre.</param>
    private void AddSettings(string key, string value)
    {
        _settingsService.AddSettings(new Settings { Key = key, Value = value });
        LoadSettings();
    }

    /// <summary>
    /// Met à jour un paramètre.
    /// </summary>
    /// <param name="key">Clé du paramètre.</param>
    /// <param name="value">Valeur du paramètre.</param>
    private void UpdateSettings(string key, string value)
    {
        _settingsService.UpdateSettings(new Settings { Key = key, Value = value });
        LoadSettings();
    }

    /// <summary>
    /// Supprime un paramètre.
    /// </summary>
    /// <param name="key">Clé du paramètre.</param>
    private void DeleteSettings(string key)
    {
        _settingsService.DeleteSettings(key);
        LoadSettings();
    }

    /// <summary>
    /// Met à jour la source de l'image.
    /// </summary>
    private void UpdateImageSource()
    {
        ImageSource = DarkTheme ? "/Ressources/Icons/IconDark.png" : "/Ressources/Icons/IconLight.png";
    }
        
    /// <summary>
    /// Met à jour l'icône.
    /// </summary>
    private void UpdateIcon()
    {
        var curentDir = Directory.GetCurrentDirectory();
        if (!test)
        {
            Icon = DarkTheme ? new BitmapImage(new Uri("pack://application:,,,/Ressources/Icons/IconDarkFull.ico")) : new BitmapImage(new Uri("pack://application:,,,/Ressources/Icons/IconLightFull.ico"));
        }
    }

    /// <summary>
    /// Met à jour le thème.
    /// </summary>
    private void UpdateTheme()
    {
        string theme = _darkTheme ? "DarkTheme" : "LightTheme";
        if (!test)
        {
            Ressources.ResourceDictionaryManager.SetTheme(theme);
        }
    }

    #endregion

    #region Événements

    /// <summary>
    /// Événement déclenché lorsqu'une propriété change.
    /// </summary>
    /// <param name="propertyName">Nom de la propriété.</param>
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Gestionnaire d'événements pour les changements de propriété.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    #endregion
}