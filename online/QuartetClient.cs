using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebSocketSharp;
using WindowsInput;

namespace GenshinQuartetPlayer2.online.requests
{
    class QuartetClient
    {
        private static QuartetClient _instance;

        public static QuartetClient Instance
        {
            get
            {
                if (_instance == null) _instance = new QuartetClient();
                return _instance;
            }
        }

        // INVOKE TO FORM ----------------------------------
        // new midi file invoke
        public delegate void OnNewMidiFile(string newMidiFilePath);

        public static event OnNewMidiFile ON_NEW_MIDI_FILE;

        // get client settings invoke
        public delegate ClientNewSettingsEntry OnClientSettings();

        public static event OnClientSettings ON_CLIENT_SETTINGS;

        // set new settings invoke
        public delegate void ClientSetNewSettings(ClientNewSettingsEntry entry);

        public static event ClientSetNewSettings ON_NEW_SETTINGS;

        // start midi play
        public delegate void OnStartPlay();

        public static event OnStartPlay ON_START_PLAY;

        // stop midi play
        public delegate void OnStopPlay();

        public static event OnStopPlay ON_STOP_PLAY;

        // set track time
        public delegate void OnNewTrackTime(TimeSpan time);

        public static event OnNewTrackTime ON_NEW_TRACK_TIME;

        // set track speed
        public delegate void OnNewTrackSpeed(decimal speed);

        public static event OnNewTrackSpeed ON_NEW_TRACK_SPEED;


        public ClientEntry Client { get; private set; }
        public WebSocket WebSocketClient { get; private set; }

        public void CreateClient(string username, int offset, string ipaddress, int port)
        {
            Client = new ClientEntry(-1, username, offset);
            WebSocketClient = new WebSocket($"ws://{ipaddress}:{port}/QuartetService");

            WebSocketClient.OnMessage += ClientOnMessage;
        }

        private void ClientOnMessage(object sender, MessageEventArgs e)
        {
            BaseRequest? jsonBaseRequest = JsonConvert.DeserializeObject<BaseRequest>(e.Data);

            // new midi file
            if (typeof(NewMidiFile).FullName == jsonBaseRequest.RequestType)
            {
                NewMidiFile newMidiFile = JsonConvert.DeserializeObject<NewMidiFile>(e.Data);
                File.WriteAllBytes(Path.Combine(Path.GetTempPath(), "temp.mid"), newMidiFile.FileBytes);
                string path = $"{Path.GetTempPath()}\\temp.mid";
                ON_NEW_MIDI_FILE.Invoke(path);
                if (Client.SessionID == "") Client.SessionID = newMidiFile.SessionId;
                //WebSocketClient.Send(JsonConvert.SerializeObject(new ConnectionConfirm(DateTime.Now)));
            }

            // connection ping & send new ping
            if (typeof(ConnectionConfirm).FullName == jsonBaseRequest.RequestType)
            {
                ConnectionConfirm? connection = JsonConvert.DeserializeObject<ConnectionConfirm>(e.Data);
                Console.WriteLine(connection.Ping);
                if (connection.PingCount == 4)
                {
                    Console.WriteLine(Client.Ping);
                    Client.Ping += (connection.Ping / 4) / 2;
                    Console.WriteLine(Client.Ping);
                    WebSocketClient.Send(JsonConvert.SerializeObject(new ClientNewPing(Client.SessionID, Client.Ping)));
                }
                else
                {
                    connection.NowDateTime = DateTime.Now;
                    WebSocketClient.Send(JsonConvert.SerializeObject(connection));
                }

            }

            // new max ping
            if (typeof(LobbyMaxPing).FullName == jsonBaseRequest.RequestType)
            {
                LobbyMaxPing? maxPing = JsonConvert.DeserializeObject<LobbyMaxPing>(e.Data);
                Client.MaxPing = maxPing.MaxPing;
            }

            // get client settings
            if (typeof(GetClientSettings).FullName == jsonBaseRequest.RequestType)
            {
                ClientNewSettingsEntry? settingsEntry = ON_CLIENT_SETTINGS?.Invoke();
                string json = JsonConvert.SerializeObject(settingsEntry);
                WebSocketClient.Send(json);
            }

            // set client settings
            if (typeof(ClientNewSettingsEntry).FullName == jsonBaseRequest.RequestType)
            {
                ClientNewSettingsEntry? settingsEntry = JsonConvert.DeserializeObject<ClientNewSettingsEntry>(e.Data);
                ON_NEW_SETTINGS?.Invoke(settingsEntry);
            }

            // disconnect
            if (typeof(DisconnectClient).FullName == jsonBaseRequest.RequestType)
            {
                Disconnect();
            }

            // start play
            if (typeof(StartPlayBroadcast).FullName == jsonBaseRequest.RequestType)
            {
                ON_START_PLAY?.Invoke();
            }

            // test play
            if (typeof(TestNotePlay).FullName == jsonBaseRequest.RequestType)
            {
                Thread.Sleep(1000 - Client.MaxPing - Client.Ping);
                InputSimulator inputSimulator = new InputSimulator();
                inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_A);
            }

            // stop play
            if (typeof(StopPlay).FullName == jsonBaseRequest.RequestType)
            {
                ON_STOP_PLAY?.Invoke();
            }

            // new track time
            if (typeof(NewTrackTime).FullName == jsonBaseRequest.RequestType)
            {
                NewTrackTime? newTrackTime = JsonConvert.DeserializeObject<NewTrackTime>(e.Data);
                ON_NEW_TRACK_TIME?.Invoke(newTrackTime.Time);
            }

            // new track speed
            if (typeof(NewTrackSpeed).FullName == jsonBaseRequest.RequestType)
            {
                NewTrackSpeed? newTrackSpeed = JsonConvert.DeserializeObject<NewTrackSpeed>(e.Data);
                ON_NEW_TRACK_SPEED?.Invoke(newTrackSpeed.Speed);
            }
        }

        public void CreateConnection()
        {
            WebSocketClient.Connect();
            string json = JsonConvert.SerializeObject(Client);
            WebSocketClient.Send(json);
        }

        public void Disconnect()
        {
            string json = JsonConvert.SerializeObject(new DisconnectClient() { SessionId = Client.SessionID });
            WebSocketClient.Send(json);
            WebSocketClient.Close();
        }
    }
}
