using GenshinQuartetPlayer2.online.requests;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace GenshinQuartetPlayer2.online;

public class QuartetService : WebSocketBehavior
{
    public delegate void NewClient(object sender);
    public static event NewClient NEW_CLIENT;
    
    
    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine(e.Data);

        BaseRequest? baseRequest = JsonConvert.DeserializeObject<BaseRequest>(e.Data);

        try
        {
            // connect
            if (typeof(ClientEntry).FullName == baseRequest?.RequestType)
            {
                Console.WriteLine(typeof(ClientEntry).FullName);

                ClientEntry? clientEntry = JsonConvert.DeserializeObject<ClientEntry>(e.Data);
                clientEntry.SessionID = Sessions.IDs.Last();
                
                QuartetServer.Instance.ClientEntries.Add(clientEntry);
                
                NEW_CLIENT?.Invoke(this);

                string json = JsonConvert.SerializeObject(Settings.CurrentMidiFile);

                Send(json);
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }
}