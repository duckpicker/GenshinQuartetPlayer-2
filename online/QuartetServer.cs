using WebSocketSharp.Server;

namespace GenshinQuartetPlayer2.online;

public class QuartetServer
{
    private static QuartetServer _instance;

    public static QuartetServer Instance
    {
        get
        {
            if (_instance == null) _instance = new QuartetServer();
            return _instance;
        }
    }


    public List<ClientEntry?> ClientEntries = new List<ClientEntry?>();
    private const string _ipAddress = "0.0.0.0";

    private WebSocketServer Listener;

    public void Start()
    {
        Listener = new WebSocketServer($"ws://{_ipAddress}:{Settings.Port}");
        Listener.AddWebSocketService<QuartetService>("/QuartetService");
        Listener.Start();
        Console.WriteLine($@"Host started {Listener.Address}");
    }
}