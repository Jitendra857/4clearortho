using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;

namespace _4eOrtho.Admin
{
    public partial class ChangePassword : PageBase
    {
        #region Declaration
        private ILog logger;
        #endregion

        #region Events
        /// <summary>
        /// Page Load Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            divMsg.Visible = false;
            logger = log4net.LogManager.GetLogger(typeof(ChangePassword));
            if (!Page.IsPostBack)
            {
                if (Request.UrlReferrer != null)
                    ViewState["PreviousPage"] = Request.UrlReferrer.ToString();
                else
                    ViewState["PreviousPage"] = "UserList.aspx";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["args"].ToString() == "true")
                {
                    UserEntity userEntity = new UserEntity();
                    User user = userEntity.GetUserByUserId(SessionHelper.LoggedAdminUserID);
                    if (user != null)
                    {
                        string password = Cryptography.EncryptStringAES(txtNewPassword.Text.ToString().Trim(), CommonLogic.GetConfigValue("SharedSecret"));
                        user.Password = password;
                        userEntity.Save(user);
                        CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("PasswordChanged").ToString(), divMsg, lblMsg);
                        //Authentication.ChangePasswordEmailSend(user.UserName, txtNewPassword.Text.ToString().Trim(), user.FirstName, user.LastName, CommonLogic.GetConfigValue("ChangePasswordEmailTemplatePath"), user.EmailAddress);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Check User current password process", ex);
            }
        }
       #endregion

        #region Helper
        /// <summary>
        /// check with current password valid or not
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void custxtCurrentPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                
                User user = userEntity.GetUserByUserId(SessionHelper.LoggedAdminUserID);
                string password = Cryptography.DecryptStringAES(user.Password, CommonLogic.GetConfigValue("SharedSecret"));
                if (user != null)
                {
                    if (password.Equals(txtCurrentPassword.Text.ToString().Trim()))
                    {
                        args.IsValid = true;
                        ViewState["args"] = "true" ;
                    }
                    else
                    {
                        args.IsValid = false;
                        ViewState["args"] = "false"; 
                    }
                    
                }
                else
                    args.IsValid = false;
            }
            catch (Exception ex)
            {
                logger.Error("Check User current password process", ex);
            }
        }
        #endregion
    }
}