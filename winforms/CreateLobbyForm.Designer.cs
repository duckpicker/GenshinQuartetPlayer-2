namespace GenshinQuartetPlayer2.winforms
{
    partial class CreateLobbyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateLobbyForm));
            nameTextBox = new TextBox();
            offsetTextBox = new TextBox();
            portTextBox = new TextBox();
            createButton = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(12, 43);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(100, 23);
            nameTextBox.TabIndex = 0;
            nameTextBox.TextChanged += nameTextBox_TextChanged;
            // 
            // offsetTextBox
            // 
            offsetTextBox.Location = new Point(118, 43);
            offsetTextBox.Name = "offsetTextBox";
            offsetTextBox.Size = new Size(100, 23);
            offsetTextBox.TabIndex = 1;
            offsetTextBox.TextChanged += offsetTextBox_TextChanged;
            // 
            // portTextBox
            // 
            portTextBox.Location = new Point(12, 87);
            portTextBox.Name = "portTextBox";
            portTextBox.Size = new Size(100, 23);
            portTextBox.TabIndex = 2;
            portTextBox.TextChanged += portTextBox_TextChanged;
            // 
            // createButton
            // 
            createButton.Location = new Point(143, 114);
            createButton.Name = "createButton";
            createButton.Size = new Size(75, 23);
            createButton.TabIndex = 3;
            createButton.Text = "Create";
            createButton.UseVisualStyleBackColor = true;
            createButton.Click += createButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 25);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 5;
            label1.Text = "Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(118, 25);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 6;
            label2.Text = "Offset";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 69);
            label3.Name = "label3";
            label3.Size = new Size(29, 15);
            label3.TabIndex = 7;
            label3.Text = "Port";
            // 
            // CreateLobbyForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(231, 153);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(createButton);
            Controls.Add(portTextBox);
            Controls.Add(offsetTextBox);
            Controls.Add(nameTextBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "CreateLobbyForm";
            Text = "Create lobby";
            Load += CreateLobbyForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox nameTextBox;
        private TextBox offsetTextBox;
        private TextBox portTextBox;
        private Button createButton;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}