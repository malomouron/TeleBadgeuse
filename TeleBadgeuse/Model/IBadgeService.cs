using TeleBadgeuse.Data;

namespace TeleBadgeuse.Model;

public interface IBadgeService
{
    void AddBadge(Badge badge);
    Badge GetBadgeById(int id);
    IEnumerable<Badge> GetAllBadges();
    void UpdateBadge(Badge badge);
    void DeleteBadge(int id);
}