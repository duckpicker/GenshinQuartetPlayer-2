using GenshinQuartetPlayer2.online.requests;
using GenshinQuartetPlayer2.winforms;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace GenshinQuartetPlayer2.online;

public class QuartetService : WebSocketBehavior
{
    // INVOKE TO FORM ----------------------------------
    // update clients
    public delegate string? UpdateClients(object sender);
    public static event UpdateClients UPDATE_CLIENTS;

    // get client settings
    public delegate void GetClientSettings(ClientNewSettingsEntry settings);
    public static event GetClientSettings GET_CLIENT_SETTINGS;



    // INVOKE FROM FORM ----------------------------------
    // broadcast message
    public static event Action<string> ON_BROADCAST_MESSAGE;

    public static void TriggerBroadcast(string message)
    {
        ON_BROADCAST_MESSAGE?.Invoke(message);
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

    
    // private message
    public static event Action<string, string> ON_PRIVATE_MESSAGE;

    public static void TriggerPrivateMessage(string session, string message)
    {
        ON_PRIVATE_MESSAGE?.Invoke(session, message);
    }

    private void PrivateMessage(string session, string message)
    {
        Sessions.SendTo(message, session);
    }

    public QuartetService()
    {
        ON_BROADCAST_MESSAGE += BroadcastMessage;
        ON_PRIVATE_MESSAGE += PrivateMessage;
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

                string? midiFile = UPDATE_CLIENTS?.Invoke(this);
                NewMidiFile newMidiFile = new NewMidiFile();
                newMidiFile.ReadFile(midiFile);
                newMidiFile.SessionId = Sessions.IDs.Last();
                string json = JsonConvert.SerializeObject(newMidiFile);

                SendMaxPing();

                Send(json);
            }

            // ready new state
            if (typeof(ReadyState).FullName == baseRequest?.RequestType)
            {
                Console.WriteLine(typeof(ReadyState).FullName);

                ReadyState? readyState = JsonConvert.DeserializeObject<ReadyState>(e.Data);
                var client = QuartetServer.Instance.ClientEntries.FirstOrDefault(c => c.SessionID == readyState.SessionId);
                client.IsReady = readyState.Ready;
                UPDATE_CLIENTS?.Invoke(this);
            }

            // client get settings
            if (typeof(ClientNewSettingsEntry).FullName == baseRequest?.RequestType)
            {
                GET_CLIENT_SETTINGS?.Invoke(JsonConvert.DeserializeObject<ClientNewSettingsEntry>(e.Data));
            }

            // send ping connection confirm
            if (typeof(ConnectionConfirm).FullName == baseRequest?.RequestType)
            {
                ConnectionConfirm? connection = JsonConvert.DeserializeObject<ConnectionConfirm>(e.Data);
                TimeSpan ping = connection.NowDateTime - connection.PreviousDateTime;
                connection.Ping += (int)ping.TotalMilliseconds;
                connection.PreviousDateTime = connection.NowDateTime + new TimeSpan(0, 0, 1);
                connection.PingCount++;

                Thread.Sleep(1000);

                string json = JsonConvert.SerializeObject(connection);
                Send(json);
            }

            // client disconnect
            if (typeof(DisconnectClient).FullName == baseRequest?.RequestType)
            {
                DisconnectClient? disconnectClient = JsonConvert.DeserializeObject<DisconnectClient>(e.Data);
                var client = QuartetServer.Instance.ClientEntries.FirstOrDefault(c => c.SessionID == disconnectClient.SessionId);
                QuartetServer.Instance.ClientEntries.Remove(client);
                UPDATE_CLIENTS?.Invoke(this);
            }
        }
        catch (Exception exception)
        {
            //Console.WriteLine(exception);
            MessageBox.Show(exception.Message);
            throw;
        }
    }

    private void SendMaxPing()
    {
        var maxPing = QuartetServer.Instance.ClientEntries.Max(c => c.Ping);
        BroadcastMessage(JsonConvert.SerializeObject(new LobbyMaxPing() { MaxPing = maxPing }));
    }
}