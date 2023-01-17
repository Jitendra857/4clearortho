using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Utility;
using _4eOrtho.Helper;

namespace _4eOrtho.Admin
{
    public partial class AddEditRecommendedDentist : PageBase
    {        
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(AddEditRecommendedDentist));
        int RecommendedId;
        #endregion

        #region Events
        /// <summary>
        /// bind country and bind recommended dentist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(CommonLogic.QueryString("id")))
                    RecommendedId = Convert.ToInt32(CommonLogic.QueryString("id"));
                if (!IsPostBack)
                {
                    BindCountry();

                    if (RecommendedId > 0)
                    {
                        lblHeader.Text = this.GetLocalResourceObject("lblHeaderResource2").ToString();
                        Page.Title = this.GetLocalResourceObject("PageResource2").ToString();
                        BindRecommendedDentist(RecommendedId);
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
        /// submit recommendeded dentist
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
                    recmdDentist.PatientId = 1;
                    recmdDentist.City = txtCity.Text.Trim();
                    recmdDentist.IsActive = true;
                    recmdDentist.IsDelete = false;
                    recmddentistEntity.Save(recmdDentist);
                    logger.Info("RecommededDentist Saved Succesfully");
                    if (RecommendedId > 0)
                    {
                        CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("UpdateRecommendedDentist").ToString(), divMsg, lblMsg);
                    }
                    else
                    {
                        CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("InsertRecommendedDentist").ToString(), divMsg, lblMsg);
                    }
                    Response.Redirect("ListRecommendedDentist.aspx", false);
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// country dropdown selectedchage event
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

        #region Helper
        /// <summary>
        /// method for bind country dropdownlist
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
        
        /// <summary>
        /// bind recommendeded dentist
        /// </summary>
        /// <param name="RecommendedId"></param>
        public void BindRecommendedDentist(int RecommendedId)
        {
            try
            {
                RecommendedDentistEntity recmddentistEntity = new RecommendedDentistEntity();
                RecommendDentist recmdDentist = recmddentistEntity.GetRecommendedDentistById(RecommendedId);

                if (recmdDentist != null)
                {
                    txtFirstName.Text = recmdDentist.FirstName;
                    txtLastname.Text = recmdDentist.LastName;
                    txtEmail.Text = recmdDentist.Email;
                    ddlCountry.SelectedValue = recmdDentist.CountryId.ToString();
                    BindState(ddlState, Convert.ToInt64(ddlCountry.SelectedValue));
                    ddlState.SelectedValue = recmdDentist.StateId.ToString();
                    txtCity.Text = recmdDentist.City;
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