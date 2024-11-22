using System.Data.SQLite;
using TeleBadgeuse.Data;

namespace TeleBadgeuse.Model
{
    #region Classe BadgeService
    /// <summary>
    /// Service pour gérer les opérations sur les badges.
    /// </summary>
    public class BadgeService : IBadgeService
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

        #region BadgeService
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="BadgeService"/>.
        /// </summary>
        /// <param name="connectionString">La chaîne de connexion à la base de données.</param>
        public BadgeService(string connectionString)
        {
            _connectionString = connectionString;
            InitializeDatabase();
        }
        #endregion

        #endregion

        #region Méthodes

        #region InitializeDatabase
        /// <summary>
        /// Initialise la base de données en créant la table des badges si elle n'existe pas.
        /// </summary>
        private void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = """
                                      CREATE TABLE IF NOT EXISTS Badges (
                                          Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                          Date DATETIME NOT NULL,
                                          IsStart INTEGER NOT NULL
                                      )
                                      """;
                command.ExecuteNonQuery();
            }
        }
        #endregion

        #region AddBadge
        /// <summary>
        /// Ajoute un nouveau badge à la base de données.
        /// </summary>
        /// <param name="badge">Le badge à ajouter.</param>
        public void AddBadge(Badge badge)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Badges (Date, IsStart) VALUES (@Date, @IsStart)";
                command.Parameters.AddWithValue("@Date", badge.Date);
                command.Parameters.AddWithValue("@IsStart", badge.IsStart ? 1 : 0);
                command.ExecuteNonQuery();
            }
        }
        #endregion

        #region GetBadgeById
        /// <summary>
        /// Récupère un badge par son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant du badge.</param>
        /// <returns>Le badge correspondant à l'identifiant, ou null s'il n'existe pas.</returns>
        public Badge GetBadgeById(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Badges WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Badge
                        {
                            Id = reader.GetInt32(0),
                            Date = reader.GetDateTime(1),
                            IsStart = reader.GetInt32(2) == 1
                        };
                    }
                }
            }
            return null;
        }
        #endregion

        #region GetAllBadges
        /// <summary>
        /// Récupère tous les badges de la base de données.
        /// </summary>
        /// <returns>Une collection de tous les badges.</returns>
        public IEnumerable<Badge> GetAllBadges()
        {
            var badges = new List<Badge>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Badges";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        badges.Add(new Badge
                        {
                            Id = reader.GetInt32(0),
                            Date = reader.GetDateTime(1),
                            IsStart = reader.GetInt32(2) == 1
                        });
                    }
                }
            }
            return badges;
        }
        #endregion

        #region getBadgesByYear
        /// <summary>
        /// Récupère les badges par année.
        /// </summary>
        /// <param name="year">L'année des badges à récupérer.</param>
        /// <returns>Une collection de badges de l'année spécifiée.</returns>
        public IEnumerable<Badge> getBadgesByYear(int year)
        {
            var badges = new List<Badge>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Badges WHERE strftime('%Y', Date) = @Year";
                command.Parameters.AddWithValue("@Year", year.ToString());
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        badges.Add(new Badge
                        {
                            Id = reader.GetInt32(0),
                            Date = reader.GetDateTime(1),
                            IsStart = reader.GetInt32(2) == 1
                        });
                    }
                }
            }
            return badges;
        }
        #endregion

        #region getBadgesByMonthYear
        /// <summary>
        /// Récupère les badges par mois et année.
        /// </summary>
        /// <param name="month">Le mois des badges à récupérer.</param>
        /// <param name="year">L'année des badges à récupérer.</param>
        /// <returns>Une collection de badges du mois et de l'année spécifiés.</returns>
        public IEnumerable<Badge> getBadgesByMonthYear(int month, int year)
        {
            var badges = new List<Badge>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Badges WHERE strftime('%m', Date) = @Month AND strftime('%Y', Date) = @Year";
                command.Parameters.AddWithValue("@Month", month.ToString("00"));
                command.Parameters.AddWithValue("@Year", year.ToString());
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        badges.Add(new Badge
                        {
                            Id = reader.GetInt32(0),
                            Date = reader.GetDateTime(1),
                            IsStart = reader.GetInt32(2) == 1
                        });
                    }
                }
            }
            return badges;
        }
        #endregion

        #region getBadgesByDayMonthYear
        /// <summary>
        /// Récupère les badges par jour, mois et année.
        /// </summary>
        /// <param name="day">Le jour des badges à récupérer.</param>
        /// <param name="month">Le mois des badges à récupérer.</param>
        /// <param name="year">L'année des badges à récupérer.</param>
        /// <returns>Une collection de badges du jour, du mois et de l'année spécifiés.</returns>
        public IEnumerable<Badge> getBadgesByDayMonthYear(int day, int month, int year)
        {
            var badges = new List<Badge>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Badges WHERE strftime('%d', Date) = @Day AND strftime('%m', Date) = @Month AND strftime('%Y', Date) = @Year";
                command.Parameters.AddWithValue("@Day", day.ToString("00"));
                command.Parameters.AddWithValue("@Month", month.ToString("00"));
                command.Parameters.AddWithValue("@Year", year.ToString());
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        badges.Add(new Badge
                        {
                            Id = reader.GetInt32(0),
                            Date = reader.GetDateTime(1),
                            IsStart = reader.GetInt32(2) == 1
                        });
                    }
                }
            }
            return badges;
        }
        #endregion

        #region UpdateBadge
        /// <summary>
        /// Met à jour un badge existant dans la base de données.
        /// </summary>
        /// <param name="badge">Le badge à mettre à jour.</param>
        public void UpdateBadge(Badge badge)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE Badges SET Date = @Date, IsStart = @IsStart WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", badge.Id);
                command.Parameters.AddWithValue("@Date", badge.Date);
                command.Parameters.AddWithValue("@IsStart", badge.IsStart ? 1 : 0);
                command.ExecuteNonQuery();
            }
        }
        #endregion

        #region DeleteBadge
        /// <summary>
        /// Supprime un badge de la base de données par son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant du badge à supprimer.</param>
        public void DeleteBadge(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Badges WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
        #endregion

        #endregion
    }
    #endregion
}