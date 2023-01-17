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
    public partial class EditStageFees : PageBase
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(EditStageFees));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<LookUpDetailsByLookupType> lookUpList = new LookupMasterEntity().GetLookUpDetails("Stage Fees");
                if (lookUpList != null && lookUpList.Count > 0)
                {
                    foreach (LookUpDetailsByLookupType obj in lookUpList)
                    {
                        if (obj != null)
                        {
                            if (obj.LookupDescription == "Stage Fees")
                            {
                                txtFees.Text = obj.LookupName;
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
                UpdateLookupData("Stage Fees", txtFees.Text);
                CommonHelper.ShowMessage(MessageType.Success, "Records updated successfully.", divMsg, lblMsg);
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                CommonHelper.ShowMessage(MessageType.Success, "Failed to update records.", divMsg, lblMsg);
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