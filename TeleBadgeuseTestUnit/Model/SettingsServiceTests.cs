using System;
using System.Collections.Generic;
using System.Data.SQLite;
using TeleBadgeuse.Data;
using TeleBadgeuse.Model;
using Xunit;

namespace TeleBadgeuseTestUnit.Model
{
    /// <summary>
    /// Tests unitaires pour la classe SettingsService.
    /// </summary>
    public class SettingsServiceTests : IDisposable
    {
        private readonly string _connectionString = "Data Source=./data.db";
        private readonly SettingsService _settingsService;

        public SettingsServiceTests()
        {
            InitializeDatabase();
            _settingsService = new SettingsService(_connectionString);
        }

        public void Dispose()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DROP TABLE IF EXISTS Settings";
                command.ExecuteNonQuery();
            }
        }

        public void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Settings (
                        Key TEXT PRIMARY KEY NOT NULL,
                        Value TEXT NOT NULL
                    )
                ";
                command.ExecuteNonQuery();
            }
        }

        #region AddSettings Tests

        [Fact]
        public void AddSettings_ValidSettings_AddsSettings()
        {
            // Arrange
            var settings = new Settings { Key = "TestKey", Value = "TestValue" };

            // Act
            _settingsService.AddSettings(settings);
            var result = _settingsService.GetSettings("TestKey");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("TestKey", result.Key);
            Assert.Equal("TestValue", result.Value);
        }

        [Fact]
        public void AddSettings_DuplicateKey_ThrowsException()
        {
            // Arrange
            var settings = new Settings { Key = "TestKey", Value = "TestValue" };
            _settingsService.AddSettings(settings);

            // Act & Assert
            var ex = Assert.Throws<SQLiteException>(() => _settingsService.AddSettings(settings));
            Assert.Contains("constraint failed", ex.Message);
        }

        #endregion

        #region GetSettings Tests

        [Fact]
        public void GetSettings_ExistingKey_ReturnsSettings()
        {
            // Arrange
            var settings = new Settings { Key = "TestKey", Value = "TestValue" };
            _settingsService.AddSettings(settings);

            // Act
            var result = _settingsService.GetSettings("TestKey");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("TestKey", result.Key);
            Assert.Equal("TestValue", result.Value);
        }

        [Fact]
        public void GetSettings_NonExistingKey_ReturnsNull()
        {
            // Act
            var result = _settingsService.GetSettings("NonExistingKey");

            // Assert
            Assert.Null(result);
        }

        #endregion

        #region GetAllSettings Tests

        [Fact]
        public void GetAllSettings_ReturnsAllSettings()
        {
            // Arrange
            var settings1 = new Settings { Key = "TestKey1", Value = "TestValue1" };
            var settings2 = new Settings { Key = "TestKey2", Value = "TestValue2" };
            _settingsService.AddSettings(settings1);
            _settingsService.AddSettings(settings2);

            // Act
            var result = _settingsService.GetAllSettings();

            // Assert
            Assert.NotNull(result);
            Assert.Collection(result,
                item => Assert.Equal("TestKey1", item.Key),
                item => Assert.Equal("TestKey2", item.Key));
        }

        #endregion

        #region UpdateSettings Tests

        [Fact]
        public void UpdateSettings_ExistingKey_UpdatesSettings()
        {
            // Arrange
            var settings = new Settings { Key = "TestKey", Value = "TestValue" };
            _settingsService.AddSettings(settings);
            var updatedSettings = new Settings { Key = "TestKey", Value = "UpdatedValue" };

            // Act
            _settingsService.UpdateSettings(updatedSettings);
            var result = _settingsService.GetSettings("TestKey");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("TestKey", result.Key);
            Assert.Equal("UpdatedValue", result.Value);
        }

        [Fact]
        public void UpdateSettings_NonExistingKey_DoesNothing()
        {
            // Arrange
            var settings = new Settings { Key = "NonExistingKey", Value = "Value" };

            // Act
            _settingsService.UpdateSettings(settings);
            var result = _settingsService.GetSettings("NonExistingKey");

            // Assert
            Assert.Null(result);
        }

        #endregion

        #region DeleteSettings Tests

        [Fact]
        public void DeleteSettings_ExistingKey_DeletesSettings()
        {
            // Arrange
            var settings = new Settings { Key = "TestKey", Value = "TestValue" };
            _settingsService.AddSettings(settings);

            // Act
            _settingsService.DeleteSettings("TestKey");
            var result = _settingsService.GetSettings("TestKey");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DeleteSettings_NonExistingKey_DoesNothing()
        {
            // Act
            _settingsService.DeleteSettings("NonExistingKey");
            var result = _settingsService.GetSettings("NonExistingKey");

            // Assert
            Assert.Null(result);
        }

        #endregion

        #region GetSettingsByValue Tests

        [Fact]
        public void GetSettingsByValue_ExistingValue_ReturnsSettings()
        {
            // Arrange
            var settings = new Settings { Key = "TestKey", Value = "TestValue" };
            _settingsService.AddSettings(settings);

            // Act
            var result = _settingsService.GetSettingsByValue("TestValue");

            // Assert
            Assert.NotNull(result);
            Assert.Collection(result, item => Assert.Equal("TestKey", item.Key));
        }

        [Fact]
        public void GetSettingsByValue_NonExistingValue_ReturnsEmpty()
        {
            // Act
            var result = _settingsService.GetSettingsByValue("NonExistingValue");

            // Assert
            Assert.Empty(result);
        }

        #endregion
    }
}
