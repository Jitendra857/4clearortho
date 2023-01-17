using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho
{
    public partial class ListTrackCase : PageBase
    {
        #region Declaration
        int totalRecords;
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListTrackCase));
        #endregion

        #region Event

        /// <summary>
        /// Page Load Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if ((CurrentSession)Session["UserLoginSession"] != null)
                    {
                        PageRight pageRight = CheckRights(this.Page.GetType().BaseType.Name);
                        if (pageRight != null)
                        {
                            PageRedirect(pageRight.RedirectPageName);
                        }
                    }

                    ViewState["SortBy"] = "TrackId";
                    ViewState["AscDesc"] = "DESC";
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

        /// <summary>
        /// Set Sort Image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvTrackCase_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (lvTrackCase.Items.Count > 0)
                {
                    SetSortImage();
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Set sortBy, sortOrder, emailId parameter for listview datasource
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsTrackCase_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                CurrentSession currentSession = new CurrentSession();
                currentSession = (CurrentSession)Session["UserLoginSession"];
                if (currentSession != null)
                {
                    e.InputParameters["sortBy"] = ViewState["SortBy"].ToString();
                    e.InputParameters["sortOrder"] = ViewState["AscDesc"].ToString();
                    e.InputParameters["emailId"] = currentSession.EmailId;
                    if (ddlFilter.SelectedValue != "0")
                    {
                        e.InputParameters["searchField"] = ddlFilter.SelectedValue;
                        e.InputParameters["searchValue"] = txtSearchVal.Text.Trim();
                    }
                    else
                    {
                        e.InputParameters["searchField"] = "0";
                        e.InputParameters["searchValue"] = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Event to bound data in track case listview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvTrackCase_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                TrackCaseListDetails trackCase = (TrackCaseListDetails)e.Item.DataItem;
                Label lblBindStatus = e.Item.FindControl("lblBindStatus") as Label;
                ImageButton hypReceived = e.Item.FindControl("hypReceived") as ImageButton;
                hypReceived.OnClientClick = "javascript:return confirm('" + this.GetLocalResourceObject("ReceiveMessage") + "')";
                switch (trackCase.Status)
                {
                    case "1":
                    case "3":
                        lblBindStatus.Text = TrackingStatus.InProcess.ToString();
                        break;
                    case "2": lblBindStatus.Text = TrackingStatus.Acknowledged.ToString();
                        break;
                    case "4": lblBindStatus.Text = TrackingStatus.Shipped.ToString();
                        break;
                    case "5": lblBindStatus.Text = TrackingStatus.Received.ToString();
                        hypReceived.ImageUrl = "Content/images/order_recieved.png";
                        hypReceived.ToolTip = Convert.ToString(this.GetLocalResourceObject("Received"));
                        hypReceived.Enabled = false;
                        hypReceived.OnClientClick = string.Empty;
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Event to search in track case listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lvTrackCase.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Event on listview item command.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                            lvTrackCase.DataBind();
                            break;
                        }
                    case "CUSTOMRECEIVED":
                        {
                            TrackCase trackCase = new TrackCaseEntity().GetTrackCaseById(Convert.ToInt64(e.CommandArgument));
                            if (trackCase != null)
                            {
                                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                                string trackNo = trackCase.TrackNo;
                                long caseId = trackCase.CaseId;
                                trackCase = new TrackCaseEntity().Create();
                                trackCase.CaseId = caseId;
                                trackCase.TrackNo = trackNo;
                                trackCase.UpdatedByEmail = currentSession.EmailId;
                                trackCase.Status = ((int)TrackingStatus.Received).ToString();
                                trackCase.IsActive = true;
                                long lTrackId = new TrackCaseEntity().Save(trackCase);

                                string emailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("MailOnUpdateTrackDetails")).ToString();

                                TrackEmailDetails trackDetails = new TrackCaseEntity().GetTrackEmailDetails(lTrackId);
                                if (trackDetails != null)
                                {
                                    string updatedby = trackDetails.FirstName + " " + trackDetails.LastName;
                                    string trackStatus = TrackingStatus.Received.ToString();
                                    TrackCaseEntity.SendMailOnUpdateStatus(emailTemplatePath, trackDetails.DoctorFirstName, trackDetails.DoctorLastName, trackDetails.CaseNo, trackStatus, updatedby, trackDetails.UpdatedDate.ToString("MM/dd/yyyy"), trackDetails.DoctorEmailId, trackDetails.Description);
                                }
                                lvTrackCase.DataBind();
                            }
                            //Session["TrackId"] = e.CommandArgument.ToString();
                            //Response.Redirect("UpdateTrackDetail.aspx", true);
                            break;
                        }
                    case "CUSTOMVIEW":
                        {
                            Session["lstFileList"] = null;
                            Session["NewCaseId"] = e.CommandArgument.ToString();
                            Response.Redirect("~/AddNewCase.aspx");
                            break;
                        }
                    case "PRINT":
                        {
                            GetPrintDetailsById(Convert.ToInt64(e.CommandArgument));
                            StringWriter sw = new StringWriter();
                            HtmlTextWriter hw = new HtmlTextWriter(sw);
                            pnlPrint.RenderControl(hw);
                            string gridHTML = sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintPanelListView", "var printWindow = window.open('', '', 'width=625,height=600,left=0,top=0,toolbar=0,resizable=1,scrollbars=1');printWindow.document.write(\"<html><head><link rel='Stylesheet' href='Styles/style.css' type='text/css' /></head><body><div style='padding-left:25px;padding-top:20px; padding-bottom:20px;padding-right:25px'>" + gridHTML + "</div></body></html>\");printWindow.document.close();setTimeout(function () {printWindow.print();}, 500);", true);
                            hw.Flush();
                            hw.Dispose();
                            sw.Flush();
                            sw.Dispose();
                            break;
                        }
                }
                //if (e.CommandName.ToUpper() == "CUSTOMDELETE")
                //{
                //    TrackCase trackCase = new TrackCaseEntity().GetTrackCaseById(Convert.ToInt64(e.CommandArgument));
                //    trackCase.IsDelete = true;
                //    new TrackCaseEntity().Save(trackCase);
                //}
            }
            catch (System.Threading.ThreadAbortException) { }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        #endregion

        #region Helper

        /// <summary>
        /// update image on basis of ascending/descending sorting option
        /// </summary>
        private void SetSortImage()
        {
            try
            {
                (lvTrackCase.FindControl("lbtnTrackNo") as LinkButton).Attributes.Add("class", "");

                LinkButton lnkSortedColumn = null;
                if (ViewState["SortBy"] != null)
                {
                    switch (ViewState["SortBy"].ToString().ToLower())
                    {
                        case "trackno":
                            lnkSortedColumn = lvTrackCase.FindControl("lbtnTrackNo") as LinkButton;
                            break;
                        case "caseno":
                            lnkSortedColumn = lvTrackCase.FindControl("lbtnCaseNo") as LinkButton;
                            break;
                        case "patientname":
                            lnkSortedColumn = lvTrackCase.FindControl("lbtnPatientName") as LinkButton;
                            break;
                        case "shippingstatus":
                            lnkSortedColumn = lvTrackCase.FindControl("lbtnShippingStatus") as LinkButton;
                            break;
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

        /// <summary>
        /// Method to get list of all track case details
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="sortOrder"></param>
        /// <param name="emailId"></param>
        /// <param name="searchField"></param>
        /// <param name="searchValue"></param>
        /// <param name="pageSize"></param>
        /// <param name="startRowIndex"></param>
        /// <returns></returns>
        public List<TrackCaseListDetails> GetAllTrackCaseDetail(string sortBy, string sortOrder, string emailId, string searchField, string searchValue, int pageSize, int startRowIndex)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecordCount;
                List<TrackCaseListDetails> lstTrackCase = new TrackCaseEntity().getTrackCaseListDetails(pageIndex, pageSize, sortBy, sortOrder, searchField, searchValue, emailId, out totalRecordCount).ToList();
                totalRecords = totalRecordCount;
                return lstTrackCase;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return null;
            }
        }

        /// <summary>
        /// Method to get total row count of listview.
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="sortOrder"></param>
        /// <param name="emailId"></param>
        /// <param name="searchField"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int GetTotalRowCount(string sortBy, string sortOrder, string emailId, string searchField, string searchValue)
        {
            try
            {
                return totalRecords;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return 0;
            }
        }

        /// <summary>
        /// Method to get print details by CaseId.
        /// </summary>
        /// <param name="lcaseid"></param>
        private void GetPrintDetailsById(long lcaseid)
        {
            try
            {
                Patient patient;
                PatientCaseDetailEntity patientCaseDetailEntity = new PatientCaseDetailEntity();
                PatientCaseDetail patientCase = patientCaseDetailEntity.GetPatientCaseById(lcaseid);

                if (patientCase != null)
                {
                    patient = new PatientEntity().GetPatientById(patientCase.PatientId);
                    if (patient != null)
                    {
                        DomainDoctorDetailsByEmail doctor = new DoctorEntity().GetDoctorListByEmail(patientCase.DoctorEmailId);

                        lblPrintCreated.Text = doctor.FullName;
                        lblPrintcreatedDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                        lblPrintCaseNo.Text = patientCase.CaseNo;
                        lblPrintDN.Text = doctor.FullName;
                        lblPrintPN.Text = patient.FirstName + " " + patient.LastName;
                        if (!string.IsNullOrEmpty(patientCase.Notes))
                            ltrPrintNotes.Text = "<span style=\"margin-left:2px;font-size: 14px;\">" + patientCase.Notes + "</span>";
                        else
                            ltrPrintNotes.Text = "";
                        lblPrintDOB.Text = patient.BirthDate.ToString("MM/dd/yyyy");

                        if (patient.Gender == "M")
                            lblPrintGender.Text = "Male";
                        else if (patient.Gender == "F")
                            lblPrintGender.Text = "Female";

                        //Ortho System
                        string sOrthoSystem = string.Empty;
                        if (!string.IsNullOrEmpty(patientCase.OrthoSystem))
                        {
                            sOrthoSystem += "<table>";
                            string[] arrOrthoSystem = patientCase.OrthoSystem.Split(',');
                            foreach (string sOrtho in arrOrthoSystem)
                            {
                                switch (sOrtho)
                                {
                                    case "1":
                                        sOrthoSystem += "<tr><td>TRAY</td></tr>";
                                        break;
                                    case "2":
                                        sOrthoSystem += "<tr><td>BRACKET</td></tr>";
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        sOrthoSystem += "</table>";
                        ltrPrintOS.Text = sOrthoSystem;

                        //Ortho Condition
                        string sOrthoCondition = string.Empty;
                        if (!string.IsNullOrEmpty(patientCase.OrthoCondition))
                        {
                            sOrthoCondition += "<table>";
                            string[] arrOrthocondition = patientCase.OrthoCondition.Split(',');
                            foreach (string sOrtho in arrOrthocondition)
                            {
                                switch (sOrtho)
                                {
                                    case "1":
                                        sOrthoCondition += "<tr><td>CROWDING</td></tr>";
                                        break;
                                    case "2":
                                        sOrthoCondition += "<tr><td>SPACING</td></tr>";
                                        break;
                                    case "3":
                                        sOrthoCondition += "<tr><td>CROSSBITE</td></tr>";
                                        break;
                                    case "4":
                                        sOrthoCondition += "<tr><td>ANTERIOR</td></tr>";
                                        break;
                                    case "5":
                                        sOrthoCondition += "<tr><td>POSTERIOR</td></tr>";
                                        break;
                                    case "6":
                                        sOrthoCondition += "<tr><td>OPENBITE</td></tr>";
                                        break;
                                    case "7":
                                        sOrthoCondition += "<tr><td>DEEPBITE</td></tr>";
                                        break;
                                    case "8":
                                        sOrthoCondition += "<tr><td>NARROWARCH</td></tr>";
                                        break;
                                    default:
                                        break;
                                }
                            }
                            sOrthoCondition += "</table>";
                        }
                        ltrPrintOC.Text = sOrthoCondition;
                        //other Condition
                        if (!(string.IsNullOrEmpty(patientCase.OtherCondition)))
                        {
                            ltrPrintOther.Text = "<span style=\"font-size: 14px;\"><b>Other :&nbsp;</b>" + patientCase.OtherCondition + "</span>";
                        }
                        else
                            ltrPrintOther.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An Error Occured:" + ex);
            }
        }

        #endregion
    }
}