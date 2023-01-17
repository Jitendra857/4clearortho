using System;
using System.Collections.Generic;
using System.Linq;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
namespace _4eOrtho
{
    public partial class DoctorLogin : PageBase
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(DoctorLogin));
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
            Session["lstFileList"] = null;
            //string decryptpassword = Cryptography.DecryptStringAES("wa9vTnQNZcKBU0mrYPFZ0g==", CommonLogic.GetConfigValue("SharedSecret"));
            //  string xy = Cryptography.DecryptStringAES("TDmM0ZTb/+jp5/xhoFxdig==",CommonLogic.GetConfigValue("SharedSecret"));            
            if (SessionHelper.IsAbleToNavigate && !string.IsNullOrEmpty(SessionHelper.LoggedUserEmailAddress))
            {
                WSB_UserDomainMaster user = new DoctorEntity().GetUserDomain(SessionHelper.LoggedUserEmailAddress);
                if (user != null)
                {
                    UserLoginProcess(user.EmailID, Cryptography.DecryptStringAES(user.Password, CommonLogic.GetConfigValue("SharedSecret")));
                }
            }
        }

        /// <summary>
        /// doctor login using email and password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                UserLoginProcess(txtEmailId.Text, txtPassword.Text);
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        private void UserLoginProcess(string emailId, string password)
        {
            LoginEntity loginentity = new LoginEntity();
            List<EmailAndPasswordForLogin> lstEmailAndPasswordForLogin = loginentity.Validateuser(emailId, Cryptography.EncryptStringAES(password, CommonLogic.GetConfigValue("SharedSecret")), "D").ToList();
            if (lstEmailAndPasswordForLogin.Count > 0)
            {
                foreach (EmailAndPasswordForLogin empass in lstEmailAndPasswordForLogin)
                {

                    if (empass.RoleType.Trim() == UserType.D.ToString() || empass.RoleType.Trim() == UserType.S.ToString())
                    {
                        if (empass.SourceType == "AAAD" && string.IsNullOrEmpty(empass.CertiType) && string.IsNullOrEmpty(empass.Title))
                        {
                            CommonHelper.ShowMessage(MessageType.ForgotError, "You are not a certified doctor.", divMsg, lblMsg);
                        }
                        else if (empass.SourceType == "AAAD" && !string.IsNullOrEmpty(empass.CertiType) && empass.Title.ToLower() != "4clearortho")
                        {
                            CommonHelper.ShowMessage(MessageType.ForgotError, "You are not a certified doctor.", divMsg, lblMsg);
                        }
                        else
                        {
                            CurrentSession currentSession = new CurrentSession();
                            currentSession.EmailId = empass.EmailID;
                            currentSession.Password = empass.Password;
                            currentSession.UserType = empass.RoleType.Trim();
                            currentSession.DomainURL = empass.DomainURL;
                            //currentSession.DatabaseName = empass.DataBaseName;
                            currentSession.DoctorName = empass.DoctorName;
                            currentSession.DoctorFirstName = empass.DoctorFirstName;
                            currentSession.DoctorLastName = empass.DoctorLastName;
                            currentSession.DoctorMobile = empass.DoctorMobile;
                            currentSession.DoctorState = empass.StateName;
                            currentSession.DoctorStreet = empass.DoctorStreet;
                            currentSession.DoctorCountry = empass.CountryName;
                            currentSession.DoctorCity = empass.City;
                            currentSession.DoctorZipcode = empass.ZipCode;
                            currentSession.IsCertified = !string.IsNullOrEmpty(empass.Title) && empass.Title.ToLower() == "4clearortho" ? true : false;
                            currentSession.SourceType = empass.SourceType;
                            currentSession.Gender = empass.Gender;
                            currentSession.DOB = empass.DOB != null ? Convert.ToString(empass.DOB) : Convert.ToString(DateTime.Now);
                            currentSession.HomeContact = empass.HomeContact;
                            currentSession.MI = empass.MI;
                            currentSession.StateId = Convert.ToInt64(empass.StateId);
                            currentSession.WorkContact = empass.WorkContact;
                            currentSession.CountryId = Convert.ToInt64(empass.CountryId);
                            currentSession.IsPayment = Convert.ToBoolean(empass.IsPayment);
                            currentSession.IsAccActive = Convert.ToBoolean(empass.IsAccActive);
                            SessionHelper.LoggedUserType = UserType.D.ToString();
                            Session["IsLoggedIn"]= UserType.D.ToString();
                            //lstcurrentSession.Add(currentSession);                            
                            //Session["LoginSession"] = lstcurrentSession;

                            UserConfigEntity userConfigEntity = new UserConfigEntity();
                            UserConfig userConfig = userConfigEntity.GetUserByEmailAddress(currentSession.EmailId);
                            if (userConfig != null)
                            {
                                if (!string.IsNullOrEmpty(userConfig.CertificateFileName) && userConfig.IsAccountActivated)
                                {
                                    currentSession.IsCertified = true;
                                }
                            }
                            Session["UserLoginSession"] = currentSession;

                            if (Convert.ToBoolean(empass.IsPayment))
                                Response.Redirect("Welcome.aspx", false);
                            else
                                Response.Redirect("Payment.aspx");
                            break;
                        }
                    }
                    else
                    {
                        CommonHelper.ShowMessage(MessageType.ForgotError, this.GetLocalResourceObject("EmailPasswordWrongError").ToString(), divMsg, lblMsg);
                    }
                }
            }
            else
            {
                CommonHelper.ShowMessage(MessageType.ForgotError, this.GetLocalResourceObject("EmailPasswordWrongError").ToString(), divMsg, lblMsg);
            }
        }

        #endregion
    }
}
