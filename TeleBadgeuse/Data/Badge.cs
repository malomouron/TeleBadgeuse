namespace TeleBadgeuse.Data;

#region Classe Badge
/// <summary>
/// Représente un badge avec un identifiant, une date et des indicateurs de début et de fin.
/// </summary>
public class Badge
{
    #region Propriétés

    #region Id
    /// <summary>
    /// Obtient ou définit l'identifiant du badge.
    /// </summary>
    public int Id { get; set; }
    #endregion

    #region Date
    /// <summary>
    /// Obtient ou définit la date du badge.
    /// </summary>
    public DateTime Date { get; set; }
    #endregion

    #region IsStart
    /// <summary>
    /// Obtient ou définit une valeur indiquant si le badge est un début.
    /// </summary>
    public bool IsStart { get; set; }
    #endregion

    #region IsEnd
    /// <summary>
    /// Obtient ou définit une valeur indiquant si le badge est une fin.
    /// </summary>
    public bool IsEnd
    {
        get => !IsStart;
        set => IsStart = !value;
    }
    #endregion

    #endregion
}
#endregion