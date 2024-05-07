using GenshinQuartetPlayer2.online;

namespace GenshinQuartetPlayer2.winforms
{
    public partial class CreateLobbyForm : Form
    {
        MainMenuForm mainMenuForm;
        public CreateLobbyForm(MainMenuForm mainMenuForm)
        {
            InitializeComponent();
            this.mainMenuForm = mainMenuForm;
        }

        private void CreateLobbyForm_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            nameTextBox.Text = Environment.UserName;
            portTextBox.Text = Settings.Port.ToString();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            int offset = 0;
            int.TryParse(offsetTextBox.Text, out offset);

            QuartetServer.Instance.ClientEntries.Add(new ClientEntry(0, nameTextBox.Text, offset));
            QuartetServer.Instance.Start();

            HostForm hostForm = new HostForm(mainMenuForm);
            hostForm.Text = $"Lobby {nameTextBox.Text}";

            mainMenuForm.Hide();
            this.Hide();
            hostForm.Show();
        }

        private void UpdateCreateButtonStatus()
        {
            if (offsetTextBox.Text == "" || nameTextBox.Text == "" || portTextBox.Text == "" || !int.TryParse(offsetTextBox.Text, out _) || !int.TryParse(portTextBox.Text, out _))
            {
                createButton.Enabled = false;
            }
            else createButton.Enabled = true;
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateCreateButtonStatus();
        }

        private void offsetTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateCreateButtonStatus();
        }

        private void portTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateCreateButtonStatus();
        }
    }
}
