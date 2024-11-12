using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
namespace TeleBadgeuse.ViewModel;

public class MainViewModel : INotifyPropertyChanged
{
    private bool _darkTheme;
    private string _imageSource;

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
    
    public MainViewModel()
    {
        UpdateImageSource();
    }
    
    private void UpdateImageSource()
    {
        ImageSource = DarkTheme ? "/Ressources/Icons/IconDark.png" : "/Ressources/Icons/IconLight.png";
    }

    // Méthode pour charger le bon thème
    private void UpdateTheme()
    {
        // Charger le thème sombre ou clair en fonction de DarkTheme
        string theme = _darkTheme ? "DarkTheme" : "LightTheme";
        Ressources.ResourceDictionaryManager.SetTheme(theme);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
