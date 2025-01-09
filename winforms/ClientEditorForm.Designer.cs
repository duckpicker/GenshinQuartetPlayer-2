namespace GenshinQuartetPlayer2.winforms
{
    partial class ClientEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientEditorForm));
            transpositionLabel = new Label();
            trackListBox = new CheckedListBox();
            transposition = new NumericUpDown();
            instrumentComboBox = new ComboBox();
            saveButton = new Button();
            pingUpDown = new NumericUpDown();
            testPingButton = new Button();
            ((System.ComponentModel.ISupportInitialize)transposition).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pingUpDown).BeginInit();
            SuspendLayout();
            // 
            // transpositionLabel
            // 
            transpositionLabel.AutoSize = true;
            transpositionLabel.Location = new Point(121, 55);
            transpositionLabel.Name = "transpositionLabel";
            transpositionLabel.Size = new Size(42, 15);
            transpositionLabel.TabIndex = 27;
            transpositionLabel.Text = "0: 0 (0)";
            // 
            // trackListBox
            // 
            trackListBox.CheckOnClick = true;
            trackListBox.FormattingEnabled = true;
            trackListBox.Location = new Point(12, 108);
            trackListBox.Name = "trackListBox";
            trackListBox.Size = new Size(216, 130);
            trackListBox.TabIndex = 26;
            trackListBox.SelectedIndexChanged += trackListBox_SelectedIndexChanged;
            trackListBox.MouseDown += trackListBox_MouseDown;
            // 
            // transposition
            // 
            transposition.Location = new Point(121, 79);
            transposition.Maximum = new decimal(new int[] { 48, 0, 0, 0 });
            transposition.Minimum = new decimal(new int[] { 48, 0, 0, int.MinValue });
            transposition.Name = "transposition";
            transposition.Size = new Size(107, 23);
            transposition.TabIndex = 25;
            transposition.ValueChanged += transposition_ValueChanged;
            // 
            // instrumentComboBox
            // 
            instrumentComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            instrumentComboBox.Items.AddRange(new object[] { "Lyre/Zither", "Old Lyre", "Drum", "Waldhorn", "Ukulele", "Jam Jam" });
            instrumentComboBox.Location = new Point(11, 79);
            instrumentComboBox.Name = "instrumentComboBox";
            instrumentComboBox.Size = new Size(103, 23);
            instrumentComboBox.TabIndex = 24;
            instrumentComboBox.SelectedIndexChanged += instrumentComboBox_SelectedIndexChanged;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(153, 12);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 23);
            saveButton.TabIndex = 28;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // pingUpDown
            // 
            pingUpDown.Location = new Point(11, 12);
            pingUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            pingUpDown.Name = "pingUpDown";
            pingUpDown.Size = new Size(85, 23);
            pingUpDown.TabIndex = 29;
            pingUpDown.ValueChanged += pingUpDown_ValueChanged;
            // 
            // testPingButton
            // 
            testPingButton.Location = new Point(12, 41);
            testPingButton.Name = "testPingButton";
            testPingButton.Size = new Size(84, 23);
            testPingButton.TabIndex = 30;
            testPingButton.Text = "Test Ping";
            testPingButton.UseVisualStyleBackColor = true;
            testPingButton.Click += testPingButton_Click;
            // 
            // ClientEditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(239, 247);
            Controls.Add(testPingButton);
            Controls.Add(pingUpDown);
            Controls.Add(saveButton);
            Controls.Add(transpositionLabel);
            Controls.Add(trackListBox);
            Controls.Add(transposition);
            Controls.Add(instrumentComboBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ClientEditorForm";
            Text = "Editor";
            ((System.ComponentModel.ISupportInitialize)transposition).EndInit();
            ((System.ComponentModel.ISupportInitialize)pingUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label transpositionLabel;
        private CheckedListBox trackListBox;
        private NumericUpDown transposition;
        private ComboBox instrumentComboBox;
        private Button saveButton;
        private NumericUpDown pingUpDown;
        private Button testPingButton;
    }
}