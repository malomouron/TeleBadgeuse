using TeleBadgeuse.Data;

namespace TeleBadgeuse.Model;

public interface IBadgeService
{
    void AddBadge(Badge badge);
    Badge GetBadgeById(int id);
    IEnumerable<Badge> GetAllBadges();
    IEnumerable<Badge> getBadgesByYear(int year);
    IEnumerable<Badge> getBadgesByMonthYear(int month, int year);
    IEnumerable<Badge> getBadgesByDayMonthYear(int day, int month, int year);
    void UpdateBadge(Badge badge);
    void DeleteBadge(int id);
    
}