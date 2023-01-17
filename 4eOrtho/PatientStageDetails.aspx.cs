using _4eOrtho.Admin;
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

namespace _4eOrtho
{
    public partial class PatientStageDetails : System.Web.UI.Page
    {
        #region Declaration

        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        int totalRecordCount;
       // private ILog logger = log4net.LogManager.GetLogger(typeof(PatientStageDetails));

        private ILog logger = log4net.LogManager.GetLogger(typeof(PatientStageDetails));
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    this.BindGrid();
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
        private void BindGrid()
        {
            int PatientId = Convert.ToInt16(Session["PatientId"]);
            string constr = SqlConnectionHelper.Connection;

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from Stage where PatientId="+PatientId+" and IsDelete!=1"))
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
                    case "CUSTOMDELETE":
                        {
                            PatientCaseDetailEntity patientCaseDetailEntity = new PatientCaseDetailEntity();
                            PatientCaseDetail patientCase = patientCaseDetailEntity.GetPatientCaseById(Convert.ToInt64(e.CommandArgument));

                            if (patientCase != null)
                            {
                                patientCase.IsDelete = true;
                                patientCaseDetailEntity.Save(patientCase);
                                lvCustomers.DataBind();

                                List<PatientGalleryMaster> lstPatientGalleryMaster = new PatientGalleryMasterEntity().GetPatientGalleryByCaseId(patientCase.CaseId);
                                if (lstPatientGalleryMaster != null && lstPatientGalleryMaster.Count > 0)
                                {
                                    foreach (PatientGalleryMaster galleryMaster in lstPatientGalleryMaster)
                                    {
                                        galleryMaster.IsDelete = true;
                                        new PatientGalleryMasterEntity().Save(galleryMaster);
                                    }
                                }
                            }
                            break;
                        }
                    case "CUSTOMEDIT":
                        {
                            HiddenField hdnIsPayment = (HiddenField)(((ListViewItem)((Control)sender).NamingContainer).FindControl("hdnIsPayment"));

                            Session["lstFileList"] = null;
                            Session["NewCaseId"] = e.CommandArgument.ToString();
                            SessionHelper.ReworkORRetainer = null;
                            SessionHelper.IsPayment = Convert.ToBoolean(hdnIsPayment.Value);
                            Response.Redirect("~/AddNewCase.aspx");
                            break;
                        }
                    case "EDIT":
                        {
                           
                            TrackCase trackCase = new TrackCaseEntity().GetTrackCaseByCaseId(Convert.ToInt64(e.CommandArgument));
                            int id = Convert.ToInt16(e.CommandArgument);

                            Response.Redirect(string.Format("AddUpdateStage.aspx?id={0}", id));
                            //if (trackCase != null)
                            //{
                            //    string trackNo = trackCase.TrackNo;
                            //    trackCase = new TrackCaseEntity().Create();
                            //    CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                            //    trackCase.CaseId = Convert.ToInt64(e.CommandArgument);
                            //    trackCase.TrackNo = trackNo;
                            //    trackCase.UpdatedByEmail = currentSession.EmailId;
                            //    trackCase.Status = ((int)TrackingStatus.Received).ToString();
                            //    trackCase.IsActive = true;
                            //    long lTrackId = new TrackCaseEntity().Save(trackCase);

                            //    string emailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("MailOnUpdateTrackDetails")).ToString();

                            //    TrackEmailDetails trackDetails = new TrackCaseEntity().GetTrackEmailDetails(lTrackId);
                            //    if (trackDetails != null)
                            //    {
                            //        string updatedby = trackDetails.FirstName + " " + trackDetails.LastName;
                            //        string trackStatus = TrackingStatus.Received.ToString();
                            //        TrackCaseEntity.SendMailOnUpdateStatus(emailTemplatePath, trackDetails.DoctorFirstName, trackDetails.DoctorLastName, trackDetails.CaseNo, trackStatus, updatedby, trackDetails.UpdatedDate.ToString("MM/dd/yyyy"), trackDetails.DoctorEmailId, trackDetails.Description);
                            //    }
                            //    lvCustomers.DataBind();
                            //  //  lvCompletedCaseList.DataBind();
                            //}
                            break;
                        }
                    
                  
                    case "PAY":
                        {
                            int id = Convert.ToInt16(e.CommandArgument);

                            Response.Redirect(string.Format("PatientStagePayment.aspx?id={0}", id));
                          //  Response.Redirect("~/PatientStagePayment.aspx?Id=");
                            break;
                        }
                    case "VIEWRECEIPT":
                        {
                            int id = Convert.ToInt16(e.CommandArgument);

                            Response.Redirect(string.Format("StagePaymentReceipt.aspx?id={0}", id));
                            //  Response.Redirect("~/PatientStagePayment.aspx?Id=");
                            break;
                        }
                    case "STAGE":
                        {
                            // int id = Convert.ToInt16(GridView1.DataKeys[e.NewEditIndex].Values["StageId"].ToString());
                            Response.Redirect(string.Format("PatientStageDetails.aspx"));
                            
                            break;
                        }
                    case "REMOVE":
                        {
                            SqlConnection con = new SqlConnection(SqlConnectionHelper.Connection);

                            int id = Convert.ToInt16(e.CommandArgument);
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Update Stage set IsDelete=1 where StageId =@id", con);
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

    }
}