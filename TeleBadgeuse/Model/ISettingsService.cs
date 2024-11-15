using TeleBadgeuse.Data;

namespace TeleBadgeuse.Model;

public interface ISettingsService
{
    void AddSettings(Settings settings);
    Settings GetSettings(string key);
    IEnumerable<Settings> GetAllSettings();
    void UpdateSettings(Settings settings);
    void DeleteSettings(string key);
    IEnumerable<Settings> GetSettingsByValue(string value);
}