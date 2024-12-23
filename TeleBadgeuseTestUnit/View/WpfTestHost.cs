using System.Data.SQLite;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using TeleBadgeuse;
using TeleBadgeuse.Model;
using Xunit;

namespace TeleBadgeuseTestUnit.View;

[CollectionDefinition(Name, DisableParallelization = true)]
public sealed class WpfTestCollection : ICollectionFixture<WpfTestHost>
{
    public const string Name = "WPF tests";
}

public sealed class WpfTestHost : IDisposable
{
    private readonly string _previousCurrentDirectory;
    private readonly string _testDirectory;
    private readonly Thread _dispatcherThread;
    private readonly Dispatcher _dispatcher;

    public WpfTestHost()
    {
        _previousCurrentDirectory = Environment.CurrentDirectory;
        _testDirectory = Path.Combine(Path.GetTempPath(), $"TeleBadgeuseWpfTests_{Guid.NewGuid():N}");
        Directory.CreateDirectory(_testDirectory);
        Environment.CurrentDirectory = _testDirectory;

        using var dispatcherStarted = new ManualResetEventSlim();
        Exception? startupException = null;
        Dispatcher? dispatcher = null;
        _dispatcherThread = new Thread(() =>
        {
            try
            {
                var application = new Application();
                application.ShutdownMode = ShutdownMode.OnExplicitShutdown;
                dispatcher = Dispatcher.CurrentDispatcher;
            }
            catch (Exception exception)
            {
                startupException = exception;
            }
            finally
            {
                dispatcherStarted.Set();
            }

            if (startupException is null)
            {
                Dispatcher.Run();
            }
        })
        {
            IsBackground = true,
            Name = "TeleBadgeuse WPF test dispatcher"
        };
        _dispatcherThread.SetApartmentState(ApartmentState.STA);
        _dispatcherThread.Start();
        dispatcherStarted.Wait();

        if (startupException is not null)
        {
            Environment.CurrentDirectory = _previousCurrentDirectory;
            ExceptionDispatchInfo.Capture(startupException).Throw();
        }

        _dispatcher = dispatcher!;
    }

    public void Run(Action test)
    {
        _dispatcher.Invoke(() =>
        {
            ResetApplicationState();
            test();
        });
    }

    public void Dispose()
    {
        try
        {
            _dispatcher.Invoke(() =>
            {
                for (var index = Application.Current.Windows.Count - 1; index >= 0; index--)
                {
                    Application.Current.Windows[index].Close();
                }
            });
            _dispatcher.BeginInvokeShutdown(DispatcherPriority.Background);
            _dispatcherThread.Join();
        }
        finally
        {
            Environment.CurrentDirectory = _previousCurrentDirectory;
        }
    }

    private void ResetApplicationState()
    {
        ResetDatabase();
        ResetResources();
        Application.Current.MainWindow = null;
        SettingsPage.SettingsViewModel = new SettingsViewModel(new SettingsService(MainWindow.dbPath), test: true);
    }

    private static void ResetDatabase()
    {
        using var connection = new SQLiteConnection(MainWindow.dbPath);
        connection.Open();
        using var command = connection.CreateCommand();

        command.CommandText = """
                              CREATE TABLE IF NOT EXISTS Settings (
                                  Key TEXT PRIMARY KEY NOT NULL,
                                  Value TEXT NOT NULL
                              )
                              """;
        command.ExecuteNonQuery();
        command.CommandText = """
                              CREATE TABLE IF NOT EXISTS Badges (
                                  Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                  Date DATETIME NOT NULL,
                                  IsStart INTEGER NOT NULL
                              )
                              """;
        command.ExecuteNonQuery();
        command.CommandText = "DELETE FROM Settings";
        command.ExecuteNonQuery();
        command.CommandText = "DELETE FROM Badges";
        command.ExecuteNonQuery();
    }

    private static void ResetResources()
    {
        var dictionaries = Application.Current.Resources.MergedDictionaries;
        dictionaries.Clear();
        dictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/TeleBadgeuse;component/Ressources/Style.xaml", UriKind.Absolute)
        });
        dictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/TeleBadgeuse;component/Ressources/Strings/fr.xaml", UriKind.Absolute)
        });
        dictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/TeleBadgeuse;component/Ressources/Themes/DarkTheme.xaml", UriKind.Absolute)
        });
    }
}
