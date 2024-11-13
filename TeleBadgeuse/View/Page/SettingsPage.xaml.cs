using System.Windows.Controls;
using TeleBadgeuse.Ressources;

namespace TeleBadgeuse
{
    /// <summary>
    /// Page des paramètres de l'application.
    /// </summary>
    public partial class SettingsPage 
    {
        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="SettingsPage"/>.
        /// </summary>
        public SettingsPage()
        {
            InitializeComponent();

            // Définit le DataContext de la page sur le ViewModel principal.
            DataContext = MainWindow.ViewModel;
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Gère le changement de sélection dans le ComboBox de langue.
        /// </summary>
        /// <param name="sender">L'objet source de l'événement.</param>
        /// <param name="e">Les données d'événement de la sélection.</param>
        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                // Récupère les valeurs des ressources de langue.
                string? langFrValue = this.FindResource("LangFr") as string;
                string? langEnValue = this.FindResource("LangEn") as string;

                // Détermine la nouvelle culture en fonction de la sélection.
                string newCulture = selectedItem.Content.ToString() switch
                {
                    var content when content == langFrValue => "fr",
                    var content when content == langEnValue => "en",
                    _ => "en"
                };

                // Définit la nouvelle culture de l'application.
                ResourceDictionaryManager.SetCulture(newCulture);
            }
        }

        #endregion
    }
}