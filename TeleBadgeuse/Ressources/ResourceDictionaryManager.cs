using System.Windows;

namespace TeleBadgeuse.Ressources;

public static class ResourceDictionaryManager
{
    private static string _currentCulture = "fr";
    private static string _currentTheme = "LightTheme";
    
    
    public static void SetCulture(string culture)
    {
        _currentCulture = culture;
        UpdateResources();
    }
    
    public static void SetTheme(string theme)
    {
        _currentTheme = theme;
        UpdateResources();
    }
    
    private static void UpdateResources()
    {
        ClearResources();
        Application.Current.Resources.MergedDictionaries.Add(ChargeCulture());
        Application.Current.Resources.MergedDictionaries.Add(ChargeTheme());
    }
    
    private static ResourceDictionary ChargeCulture()
    {
        ResourceDictionary dictionary = new ResourceDictionary
        {
            Source = new Uri($"Ressources/Strings/{_currentCulture}.xaml", UriKind.Relative)
        };
        return dictionary;
    }
    
    private static ResourceDictionary ChargeTheme()
    {
        ResourceDictionary dictionary = new ResourceDictionary
        {
            Source = new Uri($"Ressources/Themes/{_currentTheme}.xaml", UriKind.Relative)
        };
        return dictionary;
    }
    
    private static void ClearResources()
    {
        Application.Current.Resources.MergedDictionaries.Clear();
    }
}