using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4eOrtho.Utility;
using _4eOrtho.DAL;
using System.Data.Objects;
using System.IO;
using System.Net.Mail;

namespace _4eOrtho.BAL
{
    public class ContactUsEntity : BaseEntity
    {

        public ContactU Create()
        {
            return orthoEntities.ContactUs.CreateObject();
        }

        /// <summary>
        /// for save data in reommmendDentist
        /// </summary>
        /// <param name="recmdDentist"></param>
        public void Save(ContactU contactus)
        {
            if (contactus.EntityState == System.Data.EntityState.Detached)
            {
                contactus.CreatedDate = BaseEntity.GetServerDateTime;
                contactus.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToContactUs(contactus);
            }
            else
            {
                contactus.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
        }


        public ContactU GetContactIdById(int contactid)
        {
            return orthoEntities.ContactUs.Where(x => x.ContactId == contactid).FirstOrDefault();
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
        public List<AllContactUsList> GetallContactUsDetail(string sortField, string sortDirection, int pageSize, int pageIndex, string searchField, string searchValue, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));

            List<AllContactUsList> lstGetallContactUsDetail = orthoEntities.GetAllContactUsList(pageIndex, pageSize, sortField, sortDirection, searchField, searchValue, TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstGetallContactUsDetail;
        }

        public void SendAdminMail(string emailaddress,string subject,string name,string comment, string emailtemplatePath)
        {
            //ContactU contactus = orthoEntities.ContactUs.Where(x => x.Email == emailaddress).SingleOrDefault(); 
            //if (contactus.Email != null && contactus != null)
            //{
                if (File.Exists(emailtemplatePath))
                {
                    string emailtemplateHTML = File.ReadAllText(emailtemplatePath);
                    emailtemplateHTML = emailtemplateHTML.Replace("##Subject##", subject);
                    emailtemplateHTML = emailtemplateHTML.Replace("##Name##", name);
                    emailtemplateHTML = emailtemplateHTML.Replace("##EmailId##", emailaddress);
                    emailtemplateHTML = emailtemplateHTML.Replace("##Comment##", comment);
                    emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", System.DateTime.Now.Year.ToString());
                    emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));
                    MailAddress fromMailAddress = new MailAddress(emailaddress, name);
                    MailAddress toMailAddress = new MailAddress(CommonLogic.GetConfigValue("ToMail"));
                    CommonLogic.SendMail(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, "4ClearOrtho - New Inquiry");
                }
            //}
        }
        public void SendVisitorMail(string emailaddress,string name, string emailtemplatePath)
        {
            //ContactU contactus = orthoEntities.ContactUs.Where(x => x.Email == emailaddress).SingleOrDefault(); 
            //if (contactus.Email != null && contactus != null)
            //{
                if (File.Exists(emailtemplatePath))
                {
                    string emailtemplateHTML = File.ReadAllText(emailtemplatePath);
                    emailtemplateHTML = emailtemplateHTML.Replace("##Name##", name);
                    emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", System.DateTime.Now.Year.ToString());
                    emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));
                    MailAddress fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("ToMail"));
                    MailAddress toMailAddress = new MailAddress(emailaddress);
                    CommonLogic.SendMail(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, "4ClearOrtho - Thank you for Contact Us");
                 }
            //}
        }
        public List<ContactU> GetContactUsNotification()
        {
            return orthoEntities.ContactUs.Where(x => x.IsResponded == false).ToList();
        }
    }
}
