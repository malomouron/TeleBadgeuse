using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using TeleBadgeuse.Data;
using TeleBadgeuse.Model;

namespace TeleBadgeuse;

public class SettingsViewModel : INotifyPropertyChanged
{
    private readonly ISettingsService _settingsService;
    private ObservableCollection<Settings> _settings;
    private Settings _selectedSettings;

    public SettingsViewModel(ISettingsService settingsService)
    {
        _settingsService = settingsService;
        LoadSettings();
    }

    public ObservableCollection<Settings> Settings
    {
        get => _settings;
        set
        {
            _settings = value;
            OnPropertyChanged(nameof(Settings));
        }
    }

    public Settings SelectedSettings
    {
        get => _selectedSettings;
        set
        {
            _selectedSettings = value;
            OnPropertyChanged(nameof(SelectedSettings));
        }
    }

    public ICommand AddSettingsCommand => new RelayCommand<object>((param) => 
    {
        var (key, value) = ((string, string))param;
        AddSettings(key, value);
    });
    public ICommand UpdateSettingsCommand => new RelayCommand(UpdateSettings);
    public ICommand DeleteSettingsCommand => new RelayCommand(DeleteSettings);


    public IEnumerable<Settings> SelectSettingsKey(string key)
    {
        return new ObservableCollection<Settings>(_settingsService.GetSettingsByKey(key));
    }
    
    public IEnumerable<Settings> SelectSettingsValue(string value)
    {
        return new ObservableCollection<Settings>(_settingsService.GetSettingsByValue(value));
    } 
    private void LoadSettings()
    {
        Settings = new ObservableCollection<Settings>(_settingsService.GetAllSettings());
    }

    private void AddSettings(string Key, string Value)
    {
        _settingsService.AddSettings(new Settings { Key = Key, Value = Value });
        LoadSettings();
    }

    private void UpdateSettings()
    {
        if (SelectedSettings != null)
        {
            _settingsService.UpdateSettings(SelectedSettings);
            LoadSettings();
        }
    }

    private void DeleteSettings()
    {
        if (SelectedSettings != null)
        {
            _settingsService.DeleteSettings(SelectedSettings.Id);
            LoadSettings();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}