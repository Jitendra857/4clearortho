using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.Utility;
using _4eOrtho.DAL;
using log4net;
using _4eOrtho.Helper;
namespace _4eOrtho.Admin
{
    public partial class Login : PageBase
    {
        #region GLOBAL DECLARATION
        private ILog logger;
        #endregion

        #region Events
        /// <summary>
        /// intialize logger object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string pwd = Cryptography.DecryptStringAES("CAj3UNiGwmPgXN5xuG0zcw==", CommonLogic.GetConfigValue("SharedSecret"));
            string SuprAdminpwd = Cryptography.DecryptStringAES("olPyR8ktb7WkCjh8L4xIRQ==", CommonLogic.GetConfigValue("SharedSecret"));
            logger = log4net.LogManager.GetLogger(typeof(Login));
        }
        /// <summary>
        /// admin login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // int i = 0;
                // decimal g = 5 / i;
                string asd = Cryptography.DecryptStringAES("olPyR8ktb7WkCjh8L4xIRQ==", CommonLogic.GetConfigValue("SharedSecret"));
                //string asd = Cryptography.DecryptStringAES("CAj3UNiGwmPgXN5xuG0zcw==",CommonLogic.GetConfigValue("SharedSecret"));
                if (Authentication.UserLoginProcess(txtEmailAddress.Text,
                        Cryptography.EncryptStringAES(txtPassword.Text, CommonLogic.GetConfigValue("SharedSecret")), false, false))
                {
                    if (SessionHelper.LoggedUserType == UserType.SuperAdmin.ToString())
                        Response.Redirect("ListContentPage.aspx", false);
                    else
                        Response.Redirect("ListNewCaseDetails.aspx", false);
                    return;
                }
                else
                {
                    CommonHelper.ShowMessage(MessageType.ForgotError, "The username or password entered does not match our records. Please review your login details or click on the ‘Forgot password’ link to retrieve your password.", divMsg, lblMsg);
                    lblMsg.Visible = true;
                    //lblMsg.Text = "Emailid Or Password is not exist..Try again.";
                }
            }
            catch (Exception ex)
            {
                logger.Error("Login process for 4clearOrtho Admin", ex);
            }
        }
        #endregion
    }
}