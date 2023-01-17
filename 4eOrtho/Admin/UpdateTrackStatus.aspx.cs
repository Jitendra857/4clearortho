using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.Helper;

namespace _4eOrtho.Admin
{
    public partial class UpdateTrackStatus : PageBase
    {
        #region Declaration
        long lTrackId = 0;
        private ILog logger = log4net.LogManager.GetLogger(typeof(UpdateTrackStatus));
        TrackCaseEntity trackCaseEntity;
        TrackCase trackCase;
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["TrackStatusId"] != null)
                    lTrackId = Convert.ToInt64(Session["TrackStatusId"]);
                if (!Page.IsPostBack)
                {
                    if (lTrackId > 0)
                    {
                        GetTrackCaseDetailById(lTrackId);
                    }
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    trackCaseEntity = new TrackCaseEntity();
                    if (lTrackId > 0)
                    {
                        trackCase = trackCaseEntity.GetTrackCaseById(lTrackId);

                        string trackNo = trackCase.TrackNo;
                        long caseId = trackCase.CaseId;

                        trackCase = new TrackCaseEntity().Create();

                        trackCase.TrackNo = trackNo;
                        trackCase.CaseId = caseId;

                        switch (ddlUpdateStatus.SelectedValue)
                        {
                            case "Acknowledged": trackCase.Status = ((int)TrackingStatus.Acknowledged).ToString();
                                break;
                            case "InProcess": trackCase.Status = ((int)TrackingStatus.InProcess).ToString();
                                break;
                            case "Shipped": trackCase.Status = ((int)TrackingStatus.Shipped).ToString();
                                break;
                        }
                        if (!string.IsNullOrEmpty(txtDescription.Text.Trim()))
                            trackCase.Description = txtDescription.Text.Trim();
                        else
                            trackCase.Description = string.Empty;
                        //CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                        trackCase.UpdatedByEmail = SessionHelper.LoggedAdminEmailAddress;
                        trackCase.IsActive = true;

                        lTrackId = trackCaseEntity.Save(trackCase);
                        //if (!(lblCurrentStatus.Text.Trim().Equals(ddlUpdateStatus.SelectedValue)))
                        //{
                        logger.Info("btnSubmit_Click." + lTrackId);
                        string emailTemplatePath = Server.MapPath("~/" + CommonLogic.GetConfigValue("MailOnUpdateTrackDetails").ToString()).ToString();

                        TrackEmailDetails trackDetails = trackCaseEntity.GetTrackEmailDetails(lTrackId);
                        logger.Info("btnSubmit_Click." + trackDetails);
                        if (trackDetails != null)
                        {
                            string updatedby = trackDetails.FirstName + " " + trackDetails.LastName;
                            string trackStatus = string.Empty;
                            switch (trackCase.Status)
                            {
                                case "1": trackStatus = TrackingStatus.Submitted.ToString();
                                    break;
                                case "2": trackStatus = TrackingStatus.Acknowledged.ToString();
                                    break;
                                case "3": trackStatus = TrackingStatus.InProcess.ToString();
                                    break;
                                case "4": trackStatus = TrackingStatus.Shipped.ToString();
                                    break;
                                case "5": trackStatus = TrackingStatus.Received.ToString();
                                    break;
                            }

                            TrackCaseEntity.SendMailOnUpdateStatus(emailTemplatePath, trackDetails.DoctorFirstName, trackDetails.DoctorLastName, trackDetails.CaseNo, trackStatus, updatedby, trackDetails.UpdatedDate.ToString("MM/dd/yyyy"), trackDetails.DoctorEmailId, trackDetails.Description);
                        }
                        //}
                        Response.Redirect("ListNewCaseDetails.aspx", true);
                    }
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
        #endregion

        #region Helper
        public void GetTrackCaseDetailById(long ltrackId)
        {
            try
            {
                PatientCaseDetail patientCase;
                trackCaseEntity = new TrackCaseEntity();
                trackCase = trackCaseEntity.GetTrackCaseById(ltrackId);
                if (trackCase != null)
                    patientCase = new PatientCaseDetailEntity().GetPatientCaseById(trackCase.CaseId);
                else
                    patientCase = new PatientCaseDetail();

                lblCaseNo.Text = patientCase.CaseNo;
                //switch (trackCase.Status)
                //{
                //    case "1": lblCurrentStatus.Text = TrackingStatus.Submitted.ToString();
                //        break;
                //    case "2": lblCurrentStatus.Text = TrackingStatus.Acknowledged.ToString();
                //        ddlUpdateStatus.SelectedValue = TrackingStatus.Acknowledged.ToString();
                //        break;
                //    case "3": lblCurrentStatus.Text = TrackingStatus.InProcess.ToString();
                //        ddlUpdateStatus.SelectedValue = TrackingStatus.InProcess.ToString();
                //        break;
                //    case "4": lblCurrentStatus.Text = TrackingStatus.Shipped.ToString();
                //        ddlUpdateStatus.SelectedValue = TrackingStatus.Shipped.ToString();
                //        break;
                //    case "5": lblCurrentStatus.Text = TrackingStatus.Received.ToString();
                //        break;
                //}
                //txtDescription.Text = trackCase.Description;
                lblTrackNo.Text = trackCase.TrackNo;
                //chkIsActive.Checked = trackCase.IsActive;

                lvTrackHistory.DataSource = new TrackCaseEntity().GetAllTrackHistory(trackCase.TrackNo);
                lvTrackHistory.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured .", ex);
            }
        }

        #endregion

    }
}