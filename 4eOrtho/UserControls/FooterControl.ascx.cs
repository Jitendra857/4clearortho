using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.DAL;
using _4eOrtho.BAL;
using _4eOrtho.Utility;
using System.Text;
using System.Web.Routing;
using log4net;

namespace _4eOrtho.UserControls
{
    public partial class FooterControl : System.Web.UI.UserControl
    {
        #region Declaration
        long curruntrootID;
        long pageId;
        StringBuilder footerHTML = new StringBuilder();
        private ILog logger = log4net.LogManager.GetLogger(typeof(FooterControl));
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblCopyrightText.Text = "@" + System.DateTime.Now.Year + "4eDental";
                if (!Page.IsPostBack)
                {
                    if (!String.IsNullOrEmpty(CommonLogic.QueryString("id")))
                    {
                        pageId = Convert.ToInt32(CommonLogic.QueryString("id"));
                    }

                    if (!String.IsNullOrEmpty(CommonLogic.QueryString("rid")))
                    {
                        curruntrootID = Convert.ToInt32(CommonLogic.QueryString("rid"));
                    }
                    BindFooterLinks();
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        #region Helper
        private void BindFooterLinks()
        {
            PagesEntity pagesEntity = new PagesEntity();
            string roleType = "";
            if ((((CurrentSession)Session["UserLoginSession"]) == null || (((CurrentSession)Session["UserLoginSession"]).UserType == string.Empty)))
                roleType = "";
            else if (((CurrentSession)Session["UserLoginSession"]).UserType == UserType.P.ToString())
                roleType = "P";
            else if (((CurrentSession)Session["UserLoginSession"]).UserType == UserType.D.ToString() || ((CurrentSession)Session["UserLoginSession"]).UserType == UserType.S.ToString())
                roleType = "D";

            List<MenuPageWithLevel> pageWithLevelList = pagesEntity.GetMenuPage(SessionHelper.LanguageID, roleType);
            pageWithLevelList = pageWithLevelList.Where(x => x.LanguageID != null).ToList();
            pageWithLevelList = pageWithLevelList.Where(x => x.ParentID == null).ToList();

            if (pageId == 0 && curruntrootID == 0 && CommonLogic.GetThisPageName(false).ToUpper() == "Default.ASPX")
            {
                MenuPageWithLevel firstPage = pageWithLevelList.FirstOrDefault();
                if (firstPage != null)
                {
                    //--Original code of CMS
                    //--Response.Redirect(string.Format("index.aspx?rid={0}&id={0}", firstPage.PageID));

                    Response.Redirect(string.Format("~/{0}/{1}", firstPage.MenuItem));
                    return;
                }
            }

            if (pageWithLevelList.Where(x => x.PageID == pageId).FirstOrDefault() == null && CommonLogic.GetThisPageName(false).ToUpper() == "Default.ASPX")
            {
                if (pageWithLevelList.Count > 0)
                {
                    Response.Redirect("Default.aspx");
                }
            }
            string contactUSMenuItem = string.Empty;

            //if (roleType == "D")
            //{
            //    footerHTML.AppendLine(@"<li><a href='AddEditSupplyOrder.aspx'>Supply Order</a></li>");
            //    footerHTML.AppendLine(@"<li><a href='Contact-us.aspx'>Contact Us</a></li>");
            //}
            //else if (roleType == "P")
            //{
            //    footerHTML.AppendLine(@"<li><a href='AddRecommendedDentist.aspx'>Recommended Dentist</a></li>");
            //    footerHTML.AppendLine(@"<li><a href='AddClientTestimonial.aspx'>ClientTestimonial</a></li>");
            //    footerHTML.AppendLine(@"<li><a href='FindDoctor.aspx'>Find Doctor</a></li>");
            //    footerHTML.AppendLine(@"<li><a href='Contact-us.aspx'>Contact Us</a></li>");
            //}
            //else if (roleType == "")
            //{
            //    footerHTML.AppendLine(@"<li><a href='Contact-us.aspx'>Contact Us</a></li>");
            //    footerHTML.AppendLine(@"<li><a href='PatientLogin.aspx'>Patient Login</a></li>");
            //    footerHTML.AppendLine(@"<li><a href='DoctorLogin.aspx'>Doctor Login</a></li>");
            //}
            GenerateFooterHTML(pageWithLevelList);


            footerHTML.AppendLine(contactUSMenuItem);
            lblFooter.Text = footerHTML.ToString();

        }
        private void GenerateFooterHTML(List<MenuPageWithLevel> list)
        {
            foreach (MenuPageWithLevel pageWithLevel in list)
            {
                footerHTML.AppendLine(@"<a href='" + GetPageURL(pageWithLevel.PageID, pageWithLevel.URLName.ToString(), "StoreRoute") + "'>" + pageWithLevel.MenuItem + "</a>|");
            }
        }
        public static string GetPageURL(long pageid, string pageName, string routeName)
        {
            string pageURL = string.Empty;
            RouteValueDictionary urlParameters = new RouteValueDictionary{
                        {"Name", pageName},{"id",pageid}};
            VirtualPathData urlPathData = RouteTable.Routes.GetVirtualPath(null, routeName, urlParameters);
            pageURL = urlPathData.VirtualPath;
            return pageURL;
        }
        #endregion
    }
}