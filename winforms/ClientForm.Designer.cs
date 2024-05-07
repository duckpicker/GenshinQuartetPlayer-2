namespace GenshinQuartetPlayer2.winforms
{
    partial class ClientForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientForm));
            transpositionLabel = new Label();
            trackListBox = new CheckedListBox();
            transposition = new NumericUpDown();
            instrumentComboBox = new ComboBox();
            openWindow = new Button();
            disconnectButton = new Button();
            readyCheckBox = new CheckBox();
            stopButton = new Button();
            ((System.ComponentModel.ISupportInitialize)transposition).BeginInit();
            SuspendLayout();
            // 
            // transpositionLabel
            // 
            transpositionLabel.AutoSize = true;
            transpositionLabel.Location = new Point(121, 70);
            transpositionLabel.Name = "transpositionLabel";
            transpositionLabel.Size = new Size(42, 15);
            transpositionLabel.TabIndex = 22;
            transpositionLabel.Text = "0: 0 (0)";
            // 
            // trackListBox
            // 
            trackListBox.CheckOnClick = true;
            trackListBox.FormattingEnabled = true;
            trackListBox.Location = new Point(12, 123);
            trackListBox.Name = "trackListBox";
            trackListBox.Size = new Size(216, 130);
            trackListBox.TabIndex = 21;
            trackListBox.SelectedIndexChanged += trackListBox_SelectedIndexChanged;
            // 
            // transposition
            // 
            transposition.Location = new Point(121, 94);
            transposition.Maximum = new decimal(new int[] { 48, 0, 0, 0 });
            transposition.Minimum = new decimal(new int[] { 48, 0, 0, int.MinValue });
            transposition.Name = "transposition";
            transposition.Size = new Size(107, 23);
            transposition.TabIndex = 20;
            transposition.ValueChanged += transposition_ValueChanged;
            // 
            // instrumentComboBox
            // 
            instrumentComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            instrumentComboBox.Items.AddRange(new object[] { "Lyre/Zither", "Old Lyre", "Drum", "Waldhorn" });
            instrumentComboBox.Location = new Point(11, 94);
            instrumentComboBox.Name = "instrumentComboBox";
            instrumentComboBox.Size = new Size(103, 23);
            instrumentComboBox.TabIndex = 19;
            instrumentComboBox.SelectedIndexChanged += instrumentComboBox_SelectedIndexChanged;
            // 
            // openWindow
            // 
            openWindow.Location = new Point(11, 66);
            openWindow.Name = "openWindow";
            openWindow.Size = new Size(103, 23);
            openWindow.TabIndex = 18;
            openWindow.Text = "Open Window";
            openWindow.UseVisualStyleBackColor = true;
            openWindow.Click += openWindow_Click;
            // 
            // disconnectButton
            // 
            disconnectButton.Location = new Point(122, 12);
            disconnectButton.Name = "disconnectButton";
            disconnectButton.Size = new Size(107, 23);
            disconnectButton.TabIndex = 26;
            disconnectButton.Text = "Disconnect";
            disconnectButton.UseVisualStyleBackColor = true;
            disconnectButton.Click += disconnectButton_Click;
            // 
            // readyCheckBox
            // 
            readyCheckBox.AutoSize = true;
            readyCheckBox.Location = new Point(11, 41);
            readyCheckBox.Name = "readyCheckBox";
            readyCheckBox.Size = new Size(58, 19);
            readyCheckBox.TabIndex = 27;
            readyCheckBox.Text = "Ready";
            readyCheckBox.UseVisualStyleBackColor = true;
            readyCheckBox.CheckedChanged += readyCheckBox_CheckedChanged;
            // 
            // stopButton
            // 
            stopButton.Location = new Point(11, 12);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(35, 23);
            stopButton.TabIndex = 32;
            stopButton.Text = "⬛";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += stopButton_Click;
            // 
            // ClientForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(241, 267);
            Controls.Add(stopButton);
            Controls.Add(readyCheckBox);
            Controls.Add(disconnectButton);
            Controls.Add(transpositionLabel);
            Controls.Add(trackListBox);
            Controls.Add(transposition);
            Controls.Add(instrumentComboBox);
            Controls.Add(openWindow);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ClientForm";
            Text = "QuartetPlayer 2";
            FormClosing += ClientForm_FormClosing;
            Load += ClientForm_Load;
            ((System.ComponentModel.ISupportInitialize)transposition).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label transpositionLabel;
        private CheckedListBox trackListBox;
        private NumericUpDown transposition;
        private ComboBox instrumentComboBox;
        private Button openWindow;
        private Button disconnectButton;
        private CheckBox readyCheckBox;
        private Button stopButton;
    }
}