using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho.Admin
{
    public partial class AddEditExpressShipment : PageBase
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(AddEditExpressShipment));
        public long Id
        {
            get
            {
                try
                {
                    return (!String.IsNullOrEmpty(CommonLogic.QueryString("id"))) ? Convert.ToInt32(Cryptography.DecryptStringAES(CommonLogic.QueryString("id"), CommonLogic.GetConfigValue("sharedSecret"))) : 0;
                }
                catch
                {
                    CommonHelper.ShowMessage(MessageType.Error, this.GetLocalResourceObject("URLHampered").ToString(), divMsg, lblMsg);
                    return 0;
                }
            }
        }
        #endregion

        /// <summary>
        /// Page Load Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindCountry();
                    BindCaseTypes();
                    if (Id > 0)
                        FillOrthocChargesDetail();
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occur", ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    OrthoCharge orthoCharge = null;

                    if (Id > 0)
                    {
                        orthoCharge = new OrthoChargesEntity().GetOrthoChargeById(Id);
                        if (orthoCharge != null)
                        {
                            orthoCharge.LastUpdatedBy = Authentication.GetLoggedUserID();
                        }
                    }
                    else
                    {
                        orthoCharge = new OrthoChargesEntity().Create();
                        orthoCharge.CountryId = Convert.ToInt64(ddlCountry.SelectedItem.Value);
                        orthoCharge.CreatedBy = orthoCharge.LastUpdatedBy = Authentication.GetLoggedUserID();
                        orthoCharge.IsActive = true;
                    }

                    orthoCharge.ExpressShipmentCharge = Convert.ToDecimal(txtExpressShipmentAmount.Text.Trim());
                    orthoCharge.CaseShipmentCharge = Convert.ToDecimal(txtCaseShipment.Text.Trim());

                    List<string> lstCaseIds = new List<string>();
                    foreach (ListItem item in lstbxCaseType.Items)
                        if (item.Selected)
                            lstCaseIds.Add(item.Value);

                    orthoCharge.CaseTypeIds = String.Join(",", lstCaseIds);

                    new OrthoChargesEntity().Save(orthoCharge);
                    Response.Redirect("ListOrthoCharges.aspx", false);
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occur", ex);
            }
        }

        private void FillOrthocChargesDetail()
        {
            OrthoCharge orthoCharge = new OrthoChargesEntity().GetOrthoChargeById(Id);
            if (orthoCharge != null)
            {
                ddlCountry.SelectedValue = Convert.ToString(orthoCharge.CountryId);
                txtExpressShipmentAmount.Text = orthoCharge.ExpressShipmentCharge.ToString("0.00");
                txtCaseShipment.Text = orthoCharge.CaseShipmentCharge.ToString("0.00");

                foreach (ListItem item in lstbxCaseType.Items)
                    if (orthoCharge.CaseTypeIds.Split(',').ToList().Exists(x => x == item.Value))
                        item.Selected = true;

                //foreach (string sId in orthoCharge.CaseTypeIds.Split(','))
                //    if (!string.IsNullOrEmpty(sId))
                //        lstbxCaseType.Items. = lstbxCaseType.Items.IndexOf(lstbxCaseType.Items.FindByValue(Convert.ToString(sId)));
                ddlCountry.Enabled = false;
            }
        }

        /// <summary>
        /// method for  bind country
        /// </summary>
        public void BindCountry()
        {
            try
            {
                CountryEntity countryentity = new CountryEntity();
                List<WSB_Country> countries = countryentity.GetAllCountry();

                //if (Id == 0)
                //{
                //    List<OrthoCharge> lstOrthoCharge = new OrthoChargesEntity().GetAllOrthoCharge();
                //    if (lstOrthoCharge != null)
                //        countries = countries.FindAll(x => !lstOrthoCharge.Exists(y => y.CountryId == x.CountryId));
                //}

                if (countries != null && countries.Count > 0)
                {
                    ddlCountry.DataSource = countries;
                    ddlCountry.DataTextField = "CountryName";
                    ddlCountry.DataValueField = "CountryId";
                    ddlCountry.DataBind();
                }
                ddlCountry.Items.Insert(0, new ListItem("SelectCountry", "0"));
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        private void BindCaseTypes()
        {
            List<LookUpDetailsByLookupType> lstCaseType = new LookupMasterEntity().GetLookUpDetails("CaseType");

            if (Id == 0)
            {
                List<CaseCharge> lstCaseCharge = new CaseChargesEntity().GetAllCaseCharge();

                if (lstCaseCharge != null)
                {
                    hdnAlreadyUseCaseId.Value = string.Join(",", lstCaseType.FindAll(x => lstCaseCharge.Exists(y => y.LookupCaseId == x.LookupId)).Select(x => x.LookupId));

                    //lstCaseType = lstCaseType.FindAll(x => !lstCaseCharge.Exists(y => y.LookupCaseId == x.LookupId));
                }
            }

            if (lstCaseType != null && lstCaseType.Count > 0)
            {
                lstbxCaseType.DataSource = lstCaseType;
                lstbxCaseType.DataTextField = "LookupName";
                lstbxCaseType.DataValueField = "LookupId";
                lstbxCaseType.DataBind();
            }
        }
    }
}