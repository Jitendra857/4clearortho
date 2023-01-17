using _4eOrtho.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using log4net;
using _4eOrtho.Utility;

namespace _4eOrtho.Admin
{
    public partial class AddEditCaseCharges : PageBase
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(AddEditCaseCharges));
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
                    CommonHelper.ShowMessage(MessageType.Error, "URLHampered".ToString(), divMsg, lblMsg);
                    return 0;
                }
            }
        }
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindCaseTypes();
                    BindDoctorList();
                    if (Id > 0)
                        FillCaseCharges();
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

        protected void custCouponCode_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DiscountMaster discount = new DiscountMasterEntity().GetDiscountMaster(txtCouponCode.Text);
            args.IsValid = !(discount != null);
            if (!args.IsValid)
                CommonHelper.ShowMessage(MessageType.Error, Convert.ToString(this.GetLocalResourceObject("custCouponCodeResource1.ErrorMessage")), divMsg, lblMsg);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!chkDiscount.Checked)
                {
                    rqvCouponCode.Enabled = false;
                    custCouponCode.Enabled = false;
                    rqvDiscountValue.Enabled = false;
                    rqvExpiryDate.Enabled = false;
                }

                if (Page.IsValid)
                {
                    SaveCaseCharge();
                    Response.Redirect("ListCaseCharges.aspx", false);
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

        protected void customcheckdoctotcase_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (DllDoctor.SelectedIndex > 0)
                {
                    CaseChargesEntity caseChargesEntity = new CaseChargesEntity();
                    if (caseChargesEntity.GetSpecialCaseForDoctor(Convert.ToInt64(ddlCaseType.SelectedItem.Value), DllDoctor.SelectedItem.Value, Id) != null)
                        args.IsValid = false;
                    else
                        args.IsValid = true;
                }
                else
                {
                    args.IsValid = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        #endregion

        #region Helper
       
        private void BindDoctorList()
        {
            try
            {
                DoctorEntity doctor = new DoctorEntity();
                List<GetDoctorListFromOrthoAndAAAD_Result> doctorlist = doctor.GetAllGetDoctorListFromOrthoAndAAAD();
                DllDoctor.Items.Clear();
                if (doctorlist.Count > 0)
                {
                    DllDoctor.DataSource = doctorlist;
                    DllDoctor.DataTextField = "DoctorName";
                    DllDoctor.DataValueField = "EmailId";
                    DllDoctor.DataBind();
                }
                DllDoctor.Items.Insert(0, new ListItem("Select Doctor", "0"));
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        private void FillCaseCharges()
        {
            CaseCharge caseCharge = new CaseChargesEntity().GetCaseCharge(Id);
            if (caseCharge != null)
            {
                ddlCaseType.SelectedIndex = ddlCaseType.Items.IndexOf(ddlCaseType.Items.FindByValue(Convert.ToString(caseCharge.LookupCaseId)));
                ddlCaseType.Enabled = false;
                txtCaseCharge.Text = caseCharge.Amount.ToString("0.00");
                if (!string.IsNullOrEmpty(caseCharge.DoctorEmailId))
                    DllDoctor.Items.FindByValue(caseCharge.DoctorEmailId.ToString()).Selected = true;
                if (caseCharge.DiscountMaster != null)
                {
                    txtCouponCode.Text = caseCharge.DiscountMaster.CouponCode;
                    rbtnAmount.Checked = caseCharge.DiscountMaster.IsFlat;
                    rbtnPercentage.Checked = !rbtnAmount.Checked;
                    txtDiscountValue.Text = caseCharge.DiscountMaster.Amount.ToString("0.00");
                    txtExpiryDate.Text = Convert.ToDateTime(caseCharge.DiscountMaster.ExpiryDate).ToString("MM/dd/yyyy");

                    rqvCouponCode.Enabled = false;
                    custCouponCode.Enabled = false;
                    txtCouponCode.Enabled = false;
                    ddlCaseType.Enabled = false;
                }
                else
                    chkDiscount.Checked = false;
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
                ddlCaseType.DataSource = lstCaseType;
                ddlCaseType.DataTextField = "LookupName";
                ddlCaseType.DataValueField = "LookupId";
                ddlCaseType.DataBind();
            }
            ddlCaseType.Items.Insert(0, new ListItem(Convert.ToString(this.GetLocalResourceObject("SelectCaseType")), "0"));
        }     

        private void SaveCaseCharge()
        {
            CaseCharge caseCharge = null;
            DiscountMaster discount = null;

            if (Id > 0)
            {
                caseCharge = new CaseChargesEntity().GetCaseCharge(Id);
                discount = caseCharge.DiscountMaster;
            }
            else
            {
                caseCharge = new CaseChargesEntity().Create();
                discount = new DiscountMasterEntity().Create();
                caseCharge.LookupCaseId = Convert.ToInt64(ddlCaseType.SelectedItem.Value);
            }

            caseCharge.DoctorEmailId = DllDoctor.SelectedItem.Value;
            caseCharge.Amount = Convert.ToDecimal(txtCaseCharge.Text.Trim());
            caseCharge.IsActive = true;
            caseCharge.CreatedBy = caseCharge.LastUpdatedBy = SessionHelper.LoggedAdminUserID;

            if (chkDiscount.Checked)
            {
                discount = discount == null ? new DiscountMasterEntity().Create() : discount;
                discount.Amount = Convert.ToDecimal(txtDiscountValue.Text.Trim());
                discount.CouponCode = txtCouponCode.Text.Trim();
                discount.IsFlat = rbtnAmount.Checked;
                discount.ExpiryDate = Convert.ToDateTime(txtExpiryDate.Text.Trim());
                discount.IsActive = true;
                caseCharge.DiscountMaster = discount;
            }
            else
            {
                if (discount != null)
                {
                    discount.IsDelete = true;
                    discount.IsActive = false;
                }
            }

            new CaseChargesEntity().Save(caseCharge);
        }

        #endregion
    }
}