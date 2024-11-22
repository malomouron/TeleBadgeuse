using System.Windows.Input;
using TeleBadgeuse;
using Xunit;

namespace TeleBadgeuseTestUnit.ViewModel
{
    /// <summary>
    /// Tests unitaires pour la classe ConfirmationPopupViewModel.
    /// </summary>
    public class ConfirmationPopupViewModelTests
    {
        #region Constructeur Tests

        [Fact]
        public void Constructor_WithValidParameters_SetsPropertiesCorrectly()
        {
            // Arrange
            var message = "Confirmez-vous cette action ?";
            var yesCommand = new MockCommand();
            var noCommand = new MockCommand();

            // Act
            var viewModel = new ConfirmationPopupViewModel(message, yesCommand, noCommand);

            // Assert
            Assert.Equal(message, viewModel.Message);
            Assert.Equal(yesCommand, viewModel.YesCommand);
            Assert.Equal(noCommand, viewModel.NoCommand);
        }

        #endregion

        #region Propriétés Tests

        [Fact]
        public void Message_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var viewModel = new ConfirmationPopupViewModel("Initial Message", new MockCommand(), new MockCommand());
            var expectedMessage = "Nouveau message de confirmation";

            // Act
            viewModel.Message = expectedMessage;
            var actualMessage = viewModel.Message;

            // Assert
            Assert.Equal(expectedMessage, actualMessage);
        }

        #endregion

        #region Commande Tests

        [Fact]
        public void YesCommand_Execute_CallsExecuteMethod()
        {
            // Arrange
            var yesCommand = new MockCommand();
            var viewModel = new ConfirmationPopupViewModel("Message", yesCommand, new MockCommand());

            // Act
            yesCommand.Execute(null);

            // Assert
            Assert.True(yesCommand.Executed);
        }

        [Fact]
        public void NoCommand_Execute_CallsExecuteMethod()
        {
            // Arrange
            var noCommand = new MockCommand();
            var viewModel = new ConfirmationPopupViewModel("Message", new MockCommand(), noCommand);

            // Act
            noCommand.Execute(null);

            // Assert
            Assert.True(noCommand.Executed);
        }

        #endregion

        #region MockCommand Class

        private class MockCommand : ICommand
        {
            public bool Executed { get; private set; }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter) => true;

            public void Execute(object parameter)
            {
                Executed = true;
            }
        }

        #endregion
    }
}
