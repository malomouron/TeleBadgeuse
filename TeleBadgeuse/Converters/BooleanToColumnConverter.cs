using System;
using System.Globalization;
using System.Windows.Data;

#region Convertisseur
/// <summary>
/// Convertisseur de booléen en colonne de grille.
/// </summary>
public class BooleanToColumnConverter : IValueConverter
{
    #region Méthodes

    #region Convert
    /// <summary>
    /// Convertit un booléen en index de colonne.
    /// </summary>
    /// <param name="value">La valeur booléenne à convertir.</param>
    /// <param name="targetType">Le type de la valeur cible.</param>
    /// <param name="parameter">Paramètre de conversion.</param>
    /// <param name="culture">Culture utilisée pour la conversion.</param>
    /// <returns>0 si la valeur est true, sinon 1.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isStart)
        {
            Console.WriteLine(isStart ? 0 : 1);
            return isStart ? 0 : 1;
        }
        return 1;
    }
    #endregion

    #region ConvertBack
    /// <summary>
    /// Convertit un index de colonne en booléen.
    /// </summary>
    /// <param name="value">L'index de colonne à convertir.</param>
    /// <param name="targetType">Le type de la valeur cible.</param>
    /// <param name="parameter">Paramètre de conversion.</param>
    /// <param name="culture">Culture utilisée pour la conversion.</param>
    /// <returns>True si l'index est 0, sinon false.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int column)
        {
            return column == 0;
        }
        return false;
    }
    #endregion

    #endregion
}
#endregion