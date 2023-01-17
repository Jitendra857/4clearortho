using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using _4eOrtho.DAL;
using _4eOrtho.Utility;

namespace _4eOrtho.BAL
{
    public class LoginEntity : BaseEntity
    {
        public List<EmailAndPasswordForLogin> Validateuser(string email, string password, string userType)
        {
            return orthoEntities.GetEmailAndPasswordForLogin(email, password, userType).Where(x => x.EmailID.ToLower() == email.ToLower() && x.Password == password).ToList();
        }
        public void SendForgotpasswordMail(string emailaddress, string emailtemplatePath)
        {
            EmailAndPasswordForForGotPassword user = orthoEntities.GetEmailAndPasswordForForGotPassword(emailaddress).Where(x => x.EmailID == emailaddress).FirstOrDefault();
            //User user = new UserEntity().GetUserByEmailAddress(emailaddress);
            if (user != null && user.EmailID != null)
            {
                if (File.Exists(emailtemplatePath))
                {
                    string emailtemplateHTML = File.ReadAllText(emailtemplatePath);
                    emailtemplateHTML = emailtemplateHTML.Replace("##EmailId##", user.EmailID);
                    emailtemplateHTML = emailtemplateHTML.Replace("##FirstName##", user.FirstName);
                    emailtemplateHTML = emailtemplateHTML.Replace("##LastName##", user.LastName);
                    emailtemplateHTML = emailtemplateHTML.Replace("##Password##", Cryptography.DecryptStringAES(user.Password, CommonLogic.GetConfigValue("SharedSecret")));
                    emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));
                    emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", System.DateTime.Now.Year.ToString());
                    MailAddress fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("ToMail"));
                    MailAddress toMailAddress = new MailAddress(user.EmailID);
                    CommonLogic.SendMail(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, "4ClearOrtho - Forgot Password");
                }
            }
        }
        public EmailAndPasswordForForGotPassword GetEmailIdForForgotPassword(string emailAddress)
        {
            return orthoEntities.GetEmailAndPasswordForForGotPassword(emailAddress).Where(x => x.EmailID == emailAddress).FirstOrDefault();
        }
    }

    public class CurrentSession
    {
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string DomainURL { get; set; }
        public string DatabaseName { get; set; }
        public long? PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientMobile { get; set; }
        public string DoctorName { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public long PatientUserId { get; set; }
        public string DoctorMobile { get; set; }
        public string DoctorStreet { get; set; }
        public string DoctorState { get; set; }
        public string DoctorCountry { get; set; }
        public string DoctorCity { get; set; }
        public string DoctorZipcode { get; set; }
        public string RegisterdDate { get; set; }
        public bool IsCertified { get; set; }
        public string SourceType { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string HomeContact { get; set; }
        public string Mobile { get; set; }
        public string WorkContact { get; set; }
        public long CountryId { get; set; }
        public long StateId { get; set; }
        public string MI { get; set; }
        public bool IsPayment { get; set; }
        public bool IsAccActive { get; set; }
    }
}