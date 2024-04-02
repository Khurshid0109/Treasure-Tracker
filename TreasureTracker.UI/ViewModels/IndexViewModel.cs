using TreasureTracker.Domain.Entities;
using TreasureTracker.Service.DTOs.Users;

namespace TreasureTracker.UI.ViewModels;

public class IndexViewModel
{
    public UserViewModel User { get; set; }
    public IEnumerable<Collection> Collections { get; set; }
}
