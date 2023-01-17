using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Web.UI;

namespace _4eOrtho
{
    public partial class AddClientTestimonial : PageBase
    {
        #region Declaration
        int ClientTestimoialId = 0;
        private ILog logger = log4net.LogManager.GetLogger(typeof(AddClientTestimonial));
        #endregion

        #region Events
        /// <summary>
        /// page redirect based on role on page load
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
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }

        }

        /// <summary>
        /// save client testimonail 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    ClientTestimonial clienttestimonial = new ClientTestimonial();
                    ClientTestimonialEntity clienttestimonialentity = new ClientTestimonialEntity();
                    CurrentSession currentSession = new CurrentSession();
                    currentSession = (CurrentSession)Session["UserLoginSession"];
                    if (ClientTestimoialId > 0)
                    {
                        clienttestimonial = clienttestimonialentity.GetClientTestimonialByID(ClientTestimoialId);
                        clienttestimonial.LastUpdatedDate = BaseEntity.GetServerDateTime;
                        clienttestimonial.LastUpdatedBy = 1;
                    }
                    else
                    {
                        clienttestimonial = clienttestimonialentity.Create();
                        clienttestimonial.CreatedDate = BaseEntity.GetServerDateTime;
                        clienttestimonial.CreatedBy = 1;
                    }
                    clienttestimonial.FirstName = txtFirstName.Text.Trim();
                    clienttestimonial.LastName = txtLastname.Text.Trim();
                    clienttestimonial.Email = txtEmail.Text.Trim();
                    clienttestimonial.PageContent = txtPageContent.Text.Trim();
                    clienttestimonial.IsActive = false;
                    clienttestimonial.IsDelete = false;
                    clienttestimonial.UserType = currentSession.UserType == "S" ? "D" : currentSession.UserType;
                    clienttestimonial.UserEmail = currentSession.EmailId;
                    clienttestimonialentity.Save(clienttestimonial);
                    logger.Info("Testimonial Saved Successfully");
                    CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("ClientTestimonialSave").ToString(), divMsg, lblMsg);
                    ClearControls();
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
        /// clear text box 
        /// </summary>
        private void ClearControls()
        {
            txtFirstName.Text = string.Empty;
            txtLastname.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPageContent.Text = string.Empty;
        }
        #endregion
    }
}