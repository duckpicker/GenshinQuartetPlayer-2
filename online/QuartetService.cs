using GenshinQuartetPlayer2.online.requests;
using GenshinQuartetPlayer2.winforms;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace GenshinQuartetPlayer2.online;

public class QuartetService : WebSocketBehavior
{
    public delegate string? NewClient(object sender);
    public static event NewClient NEW_CLIENT;

    public static event Action<string> ON_BROADCAST_MESSAGE;

    public static void TriggerBroadcast(string message)
    {
        ON_BROADCAST_MESSAGE?.Invoke(message);
    }

    public QuartetService()
    {
        ON_BROADCAST_MESSAGE += BroadcastMessage;
        //HostForm.ON_BROADCAST_MESSAGE += (message) => BroadcastMessage(message); // броадкаст сломал нахуй все починить надо
    }
    private void BroadcastMessage(string message)
    {
        if (Sessions != null && Sessions.IDs.Count() > 0)
        {
            foreach (var session in Sessions.IDs)
            {
                Sessions.SendTo(message, session);
            }
        }
    }


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

                string? midiFile = NEW_CLIENT?.Invoke(this);
                NewMidiFile newMidiFile = new NewMidiFile();
                newMidiFile.ReadFile(midiFile);
                string json = JsonConvert.SerializeObject(newMidiFile);

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