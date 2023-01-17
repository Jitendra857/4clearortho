using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace _4eOrtho
{
    public partial class Ortho : System.Web.UI.MasterPage
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(OrthoInnerMaster));
        #endregion

        #region Events
        /// <summary>
        /// set div patient photo picture and office hours
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPublicMenu();
                if (this.Page.Request.Url.ToString().ToLower().Contains("doctorprofile"))
                {
                    dvPatientPhoto.Visible = true;
                    dvOfficeHours.Visible = true;
                }

                PagesEntity pagesEntity = new PagesEntity();

                PageDetail pageWithDetail = pagesEntity.GetPageDetailByMenuNameandLanguage("4ClearOrtho-Address", SessionHelper.LanguageId);

                if (pageWithDetail != null)
                {                    
                    ltrAddress.Text = pageWithDetail.PageContent;
                }
                if (SessionHelper.CurrentCultureName == "ar-SA" || SessionHelper.CurrentCultureName == "fa-IR")
                    lnk_style.Attributes.Add("href", "Styles/style.ar-SA.css?ver=1.1.3");
                else
                    lnk_style.Attributes.Add("href", "Styles/style.css?ver=1.1.3");
            }
        }
        /// <summary>
        /// Bind public menu
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
                //menuHTML.AppendLine(@"<li><a href='FindDoctor.aspx'>Find a Doctor</a></li>");
                //menuHTML.AppendLine(@"<li><a href='BeforeAfterPictures.aspx'>" + this.GetLocalResourceObject("BeforeAfterPicture") + "</a></li>");
                menuHTML.AppendLine(@"<li><a href='ClientTestimonials.aspx'>" + this.GetLocalResourceObject("ClientTestimonial") + "</a></li>");
                //menuHTML.AppendLine(@"<li><a href='#'>" + this.GetLocalResourceObject("NewCase") + "</a></li>");
                //menuHTML.AppendLine(@"<li><a href='#'>" + this.GetLocalResourceObject("TrackCase") + "</a></li>");
                //menuHTML.AppendLine(@"<li><a href='ListSupplyOrder.aspx'>Order Supply</a></li>");
                menuHTML.AppendLine(@"<li><a href='Brochure.aspx' target='_blank'>" + this.GetLocalResourceObject("Brochure") + "</a></li>");
                //menuHTML.AppendLine(@"<li><a  href='" + CommonLogic.GetConfigValue("StudentRegistration") + "' target='_blank'>" + this.GetLocalResourceObject("BecomeProvider") + "</a></li>");
                //menuHTML.AppendLine(@"<li><a target='_blank' href='" + CommonLogic.GetConfigValue("StudentRegistration") + "'>" + this.GetLocalResourceObject("TakeCourse") + "</a></li>");

                subcat.InnerHtml = menuHTML.ToString();

            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }

        }
        #endregion
    }
}