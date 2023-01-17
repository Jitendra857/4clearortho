using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Web.UI.WebControls;

namespace _4eOrtho
{
    public partial class Home : PageBase
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(Home));
        #endregion

        #region Events
        /// <summary>
        /// page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLanguages();
                BindPatientMenu();
                BindDoctorMenu();
                BindPublicMenu();
                ddlLanguage.SelectedIndex = ddlLanguage.Items.IndexOf(ddlLanguage.Items.FindByValue(SessionHelper.CurrentCultureName + "," + SessionHelper.LanguageId));
                if (SessionHelper.CurrentCultureName == "ar-SA" || SessionHelper.CurrentCultureName == "fa-IR")
                    lnk_style.Attributes.Add("href", "Styles/style.ar-SA.css");
                else
                    lnk_style.Attributes.Add("href", "Styles/style.css");
                
                PagesEntity pagesEntity = new PagesEntity();
                PageDetail pageWithDetail = pagesEntity.GetPageDetailByMenuNameandLanguage("4ClearOrtho-Address", SessionHelper.LanguageId);
                if (pageWithDetail != null)
                    ltrAddress.Text = pageWithDetail.PageContent;
            }
        }

        /// <summary>
        /// as per language set localization
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        #endregion

        #region Helpers
        /// <summary>
        /// bind languages in dropdown
        /// </summary>
        private void BindLanguages()
        {
            try
            {
                ddlLanguage.Items.Clear();
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
        /// <summary>
        /// bind patient menu
        /// </summary>
        public void BindPatientMenu()
        {
            try
            {                
                PageDetailsEntity pageDetailsEntity = new PageDetailsEntity();
                List<PageDetail> pageDetails = pageDetailsEntity.GetPageDetailsByRole(UserType.P.ToString(), SessionHelper.LanguageId);
                StringBuilder menuHTML = new StringBuilder();

                if (pageDetails != null && pageDetails.Count > 0)
                {
                    foreach (PageDetail pageDetail in pageDetails)
                        menuHTML.AppendLine(@"<li><a href='" + pageDetail.URLName + "'>" + pageDetail.MenuItem + "</a></li>");
                }

                menuHTML.AppendLine(@"<li><a href='BeforeAfterPictures.aspx'>" + this.GetLocalResourceObject("Gallery") + "</a></li>");
                menuHTML.AppendLine(@"<li><a href='Brochure.aspx' target='_blank'>" + this.GetLocalResourceObject("Brochure") + "</a></li>");
                menuHTML.AppendLine(@"<li><a href='ClientTestimonials.aspx'>" + this.GetLocalResourceObject("ClientTestimonial") + "</a></li>");
                menuHTML.AppendLine(@"<li><a href='FindDoctor.aspx'>" + this.GetLocalResourceObject("FREECONSULTATION/FINDADENTIST") + "</a></li>");                
                ulPatient.InnerHtml = menuHTML.ToString();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// bind doctor menu
        /// </summary>
        public void BindDoctorMenu()
        {
            try
            {                
                PageDetailsEntity pageDetailsEntity = new PageDetailsEntity();
                List<PageDetail> pageDetails = pageDetailsEntity.GetPageDetailsByRole(UserType.D.ToString(), SessionHelper.LanguageId);
                StringBuilder menuHTML = new StringBuilder();

                menuHTML.AppendLine(@"<li><a href='PatientEductionVideo.aspx'>" + this.GetLocalResourceObject("PatientEducationVideo") + " " + "<img  src='Content/images/video.png' style='margin-left:5px;margin-bottom: -4px;height:16px' /></a></li>");                
                menuHTML.AppendLine(@"<li><a href='DoctorRegistration.aspx' style='color:#016dae;'>" + this.GetLocalResourceObject("RegisterHere") + "</a></li>");
                foreach (PageDetail pageDetail in pageDetails)
                {
                    menuHTML.AppendLine(@"<li><a href='" + pageDetail.URLName + "'>" + pageDetail.MenuItem + "</a></li>");
                }                
                ulDoctors.InnerHtml = menuHTML.ToString();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// bind public menu
        /// </summary>
        public void BindPublicMenu()
        {
            try
            {
                PageDetailsEntity pageDetailsEntity = new PageDetailsEntity();
                List<PageDetail> pageDetails = pageDetailsEntity.GetPageDetailsByRole(UserType.A.ToString(), SessionHelper.LanguageId);
                StringBuilder menuHTML = new StringBuilder();

                foreach (PageDetail pageDetail in pageDetails)
                {
                    menuHTML.AppendLine(@"<li><a href='" + pageDetail.URLName + "'>" + pageDetail.MenuItem + "</a></li>");
                }
                menuHTML.AppendLine(@"<li><a href='FindDoctor.aspx'>" + this.GetLocalResourceObject("FindaDoctor") + "</a></li>");
                ulOrtho.InnerHtml = menuHTML.ToString();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        #endregion
    }
}