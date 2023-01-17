using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho
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
        private string dispatched = string.Empty;
        private ProductGalleryEntity productGalleryEntity;
        private List<ProductGallery> productGalleries;
        private PackageGalleryEntity packageGalleryEntity;
        private List<PackageGallery> packageGalleries;

        PayPalAPIInterfaceServiceService payPalService;
        DoDirectPaymentResponseType payPalServiceResponse;
        long paymentId = 0;
        int currentMonth = 0;
        int currentYear = 0;
        int selectedMonth = 0;
        int selectedYear = 0;
        bool isPaypalExpress = false;
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
                if ((CurrentSession)Session["UserLoginSession"] != null)
                {
                    PageRight pageRight = CheckRights(this.Page.GetType().BaseType.Name);
                    if (pageRight != null)
                    {
                        PageRedirect(pageRight.RedirectPageName);
                    }
                }
                if (!String.IsNullOrEmpty(CommonLogic.QueryString("id")))
                    supplyOrderId = Convert.ToInt32(CommonLogic.QueryString("id"));

                if (!String.IsNullOrEmpty(CommonLogic.QueryString("dispatched")))
                    dispatched = CommonLogic.QueryString("dispatched");

                if (!Page.IsPostBack)
                {
                    BindProductList();
                    if (supplyOrderId > 0)
                    {
                        BindSupplyOrder(dispatched);
                        this.GetLocalResourceObject("PageResource2").ToString();
                        this.GetLocalResourceObject("lblHeaderResource2").ToString();
                        btnSubmit.Visible = false;
                        ddlSuppply.Enabled = false;
                        txtAmount.Enabled = false;
                        txtQuantity.Enabled = false;
                        txtTotalAmount.Enabled = false;
                        rdblSupply.Enabled = false;
                        btnReset.Visible = false;
                    }
                    else
                    {
                        this.GetLocalResourceObject("PageResource2").ToString();
                        this.GetLocalResourceObject("lblHeaderResource1.Text").ToString();
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
            try
            {
                if (rdblSupply.SelectedIndex == 0)
                {
                    BindProductList();
                    rptPackageImage.DataSource = null;
                    rptPackageImage.DataBind();
                    dvPackageImagelist.InnerHtml = string.Empty;
                    flPackageDetails.Visible = false;
                    txtAmount.Text = string.Empty;
                    txtQuantity.Text = string.Empty;
                    txtTotalAmount.Text = string.Empty;
                }
                else
                {
                    BindPackageList();
                    dvProductImagelist.InnerHtml = string.Empty;
                    flPackageDetails.Visible = true;
                    txtAmount.Text = string.Empty;
                    txtQuantity.Text = string.Empty;
                    txtTotalAmount.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// calculate total amount on text change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalAmount();
        }

        /// <summary>
        /// calculate total amount on text change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalAmount();
        }

        /// <summary>
        /// on dropdown selected change bind product amount to text box and calculate total amount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSuppply_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdblSupply.SelectedIndex == 0)
                {
                    productMasterEntity = new ProductMasterEntity();
                    ProductMaster productMaster = new ProductMaster();
                    productMaster = productMasterEntity.GetProductByProductId(Convert.ToInt32(ddlSuppply.SelectedValue));

                    if (productMaster != null)
                        txtAmount.Text = Convert.ToString(productMaster.Amount);

                    ProductBind(Convert.ToInt64(ddlSuppply.SelectedValue));
                }
                else
                {
                    packageMasterEntity = new PackageMasterEntity();
                    PackageMaster packageMaster = new PackageMaster();
                    packageMaster = packageMasterEntity.GetPackageByPackageId(Convert.ToInt64(ddlSuppply.SelectedValue));

                    if (packageMaster != null)
                        txtAmount.Text = Convert.ToString(packageMaster.Amount);

                    PackageBind(Convert.ToInt64(ddlSuppply.SelectedValue));
                }
                CalculateTotalAmount();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// This will validate card number by selected card type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void custom_ServerCardValidate(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = (ValidateCardNoByCardType(e.Value.Trim()));
        }

        /// <summary>
        /// This will check selected month is history or not is selected or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void custom_ServerMonthValidate(object sender, ServerValidateEventArgs e)
        {
            SetCurrentAndSelectedMonthYear();
            e.IsValid = !(currentYear == selectedYear && selectedMonth < currentMonth);
        }

        /// <summary>
        /// on dropdown selected change set ccvno maxlength base on cardtype selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCCVNo.MaxLength = (ddlCardType.SelectedItem.Value == "AMEX") ? 4 : 3;
        }

        /// <summary>
        /// supply order save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            phDetail.Visible = false;
            phMakePayment.Visible = true;
            btnSubmit.Visible = false;
            btnMakePayment.Visible = false;
            btnBackPayment.Visible = true;
        }

        /// <summary>
        /// Event to submit payment data and payment using paypal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMakePayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                    SaveSupplyOrder();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Event to redirect back page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (divCreaditCard.Visible)
                {
                    divSelectPayment.Visible = true;
                    divCreaditCard.Visible = false;
                    phMakePayment.Visible = true;
                    phDetail.Visible = false;
                }
                else if (divSelectPayment.Visible)
                {
                    phDetail.Visible = true;
                    phMakePayment.Visible = false;
                    btnMakePayment.Visible = false;
                    btnBackPayment.Visible = false;
                    btnSubmit.Visible = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        #endregion Events

        #region Helpers

        /// <summary>
        /// package bind to repeater packagename
        /// </summary>
        /// <param name="packageId"></param>
        public void PackageBind(long packageId)
        {
            packageGalleryEntity = new PackageGalleryEntity();
            packageGalleries = packageGalleryEntity.GetPackageGalleriesByPackageId(Convert.ToInt32(ddlSuppply.SelectedValue));
            StringBuilder imageHtml = new StringBuilder();
            if (packageGalleries.Count > 0)
            {
                imageHtml.AppendLine("<div style='float:left;width:200px;'><div class='parsonal_textfild'>");
                imageHtml.AppendLine("<span id='ContentPlaceHolder1_lblProductImage'>" + this.GetLocalResourceObject("PackageImage") + "</span><span class='alignright'>:<span class='asteriskclass'>&nbsp;</span></span></div></div>");
                imageHtml.AppendLine("<div class='date_cont'><div style='float: left;margin-left: 5px;width: 350px;'>");
                foreach (PackageGallery packageGallery in packageGalleries)
                {
                    imageHtml.AppendLine("<img src='Files/thumbs/" + packageGallery.FileName + "' height='100' width='100'>");
                }
                imageHtml.AppendLine("</div></div>");
            }
            dvPackageImagelist.InnerHtml = imageHtml.ToString();
            List<PackageDetails> lstPackageDetails = new List<PackageDetails>();
            lstPackageDetails = new PackageMasterEntity().GetPackageDetailsByPackageId(Convert.ToInt64(ddlSuppply.SelectedValue));
            if (lstPackageDetails.Count > 0)
            {
                rptPackageImage.DataSource = lstPackageDetails;
                rptPackageImage.DataBind();
            }
        }

        /// <summary>
        /// product bind to repeater
        /// </summary>
        /// <param name="packageId"></param>
        public void ProductBind(long productId)
        {
            productGalleryEntity = new ProductGalleryEntity();
            productGalleries = productGalleryEntity.GetProductGalleriesByProductId(Convert.ToInt32(ddlSuppply.SelectedValue));
            StringBuilder imageHtml = new StringBuilder();
            imageHtml.AppendLine("<div style='float:left;width:200px;'><div class='parsonal_textfild'>");
            imageHtml.AppendLine("<span id='ContentPlaceHolder1_lblProductImage'>" + this.GetLocalResourceObject("PackageImage") + "</span><span class='alignright'>:<span class='asteriskclass'>&nbsp;</span></span></div></div>");
            imageHtml.AppendLine("<div class='date_cont'><div style='float: left;margin-left: 5px;width: 350px;'>");
            foreach (ProductGallery productGallery in productGalleries)
            {
                //imageHtml.AppendLine("<img src='ProductFiles/thumbs/" + productGallery.FileName + "' height='100' width='100'>");
                imageHtml.AppendLine("<a class='example-image-link' title='" + this.GetLocalResourceObject("ViewImageFull") + "' href='ProductFiles/" + productGallery.FileName + "' data-lightbox='example-1'><img class='example-image' src='ProductFiles/thumbs/" + productGallery.FileName + "' height='100' width='100'></a>");
            }
            imageHtml.AppendLine("</div></div>");
            dvProductImagelist.InnerHtml = imageHtml.ToString();
            rptPackageImage.DataSource = null;
            rptPackageImage.DataBind();
        }

        /// <summary>
        /// Bind order value by supplyorder id
        /// </summary>
        private void BindSupplyOrder(string dispatched)
        {
            try
            {
                if (dispatched == "true")
                {
                    dvDispatchRemarks.Visible = true;
                    dvRecieved.Visible = true;
                    ddlSuppply.Enabled = false;
                    rdblSupply.Enabled = false;
                    txtAmount.Enabled = false;
                    txtQuantity.Enabled = false;
                    btnMakePayment.Visible = true;
                    btnMakePayment.Text = "Submit";
                    btnReset.Visible = true;
                }
                supplyOrderEntity = new SupplyOrderEntity();
                supplyOrder = supplyOrderEntity.GetSupplyOrderById(supplyOrderId);
                if (supplyOrder != null)
                {
                    if (supplyOrder.ProductId != 0)
                    {
                        rdblSupply.SelectedIndex = 0;
                        ddlSuppply.SelectedValue = Convert.ToString(supplyOrder.ProductId);
                        ProductBind(supplyOrder.ProductId);
                        flPackageDetails.Visible = false;
                        //ddlSuppply.SelectedIndex = ddlSuppply.Items.IndexOf(ddlSuppply.Items.FindByValue(Convert.ToString(supplyOrder.ProductId)));
                    }
                    else
                    {
                        BindPackageList();
                        rdblSupply.SelectedIndex = 1;
                        //ddlSuppply.SelectedIndex = ddlSuppply.Items.IndexOf(ddlSuppply.Items.FindByValue(Convert.ToString(supplyOrder.PackageId)));
                        ddlSuppply.SelectedValue = Convert.ToString(supplyOrder.PackageId);
                        PackageBind(supplyOrder.PackageId);
                        flPackageDetails.Visible = true;
                    }
                    //txtEmailid.Text = Convert.ToString(supplyOrder.EmailId);
                    txtAmount.Text = Convert.ToString(supplyOrder.Amount);
                    lblDispatchRemarks.Text = supplyOrder.Remarks;
                    txtQuantity.Text = Convert.ToString(supplyOrder.Quantity);
                    txtTotalAmount.Text = Convert.ToString(supplyOrder.TotalAmount);
                }
                else
                {
                    CommonHelper.ShowMessage(MessageType.Error, "URL was hampered", divMsg, lblMsg);
                    btnSubmit.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
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
                ddlSuppply.Items.Insert(0, new ListItem("Select Product", "0"));
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
                ddlSuppply.DataSource = lstProductMaster.FindAll(f => !f.IsCase);
                ddlSuppply.DataTextField = "PackageName";
                ddlSuppply.DataValueField = "PackageId";
                ddlSuppply.DataBind();
                ddlSuppply.Items.Insert(0, new ListItem("Select Package", "0"));
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
        /// calculate total amount
        /// </summary>
        public void CalculateTotalAmount()
        {
            try
            {
                if (txtAmount.Text != "" && txtQuantity.Text != "")
                    txtTotalAmount.Text = Convert.ToString(Convert.ToDecimal(txtAmount.Text) * Convert.ToInt32(txtQuantity.Text));
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        private void SaveSupplyOrder()
        {
            TransactionScope scope = new TransactionScope();
            bool bSuccess = false;
            string supplyName = string.Empty;
            string mailSubject = string.Empty;
            string status = string.Empty;
            string titleDoctor = string.Empty;
            string titleAdmin = string.Empty;
            string redirectto = string.Empty;

            using (scope)
            {
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                if (currentSession != null)
                {
                    supplyOrderEntity = new SupplyOrderEntity();

                    _4eOrtho.DAL.PaymentSuccess payment = null;

                    if (supplyOrderId > 0)
                        payment = new PaymentSuccessEntity().GetPaymentInfo(supplyOrderId, 0);

                    if (payment == null && !isPaypalExpress)
                    {
                        CallPayPalServiceForPayment();
                        bSuccess = payPalServiceResponse.Ack.Value.ToString().ToUpper().Equals("SUCCESS");
                        redirectto = "~/paymentsuccess.aspx";
                        Session["redirect"] = "~/ListSupplyOrder.aspx";
                        if (bSuccess)
                        {
                            SessionHelper.PaymentAmount = payPalServiceResponse.Amount.value.ToString();
                            SessionHelper.TransactionId = payPalServiceResponse.TransactionID.ToString();
                            SessionHelper.TransactionTime = Convert.ToDateTime(payPalServiceResponse.Timestamp.ToString()).ToString("dd-MMM-yyyy HH:mm:ss");
                        }
                    }
                    else
                    {
                        bSuccess = true;
                        redirectto = "~/ListSupplyOrder.aspx";
                    }

                    if (bSuccess || isPaypalExpress)
                    {
                        logger.Error("bSuccess. " + bSuccess);

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
                        supplyOrder.FirstName = currentSession.DoctorFirstName;
                        supplyOrder.LastName = currentSession.DoctorLastName;
                        supplyOrder.EmailId = currentSession.EmailId;
                        supplyOrder.Amount = Convert.ToDecimal(txtAmount.Text);
                        supplyOrder.Quantity = Convert.ToInt32(txtQuantity.Text);
                        supplyOrder.DoctorId = 0;
                        supplyOrder.IsActive = true;
                        supplyOrder.IsRecieved = chkIsRecieved.Checked;
                        supplyOrder.TotalAmount = Convert.ToDecimal(txtTotalAmount.Text);

                        if (!isPaypalExpress)
                        {
                            supplyOrderEntity.Save(supplyOrder);

                            if (chkIsRecieved.Checked)
                            {
                                supplyOrder.IsDispatch = true;
                                mailSubject = "4ClearOrtho - Doctor Order Received";
                                titleAdmin = "The following order has been received by Doctor.";
                                titleDoctor = "You have received below item(s) as per your order.";
                                status = "Order Received";
                            }
                            else
                            {
                                supplyOrder.IsDispatch = false;
                                mailSubject = "4ClearOrtho - Doctor Order Supply";
                                titleAdmin = "The following order details requested by doctor.";// changed by vishal gondaliya
                                titleDoctor = "The Order Supply details are following.";
                                status = "Order Supply";
                            }

                            string doctorEmailtemplatePath = Server.MapPath(CommonLogic.GetConfigValue("DoctorSendOrderSupplyMailDoctor")).ToString();
                            string adminEmailtemplatePath = Server.MapPath(CommonLogic.GetConfigValue("DoctorSendOrderSupplyMailAdmin")).ToString();
                            string emailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("SendOrderSupplyWithPayment")).ToString();
                            if (currentSession != null)
                            {
                                if (chkIsRecieved.Checked)
                                {
                                    supplyOrderEntity.SendOrderSupplyDoctorMail(currentSession.DoctorLastName, supplyName, txtAmount.Text, txtQuantity.Text, currentSession.EmailId, "", doctorEmailtemplatePath, mailSubject, txtTotalAmount.Text, status, titleDoctor);
                                    supplyOrderEntity.SendOrderSupplyAdminMail(currentSession.DoctorLastName, supplyName, txtAmount.Text, txtQuantity.Text, currentSession.EmailId, "", adminEmailtemplatePath, mailSubject, txtTotalAmount.Text, status, titleAdmin);
                                }
                                else
                                {
                                    supplyOrderEntity.SendOrderSupplyMailWithPayment(currentSession.DoctorLastName, supplyName, txtAmount.Text, txtQuantity.Text, currentSession.EmailId, "", emailTemplatePath, mailSubject, txtTotalAmount.Text, status, titleDoctor, false);
                                    supplyOrderEntity.SendOrderSupplyMailWithPayment(currentSession.DoctorLastName, supplyName, txtAmount.Text, txtQuantity.Text, currentSession.EmailId, "", emailTemplatePath, mailSubject, txtTotalAmount.Text, status, titleAdmin, true);
                                }
                            }
                            CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject(((supplyOrderId > 0) ? "OrderUpdateSuccessfully" : "OrderSaveSuccessfully")).ToString(), divMsg, lblMsg);
                        }
                        else
                        {
                            Session["supplyOrder"] = supplyOrder;
                            Session["supplyName"] = supplyName;
                        }

                        if (payment == null || isPaypalExpress)
                            SavePaymentSuccessStatus(supplyOrder.SupplyOrderId);
                    }
                    else if (payPalServiceResponse.Ack.Value.ToString().ToUpper().Equals("FAILURE"))
                    {
                        logger.Error("payPalServiceResponse.Ack.Value. " + payPalServiceResponse.Ack.Value);

                        SessionHelper.PayPalServiceResponseErrors = payPalServiceResponse.Errors;
                        SessionHelper.PaymentAmount = payPalServiceResponse.Amount.value.ToString();                        
                        SessionHelper.TransactionTime = Convert.ToDateTime(payPalServiceResponse.Timestamp.ToString()).ToString("dd-MMM-yyyy HH:mm:ss");
                        SavePaymentFailureStatus();
                        redirectto = "~/PaymentFailure.aspx";
                        Session["redirect"] = "~/ListSupplyOrder.aspx";
                    }
                    logger.Error("End " + redirectto);

                    scope.Complete();
                    Response.Redirect(redirectto, false);
                }
            }
        }

        #endregion Helpers

        #region Paypal Helpers
        /// <summary>
        /// This method will bind year list
        /// </summary>
        private void BindYearList()
        {
            var yearList = new Dictionary<int, string>();
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear; i < (currentYear + 15); i++)
            {
                yearList.Add(i, i.ToString());
            }
            ddlYear.DataSource = yearList;
            ddlYear.DataTextField = "Value";
            ddlYear.DataValueField = "Key";
            ddlYear.DataBind();
            ddlYear.SelectedValue = currentYear.ToString();
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
        }

        /// <summary>
        /// This function will gether information and call paypal service for payment
        /// </summary>
        /// <param name="userInfo"></param>
        private void CallPayPalServiceForPayment()
        {
            try
            {
                DoDirectPaymentRequestType request = new DoDirectPaymentRequestType();
                DoDirectPaymentRequestDetailsType requestDetails = new DoDirectPaymentRequestDetailsType();
                request.DoDirectPaymentRequestDetails = requestDetails;

                requestDetails.PaymentAction = PaymentActionCodeType.SALE;
                requestDetails.CreditCard = GetCreditCardDetail();
                requestDetails.PaymentDetails = GetPaymentDetailInfo();

                // Invoke the API
                DoDirectPaymentReq wrapper = new DoDirectPaymentReq();
                wrapper.DoDirectPaymentRequest = request;
                payPalService = new PayPalAPIInterfaceServiceService();
                payPalServiceResponse = payPalService.DoDirectPayment(wrapper);
            }
            catch (Exception exp)
            {
                logger.Error("An error occured.", exp);
            }
        }

        /// <summary>
        /// This function will prepare credit card detail
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private CreditCardDetailsType GetCreditCardDetail()
        {
            try
            {
                CreditCardDetailsType creditCard = new CreditCardDetailsType();
                creditCard.CardOwner = GetCrediCardPayerInfo();
                creditCard.CreditCardNumber = txtCardNo.Text.ToString().Trim();
                creditCard.CreditCardType = (CreditCardTypeType)Enum.Parse(typeof(CreditCardTypeType), ddlCardType.SelectedValue);
                creditCard.CVV2 = txtCCVNo.Text.ToString().Trim();
                creditCard.ExpMonth = Convert.ToInt32(ddlMonth.SelectedValue.ToString());
                creditCard.ExpYear = Convert.ToInt32(ddlYear.SelectedValue.ToString());
                return creditCard;
            }
            catch (Exception exp)
            {
                logger.Error("An error occured.", exp);
                throw exp;
            }
        }

        /// <summary>
        /// This function will prepare credit card payer information
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private PayerInfoType GetCrediCardPayerInfo()
        {
            try
            {
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];

                AddressType address = new AddressType()
                {
                    CityName = currentSession.DoctorCity != null ? currentSession.DoctorCity.Trim() : string.Empty,
                    CountryName = currentSession.DoctorCountry != null ? currentSession.DoctorCountry.Trim() : string.Empty,
                    Name = currentSession.DoctorFirstName != null ? currentSession.DoctorFirstName.Trim() + " " + currentSession.DoctorLastName.Trim() : string.Empty,
                    Phone = currentSession.DoctorMobile != null ? currentSession.DoctorMobile.Trim() : string.Empty,
                    PostalCode = currentSession.DoctorZipcode != null ? currentSession.DoctorZipcode.Trim() : string.Empty,
                    Street1 = currentSession.DoctorStreet != null ? currentSession.DoctorStreet.Trim() : string.Empty,
                    Street2 = currentSession.DoctorState != null ? currentSession.DoctorState.Trim() : string.Empty
                };

                PersonNameType name = new PersonNameType()
                {
                    FirstName = currentSession.DoctorFirstName != null ? currentSession.DoctorFirstName : string.Empty,
                    LastName = currentSession.DoctorLastName != null ? currentSession.DoctorLastName : string.Empty
                };

                PayerInfoType payer = new PayerInfoType()
                {
                    Address = address,
                    ContactPhone = currentSession.DoctorMobile != null ? currentSession.DoctorMobile.Trim() : string.Empty,
                    PayerName = name
                };

                return payer;
            }
            catch (Exception exp)
            {
                logger.Error("An error occured.", exp);
                throw exp;
            }
        }

        /// <summary>
        /// This function will prepare payment information
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private PaymentDetailsType GetPaymentDetailInfo()
        {
            try
            {
                PaymentDetailsType paymentDetail = new PaymentDetailsType();
                BasicAmountType paymentAmount = new BasicAmountType()
                {
                    currencyID = CurrencyCodeType.USD,
                    value = GetPayableAmount()
                };
                paymentDetail.OrderTotal = paymentAmount;
                return paymentDetail;
            }
            catch (Exception exp)
            {
                logger.Error("An error occured.", exp);
                throw exp;
            }
        }

        ///// <summary>
        ///// This function will send mail to user.
        ///// </summary>
        //private void SendPaymentSuccessEmail()
        //{
        //    try
        //    {
        //        ////and paid $    for the same.
        //        //string PackageName = "", Amount = "", Duration = "";
        //        //string DiscountDetail = string.Empty;
        //        //string trtd = "<tr><td style='width:200px;'>##DETAIL##</td><td align='right'>$##AMOUNT##</td></tr>";
        //        //string FinalHtml = string.Empty;
        //        //if (userInfo.SelectedPackageId > 0)
        //        //{
        //        //    WebsiteBuilder.BL.PackageEntity packageEntity = new PackageEntity();
        //        //    WSB_Packages pkg = packageEntity.GetPackageById(userInfo.SelectedPackageId);

        //        //    if (pkg != null)
        //        //    {
        //        //        PackageName = pkg.PackageName.ToString();
        //        //        if (userInfo.SubscriptionPlan.Equals("M"))
        //        //        {
        //        //            Amount = pkg.MonthlyPrice.ToString();
        //        //            Duration = "Monthly";
        //        //        }
        //        //        else
        //        //        {
        //        //            Amount = pkg.YearlyPrice.ToString();
        //        //            Duration = "Yearly";
        //        //        }
        //        //    }

        //        //    FinalHtml = trtd.Replace("##DETAIL##", "Package Amount").Replace("##AMOUNT##", Amount);

        //        //    if (divDiscount.Visible)
        //        //    {
        //        //        FinalHtml += trtd.Replace("##DETAIL##", "Discount Amount").Replace("##AMOUNT##", lblDiscountAmount.Text);
        //        //        FinalHtml += trtd.Replace("##DETAIL##", "Paid Amount").Replace("##AMOUNT##", lblPayableAmount.Text);
        //        //        FinalHtml += trtd.Replace("##DETAIL##", "Discount Coupon Code").Replace("##AMOUNT##", txtCouponCode.Text).Replace("$", "");
        //        //        DiscountDetail = ltrDiscountMessage.Text;
        //        //    }

        //        //    WSB_Agent agent = new AgentEntity().GetAgentByAgentCode(txtAgentCode.Text);

        //        //    if (agent != null)
        //        //    {
        //        //        FinalHtml += trtd.Replace("##DETAIL##", "Agent Name").Replace("##AMOUNT##", agent.FirstName + " " + agent.LastName).Replace("$", ""); ;
        //        //        FinalHtml += trtd.Replace("##DETAIL##", "Agent Code").Replace("##AMOUNT##", agent.AgentCode).Replace("$", ""); ;
        //        //    }

        //        //    Amount = lblPayableAmount.Text;
        //        //}
        //        //string emailTempletePath = Server.MapPath(CommonLogic.GetConfigValue("PaymentSuccessEmailTemplatePath"));
        //        //Authentication.PaymentSuccessMail(userInfo.FirstName, userInfo.LastName, emailTempletePath, userInfo.Email, Amount, Duration, PackageName, FinalHtml, DiscountDetail);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Payment email sending process", ex);
        //    }
        //}

        /// <summary>
        /// This function will save status if payment transaction is successfull
        /// </summary>
        /// <param name="userInfo"></param>
        private void SavePaymentSuccessStatus(long supplyOrderId)
        {
            try
            {
                PaymentSuccessEntity paymentEntity = new PaymentSuccessEntity();
                _4eOrtho.DAL.PaymentSuccess newPayment = paymentEntity.Create();

                newPayment.Ammount = Convert.ToDecimal(Request.Form[txtTotalAmount.UniqueID]);
                newPayment.CreatedDate = DateTime.Now;
                newPayment.SupplyOrderId = supplyOrderId;

                if (!isPaypalExpress)
                {
                    newPayment.AVSCode = payPalServiceResponse.AVSCode.ToString();
                    newPayment.CardNo = CommonHelper.MaskCreditCardNumber(txtCardNo.Text.ToString().Trim());
                    newPayment.CardType = ddlCardType.SelectedValue.ToString();
                    newPayment.CVV2Code = payPalServiceResponse.CVV2Code.ToString();
                    newPayment.CorRelation = payPalServiceResponse.CorrelationID.ToString();
                    newPayment.ExpiryMonth = Convert.ToInt32(ddlMonth.SelectedValue.ToString());
                    newPayment.ExpiryYear = Convert.ToInt32(ddlYear.SelectedValue.ToString());
                    newPayment.NameOnCard = txtNameOnCard.Text.Trim();
                    newPayment.TimeStamp = Convert.ToDateTime(payPalServiceResponse.Timestamp);
                    newPayment.Status = payPalServiceResponse.Ack.Value.ToString();
                    newPayment.TransactionId = payPalServiceResponse.TransactionID.ToString();
                    newPayment.TransactionRespons = payPalService.getLastResponse();
                    paymentId = paymentEntity.Save(newPayment);
                }
                else
                    Session["PaymentSuccess"] = newPayment;
            }
            catch (Exception exp)
            {
                logger.Error("An error occured.", exp);
            }
        }

        /// <summary>
        /// This function will save status if payment transaction is failed
        /// </summary>
        /// <param name="userInfo"></param>
        private void SavePaymentFailureStatus()
        {
            try
            {
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                PaymentFailureEntity paymentEntity = new PaymentFailureEntity();
                _4eOrtho.DAL.PaymentFailure newPayment = paymentEntity.Create();

                newPayment.Ammount = Convert.ToDecimal(Request.Form[txtTotalAmount.UniqueID]);
                newPayment.CardNo = CommonHelper.MaskCreditCardNumber(txtCardNo.Text.ToString().Trim());
                newPayment.CardType = ddlCardType.SelectedValue.ToString();
                newPayment.CorRelation = payPalServiceResponse.CorrelationID.ToString();
                newPayment.ExpiryMonth = Convert.ToInt32(ddlMonth.SelectedValue.ToString());
                newPayment.ExpiryYear = Convert.ToInt32(ddlYear.SelectedValue.ToString());
                newPayment.NameOnCard = txtNameOnCard.Text.Trim();
                newPayment.Status = payPalServiceResponse.Ack.Value.ToString();
                newPayment.TimeStamp = Convert.ToDateTime(payPalServiceResponse.Timestamp);
                newPayment.TransactionId = string.IsNullOrEmpty(payPalServiceResponse.TransactionID) ? string.Empty : payPalServiceResponse.TransactionID.ToString();
                newPayment.TransactionRespons = payPalService.getLastResponse();
                newPayment.Email = currentSession.EmailId;
                newPayment.FirstName = currentSession.DoctorFirstName;
                newPayment.LastName = currentSession.DoctorLastName;
                newPayment.Phone = currentSession.DoctorMobile;
                newPayment.CreatedDate = DateTime.Now;
                paymentEntity.Save(newPayment);
            }
            catch (Exception exp)
            {
                logger.Error("An error occured.", exp);
            }
        }

        /// <summary>
        /// This function will set current and selected month and year
        /// </summary>
        private void SetCurrentAndSelectedMonthYear()
        {
            currentMonth = DateTime.Now.Month;
            currentYear = DateTime.Now.Year;
            selectedMonth = Convert.ToInt32(ddlMonth.SelectedValue.ToString());
            selectedYear = Convert.ToInt32(ddlYear.SelectedValue.ToString());
        }

        /// <summary>
        /// This functin will validate card number by selected card type
        /// This function will use regex function for validating card
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        private bool ValidateCardNoByCardType(string cardNo)
        {
            if (ddlCardType.SelectedValue.ToString().ToUpper().Equals("VISA"))
                return Regex.IsMatch(cardNo, @"^4[0-9]{12}(?:[0-9]{3})?$");
            if (ddlCardType.SelectedValue.ToString().ToUpper().Equals("MASTERCARD"))
                return Regex.IsMatch(cardNo, @"^5[1-5][0-9]{14}$");
            if (ddlCardType.SelectedValue.ToString().ToUpper().Equals("DISCOVER"))
                return Regex.IsMatch(cardNo, @"^6(?:011|5[0-9]{2})[0-9]{12}$");
            if (ddlCardType.SelectedValue.ToString().ToUpper().Equals("AMEX"))
                return Regex.IsMatch(cardNo, @"^3[47][0-9]{13}$");
            else
                return false;
        }

        /// <summary>
        /// Method to get Payable Amount.
        /// </summary>
        /// <returns></returns>
        private string GetPayableAmount()
        {
            return txtTotalAmount.Text.Trim();
        }

        /// <summary>
        /// Set Express Checkout API Operation method
        /// </summary>
        /// <returns></returns>
        public SetExpressCheckoutResponseType SetExpressCheckoutAPIOperation()
        {
            SetExpressCheckoutResponseType responseSetExpressCheckoutResponseType = new SetExpressCheckoutResponseType();
            try
            {
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                if (currentSession != null)
                {
                    SetSecurityHeader();

                    SetExpressCheckoutRequestDetailsType setExpressCheckoutRequestDetails = new SetExpressCheckoutRequestDetailsType();

                    setExpressCheckoutRequestDetails.ReturnURL = CommonLogic.GetConfigValue("ReturnURL").ToLower().Replace("payment", "reviewandconfirm");
                    setExpressCheckoutRequestDetails.CancelURL = CommonLogic.GetConfigValue("CancelURL").ToLower().Replace("payment", "AddEditSupplyOrder");

                    List<PaymentDetailsType> paymentDetailsList = new List<PaymentDetailsType>();
                    PaymentDetailsType paymentDetails1 = new PaymentDetailsType();
                    BasicAmountType orderTotal = new BasicAmountType(CurrencyCodeType.USD, txtTotalAmount.Text.Trim());
                    paymentDetails1.OrderTotal = orderTotal;
                    paymentDetails1.PaymentAction = PaymentActionCodeType.ORDER;

                    PaymentDetailsItemType paymentDetailsItemType;

                    if (ddlSuppply.SelectedIndex > 0)
                    {
                        paymentDetailsItemType = new PaymentDetailsItemType();
                        paymentDetailsItemType.Amount = new BasicAmountType(CurrencyCodeType.USD, txtAmount.Text);
                        paymentDetailsItemType.Description = (rdblSupply.SelectedItem.Value == "2") ? "Package" : "Product";
                        paymentDetailsItemType.Name = "Supply " + paymentDetailsItemType.Description + " (" + ddlSuppply.SelectedItem.Text + ")";
                        paymentDetailsItemType.Quantity = Convert.ToInt16(txtQuantity.Text);
                        paymentDetailsItemType.Number = ddlSuppply.SelectedItem.Text;
                        paymentDetails1.PaymentDetailsItem.Add(paymentDetailsItemType);
                    }
                    //if (!string.IsNullOrEmpty(txtPromoDiscount.Text) && Convert.ToDecimal(txtPromoDiscount.Text) > 0)
                    //{
                    //    paymentDetailsItemType = new PaymentDetailsItemType();
                    //    paymentDetailsItemType.Amount = new BasicAmountType(CurrencyCodeType.USD, "-" + txtPromoDiscount.Text);
                    //    paymentDetailsItemType.Name = "Discount";
                    //    paymentDetailsItemType.Description = "Discount";
                    //    paymentDetailsItemType.Quantity = 1;
                    //    paymentDetails1.PaymentDetailsItem.Add(paymentDetailsItemType);
                    //}

                    //paymentDetails1.PaymentRequestID = hdnLookupId.Value;
                    paymentDetails1.NotifyURL = "http://IPNhost";
                    paymentDetailsList.Add(paymentDetails1);
                    setExpressCheckoutRequestDetails.PaymentDetails = paymentDetailsList;

                    setExpressCheckoutRequestDetails.Address = new AddressType();
                    setExpressCheckoutRequestDetails.Address.CityName = currentSession.DoctorCity;
                    setExpressCheckoutRequestDetails.Address.CountryName = currentSession.DoctorCountry;
                    setExpressCheckoutRequestDetails.Address.Street1 = currentSession.DoctorStreet;
                    setExpressCheckoutRequestDetails.Address.PostalCode = currentSession.DoctorZipcode;
                    setExpressCheckoutRequestDetails.NoShipping = "0";
                    SetExpressCheckoutReq setExpressCheckout = new SetExpressCheckoutReq();
                    SetExpressCheckoutRequestType setExpressCheckoutRequest = new SetExpressCheckoutRequestType(setExpressCheckoutRequestDetails);

                    setExpressCheckout.SetExpressCheckoutRequest = setExpressCheckoutRequest;
                    // Create the service wrapper object to make the API call
                    PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();

                    //SessionHelper.PaymentAmount = ltrFees.Text.Trim();

                    // # API call            
                    // Invoke the SetExpressCheckout method in service wrapper object
                    responseSetExpressCheckoutResponseType = service.SetExpressCheckout(setExpressCheckout);

                    if (responseSetExpressCheckoutResponseType != null)
                    {
                        // Response envelope acknowledgement
                        string acknowledgement = "SetExpressCheckout API Operation - ";
                        acknowledgement += responseSetExpressCheckoutResponseType.Ack.ToString();
                        logger.Debug(acknowledgement + "\n");
                        System.Diagnostics.Debug.WriteLine(acknowledgement + "\n");
                        // # Success values
                        if (responseSetExpressCheckoutResponseType.Ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                        {
                            // # Redirecting to PayPal for authorization
                            // Once you get the "Success" response, needs to authorise the
                            // transaction by making buyer to login into PayPal. For that,
                            // need to construct redirect url using EC token from response.
                            // Express Checkout Token
                            string EcToken = responseSetExpressCheckoutResponseType.Token;
                            logger.Info("Express Checkout Token : " + EcToken + "\n");
                            System.Diagnostics.Debug.WriteLine("Express Checkout Token : " + EcToken + "\n");
                            // Store the express checkout token in session to be used in GetExpressCheckoutDetails & DoExpressCheckout API operations
                            Session["EcToken"] = EcToken;
                            SaveSupplyOrder();
                            Response.Redirect(CommonLogic.GetConfigValue("PaypalExpressRedirectURL") + HttpUtility.UrlEncode(EcToken), false);
                        }
                        else
                        {
                            List<ErrorType> errorMessages = responseSetExpressCheckoutResponseType.Errors;
                            string errorMessage = "";
                            foreach (ErrorType error in errorMessages)
                            {
                                logger.Debug("API Error Message : " + error.LongMessage);
                                System.Diagnostics.Debug.WriteLine("API Error Message : " + error.LongMessage + "\n");
                                errorMessage = errorMessage + error.LongMessage;
                            }
                            lblMsg.Text = errorMessage;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Debug("Error Message : " + ex.Message);
            }
            return responseSetExpressCheckoutResponseType;
        }
        #endregion

        protected void btnSelectPayment_Click(object sender, EventArgs e)
        {
            divCreaditCard.Visible = true;
            divSelectPayment.Visible = false;
            phMakePayment.Visible = true;

            //phDetail.Visible = false;
            btnMakePayment.Visible = true;
            btnBackPayment.Visible = true;
            btnSubmit.Visible = false;
            txtCardNo.Focus();
            BindYearList();
        }

        protected void imgbtnExpressCheckout_Click(object sender, ImageClickEventArgs e)
        {
            isPaypalExpress = true;
            SetExpressCheckoutAPIOperation();
        }
    }
}