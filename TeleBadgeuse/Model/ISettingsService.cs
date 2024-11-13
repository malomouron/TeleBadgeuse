using TeleBadgeuse.Data;

namespace TeleBadgeuse.Model;

public interface ISettingsService
{
    void AddSettings(Settings settings);
    Settings GetSettings(int id);
    IEnumerable<Settings> GetAllSettings();
    void UpdateSettings(Settings settings);
    void DeleteSettings(int id);
    IEnumerable<Settings> GetSettingsByKey(string key);
    IEnumerable<Settings> GetSettingsByValue(string value);
}