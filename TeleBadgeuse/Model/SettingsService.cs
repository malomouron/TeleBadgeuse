using System.Data.SQLite;
using TeleBadgeuse.Data;

namespace TeleBadgeuse.Model;

public class SettingsService : ISettingsService
{
    private readonly string _connectionString;

    public SettingsService(string connectionString)
    {
        _connectionString = connectionString;
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = """
                                  
                                                  CREATE TABLE IF NOT EXISTS Settings (
                                                      Key TEXT PRIMARY KEY NOT NULL,
                                                      Value TEXT NOT NULL
                                                  )
                                  """;
            command.ExecuteNonQuery();
        }
    }

    public void AddSettings(Settings settings)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Settings (Key, Value) VALUES (@Key, @Value)";
            command.Parameters.AddWithValue("@Key", settings.Key);
            command.Parameters.AddWithValue("@Value", settings.Value);
            command.ExecuteNonQuery();
        }
    }

    public Settings GetSettings(string key)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Settings WHERE Key = @Key";
            command.Parameters.AddWithValue("@Key", key);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Settings
                    {
                        Key = reader.GetString(0),
                        Value = reader.GetString(1)
                    };
                }
            }
        }
        return null;
    }

    public IEnumerable<Settings> GetAllSettings()
    {
        var settings = new List<Settings>();
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Settings";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    settings.Add(new Settings
                    {
                        Key = reader.GetString(0),
                        Value = reader.GetString(1)
                    });
                }
            }
        }
        return settings;
    }

    public void UpdateSettings(Settings settings)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Settings SET  Value = @Value WHERE Key = @Key";
            command.Parameters.AddWithValue("@Key", settings.Key);
            command.Parameters.AddWithValue("@Value", settings.Value);
            command.ExecuteNonQuery();
        }
    }

    public void DeleteSettings(string key)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Settings WHERE Key = @Key";
            command.Parameters.AddWithValue("@Key", key);
            command.ExecuteNonQuery();
        }
    }
    

    public IEnumerable<Settings> GetSettingsByValue(string value)
    {
        var settings = new List<Settings>();
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Settings WHERE Value = @Value";
            command.Parameters.AddWithValue("@Value", value);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    settings.Add(new Settings
                    {
                        Key = reader.GetString(0),
                        Value = reader.GetString(1)
                    });
                }
            }
        }
        return settings;
    }
}