using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;

namespace _4eOrtho
{
    public partial class Rating : PageBase
    {
        #region Declaration
        private long doctorRatingId = 0;
        private decimal totalAvgRating = 0;
        private string doctorEmail = string.Empty;
        public string rating = string.Empty;
        LookupMaster lookUpMaster;
        DoctorRatingDetailsEntity doctorRatingDetailsEntity;
        DoctorRatingDetail doctorRatingDetail;
        DoctorRatingEntity doctorRatingEntity;
        DoctorRating doctorRating;
        List<DoctorRating> lstDoctorRating;
        public long appointmentId = 0;
        public long officeId = 0;
        public long staffId = 0;
        public long waitTimeId = 0;
        public long decisionId = 0;
        public long conditionId = 0;
        public long answerId = 0;
        public long spendsRatingId = 0;
        private ILog logger = log4net.LogManager.GetLogger(typeof(Rating));
        #endregion

        #region Events

        /// <summary>
        /// Bind Doctor Detail on page load events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(CommonLogic.QueryString("EmailId")))
                doctorEmail = CommonLogic.DecryptStringAES(CommonLogic.QueryString("EmailId").ToString());
            if (!String.IsNullOrEmpty(CommonLogic.QueryString("Rating")))
                rating = CommonLogic.DecryptStringAES(CommonLogic.QueryString("Rating").ToString());

            if (!Page.IsPostBack)
            {
                DoctorDetailsBind(doctorEmail);
                BindRatingDetails();
            }
        }

        /// <summary>
        /// set master page as per user session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreInit(object sender, EventArgs e)
        {

            if (Session["UserLoginSession"] != null)
            {
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                if (currentSession.UserType.ToString() == UserType.D.ToString() || currentSession.UserType.ToString() == UserType.S.ToString() || currentSession.UserType.ToString() == UserType.P.ToString())
                {
                    this.MasterPageFile = "~/OrthoInnerMaster.Master";
                }
                else
                {
                    this.MasterPageFile = "~/Ortho.Master";
                }
            }
            else
            {
                this.MasterPageFile = "~/Ortho.Master";
            }
        }

        /// <summary>
        /// Event to save rating details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                lookUpMaster = new LookupMaster();
                if (currentSession != null)
                {
                    logger.Error(currentSession.EmailId);
                    logger.Error(doctorEmail);
                    doctorRating = new DoctorRatingEntity().GetDoctorRatingByPatientEmail(currentSession.EmailId, doctorEmail);
                    doctorRatingEntity = new DoctorRatingEntity();

                    if (doctorRating == null)
                    {
                        doctorRating = doctorRatingEntity.Create();
                        doctorRating.CreatedBy = Authentication.GetLoggedUserID();
                        doctorRating.CreatedDate = BaseEntity.GetServerDateTime;
                    }
                    else
                    {
                        doctorRating.LastUpdatedBy = Authentication.GetLoggedUserID();
                        doctorRating.LastUpdatedDate = BaseEntity.GetServerDateTime;
                    }

                    doctorRating.DoctorEmail = doctorEmail;
                    doctorRating.PatientEmail = currentSession.EmailId;
                    doctorRating.IsActive = true;
                    doctorRatingId = doctorRatingEntity.Save(doctorRating);


                    doctorRatingDetailsEntity = new DoctorRatingDetailsEntity();
                    doctorRatingDetailsEntity.DeleteDoctorDetailsByPatientEmail(currentSession.EmailId, doctorRatingId);
                    if (hdAppointment.Value != "")
                    {
                        appointmentId = new LookupMasterEntity().GetLookupMasterDetails("Ease of scheduling urgent appointments").LookupId;
                        doctorRatingDetail = doctorRatingDetailsEntity.Create();
                        doctorRating.CreatedBy = Authentication.GetLoggedUserID();
                        doctorRating.CreatedDate = BaseEntity.GetServerDateTime;
                        doctorRatingDetail.LookupId = appointmentId;
                        doctorRatingDetail.DoctorRatingId = doctorRatingId;
                        doctorRatingDetail.Rate = Convert.ToDecimal(hdAppointment.Value);
                        doctorRatingDetailsEntity.Save(doctorRatingDetail);
                    }

                    if (hdOffice.Value != "")
                    {
                        officeId = new LookupMasterEntity().GetLookupMasterDetails("Office environment, cleanliness, comfort, etc.").LookupId;
                        doctorRatingDetail = doctorRatingDetailsEntity.Create();
                        doctorRating.CreatedBy = Authentication.GetLoggedUserID();
                        doctorRating.CreatedDate = BaseEntity.GetServerDateTime;
                        doctorRatingDetail.LookupId = officeId;
                        doctorRatingDetail.DoctorRatingId = doctorRatingId;
                        doctorRatingDetail.Rate = Convert.ToDecimal(hdOffice.Value);
                        doctorRatingDetailsEntity.Save(doctorRatingDetail);
                    }
                    if (hdStaff.Value != "")
                    {
                        staffId = new LookupMasterEntity().GetLookupMasterDetails("Staff friendliness and courteousness").LookupId;
                        doctorRatingDetail = doctorRatingDetailsEntity.Create();
                        doctorRating.CreatedBy = Authentication.GetLoggedUserID();
                        doctorRating.CreatedDate = BaseEntity.GetServerDateTime;
                        doctorRatingDetail.LookupId = staffId;
                        doctorRatingDetail.DoctorRatingId = doctorRatingId;
                        doctorRatingDetail.Rate = Convert.ToDecimal(hdStaff.Value);
                        doctorRatingDetailsEntity.Save(doctorRatingDetail);
                    }
                    if (hdWaitTime.Value != "")
                    {
                        waitTimeId = new LookupMasterEntity().GetLookupMasterDetails("Total wait time (waiting & exam rooms)").LookupId;
                        doctorRatingDetail = doctorRatingDetailsEntity.Create();
                        doctorRating.CreatedBy = Authentication.GetLoggedUserID();
                        doctorRating.CreatedDate = BaseEntity.GetServerDateTime;
                        doctorRatingDetail.LookupId = waitTimeId;
                        doctorRatingDetail.DoctorRatingId = doctorRatingId;
                        doctorRatingDetail.Rate = Convert.ToDecimal(hdWaitTime.Value);
                        doctorRatingDetailsEntity.Save(doctorRatingDetail);
                    }
                    if (hdDecisions.Value != "")
                    {
                        decisionId = new LookupMasterEntity().GetLookupMasterDetails("Level of trust in provider's decisions").LookupId;
                        doctorRatingDetail = doctorRatingDetailsEntity.Create();
                        doctorRating.CreatedBy = Authentication.GetLoggedUserID();
                        doctorRating.CreatedDate = BaseEntity.GetServerDateTime;
                        doctorRatingDetail.LookupId = decisionId;
                        doctorRatingDetail.DoctorRatingId = doctorRatingId;
                        doctorRatingDetail.Rate = Convert.ToDecimal(hdDecisions.Value);
                        doctorRatingDetailsEntity.Save(doctorRatingDetail);
                    }
                    if (hdCondition.Value != "")
                    {
                        conditionId = new LookupMasterEntity().GetLookupMasterDetails("How well provider explains medical condition(s)").LookupId;
                        doctorRatingDetail = doctorRatingDetailsEntity.Create();
                        doctorRating.CreatedBy = Authentication.GetLoggedUserID();
                        doctorRating.CreatedDate = BaseEntity.GetServerDateTime;
                        doctorRatingDetail.LookupId = conditionId;
                        doctorRatingDetail.DoctorRatingId = doctorRatingId;
                        doctorRatingDetail.Rate = Convert.ToDecimal(hdCondition.Value);
                        doctorRatingDetailsEntity.Save(doctorRatingDetail);
                    }
                    if (hdAnswers.Value != "")
                    {
                        answerId = new LookupMasterEntity().GetLookupMasterDetails("How well provider listens and answers questions").LookupId;
                        doctorRatingDetail = doctorRatingDetailsEntity.Create();
                        doctorRating.CreatedBy = Authentication.GetLoggedUserID();
                        doctorRating.CreatedDate = BaseEntity.GetServerDateTime;
                        doctorRatingDetail.LookupId = answerId;
                        doctorRatingDetail.DoctorRatingId = doctorRatingId;
                        doctorRatingDetail.Rate = Convert.ToDecimal(hdAnswers.Value);
                        doctorRatingDetailsEntity.Save(doctorRatingDetail);
                    }
                    if (hdSpendsRating.Value != "")
                    {
                        spendsRatingId = new LookupMasterEntity().GetLookupMasterDetails("Spends appropriate amount of time with patients").LookupId;
                        doctorRatingDetail = doctorRatingDetailsEntity.Create();
                        doctorRating.CreatedBy = Authentication.GetLoggedUserID();
                        doctorRating.CreatedDate = BaseEntity.GetServerDateTime;
                        doctorRatingDetail.LookupId = spendsRatingId;
                        doctorRatingDetail.DoctorRatingId = doctorRatingId;
                        doctorRatingDetail.Rate = Convert.ToDecimal(hdSpendsRating.Value);
                        doctorRatingDetailsEntity.Save(doctorRatingDetail);
                    }
                    decimal totalRating = 0;
                    totalRating = Convert.ToDecimal(hdAppointment.Value) + Convert.ToDecimal(hdOffice.Value) + Convert.ToDecimal(hdStaff.Value) + Convert.ToDecimal(hdWaitTime.Value) + Convert.ToDecimal(hdDecisions.Value) + Convert.ToDecimal(hdCondition.Value) + Convert.ToDecimal(hdAnswers.Value) + Convert.ToDecimal(hdSpendsRating.Value);
                    totalAvgRating = totalRating / 8;


                    doctorRating = new DoctorRatingEntity().GetDoctorRatingById(doctorRatingId);
                    doctorRating.CreatedBy = Authentication.GetLoggedUserID();
                    doctorRating.CreatedDate = BaseEntity.GetServerDateTime;
                    doctorRating.DoctorEmail = doctorEmail;
                    doctorRating.PatientEmail = currentSession.EmailId;
                    doctorRating.AverageRating = totalAvgRating;
                    doctorRating.IsActive = true;
                    doctorRatingId = doctorRatingEntity.Save(doctorRating);
                    // Commented by navik
                    //Response.Redirect("FindDoctor.aspx", false);
                    Response.Redirect("PatientBeforeAfterPictures.aspx", false);
                }
                else
                {
                    Response.Redirect("PatientLogin.aspx",false);
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Method to bind doctor details.
        /// </summary>
        /// <param name="emailId"></param>
        public void DoctorDetailsBind(string emailId)
        {
            AllDoctorDetailsByEmailId doctorDetails = new DoctorEntity().GetDoctorDetailsByEmailId(emailId);
            if (doctorDetails != null)
            {
                if (doctorDetails.SourceType == SourceType.EMR.ToString() && !string.IsNullOrEmpty(doctorDetails.PhotographName))
                {
                    imgProfilePic.ImageUrl = CommonLogic.GetConfigValue("EMR_DoctorPath").ToString() + doctorDetails.PhotographName;
                }
                else if (doctorDetails.SourceType == SourceType.AAAD.ToString() && !string.IsNullOrEmpty(doctorDetails.PhotographName))
                {
                    imgProfilePic.ImageUrl = CommonLogic.GetConfigValue("AAAD_DoctorPath").ToString() + doctorDetails.PhotographName;
                }
                else
                {
                    imgProfilePic.ImageUrl = "Content/images/male_lg.jpg";
                }
                if (doctorDetails.Is4eOrthoPass == 0)
                { starimg.Visible = false; }
                else
                { starimg.Visible = true; }
                lblDoctorName.Text = "Dr." + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(doctorDetails.DoctorName);
                lblDoctorStaff.Text = "Dr." + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(doctorDetails.DoctorName) + "'s "+this.GetLocalResourceObject("Office");
                lblDoctorExperience.Text = this.GetLocalResourceObject("Experience") + " Dr." + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(doctorDetails.DoctorName);
                lblCity.Text = doctorDetails.City;
                lblStateName.Text = doctorDetails.StateName;
                lblCountryName.Text = doctorDetails.CountryName;
                lblZipCode.Text = doctorDetails.zipcode;
                lblMobileNo.Text = doctorDetails.Mobile;
            }
        }

        /// <summary>
        /// Method to bind rating details.
        /// </summary>
        public void BindRatingDetails()
        {
            doctorRatingEntity = new DoctorRatingEntity();
            lstDoctorRating = doctorRatingEntity.GetDoctorRatingByEmail(doctorEmail);
            if (lstDoctorRating.Count > 0)
            {
                totalAvgRating = Convert.ToDecimal(Convert.ToDecimal(lstDoctorRating.Sum(x => x.AverageRating)) / lstDoctorRating.Count);
                hdPatientSatisfaction.Value = Convert.ToString(totalAvgRating);
            }
        }

        #endregion
    }
}