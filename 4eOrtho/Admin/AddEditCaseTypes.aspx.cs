using _4eOrtho.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.DAL;
using _4eOrtho.BAL;
using _4eOrtho.Utility;
using log4net;

namespace _4eOrtho.Admin
{
    public partial class AddEditCaseType : PageBase
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(AddEditCaseType));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindCaseTypeList();
                }
                lblMsg.Text = string.Empty;
                divMsg.Attributes.Remove("class");
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        private void BindCaseTypeList()
        {
            List<LookUpDetailsByLookupType> lstCaseTypes = new LookupMasterEntity().GetLookUpDetails("CaseType");
            if (lstCaseTypes != null)
            {
                lvCaseType.DataSource = lstCaseTypes;
                lvCaseType.DataBind();

                if (lstCaseTypes.Count > 0)
                {
                    hdnLookupCaseTypeId.Value = lstCaseTypes[0].LookupTypeId.ToString();
                }
                else
                {
                    List<LookupTypeMaster> lstLookupTypeMaster = new LookupTypeMasterEntity().GetLookupMasterDetails("CaseType");
                    if (lstLookupTypeMaster != null && lstLookupTypeMaster.Count > 0)
                    {
                        hdnLookupCaseTypeId.Value = lstLookupTypeMaster[0].LookupTypeId.ToString();
                    }
                }
            }
        }

        protected void Custom_Command(object sender, CommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "CUSTOMEDIT":
                        {
                            Label lblLookupName = (Label)(((ListViewItem)((Control)sender).NamingContainer).FindControl("lblLookupName"));
                            if (!string.IsNullOrEmpty(lblLookupName.Text.Trim()))
                            {
                                txtCaseType.Text = lblLookupName.Text.Trim();
                                hdnCaseTypeId.Value = e.CommandArgument.ToString();
                                custxtCaseType.Enabled = false;
                            }
                            break;
                        }
                    case "CUSTOMISACTIVE":
                        {
                            LookupMaster caseType = new LookupMasterEntity().GetLookupMasterById(Convert.ToInt64(e.CommandArgument));
                            if (caseType != null)
                            {
                                caseType.IsActive = !caseType.IsActive;
                                new LookupMasterEntity().Save(caseType);
                                BindCaseTypeList();
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (!string.IsNullOrEmpty(txtCaseType.Text))
                    {
                        LookupMaster caseType = null;
                        if (!string.IsNullOrEmpty(hdnCaseTypeId.Value))
                        {
                            caseType = new LookupMasterEntity().GetLookupMasterById(Convert.ToInt64(hdnCaseTypeId.Value));
                            caseType.LastUpdatedBy = SessionHelper.LoggedAdminUserID;
                        }
                        else
                        {
                            caseType = new LookupMasterEntity().Create();
                            caseType.IsActive = true;
                            caseType.CreatedBy = SessionHelper.LoggedAdminUserID;
                        }

                        caseType.LookupDescription = "BTIsRequired|" + chkIsBTRequired.Checked;
                        caseType.LookupName = txtCaseType.Text.Trim();
                        caseType.LookupTypeId = Convert.ToInt64(hdnLookupCaseTypeId.Value);
                        if (new LookupMasterEntity().Save(caseType) > 0)
                        {
                            BindCaseTypeList();
                            hdnCaseTypeId.Value = txtCaseType.Text = string.Empty;
                            custxtCaseType.Enabled = true;

                            CommonHelper.ShowMessage(MessageType.Success, Convert.ToString(this.GetLocalResourceObject("RecordSavedSuccessfully")), divMsg, lblMsg);
                        }
                        else
                        {
                            CommonHelper.ShowMessage(MessageType.Error, Convert.ToString(this.GetLocalResourceObject("FailedtoSaveRecord")), divMsg, lblMsg);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void custxtCaseType_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCaseType.Text.Trim()))
                {
                    LookupMaster caseType = new LookupMasterEntity().GetLookupMasterDetails(txtCaseType.Text.Trim());
                    args.IsValid = !(caseType != null);
                }
                else
                    args.IsValid = false;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
    }
}