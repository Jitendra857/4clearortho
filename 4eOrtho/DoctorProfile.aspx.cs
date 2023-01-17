using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;

namespace _4eOrtho
{
    public partial class DoctorProfile : PageBase
    {
        #region Declaration
        
        private decimal totalAvgRating = 0;
        public static string emailId = string.Empty;
        public string rating = string.Empty;
        DoctorRatingEntity doctorRatingEntity;
        List<DoctorRating> lstDoctorRating;
        //public long appointmentId = 0;
        //public long officeId = 0;
        //public long staffId = 0;
        //public long waitTimeId = 0;
        //public long decisionId = 0;
        //public long conditionId = 0;
        //public long answerId = 0;
        //public long spendsRatingId = 0;
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
                emailId = CommonLogic.DecryptStringAES(CommonLogic.QueryString("EmailId").ToString());
            if (!String.IsNullOrEmpty(CommonLogic.QueryString("Rating")))
                rating = CommonLogic.DecryptStringAES(CommonLogic.QueryString("Rating").ToString());

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

                DoctorDetailsBind(emailId);
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
                    this.MasterPageFile = "~/OrthoInnerMaster.Master";
                else
                    this.MasterPageFile = "~/Ortho.Master";
            }
            else
            {
                this.MasterPageFile = "~/Ortho.Master";
            }
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Doctor Details bind by email id
        /// </summary>
        /// <param name="emailId"></param>
        public void DoctorDetailsBind(string emailId)
        {
            AllDoctorDetailsByEmailId doctorDetails = new DoctorEntity().GetDoctorDetailsByEmailId(emailId);
            if (doctorDetails != null)
            {
                if (doctorDetails.SourceType == SourceType.AAAD.ToString() && !string.IsNullOrEmpty(doctorDetails.PhotographName))
                {
                    imgProfilePic.ImageUrl = CommonLogic.GetConfigValue("AAAD_DoctorPath").ToString() + doctorDetails.PhotographName;
                }
                else if (!string.IsNullOrEmpty(doctorDetails.PhotographName))
                {
                    if (!string.IsNullOrEmpty(doctorDetails.PhotographName))
                        imgProfilePic.ImageUrl = doctorDetails.DomainURL + "Photograph/" + doctorDetails.PhotographName;
                    else
                        imgProfilePic.ImageUrl = "Content/images/male_sm.jpg";
                }
                else
                {
                    imgProfilePic.ImageUrl = "Content/images/male_lg.jpg";
                }
                if (doctorDetails.Is4eOrthoPass == 0)
                { starimg.Visible = false; }
                else
                { starimg.Visible = true; }
                lblDoctorName.Text = "Dr. " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(doctorDetails.DoctorName);
                lblDoctorNameTitle.Text = "Dr. " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(doctorDetails.DoctorName);
                lblDoctorNameTitle2.Text = "Dr. " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(doctorDetails.DoctorName);
                lblCity.Text = doctorDetails.City;
                lblStateName.Text = doctorDetails.StateName;
                lblCountryName.Text = doctorDetails.CountryName;
                lblZipCode.Text = doctorDetails.zipcode;
                lblMobileNo.Text = doctorDetails.Mobile;
            }
        }
        /// <summary>
        /// Bind Rating Details
        /// </summary>
        public void BindRatingDetails()
        {

            doctorRatingEntity = new DoctorRatingEntity();
            lstDoctorRating = doctorRatingEntity.GetDoctorRatingByEmail(emailId);
            if (lstDoctorRating.Count > 0)
            {
                totalAvgRating = Convert.ToDecimal(Convert.ToDecimal(lstDoctorRating.Sum(x => x.AverageRating)) / lstDoctorRating.Count);
                hdPatientSatisfaction.Value = Convert.ToString(totalAvgRating);
            }
            DoctorRatingDetailsByDoctorEmail doctorRatingDetails = new DoctorRatingDetailsEntity().GetDoctorDetailsByDoctorEmail(emailId);
            if (doctorRatingDetails != null)
            {
                hdAppointment.Value = Convert.ToString(doctorRatingDetails.Ease_of_scheduling_urgent_appointments);
                hdOffice.Value = Convert.ToString(doctorRatingDetails.Office_environment__cleanliness__comfort__etc_);
                hdStaff.Value = Convert.ToString(doctorRatingDetails.Staff_friendliness_and_courteousness);
                hdWaitTime.Value = Convert.ToString(doctorRatingDetails.Total_wait_time__waiting___exam_rooms_);
                hdDecisions.Value = Convert.ToString(doctorRatingDetails.Level_of_trust_in_provider_s_decisions);
                hdCondition.Value = Convert.ToString(doctorRatingDetails.How_well_provider_explains_medical_condition_s_);
                hdAnswers.Value = Convert.ToString(doctorRatingDetails.How_well_provider_listens_and_answers_questions);
                hdSpendsRating.Value = Convert.ToString(doctorRatingDetails.Spends_appropriate_amount_of_time_with_patients);
            }
        }
        #endregion

    }
}