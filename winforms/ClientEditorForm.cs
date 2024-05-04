using GenshinQuartetPlayer2.online;
using GenshinQuartetPlayer2.online.requests;
using Newtonsoft.Json;

namespace GenshinQuartetPlayer2.winforms
{
    public partial class ClientEditorForm : Form
    {
        private ClientEntry _client;
        private ClientNewSettingsEntry _settings;
        public ClientEditorForm(ClientEntry client)
        {
            InitializeComponent();

            foreach (Control control in this.Controls)
            {
                control.Enabled = false;
            }

            _client = client;

            QuartetService.GET_CLIENT_SETTINGS += (settings) => GetClientSettings(settings);

            QuartetService.TriggerPrivateMessage(_client.SessionID, JsonConvert.SerializeObject(new GetClientSettings()));
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string newJsonSettings = JsonConvert.SerializeObject(new ClientNewSettingsEntry((int)transposition.Value,
                (Instrument)instrumentComboBox.SelectedIndex, _settings.MutedTrackChunks, (int)pingUpDown.Value, new List<string>()));
            QuartetService.TriggerPrivateMessage(_client.SessionID, newJsonSettings);
            this.Close();
        }

        private void GetClientSettings(ClientNewSettingsEntry settings)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    foreach (Control control in this.Controls)
                    {
                        control.Enabled = true;
                    }
                    _settings = settings;
                    instrumentComboBox.SelectedIndex = (int)settings.Instrument;
                    transposition.Value = settings.Transposition;
                    pingUpDown.Value = _settings.NewPing;
                    trackListBox.Items.Clear();
                    for (int i = 0; i < _settings.TrackChunksString.Count(); i++)
                    {
                        var chunk = _settings.TrackChunksString.ElementAt(i);
                        bool checker = !_settings.MutedTrackChunks.Contains(i);
                        trackListBox.Items.Add(chunk, checker);
                    }
                }));
            }
        }

        // --------------------------------------------------------------------------

        private void instrumentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void transposition_ValueChanged(object sender, EventArgs e)
        {

        }

        private void trackListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_settings.MutedTrackChunks.Contains(trackListBox.SelectedIndex)) _settings.MutedTrackChunks.Remove(trackListBox.SelectedIndex);
            else _settings.MutedTrackChunks.Add(trackListBox.SelectedIndex);
        }

        private void pingUpDown_ValueChanged(object sender, EventArgs e)
        {
            _settings.NewPing = (int)pingUpDown.Value;
        }

        private void testPingButton_Click(object sender, EventArgs e)
        {
            string newJsonSettings = JsonConvert.SerializeObject(new ClientNewSettingsEntry((int)transposition.Value,
                (Instrument)instrumentComboBox.SelectedIndex, _settings.MutedTrackChunks, (int)pingUpDown.Value, new List<string>()));
            QuartetService.TriggerPrivateMessage(_client.SessionID, newJsonSettings);
            QuartetService.TriggerBroadcast(JsonConvert.SerializeObject(new TestNotePlay()));
        }
    }
}
