namespace TeleBadgeuse.Data;

public class BadgePair
{
    public Badge? StartBadge { get; set; }
    public Badge? EndBadge { get; set; }

    public double HoursDifference => (EndBadge?.Date - StartBadge?.Date)?.TotalHours ?? 0;
}
