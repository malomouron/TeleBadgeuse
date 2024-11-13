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
                                                      Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                      Key TEXT NOT NULL,
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

    public Settings GetSettings(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Settings WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", id);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Settings
                    {
                        Id = reader.GetInt32(0),
                        Key = reader.GetString(1),
                        Value = reader.GetString(2)
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
                        Id = reader.GetInt32(0),
                        Key = reader.GetString(1),
                        Value = reader.GetString(2)
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
            command.CommandText = "UPDATE Settings SET Key = @Key, Value = @Value WHERE Id = @Id";
            command.Parameters.AddWithValue("@Key", settings.Key);
            command.Parameters.AddWithValue("@Value", settings.Value);
            command.Parameters.AddWithValue("@Id", settings.Id);
            command.ExecuteNonQuery();
        }
    }

    public void DeleteSettings(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Settings WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }
    }

    public IEnumerable<Settings> GetSettingsByKey(string key)
    {
        var settings = new List<Settings>();
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Settings WHERE Key = @Key";
            command.Parameters.AddWithValue("@Key", key);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    settings.Add(new Settings
                    {
                        Id = reader.GetInt32(0),
                        Key = reader.GetString(1),
                        Value = reader.GetString(2)
                    });
                }
            }
        }
        return settings;
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
                        Id = reader.GetInt32(0),
                        Key = reader.GetString(1),
                        Value = reader.GetString(2)
                    });
                }
            }
        }
        return settings;
    }
}