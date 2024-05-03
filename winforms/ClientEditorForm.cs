using GenshinQuartetPlayer2.online;
using GenshinQuartetPlayer2.online.requests;
using Melanchall.DryWetMidi.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

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

        }
    }
}
