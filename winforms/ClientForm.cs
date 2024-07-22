using GenshinQuartetPlayer2.online.requests;
using Newtonsoft.Json;

namespace GenshinQuartetPlayer2.winforms
{
    public partial class ClientForm : Form
    {
        private string _file;
        private MidiReader _midiReader;
        private TimeSpan _currentTime = new TimeSpan(0, 0, 0);
        public ClientForm(MainMenuForm mainMenuForm)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            instrumentComboBox.SelectedIndex = 0;
            Settings.Speed = 1.0m;
            QuartetClient.ON_NEW_MIDI_FILE += (filePath) => SetNewMidiFile(filePath);
            QuartetClient.ON_CLIENT_SETTINGS += () => CreateNewSettingsEntry();
            QuartetClient.ON_NEW_SETTINGS += (settings) => SetNewSettings(settings);
            QuartetClient.ON_START_PLAY += () => StartPlay();
            QuartetClient.ON_STOP_PLAY += () => _midiReader.Stop();
            QuartetClient.ON_NEW_TRACK_TIME += (time) => _currentTime = time;
            QuartetClient.ON_NEW_TRACK_SPEED += (speed) => Settings.Speed = speed;
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {

        }

        private void openWindow_Click(object sender, EventArgs e)
        {
            WindowFinder.Find();
        }

        private void instrumentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Instrument = (Instrument)instrumentComboBox.SelectedIndex;
            if (_midiReader != null)
            {
                UpdateBestTransposition();
                Console.WriteLine(Settings.Transposition);
            }
        }

        private void transposition_ValueChanged(object sender, EventArgs e)
        {
            Settings.Transposition = (int)transposition.Value;
        }

        private void trackListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _midiReader.SwitchTracks(trackListBox.SelectedIndex);
        }

        //--------------------------//

        private void readyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            QuartetClient.Instance.WebSocketClient.Send(JsonConvert.SerializeObject(new ReadyState()
            {
                SessionId = QuartetClient.Instance.Client.SessionID,
                Ready = readyCheckBox.Checked
            }));
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            QuartetClient.Instance.Disconnect();
            Application.Exit();
        }

        private int UpdateBestTransposition()
        {
            KeyboardEmulator keyboardEmulator = new KeyboardEmulator();
            var transpositionList = keyboardEmulator.GetBestTranspostion(_midiReader);
            TranspostitionEntry closest = new TranspostitionEntry(0, 0, 0);
            int maxNotes = transpositionList.ElementAt(0).MaxNotes;
            int minimum = int.MinValue;
            for (int i = 0; i < transpositionList.Count(); i++)
            {
                if (transpositionList.ElementAt(i).TranspositionResult > minimum)
                {
                    minimum = transpositionList.ElementAt(i).TranspositionResult;
                    closest = transpositionList.ElementAt(i);
                }
            }
            transpositionLabel.Text = $"{closest.Value}/{closest.Value + 12}: {closest.TranspositionResult} ({closest.MaxNotes})";
            int bestTransposition = closest.Value > -5 ? closest.Value : closest.Value + 12;
            transposition.Value = bestTransposition;
            return closest.Value;
        }

        private void SetNewMidiFile(string filePath)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    _midiReader.myPlayBack.DisposeDevice();
                    _file = filePath;
                    _midiReader = new MidiReader(_file);
                    UpdateTrackListBox();
                    UpdateBestTransposition();
                }));
            }
        }

        private void UpdateTrackListBox()
        {
            trackListBox.Items.Clear();
            for (int i = 0; i < _midiReader.TrackChunks.Count(); i++)
            {
                bool checker = !_midiReader.MutedTrackChunks.Contains(i);
                var chunk = _midiReader.TrackChunks.ElementAt(i);
                trackListBox.Items.Add($"Event: {chunk.Events.Count} | {Convert.ToString(chunk.Events.ElementAt(0)).Replace("Sequence/Track Name ", "")}", checker);
            }
        }

        // get set settings from host
        private ClientNewSettingsEntry CreateNewSettingsEntry()
        {
            List<string> chunks = new List<string>();
            foreach (var track in trackListBox.Items)
            {
                chunks.Add(track.ToString());
            }
            return new ClientNewSettingsEntry((int)transposition.Value, Settings.Instrument,
                _midiReader.MutedTrackChunks, QuartetClient.Instance.Client.Ping, chunks);
        }

        private void SetNewSettings(ClientNewSettingsEntry settings)
        {
            if (this.InvokeRequired && settings != null)
            {
                this.Invoke(new Action(() =>
                {
                    instrumentComboBox.SelectedIndex = (int)settings.Instrument;
                    UpdateBestTransposition();
                    _midiReader.MutedTrackChunks = settings.MutedTrackChunks;
                    UpdateTrackListBox();
                    QuartetClient.Instance.Client.Ping = settings.NewPing;
                    transposition.Value = settings.Transposition;
                    QuartetClient.Instance.WebSocketClient.Send(JsonConvert.SerializeObject(new ClientNewPing(QuartetClient.Instance.Client.SessionID, QuartetClient.Instance.Client.Ping)));
                }));
            }
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            QuartetClient.Instance.Disconnect();
            Application.Exit();
        }

        private void StartPlay()
        {
            WindowFinder.Find();
            Thread.Sleep(1000 - QuartetClient.Instance.Client.MaxPing - QuartetClient.Instance.Client.Ping);
            _midiReader.Start(_currentTime);
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            _midiReader.Stop();
        }

        private void LegacyMode()
        {
            foreach (Control control in this.Controls)
            {
                control.Enabled = false;
            }
            stopButton.Enabled = true;
            readyCheckBox.Enabled = true;
            disconnectButton.Enabled = true;
        }
    }
}
