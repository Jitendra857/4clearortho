using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho.Admin
{
    public partial class PatientStageList : System.Web.UI.Page
    {
        #region Declaration

        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        int totalRecordCount;
        // private ILog logger = log4net.LogManager.GetLogger(typeof(PatientStageDetails));

        private ILog logger = log4net.LogManager.GetLogger(typeof(PatientStageList));
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    this.BindGrid();
                  
                   // ViewState["SortBy"] = "FirstName";
                    ViewState["AscDesc"] = "ASC";
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured .", ex);
            }

        }
        protected void lvCustomers_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (lvCustomers.Items.Count > 0)
                {
                    SetSortImage();
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        private void BindGrid()
        {
            int PatientId = Convert.ToInt16(Session["PatientId"]);
            string constr = SqlConnectionHelper.Connection;

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from Stage where PatientId=" + PatientId + " and IsDelete!=1"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            ViewState["Paging"] = dt;
                            lvCustomers.DataSource = dt;
                            lvCustomers.DataBind();

                        }
                    }
                }
            }
        }

        


        protected void odsCaseCharge_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();
                e.InputParameters["searchValue"] = string.Empty; // txtSearchVal.Text.Trim();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured .", ex);
            }
        }
        protected void OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            (lvCustomers.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            this.BindGrid();
        }
        protected void Custom_Command(object sender, CommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "CUSTOMSORT":
                        {
                            if (ViewState["AscDesc"] == null || ViewState["AscDesc"].ToString() == "")
                                ViewState["AscDesc"] = "DESC";
                            else
                                ViewState["AscDesc"] = (ViewState["AscDesc"].ToString() == "ASC") ? "DESC" : "ASC";

                            ViewState["SortBy"] = e.CommandArgument;
                            lvCustomers.DataBind();
                            break;
                        }
                   
                    case "EDIT":
                        {

                            TrackCase trackCase = new TrackCaseEntity().GetTrackCaseByCaseId(Convert.ToInt64(e.CommandArgument));
                            int id = Convert.ToInt16(e.CommandArgument);

                            Response.Redirect(string.Format("ConfigurationStageCharge.aspx?id={0}", id));
                           
                            break;
                        }

                  

                    case "STAGE":
                        {
                            // int id = Convert.ToInt16(GridView1.DataKeys[e.NewEditIndex].Values["StageId"].ToString());
                            Response.Redirect(string.Format("PatientStageDetails.aspx"));

                            break;
                        }
                    case "VIEWRECEIPT":
                        {
                            int id = Convert.ToInt16(e.CommandArgument);

                            Response.Redirect(string.Format("StagePaymentReceipt.aspx?id={0}", id));
                            //  Response.Redirect("~/PatientStagePayment.aspx?Id=");
                            break;
                        }
                    case "CLOSE":
                        {
                            SqlConnection con = new SqlConnection(SqlConnectionHelper.Connection);

                            int id = Convert.ToInt16(e.CommandArgument);
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Update Stage set Status=1 where StageId =@id", con);
                            cmd.Parameters.AddWithValue("id", id);
                            int i = cmd.ExecuteNonQuery();
                            con.Close();
                            this.BindGrid();
                            break;
                        }
                    case "OPEN":
                        {
                            SqlConnection con = new SqlConnection(SqlConnectionHelper.Connection);

                            int id = Convert.ToInt16(e.CommandArgument);
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Update Stage set Status=0 where StageId =@id", con);
                            cmd.Parameters.AddWithValue("id", id);
                            int i = cmd.ExecuteNonQuery();
                            con.Close();
                            this.BindGrid();
                            break;
                        }
                }
            }
            catch (System.Threading.ThreadAbortException) { }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        private void SetSortImage()
        {
            try
            {
                (lvCustomers.FindControl("lnkName") as LinkButton).Attributes.Add("class", "");
                (lvCustomers.FindControl("lnkamunt") as LinkButton).Attributes.Add("class", "");
                (lvCustomers.FindControl("lnkstatus") as LinkButton).Attributes.Add("class", "");
               

                LinkButton lnkSortedColumn = null;
                if (ViewState["SortBy"] != null)
                {
                    switch (ViewState["SortBy"].ToString().ToLower())
                    {
                        case "name":
                            lnkSortedColumn = lvCustomers.FindControl("lnkName") as LinkButton;
                            break;
                        case "price":
                            lnkSortedColumn = lvCustomers.FindControl("lnkAmount") as LinkButton;
                            break;
                        case "status":
                            lnkSortedColumn = lvCustomers.FindControl("lnkCouponCode") as LinkButton;
                            break;
                        //case "isflat":
                        //    lnkSortedColumn = lvCaseCharges.FindControl("lnkPaymentAmount") as LinkButton;
                        //    break;
                       
                    }
                }
                if (lnkSortedColumn != null)
                {
                    if (ViewState["AscDesc"].ToString().ToLower() == "asc")
                    {
                        lnkSortedColumn.Attributes.Add("class", "ascending");
                    }
                    else
                    {
                        lnkSortedColumn.Attributes.Add("class", "descending");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void ShowPopup(object sender, EventArgs e)
        {
            string title = "Greetings";
            string body = "Welcome to ASPSnippets.com";
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "', '" + body + "');", true);
        }
    }
}