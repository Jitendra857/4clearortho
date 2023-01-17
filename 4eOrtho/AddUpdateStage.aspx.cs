using _4eOrtho.Helper;
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
    public partial class AddUpdateStage : System.Web.UI.Page
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(AddUpdateStage));
        SqlDataReader reader;
        #endregion
        int stageid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                if (id > 0)
                    getStageById(id);
            }
        }

        #region Helper

        private void BindDoctorList()
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //This Stage already exists. Please choose another
                int id = Convert.ToInt32(Request.QueryString["id"]);
                // StageValidation();
                if (StageValidation(id))
                {
                    //   SaveCaseCharge();
                    Response.Redirect("PatientStageDetails.aspx", false);
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        void SaveStage(int id = 0)
        {
            string command = string.Empty;
            bool IsActive = chkIsActive.Checked ? true :false;
            SqlConnection con = new SqlConnection(SqlConnectionHelper.Connection);

            if (id > 0)
                command = "Update Stage  set StageName=@StageName,StageAmount=@StageAmount,IsActive=@IsActive,StageDescription=@StageDescription where StageId=" + id + "";
            else
                command = "insert into Stage(PatientEmail,PatientId,StageName,StageNo,StageAmount,StageDescription,isPaymentByPatient,CreatedBy,CreatedDate,LastUpdatedBy,LastUpdatedDate,Status,IsActive,IsDelete,IsDispetched,IsReceived)values(@PatientEmail,@PatientId,@StageName,@StageNo,@StageAmount,@StageDescription,@isPaymentByPatient,@CreatedBy,@CreatedDate,@LastUpdatedBy,@LastUpdatedDate,@Status,@IsActive,@IsDelete,@IsDispetched,@IsReceived)";

            //command = "insert into Stage(PatientEmail,PatientId,StageName,StageNo,StageAmount,StageDescription,isPaymentByPatient,CreatedBy,CreatedDate,LastUpdatedBy,LastUpdatedDate,Status,IsActive,IsDelete)values(@PatientEmail,@PatientId,@StageName,@StageNo,@StageAmount,@StageDescription,@isPaymentByPatient,@CreatedBy,@CreatedDate,@LastUpdatedBy,@LastUpdatedDate,@Status,@IsActive,@IsDelete)";

            con.Open();
            SqlCommand cmd = new SqlCommand(command, con);
            cmd.Parameters.AddWithValue("@StageName", txtstagename.Text);
            cmd.Parameters.AddWithValue("@StageAmount",Convert.ToDecimal(txtstageprice.Text));
            cmd.Parameters.AddWithValue("@IsActive", IsActive);

            cmd.Parameters.AddWithValue("@StageDescription",txtNotes.Text);
          //  cmd.Parameters.AddWithValue("@StagePrice", txtstageprice.Text);
          

            if (id == 0)
            {
                int PatientId =Convert.ToInt16(Session["PatientId"]);
                string Email = "";
                cmd.Parameters.AddWithValue("@IsDelete", false);
                cmd.Parameters.AddWithValue("@CreatedBy", 0);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@PatientEmail", Email);
                cmd.Parameters.AddWithValue("@PatientId", PatientId);
                cmd.Parameters.AddWithValue("@StageNo", 1234);
                cmd.Parameters.AddWithValue("@isPaymentByPatient",false);
                cmd.Parameters.AddWithValue("@LastUpdatedBy", 1);
                cmd.Parameters.AddWithValue("@LastUpdatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@Status", 0);
                cmd.Parameters.AddWithValue("@IsDispetched", false);
                cmd.Parameters.AddWithValue("@IsReceived", false);
              
            }
            cmd.ExecuteNonQuery();
            con.Close();

        }
        public bool StageValidation(int stageid)
        {
            int PatientId = Convert.ToInt16(Session["PatientId"]);

            SqlConnection con = new SqlConnection(SqlConnectionHelper.Connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Stage where StageName='" + txtstagename.Text + "' and StageId!="+ stageid + " and IsDelete!=1 and PatientId="+ PatientId + "", con);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string stagename = reader["StageName"].ToString();
                if(txtstagename.Text== stagename)
                {
                    lblstagename.Text = "Already Exits";
                    return false;
                }
                else
                {
                    con.Close();
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    SaveStage(id);
                    return true;
                }
               
            }
            else{
                con.Close();
                int id = Convert.ToInt32(Request.QueryString["id"]);
                SaveStage(id);
                return true;
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

                if (amount.Contains('.'))
                {
                    int index = amount.IndexOf('.');
                    string result = amount.Substring(0, index);
                    txtstageprice.Text = result;
                    // Console.WriteLine("result: " + result);
                }

               
                txtstagename.Text = reader["StageName"].ToString();
                txtNotes.Text = reader["StageDescription"].ToString();
                chkIsActive.Text = reader["IsActive"].ToString();
                Session["pid"] = reader["PatientId"].ToString();
            }
            con.Close();

        }


        #endregion
    }
}