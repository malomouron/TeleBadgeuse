using System.Windows;

namespace TeleBadgeuse.Ressources
{
    /// <summary>
    /// Classe statique pour gérer les dictionnaires de ressources de l'application.
    /// </summary>
    public static class ResourceDictionaryManager
    {
        #region Attributs

        #region _currentCulture
        /// <summary>
        /// Culture actuelle de l'application.
        /// </summary>
        private static string _currentCulture;
        #endregion

        #region _currentTheme
        /// <summary>
        /// Thème actuel de l'application.
        /// </summary>
        private static string _currentTheme = "DarkTheme";
        #endregion

        #endregion

        #region Méthodes

        #region SetCulture
        /// <summary>
        /// Définit la culture de l'application et met à jour les ressources.
        /// </summary>
        /// <param name="culture">Culture à définir.</param>
        public static void SetCulture(string culture)
        {
            _currentCulture = culture;
            UpdateResources();
        }
        #endregion

        #region SetTheme
        /// <summary>
        /// Définit le thème de l'application et met à jour les ressources.
        /// </summary>
        /// <param name="theme">Thème à définir.</param>
        public static void SetTheme(string theme)
        {
            _currentTheme = theme;
            UpdateResources();
        }
        #endregion

        #region UpdateResources
        /// <summary>
        /// Met à jour les dictionnaires de ressources de l'application.
        /// </summary>
        private static void UpdateResources()
        {
            ClearResources();
            Application.Current.Resources.MergedDictionaries.Add(ChargeCulture());
            Application.Current.Resources.MergedDictionaries.Add(ChargeTheme());
        }
        #endregion

        #region ChargeCulture
        /// <summary>
        /// Charge le dictionnaire de ressources pour la culture actuelle.
        /// </summary>
        /// <returns>Dictionnaire de ressources pour la culture actuelle.</returns>
        private static ResourceDictionary ChargeCulture()
        {
            ResourceDictionary dictionary = new ResourceDictionary
            {
                Source = new Uri($"Ressources/Strings/{_currentCulture}.xaml", UriKind.Relative)
            };
            return dictionary;
        }
        #endregion

        #region ChargeTheme
        /// <summary>
        /// Charge le dictionnaire de ressources pour le thème actuel.
        /// </summary>
        /// <returns>Dictionnaire de ressources pour le thème actuel.</returns>
        private static ResourceDictionary ChargeTheme()
        {
            ResourceDictionary dictionary = new ResourceDictionary
            {
                Source = new Uri($"Ressources/Themes/{_currentTheme}.xaml", UriKind.Relative)
            };
            return dictionary;
        }
        #endregion

        #region ClearResources
        /// <summary>
        /// Efface tous les dictionnaires de ressources fusionnés de l'application.
        /// </summary>
        private static void ClearResources()
        {
            Application.Current.Resources.MergedDictionaries.Clear();
        }
        #endregion

        #endregion
    }
}