using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;

namespace _4eOrtho.Admin
{
    public partial class AddEditState : PageBase
    {
        #region Declaration
        int stateId = 0;
        private ILog logger = log4net.LogManager.GetLogger(typeof(AddEditState));
        #endregion

        #region Events
        /// <summary>
        /// bind state details and bind country
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(CommonLogic.QueryString("id")))
                    stateId = Convert.ToInt32(CommonLogic.QueryString("id"));
                if (!IsPostBack)
                {
                    BindCountry();

                    if (stateId > 0)
                    {
                        lblHeader.Text = this.GetLocalResourceObject("lblHeaderResource2").ToString();
                        Page.Title = this.GetLocalResourceObject("PageResource2").ToString();
                        BindStateDetail(stateId);
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
        /// Save State
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    StateEntity stateentity = new StateEntity();
                    WSB_State state = new WSB_State();

                    if (stateId > 0)
                    {
                        state = stateentity.GetStateByStateId(stateId);
                        state.LastUpdatedDate = BaseEntity.GetServerDateTime;
                        state.LastUpdatedBy = 1;
                    }
                    else
                    {
                        state = stateentity.Create();
                        state.CreatedDate = BaseEntity.GetServerDateTime;
                        state.CreatedBy = 1;
                    }

                    state.StateName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtState.Text.Trim());
                    state.CountryId = Convert.ToInt32(ddlCountry.SelectedItem.Value);
                    state.IsActive = chkActive.Checked;
                    state.IsDelete = false;
                    stateentity.Save(state);
                    logger.Info("State Name Saved successfully");
                    if (stateId > 0)
                    {
                        CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("UpdateState").ToString(), divMsg, lblMsg);
                    }
                    else
                    {
                        CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("InsertState").ToString(), divMsg, lblMsg);
                    }
                    Response.Redirect("ListState.aspx", false);
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// check duplicate state name
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void custxtStateName_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                StateEntity stateEntity = new StateEntity();
                long GetStateID = stateEntity.GetStateIdByNameAndCountry(stateId, args.Value.ToString(), Convert.ToInt32(ddlCountry.SelectedValue.ToString()));

                if (GetStateID > 0)
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
            catch (Exception ex)
            {
                logger.Error("State Name validation process", ex);
            }
        }
        #endregion

        #region Helpers
        /// <summary>
        /// bind country
        /// </summary>
        public void BindCountry()
        {
            try
            {
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
        /// bind state details
        /// </summary>
        /// <param name="stateId"></param>
        public void BindStateDetail(int stateId)
        {
            try
            {
                WSB_State state = new WSB_State();
                StateEntity stateEntity = new StateEntity();
                state = stateEntity.GetStateByStateId(stateId);
                if (state != null)
                {
                    txtState.Text = state.StateName;
                    ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(Convert.ToString(state.CountryId)));
                    chkActive.Checked = state.IsActive;
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