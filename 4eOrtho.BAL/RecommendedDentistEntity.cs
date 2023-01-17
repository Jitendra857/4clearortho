using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using _4eOrtho.DAL;
using _4eOrtho.Utility;


namespace _4eOrtho.BAL
{
    public class RecommendedDentistEntity : BaseEntity
    {
        /// <summary>
        /// create object of table
        /// </summary>
        /// <returns></returns>
        public RecommendDentist Create()
        {
            return orthoEntities.RecommendDentists.CreateObject();
        }
        /// <summary>
        /// for save data in reommmendDentist
        /// </summary>
        /// <param name="recmdDentist"></param>
        public void Save(RecommendDentist recmdDentist)
        {
            if (recmdDentist.EntityState == System.Data.EntityState.Detached)
            {
                recmdDentist.CreatedDate = BaseEntity.GetServerDateTime;
                recmdDentist.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToRecommendDentists(recmdDentist);
            }
            else
            {
                recmdDentist.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
        }

        /// <summary>
        /// Get Recommenddentist by id
        /// </summary>
        /// <param name="RecommendedId"></param>
        /// <returns></returns>
        public RecommendDentist GetRecommendedDentistById(int RecommendedId)
        {
            return orthoEntities.RecommendDentists.Where(x => x.RecommendId == RecommendedId).FirstOrDefault();
        }
        /// <summary>
        /// method for bind list.
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<AllRecommendedDentist> GetRecommendedDentistDetail(string sortField, string sortDirection, int pageSize, int pageIndex, string searchField, string searchValue, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));

            List<AllRecommendedDentist> lstGetrecommendeddentistDetail = orthoEntities.GetAllRecommendedDentist(pageIndex, pageSize, sortField, sortDirection, searchField, searchValue, TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstGetrecommendeddentistDetail;
        }

        /// <summary>
        /// Description : Method to delete object
        /// </summary>
        /// <param name="pageDetail"></param>
        public void Delete(RecommendDentist recommendeddentist)
        {
            orthoEntities.DeleteObject(recommendeddentist);
            orthoEntities.SaveChanges();
        }
        /// <summary>
        /// Send recommanded dentist mail to admin
        /// </summary>
        /// <param name="emailaddress"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="city"></param>
        /// <param name="emailtemplatePath"></param>
        public void SendRecommendedDentistMailAdmin(string emailaddress, string firstName, string lastName, string country, string state, string city, string emailtemplatePath)
        {
            if (File.Exists(emailtemplatePath))
            {
                string emailtemplateHTML = File.ReadAllText(emailtemplatePath);
                emailtemplateHTML = emailtemplateHTML.Replace("##FirstName##", firstName);
                emailtemplateHTML = emailtemplateHTML.Replace("##LastName##", lastName);
                emailtemplateHTML = emailtemplateHTML.Replace("##EmailId##", emailaddress);
                emailtemplateHTML = emailtemplateHTML.Replace("##Country##", country);
                emailtemplateHTML = emailtemplateHTML.Replace("##State##", state);
                emailtemplateHTML = emailtemplateHTML.Replace("##City##", city);
                emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", System.DateTime.Now.Year.ToString());
                emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));
                MailAddress fromMailAddress = new MailAddress(emailaddress, firstName + " " + lastName);
                MailAddress toMailAddress = new MailAddress(CommonLogic.GetConfigValue("ToMail"));
                CommonLogic.SendMail(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, "4ClearOrtho - Recommended Doctor");
            }
        }
        /// <summary>
        /// Send recommended dentist mail to doctor
        /// </summary>
        /// <param name="emailaddress"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="PatientName"></param>
        /// <param name="emailtemplatePath"></param>
        public void SendRecommendedDentistMailDoctor(string emailaddress, string firstName, string lastName, string PatientName, string emailtemplatePath)
        {
            if (File.Exists(emailtemplatePath))
            {
                string emailtemplateHTML = File.ReadAllText(emailtemplatePath);
                emailtemplateHTML = emailtemplateHTML.Replace("##FirstName##", firstName);
                emailtemplateHTML = emailtemplateHTML.Replace("##LastName##", lastName);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientName##", PatientName);
                emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", System.DateTime.Now.Year.ToString());
                emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));
                MailAddress fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("ToMail"));
                MailAddress toMailAddress = new MailAddress(emailaddress);
                CommonLogic.SendMail(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, "4ClearOrtho - Recommended Doctor By Patient");
            }
        }
    }
}
