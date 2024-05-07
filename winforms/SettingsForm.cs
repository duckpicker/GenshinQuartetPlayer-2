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
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ShowIcon = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Text = string.Empty;

            portUpDown.Value = Settings.Port;
            trimDurationTimeUpDown.Value = Settings.TrimDurationTime;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Settings.Port = (int)portUpDown.Value;
            Settings.TrimDurationTime = (int)trimDurationTimeUpDown.Value;
        }
    }
}
