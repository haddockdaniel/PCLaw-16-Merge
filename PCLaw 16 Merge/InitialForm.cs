using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCLaw_16_Merge
{
    public partial class InitialForm : Form
    {
        public InitialForm()
        {
            InitializeComponent();
        }

        bool typeOfLaw = false;
        bool respAtty = false;

        private void buttonStart_Click(object sender, EventArgs e)
        {
            typeOfLaw = checkBoxTypeofLaw.Checked;
            respAtty = checkBoxRespAtty.Checked;
            if (!typeOfLaw && !respAtty)
            {
                this.Hide();
                var progressform = new ProgressForm(textBoxServer.Text, textBoxPrimaryDB.Text, textBoxSecondaryDB.Text, typeOfLaw, respAtty, null);
                progressform.FormClosed += (s, args) => this.Close();
                progressform.Show();
            }
            else
            {

                    this.Hide();
                    var selectform = new Selections(respAtty, typeOfLaw, textBoxServer.Text, textBoxPrimaryDB.Text, textBoxSecondaryDB.Text);
                    selectform.FormClosed += (s, args) => this.Close();
                    selectform.Show();
             }

        }

        private void checkBoxRespAtty_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxTypeofLaw.Enabled = !checkBoxRespAtty.Checked;
        }

        private void checkBoxTypeofLaw_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxRespAtty.Enabled = !checkBoxTypeofLaw.Checked;
        }
    }
}
