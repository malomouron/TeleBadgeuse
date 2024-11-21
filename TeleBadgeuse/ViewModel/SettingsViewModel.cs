using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.Input;
using TeleBadgeuse.Data;
using TeleBadgeuse.Model;
using TeleBadgeuse.Ressources;

namespace TeleBadgeuse;

public class SettingsViewModel : INotifyPropertyChanged
{
    private readonly ISettingsService _settingsService;
    private ObservableCollection<Settings> _settings;
    private bool _darkTheme;
    private string _imageSource = null!;
    private BitmapImage _icon;
    private int _nbrHoursDays;

    public SettingsViewModel(ISettingsService settingsService)
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

        UpdateImageSource();
        UpdateIcon();
    }

    public ObservableCollection<Settings> Settings
    {
        get => _settings;
        set
        {
            _settings = value;
            OnPropertyChanged();
        }
    }

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

    public ICommand AddSettingsCommand => new RelayCommand<object>((param) => 
    {
        var (key, value) = ((string, string))param;
        AddSettings(key, value);
    });

    public ICommand UpdateSettingsCommand => new RelayCommand<object>((param) =>
    {
        var (key, value) = ((string, string))param;
        UpdateSettings(key, value);
    });

    public ICommand DeleteSettingsCommand => new RelayCommand<string>(DeleteSettings);

    public ICommand ModifLanguageCommand => new RelayCommand<string>(lang =>
    {
        ResourceDictionaryManager.SetCulture(lang);
        UpdateSettingsCommand.Execute(("Language", lang));
    });
    
    public Settings SelectSettingsKey(string key) => _settingsService.GetSettings(key);
    
    public IEnumerable<Settings> SelectSettingsValue(string value) => new ObservableCollection<Settings>(_settingsService.GetSettingsByValue(value));

    private void LoadSettings()
    {
        Settings = new ObservableCollection<Settings>(_settingsService.GetAllSettings());
    }

    private void AddSettings(string key, string value)
    {
        _settingsService.AddSettings(new Settings { Key = key, Value = value });
        LoadSettings();
    }

    private void UpdateSettings(string key, string value)
    {
        _settingsService.UpdateSettings(new Settings { Key = key, Value = value });
        LoadSettings();
    }

    private void DeleteSettings(string key)
    {
        _settingsService.DeleteSettings(key);
        LoadSettings();
    }

    private void UpdateImageSource()
    {
        ImageSource = DarkTheme ? "/Ressources/Icons/IconDark.png" : "/Ressources/Icons/IconLight.png";
    }
        
    private void UpdateIcon()
    {
        Icon = DarkTheme ? new BitmapImage(new Uri("pack://application:,,,/Ressources/Icons/IconDarkFull.ico")) : new BitmapImage(new Uri("pack://application:,,,/Ressources/Icons/IconLightFull.ico"));
    }

    private void UpdateTheme()
    {
        string theme = _darkTheme ? "DarkTheme" : "LightTheme";
        Ressources.ResourceDictionaryManager.SetTheme(theme);
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}