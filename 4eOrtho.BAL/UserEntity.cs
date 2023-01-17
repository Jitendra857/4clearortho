using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using _4eOrtho.DAL;
using _4eOrtho.Utility;
using System.Data.Objects;

namespace _4eOrtho.BAL
{
    public class UserEntity : BaseEntity
    {
        /// <summary>
        /// Create instance of user object
        /// </summary>
        /// <returns></returns>
        public User Create()
        {
            return orthoEntities.Users.CreateObject();
        }

        /// <summary>
        /// Save user
        /// </summary>
        /// <param name="newUser"></param>
        public void Save(User newUser)
        {
            if (newUser.EntityState == System.Data.EntityState.Detached)
            {
                newUser.CreatedDate = BaseEntity.GetServerDateTime;
                newUser.UpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToUsers(newUser);
            }
            else
            {
                newUser.UpdatedDate = DateTime.Now;
            }
            orthoEntities.SaveChanges();
        }
        /// <summary>
        ///  Method to Delete User
        /// </summary>
        /// <param name="userId"></param>
        public void Delete(long userId)
        {
            var user = orthoEntities.Users.Where(x => x.ID == userId).SingleOrDefault();
            if (user != null)
            {
                orthoEntities.Users.DeleteObject(user);
                orthoEntities.SaveChanges();
            }
        }
        /// <summary>
        /// get user by email and password
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public User GetUserByEmailAndPassword(string emailAddress, string Password)
        {
            return orthoEntities.Users.Where(x => x.EmailAddress == emailAddress && x.Password == Password && x.IsActive == true && x.UserType.Trim().ToUpper() != "P" && x.UserType.Trim().ToUpper() != "D").FirstOrDefault();
        }

        /// <summary>
        /// Get user by usertype and email
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public User GetUserByEmailAndUserType(string emailAddress, string userType)
        {
            return orthoEntities.Users.Where(x => x.EmailAddress == emailAddress && x.IsActive == true && x.UserType.Trim().ToUpper() == userType).FirstOrDefault();
        }

        /// <summary>
        /// Get user by emai address
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public User GetUserByEmailAddress(string emailAddress)
        {
            return orthoEntities.Users.Where(x => x.EmailAddress.ToUpper() == emailAddress.ToUpper() && x.UserType == "D").FirstOrDefault();
        }

        public User GetPatientUserByEmailAddress(string emailAddress)
        {
            return orthoEntities.Users.Where(x => x.EmailAddress.ToUpper() == emailAddress.ToUpper() && x.UserType == "P").FirstOrDefault();
        }


        /// <summary>
        /// Send Forgor Password Mail
        /// </summary>
        /// <param name="emailaddress"></param>
        /// <param name="emailtemplatePath"></param>
        public bool SendForgotpasswordMail(string emailaddress, string emailtemplatePath)
        {
            User user = orthoEntities.Users.Where(x => x.EmailAddress == emailaddress && x.UserType.Trim().ToUpper().Contains("A")).FirstOrDefault();
            if (user != null && user.EmailAddress != null)
            {
                if (File.Exists(emailtemplatePath))
                {
                    string emailtemplateHTML = File.ReadAllText(emailtemplatePath);
                    emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));
                    emailtemplateHTML = emailtemplateHTML.Replace("##FirstName##", user.FirstName);
                    emailtemplateHTML = emailtemplateHTML.Replace("##LastName##", user.LastName);
                    emailtemplateHTML = emailtemplateHTML.Replace("##EmailId##", user.EmailAddress);
                    emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", System.DateTime.Now.Year.ToString());
                    emailtemplateHTML = emailtemplateHTML.Replace("##Password##", Cryptography.DecryptStringAES(user.Password, CommonLogic.GetConfigValue("SharedSecret")));

                    MailAddress fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("ToMail"));
                    MailAddress toMailAddress = new MailAddress(user.EmailAddress);
                    CommonLogic.SendMail(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, "4ClearOrtho - ForgotPassword");
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// This method will get user by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUserByUserId(long userId)
        {
            return orthoEntities.Users.Where(x => x.ID == userId).FirstOrDefault();
        }

        /// <summary>
        /// Get exist user id by email and userid
        /// </summary>
        /// <param name="emailAdaress"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public long GetExistUserIdByEmailAddressAndID(string emailAdaress, long userID,string UserType)
        {
            //User users = orthoEntities.Users.Where(x => x.EmailAddress == emailAdaress && x.ID != userID).FirstOrDefault();
            User users = orthoEntities.Users.Where(x => x.EmailAddress == emailAdaress && x.ID != userID && x.UserType == UserType.Trim()).FirstOrDefault();
            if (users != null)
            {
                return users.ID;
            }
            return 0;
        }

        /// <summary>
        /// Get exist user id by user id and username
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public long GetExistUserIdByIdAndUserName(string username, long userID)
        {
            User users = orthoEntities.Users.Where(x => x.UserName == username && x.ID != userID).FirstOrDefault();
            if (users != null)
            {
                return users.ID;
            }
            return 0;
        }

        public List<GetUserDetail> GetUserDetail(string sortField, string sortDirection, string searchField, string searchText, int pageSize, int pageIndex, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));
            List<GetUserDetail> lstGetUserDetail = orthoEntities.GetUserDetail(pageIndex, pageSize, sortField, sortDirection, searchField, searchText, TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstGetUserDetail;
        }

        public long GetAdminUserId()
        {
            return orthoEntities.Users.Where(x => x.IsSuperAdmin == true).FirstOrDefault().ID;
        }
    }
}
