using System;
using System.Globalization;
using Xunit;

namespace TeleBadgeuseTestUnit.Converters
{
    public class BooleanToColumnConverterTests
    {
        private readonly BooleanToColumnConverter _converter = new BooleanToColumnConverter();

        #region Convert Tests

        [Fact]
        public void Convert_True_ReturnsZero()
        {
            // Arrange
            bool value = true;

            // Act
            var result = _converter.Convert(value, typeof(int), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Convert_False_ReturnsOne()
        {
            // Arrange
            bool value = false;

            // Act
            var result = _converter.Convert(value, typeof(int), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void Convert_NonBoolean_ReturnsOne()
        {
            // Arrange
            string value = "not a boolean";

            // Act
            var result = _converter.Convert(value, typeof(int), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.Equal(1, result);
        }

        #endregion

        #region ConvertBack Tests

        [Fact]
        public void ConvertBack_Zero_ReturnsTrue()
        {
            // Arrange
            int value = 0;

            // Act
            var result = _converter.ConvertBack(value, typeof(bool), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.True((bool)result);
        }

        [Fact]
        public void ConvertBack_One_ReturnsFalse()
        {
            // Arrange
            int value = 1;

            // Act
            var result = _converter.ConvertBack(value, typeof(bool), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.False((bool)result);
        }

        [Fact]
        public void ConvertBack_NonInteger_ReturnsFalse()
        {
            // Arrange
            string value = "not an integer";

            // Act
            var result = _converter.ConvertBack(value, typeof(bool), null, CultureInfo.InvariantCulture);

            // Assert
            Assert.False((bool)result);
        }

        #endregion
    }
}
