namespace GenshinQuartetPlayer2.winforms
{
    partial class HostForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HostForm));
            speedNumeric = new NumericUpDown();
            stopButton = new Button();
            timeLabel = new Label();
            playTrackBar = new TrackBar();
            transpositionLabel = new Label();
            trackListBox = new CheckedListBox();
            transposition = new NumericUpDown();
            instrumentComboBox = new ComboBox();
            playlist = new ListBox();
            openWindow = new Button();
            read = new Button();
            readyCheckBox = new CheckBox();
            kickButton = new Button();
            testButton = new Button();
            noPlayCheckBox = new CheckBox();
            clientListBox = new ListBox();
            pingUpDown = new NumericUpDown();
            addFileButton = new Button();
            ((System.ComponentModel.ISupportInitialize)speedNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)playTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)transposition).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pingUpDown).BeginInit();
            SuspendLayout();
            // 
            // speedNumeric
            // 
            speedNumeric.DecimalPlaces = 1;
            speedNumeric.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            speedNumeric.Location = new Point(116, 312);
            speedNumeric.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            speedNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            speedNumeric.Name = "speedNumeric";
            speedNumeric.Size = new Size(107, 23);
            speedNumeric.TabIndex = 32;
            speedNumeric.Value = new decimal(new int[] { 1, 0, 0, 0 });
            speedNumeric.ValueChanged += speedNumeric_ValueChanged;
            // 
            // stopButton
            // 
            stopButton.Location = new Point(72, 175);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(35, 23);
            stopButton.TabIndex = 31;
            stopButton.Text = "⬛";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += stopButton_Click;
            // 
            // timeLabel
            // 
            timeLabel.AutoSize = true;
            timeLabel.Location = new Point(89, 234);
            timeLabel.Name = "timeLabel";
            timeLabel.Size = new Size(54, 15);
            timeLabel.TabIndex = 30;
            timeLabel.Text = "0:00/0:00";
            // 
            // playTrackBar
            // 
            playTrackBar.Location = new Point(6, 204);
            playTrackBar.Maximum = 100;
            playTrackBar.Name = "playTrackBar";
            playTrackBar.Size = new Size(217, 45);
            playTrackBar.TabIndex = 29;
            playTrackBar.Scroll += playTrackBar_Scroll;
            // 
            // transpositionLabel
            // 
            transpositionLabel.AutoSize = true;
            transpositionLabel.Location = new Point(116, 260);
            transpositionLabel.Name = "transpositionLabel";
            transpositionLabel.Size = new Size(42, 15);
            transpositionLabel.TabIndex = 26;
            transpositionLabel.Text = "0: 0 (0)";
            // 
            // trackListBox
            // 
            trackListBox.CheckOnClick = true;
            trackListBox.FormattingEnabled = true;
            trackListBox.Location = new Point(7, 341);
            trackListBox.Name = "trackListBox";
            trackListBox.Size = new Size(216, 130);
            trackListBox.TabIndex = 23;
            trackListBox.SelectedIndexChanged += trackListBox_SelectedIndexChanged;
            // 
            // transposition
            // 
            transposition.Location = new Point(116, 284);
            transposition.Maximum = new decimal(new int[] { 48, 0, 0, 0 });
            transposition.Minimum = new decimal(new int[] { 48, 0, 0, int.MinValue });
            transposition.Name = "transposition";
            transposition.Size = new Size(107, 23);
            transposition.TabIndex = 22;
            transposition.ValueChanged += transposition_ValueChanged;
            // 
            // instrumentComboBox
            // 
            instrumentComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            instrumentComboBox.Items.AddRange(new object[] { "Lyre/Zither", "Old Lyre", "Drum", "Waldhorn", "Ukulele", "Jam Jam" });
            instrumentComboBox.Location = new Point(6, 312);
            instrumentComboBox.Name = "instrumentComboBox";
            instrumentComboBox.Size = new Size(103, 23);
            instrumentComboBox.TabIndex = 21;
            instrumentComboBox.SelectedIndexChanged += instrumentComboBox_SelectedIndexChanged;
            // 
            // playlist
            // 
            playlist.FormattingEnabled = true;
            playlist.ItemHeight = 15;
            playlist.Location = new Point(233, 17);
            playlist.Name = "playlist";
            playlist.Size = new Size(231, 454);
            playlist.TabIndex = 20;
            playlist.SelectedIndexChanged += playlist_SelectedIndexChanged;
            // 
            // openWindow
            // 
            openWindow.Location = new Point(6, 284);
            openWindow.Name = "openWindow";
            openWindow.Size = new Size(103, 23);
            openWindow.TabIndex = 19;
            openWindow.Text = "Open Window";
            openWindow.UseVisualStyleBackColor = true;
            openWindow.Click += openWindow_Click;
            // 
            // read
            // 
            read.Location = new Point(113, 175);
            read.Name = "read";
            read.Size = new Size(35, 23);
            read.TabIndex = 18;
            read.Text = "▶";
            read.UseVisualStyleBackColor = true;
            read.Click += read_Click;
            // 
            // readyCheckBox
            // 
            readyCheckBox.AutoSize = true;
            readyCheckBox.Location = new Point(7, 235);
            readyCheckBox.Name = "readyCheckBox";
            readyCheckBox.Size = new Size(58, 19);
            readyCheckBox.TabIndex = 34;
            readyCheckBox.Text = "Ready";
            readyCheckBox.UseVisualStyleBackColor = true;
            readyCheckBox.CheckedChanged += readyCheckBox_CheckedChanged;
            // 
            // kickButton
            // 
            kickButton.Location = new Point(7, 47);
            kickButton.Name = "kickButton";
            kickButton.Size = new Size(75, 23);
            kickButton.TabIndex = 35;
            kickButton.Text = "Kick";
            kickButton.UseVisualStyleBackColor = true;
            kickButton.Click += kickButton_Click;
            // 
            // testButton
            // 
            testButton.Location = new Point(148, 47);
            testButton.Name = "testButton";
            testButton.Size = new Size(75, 23);
            testButton.TabIndex = 36;
            testButton.Text = "Test";
            testButton.UseVisualStyleBackColor = true;
            testButton.Click += testButton_Click;
            // 
            // noPlayCheckBox
            // 
            noPlayCheckBox.AutoSize = true;
            noPlayCheckBox.Location = new Point(7, 260);
            noPlayCheckBox.Name = "noPlayCheckBox";
            noPlayCheckBox.Size = new Size(67, 19);
            noPlayCheckBox.TabIndex = 37;
            noPlayCheckBox.Text = "No Play";
            noPlayCheckBox.UseVisualStyleBackColor = true;
            // 
            // clientListBox
            // 
            clientListBox.FormattingEnabled = true;
            clientListBox.ItemHeight = 15;
            clientListBox.Location = new Point(7, 76);
            clientListBox.Name = "clientListBox";
            clientListBox.Size = new Size(216, 94);
            clientListBox.TabIndex = 38;
            // 
            // pingUpDown
            // 
            pingUpDown.Location = new Point(148, 18);
            pingUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            pingUpDown.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            pingUpDown.Name = "pingUpDown";
            pingUpDown.Size = new Size(75, 23);
            pingUpDown.TabIndex = 39;
            pingUpDown.ValueChanged += pingUpDown_ValueChanged;
            // 
            // addFileButton
            // 
            addFileButton.Location = new Point(7, 18);
            addFileButton.Name = "addFileButton";
            addFileButton.Size = new Size(75, 23);
            addFileButton.TabIndex = 40;
            addFileButton.Text = "Add file";
            addFileButton.UseVisualStyleBackColor = true;
            addFileButton.Click += addFileButton_Click;
            // 
            // HostForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(476, 483);
            Controls.Add(addFileButton);
            Controls.Add(pingUpDown);
            Controls.Add(clientListBox);
            Controls.Add(noPlayCheckBox);
            Controls.Add(testButton);
            Controls.Add(kickButton);
            Controls.Add(readyCheckBox);
            Controls.Add(speedNumeric);
            Controls.Add(stopButton);
            Controls.Add(timeLabel);
            Controls.Add(playTrackBar);
            Controls.Add(transpositionLabel);
            Controls.Add(trackListBox);
            Controls.Add(transposition);
            Controls.Add(instrumentComboBox);
            Controls.Add(playlist);
            Controls.Add(openWindow);
            Controls.Add(read);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "HostForm";
            Text = "HostForm";
            FormClosing += HostForm_FormClosing;
            Load += HostForm_Load;
            ((System.ComponentModel.ISupportInitialize)speedNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)playTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)transposition).EndInit();
            ((System.ComponentModel.ISupportInitialize)pingUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown speedNumeric;
        private Button stopButton;
        private Label timeLabel;
        private TrackBar playTrackBar;
        private Label transpositionLabel;
        private CheckedListBox trackListBox;
        private NumericUpDown transposition;
        private ComboBox instrumentComboBox;
        private ListBox playlist;
        private Button openWindow;
        private Button read;
        private CheckBox readyCheckBox;
        private Button kickButton;
        private Button testButton;
        private CheckBox noPlayCheckBox;
        private ListBox clientListBox;
        private NumericUpDown pingUpDown;
        private Button addFileButton;
    }
}