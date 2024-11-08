using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
namespace TeleBadgeuse.ViewModel;

public class MainViewModel : INotifyPropertyChanged
{
    private bool _darkTheme;

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
            }
        }
    }

    // Méthode pour charger le bon thème
    private void UpdateTheme()
    {
        var appResources = Application.Current.Resources.MergedDictionaries;

        // Effacer le thème actuel
        appResources.Clear();

        // Charger le thème sombre ou clair en fonction de DarkTheme
        string theme = _darkTheme ? "Ressources/Themes/DarkTheme.xaml" : "Ressources/Themes/LightTheme.xaml";
        appResources.Add(new ResourceDictionary { Source = new Uri(theme, UriKind.Relative) });
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
