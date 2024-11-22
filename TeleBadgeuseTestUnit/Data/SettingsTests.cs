using TeleBadgeuse.Data;
using Xunit;

namespace TeleBadgeuseTestUnit.Data
{
    /// <summary>
    /// Tests unitaires pour la classe Settings.
    /// </summary>
    public class SettingsTests
    {
        #region Key Tests

        [Fact]
        public void Key_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var settings = new Settings();
            var expectedKey = "TestKey";

            // Act
            settings.Key = expectedKey;
            var actualKey = settings.Key;

            // Assert
            Assert.Equal(expectedKey, actualKey);
        }

        [Fact]
        public void Key_SetNullValue_ReturnsNull()
        {
            // Arrange
            var settings = new Settings();

            // Act
            settings.Key = null;
            var actualKey = settings.Key;

            // Assert
            Assert.Null(actualKey);
        }

        #endregion

        #region Value Tests

        [Fact]
        public void Value_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var settings = new Settings();
            var expectedValue = "TestValue";

            // Act
            settings.Value = expectedValue;
            var actualValue = settings.Value;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void Value_SetNullValue_ReturnsNull()
        {
            // Arrange
            var settings = new Settings();

            // Act
            settings.Value = null;
            var actualValue = settings.Value;

            // Assert
            Assert.Null(actualValue);
        }

        #endregion
    }
}
