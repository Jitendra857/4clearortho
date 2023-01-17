using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using System.Threading;
using System.Globalization;



namespace _4eOrtho.UserControls
{
    public partial class AdminTopControl : System.Web.UI.UserControl
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(AdminTopControl));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblLoggedUserName.Text = SessionHelper.UserDisplayName;

                if (!IsPostBack)
                {
                    BindLanguages();
                    ddlLanguage.SelectedIndex = ddlLanguage.Items.IndexOf(ddlLanguage.Items.FindByValue(SessionHelper.CurrentCultureName + "," + SessionHelper.LanguageId));
                }

                if (SessionHelper.LoggedUserType == UserAccessType.SuperAdmin.ToString())
                {
                    ContactUsEntity contactUsentity = new ContactUsEntity();
                    List<ContactU> contactus = contactUsentity.GetContactUsNotification();
                    imgResponse.Visible = (contactus.Count > 0);
                }
                else
                    imgResponse.Visible = false;
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void imgResponse_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect("ListContactUs.aspx");
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                SessionHelper.LoggedAdminUserID = 0;
                SessionHelper.LoggedAdminEmailAddress = SessionHelper.UserDisplayName = string.Empty;
                Authentication.UserLogoffProcess();
                Response.Redirect("Login.aspx");
                return;
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SessionHelper.CurrentCultureName = ddlLanguage.SelectedItem.Value.Split(',')[0];
                SessionHelper.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue.Split(',')[1]);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ddlLanguage.SelectedItem.Value);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(ddlLanguage.SelectedItem.Value.Split(',')[0]);
                Response.Redirect(Request.Url.ToString());
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        private void BindLanguages()
        {
            try
            {
                ddlLanguage.Items.Clear();
                //ddlLanguage.Items.Add(new ListItem("select", "0"));

                LanguagesEntity languageentity = new LanguagesEntity();
                List<Language> language = languageentity.GetAllLanguages();
                foreach (Language Languages in language)
                {
                    ddlLanguage.Items.Add(new ListItem(Languages.LanguageName, Languages.LanguageCultureCode + "," + Languages.LanguageID));
                }
                if (SessionHelper.LanguageId == 0)
                {
                    SessionHelper.CurrentCultureName = ddlLanguage.SelectedItem.Value.Split(',')[0];
                    SessionHelper.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue.Split(',')[1]);
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ddlLanguage.SelectedItem.Value);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(ddlLanguage.SelectedItem.Value.Split(',')[0]);
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
    }
}