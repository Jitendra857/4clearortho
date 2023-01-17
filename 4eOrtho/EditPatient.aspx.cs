using _4eOrtho.Helper;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Utility;

namespace _4eOrtho
{
    public partial class EditPatient : PageBase
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(EditPatient));
        #endregion


        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((CurrentSession)Session["UserLoginSession"] != null)
                {
                    PageRight pageRight = CheckRights(this.Page.GetType().BaseType.Name);
                    if (pageRight != null)
                    {
                        PageRedirect(pageRight.RedirectPageName);
                    }
                }
                if (Session["PatientAndDoctorId"] != null && Session["PatientAndDoctorId"].ToString() != "")
                {
                    ViewState["PatientIds"] = Session["PatientAndDoctorId"].ToString();
                    BindPatientDetail();
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    string[] id = ViewState["PatientIds"].ToString().Split('&');
                    UserEntity userEntity = new UserEntity();
                    User info = userEntity.GetUserByUserId(Convert.ToInt64(id[1]));
                    if(info != null)
                    {
                        info.FirstName = txtFirstName.Text.Trim();
                        info.LastName = txtLastName.Text.Trim();
                        info.Password = CommonLogic.EncryptStringAES(txtnewpassword.Text.ToString());
                        userEntity = new UserEntity();
                        userEntity.Save(info);
                    }
                    PatientEntity patientEntity = new PatientEntity();
                    Patient patientinfo = patientEntity.GetPatientById(Convert.ToInt64(id[0]));
                    if (patientinfo != null)
                    {
                        patientinfo.FirstName = txtFirstName.Text.Trim();
                        patientinfo.LastName = txtLastName.Text.Trim();
                        patientEntity = new PatientEntity();
                        patientEntity.Save(patientinfo);
                    }
                    string emailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("ChangePasswordTemplatePath")).ToString();
                    if (info != null)
                    {
                        string sPassword = Cryptography.DecryptStringAES(info.Password, CommonLogic.GetConfigValue("SharedSecret"));
                        PatientEntity.ChangePasswordMail(info.FirstName, info.LastName, sPassword, emailTemplatePath, info.EmailAddress);
                    }
                    CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("PasswordChangeSuccess").ToString(), divMsg, lblMsg);
                    btn_back.Visible = true;
                }
            }
            catch (Exception exp)
            {
                CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("PasswordChangeError").ToString(), divMsg, lblMsg);
                logger.Error("An error occured.", exp);
            }
        }
        #endregion


        #region Helper
        private void BindPatientDetail()
        {
            try
            {
                // Here id[0] is patients id and id[1] is user id
                string[] id = ViewState["PatientIds"].ToString().Split('&');
                if (id.Count() > 1)
                {
                    UserEntity userEntity = new UserEntity();
                    User info = userEntity.GetUserByUserId(Convert.ToInt64(id[1]));
                    if (info != null)
                    {
                        txtEmail.Text = info.EmailAddress;
                        txtFirstName.Text = info.FirstName;
                        txtLastName.Text = info.LastName;
                    }
                }
            }
            catch (Exception exp)
            {
                logger.Error("An error occured.", exp);
            }
        }
        #endregion

        protected void btn_back_Click(object sender, EventArgs e)
        {
            PageRedirect("ListPatient.aspx");
        }



    }
}