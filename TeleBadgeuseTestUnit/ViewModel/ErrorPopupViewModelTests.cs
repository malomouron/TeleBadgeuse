using System.Windows.Input;
using TeleBadgeuse;
using Xunit;

namespace TeleBadgeuseTestUnit.ViewModel
{
    /// <summary>
    /// Tests unitaires pour la classe ErrorPopupViewModel.
    /// </summary>
    public class ErrorPopupViewModelTests
    {
        #region Constructeur Tests

        [Fact]
        public void Constructor_WithValidParameters_SetsPropertiesCorrectly()
        {
            // Arrange
            var expectedMessage = "Erreur de test";
            var expectedCommand = new FakeCommand();

            // Act
            var viewModel = new ErrorPopupViewModel(expectedMessage, expectedCommand);

            // Assert
            Assert.Equal(expectedMessage, viewModel.Message);
            Assert.Equal(expectedCommand, viewModel.OkayCommand);
        }

        [Fact]
        public void Constructor_WithNullMessage_SetsPropertiesCorrectly()
        {
            // Arrange
            string expectedMessage = null;
            var expectedCommand = new FakeCommand();

            // Act
            var viewModel = new ErrorPopupViewModel(expectedMessage, expectedCommand);

            // Assert
            Assert.Null(viewModel.Message);
            Assert.Equal(expectedCommand, viewModel.OkayCommand);
        }

        [Fact]
        public void Constructor_WithNullCommand_SetsPropertiesCorrectly()
        {
            // Arrange
            var expectedMessage = "Erreur de test";
            ICommand expectedCommand = null;

            // Act
            var viewModel = new ErrorPopupViewModel(expectedMessage, expectedCommand);

            // Assert
            Assert.Equal(expectedMessage, viewModel.Message);
            Assert.Null(viewModel.OkayCommand);
        }

        #endregion

        #region Propriétés Tests

        [Fact]
        public void Message_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var viewModel = new ErrorPopupViewModel("Initial message", new FakeCommand());
            var expectedMessage = "Nouveau message";

            // Act
            viewModel.Message = expectedMessage;
            var actualMessage = viewModel.Message;

            // Assert
            Assert.Equal(expectedMessage, actualMessage);
        }

        #endregion

        #region FakeCommand Class

        private class FakeCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter) => true;

            public void Execute(object parameter) { }
        }

        #endregion
    }
}
