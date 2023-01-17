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

namespace _4eOrtho.Admin
{
    public partial class AddUpdateStageMaster : System.Web.UI.Page
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(AddEditCaseCharges));
        SqlDataReader reader;
        #endregion
        int stageid = 0;

        public AddUpdateStageMaster(int Id=0)
        {
            this.stageid = Id;
        }
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
                int id = Convert.ToInt32(Request.QueryString["id"]);
                SaveStage(id);
                if (!chkDiscount.Checked)
                {
                    var name = txtCaseCharge.Text;
                    var price = TextBox1.Text;
                   // rqvCouponCode.Enabled = false;
                    //custCouponCode.Enabled = false;
                   //// rqvDiscountValue.Enabled = false;
                   // rqvExpiryDate.Enabled = false;
                }

                if (Page.IsValid)
                {
                 //   SaveCaseCharge();
                    Response.Redirect("StageList.aspx", false);
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

        void SaveStage(int id=0)
        {
            string command = string.Empty;
            SqlConnection con = new SqlConnection(@"data source=DESKTOP-NGNOP1Q\EXPRESS1;initial catalog=4ClearOrtho;Integrated Security=true;multipleactiveresultsets=True;user id=sa;password=joshi@123;");
          
            if(id>0)
             command = "Update StageMaster  set StageName=@StageName,StagePrice=@StagePrice,IsActive=@IsActive where StageId=" + id + "";
            else
                command = "insert into StageMaster(StageName,StagePrice,IsActive,IsDeleted,CreatedBy,CreatedOn)values(@StageName,@StagePrice,@IsActive,@IsDeleted,@CreatedBy,@CreatedOn)";

            con.Open();
            SqlCommand cmd = new SqlCommand(command, con);
            cmd.Parameters.AddWithValue("@StageName", TextBox1.Text);
            cmd.Parameters.AddWithValue("@StagePrice", txtCaseCharge.Text);
            cmd.Parameters.AddWithValue("@IsActive", true);
            if (id == 0)
            {
                cmd.Parameters.AddWithValue("@IsDeleted", false);
                cmd.Parameters.AddWithValue("@CreatedBy", 0);
                cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
            }
            cmd.ExecuteNonQuery();
            con.Close();

        }

        void getStageById(int id=0)
        {
            SqlConnection con = new SqlConnection(@"data source=DESKTOP-NGNOP1Q\EXPRESS1;initial catalog=4ClearOrtho;Integrated Security=true;multipleactiveresultsets=True;user id=sa;password=joshi@123;");

           
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from StageMaster where StageId="+id+"", con);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txtCaseCharge.Text = reader["StagePrice"].ToString();
                TextBox1.Text = reader["StageName"].ToString();
            }
            con.Close();
          
        }


        #endregion
    }
}