using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;

namespace _4eOrtho.Admin
{
    public partial class AddEditReviewManagement : PageBase
    {
        #region Declaration
        long reviewId = 0;
        private ILog logger = log4net.LogManager.GetLogger(typeof(AddEditReviewManagement));
        ReviewByReviewId reviewDetails = new ReviewByReviewId();
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(CommonLogic.QueryString("Id")))
                    reviewId = Convert.ToInt64(CommonLogic.QueryString("Id"));

                if (!Page.IsPostBack)
                {
                    DoctorReviewBind(reviewId);
                    if (reviewId > 0)
                    {
                        lblHeader.Text = "Edit Doctor Review";
                        Page.Title = "Admin - Edit Doctor Review";
                    }
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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {


                ReviewEntity reviewEntity = new ReviewEntity();
                Review review = reviewEntity.GetReviewById(reviewId);
                review.IsActive = chkActive.Checked;
                reviewEntity.Save(review);
                if (chkActive.Checked)
                {
                    DoctorEntity entity = new DoctorEntity();
                    AllDoctorDetailsByEmailId doctorDetails = new AllDoctorDetailsByEmailId();
                    doctorDetails = entity.GetDoctorDetailsByEmailId(review.DoctorEmail);
                    string doctorEmailtemplatePath = Server.MapPath(CommonLogic.GetConfigValue("SendDoctorReviewMail")).ToString();
                    reviewEntity.SendDoctorMail(lblPatientEmailData.Text, review.DoctorEmail, doctorDetails.LastName, doctorDetails.FirstName, lblPatientNameData.Text, doctorDetails.DoctorName, lblReviewContentData.Text, doctorEmailtemplatePath);
                }
                Response.Redirect("~/Admin/ListReviewManagement.aspx");
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
            if (reviewDetails != null)
            {
                lblPatientNameData.Text = reviewDetails.PatientName;
                lblPatientEmailData.Text = reviewDetails.PatientEmail;
                lblReviewContentData.Text = reviewDetails.ReviewContent;
                lblDoctorEmailData.Text = reviewDetails.DoctorEmail;
                chkActive.Checked = reviewDetails.IsActive;
            }
        }        
        #endregion
    }
}