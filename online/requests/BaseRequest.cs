namespace GenshinQuartetPlayer2.online.requests;

public class BaseRequest
{
    public string? RequestType { get; set; }

    public BaseRequest()
    {
        RequestType = this.GetType().FullName;
    }
}