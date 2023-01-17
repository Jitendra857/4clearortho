using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using _4eOrtho.Utility;
using _4eOrtho.BAL;
using System.Collections.Generic;
using log4net;

namespace _4eOrtho.Helper
{
    public class PageBase : System.Web.UI.Page
    {
        string cultureNameCookieName = "CurrentCultureName";
        public string CultureName
        {
            get
            {
                if (SessionHelper.CurrentCultureName != null && !string.IsNullOrEmpty(SessionHelper.CurrentCultureName))
                {
                    return SessionHelper.CurrentCultureName;
                }
                else if (HttpContext.Current.Request.Cookies[cultureNameCookieName] != null && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies[cultureNameCookieName].Value))
                {
                    SessionHelper.CurrentCultureName = HttpContext.Current.Request.Cookies[cultureNameCookieName].Value;
                    return SessionHelper.CurrentCultureName;
                }
                else
                {
                    SessionHelper.CurrentCultureName = "en-US";
                    HttpCookie ckCultureName = new HttpCookie(cultureNameCookieName, SessionHelper.CurrentCultureName);
                    HttpContext.Current.Response.Cookies.Add(ckCultureName);
                }
                return SessionHelper.CurrentCultureName;
            }
            set
            {
                HttpCookie ckCultureName = new HttpCookie(cultureNameCookieName, value);
                HttpContext.Current.Response.Cookies.Add(ckCultureName);
                SessionHelper.CurrentCultureName = value;
            }
        }
        private ILog logger = log4net.LogManager.GetLogger(typeof(PageBase));

        protected override void InitializeCulture()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(CultureName);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(CultureName);
            // Change the calendar used by a ar-SA/en-US CultureInfo object from ThaiBuddhist to Gregorian.
            if (Thread.CurrentThread.CurrentCulture.Calendar.ToString() != "System.Globalization.GregorianCalendar" && Thread.CurrentThread.CurrentUICulture.Calendar.ToString() != "System.Globalization.GregorianCalendar")
            {
                foreach (Calendar supportCalender in Thread.CurrentThread.CurrentCulture.OptionalCalendars)
                {
                    if (supportCalender.ToString() == "System.Globalization.GregorianCalendar")
                    {
                        //supportCalender.
                        Thread.CurrentThread.CurrentCulture.DateTimeFormat.Calendar = supportCalender;
                        Thread.CurrentThread.CurrentUICulture.DateTimeFormat.Calendar = supportCalender;
                        break;
                    }
                }
            }
            Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy";
            base.InitializeCulture();
        }
        #region authentication
        public PageRight CheckRights(string pageName)
        {
            List<PageList> lstPages = SessionHelper.UserPageRights;
            PageRight pageRight = new PageRight();
            if (SessionHelper.UserPageRights == null)
            {
                pageRight.RedirectPageName = "Home.aspx";
                pageRight.IsLogIn = true;
                return pageRight;
            }
            if (lstPages.Count > 0)
            {
                if (lstPages.FindAll(x => x.PageName.Equals(pageName, StringComparison.InvariantCultureIgnoreCase) && x.UserType == SessionHelper.LoggedUserType).Count == 0)
                {
                    pageRight.RedirectPageName = "Welcome.aspx";
                    pageRight.IsLogIn = true;
                    return pageRight;

                    //if (SessionHelper.LoggedUserType == UserType.P.ToString())
                    //{
                    //    pageRight.RedirectPageName = "PatientLogin.aspx";
                    //    pageRight.IsLogIn = true;
                    //    return pageRight;
                    //}
                    //else if (SessionHelper.LoggedUserType == UserType.D.ToString() || SessionHelper.LoggedUserType == UserType.S.ToString())
                    //{
                    //    pageRight.RedirectPageName = "DoctorLogin.aspx";
                    //    pageRight.IsLogIn = true;
                    //    return pageRight;
                    //}
                }
            }
            return null;
        }
        public void PageRedirect(string PageName)
        {
            try
            {
                Response.Redirect(PageName);
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
        }
        #endregion

        #region Rediret page afterlogin
        public bool CheckUserRights(string pageName)
        {
            ArrayList pageRights = SessionHelper.UserDisplayPageRights;

            if (pageRights.Count > 0)
            {
                for (int i = 0; i < pageRights.Count; i++)
                {
                    if (pageName == pageRights[i].ToString())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void UserPageRedirect()
        {
            try
            {
                if ((((CurrentSession)Session["UserLoginSession"]) != null))
                {
                    if (((CurrentSession)Session["UserLoginSession"]).UserType == UserType.P.ToString())
                    {
                        Response.Redirect("");
                    }
                    else if (((CurrentSession)Session["UserLoginSession"]).UserType == UserType.D.ToString() || ((CurrentSession)Session["UserLoginSession"]).UserType == UserType.S.ToString())
                    {
                        Response.Redirect("");
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
        }

        #endregion

        public void SetSecurityHeader()
        {
            if (((PayPal.Manager.SDKConfigHandler)(System.Configuration.ConfigurationManager.GetSection("paypal"))).Setting("mode") == "sandbox")
            {
                logger.Error("Set Security Method Exected due to sandbox as payment method");    
                System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
                System.Net.ServicePointManager.Expect100Continue = true;
                System.Net.ServicePointManager.DefaultConnectionLimit = 9999;
            }
            else
            {
                logger.Error("Set Security Method does not Exected due to live as payment method");
            }
        }
    }
}