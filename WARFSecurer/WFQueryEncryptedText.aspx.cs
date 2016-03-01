extern alias carf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WARFSecurer
{
    public partial class WFQueryEncryptedText : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
            else
            {
            }
        }

        protected void ddlEncryptionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            generateEncryptedText();
        }

        protected void tbText_TextChanged(object sender, EventArgs e)
        {
            generateEncryptedText();
        }

        private void generateEncryptedText()
        {
            try
            {
                /*
                RF.GlobalClass.Utils.Do.Command cmd = new RF.GlobalClass.Utils.Do.Command();
                tbEncryptedText.Text = cmd.RunProgram("CARFSecurer.exe", arguments: " -n " +
                ddlEncryptionName.SelectedItem.Text + " -m " + tbText.Text);
                 * */
                tbEncryptedText.Text = carf.CARFSecurer.Program.GetEncrytedMessage(encrytionName: ddlEncryptionName.SelectedItem.Text, originMessage: tbText.Text + "");
            }
            catch (Exception ex) { }
        }

        protected void cbShowText_CheckedChanged(object sender, EventArgs e)
        {
            tbText.TextMode = cbShowText.Checked ? TextBoxMode.SingleLine : TextBoxMode.Password;
        }

        protected void cbShowEncryptedText_CheckedChanged(object sender, EventArgs e)
        {
            tbEncryptedText.TextMode = cbShowEncryptedText.Checked ? TextBoxMode.SingleLine : TextBoxMode.Password;
        }

        protected void ddlEncryptionName_Init(object sender, EventArgs e)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("txt");
            dt.Columns.Add("val");
            Enum.GetNames(typeof(carf.RF.GlobalClass.Securer.Names)).Select(delegate(string _enumName)
            {
                System.Data.DataRow dr = dt.NewRow();
                dr.ItemArray = new object[] { _enumName, _enumName };
                dt.Rows.Add(dr);
                return _enumName;
            }).ToArray();
            ddlEncryptionName.DataSource = dt;
            ddlEncryptionName.DataTextField = "txt";
            ddlEncryptionName.DataValueField = "val";
            ddlEncryptionName.DataBind();
        }



    }
}