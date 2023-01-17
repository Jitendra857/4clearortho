using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Web.UI;

namespace _4eOrtho
{
    public partial class UpdateTrackDetail : PageBase
    {
        #region Declaration
        long lTrackId = 1;
        private ILog logger = log4net.LogManager.GetLogger(typeof(UpdateTrackDetail));
        TrackCaseEntity trackCaseEntity;
        TrackCase trackCase;
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
                if (Session["TrackId"] != null)
                    lTrackId = Convert.ToInt64(Session["TrackId"]);
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
                    
                    GetTrackCaseDetailById(lTrackId);
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

        /// <summary>
        /// Event to update track details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        if (trackCase != null)
                        {
                            CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                            trackCase.UpdatedByEmail = currentSession.EmailId;
                            if (ddlUpdateStatus.SelectedValue == TrackingStatus.Received.ToString())
                                trackCase.Status = ((int)TrackingStatus.Received).ToString();
                            if (!string.IsNullOrEmpty(txtDescription.Text.Trim()))
                                trackCase.Description = txtDescription.Text.Trim();

                            trackCase.IsActive = chkIsActive.Checked;

                            lTrackId = trackCaseEntity.Save(trackCase);

                            //if (!ddlUpdateStatus.SelectedValue.Equals(lblCurrentStatus.Text.Trim()))
                            //{
                            string emailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("MailOnUpdateTrackDetails")).ToString();

                            TrackEmailDetails trackDetails = trackCaseEntity.GetTrackEmailDetails(lTrackId);
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
                            Response.Redirect("ListTrackCase.aspx", true);
                        }
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

        /// <summary>
        /// Method to get track case details by track id.
        /// </summary>
        /// <param name="ltrackId"></param>
        public void GetTrackCaseDetailById(long ltrackId)
        {
            try
            {
                PatientCaseDetail patientCase;
                trackCaseEntity = new TrackCaseEntity();
                trackCase = trackCaseEntity.GetTrackCaseById(ltrackId);
                if (trackCase != null)
                {
                    patientCase = new PatientCaseDetailEntity().GetPatientCaseById(trackCase.CaseId);

                    lblCaseNo.Text = patientCase.CaseNo;

                    switch (trackCase.Status)
                    {
                        case "1": lblCurrentStatus.Text = TrackingStatus.Submitted.ToString();
                            break;
                        case "2": lblCurrentStatus.Text = TrackingStatus.Acknowledged.ToString();
                            break;
                        case "3": lblCurrentStatus.Text = TrackingStatus.InProcess.ToString();
                            break;
                        case "4": lblCurrentStatus.Text = TrackingStatus.Shipped.ToString();
                            break;
                        case "5": lblCurrentStatus.Text = TrackingStatus.Received.ToString();
                            ddlUpdateStatus.SelectedValue = TrackingStatus.Received.ToString();
                            dvSelectStatus.Style.Add("display", "none");
                            break;
                    }
                    if (!string.IsNullOrEmpty(trackCase.Description))
                        txtDescription.Text = trackCase.Description;

                    lblTrackNo.Text = trackCase.TrackNo;
                    chkIsActive.Checked = trackCase.IsActive;
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured .", ex);
            }
        }

        #endregion
    }
}