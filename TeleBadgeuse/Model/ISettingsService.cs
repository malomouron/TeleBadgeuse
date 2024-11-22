using TeleBadgeuse.Data;

namespace TeleBadgeuse.Model;

#region Interface ISettingsService
/// <summary>
/// Interface pour le service de gestion des paramètres.
/// </summary>
public interface ISettingsService
{
    #region Méthodes

    #region AddSettings
    /// <summary>
    /// Ajoute un nouveau paramètre.
    /// </summary>
    /// <param name="settings">Le paramètre à ajouter.</param>
    void AddSettings(Settings settings);
    #endregion

    #region GetSettings
    /// <summary>
    /// Récupère un paramètre par sa clé.
    /// </summary>
    /// <param name="key">La clé du paramètre.</param>
    /// <returns>Le paramètre correspondant à la clé.</returns>
    Settings GetSettings(string key);
    #endregion

    #region GetAllSettings
    /// <summary>
    /// Récupère tous les paramètres.
    /// </summary>
    /// <returns>Une collection de tous les paramètres.</returns>
    IEnumerable<Settings> GetAllSettings();
    #endregion

    #region UpdateSettings
    /// <summary>
    /// Met à jour un paramètre existant.
    /// </summary>
    /// <param name="settings">Le paramètre à mettre à jour.</param>
    void UpdateSettings(Settings settings);
    #endregion

    #region DeleteSettings
    /// <summary>
    /// Supprime un paramètre par sa clé.
    /// </summary>
    /// <param name="key">La clé du paramètre à supprimer.</param>
    void DeleteSettings(string key);
    #endregion

    #region GetSettingsByValue
    /// <summary>
    /// Récupère les paramètres par valeur.
    /// </summary>
    /// <param name="value">La valeur des paramètres à récupérer.</param>
    /// <returns>Une collection de paramètres correspondant à la valeur spécifiée.</returns>
    IEnumerable<Settings> GetSettingsByValue(string value);
    #endregion

    #endregion
}
#endregion