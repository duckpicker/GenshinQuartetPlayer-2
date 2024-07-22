namespace GenshinQuartetPlayer2.winforms
{
    partial class SettingsForm
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
            portUpDown = new NumericUpDown();
            trimDurationTimeUpDown = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            saveButton = new Button();
            backgroundMidiPlayback = new CheckBox();
            backgroundAllMidiPlayback = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)portUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trimDurationTimeUpDown).BeginInit();
            SuspendLayout();
            // 
            // portUpDown
            // 
            portUpDown.Location = new Point(12, 30);
            portUpDown.Maximum = new decimal(new int[] { 65536, 0, 0, 0 });
            portUpDown.Name = "portUpDown";
            portUpDown.Size = new Size(108, 23);
            portUpDown.TabIndex = 0;
            // 
            // trimDurationTimeUpDown
            // 
            trimDurationTimeUpDown.Location = new Point(12, 74);
            trimDurationTimeUpDown.Name = "trimDurationTimeUpDown";
            trimDurationTimeUpDown.Size = new Size(108, 23);
            trimDurationTimeUpDown.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(29, 15);
            label1.TabIndex = 2;
            label1.Text = "Port";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 56);
            label2.Name = "label2";
            label2.Size = new Size(105, 15);
            label2.TabIndex = 3;
            label2.Text = "Trim duration time";
            // 
            // saveButton
            // 
            saveButton.Location = new Point(126, 74);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(69, 23);
            saveButton.TabIndex = 4;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // backgroundMidiPlayback
            // 
            backgroundMidiPlayback.AutoSize = true;
            backgroundMidiPlayback.Location = new Point(126, 30);
            backgroundMidiPlayback.Name = "backgroundMidiPlayback";
            backgroundMidiPlayback.Size = new Size(100, 19);
            backgroundMidiPlayback.TabIndex = 5;
            backgroundMidiPlayback.Text = "Midi playback";
            backgroundMidiPlayback.UseVisualStyleBackColor = true;
            backgroundMidiPlayback.CheckedChanged += backgroundMidiPlayback_CheckedChanged;
            // 
            // backgroundAllMidiPlayback
            // 
            backgroundAllMidiPlayback.AutoSize = true;
            backgroundAllMidiPlayback.Location = new Point(126, 55);
            backgroundAllMidiPlayback.Name = "backgroundAllMidiPlayback";
            backgroundAllMidiPlayback.Size = new Size(106, 19);
            backgroundAllMidiPlayback.TabIndex = 6;
            backgroundAllMidiPlayback.Text = "Muted channel";
            backgroundAllMidiPlayback.UseVisualStyleBackColor = true;
            backgroundAllMidiPlayback.CheckedChanged += backgroundAllMidiPlayback_CheckedChanged;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(234, 117);
            Controls.Add(backgroundAllMidiPlayback);
            Controls.Add(backgroundMidiPlayback);
            Controls.Add(saveButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(trimDurationTimeUpDown);
            Controls.Add(portUpDown);
            Name = "SettingsForm";
            Text = "SettingsForm";
            ((System.ComponentModel.ISupportInitialize)portUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)trimDurationTimeUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown portUpDown;
        private NumericUpDown trimDurationTimeUpDown;
        private Label label1;
        private Label label2;
        private Button saveButton;
        private CheckBox backgroundMidiPlayback;
        private CheckBox backgroundAllMidiPlayback;
    }
}