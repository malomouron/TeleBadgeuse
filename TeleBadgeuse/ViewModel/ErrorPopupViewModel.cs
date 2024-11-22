using System.Windows.Input;

namespace TeleBadgeuse
{
    /// <summary>
    /// ViewModel pour la gestion des popups d'erreur.
    /// </summary>
    public class ErrorPopupViewModel
    {
        #region Propriétés

        /// <summary>
        /// Message d'erreur à afficher.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Commande pour fermer le popup d'erreur.
        /// </summary>
        public ICommand OkayCommand { get; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur de la classe ErrorPopupViewModel.
        /// </summary>
        /// <param name="message">Message d'erreur à afficher.</param>
        /// <param name="okayCommand">Commande pour fermer le popup d'erreur.</param>
        public ErrorPopupViewModel(string message, ICommand okayCommand)
        {
            Message = message;
            OkayCommand = okayCommand;
        }

        #endregion
    }
}