namespace TreasureTracker.Service.DTOs.UserCodes;
public class UserCodeViewModel
{
    public long UserId { get; set; }
    public string Code { get; set; }
    public DateTime ExpireDate { get; set; }
}
