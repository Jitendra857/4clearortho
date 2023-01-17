using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using log4net;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho
{
    public partial class ReviewAndConfirm : PageBase
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(ReviewAndConfirm));
        DoExpressCheckoutPaymentResponseType responseDoExpressCheckoutPaymentResponseType;
        PayPalAPIInterfaceServiceService payPalService;
        DoDirectPaymentResponseType payPalServiceResponse;
        CurrentSession currentSession;
        _4eOrtho.DAL.PaymentSuccess newPayment;
        #endregion

        #region Event

        /// <summary>
        /// Event Page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if ((CurrentSession)Session["UserLoginSession"] != null)
                    {
                        PageRight pageRight = CheckRights(this.Page.GetType().BaseType.Name);
                        if (pageRight != null)
                        {
                            PageRedirect(pageRight.RedirectPageName);
                        }
                    }

                    if (!String.IsNullOrEmpty(CommonLogic.QueryString("token")) && !String.IsNullOrEmpty(CommonLogic.QueryString("PayerID")))
                    {
                        GetExpressCheckoutAPIOperation();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error Message : " + ex.Message);
            }
        }

        /// <summary>
        /// Event to Submit Payment and save all product and package details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DoExpressCheckoutAPIOperation();
        }

        #endregion

        #region Helper

        /// <summary>
        /// Method to get express checkout api operation for getting payment details
        /// </summary>
        private void GetExpressCheckoutAPIOperation()
        {
            SetSecurityHeader();

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
                        foreach (PaymentDetailsType paymentdetail in paymentDetails)
                        {
                            List<PaymentDetailsItemType> itemList = paymentdetail.PaymentDetailsItem;
                            foreach (PaymentDetailsItemType item in itemList)
                            {
                                switch (item.Description.ToUpper())
                                {
                                    case "CASE":
                                        txtCaseCharge.Text = item.Amount.value;
                                        divCase.Visible = true;
                                        break;
                                    case "PACKAGE":
                                    case "PRODUCT":
                                        lblPackageAmt.Text = Convert.ToString(this.GetLocalResourceObject(item.Description)) + " ($)";
                                        txtPackageAmt.Text = Convert.ToString((Convert.ToInt16(item.Quantity.Value) * Convert.ToDecimal(item.Amount.value)));
                                        hdnProductPackage.Value = item.Number;
                                        divPackage.Visible = true;
                                        break;
                                    case "DISCOUNT":
                                        txtTotalCasePackage.Text = Convert.ToString(Convert.ToDecimal(txtCaseCharge.Text) + Convert.ToDecimal(txtPackageAmt.Text));
                                        txtPromoDiscount.Text = item.Amount.value;
                                        divDiscount.Visible = true;
                                        break;
                                }
                            }

                            txtPayableAmt.Text = paymentdetail.OrderTotal.value;

                            txtExpressShipment.Text = paymentdetail.ShippingTotal.value;
                        }

                        Session["paymentDetails"] = paymentDetails;
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

        /// <summary>
        /// Method to do express checkout api operation for final payment.
        /// </summary>
        private void DoExpressCheckoutAPIOperation()
        {
            SetSecurityHeader();

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
                //PaymentActionCodeType payment_action_type = (PaymentActionCodeType.NONE);
                //Pass the order total amount which was already set in session
                string total_amount = SessionHelper.PaymentAmount;
                BasicAmountType orderTotal = new BasicAmountType(currency_code_type, total_amount);
                //paymentDetails.OrderTotal = orderTotal;
                //paymentDetails.PaymentAction = payment_action_type;

                //BN codes to track all transactions
                //paymentDetails.ButtonSource = CommonLogic.GetConfigValue("SBN_CODE");

                //paymentDetailsList.Add(paymentDetails);
                doExpressCheckoutPaymentRequestDetails.PaymentDetails = (List<PaymentDetailsType>)Session["paymentDetails"];//paymentDetailsList;

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
                                if (Session["NewCaseId"] != null)
                                {
                                    SendMail(Convert.ToInt64(Session["NewCaseId"]));
                                    Session["redirect"] = "~/ListNewCase.aspx";
                                }
                                else
                                {
                                    SendMail();
                                    Session["redirect"] = "~/ListSupplyOrder.aspx";
                                }
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
                        redirectto = "PaymentFailure.aspx";
                    }
                }
                //Session["RegistrationPaymentMessage"] = true;
                Response.Redirect(redirectto, false);
            }
            catch (System.Exception ex)
            {
                logger.Debug("Error Message : " + ex.Message);
            }
        }

        /// <summary>
        /// Method to save payment success details
        /// </summary>
        private void SavePaymentSuccessStatus()
        {
            try
            {
                PaymentSuccessEntity paymentEntity = new PaymentSuccessEntity();
                newPayment = paymentEntity.Create();
                newPayment = (_4eOrtho.DAL.PaymentSuccess)Session["PaymentSuccess"];
                newPayment.TransactionId = payPalServiceResponse.TransactionID;
                newPayment.TimeStamp = Convert.ToDateTime(payPalServiceResponse.Timestamp);
                newPayment.Status = Convert.ToString(payPalServiceResponse.PaymentStatus.Value).ToUpper();
                //newPayment.Status = responseDoExpressCheckoutPaymentResponseType != null ? responseDoExpressCheckoutPaymentResponseType.Ack.ToString().Trim().ToUpper() : string.Empty;
                newPayment.CreatedDate = DateTime.Now;

                if (divDiscount.Visible)
                {
                    LookupMaster lookup = new LookupMasterEntity().GetLookupMasterByDesc("FirstCaseDiscount");
                    if (lookup != null)
                    {
                        newPayment.LookupId = Convert.ToInt32(lookup.LookupId);
                    }
                }
                paymentEntity.Save(newPayment);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// Method to send mail for patient case and order supply.
        /// </summary>
        /// <param name="lcaseId"></param>
        private void SendMail(long lcaseId)
        {
            PatientCaseEmailDetail caseEmailData = new PatientCaseDetailEntity().GetPatientCaseEmailDetail(lcaseId);
            PatientCaseDetail patientCase = new PatientCaseDetailEntity().GetPatientCaseById(lcaseId);
            SupplyOrder supplyOrder = new SupplyOrderEntity().GetSupplyOrderById(patientCase.SupplyOrderId);

            //Create Document.
            CreateCasePdfForMail(supplyOrder, caseEmailData, patientCase);
            GenerateBeforeTemplatePDF(supplyOrder, caseEmailData, patientCase);

            if (caseEmailData != null)
            {
                string emailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("NewCaseEmailForDoctor")).ToString();
                string PatientEmailtemplatePath = Server.MapPath(CommonLogic.GetConfigValue("PatientRegistrationMail")).ToString();
                if (SessionHelper.IsSendPatientMail)
                {
                    string sPassword = Cryptography.DecryptStringAES(caseEmailData.Password.ToString(), CommonLogic.GetConfigValue("SharedSecret"));
                    //send mail to patient
                    PatientEntity.PatientRegistrationMail(caseEmailData.FirstName, caseEmailData.LastName, sPassword, PatientEmailtemplatePath
                        , caseEmailData.EmailId, "Patient", "RegistrationByDoctor", caseEmailData.DoctorEmailId, caseEmailData.DoctorFirstName, caseEmailData.DoctorLastName);
                }
                SendAllDetail(supplyOrder, caseEmailData, patientCase);
            }
        }

        /// <summary>
        /// Method to send mail for order supply
        /// </summary>
        private void SendMail()
        {
            string mailSubject = string.Empty;
            string status = string.Empty;
            string titleDoctor = string.Empty;
            string titleAdmin = string.Empty;

            SupplyOrder supplyOrder = (SupplyOrder)Session["supplyOrder"];
            if (supplyOrder != null)
            {
                string supplyName = Convert.ToString(Session["supplyName"]);

                currentSession = (CurrentSession)Session["UserLoginSession"];

                new SupplyOrderEntity().Save(supplyOrder);

                if (Convert.ToBoolean(supplyOrder.IsRecieved))
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
                    if (Convert.ToBoolean(supplyOrder.IsRecieved))
                    {
                        new SupplyOrderEntity().SendOrderSupplyDoctorMail(currentSession.DoctorLastName, supplyName, supplyOrder.Amount.ToString("0.00"), supplyOrder.Quantity.ToString("0"), currentSession.EmailId, "", doctorEmailtemplatePath, mailSubject, Convert.ToDecimal(supplyOrder.TotalAmount).ToString("0.00"), status, titleDoctor);
                        new SupplyOrderEntity().SendOrderSupplyAdminMail(currentSession.DoctorLastName, supplyName, supplyOrder.Amount.ToString("0.00"), supplyOrder.Quantity.ToString("0"), currentSession.EmailId, "", adminEmailtemplatePath, mailSubject, Convert.ToDecimal(supplyOrder.TotalAmount).ToString("0.00"), status, titleAdmin);
                    }
                    else
                    {
                        new SupplyOrderEntity().SendOrderSupplyMailWithPayment(currentSession.DoctorLastName, supplyName, supplyOrder.Amount.ToString("0.00"), supplyOrder.Quantity.ToString("0"), currentSession.EmailId, "", emailTemplatePath, mailSubject, Convert.ToDecimal(supplyOrder.TotalAmount).ToString("0.00"), status, titleDoctor, false);
                        new SupplyOrderEntity().SendOrderSupplyMailWithPayment(currentSession.DoctorLastName, supplyName, supplyOrder.Amount.ToString("0.00"), supplyOrder.Quantity.ToString("0"), currentSession.EmailId, "", emailTemplatePath, mailSubject, Convert.ToDecimal(supplyOrder.TotalAmount).ToString("0.00"), status, titleAdmin, true);
                    }
                }
            }
        }

        /// <summary>
        /// Method to send all details to user.
        /// </summary>
        /// <param name="supplyOrder"></param>
        /// <param name="patient"></param>
        /// <param name="patientCase"></param>
        private void SendAllDetail(SupplyOrder supplyOrder, PatientCaseEmailDetail patient, PatientCaseDetail patientCase)
        {
            try
            {
                string emailtemplatePath = Server.MapPath(CommonLogic.GetConfigValue("CaseSupplyPaymentDetails")).ToString();
                //string mailSubject = string.Empty;
                string status = string.Empty;
                string titleDoctor = string.Empty;
                string titleAdmin = string.Empty;
                string trackno = new TrackCaseEntity().GetTrackCaseByCaseId(patientCase.CaseId).TrackNo;
                currentSession = (CurrentSession)Session["UserLoginSession"];

                //mailSubject = "4ClearOrtho - Doctor Order Supply";
                titleAdmin = "The following order details are supplied:";
                titleDoctor = "The Order Supply details are following:";
                status = "Ordered";

                if (File.Exists(emailtemplatePath))
                {
                    string emailtemplateHTML = File.ReadAllText(emailtemplatePath);
                    emailtemplateHTML = emailtemplateHTML.Replace("##DoctorFirstName##", currentSession.DoctorFirstName);
                    emailtemplateHTML = emailtemplateHTML.Replace("##DoctorLastName##", currentSession.DoctorLastName);
                    emailtemplateHTML = emailtemplateHTML.Replace("##PatientFirstName##", patient.FirstName);
                    emailtemplateHTML = emailtemplateHTML.Replace("##PatientLastName##", patient.LastName);
                    emailtemplateHTML = emailtemplateHTML.Replace("##CaseNo##", patientCase.CaseNo);
                    emailtemplateHTML = emailtemplateHTML.Replace("##TrackNo##", trackno);

                    //emailtemplateHTML = emailtemplateHTML.Replace("##Title##", titleDoctor);
                    emailtemplateHTML = emailtemplateHTML.Replace("##DoctorName##", currentSession.DoctorName);

                    if (supplyOrder != null && supplyOrder.Quantity > 0)
                    {
                        emailtemplateHTML = emailtemplateHTML.Replace("##SupplyName##", hdnProductPackage.Value);
                        emailtemplateHTML = emailtemplateHTML.Replace("##Amount##", Convert.ToString(supplyOrder.Amount));
                        emailtemplateHTML = emailtemplateHTML.Replace("##Quantity##", Convert.ToString(supplyOrder.Quantity));
                        emailtemplateHTML = emailtemplateHTML.Replace("##TotalPackage##", Convert.ToString(supplyOrder.TotalAmount));
                        emailtemplateHTML = emailtemplateHTML.Replace("##TotalCasePackage##", Convert.ToString(Convert.ToDecimal(txtCaseCharge.Text.Trim()) + supplyOrder.TotalAmount));
                        emailtemplateHTML = emailtemplateHTML.Replace("##isSupply##", "");
                    }
                    else
                        emailtemplateHTML = emailtemplateHTML.Replace("##isSupply##", "none");

                    //emailtemplateHTML = emailtemplateHTML.Replace("##Remarks##", Convert.ToString(supplyOrder.Remarks));
                    emailtemplateHTML = emailtemplateHTML.Replace("##Status##", status);

                    emailtemplateHTML = emailtemplateHTML.Replace("##TRANSACTIONID##", SessionHelper.TransactionId);
                    emailtemplateHTML = emailtemplateHTML.Replace("##TRANSACTIONDATE##", !string.IsNullOrEmpty(SessionHelper.TransactionTime) ? SessionHelper.TransactionTime.Split(' ')[0] : string.Empty);
                    emailtemplateHTML = emailtemplateHTML.Replace("##TRANSACTIONAMOUNT##", SessionHelper.PaymentAmount);
                    emailtemplateHTML = emailtemplateHTML.Replace("##TRANSACTIONSTATUS##", "Success");

                    emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));
                    emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", System.DateTime.Now.Year.ToString());
                    emailtemplateHTML = emailtemplateHTML.Replace("##SiteURL##", CommonLogic.GetSiteURL() + @"Home.aspx");
                    emailtemplateHTML = emailtemplateHTML.Replace("##CaseCharge##", txtCaseCharge.Text.Trim());
                    emailtemplateHTML = emailtemplateHTML.Replace("##isdiscount##", divDiscount.Visible ? "" : "none");
                    emailtemplateHTML = emailtemplateHTML.Replace("##Discount##", (!string.IsNullOrEmpty(txtPromoDiscount.Text)) ? txtPromoDiscount.Text : string.Empty);
                    emailtemplateHTML = emailtemplateHTML.Replace("##TotalPaid##", SessionHelper.PaymentAmount);
                    emailtemplateHTML = emailtemplateHTML.Replace("##ExpressShipment##", (newPayment.Shipment != null ? Convert.ToDecimal(newPayment.Shipment).ToString("0.00") : "0.00"));
                    emailtemplateHTML = emailtemplateHTML.Replace("##LOCALCONTACT##", "<div>" + Session["LocalContactAddress"] + "</div>");

                    MailAddress fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("ToMail"));
                    MailAddress toMailAddress = new MailAddress(currentSession.EmailId, currentSession.DoctorName);

                    string filePaths = Server.MapPath("~/PDF/PatientCasePdf/" + patient.FirstName + "_" + patient.LastName + "_" + patientCase.CaseId + ".pdf");
                    filePaths += "," + Server.MapPath("~/PDF/PatientCasePdf/BeforeTemplate_" + patient.FirstName + "_" + patient.LastName + "_" + patientCase.CaseId + ".pdf");

                    CommonLogic.SendMailWithAttachment(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, "4ClearOrtho - Case and Supply Detail : " + patient.FirstName + " " + patient.LastName, filePaths, "");

                    emailtemplateHTML = emailtemplateHTML.Replace("Dear " + currentSession.DoctorFirstName + " " + currentSession.DoctorLastName, "Dear Admin");
                    emailtemplateHTML = emailtemplateHTML.Replace(titleDoctor, titleAdmin);
                    //emailtemplateHTML = emailtemplateHTML.Replace("You have", "Dr. " + currentSession.DoctorName);
                    toMailAddress = new MailAddress(CommonLogic.GetConfigValue("ToMail"));
                    CommonLogic.SendMailWithAttachment(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, "4ClearOrtho - Case and Supply Detail : " + patient.FirstName + " " + patient.LastName, filePaths, "");
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured" + ex);
            }
        }

        #endregion

        #region PDF Methods

        /// <summary>
        /// Method to Create Case Pdf For Mail.
        /// </summary>
        /// <param name="lcaseid"></param>
        private void CreateCasePdfForMail(SupplyOrder supplyOrder, PatientCaseEmailDetail patient, PatientCaseDetail patientCase)
        {
            try
            {
                if (patientCase != null)
                {
                    Patient patientDetail = new PatientEntity().GetPatientById(patientCase.PatientId);
                    if (patient != null)
                    {
                        DomainDoctorDetailsByEmail doctor = new DoctorEntity().GetDoctorListByEmail(patientCase.DoctorEmailId);

                        lblPrintCreated.Text = doctor.FullName;
                        lblPrintcreatedDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                        lblPrintCaseNo.Text = patientCase.CaseNo;
                        lblPrintDN.Text = doctor.FullName;
                        lblPrintPN.Text = patientDetail.FirstName + " " + patientDetail.LastName;
                        if (!string.IsNullOrEmpty(patientCase.Notes))
                            ltrPrintNotes.Text = "<span style=\"margin-left:2px;font-size: 14px;\">" + patientCase.Notes + "</span>";
                        else
                            ltrPrintNotes.Text = "";
                        lblPrintDOB.Text = patientDetail.BirthDate.ToString("MM/dd/yyyy");

                        if (patient.Gender == "M")
                            lblPrintGender.Text = "Male";
                        else if (patient.Gender == "F")
                            lblPrintGender.Text = "Female";

                        //Ortho System
                        string sOrthoSystem = string.Empty;
                        if (!string.IsNullOrEmpty(patientCase.OrthoSystem))
                        {
                            sOrthoSystem += "";
                            string[] arrOrthoSystem = patientCase.OrthoSystem.Split(',');
                            if (arrOrthoSystem.Length > 0)
                            {
                                foreach (string sOrtho in arrOrthoSystem)
                                {
                                    switch (sOrtho)
                                    {
                                        case "1":
                                            sOrthoSystem += "TRAY<br/>";
                                            break;
                                        case "2":
                                            sOrthoSystem += "BRACKET<br/>";
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                        sOrthoSystem += "";
                        ltrPrintOS.Text = sOrthoSystem;

                        //Ortho Condition
                        string sOrthoCondition = string.Empty;
                        if (!string.IsNullOrEmpty(patientCase.OrthoCondition))
                        {
                            sOrthoCondition += "";
                            string[] arrOrthocondition = patientCase.OrthoCondition.Split(',');
                            foreach (string sOrtho in arrOrthocondition)
                            {
                                switch (sOrtho)
                                {
                                    case "1":
                                        sOrthoCondition += "CROWDING<br/>";
                                        break;
                                    case "2":
                                        sOrthoCondition += "SPACING<br/>";
                                        break;
                                    case "3":
                                        sOrthoCondition += "CROSSBITE<br/>";
                                        break;
                                    case "4":
                                        sOrthoCondition += "ANTERIOR<br/>";
                                        break;
                                    case "5":
                                        sOrthoCondition += "POSTERIOR<br/>";
                                        break;
                                    case "6":
                                        sOrthoCondition += "OPENBITE<br/>";
                                        break;
                                    case "7":
                                        sOrthoCondition += "DEEPBITE<br/>";
                                        break;
                                    case "8":
                                        sOrthoCondition += "NARROWARCH<br/>";
                                        break;
                                    default:
                                        break;
                                }
                            }
                            sOrthoCondition += "";
                        }
                        ltrPrintOC.Text = sOrthoCondition;
                        //other Condition
                        if (!(string.IsNullOrEmpty(patientCase.OtherCondition)))
                        {
                            ltrPrintOther.Text = "<span style=\"font-size: 14px;\"><b>Other :&nbsp;</b>" + patientCase.OtherCondition + "</span>";
                        }
                        else
                            ltrPrintOther.Text = "";

                        //create pdf file\
                        StringWriter sw = new StringWriter();
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        pnlPrint.RenderControl(hw);
                        StringReader sr = new StringReader(sw.ToString());
                        iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10f, 10f, 25f, 10f);
                        PdfWriter writer =PdfWriter.GetInstance(pdfDoc, new FileStream(Server.MapPath("~/PDF/PatientCasePdf/" + patientDetail.FirstName + "_" + patientDetail.LastName + "_" + patient.CaseId + ".pdf"), FileMode.Create));
                        pdfDoc.Open();
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                        pdfDoc.Close();
                        hw.Flush();
                        hw.Dispose();
                        sw.Flush();
                        sw.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An Error Occured:" + ex);
            }
        }

        /// <summary>
        /// Method to Generate Before Template PDF for mail attachment.
        /// </summary>
        /// <param name="lcaseid"></param>
        private void GenerateBeforeTemplatePDF(SupplyOrder supplyOrder, PatientCaseEmailDetail patient, PatientCaseDetail patientCase)
        {
            try
            {
                Patient patientDetail = new PatientEntity().GetPatientById(patientCase.PatientId);

                Document document = new Document(PageSize.A4.Rotate(), 88f, 88f, 10f, 10f);
                iTextSharp.text.Font NormalFont = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
                FileStream fs = new FileStream(Server.MapPath("~/PDF/PatientCasePdf/BeforeTemplate_" + patientDetail.FirstName + "_" + patientDetail.LastName + "_" + patientCase.CaseId + ".pdf"), FileMode.Create);
                currentSession = (CurrentSession)Session["UserLoginSession"];
                using (fs)
                {
                    List<string> lstFileList = (List<string>)Session["lstFileList"];

                    if (lstFileList != null && lstFileList.Count > 0)
                    {
                        PdfWriter writer = PdfWriter.GetInstance(document, fs);
                        Phrase phrase = null;
                        PdfPCell cell = null;
                        PdfPTable table = null;                        

                        document.Open();

                        //Header Table
                        table = new PdfPTable(3);
                        table.TotalWidth = (float)(980 * 72 / 96);
                        table.LockedWidth = true;
                        table.DefaultCell.Border = PdfPCell.NO_BORDER;
                        table.SetWidths(new float[] { 0.3f, 0.3f, 0.3f });

                        int xheight = 225;
                        int xweight = 305;

                        cell = ImageCell(Server.MapPath("~/PatientFiles/slides/" + lstFileList[0]), xheight, xweight, PdfPCell.ALIGN_CENTER);
                        table.AddCell(cell);
                        cell = ImageCell(Server.MapPath("~/PatientFiles/slides/" + lstFileList[1]), xheight, xweight, PdfPCell.ALIGN_CENTER);
                        table.AddCell(cell);
                        cell = ImageCell(Server.MapPath("~/PatientFiles/slides/" + lstFileList[2]), xheight, xweight, PdfPCell.ALIGN_CENTER);
                        table.AddCell(cell);
                        cell = ImageCell(Server.MapPath("~/PatientFiles/slides/" + lstFileList[3]), xheight, xweight, PdfPCell.ALIGN_CENTER);
                        table.AddCell(cell);


                        PdfPTable innerTable = new PdfPTable(1);
                        BaseColor myColor = WebColors.GetRGBColor("#164D8E");
                        innerTable.SetTotalWidth(new float[] { 1f });
                        innerTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                        phrase = new Phrase();
                        phrase.Add(new Chunk("Before Treatment", FontFactory.GetFont("Arial", 20, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                        cell.PaddingBottom = 20f;
                        cell.BackgroundColor = myColor;
                        cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        cell.BorderColor = myColor;
                        innerTable.AddCell(cell);

                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Content/images/logo.png"));
                        image.ScaleAbsoluteHeight((float)(70 * 72 / 96));
                        image.ScaleAbsoluteWidth((float)(115 * 72 / 96));

                        PdfPCell innerCell = new PdfPCell(image);
                        innerCell.BackgroundColor = myColor;
                        innerCell.BorderColor = myColor;
                        innerCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        innerCell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        innerCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        innerTable.AddCell(innerCell);

                        phrase = new Phrase();
                        phrase.Add(new Chunk("Dr. " + currentSession.DoctorName, FontFactory.GetFont("Arial", 20, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                        cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                        cell.BackgroundColor = myColor;
                        cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        cell.BorderColor = myColor;
                        innerTable.AddCell(cell);

                        phrase = new Phrase();
                        phrase.Add(new Chunk("Patient: " + patientDetail.FirstName + " " + patientDetail.LastName, FontFactory.GetFont("Arial", 15, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                        cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                        cell.BackgroundColor = myColor;
                        cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        cell.BorderColor = myColor;
                        innerTable.AddCell(cell);

                        phrase = new Phrase();
                        phrase.Add(new Chunk("Created Date: " + patientDetail.CreatedDate.ToString("MM/dd/yyyy"), FontFactory.GetFont("Arial", 15, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                        cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                        cell.BackgroundColor = myColor;
                        cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        cell.BorderColor = myColor;
                        innerTable.AddCell(cell);

                        cell.AddElement(innerTable);
                        cell.PaddingLeft = 3f;
                        cell.PaddingRight = 3f;
                        cell.PaddingBottom = 10f;
                        cell.PaddingTop = 10f;
                        table.AddCell(cell);

                        cell = ImageCell(Server.MapPath("~/PatientFiles/slides/" + lstFileList[4]), xheight, xweight, PdfPCell.ALIGN_CENTER);
                        table.AddCell(cell);
                        cell = ImageCell(Server.MapPath("~/PatientFiles/slides/" + lstFileList[5]), xheight, xweight, PdfPCell.ALIGN_CENTER);
                        table.AddCell(cell);
                        cell = ImageCell(Server.MapPath("~/PatientFiles/slides/" + lstFileList[6]), xheight, xweight, PdfPCell.ALIGN_CENTER);
                        table.AddCell(cell);
                        cell = ImageCell(Server.MapPath("~/PatientFiles/slides/" + lstFileList[7]), xheight, xweight, PdfPCell.ALIGN_CENTER);
                        table.AddCell(cell);

                        document.Add(table);

                        document.Close();
                        writer.Close();
                        fs.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Method to drawline when we create PDF.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="color"></param>
        private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, iTextSharp.text.BaseColor color)
        {
            PdfContentByte contentByte = writer.DirectContent;
            contentByte.SetColorStroke(color);
            contentByte.MoveTo(x1, y1);
            contentByte.LineTo(x2, y2);
            contentByte.Stroke();
        }

        /// <summary>
        /// Method to create PDFCell.
        /// </summary>
        /// <param name="phrase"></param>
        /// <param name="align"></param>
        /// <returns></returns>
        private static PdfPCell PhraseCell(Phrase phrase, int align)
        {
            PdfPCell cell = new PdfPCell(phrase);
            //cell.BorderColor = iTextSharp.text.BaseColor.WHITE;                        
            cell.VerticalAlignment = align;
            cell.HorizontalAlignment = align;
            //cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
            //cell.HorizontalAlignment = align;
            return cell;
        }

        /// <summary>
        /// Method to create PDF Image Cell.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="align"></param>
        /// <returns></returns>
        private static PdfPCell ImageCell(string path, int height, int width, int align)
        {
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(path);
            image.ScaleAbsoluteHeight((float)(height * 72 / 96));
            image.ScaleAbsoluteWidth((float)(width * 72 / 96));
            //image.ScaleToFit((float)(width * 72 / 96), (float)(height * 72 / 96));
            PdfPCell cell = new PdfPCell(image);
            cell.BorderColor = iTextSharp.text.BaseColor.WHITE;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            //cell.HorizontalAlignment = align;
            cell.PaddingLeft = 3f;
            cell.PaddingRight = 3f;
            cell.PaddingBottom = 10f;
            cell.PaddingTop = 10f;
            return cell;
        }

        #endregion
    }
}