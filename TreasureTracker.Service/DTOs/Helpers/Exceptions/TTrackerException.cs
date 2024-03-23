namespace TreasureTracker.Service.DTOs.Helpers.Exceptions;
public class TTrackerException:Exception
{
    public int StatusCode { get; set; }
    public TTrackerException(int code,string massage):base(message)
    {
        StatusCode = code;
    }
}
