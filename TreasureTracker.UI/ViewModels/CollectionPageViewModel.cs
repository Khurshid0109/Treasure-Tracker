using TreasureTracker.Domain.Entities;
using TreasureTracker.Service.DTOs.Users;

namespace TreasureTracker.UI.ViewModels;
public class CollectionPageViewModel
{
    public UserViewModel User { get; set; }
    public Collection Collection { get; set; }
}
