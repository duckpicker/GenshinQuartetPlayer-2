namespace GenshinQuartetPlayer2
{
    partial class MainMenuForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenuForm));
            read = new Button();
            openWindow = new Button();
            playlist = new ListBox();
            instrumentComboBox = new ComboBox();
            transposition = new NumericUpDown();
            trackListBox = new CheckedListBox();
            nextButton = new Button();
            previousButton = new Button();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openFileToolStripMenuItem = new ToolStripMenuItem();
            onlineToolStripMenuItem = new ToolStripMenuItem();
            createLobbyToolStripMenuItem = new ToolStripMenuItem();
            connectToolStripMenuItem = new ToolStripMenuItem();
            deleteButton = new Button();
            transpositionLabel = new Label();
            inputDeviceComboBox = new ComboBox();
            reloadInputDeviceButton = new Button();
            playTrackBar = new TrackBar();
            timeLabel = new Label();
            stopButton = new Button();
            speedNumeric = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)transposition).BeginInit();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)playTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)speedNumeric).BeginInit();
            SuspendLayout();
            // 
            // read
            // 
            read.Location = new Point(119, 145);
            read.Name = "read";
            read.Size = new Size(35, 23);
            read.TabIndex = 0;
            read.Text = "▶";
            read.UseVisualStyleBackColor = true;
            read.Click += read_Click;
            // 
            // openWindow
            // 
            openWindow.Location = new Point(12, 225);
            openWindow.Name = "openWindow";
            openWindow.Size = new Size(103, 23);
            openWindow.TabIndex = 1;
            openWindow.Text = "Open Window";
            openWindow.UseVisualStyleBackColor = true;
            openWindow.Click += openWindow_Click;
            // 
            // playlist
            // 
            playlist.FormattingEnabled = true;
            playlist.ItemHeight = 15;
            playlist.Location = new Point(239, 62);
            playlist.Name = "playlist";
            playlist.Size = new Size(231, 379);
            playlist.TabIndex = 2;
            playlist.SelectedIndexChanged += playlist_SelectedIndexChanged;
            // 
            // instrumentComboBox
            // 
            instrumentComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            instrumentComboBox.Items.AddRange(new object[] { "Lyre/Zither", "Old Lyre", "Drum", "Waldhorn" });
            instrumentComboBox.Location = new Point(12, 282);
            instrumentComboBox.Name = "instrumentComboBox";
            instrumentComboBox.Size = new Size(103, 23);
            instrumentComboBox.TabIndex = 3;
            instrumentComboBox.SelectedIndexChanged += instrumentComboBox_SelectedIndexChanged;
            // 
            // transposition
            // 
            transposition.Location = new Point(122, 254);
            transposition.Maximum = new decimal(new int[] { 48, 0, 0, 0 });
            transposition.Minimum = new decimal(new int[] { 48, 0, 0, int.MinValue });
            transposition.Name = "transposition";
            transposition.Size = new Size(107, 23);
            transposition.TabIndex = 5;
            transposition.ValueChanged += transposition_ValueChanged;
            // 
            // trackListBox
            // 
            trackListBox.CheckOnClick = true;
            trackListBox.FormattingEnabled = true;
            trackListBox.Location = new Point(13, 311);
            trackListBox.Name = "trackListBox";
            trackListBox.Size = new Size(216, 130);
            trackListBox.TabIndex = 6;
            trackListBox.SelectedIndexChanged += trackListBox_SelectedIndexChanged;
            // 
            // nextButton
            // 
            nextButton.Location = new Point(160, 145);
            nextButton.Name = "nextButton";
            nextButton.Size = new Size(35, 23);
            nextButton.TabIndex = 7;
            nextButton.Text = ">>";
            nextButton.UseVisualStyleBackColor = true;
            nextButton.Click += nextButton_Click;
            // 
            // previousButton
            // 
            previousButton.Location = new Point(37, 145);
            previousButton.Name = "previousButton";
            previousButton.Size = new Size(35, 23);
            previousButton.TabIndex = 8;
            previousButton.Text = "<<";
            previousButton.UseVisualStyleBackColor = true;
            previousButton.Click += previousButton_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, onlineToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(482, 24);
            menuStrip1.TabIndex = 9;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openFileToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openFileToolStripMenuItem
            // 
            openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            openFileToolStripMenuItem.Size = new Size(127, 22);
            openFileToolStripMenuItem.Text = "Open files";
            openFileToolStripMenuItem.Click += openFileToolStripMenuItem_Click;
            // 
            // onlineToolStripMenuItem
            // 
            onlineToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { createLobbyToolStripMenuItem, connectToolStripMenuItem });
            onlineToolStripMenuItem.Name = "onlineToolStripMenuItem";
            onlineToolStripMenuItem.Size = new Size(54, 20);
            onlineToolStripMenuItem.Text = "Online";
            // 
            // createLobbyToolStripMenuItem
            // 
            createLobbyToolStripMenuItem.Name = "createLobbyToolStripMenuItem";
            createLobbyToolStripMenuItem.Size = new Size(141, 22);
            createLobbyToolStripMenuItem.Text = "Create lobby";
            createLobbyToolStripMenuItem.Click += createLobbyToolStripMenuItem_Click;
            // 
            // connectToolStripMenuItem
            // 
            connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            connectToolStripMenuItem.Size = new Size(141, 22);
            connectToolStripMenuItem.Text = "Connect";
            connectToolStripMenuItem.Click += connectToolStripMenuItem_Click;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(239, 33);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(75, 23);
            deleteButton.TabIndex = 10;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteButton_Click;
            // 
            // transpositionLabel
            // 
            transpositionLabel.AutoSize = true;
            transpositionLabel.Location = new Point(122, 229);
            transpositionLabel.Name = "transpositionLabel";
            transpositionLabel.Size = new Size(42, 15);
            transpositionLabel.TabIndex = 11;
            transpositionLabel.Text = "0: 0 (0)";
            // 
            // inputDeviceComboBox
            // 
            inputDeviceComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            inputDeviceComboBox.FormattingEnabled = true;
            inputDeviceComboBox.Location = new Point(12, 254);
            inputDeviceComboBox.Name = "inputDeviceComboBox";
            inputDeviceComboBox.Size = new Size(77, 23);
            inputDeviceComboBox.TabIndex = 12;
            inputDeviceComboBox.SelectedIndexChanged += inputDeviceComboBox_SelectedIndexChanged;
            // 
            // reloadInputDeviceButton
            // 
            reloadInputDeviceButton.Location = new Point(95, 254);
            reloadInputDeviceButton.Name = "reloadInputDeviceButton";
            reloadInputDeviceButton.Size = new Size(20, 23);
            reloadInputDeviceButton.TabIndex = 13;
            reloadInputDeviceButton.Text = "↻";
            reloadInputDeviceButton.UseVisualStyleBackColor = true;
            reloadInputDeviceButton.Click += reloadInputDeviceButton_Click;
            // 
            // playTrackBar
            // 
            playTrackBar.Location = new Point(12, 174);
            playTrackBar.Maximum = 100;
            playTrackBar.Name = "playTrackBar";
            playTrackBar.Size = new Size(217, 45);
            playTrackBar.TabIndex = 14;
            playTrackBar.Scroll += playTrackBar_Scroll;
            // 
            // timeLabel
            // 
            timeLabel.AutoSize = true;
            timeLabel.Location = new Point(95, 204);
            timeLabel.Name = "timeLabel";
            timeLabel.Size = new Size(54, 15);
            timeLabel.TabIndex = 15;
            timeLabel.Text = "0:00/0:00";
            // 
            // stopButton
            // 
            stopButton.Location = new Point(78, 145);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(35, 23);
            stopButton.TabIndex = 16;
            stopButton.Text = "⬛";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += stopButton_Click;
            // 
            // speedNumeric
            // 
            speedNumeric.DecimalPlaces = 1;
            speedNumeric.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            speedNumeric.Location = new Point(122, 282);
            speedNumeric.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            speedNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            speedNumeric.Name = "speedNumeric";
            speedNumeric.Size = new Size(107, 23);
            speedNumeric.TabIndex = 17;
            speedNumeric.Value = new decimal(new int[] { 1, 0, 0, 0 });
            speedNumeric.ValueChanged += speedNumeric_ValueChanged;
            // 
            // MainMenuForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(482, 450);
            Controls.Add(speedNumeric);
            Controls.Add(stopButton);
            Controls.Add(timeLabel);
            Controls.Add(playTrackBar);
            Controls.Add(reloadInputDeviceButton);
            Controls.Add(inputDeviceComboBox);
            Controls.Add(transpositionLabel);
            Controls.Add(deleteButton);
            Controls.Add(previousButton);
            Controls.Add(nextButton);
            Controls.Add(trackListBox);
            Controls.Add(transposition);
            Controls.Add(instrumentComboBox);
            Controls.Add(playlist);
            Controls.Add(openWindow);
            Controls.Add(read);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "MainMenuForm";
            Text = "GenshinQuartetPlayer 2";
            FormClosing += MainMenuForm_FormClosing;
            Load += MainMenuForm_Load;
            ((System.ComponentModel.ISupportInitialize)transposition).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)playTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)speedNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button read;
        private Button openWindow;
        private ListBox playlist;
        private ComboBox instrumentComboBox;
        private NumericUpDown transposition;
        private CheckedListBox trackListBox;
        private Button nextButton;
        private Button previousButton;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openFileToolStripMenuItem;
        private Button deleteButton;
        private Label transpositionLabel;
        private ComboBox inputDeviceComboBox;
        private Button reloadInputDeviceButton;
        private TrackBar playTrackBar;
        private Label timeLabel;
        private Button stopButton;
        private NumericUpDown speedNumeric;
        private ToolStripMenuItem onlineToolStripMenuItem;
        private ToolStripMenuItem createLobbyToolStripMenuItem;
        private ToolStripMenuItem connectToolStripMenuItem;
    }
}
