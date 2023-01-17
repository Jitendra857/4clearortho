using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho
{
    public partial class Payment : PageBase
    {
        #region Declaration                
        private ILog logger = log4net.LogManager.GetLogger(typeof(Payment));
        PayPalAPIInterfaceServiceService payPalService;
        DoDirectPaymentResponseType payPalServiceResponse;
        DoExpressCheckoutPaymentResponseType responseDoExpressCheckoutPaymentResponseType;
        long paymentId = 0;
        int currentMonth = 0;
        int currentYear = 0;
        int selectedMonth = 0;
        int selectedYear = 0;
        #endregion Declaration

        #region Event

        /// <summary>
        /// Page Load Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            LookupMaster feesLookup = new LookupMasterEntity().GetLookupMasterByDesc("RegistrationFees");
            if (feesLookup != null)
            {
                ltrFees.Text = feesLookup.LookupName;
                hdnLookupId.Value = feesLookup.LookupId.ToString();
            }

            if (!IsPostBack)
            {
                if ((CurrentSession)Session["UserLoginSession"] == null)
                {
                    PageRedirect("Home.aspx");
                }
                else
                {
                    if (String.IsNullOrEmpty(CommonLogic.QueryString("token")) && String.IsNullOrEmpty(CommonLogic.QueryString("PayerID")))
                    {
                        txtCardNo.Focus();
                        CurrentSession currentSession = new CurrentSession();
                        currentSession = (CurrentSession)Session["UserLoginSession"];
                        if (currentSession.UserType == UserType.P.ToString())
                        {
                            lblUser.Text = currentSession.PatientFirstName + ' ' + currentSession.PatientLastName;
                        }
                        else if (currentSession.UserType == UserType.D.ToString() || currentSession.UserType == UserType.S.ToString())
                        {
                            lblUser.Text = currentSession.DoctorName;
                        }
                        BindYearList();
                    }
                    else
                    {
                        //phMakePayment.Visible = false;
                        //phReviewPayment.Visible = true;
                        //ltrMessage.Visible = false;
                        GetExpressCheckoutAPIOperation();
                    }
                }
            }
        }

        /// <summary>
        /// Event to Log out user session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgLogOut_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Session["UserLoginSession"] = null;
                SessionHelper.IsAbleToNavigate = false;
                SessionHelper.LoggedUserEmailAddress = string.Empty;

                HttpCookie hcAccount = Request.Cookies["4eOrtho_Cookie"];
                if (hcAccount != null)
                {
                    hcAccount.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(hcAccount);
                }

                if (((CurrentSession)Session["UserLoginSession"]) == null)
                {
                    Response.Redirect("Home.aspx");
                    return;
                }
                else if (((CurrentSession)Session["UserLoginSession"]).UserType == UserType.P.ToString())
                {
                    ((CurrentSession)Session["UserLoginSession"]).UserType = string.Empty;
                    Response.Redirect("PatientLogin.aspx");
                    return;

                }
                else if (((CurrentSession)Session["UserLoginSession"]).UserType == UserType.D.ToString() || ((CurrentSession)Session["UserLoginSession"]).UserType == UserType.S.ToString())
                {
                    ((CurrentSession)Session["UserLoginSession"]).UserType = string.Empty;
                    Response.Redirect("DoctorLogin.aspx");
                    return;
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
            if (ValidateCardNoByCardType(e.Value.Trim()))
                e.IsValid = true;
            else
                e.IsValid = false;
        }

        /// <summary>
        /// This will check selected month is history or not is selected or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void custom_ServerMonthValidate(object sender, ServerValidateEventArgs e)
        {
            SetCurrentAndSelectedMonthYear();
            if (currentYear == selectedYear && selectedMonth < currentMonth)
                e.IsValid = false;
            else
                e.IsValid = true;
        }

        /// <summary>
        /// This will change ccv no length as per card type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCardType.SelectedItem.Value == "AMEX")
            {
                txtCCVNo.MaxLength = 4;
            }
            else
            {
                txtCCVNo.MaxLength = 3;
            }
        }

        /// <summary>
        /// Event to submit payment details and make payment using paypal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    bool bSuccess = false;
                    string supplyName = string.Empty;
                    string mailSubject = string.Empty;
                    string status = string.Empty;
                    string titleDoctor = string.Empty;
                    string titleAdmin = string.Empty;
                    string redirectto = string.Empty;

                    CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                    if (currentSession != null)
                    {
                        CallPayPalServiceForPayment();
                        bSuccess = payPalServiceResponse.Ack.Value.ToString().ToUpper().Equals("SUCCESS");

                        if (bSuccess)
                        {
                            redirectto = "paymentsuccess.aspx";
                            Session["redirect"] = "Welcome.aspx";

                            SessionHelper.PaymentAmount = payPalServiceResponse.Amount.value.ToString();
                            SessionHelper.TransactionId = payPalServiceResponse.TransactionID.ToString();
                            SessionHelper.TransactionTime = Convert.ToDateTime(payPalServiceResponse.Timestamp.ToString()).ToString("dd-MMM-yyyy HH:mm:ss");
                            ((CurrentSession)Session["UserLoginSession"]).IsPayment = true;
                            SavePaymentSuccessStatus();
                            SendPaymentSuccessEmail();

                            Session["RegistrationPaymentMessage"] = true;
                            Response.Redirect(redirectto, false);
                        }
                        if (payPalServiceResponse.Ack.Value.ToString().ToUpper().Equals("FAILURE"))
                        {
                            SessionHelper.PayPalServiceResponseErrors = payPalServiceResponse.Errors;
                            SessionHelper.PaymentAmount = payPalServiceResponse.Amount.value.ToString();
                            SessionHelper.TransactionTime = Convert.ToDateTime(payPalServiceResponse.Timestamp.ToString()).ToString("dd-MMM-yyyy HH:mm:ss");
                            SavePaymentFailureStatus();
                            //redirectto = "PaymentFailure.aspx";

                            //userInfo = (UserSessionData)SessionHelper.PersonalInformation;
                            ltrErrorMsg.Text = string.Empty;

                            List<ErrorType> errorList = (List<ErrorType>)SessionHelper.PayPalServiceResponseErrors;
                            if (errorList != null && errorList.Count > 0)
                            {
                                int i = 1;
                                foreach (ErrorType item in errorList)
                                {
                                    ltrErrorMsg.Text += GetErrorMessage(item.LongMessage, i);
                                    i++;
                                }
                            }
                            divErrorMessage.Visible = true;
                            divErrorMessage.Focus();
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "callErrorDiv()", "callErrorDiv()", false);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "callErrorDiv", "callErrorDiv();", true);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "callErrorDiv", "callErrorDiv()", true);                            
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

        protected void imgbtnExpressCheckout_Click(object sender, ImageClickEventArgs e)
        {
            SetExpressCheckoutAPIOperation();
        }

        protected void btnDoExpressCheckout_Click(object sender, EventArgs e)
        {
            //DoExpressCheckoutAPIOperation();
        }

        protected void btnSelectPayment_Click(object sender, EventArgs e)
        {
            phCreditCard.Visible = true;
            phSelectPayment.Visible = false;
        }

        /// <summary>
        /// This method will format label with error message.
        /// </summary>
        /// <param name="longMsg"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        private string GetErrorMessage(string longMsg, int step)
        {
            return string.Format("<label style=\"margin-bottom: 15px; width:100%;\"> (" + step.ToString() + ") " + longMsg + "</label><br>");
        }

        #endregion

        #region Helper

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
                //SetSecurityHeader();

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
                throw exp;
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
                throw exp;
            }
        }

        /// <summary>
        /// This function will save status if payment transaction is successfull
        /// </summary>
        /// <param name="userInfo"></param>
        private void SavePaymentSuccessStatus()
        {
            try
            {

                TransactionScope scope = new TransactionScope();
                using (scope)
                {
                    CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                    PaymentSuccessEntity paymentEntity = new PaymentSuccessEntity();
                    _4eOrtho.DAL.PaymentSuccess newPayment = paymentEntity.Create();

                    newPayment.Status = responseDoExpressCheckoutPaymentResponseType != null ? responseDoExpressCheckoutPaymentResponseType.Ack.ToString().Trim().ToUpper() : string.Empty;

                    if (!string.IsNullOrEmpty(txtCardNo.Text))
                    {
                        newPayment.AVSCode = Convert.ToString(payPalServiceResponse.AVSCode);
                        newPayment.CardNo = CommonHelper.MaskCreditCardNumber(Convert.ToString(txtCardNo.Text.Trim()));
                        newPayment.CardType = Convert.ToString(ddlCardType.SelectedValue);
                        newPayment.CVV2Code = Convert.ToString(payPalServiceResponse.CVV2Code);
                        newPayment.CorRelation = Convert.ToString(payPalServiceResponse.CorrelationID);
                        newPayment.ExpiryMonth = Convert.ToInt32(Convert.ToString(ddlMonth.SelectedValue));
                        newPayment.ExpiryYear = Convert.ToInt32(Convert.ToString(ddlYear.SelectedValue));
                        newPayment.NameOnCard = txtNameOnCard.Text.Trim();
                        newPayment.Status = Convert.ToString(payPalServiceResponse.Ack.Value);
                    }
                    newPayment.TimeStamp = Convert.ToDateTime(payPalServiceResponse.Timestamp);
                    newPayment.TransactionId = Convert.ToString(payPalServiceResponse.TransactionID);
                    newPayment.TransactionRespons = payPalService.getLastResponse();
                    newPayment.Ammount = Convert.ToDecimal(!string.IsNullOrEmpty(ltrFees.Text) ? ltrFees.Text : SessionHelper.PaymentAmount);
                    newPayment.CreatedDate = DateTime.Now;
                    newPayment.SupplyOrderId = 0;
                    newPayment.LookupId = Convert.ToInt32(hdnLookupId.Value);
                    newPayment.DoctorEmailId = currentSession.EmailId;
                    paymentId = paymentEntity.Save(newPayment);

                    UserConfigEntity userConfigEntity = new UserConfigEntity();
                    UserConfig userConfig = userConfigEntity.GetUserByEmailAddress(currentSession.EmailId);
                    if (userConfig != null)
                    {
                        userConfig.IsPayment = !userConfig.IsPayment;
                        userConfig.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        userConfig = new UserConfig();
                        userConfig.IsPayment = true;
                        userConfig.CreatedBy = userConfig.UpdatedBy = SessionHelper.LoggedAdminUserID;
                        userConfig.CreatedDate = userConfig.UpdatedDate = DateTime.Now;
                        userConfig.EmailId = currentSession.EmailId;
                    }
                    userConfigEntity.Save(userConfig);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                throw exp;
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

                newPayment.Ammount = Convert.ToDecimal(Request.Form[ltrFees.Text]);
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
                newPayment.LookupId = Convert.ToInt32(hdnLookupId.Value);
                paymentEntity.Save(newPayment);
            }
            catch (Exception exp)
            {
                throw exp;
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

        private string GetPayableAmount()
        {
            return ltrFees.Text.Trim();
        }
        #endregion

        /// <summary>
        /// This function will send mail to user.
        /// </summary>
        private void SendPaymentSuccessEmail()
        {
            try
            {
                string emailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("PaymentSuccess")).ToString();

                if (File.Exists(emailTemplatePath))
                {
                    CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];

                    string emailtemplateHTML = File.ReadAllText(emailTemplatePath);
                    emailtemplateHTML = emailtemplateHTML.Replace("##DoctorName##", currentSession.DoctorName);
                    emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", DateTime.Now.Year.ToString());
                    emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));

                    emailtemplateHTML = emailtemplateHTML.Replace("##TRANSACTIONSTATUS##", "Success");
                    emailtemplateHTML = emailtemplateHTML.Replace("##TRANSACTIONID##", SessionHelper.TransactionId);
                    emailtemplateHTML = emailtemplateHTML.Replace("##TRANSACTIONDATE##", SessionHelper.TransactionTime);
                    emailtemplateHTML = emailtemplateHTML.Replace("##TRANSACTIONAMOUNT##", SessionHelper.PaymentAmount);

                    MailAddress fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("ToMail"));
                    MailAddress toMailAddress = new MailAddress(currentSession.EmailId);
                    CommonLogic.SendMail(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, "4ClearOrtho – Payment Success");
                }
                else
                    logger.Info(emailTemplatePath + " emailtemplate is not exist.");
            }
            catch (Exception ex)
            {
                logger.Error("Payment email sending process", ex);
            }
        }

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

                    setExpressCheckoutRequestDetails.ReturnURL = CommonLogic.GetConfigValue("ReturnURL");
                    setExpressCheckoutRequestDetails.CancelURL = CommonLogic.GetConfigValue("CancelURL");

                    List<PaymentDetailsType> paymentDetailsList = new List<PaymentDetailsType>();
                    PaymentDetailsType paymentDetails1 = new PaymentDetailsType();
                    BasicAmountType orderTotal = new BasicAmountType(CurrencyCodeType.USD, ltrFees.Text.Trim());
                    PaymentDetailsItemType paymentDetailsItemType = new PaymentDetailsItemType();
                    paymentDetailsItemType.Amount = new BasicAmountType(CurrencyCodeType.USD, ltrFees.Text.Trim());
                    paymentDetailsItemType.Name = "Registration Fees";
                    paymentDetails1.PaymentDetailsItem.Add(paymentDetailsItemType);
                    paymentDetails1.OrderTotal = orderTotal;
                    paymentDetails1.PaymentAction = PaymentActionCodeType.ORDER;
                    paymentDetails1.PaymentRequestID = hdnLookupId.Value;
                    paymentDetails1.NotifyURL = "http://IPNhost";
                    paymentDetailsList.Add(paymentDetails1);
                    setExpressCheckoutRequestDetails.PaymentDetails = paymentDetailsList;
                    setExpressCheckoutRequestDetails.NoShipping = "2";
                    SetExpressCheckoutReq setExpressCheckout = new SetExpressCheckoutReq();
                    SetExpressCheckoutRequestType setExpressCheckoutRequest = new SetExpressCheckoutRequestType(setExpressCheckoutRequestDetails);
                    setExpressCheckout.SetExpressCheckoutRequest = setExpressCheckoutRequest;
                    // Create the service wrapper object to make the API call
                    PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();

                    SessionHelper.PaymentAmount = ltrFees.Text.Trim();

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
                            lblMessage.Text = errorMessage;
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

        private void GetExpressCheckoutAPIOperation()
        {
            // Create the GetExpressCheckoutDetailsResponseType object
            GetExpressCheckoutDetailsResponseType responseGetExpressCheckoutDetailsResponseType = new GetExpressCheckoutDetailsResponseType();
            try
            {
                // Create the GetExpressCheckoutDetailsReq object
                GetExpressCheckoutDetailsReq getExpressCheckoutDetails = new GetExpressCheckoutDetailsReq();
                // A timestamped token, the value of which was returned by `SetExpressCheckout` API response
                string EcToken = CommonLogic.QueryString("token");
                GetExpressCheckoutDetailsRequestType getExpressCheckoutDetailsRequest = new GetExpressCheckoutDetailsRequestType(EcToken);
                getExpressCheckoutDetails.GetExpressCheckoutDetailsRequest = getExpressCheckoutDetailsRequest;
                // Create the service wrapper object to make the API call
                PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();
                // # API call
                // Invoke the GetExpressCheckoutDetails method in service wrapper object
                responseGetExpressCheckoutDetailsResponseType = service.GetExpressCheckoutDetails(getExpressCheckoutDetails);
                if (responseGetExpressCheckoutDetailsResponseType != null)
                {
                    // Response envelope acknowledgement
                    string acknowledgement = "GetExpressCheckoutDetails API Operation - ";
                    acknowledgement += responseGetExpressCheckoutDetailsResponseType.Ack.ToString();
                    logger.Info(acknowledgement + "\n");
                    //System.Diagnostics.Debug.WriteLine(acknowledgement + "\n");
                    // # Success values
                    if (responseGetExpressCheckoutDetailsResponseType.Ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                    {
                        // Unique PayPal Customer Account identification number. This
                        // value will be null unless you authorize the payment by
                        // redirecting to PayPal after `SetExpressCheckout` call.
                        string PayerId = responseGetExpressCheckoutDetailsResponseType.GetExpressCheckoutDetailsResponseDetails.PayerInfo.PayerID;
                        // Store PayerId in session to be used in DoExpressCheckout API operation
                        Session["PayerId"] = PayerId;

                        List<PaymentDetailsType> paymentDetails = responseGetExpressCheckoutDetailsResponseType.GetExpressCheckoutDetailsResponseDetails.PaymentDetails;
                        //foreach (PaymentDetailsType paymentdetail in paymentDetails)
                        //{
                        //    //AddressType ShippingAddress = paymentdetail.ShipToAddress;
                        //    //if (ShippingAddress != null)
                        //    //{
                        //    //    Session["Address_Name"] = ShippingAddress.Name;
                        //    //    Session["Address_Street"] = ShippingAddress.Street1 + " " + ShippingAddress.Street2;
                        //    //    Session["Address_CityName"] = ShippingAddress.CityName;
                        //    //    Session["Address_StateOrProvince"] = ShippingAddress.StateOrProvince;
                        //    //    Session["Address_CountryName"] = ShippingAddress.CountryName;
                        //    //    Session["Address_PostalCode"] = ShippingAddress.PostalCode;
                        //    //}
                        //    Session["Currency_Code"] = paymentdetail.OrderTotal.currencyID;
                        //    Session["Order_Total"] = paymentdetail.OrderTotal.value;
                        //    Session["Shipping_Total"] = paymentdetail.ShippingTotal.value;
                        //    List<PaymentDetailsItemType> itemList = paymentdetail.PaymentDetailsItem;
                        //    foreach (PaymentDetailsItemType item in itemList)
                        //    {
                        //        Session["Product_Quantity"] = item.Quantity;
                        //        Session["Product_Name"] = item.Name;
                        //    }
                        //}

                        DoExpressCheckoutAPIOperation(paymentDetails);
                    }
                    else
                    {
                        List<ErrorType> errorMessages = responseGetExpressCheckoutDetailsResponseType.Errors;
                        foreach (ErrorType error in errorMessages)
                            logger.Error("API Error Message : " + error.LongMessage);
                    }
                }
            }
            catch (System.Exception ex)
            {
                logger.Error("Error Message : " + ex.Message);
            }
        }

        private void DoExpressCheckoutAPIOperation(List<PaymentDetailsType> paymentDetails)
        {
            // Create the DoExpressCheckoutPaymentResponseType object
            responseDoExpressCheckoutPaymentResponseType = new DoExpressCheckoutPaymentResponseType();
            try
            {
                string redirectto = string.Empty;
                // Create the DoExpressCheckoutPaymentReq object
                DoExpressCheckoutPaymentReq doExpressCheckoutPayment = new DoExpressCheckoutPaymentReq();
                DoExpressCheckoutPaymentRequestDetailsType doExpressCheckoutPaymentRequestDetails = new DoExpressCheckoutPaymentRequestDetailsType();
                // The timestamped token value that was returned in the
                // `SetExpressCheckout` response and passed in the
                // `GetExpressCheckoutDetails` request.
                doExpressCheckoutPaymentRequestDetails.Token = (string)(Session["EcToken"]);
                // Unique paypal buyer account identification number as returned in
                // `GetExpressCheckoutDetails` Response
                doExpressCheckoutPaymentRequestDetails.PayerID = CommonLogic.QueryString("PayerID");
                doExpressCheckoutPaymentRequestDetails.PaymentAction = PaymentActionCodeType.SALE;
                // # Payment Information
                // list of information about the payment
                //List<PaymentDetailsType> paymentDetailsList = new List<PaymentDetailsType>();
                // information about the payment
                //PaymentDetailsType paymentDetails = new PaymentDetailsType();
                CurrencyCodeType currency_code_type = CurrencyCodeType.USD;                
                //Pass the order total amount which was already set in session
                string total_amount = SessionHelper.PaymentAmount;
                BasicAmountType orderTotal = new BasicAmountType(currency_code_type, total_amount);
                //paymentDetails.OrderTotal = orderTotal;
                //paymentDetails.PaymentAction = payment_action_type;

                //BN codes to track all transactions
                //paymentDetails.ButtonSource = CommonLogic.GetConfigValue("SBN_CODE");

                //paymentDetailsList.Add(paymentDetails);
                doExpressCheckoutPaymentRequestDetails.PaymentDetails = paymentDetails;//paymentDetailsList;

                DoExpressCheckoutPaymentRequestType doExpressCheckoutPaymentRequest = new DoExpressCheckoutPaymentRequestType(doExpressCheckoutPaymentRequestDetails);
                doExpressCheckoutPayment.DoExpressCheckoutPaymentRequest = doExpressCheckoutPaymentRequest;
                // Create the service wrapper object to make the API call                
                // # API call
                // Invoke the DoExpressCheckoutPayment method in service wrapper object
                payPalService = new PayPalAPIInterfaceServiceService();
                responseDoExpressCheckoutPaymentResponseType = payPalService.DoExpressCheckoutPayment(doExpressCheckoutPayment);
                if (responseDoExpressCheckoutPaymentResponseType != null)
                {
                    // Response envelope acknowledgement
                    string acknowledgement = "DoExpressCheckoutPayment API Operation - ";
                    acknowledgement += responseDoExpressCheckoutPaymentResponseType.Ack.ToString();
                    logger.Info(acknowledgement + "\n");
                    //System.Diagnostics.Debug.WriteLine(acknowledgement + "\n");
                    // # Success values
                    if (responseDoExpressCheckoutPaymentResponseType.Ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                    {
                        // Transaction identification number of the transaction that was
                        // created.
                        // This field is only returned after a successful transaction
                        // for DoExpressCheckout has occurred.
                        if (responseDoExpressCheckoutPaymentResponseType.DoExpressCheckoutPaymentResponseDetails.PaymentInfo != null)
                        {
                            IEnumerator<PaymentInfoType> paymentInfoIterator = responseDoExpressCheckoutPaymentResponseType.DoExpressCheckoutPaymentResponseDetails.PaymentInfo.GetEnumerator();
                            while (paymentInfoIterator.MoveNext())
                            {
                                PaymentInfoType paymentInfo = paymentInfoIterator.Current;
                                logger.Info("Transaction ID : " + paymentInfo.TransactionID + "\n");

                                SessionHelper.PaymentAmount = paymentInfo.GrossAmount.value.ToString();
                                SessionHelper.TransactionId = paymentInfo.TransactionID.ToString();
                                SessionHelper.TransactionTime = Convert.ToDateTime(paymentInfo.PaymentDate.ToString()).ToString("dd-MMM-yyyy HH:mm:ss");
                                ((CurrentSession)Session["UserLoginSession"]).IsPayment = true;

                                payPalServiceResponse = new DoDirectPaymentResponseType();
                                payPalServiceResponse.Amount = new BasicAmountType(CurrencyCodeType.USD, paymentInfo.GrossAmount.value);
                                payPalServiceResponse.TransactionID = paymentInfo.TransactionID;
                                payPalServiceResponse.PaymentStatus = paymentInfo.PaymentStatus;
                                payPalServiceResponse.Timestamp = Convert.ToDateTime(paymentInfo.PaymentDate.ToString()).ToString("dd-MMM-yyyy HH:mm:ss");

                                SavePaymentSuccessStatus();
                                SendPaymentSuccessEmail();
                                redirectto = "paymentsuccess.aspx";

                                //Session["Transaction_Id"] = paymentInfo.TransactionID;
                                //Session["Transaction_Type"] = paymentInfo.TransactionType;
                                //Session["Payment_Status"] = paymentInfo.PaymentStatus;
                                //Session["Payment_Type"] = paymentInfo.PaymentType;
                                //Session["Payment_Total_Amount"] = paymentInfo.GrossAmount.value;                               
                            }
                        }
                    }
                    else
                    {
                        SessionHelper.PayPalServiceResponseErrors = responseDoExpressCheckoutPaymentResponseType.Errors;
                        SessionHelper.PaymentAmount = payPalServiceResponse.Amount.value.ToString();
                        SessionHelper.TransactionTime = Convert.ToDateTime(payPalServiceResponse.Timestamp.ToString()).ToString("dd-MMM-yyyy HH:mm:ss");
                        redirectto = "PaymentFailure.aspx";
                    }
                }
                Session["RegistrationPaymentMessage"] = true;
                Response.Redirect(redirectto, false);
            }
            catch (System.Exception ex)
            {
                logger.Debug("Error Message : " + ex.Message);
            }
        }
        #endregion
    }
}
