using System.Data.SQLite;
using TeleBadgeuse.Data;

namespace TeleBadgeuse.Model;

#region Classe SettingsService
/// <summary>
/// Service pour gérer les opérations sur les paramètres.
/// </summary>
public class SettingsService : ISettingsService
{
    #region Attributs

    #region _connectionString
    /// <summary>
    /// Chaîne de connexion à la base de données SQLite.
    /// </summary>
    private readonly string _connectionString;
    #endregion

    #endregion

    #region Constructeurs

    #region SettingsService
    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="SettingsService"/>.
    /// </summary>
    /// <param name="connectionString">La chaîne de connexion à la base de données.</param>
    public SettingsService(string connectionString)
    {
        _connectionString = connectionString;
        InitializeDatabase();
    }
    #endregion

    #endregion

    #region Méthodes

    #region InitializeDatabase
    /// <summary>
    /// Initialise la base de données en créant la table des paramètres si elle n'existe pas.
    /// </summary>
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
    #endregion

    #region AddSettings
    /// <summary>
    /// Ajoute un nouveau paramètre à la base de données.
    /// </summary>
    /// <param name="settings">Le paramètre à ajouter.</param>
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
    #endregion

    #region GetSettings
    /// <summary>
    /// Récupère un paramètre par sa clé.
    /// </summary>
    /// <param name="key">La clé du paramètre.</param>
    /// <returns>Le paramètre correspondant à la clé, ou null s'il n'existe pas.</returns>
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
    #endregion

    #region GetAllSettings
    /// <summary>
    /// Récupère tous les paramètres de la base de données.
    /// </summary>
    /// <returns>Une collection de tous les paramètres.</returns>
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
    #endregion

    #region UpdateSettings
    /// <summary>
    /// Met à jour un paramètre existant dans la base de données.
    /// </summary>
    /// <param name="settings">Le paramètre à mettre à jour.</param>
    public void UpdateSettings(Settings settings)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Settings SET Value = @Value WHERE Key = @Key";
            command.Parameters.AddWithValue("@Key", settings.Key);
            command.Parameters.AddWithValue("@Value", settings.Value);
            command.ExecuteNonQuery();
        }
    }
    #endregion

    #region DeleteSettings
    /// <summary>
    /// Supprime un paramètre de la base de données par sa clé.
    /// </summary>
    /// <param name="key">La clé du paramètre à supprimer.</param>
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
    #endregion

    #region GetSettingsByValue
    /// <summary>
    /// Récupère les paramètres par valeur.
    /// </summary>
    /// <param name="value">La valeur des paramètres à récupérer.</param>
    /// <returns>Une collection de paramètres correspondant à la valeur spécifiée.</returns>
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
    #endregion

    #endregion
}
#endregion