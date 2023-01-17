using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4eOrtho.DAL;
using System.IO;
using _4eOrtho.Utility;
using System.Net.Mail;
using System.Data.Objects;

namespace _4eOrtho.BAL
{
    public class UserDomainMaster
    {
        //For UserDomainMaster
        public string EMailId { get; set; }
        public long UserID { get; set; }
        public string Password { get; set; }
        public long SourceId { get; set; }
        public Boolean IsUpdate { get; set; }
        public string RoleType { get; set; }
        public string DomainURL { get; set; }
        //public string RoleType { get; set; }
        //public string DomainURL { get; set; }
        //public bool ISAutoLogin { get; set; }
        //public string UserType { get; set; }
        //public string RequestId { get; set; }
        public string SourceType { get; set; }
        public string DatabaseName { get; set; }
    }
    public class PatientEntity : BaseEntity
    {
        public void InsertToUserDomainMaster(UserDomainMaster user)
        {
            orthoEntities.InsertPatientToUserDomain(user.EMailId, user.UserID, user.Password, user.SourceId, user.IsUpdate, user.RoleType, user.DomainURL, user.SourceType, user.DatabaseName);
        }

        /// <summary>
        /// for save data in become provider
        /// </summary>
        /// <param name="recmdDentist"></param>
        public long Save(Patient patient)
        {
            if (patient.EntityState == System.Data.EntityState.Detached)
            {
                patient.CreatedDate = BaseEntity.GetServerDateTime;
                patient.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToPatients(patient);
            }
            else
            {
                patient.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
            return patient.PatientId;
        }
        /// <summary>
        /// create instance of become provider
        /// </summary>
        /// <returns></returns>
        public Patient Create()
        {
            return orthoEntities.Patients.CreateObject();
        }
        public Patient GetPatientById(long patientId)
        {
            return orthoEntities.Patients.Where(x => x.PatientId == patientId).FirstOrDefault();
        }

        public List<Patient> GetAllPatient()
        {
            return orthoEntities.Patients.Where(p => p.IsActive == true).ToList();
        }
        public List<PatientListByDoctorEmail> GetAllPatientByDoctorEmail(string sEmail)
        {
            return orthoEntities.GetPatientListByDoctorEmail(sEmail).ToList();
        }
        public Patient GetPatientByEmail(string sEmail)
        {
            return orthoEntities.Patients.Where(x => x.EmailId.Trim() == sEmail.Trim()).FirstOrDefault();
        }
        public PatientRegistrationDetailForMail GetPatientRegistrationDetailForEmail(long lPatientId)
        {
            return orthoEntities.GetPatientRegistrationDetailForMail(lPatientId).FirstOrDefault();
        }
        public static void PatientRegistrationMail(string firstName, string lastName, string password, string emailTemplatePath, string emailId, string userType, string templateName, string DoctorEmailId, string DoctorFirstName, string DoctorLastName)
        {
            if (File.Exists(emailTemplatePath))
            {
                string emailtemplateHTML = File.ReadAllText(emailTemplatePath);
                emailtemplateHTML = emailtemplateHTML.Replace("##FirstName##", firstName);
                emailtemplateHTML = emailtemplateHTML.Replace("##LastName##", lastName);
                emailtemplateHTML = emailtemplateHTML.Replace("##UserType##", userType);
                emailtemplateHTML = emailtemplateHTML.Replace("##EmailId##", emailId);
                emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));
                emailtemplateHTML = emailtemplateHTML.Replace("##Password##", password);
                emailtemplateHTML = emailtemplateHTML.Replace("##SiteURL##", CommonLogic.GetSiteURL() + @"Home.aspx");
                emailtemplateHTML = emailtemplateHTML.Replace("##EditProfileUrl##", CommonLogic.GetSiteURL());
                emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", DateTime.Today.Year.ToString());
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientLogin##", CommonLogic.GetSiteURL() + @"PatientLogin.aspx");


                MailAddress fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("FromNoReplyEmail"), CommonLogic.GetConfigValue("FromNoReplyName"));
                MailAddress toMailAddress = new MailAddress(emailId, firstName + " " + lastName);
                //MailAddress ccMailAddress = new MailAddress(DoctorEmailId, DoctorFirstName + " " + DoctorLastName);
                if (templateName.Equals("Registration"))
                {
                    emailtemplateHTML = emailtemplateHTML.Replace("##ByDoctor##", "");
                    CommonLogic.SendMail(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, "4ClearOrtho – Welcome User");
                }
                else if (templateName.Equals("RegistrationByDoctor"))
                {
                    emailtemplateHTML = emailtemplateHTML.Replace("##ByDoctor##", "By " + DoctorFirstName + " " + DoctorLastName);
                    CommonLogic.SendMail(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, "4ClearOrtho – Welcome User");
                }
            }
        }
        public static void ChangePasswordMail(string firstName, string lastName, string password, string emailTemplatePath, string emailId)
        {
            if (File.Exists(emailTemplatePath))
            {
                string emailtemplateHTML = File.ReadAllText(emailTemplatePath);
                emailtemplateHTML = emailtemplateHTML.Replace("##FirstName##", firstName);
                emailtemplateHTML = emailtemplateHTML.Replace("##LastName##", lastName);
                emailtemplateHTML = emailtemplateHTML.Replace("##EmailId##", emailId);
                emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));
                emailtemplateHTML = emailtemplateHTML.Replace("##Password##", password);
                emailtemplateHTML = emailtemplateHTML.Replace("##SiteURL##", CommonLogic.GetSiteURL() + @"Home.aspx");
                emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", DateTime.Today.Year.ToString());

                MailAddress fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("FromNoReplyEmail"), CommonLogic.GetConfigValue("FromNoReplyName"));
                MailAddress toMailAddress = new MailAddress(emailId, firstName + " " + lastName);
                CommonLogic.SendMail(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, "4ClearOrtho – Login Credentials Changed");
            }
        }

        /// <summary>
        /// getting all the patients and doctor 
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public List<GetAllPatientOrDoctor> GetAllPatientOrDoctor(string sortField, string sortDirection, int pageSize, int pageIndex, string searchField, string searchText, out int totalRecords, string userType,string DoctorEmailId)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));
            List<GetAllPatientOrDoctor> lstAllReviewDetail = orthoEntities.GetAllPatientOrDoctor(pageIndex, pageSize, sortField, sortDirection, searchField, searchText, userType, DoctorEmailId, TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstAllReviewDetail;
        }
    }
}
