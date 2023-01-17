using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;

namespace _4eOrtho.Admin
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        #region Declaration

        private ILog logger;

        #endregion

        #region Events
        /// <summary>
        /// check validate user in on it method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            try
            {
                logger = log4net.LogManager.GetLogger(typeof(AdminMaster));

                if (Authentication.HasAdminLoggedIn())
                {
                    if (string.IsNullOrEmpty(SessionHelper.LoggedAdminEmailAddress))
                    {
                        Response.Redirect("Login.aspx", true);
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx", true);
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
        /// <summary>
        /// set css and style sheet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                csEnglish.Attributes.Add("href", "Styles/AdminStyle.css");
                csajaxEnglish.Attributes.Add("href", "Styles/Ajaxtoolkit.css");
            }
        }
        #endregion
    }
}
