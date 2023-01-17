using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4eOrtho.DAL;
using System.Data.Objects;
using System.IO;
using _4eOrtho.Utility;
using System.Net.Mail;

namespace _4eOrtho.BAL
{
    public class TrackCaseEntity : BaseEntity
    {
        /// <summary>
        /// for save data in become provider
        /// </summary>
        /// <param name="recmdDentist"></param>
        public long Save(TrackCase trackCase)
        {
            if (trackCase.EntityState == System.Data.EntityState.Detached)
            {
                trackCase.CreatedDate = BaseEntity.GetServerDateTime;
                trackCase.UpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToTrackCases(trackCase);
            }
            else
            {
                trackCase.UpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
            return trackCase.TrackId;
        }
        /// <summary>
        /// create instance of become provider
        /// </summary>
        /// <returns></returns>
        public TrackCase Create()
        {
            return orthoEntities.TrackCases.CreateObject();
        }
        public TrackCase GetTrackCaseById(long trackId)
        {
            return orthoEntities.TrackCases.Where(x => x.TrackId == trackId).FirstOrDefault();
        }
        public TrackCase GetTrackCaseByCaseId(long caseId)
        {
            return orthoEntities.TrackCases.Where(x => x.CaseId == caseId).FirstOrDefault();
        }

        public List<TrackCaseListDetails> getTrackCaseListDetails(int pageIndex, int pageSize, string sortBy, string sortOrder, string searchField, string searchValue, string emailId, out int totalRecCount)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));
            List<TrackCaseListDetails> lstTrackCase = orthoEntities.GetTrackCaseListDetails(pageIndex, pageSize, sortBy, sortOrder, searchField, searchValue, emailId, TotalRecCount).ToList();
            totalRecCount = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstTrackCase;
        }

        public TrackEmailDetails GetTrackEmailDetails(long lTrackId)
        {
            return orthoEntities.GetTrackEmailDetails(lTrackId).FirstOrDefault();
        }

        public static void SendMailOnUpdateStatus(string emailTemplatePath, string firstName, string lastName, string caseNo, string OrderStatus, string UpdatedBy, string UpdatedDate, string Email, string Description)
        {
            try
            {
                if (File.Exists(emailTemplatePath))
                {
                    string emailtemplateHTML = File.ReadAllText(emailTemplatePath);
                    emailtemplateHTML = emailtemplateHTML.Replace("##FirstName##", firstName);
                    emailtemplateHTML = emailtemplateHTML.Replace("##LastName##", lastName);
                    emailtemplateHTML = emailtemplateHTML.Replace("##CaseNo##", caseNo);
                    emailtemplateHTML = emailtemplateHTML.Replace("##OrderStatus##", OrderStatus);
                    emailtemplateHTML = emailtemplateHTML.Replace("##UpdatedBy##", UpdatedBy);
                    emailtemplateHTML = emailtemplateHTML.Replace("##UpdatedDate##", UpdatedDate);
                    emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));
                    emailtemplateHTML = emailtemplateHTML.Replace("##SiteURL##", CommonLogic.GetSiteURL() + @"Home.aspx");
                    emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", DateTime.Today.Year.ToString());
                    if (!string.IsNullOrEmpty(Description))
                        emailtemplateHTML = emailtemplateHTML.Replace("##Description##", Description);
                    else
                        emailtemplateHTML = emailtemplateHTML.Replace("##style##", "style='display:none;'");


                    MailAddress fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("FromNoReplyEmail"), CommonLogic.GetConfigValue("FromNoReplyName"));
                    MailAddress toMailAddress = new MailAddress(Email, firstName + " " + lastName);
                    MailAddress bccMailAddress = new MailAddress(CommonLogic.GetConfigValue("FromNoReplyEmail"), CommonLogic.GetConfigValue("FromNoReplyName"));
                    CommonLogic.SendMail(fromMailAddress, toMailAddress, null, bccMailAddress, emailtemplateHTML, "4ClearOrtho – Track Status");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<usp_Admin_GetAllTrackHistory_Result> GetAllTrackHistory(string trackNo)
        {
            return orthoEntities.usp_Admin_GetAllTrackHistory(trackNo).ToList();
        }
    }
}
