using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _4eOrtho
{
    public partial class PatientLogin : PageBase
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(PatientLogin));
        #endregion

        #region Events
        /// <summary>
        /// page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SessionHelper.CurrentCultureName == "ar-SA" || SessionHelper.CurrentCultureName == "fa-IR")
                    lnk_style.Attributes.Add("href", "Styles/style.ar-SA.css");
                else
                    lnk_style.Attributes.Add("href", "Styles/style.css");
            }
            //string encryptpassword = Cryptography.EncryptStringAES("4edental123", CommonLogic.GetConfigValue("SharedSecret"));
            //string decryptpassword = Cryptography.DecryptStringAES("5TuL2kw34dgGEYznLu47Aw==", CommonLogic.GetConfigValue("SharedSecret"));
        }
        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                LoginEntity loginentity = new LoginEntity();

                List<EmailAndPasswordForLogin> lstEmailAndPasswordForLogin = loginentity.Validateuser(txtEmailId.Text, Cryptography.EncryptStringAES(txtPassword.Text, CommonLogic.GetConfigValue("SharedSecret")), "P").ToList();

                if (lstEmailAndPasswordForLogin.Count > 0)
                {
                    foreach (EmailAndPasswordForLogin empass in lstEmailAndPasswordForLogin)
                    {
                        if (empass.RoleType.Trim() == UserType.P.ToString())
                        {
                            CurrentSession currentSession = new CurrentSession();
                            List<CurrentSession> lstcurrentSession = new List<CurrentSession>();

                            currentSession = new CurrentSession();
                            currentSession.EmailId = empass.EmailID;
                            currentSession.Password = empass.Password;
                            currentSession.UserType = empass.RoleType.Trim();
                            currentSession.DomainURL = empass.DomainURL;
                            //currentSession.DatabaseName = empass.DataBaseName;
                            currentSession.PatientId = empass.PatientId;
                            currentSession.PatientName = empass.PatientName;
                            currentSession.PatientFirstName = empass.FirstName;
                            currentSession.PatientLastName = empass.LastName;
                            currentSession.PatientMobile = empass.Mobile;
                            //currentSession.PatientUserId = Convert.ToInt64(empass.UserID);                            
                            currentSession.RegisterdDate = Convert.ToDateTime(empass.CreatedDate).ToString("MM/dd/yyyy");
                            SessionHelper.LoggedUserType = UserType.P.ToString();
                            SessionHelper.LoggedUserEmailAddress = empass.EmailID;
                            SessionHelper.PatientId = empass.PatientId;
                            Session["IsLoggedIn"] = UserType.P.ToString();
                            //lstcurrentSession.Add(currentSession);
                            //Session["LoginSession"] = lstcurrentSession;
                            Session["UserLoginSession"] = currentSession;
                            Session["PatientId"] = empass.PatientId;
                            PageRedirect("Welcome.aspx");
                            break;
                        }
                        else
                        {
                            CommonHelper.ShowMessage(MessageType.ForgotError, this.GetLocalResourceObject("EmailIdPasswordErrorMessage").ToString(), divMsg, lblMsg);
                        }
                    }
                }
                else
                {
                    CommonHelper.ShowMessage(MessageType.ForgotError, this.GetLocalResourceObject("EmailIdPasswordErrorMessage").ToString(), divMsg, lblMsg);
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