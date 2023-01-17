using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;

namespace _4eOrtho
{
    public partial class Welcome : PageBase
    {
        #region Declaration
        CurrentSession currentSession = null;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserLoginSession"] != null)
            {
                currentSession = new CurrentSession();
                currentSession = (CurrentSession)Session["UserLoginSession"];
                if (currentSession.UserType == UserType.P.ToString())
                {
                    lblWelcome.Text = this.GetLocalResourceObject("Heading").ToString()+ " "+ currentSession.PatientFirstName + ' ' + currentSession.PatientLastName + "!";
                }
                else if (currentSession.UserType == UserType.D.ToString() || currentSession.UserType == UserType.S.ToString())
                {
                    lblWelcome.Text = this.GetLocalResourceObject("Heading").ToString() + " " + currentSession.DoctorName + "!";
                }
            }
            if (!Page.IsPostBack)
            {
            }

        }
    }
}