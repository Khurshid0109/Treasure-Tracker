namespace TreasureTracker.Service.DTOs.UserCodes;
public class UserCodeViewModel
{
    public long UserId { get; set; }
    public long Code { get; set; }
    public DateTime ExpireDate { get; set; }
}
