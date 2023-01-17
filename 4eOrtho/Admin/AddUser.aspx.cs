using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Utility;
using _4eOrtho.Helper;
using System.Threading;

namespace _4eOrtho.Admin
{
    public partial class AddUser : PageBase
    {
        #region Global Declaration

        long userID = 0;
        private ILog logger;

        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            logger = log4net.LogManager.GetLogger(typeof(AddUser));
            try
            {
                if (CommonLogic.GetConfigValue("EnableUserAuthModule") != "1")
                {
                    Response.Redirect("UserList.aspx", false);
                    return;
                }
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    userID = Convert.ToInt32(Request.QueryString["id"]);
                }

                if (!IsPostBack)
                {
                    if (userID > 0)
                    {
                        cstmtxtEmailAddress.Enabled = false;
                        vcecstmtxtEmailAddress.Enabled = false;
                        GetData();
                        //lblHeader.Text = "Edit User";
                        //Page.Title = "Admin - Edit User";

                        lblHeader.Text = this.GetLocalResourceObject("lblHeaderResource2").ToString();
                        Page.Title = this.GetLocalResourceObject("PageResource2").ToString();

                        txtEmailAddress.Enabled = false;
                    }
                    else
                    {
                        //lblHeader.Text = "Add User";
                        lblHeader.Text = this.GetLocalResourceObject("lblHeaderResource1.Text").ToString();
                        chkStatus.Checked = true;
                        chkStatus.Enabled = false;
                    }
                    //this.Form.DefaultButton = this.btnAdd.UniqueID;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Add user page loding", ex);
            }
        }

        /// <summary>
        /// This methos will check user name is exists or not in database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cusCustom_ServerUsernameValidate(object sender, ServerValidateEventArgs e)
        {
            try
            {
                UserEntity usersEntity = new UserEntity();
                long GetUserID = usersEntity.GetExistUserIdByIdAndUserName(e.Value.ToString(), userID);

                if (GetUserID > 0)
                    e.IsValid = false;
                else
                    e.IsValid = true;
            }
            catch (Exception ex)
            {
                logger.Error("Email validation process", ex);
            }
        }

        /// <summary>
        /// This method will check email address is exists or not in database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cusCustom_ServerEmailValidate(object sender, ServerValidateEventArgs e)
        {
            try
            {
                UserEntity usersEntity = new UserEntity();
                long GetUserID = usersEntity.GetExistUserIdByEmailAddressAndID(e.Value.ToString(), userID, "AU");

                if (GetUserID > 0)
                    e.IsValid = false;
                else
                    e.IsValid = true;
            }
            catch (Exception ex)
            {
                logger.Error("Email validation process", ex);
            }
        }

        /// <summary>
        /// This click event will save user in database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SaveUserData();
            }
        }

        /// <summary>
        /// Cancel button event will redirect user to User List page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btnBack_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("UserList.aspx", false);
        //}

        #endregion

        #region Functions

        private void GetData()
        {
            try
            {
                UserEntity usersEntity = new UserEntity();
                User user = usersEntity.GetUserByUserId(userID);
                if (user != null)
                {
                    txtFirstName.Text = user.FirstName;
                    txtLastName.Text = user.LastName;
                    //txtUserName.Text = user.UserName;
                    txtEmailAddress.Text = user.EmailAddress;
                    chkStatus.Checked = user.IsActive;
                    if (user.ID.Equals(SessionHelper.LoggedAdminUserID) || user.IsSuperAdmin)
                        chkStatus.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Get User Data", ex);
            }
        }

        private void SaveUserData()
        {
            try
            {
                bool ShouldSendEmail = false;
                string userAutoPassword = Guid.NewGuid().ToString().Substring(0, 6);
                UserEntity usersEntity = null;
                User user = null;
                if (userID > 0)
                {
                    usersEntity = new UserEntity();
                    user = usersEntity.GetUserByUserId(userID);
                    if (user == null)
                    {
                        Response.Redirect("UserList.aspx", false);
                        return;
                    }
                }
                else
                {
                    usersEntity = new UserEntity();
                    user = usersEntity.Create();
                    ShouldSendEmail = true;
                }

                user.FirstName = txtFirstName.Text;
                user.LastName = txtLastName.Text;
                user.UserName = user.EmailAddress = txtEmailAddress.Text;
                user.UserType = "AU";

                if (user.ID == 0)
                {
                    user.Password = Cryptography.EncryptStringAES(userAutoPassword, CommonLogic.GetConfigValue("SharedSecret"));
                }

                if (user.IsSuperAdmin)
                {
                    user.IsActive = true;
                }
                else
                {
                    user.IsActive = chkStatus.Checked;
                }
                usersEntity.Save(user);
                if (ShouldSendEmail)
                {
                    string welcomeEmailTemplatePath = Server.MapPath("\\" + CommonLogic.GetConfigValue("UserRegistraion"));
                    CommonLogic.RegisterUserEmail(user.UserName, userAutoPassword, user.FirstName, user.LastName, welcomeEmailTemplatePath, user.EmailAddress, "Admin", "Registration");
                }
                Response.Redirect("UserList.aspx", false);                
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                logger.Error("Save new user data", ex);
            }
        }
        #endregion
    }
}