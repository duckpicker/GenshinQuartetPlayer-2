using Melanchall.DryWetMidi.Interaction;


namespace GenshinQuartetPlayer2
{
    public partial class MainMenuForm : Form
    {
        private string _file;
        private MidiReader _midiReader;
        private Database _database;
        private TimeSpan _currentTime = new TimeSpan(0, 0, 0);




        public MainMenuForm()
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
            MyPlayback.ON_GAME_UNFOCUS += (e, d) => SetPlayTrackBarValue(d);
        }

        private void read_Click(object sender, EventArgs e)
        {
            _midiReader.Start(_currentTime);
        }

        private void openWindow_Click(object sender, EventArgs e)
        {
            WindowFinder.Find();
        }

        private void playlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (playlist.SelectedIndex != -1 && !File.Exists(_database.GetAllEntries().GetAwaiter().GetResult()[playlist.SelectedIndex].FilePath))
            {
                _database.DeleteEntryAsync((int)_database.GetAllEntries().GetAwaiter().GetResult()[playlist.SelectedIndex].ID);
                UpdatePlaylist();
                FormUpdate();
            }
            else if (playlist.SelectedIndex != -1 && File.Exists(_database.GetAllEntries().GetAwaiter().GetResult()[playlist.SelectedIndex].FilePath))
            {
                _file = _database.GetAllEntries().GetAwaiter().GetResult()[playlist.SelectedIndex].FilePath;
                _midiReader = new MidiReader(_file);
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
            Settings._instrument = (Instrument)instrumentComboBox.SelectedIndex;
            UpdateBestTransposition();
            Console.WriteLine(Settings._transposition);
        }

        private void transposition_ValueChanged(object sender, EventArgs e)
        {
            Settings._transposition = (int)transposition.Value;
        }

        private void trackListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _midiReader.SwitchTraks(trackListBox.SelectedIndex);
        }

        private void UpdateTrackListBox()
        {
            trackListBox.Items.Clear();
            foreach (var chunk in _midiReader._trackChunks)
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
                _database.DeleteEntryAsync((int)_database.GetAllEntries().GetAwaiter().GetResult()[playlist.SelectedIndex].ID);
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
            int maxNotes = transpositionList.ElementAt(0)._maxNotes;
            int minimum = int.MinValue;
            for (int i = 0; i < transpositionList.Count(); i++)
            {
                if (transpositionList.ElementAt(i)._transpositionResult > minimum)
                {
                    minimum = transpositionList.ElementAt(i)._transpositionResult;
                    closest = transpositionList.ElementAt(i);
                }
            }
            transpositionLabel.Text = $"{closest._i}/{closest._i + 12}: {closest._transpositionResult} ({closest._maxNotes})";
            int bestTransposition = closest._i > -5 ? closest._i : closest._i + 12;
            transposition.Value = bestTransposition;
            return closest._i;
        }

        private void inputDeviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MidiInput.StopInputDevice();
            MidiInput.StartInputDevice(inputDeviceComboBox.SelectedIndex);
        }

        private void reloadInputDeviceButton_Click(object sender, EventArgs e)
        {
            inputDeviceComboBox.Items.Clear();
            try
            {
                MidiInput.StopInputDevice();
                List<string> devices = MidiInput.GetInputDeviceNames();
                foreach (string device in devices) inputDeviceComboBox.Items.Add(device);
            }
            catch (Exception ex) { }
        }

        private void playTrackBar_Scroll(object sender, EventArgs e)
        {
            _currentTime = TimeSpan.FromMilliseconds((_midiReader._totalTime.TotalMilliseconds * playTrackBar.Value) / 100);
            UpdateTimeLabel();
        }

        public void SetPlayTrackBarValue(TimeSpan timeSpan)
        {
            _currentTime = timeSpan;
            if (playTrackBar.InvokeRequired)
            {
                playTrackBar.Invoke(() =>
                {
                    playTrackBar.Value = (((int)timeSpan.TotalMilliseconds) * 100) / ((int)_midiReader._totalTime.TotalMilliseconds);
                    UpdateTimeLabel();
                });
            }
            else
            {
                playTrackBar.Value = (((int)timeSpan.TotalMilliseconds) * 100) / ((int)_midiReader._totalTime.TotalMilliseconds);
                UpdateTimeLabel();
            }
        }

        private void UpdateTimeLabel()
        {
            //timeLabel.Text = $"{_currentTime.Minutes}:{_currentTime.Seconds}/{_midiReader._totalTime.Minutes}:{_midiReader._totalTime.Seconds}";
            timeLabel.Text = _currentTime.ToString(@"mm\:ss") + "/" + _midiReader._totalTime.ToString(@"mm\:ss");
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            _currentTime = new TimeSpan(0, 0, 0);
            playTrackBar.Value = 0;
            UpdateTimeLabel();
        }

        private void speedNumeric_ValueChanged(object sender, EventArgs e)
        {
            Settings._speed = speedNumeric.Value;
        }
    }
}
