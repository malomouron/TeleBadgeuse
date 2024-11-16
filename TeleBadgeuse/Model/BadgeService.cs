using System.Data.SQLite;
using TeleBadgeuse.Data;

namespace TeleBadgeuse.Model
{
    public class BadgeService : IBadgeService
    {
        private readonly string _connectionString;

        public BadgeService(string connectionString)
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
                                      CREATE TABLE IF NOT EXISTS Badges (
                                          Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                          Date DATETIME NOT NULL,
                                          IsStart INTEGER NOT NULL
                                      )
                                      """;
                command.ExecuteNonQuery();
            }
        }

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
    }
}