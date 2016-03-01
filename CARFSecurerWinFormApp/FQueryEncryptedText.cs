extern alias carf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CARFSecurerWinFormApp
{
    public partial class FQueryEncryptedText : Form
    {
        public FQueryEncryptedText()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbText_TextChanged(object sender, EventArgs e)
        {
            generateEncryptedText();
        }

        private void generateEncryptedText()
        {
            try
            {
                RF.GlobalClass.Utils.Do.Command cmd = new RF.GlobalClass.Utils.Do.Command();
                tbEncryptedText.Text = cmd.RunProgram("CARFSecurer.exe", arguments: " -n " +
                comboBoxEncryptName.Text + " -m " + tbText.Text);
            }
            catch (Exception ex) { }
        }

        private void cbShowText_CheckedChanged(object sender, EventArgs e)
        {
            tbText.UseSystemPasswordChar = !cbShowText.Checked;
        }

        private void cbShowEncryptedText_CheckedChanged(object sender, EventArgs e)
        {
            tbEncryptedText.UseSystemPasswordChar = !cbShowEncryptedText.Checked;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbEncryptedText.Text = "";
            tbText.Text = "";
        }

        private void lEncryptName_Click(object sender, EventArgs e)
        {
            DefaultSelectedItemText = "16_3_a";
            if (comboBoxEncryptName.Items.Contains(DefaultSelectedItemText))
            {
                //comboBoxEncryptName.SelectedText = DefaultSelectedItemText;
                comboBoxEncryptName.SelectedIndex = comboBoxEncryptName.Items.IndexOf(DefaultSelectedItemText);
            }
            else { }
        }

        public string DefaultSelectedItemText { get; set; }

        private void comboBoxEncryptName_SelectedIndexChanged(object sender, EventArgs e)
        {
            generateEncryptedText();
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelMain_Layout(object sender, LayoutEventArgs e)
        {
            comboBoxEncryptName.DataSource = Enum.GetNames(typeof(carf.RF.GlobalClass.Securer.Names)).Select(delegate(string _enumName)
            {
                return _enumName;
            }).ToArray();
        }
    }
}
