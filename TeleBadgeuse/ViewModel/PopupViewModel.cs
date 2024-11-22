using System.Windows.Input;

namespace TeleBadgeuse
{
    /// <summary>
    /// ViewModel pour la gestion des popups de confirmation.
    /// </summary>
    public class ConfirmationPopupViewModel
    {
        #region Propriétés

        /// <summary>
        /// Message de confirmation à afficher.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Commande pour confirmer l'action.
        /// </summary>
        public ICommand YesCommand { get; }

        /// <summary>
        /// Commande pour annuler l'action.
        /// </summary>
        public ICommand NoCommand { get; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur de la classe ConfirmationPopupViewModel.
        /// </summary>
        /// <param name="message">Message de confirmation à afficher.</param>
        /// <param name="yesCommand">Commande pour confirmer l'action.</param>
        /// <param name="noCommand">Commande pour annuler l'action.</param>
        public ConfirmationPopupViewModel(string message, ICommand yesCommand, ICommand noCommand)
        {
            Message = message;
            YesCommand = yesCommand;
            NoCommand = noCommand;
        }

        #endregion
    }
}