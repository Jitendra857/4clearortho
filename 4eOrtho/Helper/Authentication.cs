using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using System.Web.Security;
using _4eOrtho.Utility;
using System.IO;
using System.Net.Mail;

namespace _4eOrtho.Helper
{
    public class Authentication
    {
        public static int GetLoggedUserID()
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                }
            }
            return 0;
        }
        public static void UserLogoffProcess()
        {
            HttpContext.Current.Session.Abandon();
            FormsAuthentication.SignOut();
        }
        public static bool UserLoginProcess(string EmailAddress, string password, bool IsPersist, bool checkForAdmin)
        {
            long userId;
            bool isSuperAdmin;
            if (ValidateUser(EmailAddress, password, checkForAdmin, out userId, out isSuperAdmin))
            {
                SessionHelper.LoggedAdminUserID = userId;
                SessionHelper.LoggedAdminEmailAddress = EmailAddress;
                if (isSuperAdmin)
                    SessionHelper.LoggedUserType = UserType.SuperAdmin.ToString();
                else
                    //SessionHelper.LoggedUserType = RoleEnum.Doctor.ToString();
                    SessionHelper.IsSuperAdmin = isSuperAdmin;
                return true;
            }
            return false;
        }
        public static bool ValidateUser(string EmailAddress, string password, bool checkForAdmin, out long uId, out bool isSuperAdmin)
        {
            uId = 0;
            isSuperAdmin = false;
            UserEntity userEntity = new UserEntity();
            User user = userEntity.GetUserByEmailAndPassword(EmailAddress, password);
            if (user != null)
            {
                uId = user.ID;
                isSuperAdmin = user.IsSuperAdmin;
                SessionHelper.UserDisplayName = user.FirstName + " " + user.LastName;
                SessionHelper.LoggedUserType = user.UserType;
                return true;
            }
            return false;
        }
        public static bool HasAdminLoggedIn()
        {
            return string.IsNullOrEmpty(SessionHelper.LoggedAdminEmailAddress) && SessionHelper.LoggedAdminUserID == 0 ? false : true;
        }        
    }
}