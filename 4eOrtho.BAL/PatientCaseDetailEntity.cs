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
    public class PatientCaseDetailEntity : BaseEntity
    {
        public List<NewCaseDetails> GetNewCaseDetails(int pageIndex, int pageSize, string sortField, string sortDirection, string searchField, string searchValue, string emailId, bool isCompleted, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));
            List<NewCaseDetails> lstNewCaseDetails = orthoEntities.GetNewCaseDetails(pageIndex, pageSize, sortField, sortDirection, searchField, searchValue, emailId, isCompleted, TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstNewCaseDetails;
        }
        /// <summary>
        /// for save data in become provider
        /// </summary>
        /// <param name="recmdDentist"></param>
        public long Save(PatientCaseDetail patientcase)
        {
            if (patientcase.EntityState == System.Data.EntityState.Detached)
            {
                patientcase.CreatedDate = BaseEntity.GetServerDateTime;
                patientcase.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToPatientCaseDetails(patientcase);
            }
            else
            {
                patientcase.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
            return patientcase.CaseId;
        }
        /// <summary>
        /// create instance of become provider
        /// </summary>
        /// <returns></returns>
        public PatientCaseDetail Create()
        {
            return orthoEntities.PatientCaseDetails.CreateObject();
        }
        public PatientCaseDetail GetPatientCaseById(long caseId)
        {
            return orthoEntities.PatientCaseDetails.Where(x => x.CaseId == caseId).FirstOrDefault();
        }
        public PatientCaseEmailDetail GetPatientCaseEmailDetail(long lCaseId)
        {
            return orthoEntities.GetPatientCaseEmailDetail(lCaseId).FirstOrDefault();
        }
        public bool IsPatientCaseCashPaid(string caseno)
        {
            bool IsCashPaid = (from q in orthoEntities.PatientCaseDetails
                               join p in orthoEntities.PaymentSuccesses on q.CaseId equals p.CaseId
                               where q.CaseNo.Trim() == caseno.Trim()
                               select p.IsCashPayed).FirstOrDefault();
            return IsCashPaid;
        }
        public List<AllDoctorByNewCase> GetDoctorListByNewCase()
        {
            return orthoEntities.GetAllDoctorByNewCase().ToList();
        }
        public static void PatientCaseDetailsMail(string firstName, string lastName, string doctorfirstName, string doctorlastName, string caseNo, string trackNo, string emailTemplatePath, string doctorEmailId, string filePaths)
        {
            if (File.Exists(emailTemplatePath))
            {
                string emailtemplateHTML = File.ReadAllText(emailTemplatePath);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorFirstName##", doctorfirstName);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorLastName##", doctorlastName);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientFirstName##", firstName);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientLastName##", lastName);
                emailtemplateHTML = emailtemplateHTML.Replace("##CaseNo##", caseNo);
                emailtemplateHTML = emailtemplateHTML.Replace("##TrackNo##", trackNo);
                //emailtemplateHTML = emailtemplateHTML.Replace("##OrthoSystem##", orthoSystem);
                //emailtemplateHTML = emailtemplateHTML.Replace("##OrthoCondition##", orthoCondition);
                //emailtemplateHTML = emailtemplateHTML.Replace("##OtherCondition##", otherCondition);
                //emailtemplateHTML = emailtemplateHTML.Replace("##Notes##", notes);
                emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));
                emailtemplateHTML = emailtemplateHTML.Replace("##SiteURL##", CommonLogic.GetSiteURL() + @"Home.aspx");
                emailtemplateHTML = emailtemplateHTML.Replace("##TrackCaseUrl##", CommonLogic.GetSiteURL());
                emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", DateTime.Today.Year.ToString());
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorLogin##", CommonLogic.GetSiteURL() + @"DoctorLogin.aspx");


                MailAddress fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("FromNoReplyEmail"), CommonLogic.GetConfigValue("FromNoReplyName"));
                MailAddress toMailAddress = new MailAddress(doctorEmailId, doctorfirstName + " " + doctorlastName);
                CommonLogic.SendMailWithAttachment(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, "4ClearOrtho – New Case Created", filePaths, "4ClearOrtho_New_Case_Details");
                //CommonLogic.SendMail(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, "4ClearOrtho – New Case Created");
            }
        }
        public List<GetCaseAndTrackDetailForSA_Test_Result> GetCaseAndTrackDetailForSA(int pageIndex, int pageSize, string sortField, string sortDirection, string searchField, string searchValue, bool isDiscounted, bool isCompleted, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));
           List<GetCaseAndTrackDetailForSA_Test_Result> lstCaseDetails = orthoEntities.GetCaseAndTrackDetailForSA_Test(pageIndex, pageSize, sortField, sortDirection, searchField, searchValue, isDiscounted, isCompleted, TotalRecCount).ToList();


            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstCaseDetails;
        }
        public List<GetAllCaseWithDiscounted> GetAllCaseWithDiscounted(int pageIndex, int pageSize, string sortField, string sortDirection, string searchField, string searchValue, string doctorEmail, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));
            List<GetAllCaseWithDiscounted> lstCaseDetails = orthoEntities.GetAllCaseWithDiscounted(pageIndex, pageSize, sortField, sortDirection, searchField, searchValue, doctorEmail, TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstCaseDetails;
        }
        public PatientCaseDetail GetCaseByCaseNo(string caseNo)
        {
            return orthoEntities.PatientCaseDetails.Where(x => x.CaseNo.Contains(caseNo)).OrderByDescending(x => x.CaseId).FirstOrDefault();
        }
    }
}
