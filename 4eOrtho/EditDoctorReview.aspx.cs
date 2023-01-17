using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Web.UI;

namespace _4eOrtho
{
    public partial class EditDoctorReview : PageBase
    {
        #region Declaration
        long reviewId = 0;
        private ILog logger = log4net.LogManager.GetLogger(typeof(EditDoctorReview));
        ReviewByReviewId reviewDetails = new ReviewByReviewId();
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(CommonLogic.QueryString("reviewId")))
                    reviewId = Convert.ToInt64(CommonLogic.QueryString("reviewId"));

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

                    DoctorReviewBind(reviewId);
                    lblHeader.Text = "Edit Doctor Review";
                    Page.Title = "4ClearOrtho - Edit Doctor Review";
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
        #endregion

        #region Helpers
        public void DoctorReviewBind(long reviewId)
        {
            ReviewEntity reviewEntity = new ReviewEntity();
            reviewDetails = new ReviewByReviewId();
            reviewDetails = reviewEntity.GetReviewByReviewId(reviewId);
            lblPatientName.Text = reviewDetails.PatientName;
            lblPatientEmail.Text = reviewDetails.PatientEmail;
            lblReviewContent.Text = reviewDetails.ReviewContent;
            chkIsActive.Checked = reviewDetails.IsActive;

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                if (currentSession != null)
                {

                    ReviewEntity reviewEntity = new ReviewEntity();
                    Review review = reviewEntity.GetReviewById(reviewId);
                    review.IsActive = chkIsActive.Checked;
                    reviewEntity.Save(review);
                    if (chkIsActive.Checked)
                    {
                        string doctorEmailtemplatePath = Server.MapPath(CommonLogic.GetConfigValue("SendDoctorReviewMail")).ToString();
                        reviewEntity.SendDoctorMail(lblPatientEmail.Text, currentSession.EmailId, currentSession.DoctorLastName, currentSession.DoctorFirstName, lblPatientName.Text, currentSession.DoctorName, lblReviewContent.Text, doctorEmailtemplatePath);
                    }
                    Response.Redirect("~/ListReview.aspx");
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
        #endregion
    }
}