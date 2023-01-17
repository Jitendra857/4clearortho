using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho
{
    public partial class ChangePassword : PageBase
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(ChangePassword));
        CurrentSession currentSession;
        long lPatientId = 0;
        #endregion

        #region Event

        /// <summary>
        /// Page Load Event
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
                currentSession = (CurrentSession)Session["UserLoginSession"];
                if (currentSession != null)
                {
                    hdnPassword.Value = currentSession.Password;
                    lPatientId = Convert.ToInt64(currentSession.PatientId);
                }
                txtPassword.Attributes.Add("oncopy", "return false;");
                txtConfirmPassword.Attributes.Add("oncopy", "return false;");
                txtPassword.Attributes.Add("onpaste", "return false;");
                txtConfirmPassword.Attributes.Add("onpaste", "return false;");
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Event to submit new password record.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    SaveChangePassword();
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Event to validated old password.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void customtxtOldPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                args.IsValid = (Cryptography.DecryptStringAES(hdnPassword.Value, CommonLogic.GetConfigValue("SharedSecret")).Equals(txtOldPassword.Text));
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Event to validated old password and new password.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void customtxtPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                args.IsValid = !(txtOldPassword.Text.Trim().Equals(txtPassword.Text.Trim()));
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        #endregion

        #region Helper

        /// <summary>
        /// Method to save change password.
        /// </summary>
        private void SaveChangePassword()
        {
            try
            {
                CurrentSession currentSession = ((CurrentSession)Session["UserLoginSession"]);
                //PatientRegistrationDetailForMail patientDetail;
                //PatientEntity patientEntity = new PatientEntity();
                if (currentSession != null)
                {
                    //patientDetail = new PatientEntity().GetPatientRegistrationDetailForEmail(lPatientId);

                    User user = new UserEntity().GetUserByEmailAndUserType(currentSession.EmailId, currentSession.UserType);

                    if (user != null)
                    {
                        user.Password = Cryptography.EncryptStringAES(txtPassword.Text, CommonLogic.GetConfigValue("SharedSecret"));
                        new UserEntity().Save(user);
                        //UserDomainMaster user = new UserDomainMaster();
                        //user.EMailId = patientDetail.EmailAddress;
                        //user.IsUpdate = true;
                        //user.Password = Cryptography.EncryptStringAES(txtPassword.Text, CommonLogic.GetConfigValue("SharedSecret"));
                        //user.SourceId = Convert.ToInt64(lPatientId);
                        //user.UserID = Convert.ToInt64(patientDetail.PatientId);
                        //user.SourceType = "Ortho";
                        //user.DatabaseName = "4ClearOrtho";
                        //patientEntity.InsertToUserDomainMaster(user);

                        string emailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("ChangePasswordTemplatePath")).ToString();
                        //patientDetail = new PatientEntity().GetPatientRegistrationDetailForEmail(Convert.ToInt64(lPatientId));
                        if (user != null)
                        {
                            string sPassword = Cryptography.DecryptStringAES(user.Password, CommonLogic.GetConfigValue("SharedSecret"));
                            PatientEntity.ChangePasswordMail(user.FirstName, user.LastName, sPassword, emailTemplatePath, user.EmailAddress);
                        }
                        CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("PasswordChangeSuccess").ToString(), divMsg, lblMsg);
                    }
                    else
                    {
                        CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("CannotChangePassword").ToString(), divMsg, lblMsg);
                    }
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