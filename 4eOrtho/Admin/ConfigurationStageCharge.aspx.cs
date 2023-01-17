using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho
{
    public partial class ConfigurationStageCharge : System.Web.UI.Page
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(ConfigurationStageCharge));
        SqlDataReader reader;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                if (id > 0)
                {
                  getStageById(id);
                }
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                string val = ddlstagestatus.SelectedValue;

                int id = Convert.ToInt32(Request.QueryString["id"]);
                UpdateStageCharge(id);
                Response.Redirect("PatientStageList.aspx", false);
               // CommonHelper.ShowMessage(MessageType.Success, "Records updated successfully.", divMsg, lblMsg);
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                CommonHelper.ShowMessage(MessageType.Success, "Failed to update records.", divMsg, lblMsg);
            }
        }

        private void UpdateLookupData(string lookupDesc, string lookupValue)
        {
            LookupMaster feesLookup = new LookupMasterEntity().GetLookupMasterByDesc(lookupDesc);
            if (feesLookup != null)
            {
                feesLookup.LookupName = lookupValue;
                new LookupMasterEntity().UpdateLookup(feesLookup);
            }
        }

        void getStageById(int id = 0)
        {
            SqlConnection con = new SqlConnection(SqlConnectionHelper.Connection);


            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Stage where StageId=" + id + "", con);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string amount = reader["StageAmount"].ToString();
                string status = reader["Status"].ToString();

                if (amount.Contains('.'))
                {
                    int index = amount.IndexOf('.');
                    string result = amount.Substring(0, index);
                    txtFees.Text = result;
                    // Console.WriteLine("result: " + result);
                    ddlstagestatus.SelectedValue = status;
                }


            }
            con.Close();

        }

        void UpdateStageCharge(int id = 0)
        {
            string command = string.Empty;
           
            SqlConnection con = new SqlConnection(SqlConnectionHelper.Connection);

            if (id > 0)
                command = "Update Stage  set StageAmount=@StageAmount,Status=@Status where StageId=" + id + "";
           
            con.Open();
            SqlCommand cmd = new SqlCommand(command, con);
            cmd.Parameters.AddWithValue("@StageAmount", txtFees.Text);
            cmd.Parameters.AddWithValue("@Status", Convert.ToInt16(ddlstagestatus.SelectedValue));
            cmd.ExecuteNonQuery();
            con.Close();

        }
    }
}