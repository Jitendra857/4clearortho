using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using _4eOrtho.Utility;
using _4eOrtho.DAL;
using _4eOrtho.BAL;
using _4eOrtho.Helper;
using System.Globalization;

namespace _4eOrtho.Admin
{
    public partial class AddEditCountry : PageBase
    {
        #region Declaration
        int countryId = 0;
        private ILog logger = log4net.LogManager.GetLogger(typeof(AddEditCountry));
        #endregion

        #region Events
        /// <summary>
        /// bind country in page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(CommonLogic.QueryString("id")))
                    countryId = Convert.ToInt32(CommonLogic.QueryString("id"));
                if (!Page.IsPostBack)
                {
                    if (countryId > 0)
                    {
                        lblHeader.Text = this.GetLocalResourceObject("lblHeaderResource2").ToString();
                        Page.Title = this.GetLocalResourceObject("PageResource2").ToString();
                        BindCountry(countryId);
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
        /// Save Country
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    CountryEntity countryentity = new CountryEntity();
                    WSB_Country country = new WSB_Country();

                    if (countryId > 0)
                    {
                        country = countryentity.GetCountryByCountryId(countryId);
                        country.LastUpdatedDate = BaseEntity.GetServerDateTime;
                        country.LastUpdatedBy = 1;
                    }
                    else
                    {
                        country = countryentity.Create();
                        country.CreatedBy = 1;
                        country.CreatedDate = BaseEntity.GetServerDateTime;
                    }

                    country.CountryName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtCountryName.Text.Trim());
                     
                    country.IsActive = chkActive.Checked;
                    country.IsDelete = false;
                    countryentity.Save(country);
                    logger.Info("Country Saved Successfully");
                    if (countryId > 0)
                    {
                        CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("UpdateCountry").ToString(), divMsg, lblMsg);
                    }
                    else
                    {
                        CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("InsertCountry").ToString(), divMsg, lblMsg);
                    }
                    Response.Redirect("ListCountry.aspx", false);
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
        /// Bind Country
        /// </summary>
        /// <param name="countryId"></param>
        public void BindCountry(int countryId)
        {
            try
            {
                WSB_Country country = new WSB_Country();
                CountryEntity countryentity = new CountryEntity();

                country = countryentity.GetCountryByCountryId(countryId);
                if (country != null)
                {
                    txtCountryName.Text = country.CountryName;
                    chkActive.Checked = country.IsActive;
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// Country server validate change events check duplicate country
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void custxtCountryName_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                CountryEntity countryEntity = new CountryEntity();
                int GetCountryID = countryEntity.GetCountryIdByName(countryId, args.Value.ToString());

                if (GetCountryID > 0)
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
            catch (Exception ex)
            {
                logger.Error("Country Name validation process", ex);
            }
        }
        #endregion
    }
}