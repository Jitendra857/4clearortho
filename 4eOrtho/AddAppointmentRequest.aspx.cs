using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Web.UI;

namespace _4eOrtho
{
    public partial class AddAppointmentRequest : PageBase
    {
        #region Declaration

        private ILog logger = log4net.LogManager.GetLogger(typeof(AddAppointmentRequest));
        private string dataBaseName = string.Empty;
        private string doctorFirstName = string.Empty;
        private string doctorLastName = string.Empty;
        private string doctorEmail = string.Empty;
        private long doctorId = 0;

        #endregion Declaration

        #region Events

        /// <summary>
        /// Page Load Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ((CurrentSession)Session["UserLoginSession"] != null)
                {
                    PageRight pageRight = CheckRights(this.Page.GetType().BaseType.Name);
                    if (pageRight != null)
                    {
                        PageRedirect(pageRight.RedirectPageName);
                    }
                }

                if (CommonLogic.QueryString("dataBaseName") != null)
                    dataBaseName = Convert.ToString(CommonLogic.QueryString("dataBaseName"));

                if (CommonLogic.QueryString("doctorFirstName") != null)
                    doctorFirstName = Convert.ToString(CommonLogic.QueryString("doctorFirstName"));

                if (CommonLogic.QueryString("doctorLastName") != null)
                    doctorLastName = Convert.ToString(CommonLogic.QueryString("doctorLastName"));

                if (CommonLogic.QueryString("doctorEmail") != null)
                    doctorEmail = Convert.ToString(CommonLogic.QueryString("doctorEmail"));

                if (CommonLogic.QueryString("doctorId") != null)
                    doctorId = Convert.ToInt64(CommonLogic.QueryString("doctorId").ToString());                
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
        /// Event to submit appointment request.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    SaveAppointmentRequest();
                    ClearControls();
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

        #endregion Events

        #region Helpers

        ///// <summary>
        ///// Method to bind doctorlist
        ///// </summary>
        //private void DoctorListFill()
        //{
        //    try
        //    {
        //        //DoctorEntity doctorEntity = new DoctorEntity();
        //        //ddlDoctor.DataSource = doctorEntity.GetDoctorList();
        //        //ddlDoctor.DataTextField = "FullName";
        //        //ddlDoctor.DataValueField = "UserId";
        //        //ddlDoctor.DataBind();
        //        //ddlDoctor.Items.Insert(0, new ListItem("Select Doctor", "0"));
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("An error occured.", ex);
        //    }
        //}

        /// <summary>
        /// Method to Save appointment request.
        /// </summary>
        private void SaveAppointmentRequest()
        {
            try
            {
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                if (currentSession != null)
                {
                    DoctorEntity doctorEntity = new DoctorEntity();
                    DomainDoctorDetailsByEmail doctorDetails = doctorEntity.GetDoctorListByEmail(doctorEmail);
                    DomainPatientByEmail patientDetails = doctorEntity.GetPatientByEmail(currentSession.EmailId);                                       

                    DateTime Date = DateTime.ParseExact(txtAppointmentDate.Text.ToString(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    if (dataBaseName.ToLower() != CommonLogic.GetConfigValue("AADDatabaseName").ToLower() && dataBaseName.ToLower() != CommonLogic.GetConfigValue("4ClearOrtho").ToLower() && ((CurrentSession)Session["UserLoginSession"]).DatabaseName.ToLower() == dataBaseName.ToLower())
                    {
                        AppointmentRequestEntity entity = new AppointmentRequestEntity();
                        AppointmentRequestDetails appointmentRequest = new AppointmentRequestDetails();
                        appointmentRequest.CreatedBy = Convert.ToInt32(currentSession.PatientId);
                        appointmentRequest.PatientId = Convert.ToInt32(currentSession.PatientId);
                        appointmentRequest.PatientMobile = Convert.ToString(currentSession.PatientMobile);
                        appointmentRequest.PatientName = Convert.ToString(currentSession.PatientName);
                        appointmentRequest.PatientEmail = Convert.ToString(currentSession.EmailId);
                        appointmentRequest.DoctorId = doctorId;
                        appointmentRequest.AppointmentDate = DateTime.ParseExact(txtAppointmentDate.Text.ToString(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        appointmentRequest.PreferedTime = Date + TimeSpan.Parse(txtPreferedTime.Text);
                        appointmentRequest.Duration = hdAppLength.Value;
                        appointmentRequest.Description = txtDescription.Text;
                        appointmentRequest.CreatedDate = BaseEntity.GetServerDateTime;
                        appointmentRequest.LastUpdatedDate = BaseEntity.GetServerDateTime;
                        appointmentRequest.DatabaseName = dataBaseName;
                        entity.CreateAppointmentRequest(appointmentRequest);
                        logger.Info("AppointmentRequest saved successfully.");

                        AppointmentRequestEntity appointmentRequestEntity = new AppointmentRequestEntity();
                        AppointmentRequest request;
                        request = appointmentRequestEntity.Create();
                        request.CreatedBy = Convert.ToInt32(currentSession.PatientId);
                        request.CreatedDate = BaseEntity.GetServerDateTime;
                        request.PatientId = Convert.ToInt32(currentSession.PatientId);
                        request.PatientMobile = Convert.ToString(currentSession.PatientMobile);
                        request.PatientName = Convert.ToString(currentSession.PatientName);
                        request.PatientEmail = Convert.ToString(currentSession.EmailId);
                        request.DoctorId = doctorId;
                        request.AppointmentDate = DateTime.ParseExact(txtAppointmentDate.Text.ToString(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        Date = DateTime.ParseExact(txtAppointmentDate.Text.ToString(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        request.PreferedTime = Date + TimeSpan.Parse(txtPreferedTime.Text);
                        request.Duration = hdAppLength.Value;
                        request.Description = txtDescription.Text;
                        request.CreatedDate = BaseEntity.GetServerDateTime;
                        request.LastUpdatedDate = BaseEntity.GetServerDateTime;
                        request.IsBooked = false;
                        request.AppointmentId = 0;
                        request.IsActive = true;
                        request.IsPurge = false;
                        request.DoctorEmail = doctorEmail;
                        appointmentRequestEntity.Save(request);
                        DateTime appointmentDate = DateTime.ParseExact(txtAppointmentDate.Text.ToString(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime appointmentTime = appointmentDate + TimeSpan.Parse(txtPreferedTime.Text);
                        CommonHelper.ShowMessage(MessageType.Success, "Appointment request send successfully", divMsg, lblMsg);
                        SendAppointmentRequestMail(patientDetails, doctorDetails, doctorEmail, doctorFirstName, doctorLastName, currentSession.PatientFirstName, currentSession.PatientLastName, txtDescription.Text, hdAppLength.Value, appointmentDate.ToString("MM/dd/yyyy"), appointmentTime.ToString("MM/dd/yyy HH:mm tt"));
                        CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("AppointMentRequestSave").ToString(), divMsg, lblMsg);
                    }
                    else
                    {
                        AppointmentRequestEntity appointmentRequestEntity = new AppointmentRequestEntity();
                        AppointmentRequest request;
                        request = appointmentRequestEntity.Create();
                        request.CreatedBy = Convert.ToInt32(currentSession.PatientId);
                        request.CreatedDate = BaseEntity.GetServerDateTime;
                        request.PatientId = Convert.ToInt32(currentSession.PatientId);
                        request.PatientMobile = Convert.ToString(currentSession.PatientMobile);
                        request.PatientName = Convert.ToString(currentSession.PatientName);
                        request.PatientEmail = Convert.ToString(currentSession.EmailId);
                        request.DoctorId = doctorId;
                        request.AppointmentDate = DateTime.ParseExact(txtAppointmentDate.Text.ToString(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        Date = DateTime.ParseExact(txtAppointmentDate.Text.ToString(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        request.PreferedTime = Date + TimeSpan.Parse(txtPreferedTime.Text);
                        request.Duration = hdAppLength.Value;
                        request.Description = txtDescription.Text;
                        request.CreatedDate = BaseEntity.GetServerDateTime;
                        request.LastUpdatedDate = BaseEntity.GetServerDateTime;
                        request.IsBooked = false;
                        request.AppointmentId = 0;
                        request.IsActive = true;
                        request.IsPurge = false;
                        request.DoctorEmail = doctorEmail;
                        appointmentRequestEntity.Save(request);

                        DateTime appointmentDate = DateTime.ParseExact(txtAppointmentDate.Text.ToString(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime appointmentTime = appointmentDate + TimeSpan.Parse(txtPreferedTime.Text);
                        SendAppointmentRequestMail(patientDetails, doctorDetails, doctorEmail, doctorFirstName, doctorLastName, currentSession.PatientFirstName, currentSession.PatientLastName, txtDescription.Text, hdAppLength.Value, appointmentDate.ToString("MM/dd/yyyy"), appointmentTime.ToString("MM/dd/yyy HH:mm tt"));
                        CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("AppointMentRequestSave").ToString(), divMsg, lblMsg);
                    }
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "ResetForm();", true);
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Method to clear controls data.
        /// </summary>
        private void ClearControls()
        {
            hdEditLength.Value = "10";
            txtAppointmentDate.Text = string.Empty;
            txtPreferedTime.Text = string.Empty;
            txtDescription.Text = string.Empty;
            chkIsBooked.Checked = false;
        }

        /// <summary>
        /// Method to send appointment request mail to doctor.
        /// </summary>
        /// <param name="patientDetails"></param>
        /// <param name="doctorDetails"></param>
        /// <param name="doctorEmailId"></param>
        /// <param name="doctorFirstName"></param>
        /// <param name="doctorLastName"></param>
        /// <param name="patientFirstName"></param>
        /// <param name="patientLastName"></param>
        /// <param name="description"></param>
        /// <param name="duration"></param>
        /// <param name="appointmentDate"></param>
        /// <param name="prefferedTime"></param>
        public void SendAppointmentRequestMail(DomainPatientByEmail patientDetails, DomainDoctorDetailsByEmail doctorDetails, string doctorEmailId, string doctorFirstName, string doctorLastName, string patientFirstName, string patientLastName, string description, string duration, string appointmentDate, string prefferedTime)
        {
            try
            {
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                string patientEmailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("PatientAppointmentRequest")).ToString();
                DoctorEntity doctorEntity = new DoctorEntity();
                doctorEntity.SendPatientAppointmentRequestMail(patientDetails, doctorDetails, doctorEmailId, currentSession.EmailId, doctorFirstName + " " + doctorLastName, doctorFirstName, doctorLastName, currentSession.PatientName, patientFirstName, patientLastName, description, duration, appointmentDate, prefferedTime, patientEmailTemplatePath, "4ClearOrtho - Appointment Request");

                string doctorEmailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("DoctorAppointmentRequest")).ToString();
                doctorEntity.SendDoctorAppointmentRequestMail(patientDetails, doctorDetails, doctorEmailId, currentSession.EmailId, doctorFirstName + " " + doctorLastName, doctorFirstName, doctorLastName, currentSession.PatientName, patientFirstName, patientLastName, description, duration, appointmentDate, prefferedTime, doctorEmailTemplatePath, "4ClearOrtho - Appointment Request");
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        #endregion Helpers
    }
}