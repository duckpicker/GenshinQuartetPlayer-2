using GenshinQuartetPlayer2.online.requests;

namespace GenshinQuartetPlayer2.online;

public class ClientEntry : BaseRequest
{
    public string SessionID { get; set; } = "";
    public int MaxPing { get; set; }
    public string Username { get; set; }
    public int Ping { get; set; }
    public bool IsReady { get; set; } = false;

    public int ServerToClientPing = 0; // todo rework

    public int ServerToClientPingCounts = 0;

    public ClientEntry(int id, string username, int ping)
    {
        Username = username;
        Ping = ping;
    }
}