using System;
using System.Collections.Generic;
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
    public partial class ForgotPassword : PageBase
    {
        #region Declaration

        private ILog logger;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            logger = log4net.LogManager.GetLogger(typeof(ForgotPassword));
        }
        /// <summary>
        /// send forgot password mail to 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SendForgotPasswordmail();
            }
            catch (Exception ex)
            {
                logger.Error("Forgot password retrival process for 4clearOrtho Admin", ex);
            }
        }

        /// <summary>
        /// send forgot password mail
        /// </summary>
        public void SendForgotPasswordmail()
        {
            try
            {
                string forgotEmailTemplatePath = Server.MapPath("\\" + CommonLogic.GetConfigValue("ForgotPasswordmail")).ToString();
                UserEntity userEntity = new UserEntity();
                bool isSuccess = userEntity.SendForgotpasswordMail(txtEmail.Text, forgotEmailTemplatePath);
                if(isSuccess)
                    CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("MailSent").ToString(), divMsg, lblMsg);
                else
                    CommonHelper.ShowMessage(MessageType.ForgotError, this.GetLocalResourceObject("ForgotPaswordErrorMessage").ToString(), divMsg, lblMsg);
                //Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
    }
}