using GenshinQuartetPlayer2.online;
using GenshinQuartetPlayer2.online.requests;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenshinQuartetPlayer2.winforms
{
    public partial class HostForm : Form
    {
        private string _file;
        private MidiReader _midiReader;
        private Database _database;
        private TimeSpan _currentTime = new TimeSpan(0, 0, 0);
        public HostForm(MainMenuForm mainMenuForm)
        {
            InitializeComponent();

            _database = new Database();
            _database.CreateConnection();

            UpdatePlaylist();
            if (playlist.Items.Count > 0)
            {
                _file = _database.GetAllEntries().GetAwaiter().GetResult()[0].FilePath;
                _midiReader = new MidiReader(_file);
                playlist.SelectedIndex = 0;
                instrumentComboBox.SelectedIndex = 0;
                UpdateTrackListBox();
                speedNumeric.Value = 1.0m;
            }

            pingUpDown.Value = QuartetServer.Instance.ClientEntries.ElementAt(0).Ping;

            MyPlayback.ON_GAME_UNFOCUS += (e, d) => SetPlayTrackBarValue(d);

            QuartetService.UPDATE_CLIENTS += (e) => UpdateClients();

            UpdateClients();

            clientListBox.DoubleClick += (sender, e) => OpenClientEditorForm(sender, e);
        }

        private void read_Click(object sender, EventArgs e)
        {
            if (QuartetServer.Instance.ClientEntries.Any(c => c.IsReady))
            {
                QuartetService.TriggerBroadcast(JsonConvert.SerializeObject(new StartPlayBroadcast()));
                var maxPing = QuartetServer.Instance.ClientEntries.Max(c => c.Ping);
                Thread.Sleep(1000 - maxPing - QuartetServer.Instance.ClientEntries.ElementAt(0).Ping);
                _midiReader.Start(_currentTime);
            }
        }

        private void openWindow_Click(object sender, EventArgs e)
        {
            WindowFinder.Find();
        }

        private void playlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (playlist.SelectedIndex != -1 && !File.Exists(_database.GetAllEntries().GetAwaiter().GetResult()[playlist.SelectedIndex].FilePath))
            {
                _database.DeleteEntryAsync((int)_database.GetAllEntries().GetAwaiter().GetResult()[playlist.SelectedIndex].Id);
                UpdatePlaylist();
                FormUpdate();
            }
            else if (playlist.SelectedIndex != -1 && File.Exists(_database.GetAllEntries().GetAwaiter().GetResult()[playlist.SelectedIndex].FilePath))
            {
                _file = _database.GetAllEntries().GetAwaiter().GetResult()[playlist.SelectedIndex].FilePath;
                _midiReader = new MidiReader(_file);

                NewMidiFile newMidiFile = new NewMidiFile();
                newMidiFile.ReadFile(_file);
                QuartetService.TriggerBroadcast(JsonConvert.SerializeObject(newMidiFile));
                UpdateTrackListBox();
                FormUpdate();
            }
        }

        private void FormUpdate()
        {
            UpdateBestTransposition();
            playTrackBar.Value = 0;
            _currentTime = new TimeSpan(0, 0, 0);
            UpdateTimeLabel();
        }

        private void instrumentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Instrument = (Instrument)instrumentComboBox.SelectedIndex;
            UpdateBestTransposition();
            Console.WriteLine(Settings.Transposition);
        }

        private void transposition_ValueChanged(object sender, EventArgs e)
        {
            Settings.Transposition = (int)transposition.Value;
        }

        private void trackListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _midiReader.SwitchTracks(trackListBox.SelectedIndex);
        }

        private void UpdateTrackListBox()
        {
            trackListBox.Items.Clear();
            foreach (var chunk in _midiReader.TrackChunks)
            {
                trackListBox.Items.Add($"Event: {chunk.Events.Count} | {Convert.ToString(chunk.Events.ElementAt(0)).Replace("Sequence/Track Name ", "")}", true);
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MIDI files (*.mid)|*.mid";
            openFileDialog.Multiselect = true;
            openFileDialog.Title = "Select a files";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < openFileDialog.FileNames.Length; i++)
                {
                    string fileName = openFileDialog.SafeFileNames[i];
                    string filePath = openFileDialog.FileNames[i];
                    _database.AddEntry(fileName, filePath);
                }
                UpdatePlaylist();
                if (openFileDialog.FileNames.Length == 1)
                {
                    playlist.SelectedIndex = playlist.Items.Count - 1 - openFileDialog.FileNames.Length;
                }
                else if (playlist.Items.Count != 0) playlist.SelectedIndex = 0;
            }
        }

        void UpdatePlaylist()
        {
            playlist.Items.Clear();
            var entries = _database.GetAllEntries().GetAwaiter().GetResult();
            foreach (var entry in entries)
            {
                playlist.Items.Add(entry.Name);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (playlist.Items.Count > 0 && playlist.SelectedIndex != -1)
            {
                int nextIndex = playlist.SelectedIndex - 1 > 0 && playlist.SelectedIndex < playlist.Items.Count - 2 ? playlist.SelectedIndex - 1 : 0;
                _database.DeleteEntryAsync((int)_database.GetAllEntries().GetAwaiter().GetResult()[playlist.SelectedIndex].Id);
                UpdatePlaylist();
                if (playlist.Items.Count > 0) playlist.SelectedIndex = nextIndex;
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            playlist.SelectedIndex = playlist.SelectedIndex < playlist.Items.Count - 1 ? playlist.SelectedIndex + 1 : 0;
            _midiReader.Start(new MetricTimeSpan(0, 0, 0));
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            playlist.SelectedIndex = playlist.SelectedIndex > 0 ? playlist.SelectedIndex - 1 : playlist.Items.Count - 1;
            _midiReader.Start(new MetricTimeSpan(0, 0, 0));
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

        private void playTrackBar_Scroll(object sender, EventArgs e)
        {
            _currentTime = TimeSpan.FromMilliseconds((_midiReader.TotalTime.TotalMilliseconds * playTrackBar.Value) / 100);
            UpdateTimeLabel();
            QuartetService.TriggerBroadcast(JsonConvert.SerializeObject(new NewTrackTime(_currentTime)));
        }

        public void SetPlayTrackBarValue(TimeSpan timeSpan)
        {
            _currentTime = timeSpan;
            if (playTrackBar.InvokeRequired)
            {
                playTrackBar.Invoke(() =>
                {
                    playTrackBar.Value = (((int)timeSpan.TotalMilliseconds) * 100) / ((int)_midiReader.TotalTime.TotalMilliseconds);
                    UpdateTimeLabel();
                });
            }
            else
            {
                playTrackBar.Value = (((int)timeSpan.TotalMilliseconds) * 100) / ((int)_midiReader.TotalTime.TotalMilliseconds);
                UpdateTimeLabel();
            }
        }

        private void UpdateTimeLabel()
        {
            timeLabel.Text = _currentTime.ToString(@"mm\:ss") + "/" + _midiReader.TotalTime.ToString(@"mm\:ss");
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            _currentTime = new TimeSpan(0, 0, 0);
            playTrackBar.Value = 0;
            UpdateTimeLabel();
            QuartetService.TriggerBroadcast(JsonConvert.SerializeObject(new StopPlay()));
        }

        private void speedNumeric_ValueChanged(object sender, EventArgs e)
        {
            Settings.Speed = speedNumeric.Value;
        }

        private void readyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            QuartetServer.Instance.ClientEntries.ElementAt(0).IsReady = readyCheckBox.Checked;
            UpdateClients();
        }

        private void noPlayCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void timeLabel_Click(object sender, EventArgs e)
        {

        }

        private void transpositionLabel_Click(object sender, EventArgs e)
        {

        }

        private void clientListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void kickButton_Click(object sender, EventArgs e)
        {
            if (clientListBox.SelectedItems != null && clientListBox.SelectedIndex != 0)
            {
                QuartetService.TriggerPrivateMessage(QuartetServer.Instance.ClientEntries.ElementAt(clientListBox.SelectedIndex).SessionID,
                    JsonConvert.SerializeObject(new DisconnectClient()));
                QuartetServer.Instance.ClientEntries.RemoveAt(clientListBox.SelectedIndex);
                UpdateClients();
            }
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            QuartetService.TriggerBroadcast(JsonConvert.SerializeObject(new TestNotePlay()));
            var maxPing = QuartetServer.Instance.ClientEntries.Max(c => c.Ping);
            Thread.Sleep(1000 - maxPing - QuartetServer.Instance.ClientEntries.ElementAt(0).Ping);
        }

        //----------------------------//

        private string UpdateClients()
        {
            if (clientListBox.InvokeRequired)
            {
                clientListBox.Invoke(() =>
                {
                    clientListBox.Items.Clear();
                    foreach (var client in QuartetServer.Instance.ClientEntries)
                    {
                        string check = client.IsReady ? "✔" : "✖";
                        clientListBox.Items.Add($"{client.Username} {check}");
                    }

                });
            }
            else
            {
                clientListBox.Items.Clear();
                foreach (var client in QuartetServer.Instance.ClientEntries)
                {
                    string check = client.IsReady ? "✔" : "✖";
                    clientListBox.Items.Add($"{client.Username} {check}");
                }
            }
            return _file;
        }

        private void OpenClientEditorForm(object sender, EventArgs e)
        {
            if (clientListBox.SelectedItems != null && clientListBox.SelectedIndex != 0)
            {
                ClientEditorForm clientEditorForm = new ClientEditorForm(QuartetServer.Instance.ClientEntries.ElementAt(clientListBox.SelectedIndex));
                clientEditorForm.ShowDialog();
            }

        }

        private void pingUpDown_ValueChanged(object sender, EventArgs e)
        {
            QuartetServer.Instance.ClientEntries.ElementAt(0).Ping = (int)pingUpDown.Value;
            var maxPing = QuartetServer.Instance.ClientEntries.Max(c => c.Ping);
            QuartetService.TriggerBroadcast(JsonConvert.SerializeObject(new LobbyMaxPing() { MaxPing = maxPing }));
        }
    }
}
