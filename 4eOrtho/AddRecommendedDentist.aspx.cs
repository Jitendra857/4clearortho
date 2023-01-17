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
    public partial class AddRecommendedDentist : PageBase
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(AddRecommendedDentist));
        int RecommendedId = 0;
        #endregion

        #region Events
        /// <summary>
        /// PAgeLoad Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ((CurrentSession)Session["UserLoginSession"] != null)
                {
                    PageRight pageRight = CheckRights(this.Page.GetType().BaseType.Name);
                    if (pageRight != null)
                    {
                        PageRedirect(pageRight.RedirectPageName);
                    }
                }
                if (!IsPostBack)
                {
                    BindCountry();
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// method for save data in recommendeddentist table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    RecommendedDentistEntity recmddentistEntity = new RecommendedDentistEntity();
                    RecommendDentist recmdDentist = new RecommendDentist();

                    if (RecommendedId > 0)
                    {
                        recmdDentist = recmddentistEntity.GetRecommendedDentistById(RecommendedId);
                        recmdDentist.LastUpdatedDate = BaseEntity.GetServerDateTime;
                        recmdDentist.LastUpdatedBy = 1;
                    }
                    else
                    {
                        recmdDentist = recmddentistEntity.Create();
                        recmdDentist.CreatedBy = 1;
                        recmdDentist.CreatedDate = BaseEntity.GetServerDateTime;
                    }

                    recmdDentist.FirstName = txtFirstName.Text.Trim();
                    recmdDentist.LastName = txtLastname.Text.Trim();
                    recmdDentist.Email = txtEmail.Text.Trim();
                    recmdDentist.CountryId = Convert.ToInt64(ddlCountry.SelectedItem.Value);
                    recmdDentist.StateId = Convert.ToInt64(ddlState.SelectedItem.Value);
                    recmdDentist.PatientId = ((CurrentSession)Session["UserLoginSession"]).PatientId;
                    recmdDentist.City = txtCity.Text.Trim();
                    recmdDentist.IsActive = true;
                    recmdDentist.IsDelete = false;
                    recmddentistEntity.Save(recmdDentist);
                    logger.Info("RecommededDentist Saved Succesfully");
                    SendRecommededDentistMail();
                    CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("RecommendMailSend").ToString(), divMsg, lblMsg);
                    ClearControls();                    
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// country drodown selectedchage event
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
        /// Clear Controls
        /// </summary>
        private void ClearControls()
        {
            txtFirstName.Text = string.Empty;
            txtLastname.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtCity.Text = string.Empty;
            ddlCountry.SelectedValue = "0";
            ddlState.SelectedValue = "0";
        }

        /// <summary>
        /// send recommended mail function
        /// </summary>
        public void SendRecommededDentistMail()
        {
            try
            {
                string recommendedEmailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("SendRecommendedDentistMailAdmin")).ToString();
                RecommendedDentistEntity recmddentistentity = new RecommendedDentistEntity();
                recmddentistentity.SendRecommendedDentistMailAdmin(txtEmail.Text, txtFirstName.Text, txtLastname.Text, ddlCountry.SelectedItem.Text, ddlState.SelectedItem.Text, txtCity.Text, recommendedEmailTemplatePath);

                if (txtEmail.Text != string.Empty)
                {
                    string recommendeddentistEmailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("SendRecommendedDentistMailDoctor")).ToString();
                    recmddentistentity.SendRecommendedDentistMailDoctor(txtEmail.Text, txtFirstName.Text, txtLastname.Text, ((CurrentSession)Session["UserLoginSession"]).PatientName, recommendeddentistEmailTemplatePath);
                    txtEmail.Text = "";
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// method for bind country dropdownlist
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
        /// for bind state dropdownlist
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
        #endregion
    }
}