using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;

namespace _4eOrtho
{
    public partial class ForgotPassword : PageBase
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(ForgotPassword));
        #endregion

        #region Events
        /// <summary>
        /// page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (CommonLogic.QueryString("patient").ToLower() == "p")
            //{
            //    btnCancel.PostBackUrl = "~/PatientLogin.aspx";
            //}
            //else if (CommonLogic.QueryString("doctor").ToLower() == "d")
            //{
            //    btnCancel.PostBackUrl = "~/DoctorLogin.aspx";
            //}
            //else
            //{
            //    btnCancel.PostBackUrl = "~/Home.aspx";
            //}
        }

        /// <summary>
        /// send password to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSendPassWord_Click(object sender, EventArgs e)
        {
            try
            {
                SendForgotPasswordmail();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// method to send mail to the user
        /// </summary>
        public void SendForgotPasswordmail()
        {
            try
            {
                string emailaddress = string.Empty;
                string forgotEmailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("ForgotPasswordmail")).ToString();
                LoginEntity loginentity = new LoginEntity();
                EmailAndPasswordForForGotPassword user = loginentity.GetEmailIdForForgotPassword(txtEmailId.Text.Trim());
                //User user = new UserEntity().GetUserByEmailAddress(txtEmailId.Text.Trim());
                if (user != null)
                {
                    loginentity.SendForgotpasswordMail(txtEmailId.Text, forgotEmailTemplatePath);
                    //Response.Redirect("Home.aspx", false);                    
                    CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("MailSent").ToString(), divMsg, lblMsg);
                    txtEmailId.Text = string.Empty;
                }
                else
                {
                    CommonHelper.ShowMessage(MessageType.ForgotError, this.GetLocalResourceObject("ForgotPaswordErrorMessage").ToString(), divMsg, lblMsg);
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