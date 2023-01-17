using _4eOrtho.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho.UserControls
{
    public partial class AdminMenuControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SessionHelper.LoggedUserType.ToUpper().Trim() == UserType.AU.ToString().ToUpper().Trim())
                {
                    liSetup.Visible = false;
                    lnkuserManagement.Visible = false;
                    hyplnkContentManagement.Visible = false;
                    hyplnkClientTestimonial.Visible = false;
                    hypReviewManagment.Visible = false;
                    liGallery.Visible = false;
                    ChangePassword.Attributes.Add("Class", "none");
                }
                else if (SessionHelper.LoggedUserType.ToUpper().Trim() == UserType.LC.ToString().ToUpper().Trim())
                {
                    liSetup.Visible = false;
                    hyplnkLocalContactUser.Visible = false;
                    lnkuserManagement.Visible = false;
                    hyplnkContentManagement.Visible = false;
                    hyplnkClientTestimonial.Visible = false;
                    hypReviewManagment.Visible = false;
                    hypListDoctors.Visible = false;
                    liGallery.Visible = false;
                    liProductManagement.Visible = false;
                    ChangePassword.Attributes.Add("Class", "none");
                }
            }
        }
    }
}