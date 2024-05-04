using GenshinQuartetPlayer2.online.requests;
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
    public partial class ConnectLobbyForm : Form
    {
        private MainMenuForm _menuForm;
        public ConnectLobbyForm(MainMenuForm mainMenuForm)
        {
            InitializeComponent();
            _menuForm = mainMenuForm;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            nameTextBox.Text = Environment.UserName;
            //createButton.Enabled = false;
            portTextBox.Text = Settings.port.ToString();
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateConnectButtonStatus();
        }

        private void offsetTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateConnectButtonStatus();
        }

        private void ipAddressTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateConnectButtonStatus();
        }

        private void portTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateConnectButtonStatus();
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            int offset = 0;
            int port = 0;
            int.TryParse(offsetTextBox.Text, out offset);
            int.TryParse(portTextBox.Text, out port);
            QuartetClient.Instance.CreateClient(nameTextBox.Text, offset, ipAddressTextBox.Text, port);
            QuartetClient.Instance.CreateConnection();

            ClientForm clientForm = new ClientForm(_menuForm);
            _menuForm.Hide();
            this.Hide();
            clientForm.Show();
        }

        private void UpdateConnectButtonStatus()
        {
            if (offsetTextBox.Text == "" || nameTextBox.Text == "" || portTextBox.Text == "" || ipAddressTextBox.Text == "" ||
                !int.TryParse(offsetTextBox.Text, out _) || !int.TryParse(portTextBox.Text, out _))
            {
                connectButton.Enabled = false;
            }
            else connectButton.Enabled = true;
        }
    }
}
