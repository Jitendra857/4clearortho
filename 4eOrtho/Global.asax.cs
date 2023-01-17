using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace _4eOrtho
{
    public class Global : System.Web.HttpApplication
    {
        #region Declaration
        public static int PageSize = 10;
        #endregion

        #region Events
        /// <summary>
        /// bind register routes when application start and log4net config
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            log4net.Config.XmlConfigurator.Configure();
            RegisterRoutes();
        }

        /// <summary>
        /// application end
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        /// <summary>
        /// application error
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        /// <summary>
        /// application start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        /// <summary>
        /// session end
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }
        #endregion

        #region Helpers
        /// <summary>
        /// register routes path
        /// </summary>
        private static void RegisterRoutes()
        {
            ////RouteTable.Routes.MapPageRoute("StoreRoute",
            ////    "Route/{Name}",
            ////    "~/Index.aspx");
            RouteTable.Routes.Ignore("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.MapPageRoute("StoreRoute",
                "{Name}",
                "~/Default.aspx");
        }
        #endregion

    }
}
