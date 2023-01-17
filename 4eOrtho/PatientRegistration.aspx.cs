using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho
{
    public partial class PatientRegistration : PageBase
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(PatientRegistration));
        #endregion

        #region Event
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                    SavePatient();
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void cvtxtEmail_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {                
                if (new UserEntity().GetExistUserIdByEmailAddressAndID(txtEmail.Text.Trim(), 0, "P") > 0)
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        #endregion

        #region Helper
        private void SavePatient()
        {
            try
            {
                long patientId = 0;
                TransactionScope scope = new TransactionScope();
                using (scope)
                {
                    PatientEntity patientEntity = new PatientEntity();
                    Patient patient = patientEntity.Create();
                    patient.EmailId = txtEmail.Text;
                    patient.BirthDate = Convert.ToDateTime(txtDateofBirth.Text);
                    patient.CreatedBy = 0;
                    patient.FirstName = txtFirstName.Text;
                    patient.LastName = txtLastName.Text;
                    patient.LastUpdatedBy = 0;
                    patient.IsActive = true;

                    if (rbtnFemale.Checked)
                        patient.Gender = "F";
                    else
                        patient.Gender = "M";
                    patient.IsActive = true;
                    patientId = patientEntity.Save(patient);

                    User user = new UserEntity().Create();
                    user.CreatedDate = DateTime.Now;
                    user.EmailAddress = txtEmail.Text.Trim();
                    user.FirstName = txtFirstName.Text.Trim();
                    user.IsActive = true;
                    user.IsSuperAdmin = false;
                    user.LastName = txtLastName.Text.Trim();
                    user.Password = Cryptography.EncryptStringAES(txtPassword.Text, CommonLogic.GetConfigValue("SharedSecret"));
                    user.UpdatedDate = DateTime.Now;
                    user.UserType = "P";
                    new UserEntity().Save(user);

                    //UserDomainMaster user = new UserDomainMaster();
                    //user.EMailId = txtEmail.Text;
                    //user.Password = Cryptography.EncryptStringAES(txtPassword.Text, CommonLogic.GetConfigValue("SharedSecret"));
                    //user.SourceId = patientId;
                    //user.UserID = patientId;
                    //user.RoleType = "P";
                    //user.IsUpdate = false;
                    //user.DomainURL = Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf('/'));
                    //user.SourceType = "Ortho";
                    //user.DatabaseName = "4ClearOrtho";
                    //patientEntity.InsertToUserDomainMaster(user);
                    scope.Complete();
                }
                SendMailOnRegistrationComplete(patientId);
                Response.Redirect("PatientLogin.aspx");
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
      
        private void SendMailOnRegistrationComplete(long lPatientId)
        {
            try
            {
                PatientRegistrationDetailForMail patientDetail = new PatientEntity().GetPatientRegistrationDetailForEmail(lPatientId);
                if (patientDetail != null)
                {
                    string emailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("PatientRegistrationMail")).ToString();
                    string sPassword = Cryptography.DecryptStringAES(patientDetail.Password, CommonLogic.GetConfigValue("SharedSecret"));
                    string userType = string.Empty;
                    if (patientDetail.UserType == "P")
                        userType = "Patient";
                    PatientEntity.PatientRegistrationMail(patientDetail.FirstName, patientDetail.LastName, sPassword, emailTemplatePath, patientDetail.EmailId, userType, "Registration", "", "", "");
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