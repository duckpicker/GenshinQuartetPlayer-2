using GenshinQuartetPlayer2.online.requests;
using Melanchall.DryWetMidi.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenshinQuartetPlayer2.winforms
{
    public partial class ClientForm : Form
    {
        private string _file;
        private MidiReader _midiReader;
        public ClientForm(MainMenuForm mainMenuForm)
        {
            InitializeComponent();
            instrumentComboBox.SelectedIndex = 0;
            QuartetClient.ON_NEW_MIDI_FILE += (filePath) => SetNewMidiFile(filePath);
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

        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {

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
            foreach (var chunk in _midiReader.TrackChunks)
            {
                trackListBox.Items.Add($"Event: {chunk.Events.Count} | {Convert.ToString(chunk.Events.ElementAt(0)).Replace("Sequence/Track Name ", "")}", true);
            }
        }
    }
}
