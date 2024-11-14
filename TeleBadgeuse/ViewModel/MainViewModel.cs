using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace TeleBadgeuse.ViewModel
{
    /// <summary>
    /// ViewModel principal de l'application.
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Attributs

        /// <summary>
        /// Indique si le thème sombre est activé.
        /// </summary>
        private bool _darkTheme;

        /// <summary>
        /// Source de l'image actuelle.
        /// </summary>
        private string _imageSource = null!;
        private BitmapImage _icon;

        #endregion

        #region Propriétés

        /// <summary>
        /// Obtient ou définit une valeur indiquant si le thème sombre est activé.
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
                }
            }
        }

        /// <summary>
        /// Obtient ou définit la source de l'image actuelle.
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

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="MainViewModel"/>.
        /// </summary>
        public MainViewModel(bool darkTheme)
        {
            _darkTheme = darkTheme;
            UpdateImageSource();
            UpdateIcon();
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Met à jour la source de l'image en fonction du thème actuel.
        /// </summary>
        private void UpdateImageSource()
        {
            ImageSource = DarkTheme ? "/Ressources/Icons/IconDark.png" : "/Ressources/Icons/IconLight.png";
        }
        
        private void UpdateIcon()
        {
            Icon = DarkTheme ? new BitmapImage(new Uri("pack://application:,,,/Ressources/Icons/IconDarkFull.ico")) : new BitmapImage(new Uri("pack://application:,,,/Ressources/Icons/IconLightFull.ico"));
        }

        /// <summary>
        /// Met à jour le thème de l'application.
        /// </summary>
        private void UpdateTheme()
        {
            // Charger le thème sombre ou clair en fonction de DarkTheme
            string theme = _darkTheme ? "DarkTheme" : "LightTheme";
            Ressources.ResourceDictionaryManager.SetTheme(theme);
        }

        /// <summary>
        /// Déclenche l'événement PropertyChanged.
        /// </summary>
        /// <param name="propertyName">Nom de la propriété modifiée.</param>
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Événements

        /// <summary>
        /// Se produit lorsqu'une propriété de l'objet change.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion
    }
}