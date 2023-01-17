using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho
{
    public partial class ListNewCase : PageBase
    {
        #region Declaration
        int totalRecordCount;
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListNewCase));
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

                    ViewState["SortBy"] = "CreatedDate";
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
        /// Event to search in listview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lvNewCase.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("An Error Occured:" + ex);
            }
        }

        /// <summary>
        /// Event to cutom command in listview action.
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
                            lvNewCase.DataBind();
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
                                lvNewCase.DataBind();

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
                    case "RECEIVED":
                        {
                            TrackCase trackCase = new TrackCaseEntity().GetTrackCaseByCaseId(Convert.ToInt64(e.CommandArgument));
                            if (trackCase != null)
                            {
                                string trackNo = trackCase.TrackNo;
                                trackCase = new TrackCaseEntity().Create();
                                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                                trackCase.CaseId = Convert.ToInt64(e.CommandArgument);
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
                                lvNewCase.DataBind();
                                lvCompletedCaseList.DataBind();
                            }
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
                    case "REWORKCASECHARGE":
                    case "RETAINERCASECHARGE":
                        {
                            SessionHelper.ReworkORRetainer = e.CommandName;
                            Session["lstFileList"] = null;
                            Session["NewCaseId"] = e.CommandArgument.ToString();
                            Response.Redirect("~/AddNewCase.aspx");
                            break;
                        }
                    case "SHARE":
                        {
                            PatientCaseDetailEntity patientCaseDetailEntity = new PatientCaseDetailEntity();
                            PatientCaseDetail patientCase = patientCaseDetailEntity.GetPatientCaseById(Convert.ToInt64(e.CommandArgument));

                            if (patientCase != null)
                            {
                                SessionHelper.PatientId = patientCase.PatientId;
                                patientCase.IsShared = true;
                                patientCaseDetailEntity.Save(patientCase);
                                lvNewCase.DataBind();
                            }
                            break;
                        }
                    case "STAGE":
                        {
                            Patient patient;
                            PatientCaseDetailEntity patientCaseDetailEntity = new PatientCaseDetailEntity();
                            PatientCaseDetail patientCase = patientCaseDetailEntity.GetPatientCaseById(Convert.ToInt64(e.CommandArgument));

                            if (patientCase != null)
                            {
                                patient = new PatientEntity().GetPatientById(patientCase.PatientId);
                                Session["PatientId"]= patientCase.PatientId;
                                Session["PatientEmail"] = patientCase.SharedDoctorEmailId;
                               // SessionHelper.PatientId = patientCase.PatientId;
                               
                            }
                            Response.Redirect(string.Format("PatientStageDetails.aspx"));
                           
                            break;
                        }
                    case "FILE":
                        {
                            Patient patient;
                            PatientCaseDetailEntity patientCaseDetailEntity = new PatientCaseDetailEntity();
                            PatientCaseDetail patientCase = patientCaseDetailEntity.GetPatientCaseById(Convert.ToInt64(e.CommandArgument));

                            if (patientCase != null)
                            {
                                patient = new PatientEntity().GetPatientById(patientCase.PatientId);
                                Session["CaseId"] = patientCase.CaseId;
                                Session["PatientEmail"] = patientCase.SharedDoctorEmailId;
                                // SessionHelper.PatientId = patientCase.PatientId;

                            }
                            Response.Redirect(string.Format("UploadCaseAnimation.aspx"));

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

        /// <summary>
        /// Set Sort Image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvNewCase_PreRender(object sender, EventArgs e)
        {

            try
            {
                if (lvNewCase.Items.Count > 0)
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
        /// Event to add new case.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddNewCase_Click(object sender, EventArgs e)
        {
            try
            {
                Session["NewCaseId"] = null;
                SessionHelper.ReworkORRetainer = null;
                Session["lstFileList"] = null;
                Response.Redirect("AddNewCase.aspx");
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An Error Occured:" + ex);
            }
        }

        /// <summary>
        /// Event set sortField, sortDirection, emailId, and isCompleted parameter to datasource control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsNewCase_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                CurrentSession currentSession = new CurrentSession();
                currentSession = (CurrentSession)Session["UserLoginSession"];
                if (currentSession != null)
                {
                    e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                    e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();
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
                    e.InputParameters["isCompleted"] = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("An Error Occured:" + ex);
            }
        }

        /// <summary>
        /// Event to data bound to listview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvNewCase_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            NewCaseDetails trackCase = (NewCaseDetails)e.Item.DataItem;
            ImageButton hypEdit = e.Item.FindControl("hypEdit") as ImageButton;
            ImageButton imgbtnReceived = e.Item.FindControl("imgbtnReceived") as ImageButton;
            HiddenField hdnIsPayment = e.Item.FindControl("hdnIsPayment") as HiddenField;


            if (hypEdit != null)
            {
                if (trackCase.PaymentId > 0)
                {
                    hypEdit.ToolTip = Convert.ToString(this.GetLocalResourceObject("hypViewResource1.ToolTip"));
                    hypEdit.Style.Add("margin", "-5px");
                    hdnIsPayment.Value = "true";
                }
                else
                    hypEdit.ToolTip = Convert.ToString(this.GetLocalResourceObject("hypEditResource2.ToolTip"));

                imgbtnReceived.ToolTip = Convert.ToString(this.GetLocalResourceObject("ClickReceive"));
                imgbtnReceived.OnClientClick = "javascript:return confirm('" + this.GetLocalResourceObject("ReceiveMessage") + "')";
            }
        }

        /// <summary>
        /// Set Sort Image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvCompletedCaseList_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (lvCompletedCaseList.Items.Count > 0)
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
        /// Event to set sortField, sortDirection, emailId, isCompleted to datasource control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsCompletedCaseList_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                CurrentSession currentSession = new CurrentSession();
                currentSession = (CurrentSession)Session["UserLoginSession"];
                if (currentSession != null)
                {
                    e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                    e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();
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
                    e.InputParameters["isCompleted"] = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("An Error Occured:" + ex);
            }
        }

        //protected void lvCompletedCaseList_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{
        //    NewCaseDetails trackCase = (NewCaseDetails)e.Item.DataItem;
        //    ImageButton hypEdit = e.Item.FindControl("hypEdit") as ImageButton;
        //    //ImageButton imgbtnReceived = e.Item.FindControl("imgbtnReceived") as ImageButton;

        //    if (hypEdit != null)
        //    {
        //        if (trackCase.PaymentId > 0)
        //        {
        //            hypEdit.ToolTip = Convert.ToString(this.GetLocalResourceObject("hypViewResource1.ToolTip"));
        //            hypEdit.Style.Add("margin", "-5px");
        //        }
        //        else
        //            hypEdit.ToolTip = Convert.ToString(this.GetLocalResourceObject("hypEditResource2.ToolTip"));

        //        //imgbtnReceived.ToolTip = Convert.ToString(this.GetLocalResourceObject("ClickReceive"));
        //        //imgbtnReceived.OnClientClick = "javascript:return confirm('" + this.GetLocalResourceObject("ReceiveMessage") + "')";
        //    }
        //}

        #endregion

        #region Helper

        /// <summary>
        /// update image on basis of ascending/descending sorting option
        /// </summary>
        private void SetSortImage()
        {
            try
            {
                (lvNewCase.FindControl("lnkFirstName") as LinkButton).Attributes.Add("class", "");

                LinkButton lnkSortedColumn = null;
                if (ViewState["SortBy"] != null)
                {
                    switch (ViewState["SortBy"].ToString().ToLower())
                    {
                        case "firstname":
                            lnkSortedColumn = lvNewCase.FindControl("lnkFirstName") as LinkButton;
                            break;
                        case "lastname":
                            lnkSortedColumn = lvNewCase.FindControl("lnkLastName") as LinkButton;
                            break;
                        case "dateofbirth":
                            lnkSortedColumn = lvNewCase.FindControl("lnkDateofBirth") as LinkButton;
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
        /// Method to get all new case list details.
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="emailId"></param>
        /// <param name="isCompleted"></param>
        /// <param name="searchField"></param>
        /// <param name="searchValue"></param>
        /// <param name="pageSize"></param>
        /// <param name="startRowIndex"></param>
        /// <returns></returns>
        public List<NewCaseDetails> GetAllNewCase(string sortField, string sortDirection, string emailId, bool isCompleted, string searchField, string searchValue, int pageSize, int startRowIndex)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;
                List<NewCaseDetails> lstAllNewCase = new PatientCaseDetailEntity().GetNewCaseDetails(pageIndex, pageSize, sortField, sortDirection, searchField, searchValue, emailId, isCompleted, out totalRecords);
                totalRecordCount = totalRecords;
                return lstAllNewCase;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return null;
            }
        }

        /// <summary>
        /// Method to get total row count of list.
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="emailId"></param>
        /// <param name="searchField"></param>
        /// <param name="searchValue"></param>
        /// <param name="isCompleted"></param>
        /// <returns></returns>
        public int GetTotalRowCount(string sortField, string sortDirection, string emailId, string searchField, string searchValue, bool isCompleted)
        {
            try
            {
                return totalRecordCount;
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
                #region comment old code
                //NewCaseEntity newCaseEntity = new NewCaseEntity();
                //NewCase newCase = newCaseEntity.GetCaseById(lcaseid);

                //DomainDoctorDetailsByEmail doctor = new DoctorEntity().GetDoctorListByEmail(newCase.CreatedBy);

                //lblPrintCreated.Text = doctor.FullName;
                //lblPrintcreatedDate.Text = DateTime.Now.ToString("MM/dd/yyyy");

                //lblPrintFN.Text = newCase.FirstName;
                //lblPrintLN.Text = newCase.LastName;
                //if (!string.IsNullOrEmpty(newCase.Notes))
                //    ltrPrintNotes.Text = "<span style=\"margin-left:2px;font-size: 14px;\">" + newCase.Notes + "</span>";
                //else
                //    ltrPrintNotes.Text = "";
                //lblPrintDOB.Text = newCase.DateOfBirth.ToString("MM/dd/yyyy");

                //if (newCase.Gender == "M")
                //    lblPrintGender.Text = "Male";
                //else if (newCase.Gender == "F")
                //    lblPrintGender.Text = "Female";

                ////Ortho System
                //string sOrthoSystem = string.Empty;
                //if (!string.IsNullOrEmpty(newCase.OrthoSystem))
                //{
                //    sOrthoSystem += "<table>";
                //    string[] arrOrthoSystem = newCase.OrthoSystem.Split(',');
                //    foreach (string sOrtho in arrOrthoSystem)
                //    {
                //        switch (sOrtho)
                //        {
                //            case "1":
                //                sOrthoSystem += "<tr><td>TRAY</td></tr>";
                //                break;
                //            case "2":
                //                sOrthoSystem += "<tr><td>BRACKET</td></tr>";
                //                break;
                //            default:
                //                break;
                //        }
                //    }
                //}
                //sOrthoSystem += "</table>";
                //ltrPrintOS.Text = sOrthoSystem;

                ////Ortho Condition
                //string sOrthoCondition = string.Empty;
                //if (!string.IsNullOrEmpty(newCase.OrthoCondition))
                //{
                //    sOrthoCondition += "<table>";
                //    string[] arrOrthocondition = newCase.OrthoCondition.Split(',');
                //    foreach (string sOrtho in arrOrthocondition)
                //    {
                //        switch (sOrtho)
                //        {
                //            case "1":
                //                sOrthoCondition += "<tr><td>CROWDING</td></tr>";
                //                break;
                //            case "2":
                //                sOrthoCondition += "<tr><td>SPACING</td></tr>";
                //                break;
                //            case "3":
                //                sOrthoCondition += "<tr><td>CROSSBITE</td></tr>";
                //                break;
                //            case "4":
                //                sOrthoCondition += "<tr><td>ANTERIOR</td></tr>";
                //                break;
                //            case "5":
                //                sOrthoCondition += "<tr><td>POSTERIOR</td></tr>";
                //                break;
                //            case "6":
                //                sOrthoCondition += "<tr><td>OPENBITE</td></tr>";
                //                break;
                //            case "7":
                //                sOrthoCondition += "<tr><td>DEEPBITE</td></tr>";
                //                break;
                //            case "8":
                //                sOrthoCondition += "<tr><td>NARROWARCH</td></tr>";
                //                break;
                //            default:
                //                break;
                //        }
                //    }
                //    sOrthoCondition += "</table>";
                //}
                //ltrPrintOC.Text = sOrthoCondition;

                ////other Condition
                //if (!(string.IsNullOrEmpty(newCase.OtherCondition)))
                //{
                //    ltrPrintOther.Text = "<span style=\"font-size: 14px;\"><b>Other :&nbsp;</b>" + newCase.OtherCondition + "</span>";
                //}
                //else
                //    ltrPrintOther.Text = "";
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error("An Error Occured:" + ex);
            }
        }

        #endregion
    }
}