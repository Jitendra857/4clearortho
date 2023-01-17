using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho
{
    public partial class Contact_Us : PageBase
    {
        #region Declaration

        private ILog logger = log4net.LogManager.GetLogger(typeof(Contact_Us));

        #endregion declaration

        #region Events

        /// <summary>
        /// pageload
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if ((CurrentSession)Session["UserLoginSession"] != null)
                    {
                        PageRight pageRight = CheckRights(this.Page.GetType().BaseType.Name);
                        if (pageRight != null)
                        {
                            PageRedirect(pageRight.RedirectPageName);
                        }
                    }
                    BindCountry();
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
        /// Page Pre Init Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.Page.MasterPageFile = (Session["UserLoginSession"] != null) ? "~/OrthoInnerMaster.master" : "~/Ortho.master";
        }

        /// <summary>
        /// method for save data in database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    ContactUsEntity contactusEntity = new ContactUsEntity();
                    ContactU contactus = new ContactU();
                    contactus = contactusEntity.Create();
                    contactus.CreatedBy = 1;
                    contactus.CreatedDate = BaseEntity.GetServerDateTime;

                    contactus.Name = txtName.Text.Trim();
                    contactus.Email = txtEmail.Text.Trim();
                    contactus.CountryId = Convert.ToInt64(ddlCountry.SelectedItem.Value);
                    contactus.StateId = Convert.ToInt64(ddlState.SelectedItem.Value);
                    contactus.City = txtCity.Text.Trim();
                    contactus.Subject = txtSubject.Text.Trim();
                    contactus.Comment = txtComment.Text.Trim();
                    contactus.Mobile = txtMobile.Text.Trim();
                    contactus.Isactive = true;
                    contactus.IsResponded = false;
                    contactusEntity.Save(contactus);
                    logger.Info("Contact Us information Saved Succesfully");
                    SendInquiryMail();
                    CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("SendQueryMessage").ToString(), divMsg, lblMsg);
                    ClearControls();
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// country dropdown selected index changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindState(ddlState, Convert.ToInt64(ddlCountry.SelectedValue));
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// method for  bind country
        /// </summary>
        public void BindCountry()
        {
            try
            {
                ddlState.Enabled = false;

                CountryEntity countryentity = new CountryEntity();
                List<WSB_Country> countries = countryentity.GetAllCountry();
                if (countries != null && countries.Count > 0)
                {
                    ddlCountry.DataSource = countries;
                    ddlCountry.DataTextField = "CountryName";
                    ddlCountry.DataValueField = "CountryId";
                    ddlCountry.DataBind();
                }
                ddlCountry.Items.Insert(0, new ListItem(this.GetLocalResourceObject("SelectCountry").ToString(), "0"));
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// method for bind state dropdown
        /// </summary>
        /// <param name="ddlState"></param>
        /// <param name="countryId"></param>
        public void BindState(DropDownList ddlState, long countryId)
        {
            try
            {
                ddlState.Items.Clear();
                StateEntity stateEntity = new StateEntity();
                List<WSB_State> stateList = stateEntity.GetStateByCountryId(Convert.ToInt32(countryId));

                stateList = stateList.Where(x => x.IsActive == true).ToList();

                if (stateList != null && stateList.Count > 0)
                {
                    ddlState.DataSource = stateList;
                    ddlState.DataTextField = "StateName";
                    ddlState.DataValueField = "StateId";
                    ddlState.DataBind();
                    ddlState.Enabled = true;
                    ddlState.Focus();
                }
                else
                {
                    ddlState.Enabled = false;
                }
                ddlState.Items.Insert(0, new ListItem(this.GetLocalResourceObject("SelectState").ToString(), "0"));
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Clear Controls
        /// </summary>
        private void ClearControls()
        {
            txtCity.Text = string.Empty;
            txtComment.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtName.Text = string.Empty;
            txtSubject.Text = string.Empty;
            ddlCountry.SelectedValue = "0";
            ddlState.SelectedValue = "0";
        }

        /// <summary>
        /// function for sending mail
        /// </summary>
        public void SendInquiryMail()
        {
            try
            {
                string forgotEmailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("SendInquiry")).ToString();
                ContactUsEntity contactusentity = new ContactUsEntity();
                contactusentity.SendAdminMail(txtEmail.Text, txtSubject.Text, txtName.Text, txtComment.Text, forgotEmailTemplatePath);

                string visitormailtemplatePath = Server.MapPath(CommonLogic.GetConfigValue("SendVisitorMail")).ToString();
                contactusentity.SendVisitorMail(txtEmail.Text, txtName.Text, visitormailtemplatePath);
                txtEmail.Text = "";
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        #endregion
    }
}