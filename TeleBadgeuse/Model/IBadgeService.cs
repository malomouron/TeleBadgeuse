using TeleBadgeuse.Data;

namespace TeleBadgeuse.Model;

#region Interface IBadgeService
/// <summary>
/// Interface pour le service de gestion des badges.
/// </summary>
public interface IBadgeService
{
    #region Méthodes

    #region AddBadge
    /// <summary>
    /// Ajoute un nouveau badge.
    /// </summary>
    /// <param name="badge">Le badge à ajouter.</param>
    void AddBadge(Badge badge);
    #endregion

    #region GetBadgeById
    /// <summary>
    /// Récupère un badge par son identifiant.
    /// </summary>
    /// <param name="id">L'identifiant du badge.</param>
    /// <returns>Le badge correspondant à l'identifiant.</returns>
    Badge GetBadgeById(int id);
    #endregion

    #region GetAllBadges
    /// <summary>
    /// Récupère tous les badges.
    /// </summary>
    /// <returns>Une collection de tous les badges.</returns>
    IEnumerable<Badge> GetAllBadges();
    #endregion

    #region getBadgesByYear
    /// <summary>
    /// Récupère les badges par année.
    /// </summary>
    /// <param name="year">L'année des badges à récupérer.</param>
    /// <returns>Une collection de badges de l'année spécifiée.</returns>
    IEnumerable<Badge> getBadgesByYear(int year);
    #endregion

    #region getBadgesByMonthYear
    /// <summary>
    /// Récupère les badges par mois et année.
    /// </summary>
    /// <param name="month">Le mois des badges à récupérer.</param>
    /// <param name="year">L'année des badges à récupérer.</param>
    /// <returns>Une collection de badges du mois et de l'année spécifiés.</returns>
    IEnumerable<Badge> getBadgesByMonthYear(int month, int year);
    #endregion

    #region getBadgesByDayMonthYear
    /// <summary>
    /// Récupère les badges par jour, mois et année.
    /// </summary>
    /// <param name="day">Le jour des badges à récupérer.</param>
    /// <param name="month">Le mois des badges à récupérer.</param>
    /// <param name="year">L'année des badges à récupérer.</param>
    /// <returns>Une collection de badges du jour, du mois et de l'année spécifiés.</returns>
    IEnumerable<Badge> getBadgesByDayMonthYear(int day, int month, int year);
    #endregion

    #region UpdateBadge
    /// <summary>
    /// Met à jour un badge existant.
    /// </summary>
    /// <param name="badge">Le badge à mettre à jour.</param>
    void UpdateBadge(Badge badge);
    #endregion

    #region DeleteBadge
    /// <summary>
    /// Supprime un badge par son identifiant.
    /// </summary>
    /// <param name="id">L'identifiant du badge à supprimer.</param>
    void DeleteBadge(int id);
    #endregion

    #endregion
}
#endregion