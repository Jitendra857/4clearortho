using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho
{
    public partial class PatientStagePayment : PageBase
    {
        #region Declaration

        private long supplyOrderId = 0;
        private ProductMasterEntity productMasterEntity;
        private PackageMasterEntity packageMasterEntity;
        private SupplyOrderEntity supplyOrderEntity;
        private StageEntity stageEntity;
        private SupplyOrder supplyOrder;
        private ILog logger = log4net.LogManager.GetLogger(typeof(PatientStagePayment));
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
                if (!Page.IsPostBack)
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    if (id > 0)
                    {
                        StageEntity st = new StageEntity();
                        var stage = st.GetStageById(id);
                      
                       
                        if (stage.StageAmount.ToString().Contains('.'))
                        {
                            int index = stage.StageAmount.ToString().IndexOf('.');
                            string result = stage.StageAmount.ToString().Substring(0, index);
                            lblamount.Text ="$"+result;
                            // Console.WriteLine("result: " + result);
                        }
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
          //  phDetail.Visible = false;
            phMakePayment.Visible = true;
            btnSubmit.Visible = false;
            btnMakePayment.Visible = false;
           // btnBackPayment.Visible = true;
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



        #endregion Events

        #region Helpers



        /// <summary>
        /// Bind package list
        /// </summary>


        /// <summary>
        /// calculate total amount
        /// </summary>


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
                            SessionHelper.PaymentAmount = lblamount.Text.ToString();
                            SessionHelper.TransactionId =  payPalServiceResponse.TransactionID.ToString();
                            SessionHelper.TransactionTime = Convert.ToDateTime(payPalServiceResponse.Timestamp.ToString()).ToString("dd-MMM-yyyy HH:mm:ss");
                        }


                    // isPaypalExpress = false;

                    if (payment == null || isPaypalExpress)
                    {
                        int id = Convert.ToInt32(Request.QueryString["id"]);
                        StageEntity st = new StageEntity();
                        var stage = st.GetStageById(id);
                        if (stage != null)
                        {
                            if (stage.isPaymentByPatient != true)
                            {
                                // 
                                stage.isPaymentByPatient = true;
                                stage.IsReceived = true;
                                stage.Status = 2;
                                st.save();
                            }
                                SavePaymentSuccessStatus(0, Convert.ToDecimal(stage.StageAmount));
                            }

                    }
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

                SetSecurityHeader();
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
        private void SavePaymentSuccessStatus(long supplyOrderId,decimal amount)
        {
            try
            {
                PaymentSuccessEntity paymentEntity = new PaymentSuccessEntity();
                _4eOrtho.DAL.PaymentSuccess newPayment = paymentEntity.Create();
                int id = Convert.ToInt32(Request.QueryString["id"]);
                newPayment.Ammount = amount;
                newPayment.CreatedDate = DateTime.Now;
                newPayment.SupplyOrderId = supplyOrderId;

                if (!isPaypalExpress)
                {
                    newPayment.AVSCode =  payPalServiceResponse.AVSCode.ToString();
                    newPayment.CardNo = CommonHelper.MaskCreditCardNumber(txtCardNo.Text.ToString().Trim());
                    newPayment.CardType = ddlCardType.SelectedValue.ToString();
                    newPayment.CVV2Code =  payPalServiceResponse.CVV2Code.ToString();
                    newPayment.CorRelation = payPalServiceResponse.CorrelationID.ToString();
                    newPayment.ExpiryMonth = Convert.ToInt32(ddlMonth.SelectedValue.ToString());
                    newPayment.ExpiryYear = Convert.ToInt32(ddlYear.SelectedValue.ToString());
                    newPayment.NameOnCard = txtNameOnCard.Text.Trim();
                    newPayment.TimeStamp = Convert.ToDateTime(payPalServiceResponse.Timestamp);
                    newPayment.Status =  payPalServiceResponse.Ack.Value.ToString();
                    newPayment.TransactionId =  payPalServiceResponse.TransactionID.ToString();
                   newPayment.TransactionRespons = payPalService.getLastResponse();
                    newPayment.StageId = id;
                    newPayment.PatientEmailId = SessionHelper.LoggedUserEmailAddress;
                    newPayment.PatientId = SessionHelper.PatientId;
                    newPayment.AppointmentId = 1;
                    newPayment.Description = "Payment from 4ClearOrtho";
                    newPayment.AcknowledgementNumber = AcknowledgementNumber();
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

        public string AcknowledgementNumber()
        {
            string numbers = "1234567890";

            string characters = numbers;
            int length = 10;
            string id = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (id.IndexOf(character) != -1);
                id += character;
            }
           return "##" + id;
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

                newPayment.Ammount = Convert.ToDecimal(Request.Form[lblPayableAmount.UniqueID]);
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
            return "9";// txtTotalAmount.Text.Trim();
        }

        /// <summary>
        /// Set Express Checkout API Operation method
        /// </summary>
        /// <returns></returns>
    
        #endregion

        protected void btnSelectPayment_Click(object sender, EventArgs e)
        {
            divCreaditCard.Visible = true;
            divSelectPayment.Visible = false;
            phMakePayment.Visible = true;

            //phDetail.Visible = false;
            btnMakePayment.Visible = true;
           
            btnSubmit.Visible = false;
            txtCardNo.Focus();
            BindYearList();
        }

  

        protected void imgbtnExpressCheckout_Click(object sender, ImageClickEventArgs e)
        {
            isPaypalExpress = true;
           // SetExpressCheckoutAPIOperation();
        }
    }
}