using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Web.UI;

namespace _4eOrtho
{
    public partial class Default : PageBase
    {
        #region Declaration
        string menuPageName;
        private ILog logger = log4net.LogManager.GetLogger(typeof(Default));
        #endregion

        #region Events
        /// <summary>
        /// set page route data and bind cms data on page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                menuPageName = Page.RouteData.Values["Name"] as string;
                //pageID = Convert.ToInt32(Page.RouteData.Values["id"].ToString());
                BindCMSData();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);

            }
        }

        /// <summary>
        /// set master page as per logged in user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserLoginSession"] != null)
            {
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];

                if (currentSession.UserType.ToString() == UserType.D.ToString() || currentSession.UserType.ToString() == UserType.S.ToString() || currentSession.UserType.ToString() == UserType.P.ToString())                
                    this.MasterPageFile = "~/OrthoInnerMaster.Master";                
                else                
                    this.MasterPageFile = "~/Ortho.Master";                
            }
            else
            {
                this.MasterPageFile = "~/Ortho.Master";
            }
        }
        #endregion

        #region Helpers
        /// <summary>
        /// bind cms data and links
        /// </summary>
        private void BindCMSData()
        {
            try
            {
                PagesEntity pagesEntity = new PagesEntity();
                PageDetail pageWithDetail = pagesEntity.GetPageDetailByMenuNameandLanguage(menuPageName, Convert.ToInt32(SessionHelper.LanguageId));

                if (pageWithDetail != null)
                {
                    Page.Title = pageWithDetail.PageTitle;
                    Page.MetaKeywords = pageWithDetail.PageKeyword;
                    Page.MetaDescription = pageWithDetail.PageMetaDescription;
                    ltrCMSContent.Text = pageWithDetail.PageContent;
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        #endregion
    }
}