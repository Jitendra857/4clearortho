using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.Utility;
using _4eOrtho.DAL;
using _4eOrtho.BAL;
using log4net;
using _4eOrtho.Helper;

namespace _4eOrtho.Admin
{
    public partial class AddEditClientTestimonial : PageBase
    {
        #region Declaration
        int ClientTestimoialId = 0;
        private ILog logger = log4net.LogManager.GetLogger(typeof(AddEditClientTestimonial));
        #endregion

        #region Events
        /// <summary>
        /// page load - binding client testimonail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(CommonLogic.QueryString("id")))
                    ClientTestimoialId = Convert.ToInt32(CommonLogic.QueryString("id"));

                if (!Page.IsPostBack)
                {
                    if (ClientTestimoialId > 0)
                    {
                        BindClientTestinmonealData(ClientTestimoialId);
                        lblHeader.Text = this.GetLocalResourceObject("lblHeaderResource2").ToString();
                        Page.Title = this.GetLocalResourceObject("PageResource2").ToString();
                    }
                    else
                    {
                        lblHeader.Text = this.GetLocalResourceObject("lblHeaderResource1.Text").ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// client testimonial page save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    ClientTestimonial clienttestimonial = new ClientTestimonial();
                    ClientTestimonialEntity clienttestimonialentity = new ClientTestimonialEntity();

                    if (ClientTestimoialId > 0)
                    {
                        clienttestimonial = clienttestimonialentity.GetClientTestimonialByID(ClientTestimoialId);
                        clienttestimonial.LastUpdatedDate = BaseEntity.GetServerDateTime;
                        clienttestimonial.LastUpdatedBy = 1;
                    }
                    else
                    {

                        clienttestimonial = clienttestimonialentity.Create();
                        clienttestimonial.CreatedDate = BaseEntity.GetServerDateTime;
                        clienttestimonial.CreatedBy = 1;
                    }
                    clienttestimonial.FirstName = txtFirstName.Text.Trim();
                    clienttestimonial.LastName = txtLastname.Text.Trim();
                    clienttestimonial.Email = txtEmail.Text.Trim();
                    clienttestimonial.PageContent = ucHTMLEditorControl.Text.Trim();
                    clienttestimonial.IsActive = chkIsActive.Checked;
                    clienttestimonial.IsDelete = false;
                    clienttestimonialentity.Save(clienttestimonial);
                    logger.Info("Testimonial Saved Successfully");
                    if (ClientTestimoialId > 0)
                    {
                        CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("UpdateClientTestimonial").ToString(), divMsg, lblMsg);
                    }
                    else
                    {
                        CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("InsertClientTestimonial").ToString(), divMsg, lblMsg);
                    }
                    Response.Redirect("ListClientTestimonial.aspx", false);

                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        #endregion

        #region Helpers
        /// <summary>
        /// bind client testimonial
        /// </summary>
        /// <param name="ClientTestimoialId"></param>
        public void BindClientTestinmonealData(int ClientTestimoialId)
        {
            try
            {
                ClientTestimonialEntity clienttesimonialentity = new ClientTestimonialEntity();
                ClientTestimonial clienttestimonial = clienttesimonialentity.GetClientTestimonialByID(ClientTestimoialId);

                if (clienttestimonial != null)
                {
                    txtFirstName.Text = clienttestimonial.FirstName;
                    txtLastname.Text = clienttestimonial.LastName;
                    txtEmail.Text = clienttestimonial.Email;
                    ucHTMLEditorControl.Text = clienttestimonial.PageContent;
                    chkIsActive.Checked = Convert.ToBoolean(clienttestimonial.IsActive);
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