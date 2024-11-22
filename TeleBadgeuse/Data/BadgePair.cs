namespace TeleBadgeuse.Data;

#region Classe BadgePair
/// <summary>
/// Représente une paire de badges avec un badge de début et un badge de fin.
/// </summary>
public class BadgePair
{
    #region Propriétés

    #region StartBadge
    /// <summary>
    /// Obtient ou définit le badge de début.
    /// </summary>
    public Badge? StartBadge { get; set; }
    #endregion

    #region EndBadge
    /// <summary>
    /// Obtient ou définit le badge de fin.
    /// </summary>
    public Badge? EndBadge { get; set; }
    #endregion

    #region HoursDifference
    /// <summary>
    /// Obtient la différence en heures entre le badge de début et le badge de fin.
    /// </summary>
    public double HoursDifference => (EndBadge?.Date - StartBadge?.Date)?.TotalHours ?? 0;
    #endregion

    #endregion
}
#endregion