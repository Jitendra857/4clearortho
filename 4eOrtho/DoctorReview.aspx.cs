using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho
{
    public partial class DoctorReview : PageBase
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
        private ILog logger = log4net.LogManager.GetLogger(typeof(DoctorReview));
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

            CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
            if (currentSession !=  null && !string.IsNullOrEmpty(currentSession.EmailId))
                dvDoctorReview.Visible = true;
            else
                dvDoctorReview.Visible = false;
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
                BindReviewDetails();
            }
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        }

        /// <summary>
        /// display no review when there is no review entered by patient
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Repeater rptDemo = sender as Repeater; // Get the Repeater control object.

                // If the Repeater contains no data.
                if (rptPatientReview != null && rptPatientReview.Items.Count < 1)
                {
                    if (e.Item.ItemType == ListItemType.Footer)
                    {
                        Label lblErrorMsg = e.Item.FindControl("lblErrorMsg") as Label;
                        if (lblErrorMsg != null)
                        {
                            lblErrorMsg.Text = "No review for " + lblDoctorName.Text + " yet";
                            lblErrorMsg.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// set master page as per user session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Save Review
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                if (currentSession != null && txtReview.Text != "")
                {
                    ReviewEntity reviewEntity = new ReviewEntity();
                    Review review = reviewEntity.Create();
                    review.CreatedBy = Convert.ToInt32(currentSession.PatientUserId);
                    review.CreatedDate = BaseEntity.GetServerDateTime;
                    review.ReviewContent = txtReview.Text.Trim();
                    review.PatientEmail = currentSession.EmailId;
                    review.DoctorEmail = emailId;
                    review.IsActive = false;
                    reviewEntity.Save(review);
                    txtReview.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ReviewMessage", "DoctorReviewMessage();", true);
                }
                else
                {
                    Response.Redirect("PatientLogin.aspx", false);
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
        /// Doctor Details bind by email id
        /// </summary>
        /// <param name="emailId"></param>
        public void DoctorDetailsBind(string emailId)
        {
            try
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
                    lblDoctorName.Text = lblDoctorname1.Text = lblDoctorName2.Text = "Dr." + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(doctorDetails.DoctorName);
                    lblCity.Text = doctorDetails.City;
                    lblStateName.Text = doctorDetails.StateName;
                    lblCountryName.Text = doctorDetails.CountryName;
                    lblZipCode.Text = doctorDetails.zipcode;
                    lblMobileNo.Text = doctorDetails.Mobile;
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// bind review details b
        /// </summary>
        public void BindReviewDetails()
        {
            try
            {
                List<AllPatientReviewByDoctorEmail> lstPatientReview = new ReviewEntity().GetAllPatientReviews(emailId);
                rptPatientReview.DataSource = lstPatientReview;
                rptPatientReview.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// Bind Rating Details
        /// </summary>
        public void BindRatingDetails()
        {
            try
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
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        #endregion
    }
}