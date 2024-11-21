namespace TeleBadgeuse.Data;

public class Badge
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public bool IsStart { get; set; }
    public bool IsEnd
    {
        get => !IsStart;
        set => IsStart = !value;
    }
}