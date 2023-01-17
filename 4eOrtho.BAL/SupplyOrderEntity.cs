using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Net.Mail;
using _4eOrtho.DAL;
using _4eOrtho.Utility;

namespace _4eOrtho.BAL
{
    public class SupplyOrderEntity : BaseEntity
    {
        /// <summary>
        /// Method to Get List of Supply Order with paging
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public List<SupplyOrderDetails> GetAllSupplyOrder(string sortField, string sortDirection, int pageSize, int pageIndex, string searchField, string searchValue, string doctorEmail, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));
            List<SupplyOrderDetails> lstOrderSupply = orthoEntities.GetAllSupplyOrder(pageIndex, pageSize, sortField, sortDirection, searchField, searchValue, doctorEmail, TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount.Value) ? 0 : (int)TotalRecCount.Value;
            return lstOrderSupply;
        }
        /// <summary>
        /// Method to Get List of Supply Order with paging for admin
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public List<AllSupplyOrderAdmin> GetAllSupplyOrderAdmin(string sortField, string sortDirection, int pageSize, int pageIndex, string searchField, string searchValue, string doctorEmail, out int totalRecords)
        {
            try
            {
                ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));
                List<AllSupplyOrderAdmin> lstOrderSupply = orthoEntities.GetAllSupplyOrderAdmin(pageIndex, pageSize, sortField, sortDirection, searchField, searchValue, doctorEmail, TotalRecCount).ToList();
                totalRecords = Convert.IsDBNull(TotalRecCount.Value) ? 0 : (int)TotalRecCount.Value;
                return lstOrderSupply;
            }
            catch (Exception)
            {
                totalRecords = 0;
                return new List<AllSupplyOrderAdmin>();
            }            
        }

        /// <summary>
        /// Method to save supply order in add and edit condition
        /// </summary>
        /// <param name="emrCountry"></param>
        public void Save(SupplyOrder supplyOrder)
        {
            if (supplyOrder.EntityState == System.Data.EntityState.Detached)
            {
                supplyOrder.CreatedDate = BaseEntity.GetServerDateTime;
                supplyOrder.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToSupplyOrders(supplyOrder);
            }
            else
            {
                supplyOrder.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
        }

        /// <summary>
        /// Method to Delete Supply Order
        /// </summary>
        /// <param name="emrCountry"></param>
        public void Delete(SupplyOrder supplyOrder)
        {
            orthoEntities.DeleteObject(supplyOrder);
            orthoEntities.SaveChanges();
        }

        /// <summary>
        ///  Method to create instance of Supply Order Master
        /// </summary>
        /// <returns></returns>
        public SupplyOrder Create()
        {
            return orthoEntities.SupplyOrders.CreateObject();
        }
        /// <summary>
        /// Get supply order by id
        /// </summary>
        /// <param name="supplyOrderId"></param>
        /// <returns></returns>
        public SupplyOrder GetSupplyOrderById(long supplyOrderId)
        {
            return orthoEntities.SupplyOrders.Where(x => x.SupplyOrderId == supplyOrderId).SingleOrDefault();
        }
        /// <summary>
        /// Send order supply to doctor with order details
        /// </summary>
        /// <param name="doctorName"></param>
        /// <param name="supplyName"></param>
        /// <param name="amount"></param>
        /// <param name="quantity"></param>
        /// <param name="emailaddress"></param>
        /// <param name="remarks"></param>
        /// <param name="emailtemplatePath"></param>
        /// <param name="subject"></param>
        /// <param name="totalAmount"></param>
        public void SendOrderSupplyDoctorMail(string doctorName, string supplyName, string amount, string quantity, string emailaddress, string remarks, string emailtemplatePath, string subject, string totalAmount, string status, string title)
        {
            if (File.Exists(emailtemplatePath))
            {
                string emailtemplateHTML = File.ReadAllText(emailtemplatePath);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorName##", doctorName);
                emailtemplateHTML = emailtemplateHTML.Replace("##SupplyName##", supplyName);
                emailtemplateHTML = emailtemplateHTML.Replace("##Amount##", amount);
                emailtemplateHTML = emailtemplateHTML.Replace("##Quantity##", quantity);
                emailtemplateHTML = emailtemplateHTML.Replace("##TotalAmount##", totalAmount);
                emailtemplateHTML = emailtemplateHTML.Replace("##Remarks##", remarks);
                emailtemplateHTML = emailtemplateHTML.Replace("##Status##", status);
                emailtemplateHTML = emailtemplateHTML.Replace("##Title##", title);
                emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));
                emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", System.DateTime.Now.Year.ToString());
                MailAddress fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("ToMail"));
                MailAddress toMailAddress = new MailAddress(emailaddress, doctorName);
                CommonLogic.SendMail(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, subject);
            }
        }
        /// <summary>
        /// Send order supply to admin with order details
        /// </summary>
        /// <param name="doctorName"></param>
        /// <param name="supplyName"></param>
        /// <param name="amount"></param>
        /// <param name="quantity"></param>
        /// <param name="emailaddress"></param>
        /// <param name="remarks"></param>
        /// <param name="emailtemplatePath"></param>
        /// <param name="subject"></param>
        /// <param name="totalAmount"></param>
        public void SendOrderSupplyAdminMail(string doctorName, string supplyName, string amount, string quantity, string emailaddress, string remarks, string emailtemplatePath, string subject, string totalAmount, string status, string title)
        {
            if (File.Exists(emailtemplatePath))
            {
                string emailtemplateHTML = File.ReadAllText(emailtemplatePath);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorName##", doctorName);
                emailtemplateHTML = emailtemplateHTML.Replace("##SupplyName##", supplyName);
                emailtemplateHTML = emailtemplateHTML.Replace("##Amount##", amount);
                emailtemplateHTML = emailtemplateHTML.Replace("##Quantity##", quantity);
                emailtemplateHTML = emailtemplateHTML.Replace("##TotalAmount##", totalAmount);
                emailtemplateHTML = emailtemplateHTML.Replace("##Remarks##", remarks);
                emailtemplateHTML = emailtemplateHTML.Replace("##Status##", status);
                emailtemplateHTML = emailtemplateHTML.Replace("##Title##", title);
                emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));
                emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", System.DateTime.Now.Year.ToString());
                MailAddress fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("ToMail"));
                MailAddress toMailAddress = new MailAddress(CommonLogic.GetConfigValue("ToMail"));
                CommonLogic.SendMail(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, subject);
            }
        }
        /// <summary>
        /// Send order supply to doctor with payment and order details
        /// </summary>
        /// <param name="doctorName"></param>
        /// <param name="supplyName"></param>
        /// <param name="amount"></param>
        /// <param name="quantity"></param>
        /// <param name="emailaddress"></param>
        /// <param name="remarks"></param>
        /// <param name="emailtemplatePath"></param>
        /// <param name="subject"></param>
        /// <param name="totalAmount"></param>
        public void SendOrderSupplyMailWithPayment(string doctorName, string supplyName, string amount, string quantity, string emailaddress, string remarks, string emailtemplatePath, string subject, string totalAmount, string status, string title, bool isAdmin)
        {
            if (File.Exists(emailtemplatePath))
            {
                string emailtemplateHTML = File.ReadAllText(emailtemplatePath);
                MailAddress fromMailAddress = null;
                MailAddress toMailAddress = null;
                if (isAdmin)
                {
                    fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("ToMail"));
                    toMailAddress = new MailAddress(CommonLogic.GetConfigValue("ToMail"));
                    emailtemplateHTML = emailtemplateHTML.Replace("##DoctorName##", "Admin");
                }
                else
                {
                    fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("ToMail"));
                    toMailAddress = new MailAddress(emailaddress, doctorName);
                    emailtemplateHTML = emailtemplateHTML.Replace("##DoctorName##", doctorName);
                }
                emailtemplateHTML = emailtemplateHTML.Replace("##SupplyName##", supplyName);
                emailtemplateHTML = emailtemplateHTML.Replace("##Amount##", amount);
                emailtemplateHTML = emailtemplateHTML.Replace("##Quantity##", quantity);
                emailtemplateHTML = emailtemplateHTML.Replace("##TotalAmount##", totalAmount);
                emailtemplateHTML = emailtemplateHTML.Replace("##Remarks##", remarks);
                emailtemplateHTML = emailtemplateHTML.Replace("##Status##", status);
                emailtemplateHTML = emailtemplateHTML.Replace("##Title##", title);
                emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));
                emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", System.DateTime.Now.Year.ToString());
                emailtemplateHTML = emailtemplateHTML.Replace("##TRANSACTIONID##", SessionHelper.TransactionId);
                emailtemplateHTML = emailtemplateHTML.Replace("##TRANSACTIONDATE##", !string.IsNullOrEmpty(SessionHelper.TransactionTime) ? SessionHelper.TransactionTime.Split(' ')[0] : string.Empty);
                emailtemplateHTML = emailtemplateHTML.Replace("##TRANSACTIONAMOUNT##", SessionHelper.PaymentAmount);
                emailtemplateHTML = emailtemplateHTML.Replace("##TRANSACTIONSTATUS##", "Success");
                CommonLogic.SendMail(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, subject);                
            }
        }
    }
}