using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;

namespace _4eOrtho.Admin
{
    public partial class AddEditSupplyOrder : PageBase
    {
        #region Declaration

        private long supplyOrderId = 0;
        private ProductMasterEntity productMasterEntity;
        private PackageMasterEntity packageMasterEntity;
        private SupplyOrderEntity supplyOrderEntity;
        private SupplyOrder supplyOrder;
        private ILog logger = log4net.LogManager.GetLogger(typeof(AddEditSupplyOrder));

        #endregion Declaration

        #region Events

        /// <summary>
        /// Page Load Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!String.IsNullOrEmpty(CommonLogic.QueryString("id")))
                    supplyOrderId = Convert.ToInt32(CommonLogic.QueryString("id"));

                if (!Page.IsPostBack)
                {
                    BindProductList();
                    if (supplyOrderId > 0)
                    {
                        BindSupplyOrder();
                        // lblHeader.Visible = false;

                        lblHeader.Text = this.GetLocalResourceObject("lblHeaderResource1.Text").ToString();
                        Page.Title = this.GetLocalResourceObject("PageResource2").ToString();
                    }
                    else
                    {
                        lblHeader.Text = this.GetLocalResourceObject("lblHeaderResource2").ToString();
                    }
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

        /// <summary>
        /// product or package bind as per selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rdblSupply_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdblSupply.SelectedIndex == 0)
            {
                BindProductList();
            }
            else
            {
                BindPackageList();
            }
        }

        /// <summary>
        /// supply order save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                supplyOrderEntity = new SupplyOrderEntity();
                if (supplyOrderId > 0)
                {
                    supplyOrder = supplyOrderEntity.GetSupplyOrderById(supplyOrderId);
                    supplyOrder.LastUpdatedBy = Authentication.GetLoggedUserID();
                    supplyOrder.LastUpdatedDate = BaseEntity.GetServerDateTime;
                }
                else
                {
                    supplyOrder = supplyOrderEntity.Create();
                    supplyOrder.CreatedBy = Authentication.GetLoggedUserID();
                    supplyOrder.CreatedDate = BaseEntity.GetServerDateTime;
                }
                string supplyName = string.Empty;
                if (rdblSupply.SelectedIndex == 0)
                {
                    supplyOrder.ProductId = Convert.ToInt32(ddlSuppply.SelectedValue.ToString());
                    supplyName = ddlSuppply.SelectedItem.ToString();
                    supplyOrder.PackageId = 0;
                }
                else
                {
                    supplyOrder.PackageId = Convert.ToInt32(ddlSuppply.SelectedValue.ToString());
                    supplyName = ddlSuppply.SelectedItem.ToString();
                    supplyOrder.ProductId = 0;
                }
                supplyOrder.EmailId = txtEmailid.Text;
                supplyOrder.Amount = Convert.ToDecimal(txtAmount.Text);
                supplyOrder.Quantity = Convert.ToInt32(txtQuantity.Text);
                supplyOrder.DoctorId = 0;
                supplyOrder.IsActive = chkIsActive.Checked;
                supplyOrder.IsDispatch = chkIsDispatch.Checked;
                supplyOrder.Remarks = txtDispatchRemarks.Text;
                supplyOrderEntity.Save(supplyOrder);
                string doctorEmailtemplatePath = Server.MapPath(CommonLogic.GetConfigValue("SendOrderSupplyMailDoctor")).ToString();
                string adminEmailtemplatePath = Server.MapPath(CommonLogic.GetConfigValue("SendOrderSupplyMailAdmin")).ToString();
                //supplyOrderEntity.SendOrderSupplyDoctorMail(supplyOrder.LastName + ", " + supplyOrder.FirstName, supplyName, txtAmount.Text, txtQuantity.Text, txtEmailid.Text, txtDispatchRemarks.Text, doctorEmailtemplatePath, "4ClearOrtho - Doctor Dispatch Order", txtTotalAmount.Text, "Dispatch", string.Empty);
                supplyOrderEntity.SendOrderSupplyDoctorMail(supplyOrder.LastName, supplyName, txtAmount.Text, txtQuantity.Text, txtEmailid.Text, txtDispatchRemarks.Text, doctorEmailtemplatePath, "4ClearOrtho - Doctor Dispatch Order", txtTotalAmount.Text, "Dispatch", string.Empty);
                supplyOrderEntity.SendOrderSupplyAdminMail("Admin", supplyName, txtAmount.Text, txtQuantity.Text, txtEmailid.Text, txtDispatchRemarks.Text, adminEmailtemplatePath, "4ClearOrtho - Doctor Dispatch Order", txtTotalAmount.Text, "Dispatch", string.Empty);

                if (supplyOrderId > 0)
                {
                    CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("SupplyOrderUpdate").ToString(), divMsg, lblMsg);
                }
                else
                {
                    CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("supplyOrderSave").ToString(), divMsg, lblMsg);
                }
                Response.Redirect("~/Admin/ListSupplyOrder.aspx", false);
            }
        }

        #endregion Events

        #region Helpers

        /// <summary>
        /// Bind order value by supplyorder id
        /// </summary>
        private void BindSupplyOrder()
        {
            supplyOrderEntity = new SupplyOrderEntity();
            supplyOrder = supplyOrderEntity.GetSupplyOrderById(supplyOrderId);
            if (supplyOrder != null)
            {
                if (supplyOrder.ProductId != 0)
                {
                    rdblSupply.SelectedIndex = 0;
                    ddlSuppply.SelectedValue = Convert.ToString(supplyOrder.ProductId);
                    //ddlSuppply.SelectedIndex = ddlSuppply.Items.IndexOf(ddlSuppply.Items.FindByValue(Convert.ToString(supplyOrder.ProductId)));
                }
                else
                {
                    BindPackageList();
                    rdblSupply.SelectedIndex = 1;
                    //ddlSuppply.SelectedIndex = ddlSuppply.Items.IndexOf(ddlSuppply.Items.FindByValue(Convert.ToString(supplyOrder.PackageId)));
                    ddlSuppply.SelectedValue = Convert.ToString(supplyOrder.PackageId);
                }
                txtEmailid.Text = Convert.ToString(supplyOrder.EmailId);
                txtAmount.Text = Convert.ToString(supplyOrder.Amount);
                txtQuantity.Text = Convert.ToString(supplyOrder.Quantity);
                chkIsActive.Checked = supplyOrder.IsActive;
                txtDispatchRemarks.Text = supplyOrder.Remarks;
                chkIsDispatch.Checked = supplyOrder.IsDispatch;
                txtTotalAmount.Text = Convert.ToString(supplyOrder.TotalAmount);
            }
            else
            {
                CommonHelper.ShowMessage(MessageType.Error, this.GetLocalResourceObject("URLwashampered").ToString(), divMsg, lblMsg);
                btnSubmit.Enabled = false;
            }
        }

        /// <summary>
        /// Bind product list
        /// </summary>
        private void BindProductList()
        {
            try
            {
                productMasterEntity = new ProductMasterEntity();
                List<ProductMaster> lstProductMaster = new List<ProductMaster>();
                lstProductMaster = productMasterEntity.GetProductMasters();
                ddlSuppply.DataSource = lstProductMaster;
                ddlSuppply.DataTextField = "ProductName";
                ddlSuppply.DataValueField = "ProductId";
                ddlSuppply.DataBind();
                ddlSuppply.Items.Insert(0, new ListItem(this.GetLocalResourceObject("SelectProduct").ToString(), "0"));
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Bind package list
        /// </summary>
        private void BindPackageList()
        {
            try
            {
                packageMasterEntity = new PackageMasterEntity();
                List<PackageMaster> lstProductMaster = new List<PackageMaster>();
                lstProductMaster = packageMasterEntity.GetPackageMaster();
                ddlSuppply.DataSource = lstProductMaster;
                ddlSuppply.DataTextField = "PackageName";
                ddlSuppply.DataValueField = "PackageId";
                ddlSuppply.DataBind();
                ddlSuppply.Items.Insert(0, new ListItem(this.GetLocalResourceObject("SelectPackage").ToString(), "0"));
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        #endregion Helpers


    }
}