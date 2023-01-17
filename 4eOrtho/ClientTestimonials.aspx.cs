using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using System;
using System.Collections.Generic;

namespace _4eOrtho
{
    public partial class ClientTestimonials : PageBase
    {
        #region Declaration
        string useType = string.Empty;
        #endregion

        #region Events

        /// <summary>
        /// bind testimonial on page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserLoginSession"] != null)
                {
                    PageRight pageRight = CheckRights(this.Page.GetType().BaseType.Name);
                    if (pageRight != null)
                    {
                        PageRedirect(pageRight.RedirectPageName);
                    }
                    CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                    useType = currentSession.UserType == "S" ? "D" : currentSession.UserType;
                    BindTestimonial(useType);
                    btnAddTestimonail.PostBackUrl = "~/AddClientTestimonial.aspx";
                }
                else
                {
                    btnAddTestimonail.Visible = false;
                    //btnAddTestimonail.PostBackUrl = "~/PatientLogin.aspx";
                    BindTestimonial("P");
                }
            }
        }

        /// <summary>
        /// set master page as per user session
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
        /// method for display Approved  Testimonial
        /// </summary>
        public void BindTestimonial(string userType)
        {
            ClientTestimonialEntity clientTestimonialentity = new ClientTestimonialEntity();
            List<ClientTestimonial> lstClientTestimonail = clientTestimonialentity.GetAllActiveTestimonial(userType);
            rptClientTestimonial.DataSource = lstClientTestimonail;
            rptClientTestimonial.DataBind();
        }

        #endregion
    }
}