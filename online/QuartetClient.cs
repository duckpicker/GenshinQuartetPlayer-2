using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebSocketSharp;

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

        public delegate void OnNewMidiFile(string newMidiFilePath);

        public static event OnNewMidiFile ON_NEW_MIDI_FILE;


        private ClientEntry _client;
        private WebSocket _webSocketClient;

        public void CreateClient(string username, int offset, string ipaddress, int port)
        {
            _client = new ClientEntry(-1, username, offset);
            _webSocketClient = new WebSocket($"ws://{ipaddress}:{port}/QuartetService");

            _webSocketClient.OnMessage += ClientOnMessage;
        }

        private static void ClientOnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine(e.Data);

            BaseRequest? jsonBaseRequest = JsonConvert.DeserializeObject<BaseRequest>(e.Data);

            // new midi file
            if (typeof(NewMidiFile).FullName == jsonBaseRequest.RequestType)
            {
                
                NewMidiFile newMidiFile = JsonConvert.DeserializeObject<NewMidiFile>(e.Data);
                File.WriteAllBytes(Path.Combine(Path.GetTempPath(), "temp.mid"), newMidiFile.FileBytes);
                string path = $"{Path.GetTempPath()}\\temp.mid";
                ON_NEW_MIDI_FILE.Invoke(path);
            }
        }

        public void CreateConnection()
        {
            _webSocketClient.Connect();
            string json = JsonConvert.SerializeObject(_client);
            _webSocketClient.Send(json);
        }
    }
}
