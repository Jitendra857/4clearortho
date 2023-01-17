using System;
using System.Collections.Generic;
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
    public partial class EditRegistrationFees : PageBase
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(EditRegistrationFees));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<LookUpDetailsByLookupType> lookUpList = new LookupMasterEntity().GetLookUpDetails("RegistrationConfig");
                if (lookUpList != null && lookUpList.Count > 0)
                {
                    foreach (LookUpDetailsByLookupType obj in lookUpList)
                    {
                        if (obj != null)
                        {
                            switch (obj.LookupDescription.ToLower())
                            {
                                case "registrationfees":
                                    txtFees.Text = obj.LookupName;
                                    break;
                                case "firstcasediscount":
                                    txtPromotionalDiscount.Text = obj.LookupName;
                                    break;
                                case "firstcasediscountdate":
                                    txtDate.Text = Convert.ToString(Convert.ToDateTime(obj.LookupName).AddHours(-24).ToShortDateString().Replace('-', '/'));
                                    break;
                                case "casecharge":
                                    txtCaseCharge.Text = obj.LookupName;
                                    break;
                                case "retainercasecharge":
                                    txtRetainerCaseCharge.Text = obj.LookupName;
                                    break;
                                case "reworkcasecharge":
                                    txtReworkCaseCharge.Text = obj.LookupName;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<LookUpDetailsByLookupType> lookUpList = new LookupMasterEntity().GetLookUpDetails("RegistrationConfig");
                if (lookUpList != null && lookUpList.Count > 0)
                {
                    foreach (LookUpDetailsByLookupType obj in lookUpList)
                    {
                        if (obj != null)
                        {
                            switch (obj.LookupDescription.ToLower())
                            {
                                case "registrationfees":
                                    UpdateLookupData(obj.LookupDescription, txtFees.Text);
                                    break;
                                case "firstcasediscount":
                                    UpdateLookupData(obj.LookupDescription, txtPromotionalDiscount.Text);
                                    break;
                                case "firstcasediscountdate":
                                    DateTime date = Convert.ToDateTime(txtDate.Text).AddHours(24);
                                    UpdateLookupData(obj.LookupDescription, date.ToString());
                                    break;
                                case "casecharge":
                                    UpdateLookupData(obj.LookupDescription, txtCaseCharge.Text);
                                    break;
                                case "retainercasecharge":
                                    UpdateLookupData(obj.LookupDescription, txtRetainerCaseCharge.Text);
                                    break;
                                case "reworkcasecharge":
                                    UpdateLookupData(obj.LookupDescription, txtReworkCaseCharge.Text);
                                    break;
                            }
                        }
                    }
                    CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("UpdateSuccessfully").ToString(), divMsg, lblMsg);
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("UpdateFailed").ToString(), divMsg, lblMsg);
            }
        }

        private void UpdateLookupData(string lookupDesc, string lookupValue)
        {
            LookupMaster feesLookup = new LookupMasterEntity().GetLookupMasterByDesc(lookupDesc);
            if (feesLookup != null)
            {
                feesLookup.LookupName = lookupValue;
                new LookupMasterEntity().UpdateLookup(feesLookup);
            }
        }
    }
}