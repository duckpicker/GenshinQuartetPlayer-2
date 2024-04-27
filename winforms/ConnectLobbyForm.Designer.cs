namespace GenshinQuartetPlayer2.winforms
{
    partial class ConnectLobbyForm
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
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            connectButton = new Button();
            portTextBox = new TextBox();
            offsetTextBox = new TextBox();
            nameTextBox = new TextBox();
            ipAddressTextBox = new TextBox();
            label4 = new Label();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(118, 53);
            label3.Name = "label3";
            label3.Size = new Size(29, 15);
            label3.TabIndex = 15;
            label3.Text = "Port";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(118, 9);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 14;
            label2.Text = "Offset";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 13;
            label1.Text = "Name";
            // 
            // connectButton
            // 
            connectButton.Location = new Point(143, 117);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(75, 23);
            connectButton.TabIndex = 11;
            connectButton.Text = "Connect";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += connectButton_Click;
            // 
            // portTextBox
            // 
            portTextBox.Location = new Point(118, 71);
            portTextBox.Name = "portTextBox";
            portTextBox.Size = new Size(100, 23);
            portTextBox.TabIndex = 10;
            portTextBox.TextChanged += portTextBox_TextChanged;
            // 
            // offsetTextBox
            // 
            offsetTextBox.Location = new Point(118, 27);
            offsetTextBox.Name = "offsetTextBox";
            offsetTextBox.Size = new Size(100, 23);
            offsetTextBox.TabIndex = 9;
            offsetTextBox.TextChanged += offsetTextBox_TextChanged;
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(12, 27);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(100, 23);
            nameTextBox.TabIndex = 8;
            nameTextBox.TextChanged += nameTextBox_TextChanged;
            // 
            // ipAddressTextBox
            // 
            ipAddressTextBox.Location = new Point(12, 71);
            ipAddressTextBox.Name = "ipAddressTextBox";
            ipAddressTextBox.Size = new Size(100, 23);
            ipAddressTextBox.TabIndex = 16;
            ipAddressTextBox.TextChanged += ipAddressTextBox_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(13, 53);
            label4.Name = "label4";
            label4.Size = new Size(62, 15);
            label4.TabIndex = 17;
            label4.Text = "IP Address";
            // 
            // ConnectLobbyForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(234, 152);
            Controls.Add(label4);
            Controls.Add(ipAddressTextBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(connectButton);
            Controls.Add(portTextBox);
            Controls.Add(offsetTextBox);
            Controls.Add(nameTextBox);
            Name = "ConnectLobbyForm";
            Text = "ConnectLobbyForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private Label label2;
        private Label label1;
        private Button connectButton;
        private TextBox portTextBox;
        private TextBox offsetTextBox;
        private TextBox nameTextBox;
        private TextBox ipAddressTextBox;
        private Label label4;
    }
}