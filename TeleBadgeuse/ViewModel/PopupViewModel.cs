
using System.Windows.Input;

namespace TeleBadgeuse
{
    public class ConfirmationPopupViewModel
    {
        public string Message { get; set; }
        public ICommand YesCommand { get; }
        public ICommand NoCommand { get; }

        public ConfirmationPopupViewModel(string message, ICommand yesCommand, ICommand noCommand)
        {
            Message = message;
            YesCommand = yesCommand;
            NoCommand = noCommand;
        }
    }
}