using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using log4net;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Transactions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho
{
    public partial class AddNewCase : PageBase
    {
        #region Declaration
        public static string _ResorseObject { get; set; }
        public long lCaseId;
        Boolean IsSendMail = true;
        Boolean IsSendPatientMail = false;
        CurrentSession currentSession;
        PatientEntity patientEntity;
        Patient patient;
        PatientCaseDetailEntity patientCaseDetailsEntity;
        PatientCaseDetail patientCase;
        private ILog logger = log4net.LogManager.GetLogger(typeof(AddNewCase));
        string path = HttpContext.Current.Request.PhysicalApplicationPath + "PatientFiles\\slides\\";
        string trackno = string.Empty;
        List<string> lstFileList = new List<string>();

        private long supplyOrderId = 0;
        private SupplyOrderEntity supplyOrderEntity;
        private SupplyOrder supplyOrder;
        private PackageGalleryEntity packageGalleryEntity;
        private List<PackageGallery> packageGalleries;
        private StageEntity stageEntity = new StageEntity();
        private List<Stage> lstStages = new List<Stage>();

        PayPalAPIInterfaceServiceService payPalService;
        DoDirectPaymentResponseType payPalServiceResponse;
        bool isPaypalExpress;
        bool isPaymentDone = false;
        #endregion

        #region Event

        /// <summary>
        /// Page Load Event
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

                lCaseId = (Session["NewCaseId"] != null) ? Convert.ToInt64(Session["NewCaseId"]) : 0;

                if (!IsPostBack)
                {
                    _ResorseObject = this.GetLocalResourceObject("_ResorseObject").ToString();
                    ViewState["isView"] = false;
                    BindCaseTypes();
                    BindPatientList();
                    BindPackageList();
                    BindYearList();
                    BindCaseCharge();

                    if (lCaseId > 0)
                    {
                        BindCaseDetail(lCaseId);

                        lblHeaderSelectPatient.Text = Convert.ToString(this.GetLocalResourceObject("ViewPatient"));

                        if (string.IsNullOrEmpty(SessionHelper.ReworkORRetainer) && SessionHelper.IsPayment)
                        {
                            lblHeadPayment.Text = Convert.ToString(this.GetLocalResourceObject("ViewPayment"));
                        }
                        else if (!string.IsNullOrEmpty(SessionHelper.ReworkORRetainer))
                            Session["NewCaseId"] = null;

                        if (phSelectPackage.Visible && ddlPackage.SelectedIndex > 0 && SessionHelper.IsPayment)
                        {
                            lblHeaderSelectPackage.Text = Convert.ToString(this.GetLocalResourceObject("ViewPackage"));
                        }

                        lblHeader.Text = (string.IsNullOrEmpty(SessionHelper.ReworkORRetainer)) ? Convert.ToString(this.GetLocalResourceObject("EditPatientCase")) : Convert.ToString(this.GetLocalResourceObject(SessionHelper.ReworkORRetainer));
                    }
                    else
                    {
                        lblDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                        CheckPromotionalDiscount();
                    }
                    BindStageCharge();
                }
                System.Web.UI.ScriptManager.GetCurrent(this.Page).RegisterAsyncPostBackControl(btnAddStage);
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error Occured", ex);
            }
        }

        private void BindStageCharge()
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
                            txtStageCharges.Text = obj.LookupName;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Save Patient Case Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    CalculateTotalAmount();
                    SaveNewCase();
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error Occured", ex);
            }
        }

        /// <summary>
        /// Event to set express checkout api operation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnExpressCheckout_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                CalculateTotalAmount();
                isPaypalExpress = true;
                SetExpressCheckoutAPIOperation();
                Session["lstFileList"] = null;
            }
            catch (Exception ex)
            {
                logger.Debug("Error Message : " + ex.Message);
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Method to apply discount.
        /// </summary>
        private void CalculateTotalAmount()
        {
            try
            {
                decimal Casecharge = 0;
                decimal discount = decimal.Round(string.IsNullOrEmpty(txtPromoDiscount.Text) ? 0 : Convert.ToDecimal(txtPromoDiscount.Text), 2);
                decimal PayableAmount = 0;
                currentSession = (CurrentSession)Session["UserLoginSession"];
                // Getting Case Charges and Discount Starts
                if (ddlCaseType.SelectedIndex > 0)
                {
                    if (!string.IsNullOrEmpty(SessionHelper.ReworkORRetainer))
                    {
                        LookupMaster lookup = new LookupMasterEntity().GetLookupMasterByDesc(string.IsNullOrEmpty(SessionHelper.ReworkORRetainer) ? "CaseCharge" : SessionHelper.ReworkORRetainer);
                        if (lookup != null)
                        {
                            Casecharge = Convert.ToDecimal(lookup.LookupName);
                            hdnCaseCharge.Value = Convert.ToDecimal(lookup.LookupName).ToString("0.00");
                        }
                    }
                    else
                    {
                        CaseCharge caseCharge = new CaseChargesEntity().GetDoctorCaseChargeByCaseId(Convert.ToInt64(ddlCaseType.SelectedItem.Value), currentSession.EmailId);
                        if (caseCharge != null)
                        {
                            Casecharge = decimal.Round(caseCharge.Amount, 2);
                        }
                        hdnCaseCharge.Value = Casecharge.ToString("0.00");
                        if (caseCharge != null && caseCharge.DiscountMaster != null)
                        {
                            if (caseCharge.DiscountMaster.CouponCode == txtDiscountCouponCode.Text.Trim() && caseCharge.DiscountMaster.ExpiryDate > DateTime.Now)
                            {
                                if (caseCharge.DiscountMaster.IsFlat)
                                {
                                    PayableAmount = caseCharge.Amount - caseCharge.DiscountMaster.Amount;
                                    Casecharge = caseCharge.Amount - caseCharge.DiscountMaster.Amount;
                                }
                                else
                                {
                                    PayableAmount = caseCharge.Amount - (caseCharge.Amount * caseCharge.DiscountMaster.Amount / 100);
                                    Casecharge = caseCharge.Amount - (caseCharge.Amount * caseCharge.DiscountMaster.Amount / 100);
                                }
                                hdnCaseTypeDiscount.Value = (Convert.ToDecimal(hdnCaseCharge.Value) - PayableAmount).ToString("0.00");
                            }
                        }
                    }
                }
                else
                {
                    LookupMaster lookup = new LookupMasterEntity().GetLookupMasterByDesc(string.IsNullOrEmpty(SessionHelper.ReworkORRetainer) ? "CaseCharge" : SessionHelper.ReworkORRetainer);
                    if (lookup != null)
                    {
                        Casecharge = Convert.ToDecimal(lookup.LookupName);
                        hdnCaseCharge.Value = Convert.ToDecimal(lookup.LookupName).ToString("0.00");
                    }
                }
                // Getting Case Charges and Discount Ends

                // Adding Shipping Charges Starts                
                if (!chkIsRegularShipment.Checked)
                {
                    OrthoCharge orthoCharges = new OrthoChargesEntity().GetOrthoCharges(ddlCaseType.SelectedItem.Value, currentSession.CountryId);
                    if (orthoCharges != null)
                    {
                        hdnExpressShipment.Value = orthoCharges.CaseShipmentCharge > 0 ? orthoCharges.CaseShipmentCharge.ToString("0.00") : orthoCharges.ExpressShipmentCharge.ToString("0.00");
                        Casecharge = Casecharge + Convert.ToDecimal(orthoCharges.CaseShipmentCharge > 0 ? orthoCharges.CaseShipmentCharge.ToString("0.00") : orthoCharges.ExpressShipmentCharge.ToString("0.00"));
                    }
                    else
                    {
                        hdnExpressShipment.Value = "0.00";
                    }
                }
                else
                {
                    hdnExpressShipment.Value = "0.00";
                }
                // Adding Shipping Charges Ends


                // Calculating Package Charges Ends
                decimal packagecharge = 0;
                if (ddlPackage.SelectedIndex > 0)
                {
                    PackageMasterEntity PackageMasterEntity = new PackageMasterEntity();
                    PackageMaster master = PackageMasterEntity.GetPackageByPackageId(Convert.ToInt64(ddlPackage.SelectedItem.Value));
                    if (master != null)
                    {
                        packagecharge = master.Amount;
                        hdnPackageAmt.Value = master.Amount.ToString("0.00");
                    }
                    if (!string.IsNullOrEmpty(txtQuantity.Text))
                    {
                        packagecharge = packagecharge * Convert.ToInt64(txtQuantity.Text);
                    }
                }

                // Calculating Package Charges Ends

                // Calculating Final Payment Starts
                Casecharge = Casecharge + packagecharge - discount;
                // Calculating Final Payment Ends                
                hdnTotalAmount.Value = Casecharge.ToString("0.00");
            }
            catch (Exception exp)
            {
                logger.Error("Error at applying discount in add new case page", exp);
            }
        }

        /// <summary>
        /// Method to Save new case.
        /// </summary>
        private void SaveNewCase()
        {
            try
            {
                txtCaseCharge.Text = hdnCaseCharge.Value;
                TransactionScope scope = new TransactionScope();

                string redirectto = "~/ListNewCase.aspx";

                using (scope)
                {
                    supplyOrderId = Convert.ToInt64(hdnSupplyId.Value);

                    _4eOrtho.DAL.PaymentSuccess payment = null;

                    if (hdnIsSkipPayment.Value == "0")
                    {
                        if (supplyOrderId > 0 || lCaseId > 0)
                            payment = new PaymentSuccessEntity().GetPaymentInfo(supplyOrderId, lCaseId);
                        if (rbtcashpayment.Checked)
                        {
                            isPaymentDone = true;
                            redirectto = "~/paymentsuccess.aspx";
                        }
                        else if (payment == null && !isPaypalExpress)
                        {
                            CallPayPalServiceForPayment();
                            isPaymentDone = payPalServiceResponse != null ? payPalServiceResponse.Ack.Value.ToString().ToUpper().Equals("SUCCESS") : false;
                            redirectto = "~/paymentsuccess.aspx";
                            Session["redirect"] = "~/ListNewCase.aspx";

                            if (isPaymentDone)
                            {
                                SessionHelper.PaymentAmount = payPalServiceResponse.Amount.value.ToString();
                                SessionHelper.TransactionId = payPalServiceResponse.TransactionID.ToString();
                                SessionHelper.TransactionTime = Convert.ToDateTime(payPalServiceResponse.Timestamp.ToString()).ToString("dd-MMM-yyyy HH:mm:ss");
                            }
                        }
                        else
                        {
                            isPaymentDone = true;
                            redirectto = "~/ListNewCase.aspx";
                        }
                    }

                    Session["NewCaseId"] = lCaseId = SavePatientCase(lCaseId);
                    supplyOrderId = SaveSupplyPackageDetail();

                    if (hdnIsSkipPayment.Value == "0")
                        SavePaymentSuccessStatus(lCaseId, supplyOrderId, payment);

                    if (payPalServiceResponse != null)
                    {
                        if (payPalServiceResponse.Ack.Value.ToString().ToUpper().Equals("FAILURE"))
                        {
                            SessionHelper.PaymentAmount = payPalServiceResponse.Amount.value.ToString();
                            SessionHelper.TransactionTime = Convert.ToDateTime(payPalServiceResponse.Timestamp.ToString()).ToString("dd-MMM-yyyy HH:mm:ss");

                            SessionHelper.PayPalServiceResponseErrors = payPalServiceResponse.Errors;
                            SavePaymentFailureStatus();
                            redirectto = "~/PaymentFailure.aspx";
                            Session["redirect"] = "~/AddNewCase.aspx";
                        }
                    }
                    scope.Complete();
                    SessionHelper.IsSendPatientMail = IsSendPatientMail;
                    SessionHelper.SupplyOrderId = supplyOrderId;
                }

                if (!isPaypalExpress && isPaymentDone)
                {
                    //GetCase Detail By Id
                    PatientCaseEmailDetail caseEmailData = new PatientCaseDetailEntity().GetPatientCaseEmailDetail(lCaseId);

                    if (caseEmailData != null && !Convert.ToBoolean(ViewState["isView"]))
                    {
                        //Create Document.
                        CreateCasePdfForMail(lCaseId);
                        GenerateBeforeTemplatePDF(lCaseId);

                        string emailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("NewCaseEmailForDoctor")).ToString();
                        string PatientEmailtemplatePath = Server.MapPath(CommonLogic.GetConfigValue("PatientRegistrationMail")).ToString();
                        if (IsSendPatientMail && isPaymentDone)
                        {
                            string sPassword = Cryptography.DecryptStringAES(caseEmailData.Password.ToString(), CommonLogic.GetConfigValue("SharedSecret"));
                            //send mail to patient
                            PatientEntity.PatientRegistrationMail(caseEmailData.FirstName, caseEmailData.LastName, sPassword, PatientEmailtemplatePath
                                , caseEmailData.EmailId, "Patient", "RegistrationByDoctor", caseEmailData.DoctorEmailId, caseEmailData.DoctorFirstName, caseEmailData.DoctorLastName);
                        }
                        if (IsSendMail && hdnSkip.Value.ToLower() == "true")
                        {
                            string filePaths = Server.MapPath("~/PDF/PatientCasePdf/" + patient.FirstName + "_" + patient.LastName + "_" + lCaseId + ".pdf");
                            filePaths += "," + Server.MapPath("~/PDF/PatientCasePdf/BeforeTemplate_" + patient.FirstName + "_" + patient.LastName + "_" + lCaseId + ".pdf");
                            PatientCaseDetailEntity.PatientCaseDetailsMail(caseEmailData.FirstName, caseEmailData.LastName, caseEmailData.DoctorFirstName,
                                caseEmailData.DoctorLastName, caseEmailData.CaseNo, trackno, emailTemplatePath, caseEmailData.DoctorEmailId, filePaths);
                            redirectto = "~/ListNewCase.aspx";
                        }
                        else
                            SendAllDetail(supplyOrder, patient, patientCase);
                    }
                }

                SessionHelper.ShippingCharge = hdnExpressShipment.Value.Trim();

                Session["lstFileList"] = null;
                if (!isPaypalExpress)
                    Response.Redirect(redirectto, true);
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured", ex);
                Session["lstFileList"] = null;
            }
        }

        /// <summary>
        /// Method to save patint case detail.
        /// </summary>
        /// <param name="lcaseId"></param>
        /// <returns></returns>
        private long SavePatientCase(long lcaseId)
        {
            try
            {
                patientEntity = new PatientEntity();
                patientCaseDetailsEntity = new PatientCaseDetailEntity();
                currentSession = (CurrentSession)Session["UserLoginSession"];

                if (lCaseId > 0)
                {
                    patientCase = patientCaseDetailsEntity.GetPatientCaseById(lCaseId);
                    if (patientCase != null)
                        patient = patientEntity.GetPatientById(patientCase.PatientId);
                    IsSendMail = false;
                }
                else
                {
                    patientCase = patientCaseDetailsEntity.Create();

                    if (rbtnNew.Checked && string.IsNullOrEmpty(hdnPatientId.Value))
                    {
                        IsSendPatientMail = true;
                        patient = patientEntity.Create();
                        //generate otp
                        hdnPassword.Value = GetUniquePassword(8);
                        patient.EmailId = txtEmail.Text;
                        patient.FirstName = txtFirstName.Text;
                        patient.LastName = txtLastName.Text;
                        patient.BirthDate = Convert.ToDateTime(txtDateofBirth.Text);
                        patient.Gender = rbtnMale.Checked ? "M" : "F";
                    }
                    else
                    {
                        patient = patientEntity.GetPatientById(Convert.ToInt64(hdnPatientId.Value));
                    }
                    patientCase.CaseNo = GetCaseNo(patient.FirstName, patient.LastName);
                    patientCase.CreatedBy = 0;
                }
                patientCase.IsActive = true;
                patient.IsActive = true;
                patient.IsDelete = false;

                long patientId = patientEntity.Save(patient);

                //Add to UserDomainMaster
                if (rbtnNew.Checked && lCaseId == 0 && string.IsNullOrEmpty(hdnPatientId.Value))
                {
                    User user = new UserEntity().Create();
                    user.CreatedDate = DateTime.Now;
                    user.EmailAddress = txtEmail.Text.Trim();
                    user.FirstName = txtFirstName.Text.Trim();
                    user.IsActive = true;
                    user.IsSuperAdmin = false;
                    user.LastName = txtLastName.Text.Trim();
                    user.Password = Cryptography.EncryptStringAES(hdnPassword.Value, CommonLogic.GetConfigValue("SharedSecret"));
                    user.UpdatedDate = DateTime.Now;
                    user.UserType = "P";
                    new UserEntity().Save(user);

                    SendMailOnRegistrationComplete(patientId);
                }

                //Add to patient Case Details
                patientCase.CaseCharge = Convert.ToDecimal(hdnCaseCharge.Value);
                patientCase.SupplyOrderId = supplyOrderId;
                patientCase.LastUpdatedBy = 0;
                patientCase.PatientId = patientId;
                patientCase.DoctorEmailId = currentSession.EmailId;
                patientCase.Notes = (!string.IsNullOrEmpty(txtNotes.Text.Trim())) ? txtNotes.Text.Trim() : string.Empty;

                if (ddlCaseType.Visible)
                    patientCase.CaseTypeId = Convert.ToInt32(ddlCaseType.SelectedItem.Value);
                else
                {
                    if (divDiscount.Visible)
                    {
                        LookupMaster lookup = new LookupMasterEntity().GetLookupMasterByDesc("FirstCaseDiscount");
                        if (lookup != null)
                            patientCase.CaseTypeId = Convert.ToInt32(lookup.LookupId);
                    }
                    else
                        patientCase.CaseTypeId = ViewState["CaseId"] != null ? Convert.ToInt32(ViewState["CaseId"]) : patientCase.CaseTypeId;
                }

                string sOrthoCondition = string.Empty;

                sOrthoCondition += (rbtnAnterior.Checked) ? (int)OrthoCondition.ANTERIOR + "," : (int)OrthoCondition.POSTERIOR + ",";

                foreach (System.Web.UI.WebControls.ListItem item in chkOrthoCondition.Items)
                {
                    if (item.Selected)
                    {
                        string sCondition = item.Value.ToString();
                        switch (sCondition)
                        {
                            case "CROWDING": sOrthoCondition += (int)OrthoCondition.CROWDING + ","; break;
                            case "SPACING": sOrthoCondition += (int)OrthoCondition.SPACING + ","; break;
                            case "CROSSBITE": sOrthoCondition += (int)OrthoCondition.CROSSBITE + ","; break;
                            case "OPENBITE": sOrthoCondition += (int)OrthoCondition.OPENBITE + ","; break;
                            case "DEEPBITE": sOrthoCondition += (int)OrthoCondition.DEEPBITE + ","; break;
                            case "NARROWARCH": sOrthoCondition += (int)OrthoCondition.NARROWARCH + ","; break;
                            default: break;
                        }
                    }
                }
                patientCase.OrthoCondition = sOrthoCondition;
                patientCase.OtherCondition = chkOtherCondition.Checked && (!string.IsNullOrEmpty(txtOtherCondition.Text.Trim())) ? txtOtherCondition.Text.Trim() : string.Empty;
                patientCase.IsActive = chkIsActive.Checked;
                patientCase.IsDelete = false;

                if (!string.IsNullOrEmpty(SessionHelper.ReworkORRetainer))
                {
                    patientCase.IsRetainer = SessionHelper.ReworkORRetainer.ToLower().Contains("retainer");
                    patientCase.IsRework = !patientCase.IsRetainer;
                    SessionHelper.CaseType = Regex.Replace(SessionHelper.ReworkORRetainer, "(\\B[A-Z])", " $1");
                }
                else
                    SessionHelper.CaseType = ddlCaseType.SelectedItem.Text;

                lcaseId = patientCaseDetailsEntity.Save(patientCase);

                SessionHelper.CaseNo = patientCase.CaseNo.ToString();
                SessionHelper.CaseCharge = patientCase.CaseCharge.ToString("0.00");
                SessionHelper.PackageId = Convert.ToInt32(ddlPackage.SelectedValue.ToString());

                if (IsSendMail)
                {
                    trackno = GetUniqueTrackNo(currentSession.DoctorFirstName, currentSession.DoctorLastName, 9);
                    //add track details
                    TrackCaseEntity trackCaseEntity = new TrackCaseEntity();
                    TrackCase trackCase = trackCaseEntity.Create();
                    trackCase.TrackNo = trackno;
                    trackCase.CreatedBy = 0;
                    trackCase.UpdatedBy = 0;
                    trackCase.UpdatedByEmail = currentSession.EmailId;
                    trackCase.CaseId = lcaseId;
                    trackCase.IsActive = true;
                    trackCase.IsDelete = false;
                    trackCase.Status = ((int)TrackingStatus.Submitted).ToString();
                    trackCaseEntity.Save(trackCase);
                }
                else
                {
                    TrackCase trackCase = new TrackCaseEntity().GetTrackCaseByCaseId(lcaseId);
                    if (trackCase != null)
                        trackno = trackCase.TrackNo;
                }

                SaveImage();
                SavePhotoGallery(Convert.ToInt64(hdnBeforeId.Value), lcaseId, "Before", patient.EmailId);

                return lcaseId;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured", ex);
                return 0;
            }
        }

        /// <summary>
        /// Method to Save Supply Package Details.
        /// </summary>
        /// <returns></returns>
        private long SaveSupplyPackageDetail()
        {
            try
            {
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                if (currentSession != null && ddlPackage.SelectedIndex > 0)
                {
                    supplyOrderEntity = new SupplyOrderEntity();
                    if (supplyOrderId > 0)
                    {
                        supplyOrder = supplyOrderEntity.GetSupplyOrderById(supplyOrderId);
                        supplyOrder.LastUpdatedBy = Authentication.GetLoggedUserID();
                        supplyOrder.LastUpdatedDate = BaseEntity.GetServerDateTime;
                        supplyOrder.IsRecieved = chkIsRecieved.Checked;
                    }
                    else
                    {
                        supplyOrder = supplyOrderEntity.Create();
                        supplyOrder.CreatedBy = Authentication.GetLoggedUserID();
                        supplyOrder.CreatedDate = BaseEntity.GetServerDateTime;
                        supplyOrder.Quantity = Convert.ToInt16(txtQuantity.Text);
                        supplyOrder.Amount = Convert.ToDecimal(hdnPackageAmt.Value);
                        supplyOrder.TotalAmount = supplyOrder.Amount * Convert.ToInt64(txtQuantity.Text);
                        supplyOrder.PackageId = Convert.ToInt32(ddlPackage.SelectedValue.ToString());
                        supplyOrder.ProductId = 0;
                        supplyOrder.FirstName = currentSession.DoctorFirstName;
                        supplyOrder.LastName = currentSession.DoctorLastName;
                        supplyOrder.EmailId = currentSession.EmailId;
                        supplyOrder.DoctorId = Authentication.GetLoggedUserID();
                    }
                    supplyOrder.IsActive = true;
                    supplyOrderEntity.Save(supplyOrder);

                    patientCase.SupplyOrderId = supplyOrder.SupplyOrderId;
                    patientCaseDetailsEntity.Save(patientCase);

                    //if (change)
                    //{
                    //    if (chkIsRecieved.Checked)
                    //    {
                    //        supplyOrder.IsDispatch = true;
                    //        mailSubject = "4ClearOrtho - Doctor Order Received";
                    //        titleAdmin = "The following order has been received by Doctor.";
                    //        titleDoctor = "You have received below item(s) as per your order.";
                    //        status = "Order Received";
                    //    }
                    //    else
                    //    {
                    //        supplyOrder.IsDispatch = false;
                    //        mailSubject = "4ClearOrtho - Doctor Order Supply";
                    //        titleAdmin = "The following order details are supplied.";
                    //        titleDoctor = "The Order Supply details are following.";
                    //        status = "Order Supply";
                    //    }


                    //    supplyName = ddlPackage.SelectedItem.ToString();
                    //    string doctorEmailtemplatePath = Server.MapPath(CommonLogic.GetConfigValue("DoctorSendOrderSupplyMailDoctor")).ToString();
                    //    string adminEmailtemplatePath = Server.MapPath(CommonLogic.GetConfigValue("DoctorSendOrderSupplyMailAdmin")).ToString();

                    //    supplyOrderEntity.SendOrderSupplyDoctorMail(currentSession.DoctorName, supplyName, Convert.ToString(supplyOrder.Amount), txtQuantity.Text, currentSession.EmailId, "", doctorEmailtemplatePath, mailSubject, Convert.ToString(supplyOrder.TotalAmount), status, titleDoctor);
                    //    supplyOrderEntity.SendOrderSupplyAdminMail(currentSession.DoctorName, supplyName, Convert.ToString(supplyOrder.Amount), txtQuantity.Text, currentSession.EmailId, "", adminEmailtemplatePath, mailSubject, Convert.ToString(supplyOrder.TotalAmount), status, titleAdmin);
                    //}
                }
                return supplyOrder != null ? supplyOrder.SupplyOrderId : 0;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured", ex);
                return 0;
            }
        }

        /// <summary>
        /// Method to send all details to user.
        /// </summary>
        /// <param name="supplyOrder"></param>
        /// <param name="patient"></param>
        /// <param name="patientCase"></param>
        private void SendAllDetail(SupplyOrder supplyOrder, Patient patient, PatientCaseDetail patientCase)
        {
            try
            {
                if (supplyOrder != null)
                {
                    string emailtemplatePath = Server.MapPath(CommonLogic.GetConfigValue("CaseSupplyPaymentDetails")).ToString();
                    string status = string.Empty;
                    string titleDoctor = string.Empty;
                    string titleAdmin = string.Empty;
                    currentSession = (CurrentSession)Session["UserLoginSession"];

                    if (chkIsRecieved.Checked)
                    {
                        supplyOrder.IsDispatch = true;
                        titleAdmin = "The following order has been received by Doctor:";
                        titleDoctor = "You have received below item(s) as per your order:";
                        status = "Order Received";
                    }
                    else
                    {
                        supplyOrder.IsDispatch = false;
                        titleAdmin = "The following order details are supplied:";
                        titleDoctor = "The Order Supply details are following:";
                        status = "Ordered";
                    }

                    if (File.Exists(emailtemplatePath))
                    {
                        string emailtemplateHTML = File.ReadAllText(emailtemplatePath);
                        emailtemplateHTML = emailtemplateHTML.Replace("##DoctorFirstName##", currentSession.DoctorFirstName);
                        emailtemplateHTML = emailtemplateHTML.Replace("##DoctorLastName##", currentSession.DoctorLastName);
                        emailtemplateHTML = emailtemplateHTML.Replace("##PatientFirstName##", patient.FirstName);
                        emailtemplateHTML = emailtemplateHTML.Replace("##PatientLastName##", patient.LastName);
                        emailtemplateHTML = emailtemplateHTML.Replace("##CaseNo##", patientCase.CaseNo);
                        emailtemplateHTML = emailtemplateHTML.Replace("##TrackNo##", trackno);
                        emailtemplateHTML = emailtemplateHTML.Replace("##DoctorName##", currentSession.DoctorName);
                        emailtemplateHTML = emailtemplateHTML.Replace("##SupplyName##", ddlPackage.SelectedItem.Text);
                        emailtemplateHTML = emailtemplateHTML.Replace("##Amount##", Convert.ToString(supplyOrder.Amount));
                        emailtemplateHTML = emailtemplateHTML.Replace("##Quantity##", Convert.ToString(supplyOrder.Quantity));
                        emailtemplateHTML = emailtemplateHTML.Replace("##Status##", status);
                        emailtemplateHTML = emailtemplateHTML.Replace("##TRANSACTIONID##", SessionHelper.TransactionId);
                        emailtemplateHTML = emailtemplateHTML.Replace("##TRANSACTIONDATE##", !string.IsNullOrEmpty(SessionHelper.TransactionTime) ? SessionHelper.TransactionTime.Split(' ')[0] : string.Empty);
                        emailtemplateHTML = emailtemplateHTML.Replace("##TRANSACTIONAMOUNT##", SessionHelper.PaymentAmount);
                        emailtemplateHTML = emailtemplateHTML.Replace("##TRANSACTIONSTATUS##", "Success");
                        emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));
                        emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", System.DateTime.Now.Year.ToString());
                        emailtemplateHTML = emailtemplateHTML.Replace("##SiteURL##", CommonLogic.GetSiteURL() + @"Home.aspx");
                        emailtemplateHTML = emailtemplateHTML.Replace("##TotalPackage##", Convert.ToString(supplyOrder.TotalAmount));
                        emailtemplateHTML = emailtemplateHTML.Replace("##CaseCharge##", txtCaseCharge.Text.Trim());
                        emailtemplateHTML = emailtemplateHTML.Replace("##TotalCasePackage##", Convert.ToString(Convert.ToDecimal(txtCaseCharge.Text.Trim()) + supplyOrder.TotalAmount + (!chkIsRegularShipment.Checked ? Convert.ToDecimal(hdnExpressShipment.Value) : 0)));
                        emailtemplateHTML = emailtemplateHTML.Replace("##ExpressShipment##", (!chkIsRegularShipment.Checked ? hdnExpressShipment.Value : "0.00"));
                        emailtemplateHTML = emailtemplateHTML.Replace("##isdiscount##", divDiscount.Visible ? "" : "none");
                        emailtemplateHTML = emailtemplateHTML.Replace("##Discount##", (!string.IsNullOrEmpty(txtPromoDiscount.Text)) ? txtPromoDiscount.Text : string.Empty);
                        emailtemplateHTML = emailtemplateHTML.Replace("##TotalPaid##", SessionHelper.PaymentAmount);

                        if (chkIsRegularShipment.Checked)
                            emailtemplateHTML = emailtemplateHTML.Replace("##LOCALCONTACT##", "<div>" + divLocalContact.InnerHtml + "</div>");
                        else
                            emailtemplateHTML = emailtemplateHTML.Replace("##LOCALCONTACT##", string.Empty);

                        if (chkIsRecieved.Checked)
                            emailtemplateHTML = emailtemplateHTML.Replace("block", "none");

                        MailAddress fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("ToMail"));
                        MailAddress toMailAddress = new MailAddress(currentSession.EmailId, currentSession.DoctorName);

                        string filePaths = Server.MapPath("~/PDF/PatientCasePdf/" + patient.FirstName + "_" + patient.LastName + "_" + patientCase.CaseId + ".pdf");

                        if (File.Exists(Server.MapPath("~/PDF/PatientCasePdf/BeforeTemplate_" + patient.FirstName + "_" + patient.LastName + "_" + patientCase.CaseId + ".pdf")))
                            filePaths += "," + Server.MapPath("~/PDF/PatientCasePdf/BeforeTemplate_" + patient.FirstName + "_" + patient.LastName + "_" + patientCase.CaseId + ".pdf");

                        CommonLogic.SendMailWithAttachment(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, "4ClearOrtho - Case and Supply Detail : " + patient.FirstName + " " + patient.LastName, filePaths, "");

                        emailtemplateHTML = emailtemplateHTML.Replace("Dear " + currentSession.DoctorFirstName + " " + currentSession.DoctorLastName, "Dear Admin");
                        emailtemplateHTML = emailtemplateHTML.Replace(titleDoctor, titleAdmin);
                        toMailAddress = new MailAddress(CommonLogic.GetConfigValue("ToMail"));
                        CommonLogic.SendMailWithAttachment(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, "4ClearOrtho - Case and Supply Detail : " + patient.FirstName + " " + patient.LastName, filePaths, "");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured", ex);
            }
        }

        /// <summary>
        /// Method to Save Photo Gallery.
        /// </summary>
        /// <param name="patientGalleryId"></param>
        /// <param name="CaseId"></param>
        /// <param name="sBeforeAfter"></param>
        /// <param name="patientEmailId"></param>
        private void SavePhotoGallery(long patientGalleryId, long CaseId, string sBeforeAfter, string patientEmailId)
        {
            try
            {
                PatientGalleryEntity patientGalleryEntity = new PatientGalleryEntity();
                PatientGalleryMasterEntity patientGalleryMasterEntity = new PatientGalleryMasterEntity();
                PatientGalleryMaster patientGalleryMaster;

                if (string.IsNullOrEmpty(SessionHelper.ReworkORRetainer) && patientGalleryId > 0)
                {
                    patientGalleryMaster = patientGalleryMasterEntity.GetPatientGalleryById(patientGalleryId);
                    patientGalleryMaster.LastUpdatedBy = Authentication.GetLoggedUserID();
                    patientGalleryMaster.LastUpdatedDate = BaseEntity.GetServerDateTime;
                    patientGalleryEntity.RemoveGalleryIdFiles(patientGalleryId);
                }
                else
                {
                    patientGalleryMaster = patientGalleryMasterEntity.Create();
                    patientGalleryMaster.CreatedBy = Authentication.GetLoggedUserID();
                    patientGalleryMaster.CreatedDate = BaseEntity.GetServerDateTime;
                }

                patientGalleryMaster.PatientId = Convert.ToInt64(currentSession.PatientId);
                patientGalleryMaster.Treatment = sBeforeAfter;
                patientGalleryMaster.IsActive = chkIsActive.Checked;
                patientGalleryMaster.CaseId = CaseId;
                patientGalleryMaster.isTemplate = true;
                patientGalleryMaster.DoctorEmail = currentSession.EmailId;
                patientGalleryMaster.PatientEmail = patientEmailId;
                patientGalleryId = patientGalleryMasterEntity.Save(patientGalleryMaster);

                PatientGallery patientGallery;

                foreach (string newFileName in lstFileList)
                {
                    if (sBeforeAfter == "Before" && lstFileList.IndexOf(newFileName) > 7)
                        break;
                    else if (sBeforeAfter == "After" && lstFileList.IndexOf(newFileName) <= 7)
                        continue;

                    patientGallery = patientGalleryEntity.Create();
                    patientGallery.CreatedBy = Authentication.GetLoggedUserID();
                    patientGallery.CreatedDate = BaseEntity.GetServerDateTime;
                    patientGallery.GalleryId = patientGalleryId;
                    patientGallery.FileName = newFileName;
                    patientGallery.IsActive = true;
                    patientGalleryEntity.Save(patientGallery);
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured", ex);
            }
        }

        /// <summary>
        /// Method to save images on server.
        /// </summary>
        private void SaveImage()
        {
            try
            {
                if (Session["lstFileList"] != null)
                    lstFileList = (List<string>)Session["lstFileList"];

                FileUpload imgFile;
                for (int i = 1; i <= 8; i++)
                {
                    imgFile = new FileUpload();
                    imgFile = (FileUpload)pnlImage.FindControl("fuFile" + i);

                    if (imgFile == null)
                        imgFile = (FileUpload)pnlImage1.FindControl("fuFile" + i);

                    if (imgFile != null && imgFile.HasFile)
                    {
                        if (imgFile.PostedFile != null)
                        {
                            string photographName = imgFile.PostedFile.FileName;
                            String FileExtension = Path.GetExtension(photographName).ToLower();
                            Guid newName = Guid.NewGuid();
                            string[] phName = photographName.Split('.');
                            string newPhotoName = phName[0].ToString() + "_" + newName + "." + phName[1].ToString();
                            string newPhotoPath = path + newPhotoName;
                            Stream strm = imgFile.PostedFile.InputStream;
                            GenerateThumbnails(0.5, strm, newPhotoPath);

                            if (lstFileList != null && i <= lstFileList.Count)
                            {
                                if (File.Exists(path + lstFileList[i - 1]) && string.IsNullOrEmpty(SessionHelper.ReworkORRetainer))
                                {
                                    File.Delete(path + lstFileList[i - 1]);
                                }
                                lstFileList[i - 1] = newPhotoName;
                            }
                            else
                                lstFileList.Add(newPhotoName);
                        }
                    }
                }
                Session["lstFileList"] = lstFileList;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured", ex);
            }
        }

        /// <summary>
        /// Method to compress and generate thumbnails image from file upload control.
        /// </summary>
        /// <param name="scaleFactor"></param>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        private void GenerateThumbnails(double scaleFactor, Stream sourcePath, string targetPath)
        {
            using (var image = System.Drawing.Image.FromStream(sourcePath))
            {
                var newWidth = (int)(image.Width * scaleFactor);
                var newHeight = (int)(image.Height * scaleFactor);
                var thumbnailImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath, image.RawFormat);
            }
        }

        /// <summary>
        /// Method to Get Cas No.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        private string GetCaseNo(string firstName, string lastName)
        {
            string caseNo = string.Empty;

            if (!string.IsNullOrEmpty(SessionHelper.ReworkORRetainer))
            {
                if (hdnCaseNo.Value.Contains("-"))
                {
                    caseNo = hdnCaseNo.Value.Split('-')[0];

                    PatientCaseDetail oldSameCaseNo = new PatientCaseDetailEntity().GetCaseByCaseNo(caseNo);
                    if (oldSameCaseNo != null)
                    {
                        if (oldSameCaseNo.CaseNo.Contains("-"))
                        {
                            caseNo = oldSameCaseNo.CaseNo.Split('-')[0];
                            int count = Convert.ToInt16(oldSameCaseNo.CaseNo.Split('-')[1]);
                            caseNo = caseNo + "-" + (count + 1);
                        }
                    }
                }
                else
                {
                    PatientCaseDetail oldSameCaseNo = new PatientCaseDetailEntity().GetCaseByCaseNo(hdnCaseNo.Value);
                    if (oldSameCaseNo != null)
                    {
                        if (oldSameCaseNo.CaseNo.Contains("-"))
                        {
                            caseNo = oldSameCaseNo.CaseNo.Split('-')[0];
                            int count = Convert.ToInt16(oldSameCaseNo.CaseNo.Split('-')[1]);
                            caseNo = caseNo + "-" + (count + 1);
                        }
                        else
                        {
                            caseNo = oldSameCaseNo.CaseNo + "-" + 1;
                        }
                    }
                    else
                        caseNo = hdnCaseNo.Value + "-" + 1;
                }
            }
            else
                caseNo = GetUniqueCaseNo(8, firstName, lastName);
            return caseNo;
        }

        /// <summary>
        /// Method to Get Unique Password.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private string GetUniquePassword(int size)
        {
            char[] chars = new char[70];
            string a;
            byte[] data = new byte[size];
            a = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!@#$%&*1234567890";
            chars = a.ToCharArray();
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            { result.Append(chars[b % (chars.Length)]); }
            return result.ToString();
        }

        /// <summary>
        /// Method to Get Unique Case No.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="sFirstName"></param>
        /// <param name="sLastName"></param>
        /// <returns></returns>
        private string GetUniqueCaseNo(int size, string sFirstName, string sLastName)
        {
            char[] chars = new char[50];
            string a = "1234567890";
            byte[] data = new byte[size];
            StringBuilder result = new StringBuilder(size);
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            if (!string.IsNullOrEmpty(sFirstName))
            {
                chars = sFirstName.ToCharArray();
                result.Append(chars[0].ToString().ToUpper());
                chars = sLastName.ToCharArray();
                result.Append(chars[0].ToString().ToUpper());
                chars = a.ToCharArray();
                crypto.GetNonZeroBytes(data);
                foreach (byte b in data)
                { result.Append(chars[b % (chars.Length)]); }
            }
            return result.ToString();
        }

        /// <summary>
        /// Method to Get Unique Track No.
        /// </summary>
        /// <param name="doctorFirstName"></param>
        /// <param name="docotorLastName"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private string GetUniqueTrackNo(string doctorFirstName, string docotorLastName, int size)
        {
            string sFirstName, sLastName;
            char[] chars = new char[50];
            string a = "1234567890";
            byte[] data = new byte[size];
            StringBuilder result = new StringBuilder(size);
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            sFirstName = doctorFirstName;
            sLastName = docotorLastName;
            chars = sFirstName.ToCharArray();
            result.Append(chars[0].ToString().ToUpper());
            chars = sLastName.ToCharArray();
            result.Append(chars[0].ToString().ToUpper());
            chars = a.ToCharArray();
            crypto.GetNonZeroBytes(data);
            foreach (byte b in data)
            { result.Append(chars[b % (chars.Length)]); }
            return result.ToString();
        }

        /// <summary>
        /// Method to check promotional discount
        /// </summary>
        private void CheckPromotionalDiscount()
        {
            currentSession = (CurrentSession)Session["UserLoginSession"];
            if (currentSession.IsCertified && currentSession.SourceType.ToLower() == "aad")
            {
                LookupMaster lookupFirstCase = new LookupMasterEntity().GetLookupMasterByDesc("FirstCaseDiscount");
                if (lookupFirstCase != null)
                {
                    PaymentSuccessEntity paymentSuccessEntity = new PaymentSuccessEntity();
                    if (!paymentSuccessEntity.IsFirstCasePaymentDone(currentSession.EmailId, lookupFirstCase.LookupId))
                    {
                        LookupMaster lookup = new LookupMasterEntity().GetLookupMasterByDesc("FirstCaseDiscountDate");
                        if (lookup != null && Convert.ToDateTime(lookup.LookupName) > DateTime.Now)
                        {
                            txtPromoDiscount.Text = lookupFirstCase.LookupName;
                            divDiscount.Visible = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Method to get case details by CaseId.
        /// </summary>
        /// <param name="lcaseid"></param>
        private void BindCaseDetail(long lcaseid)
        {
            try
            {
                patientCaseDetailsEntity = new PatientCaseDetailEntity();
                patientEntity = new PatientEntity();
                patientCase = patientCaseDetailsEntity.GetPatientCaseById(lcaseid);

                if (patientCase != null)
                {
                    patient = patientEntity.GetPatientById(patientCase.PatientId);

                    txtFirstName.Text = patient.FirstName;
                    txtLastName.Text = patient.LastName;
                    txtDateofBirth.Text = patient.BirthDate.ToString("MM/dd/yyyy");
                    txtEmail.Text = patient.EmailId;
                    lblDate.Text = patientCase.CreatedDate.ToString("MM/dd/yyyy");
                    rbtnExisting.Checked = true;
                    ddlPatient.SelectedIndex = ddlPatient.Items.IndexOf(ddlPatient.Items.FindByValue(patient.EmailId));
                    ddlCaseType.SelectedIndex = ddlCaseType.Items.IndexOf(ddlCaseType.Items.FindByValue(Convert.ToString(patientCase.CaseTypeId)));

                    if (ddlCaseType.SelectedIndex > 0)
                    {
                        ddlCaseType.SelectedItem.Value = patientCase.CaseTypeId.ToString();
                        OrthoCharge orthoCharges = new OrthoChargesEntity().GetOrthoCharges(ddlCaseType.SelectedItem.Value, Convert.ToInt64(currentSession.CountryId));
                        if (orthoCharges != null)
                            hdnExpressShipment.Value = txtExpressShipment.Text = (orthoCharges.CaseShipmentCharge > 0 ? orthoCharges.CaseShipmentCharge.ToString("0.00") : orthoCharges.ExpressShipmentCharge.ToString("0.00"));
                    }
                    else
                        divCaseType.Visible = false;

                    hdnPatientId.Value = Convert.ToString(patient.PatientId);

                    if (string.IsNullOrEmpty(SessionHelper.ReworkORRetainer))
                    {
                        // Added By navik
                        hdnCaseCharge.Value = patientCase.CaseCharge.ToString("0.00");
                        txtCaseCharge.Text = patientCase.CaseCharge.ToString("0.00");
                    }
                    else
                    {
                        hdnCaseTypeID.Value = ddlCaseType.SelectedIndex.ToString();
                    }

                    if (patient.Gender == "M")
                        rbtnMale.Checked = true;
                    else if (patient.Gender == "F")
                        rbtnFemale.Checked = true;
                    if (!string.IsNullOrEmpty(patientCase.Notes))
                        txtNotes.Text = patientCase.Notes;
                    hdnCaseNo.Value = patientCase.CaseNo;
                    dvExistingPatientName.Visible = false;

                    //Ortho Condition
                    string sOrthoCondition = string.Empty;
                    if (!string.IsNullOrEmpty(patientCase.OrthoCondition))
                    {
                        sOrthoCondition = patientCase.OrthoCondition;
                        foreach (System.Web.UI.WebControls.ListItem item in chkOrthoCondition.Items)
                        {
                            string sCondition = item.Value.ToString();
                            switch (sCondition)
                            {
                                case "CROWDING":
                                    if (sOrthoCondition.Contains('1'))
                                        item.Selected = true;
                                    break;
                                case "SPACING":
                                    if (sOrthoCondition.Contains('2'))
                                        item.Selected = true;
                                    break;
                                case "CROSSBITE":
                                    if (sOrthoCondition.Contains('3'))
                                        item.Selected = true;
                                    break;
                                case "OPENBITE":
                                    if (sOrthoCondition.Contains('6'))
                                        item.Selected = true;
                                    break;
                                case "DEEPBITE":
                                    if (sOrthoCondition.Contains('7'))
                                        item.Selected = true;
                                    break;
                                case "NARROWARCH":
                                    if (sOrthoCondition.Contains('8'))
                                        item.Selected = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (sOrthoCondition.Contains('4'))
                            rbtnAnterior.Checked = true;
                        else if (sOrthoCondition.Contains('5'))
                            rbtnPosterior.Checked = true;
                    }
                    //other Condition
                    if (!(string.IsNullOrEmpty(patientCase.OtherCondition)))
                    {
                        txtOtherCondition.Text = patientCase.OtherCondition;
                        chkOtherCondition.Checked = true;
                    }
                    chkIsActive.Checked = patientCase.IsActive;

                    BindGallery(lcaseid);

                    supplyOrderId = patientCase.SupplyOrderId;
                    hdnSupplyId.Value = Convert.ToString(supplyOrderId);

                    BindSupplyOrder(lcaseid, patientCase.CaseCharge);

                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured", ex);
            }
        }

        /// <summary>
        /// Bind order value by supplyorder id
        /// </summary>
        private void BindSupplyOrder(long lcaseid, decimal caseCharge)
        {
            try
            {
                supplyOrderEntity = new SupplyOrderEntity();
                supplyOrder = supplyOrderEntity.GetSupplyOrderById(supplyOrderId);

                txtPackageAmt.Text = "0.00";

                if (supplyOrder != null)
                {
                    if (supplyOrder.IsDispatch)
                    {
                        dvDispatchRemarks.Visible = true;
                        dvRecieved.Visible = true;
                    }

                    chkIsRecieved.Checked = Convert.ToBoolean(supplyOrder.IsRecieved);

                    BindPackageList();
                    ddlPackage.SelectedValue = Convert.ToString(supplyOrder.PackageId);
                    BindPackageDetail(supplyOrder.PackageId);
                    flPackageDetails.Visible = true;

                    txtPackageAmount.Text = Convert.ToString(supplyOrder.Amount);
                    lblDispatchRemarks.Text = supplyOrder.Remarks;
                    txtQuantity.Text = Convert.ToString(supplyOrder.Quantity);
                    txtPackageAmt.Text = txtAmount.Text = !string.IsNullOrEmpty(Convert.ToString(supplyOrder.TotalAmount)) ? Convert.ToString(supplyOrder.TotalAmount) : "0.00";
                }
                else
                    CheckPromotionalDiscount();

                if (string.IsNullOrEmpty(SessionHelper.ReworkORRetainer))
                {
                    _4eOrtho.DAL.PaymentSuccess payment = new PaymentSuccessEntity().GetPaymentInfo(supplyOrderId, lcaseid);
                    if (payment != null)
                    {
                        isPaymentDone = true;
                        if (payment.Discount != null && payment.Discount > 0)
                        {
                            txtTotalCasePackage.Text = (Convert.ToDecimal(supplyOrder != null ? supplyOrder.TotalAmount : 0) + Convert.ToDecimal(caseCharge)).ToString("0.00");
                            txtPromoDiscount.Text = Convert.ToDecimal(payment.Discount).ToString("0.00");
                            divDiscount.Visible = true;
                        }
                        txtCaseCharge.Text = caseCharge > 0 ? Convert.ToDecimal(caseCharge).ToString("0.00") : "0";
                        txtCashAmount.Text = txtPayableAmt.Text = payment.Ammount.ToString("0.00");

                        txtCardNo.Text = payment.CardNo;
                        txtNameOnCard.Text = payment.NameOnCard;
                        ddlCardType.SelectedValue = payment.CardType;
                        ddlMonth.SelectedValue = payment.ExpiryMonth.ToString();
                        ddlYear.SelectedValue = payment.ExpiryYear.ToString();
                        txtExpressShipment.Text = payment.Shipment != null && payment.Shipment > 0 ? Convert.ToDecimal(payment.Shipment).ToString("0.00") : "0";
                        chkIsRegularShipment.Visible = false;
                        divCaseTypeDiscount.Visible = false;
                        if (payment.LookupId != null && payment.LookupId > 0)
                        {
                            ddlCaseType.SelectedIndex = ddlCaseType.Items.IndexOf(ddlCaseType.Items.FindByValue(payment.LookupId.ToString()));
                            if (ddlCaseType.SelectedIndex == 0)
                            {
                                ddlCaseType.Visible = false;
                                LookupMaster caseType = new LookupMasterEntity().GetLookupMasterById(Convert.ToInt64(payment.LookupId));
                                if (caseType != null)
                                {
                                    ddlCaseType.Items.Clear();
                                    ddlCaseType.Items.Add(Regex.Replace(caseType.LookupDescription, "(\\B[A-Z])", " $1"));
                                    ddlCaseType.Visible = true;
                                    ddlCaseType.Enabled = false;
                                }
                            }
                            else
                                ddlCaseType.Enabled = false;
                        }
                        ViewState["isView"] = true;
                        divpaymenttobedone.Visible = false;
                    }
                    else
                        isPaymentDone = false;
                }
                DisableAllControl();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// package bind to repeater packagename
        /// </summary>
        /// <param name="packageId"></param>
        private void BindPackageDetail(long packageId)
        {
            packageGalleryEntity = new PackageGalleryEntity();
            packageGalleries = packageGalleryEntity.GetPackageGalleriesByPackageId(Convert.ToInt32(ddlPackage.SelectedValue));
            StringBuilder imageHtml = new StringBuilder();
            if (packageGalleries.Count > 0)
            {
                imageHtml.AppendLine("<div style='float:left;width:200px;'><div class='parsonal_textfild'>");
                imageHtml.AppendLine("<span id='ContentPlaceHolder1_lblProductImage'>" + this.GetLocalResourceObject("PackageImage") + "</span><span class='alignright'>:<span class='asteriskclass'>&nbsp;</span></span></div></div>");
                imageHtml.AppendLine("<div class='date_cont'><div style='float: left;margin-left: 5px;width: 350px;'>");
                foreach (PackageGallery packageGallery in packageGalleries)
                {
                    imageHtml.AppendLine("<a class='example-image-link' title='" + _ResorseObject + "' href='Files/" + packageGallery.FileName + "' data-lightbox='example-1'><img class='example-image' src='Files/thumbs/" + packageGallery.FileName + "' height='100' width='100'></a>");
                }
                imageHtml.AppendLine("</div></div>");
            }
            dvPackageImagelist.InnerHtml = imageHtml.ToString();
            List<PackageDetails> lstPackageDetails = new List<PackageDetails>();
            lstPackageDetails = new PackageMasterEntity().GetPackageDetailsByPackageId(Convert.ToInt64(ddlPackage.SelectedValue));
            if (lstPackageDetails.Count > 0)
            {
                rptPackageImage.DataSource = lstPackageDetails;
                rptPackageImage.DataBind();
                rptPackageImage.Visible = true;
            }
        }

        /// <summary>
        /// Method to bind case charge.
        /// </summary>
        private void BindCaseCharge()
        {
            if (!string.IsNullOrEmpty(SessionHelper.ReworkORRetainer))
            {
                divCaseType.Visible = false;
                divCaseTypeDiscount.Visible = false;
                LookupMaster lookup = new LookupMasterEntity().GetLookupMasterByDesc(SessionHelper.ReworkORRetainer);
                if (lookup != null)
                {
                    hdnCaseCharge.Value = txtCaseCharge.Text = Convert.ToDecimal(lookup.LookupName).ToString("0.00");

                    ViewState["CaseId"] = lookup.LookupId;
                }
            }

            currentSession = (CurrentSession)Session["UserLoginSession"];
            if (currentSession != null)
            {
                if (currentSession.CountryId > 0)
                {
                    hdnCountryId.Value = currentSession.CountryId.ToString();

                    OrthoCharge orthocharge = new OrthoChargesEntity().GetOrthoChargeByCountryId(currentSession.CountryId);
                    if (orthocharge != null)
                        hdnExpressShipment.Value = txtExpressShipment.Text = orthocharge.ExpressShipmentCharge.ToString("0.00");
                    else
                        divExpressShipment.Visible = false;

                    LocalContact localContact = new LocalContactEntity().GetLocalContact(currentSession.DoctorCity, currentSession.StateId, currentSession.CountryId);
                    if (localContact != null)
                    {
                        if (localContact.AddressMaster != null)
                        {
                            WSB_Country country = new CountryEntity().GetCountryByCountryId(Convert.ToInt32(localContact.AddressMaster.CountryId));
                            WSB_State state = new StateEntity().GetStateByStateId(Convert.ToInt32(localContact.AddressMaster.StateId));
                            string countryName = country != null ? country.CountryName : string.Empty;
                            string stateName = state != null ? state.StateName : string.Empty;

                            lblName.Text = localContact.OrganizationName;
                            lblAddress.Text = localContact.AddressMaster.Street + ", " + localContact.AddressMaster.City + ", " + stateName + ", " + localContact.AddressMaster.ZipCode + ", " + countryName;
                            lblContact.Text = "Phone : " + (!string.IsNullOrEmpty(localContact.ContactMaster.WorkContact) ? localContact.ContactMaster.WorkContact : localContact.ContactMaster.Mobile);
                            lblLocalEmail.Text = localContact.ContactMaster.EmailID;
                        }
                    }
                    else
                    {
                        divLocalContact.Visible = false;
                    }
                }
                else
                {
                    divExpressShipment.Visible = false;
                }
            }
        }

        /// <summary>
        /// Bind package list
        /// </summary>
        private void BindPackageList()
        {
            try
            {
                List<PackageMaster> lstProductMaster = new List<PackageMaster>();
                lstProductMaster = new PackageMasterEntity().GetPackageMaster();
                if (lstProductMaster != null && lstProductMaster.Count > 0)
                {
                    ddlPackage.DataSource = lstProductMaster;
                    ddlPackage.DataTextField = "PackageName";
                    ddlPackage.DataValueField = "PackageId";
                    ddlPackage.DataBind();
                }
                ddlPackage.Items.Insert(0, new System.Web.UI.WebControls.ListItem(this.GetLocalResourceObject("SelectPackage").ToString(), "0"));
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// patient list bind to dropdown
        /// </summary>
        private void BindPatientList()
        {
            patientEntity = new PatientEntity();
            currentSession = (CurrentSession)Session["UserLoginSession"];
            if (currentSession != null)
            {
                List<PatientListByDoctorEmail> lstPatient = patientEntity.GetAllPatientByDoctorEmail(currentSession.EmailId);
                if (lstPatient != null && lstPatient.Count > 0)
                {
                    ddlPatient.DataSource = lstPatient;
                    ddlPatient.DataTextField = "PatientName";
                    ddlPatient.DataValueField = "EmailId";
                    ddlPatient.DataBind();
                }
            }
            ddlPatient.Items.Insert(0, new System.Web.UI.WebControls.ListItem(this.GetLocalResourceObject("SelectPatient").ToString(), "0"));
        }

        /// <summary>
        /// Method to fill gallery by caseId.
        /// </summary>
        /// <param name="lcaseid"></param>
        private void BindGallery(long lcaseid)
        {
            try
            {
                //Image imgGallery;
                List<PatientGalleryMaster> lstPatientGalleryMaster = new PatientGalleryMasterEntity().GetPatientGalleryByCaseId(lcaseid);
                if (lstPatientGalleryMaster != null && lstPatientGalleryMaster.Count > 0)
                {
                    hdnBeforeId.Value = lstPatientGalleryMaster.Find(x => x.Treatment.Equals("before", StringComparison.InvariantCultureIgnoreCase)).PatientGalleryId.ToString();
                    hdnAfterId.Value = lstPatientGalleryMaster.Find(x => x.Treatment.Equals("after", StringComparison.InvariantCultureIgnoreCase)) != null ? lstPatientGalleryMaster.Find(x => x.Treatment.Equals("after", StringComparison.InvariantCultureIgnoreCase)).PatientGalleryId.ToString() : "0";

                    foreach (PatientGalleryMaster patientGalleryMaster in lstPatientGalleryMaster)
                    {
                        List<PatientGallery> lstPatientGallery = new PatientGalleryEntity().GetPatientGalleriesByGalleryId(patientGalleryMaster.PatientGalleryId);
                        if (lstPatientGallery != null && lstPatientGallery.Count > 0)
                        {
                            foreach (PatientGallery patientGallery in lstPatientGallery)
                            {
                                lstFileList.Add(patientGallery.FileName);
                            }
                        }
                    }

                    Session["lstFileList"] = lstFileList;

                    hdnImages.Value = string.Join(",", lstFileList.ToArray());
                    int j = 17;
                    if (hdnAfterId.Value == "0")
                        j = 9;

                    for (int i = 1; i < j; i++)
                    {
                        RequiredFieldValidator rfv;
                        rfv = (RequiredFieldValidator)pnlImage.FindControl("rfvFile" + i);
                        if (rfv == null)
                            rfv = (RequiredFieldValidator)pnlImage1.FindControl("rfvFile" + i);
                        if (rfv != null)
                        {
                            rfv.Enabled = false;
                            rfv.ValidationGroup = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured", ex);
            }
        }

        /// <summary>
        /// Bind Case Types
        /// </summary>
        private void BindCaseTypes()
        {
            List<GetCaseType> lstCaseType = new LookupMasterEntity().GetCaseType("CaseType");

            if (lstCaseType != null && lstCaseType.Count > 0)
            {
                hdnCaseType.Value = string.Join(",", lstCaseType.Select(x => x.LookupDescription.Split('|')[1]));

                ddlCaseType.DataSource = lstCaseType;
                ddlCaseType.DataTextField = "LookupName";
                ddlCaseType.DataValueField = "LookupId";
                ddlCaseType.DataBind();
            }
            ddlCaseType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Case Type", "0"));
        }

        /// <summary>
        /// Method to Disable all control.
        /// </summary>
        private void DisableAllControl()
        {
            #region Tab : Patient
            ddlPatient.Enabled = false;
            dvType.Style.Add("display", "none");
            dvExistingPatientName.Style.Add("display", "none");
            rbtnExisting.Enabled = false;
            rbtnNew.Enabled = false;
            txtEmail.Enabled = false;
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            txtDateofBirth.Enabled = false;
            txtDateofBirth.ReadOnly = true;
            rbtnMale.Enabled = false;
            rbtnFemale.Enabled = false;
            #endregion

            if (string.IsNullOrEmpty(SessionHelper.ReworkORRetainer) && isPaymentDone)
            {
                #region Tab : Before After Template
                fuFile1.Enabled = false;
                fuFile2.Enabled = false;
                fuFile3.Enabled = false;
                fuFile4.Enabled = false;
                fuFile5.Enabled = false;
                fuFile6.Enabled = false;
                fuFile7.Enabled = false;
                fuFile8.Enabled = false;
                fuFile9.Enabled = false;
                fuFile10.Enabled = false;
                fuFile11.Enabled = false;
                fuFile12.Enabled = false;
                fuFile13.Enabled = false;
                fuFile14.Enabled = false;
                fuFile15.Enabled = false;
                fuFile16.Enabled = false;
                #endregion

                #region Tab : Case Detail                
                rbtnAnterior.Enabled = false;
                rbtnPosterior.Enabled = false;
                chkOrthoCondition.Enabled = false;
                chkOtherCondition.Enabled = false;
                txtOtherCondition.Enabled = false;
                txtNotes.Enabled = false;
                chkIsActive.Enabled = false;
                #endregion

                ddlPackage.Enabled = false;
                txtQuantity.Enabled = false;

                #region Tab : Make Payment
                if (SessionHelper.IsPayment)
                {
                    ddlMonth.Enabled = false;
                    txtCardNo.Enabled = false;
                    txtNameOnCard.Enabled = false;
                    ddlCardType.Enabled = false;
                    txtAmount.Enabled = false;
                    ddlYear.Enabled = false;
                    txtCCVNo.Enabled = false;
                    divSelectPayment.Visible = false;
                }
                #endregion
            }
            else
            {
                if (isPaymentDone)
                    hdnSupplyId.Value = hdnBeforeId.Value = "0";
                else
                    hdnSupplyId.Value = "0";
            }
        }

        #endregion

        #region Payment

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
            catch (Exception ex)
            {
                logger.Error("An error occured", ex);
            }
        }

        /// <summary>
        /// This function will prepare credit card detail
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private CreditCardDetailsType GetCreditCardDetail()
        {
            CreditCardDetailsType creditCard = new CreditCardDetailsType();
            try
            {

                creditCard.CardOwner = GetCrediCardPayerInfo();
                creditCard.CreditCardNumber = txtCardNo.Text.ToString().Trim();
                creditCard.CreditCardType = (CreditCardTypeType)Enum.Parse(typeof(CreditCardTypeType), ddlCardType.SelectedValue);
                creditCard.CVV2 = txtCCVNo.Text.ToString().Trim();
                creditCard.ExpMonth = Convert.ToInt32(ddlMonth.SelectedValue.ToString());
                creditCard.ExpYear = Convert.ToInt32(ddlYear.SelectedValue.ToString());
                return creditCard;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured", ex);
                return null;
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
                currentSession = (CurrentSession)Session["UserLoginSession"];

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
                logger.Error("An error occured", exp);
                return null;
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
                    value = hdnTotalAmount.Value
                };
                paymentDetail.OrderTotal = paymentAmount;
                return paymentDetail;
            }
            catch (Exception exp)
            {
                logger.Error("An error occured", exp);
                return null;
            }
        }

        /// <summary>
        /// This function will save status if payment transaction is successfull
        /// </summary>
        /// <param name="userInfo"></param>
        public void SavePaymentSuccessStatus(long caseId, long supplyOrderId, _4eOrtho.DAL.PaymentSuccess payment)
        {
            try
            {
                if (payment == null)
                {
                    PaymentSuccessEntity paymentEntity = new PaymentSuccessEntity();
                    _4eOrtho.DAL.PaymentSuccess newPayment = paymentEntity.Create();

                    newPayment.Ammount = Convert.ToDecimal(hdnTotalAmount.Value);
                    newPayment.CreatedDate = DateTime.Now;
                    newPayment.SupplyOrderId = supplyOrderId;
                    newPayment.DoctorEmailId = ((CurrentSession)Session["UserLoginSession"]).EmailId;
                    newPayment.Discount = (!string.IsNullOrEmpty(txtPromoDiscount.Text)) ? Convert.ToDecimal(txtPromoDiscount.Text) : newPayment.Discount;
                    newPayment.Discount = newPayment.Discount + Convert.ToDecimal(hdnCaseTypeDiscount.Value);
                    newPayment.CaseId = caseId;
                    newPayment.IsCashPayed = false;

                    if (ddlCaseType.Visible)
                        newPayment.LookupId = Convert.ToInt32(ddlCaseType.SelectedItem.Value);

                    if (!chkIsRegularShipment.Checked)
                        newPayment.Shipment = Convert.ToDecimal(hdnExpressShipment.Value.Trim());

                    if (divDiscount.Visible)
                    {
                        LookupMaster lookup = new LookupMasterEntity().GetLookupMasterByDesc("FirstCaseDiscount");
                        if (lookup != null)
                        {
                            newPayment.LookupId = Convert.ToInt32(lookup.LookupId);
                        }
                    }
                    newPayment.LookupId = ViewState["CaseId"] != null ? Convert.ToInt32(ViewState["CaseId"]) : newPayment.LookupId;

                    if (rbtcashpayment.Checked)
                    {
                        newPayment.IsCashPayed = true;
                        newPayment.Status = "PENDING";
                        paymentEntity.Save(newPayment);
                    }
                    else if (!string.IsNullOrEmpty(txtCardNo.Text) && isPaymentDone)
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
                        newPayment.TimeStamp = Convert.ToDateTime(payPalServiceResponse.Timestamp);
                        newPayment.Status = payPalServiceResponse.Ack.Value.ToString();
                        newPayment.TransactionId = payPalServiceResponse.TransactionID.ToString();
                        newPayment.TransactionRespons = payPalService.getLastResponse();
                        paymentEntity.Save(newPayment);
                    }
                    else
                    {
                        Session["PaymentSuccess"] = newPayment;

                        if (chkIsRegularShipment.Checked)
                            Session["LocalContactAddress"] = hdnLocalContact.Value;
                        else
                            Session["LocalContactAddress"] = string.Empty;
                    }
                }
            }
            catch (Exception exp)
            {
                logger.Error("An error occured", exp);
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
                PaymentFailureEntity paymentEntity = new PaymentFailureEntity();
                _4eOrtho.DAL.PaymentFailure newPayment = paymentEntity.Create();
                currentSession = (CurrentSession)Session["UserLoginSession"];
                newPayment.Ammount = Convert.ToDecimal(hdnTotalAmount.Value);
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
                logger.Error("An error occured", exp);
            }
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
                    setExpressCheckoutRequestDetails.CancelURL = CommonLogic.GetConfigValue("CancelURL").ToLower().Replace("payment", "AddNewCase");

                    List<PaymentDetailsType> paymentDetailsList = new List<PaymentDetailsType>();
                    PaymentDetailsType paymentDetails1 = new PaymentDetailsType();
                    BasicAmountType orderTotal = new BasicAmountType(CurrencyCodeType.USD, hdnTotalAmount.Value.Trim());
                    paymentDetails1.OrderTotal = orderTotal;
                    paymentDetails1.PaymentAction = PaymentActionCodeType.ORDER;

                    if (!chkIsRegularShipment.Checked)
                    {
                        string totalWithoutShipping = (Convert.ToDecimal(hdnTotalAmount.Value.Trim()) - Convert.ToDecimal(hdnExpressShipment.Value.Trim())).ToString("0.00");
                        paymentDetails1.ShippingTotal = new BasicAmountType(CurrencyCodeType.USD, hdnExpressShipment.Value.Trim());
                        paymentDetails1.ItemTotal = new BasicAmountType(CurrencyCodeType.USD, totalWithoutShipping);
                    }

                    PaymentDetailsItemType paymentDetailsItemType = new PaymentDetailsItemType();
                    paymentDetailsItemType.Amount = new BasicAmountType(CurrencyCodeType.USD, hdnCaseCharge.Value.Trim());
                    paymentDetailsItemType.Name = "Case Fees";
                    paymentDetailsItemType.Description = "Case";
                    paymentDetailsItemType.Quantity = 1;
                    paymentDetailsItemType.Number = Convert.ToString(lCaseId);

                    paymentDetails1.PaymentDetailsItem.Add(paymentDetailsItemType);

                    if (currentSession != null && ddlPackage.SelectedIndex > 0)
                    {
                        paymentDetailsItemType = new PaymentDetailsItemType();
                        paymentDetailsItemType.Amount = new BasicAmountType(CurrencyCodeType.USD, (!string.IsNullOrEmpty(hdnPackageAmt.Value) ? hdnPackageAmt.Value : txtPackageAmount.Text));
                        paymentDetailsItemType.Name = "Supply Package (" + ddlPackage.SelectedItem.Text + ")";
                        paymentDetailsItemType.Description = "Package";
                        paymentDetailsItemType.Quantity = Convert.ToInt16(txtQuantity.Text);
                        paymentDetailsItemType.Number = ddlPackage.SelectedItem.Text;
                        paymentDetails1.PaymentDetailsItem.Add(paymentDetailsItemType);
                    }
                    if ((!string.IsNullOrEmpty(txtPromoDiscount.Text) && Convert.ToDecimal(txtPromoDiscount.Text) > 0) || !string.IsNullOrEmpty(txtDiscountCouponCode.Text))
                    {
                        decimal totalDiscount = Convert.ToDecimal(txtPromoDiscount.Text) + Convert.ToDecimal(hdnCaseTypeDiscount.Value);
                        paymentDetailsItemType = new PaymentDetailsItemType();
                        paymentDetailsItemType.Amount = new BasicAmountType(CurrencyCodeType.USD, "-" + totalDiscount.ToString("0.00"));
                        paymentDetailsItemType.Name = "Discount";
                        paymentDetailsItemType.Description = "Discount";
                        paymentDetailsItemType.Quantity = 1;
                        paymentDetails1.PaymentDetailsItem.Add(paymentDetailsItemType);
                    }

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
                            SaveNewCase();
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
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("Error Message", ex);
            }
            return responseSetExpressCheckoutResponseType;
        }

        private void SendMailOnRegistrationComplete(long lPatientId)
        {
            try
            {
                PatientRegistrationDetailForMail patientDetail = new PatientEntity().GetPatientRegistrationDetailForEmail(lPatientId);
                if (patientDetail != null)
                {
                    string emailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("PatientRegistrationMail")).ToString();
                    string sPassword = Cryptography.DecryptStringAES(patientDetail.Password, CommonLogic.GetConfigValue("SharedSecret"));
                    string userType = string.Empty;
                    if (patientDetail.UserType == "P")
                        userType = "Patient";
                    PatientEntity.PatientRegistrationMail(patientDetail.FirstName, patientDetail.LastName, sPassword, emailTemplatePath, patientDetail.EmailId, userType, "Registration", "", "", "");
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        #endregion

        #region PDF Methods

        /// <summary>
        /// Method to Create Case Pdf For Mail.
        /// </summary>
        /// <param name="lcaseid"></param>
        private void CreateCasePdfForMail(long lcaseid)
        {
            try
            {
                Patient patient;
                PatientCaseDetailEntity patientCaseDetailEntity = new PatientCaseDetailEntity();
                PatientCaseDetail patientCase = patientCaseDetailEntity.GetPatientCaseById(lcaseid);

                if (patientCase != null)
                {
                    patient = new PatientEntity().GetPatientById(patientCase.PatientId);
                    if (patient != null)
                    {
                        DomainDoctorDetailsByEmail doctor = new DoctorEntity().GetDoctorListByEmail(patientCase.DoctorEmailId);

                        lblPrintCreated.Text = doctor.FullName;
                        lblPrintcreatedDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                        lblPrintCaseNo.Text = patientCase.CaseNo;
                        lblPrintDN.Text = doctor.FullName;
                        lblPrintPN.Text = patient.FirstName + " " + patient.LastName;
                        if (!string.IsNullOrEmpty(patientCase.Notes))
                            ltrPrintNotes.Text = "<span style=\"margin-left:2px;font-size: 14px;\">" + patientCase.Notes + "</span>";
                        else
                            ltrPrintNotes.Text = "";
                        lblPrintDOB.Text = patient.BirthDate.ToString("MM/dd/yyyy");

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
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Server.MapPath("~/PDF/PatientCasePdf/" + patient.FirstName + "_" + patient.LastName + "_" + lcaseid + ".pdf"), FileMode.Create));
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
                logger.Error("An Error Occured:", ex);
            }
        }

        /// <summary>
        /// Method to Generate Before Template PDF for mail attachment.
        /// </summary>
        /// <param name="lcaseid"></param>
        private void GenerateBeforeTemplatePDF(long lcaseid)
        {
            try
            {
                lstFileList = (List<string>)Session["lstFileList"];

                if (lstFileList != null && lstFileList.Count > 0)
                {
                    Document document = new Document(PageSize.A4.Rotate(), 88f, 88f, 10f, 10f);
                    iTextSharp.text.Font NormalFont = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
                    FileStream fs = new FileStream(Server.MapPath("~/PDF/PatientCasePdf/BeforeTemplate_" + patient.FirstName + "_" + patient.LastName + "_" + lcaseid + ".pdf"), FileMode.Create);
                    currentSession = (CurrentSession)Session["UserLoginSession"];
                    using (fs)
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
                        phrase.Add(new Chunk("Patient: " + patient.FirstName + " " + patient.LastName, FontFactory.GetFont("Arial", 15, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                        cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                        cell.BackgroundColor = myColor;
                        cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        cell.BorderColor = myColor;
                        innerTable.AddCell(cell);

                        phrase = new Phrase();
                        phrase.Add(new Chunk(lblCreated.Text + patient.CreatedDate.ToString("MM/dd/yyyy"), FontFactory.GetFont("Arial", 15, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
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
            cell.VerticalAlignment = align;
            cell.HorizontalAlignment = align;
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
            PdfPCell cell = new PdfPCell(image);
            cell.BorderColor = iTextSharp.text.BaseColor.WHITE;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.PaddingLeft = 3f;
            cell.PaddingRight = 3f;
            cell.PaddingBottom = 10f;
            cell.PaddingTop = 10f;
            return cell;
        }

        #endregion

        #region WebMethods

        /// <summary>
        /// Method to validate month.
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool ServerMonthValidate(string month, string year)
        {
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            int selectedMonth = Convert.ToInt32(month);
            int selectedYear = Convert.ToInt32(year);

            if (currentYear == selectedYear && selectedMonth < currentMonth)
                return false;
            else
                return true;
        }

        /// <summary>
        /// This functin will validate card number by selected card type
        /// This function will use regex function for validating card
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        /// 
        [WebMethod]
        public static bool ValidateCardNoByCardType(string cardNo, string cardType)
        {
            if (cardType.ToUpper().Equals("VISA"))
                return Regex.IsMatch(cardNo, @"^4[0-9]{12}(?:[0-9]{3})?$");
            if (cardType.ToUpper().Equals("MASTERCARD"))
                return Regex.IsMatch(cardNo, @"^5[1-5][0-9]{14}$");
            if (cardType.ToUpper().Equals("DISCOVER"))
                return Regex.IsMatch(cardNo, @"^6(?:011|5[0-9]{2})[0-9]{12}$");
            if (cardType.ToUpper().Equals("AMEX"))
                return Regex.IsMatch(cardNo, @"^3[47][0-9]{13}$");
            else
                return false;
        }

        /// <summary>
        /// Method to get patient details on email text changes.
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        [WebMethod]
        public static PatientSerialize txtEmailChange(string emailId)
        {
            ILog logger = log4net.LogManager.GetLogger(typeof(AddNewCase));

            try
            {
                Patient patient = new PatientEntity().GetPatientByEmail(emailId.Trim());
                if (patient != null)
                {
                    PatientSerialize patientSerialize = new PatientSerialize();
                    patientSerialize.FirstName = patient.FirstName;
                    patientSerialize.LastName = patient.LastName;
                    patientSerialize.BirthDate = patient.BirthDate.ToString("MM/dd/yyyy");
                    patientSerialize.Gender = patient.Gender;
                    patientSerialize.PatientId = patient.PatientId.ToString();
                    patientSerialize.CreatedDate = patient.CreatedDate.ToString("MM/dd/yyyy");

                    User user = new UserEntity().GetPatientUserByEmailAddress(patient.EmailId);
                    if(user != null)
                    {
                        if (user.is_connect_user == true) { 
                            patientSerialize.IsConnectUser = true;                            
                        }
                    }

                    return patientSerialize;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occure.", ex);
                return null;
            }
        }

        /// <summary>
        /// Method to get package details by packageId.
        /// </summary>
        /// <param name="packageId"></param>
        /// <returns></returns>
        [WebMethod]
        public static PackageSerialize ddlPackageChange(string packageId)
        {
            PackageSerialize packageSerialize = new PackageSerialize();
            packageSerialize.Amount = string.Empty;
            packageSerialize.ImageHtml = string.Empty;
            packageSerialize.PackageDetailsHtml = string.Empty;

            PackageMaster packageMaster = new PackageMaster();
            packageMaster = new PackageMasterEntity().GetPackageByPackageId(Convert.ToInt64(packageId));
            if (packageMaster != null)
            {
                packageSerialize.Amount = Convert.ToString(packageMaster.Amount);
            }

            List<PackageGallery> lstPackageGalleries = new PackageGalleryEntity().GetPackageGalleriesByPackageId(Convert.ToInt32(packageId));
            StringBuilder imageHtml = new StringBuilder();
            if (lstPackageGalleries.Count > 0)
            {
                imageHtml.AppendLine("<div class='parsonal_textfild alignleft'><div class='date_cont_right'>");
                imageHtml.AppendLine("<label><span id='lblProductImage'>" + "##lblProductImage##" + "</span><span class='alignright'>:<span class='asteriskclass'>&nbsp;</span></span></label></div></div>");
                imageHtml.AppendLine("<div class='date_cont'><div class='date_cont_right'>");
                foreach (PackageGallery packageGallery in lstPackageGalleries)
                {
                    imageHtml.AppendLine("<a class='example-image-link' title='" + _ResorseObject + "' href='Files/" + packageGallery.FileName + "' data-lightbox='example-1'><img class='example-image' src='Files/thumbs/" + packageGallery.FileName + "' height='100' width='100'></a>");
                }
                imageHtml.AppendLine("</div></div>");
            }
            packageSerialize.ImageHtml = imageHtml.ToString();

            List<PackageDetails> lstPackageDetails = new List<PackageDetails>();
            lstPackageDetails = new PackageMasterEntity().GetPackageDetailsByPackageId(Convert.ToInt64(packageId));
            if (lstPackageDetails.Count > 0)
            {
                string td = "<td><div class='grenchk dark' id='flex'><div class='whitetext'>##Value##</div></div></td>";
                imageHtml = new StringBuilder();

                imageHtml.Append("<tr><td><div class='topadd_f flex'><span class='one'><span>##ProductName##</span></span></div></td>");
                imageHtml.Append("<td><div class='topadd_f flex'><span class='one'><span>##Quantity##</span></span></div></td>");
                imageHtml.Append("<td><div class='topadd_f flex'><span class='one'><span>##Amount##</span></span></div></td>");
                imageHtml.Append("<td><div class='topadd_f flex'><span class='one'><span>##Total##</span></span></div></td></tr>");

                foreach (PackageDetails packageDetails in lstPackageDetails)
                {
                    imageHtml.AppendLine("<tr>");
                    imageHtml.AppendLine(td.Replace("##Value##", Convert.ToString(packageDetails.ProductName)));
                    imageHtml.AppendLine(td.Replace("##Value##", Convert.ToString(packageDetails.Quantity)));
                    imageHtml.AppendLine(td.Replace("##Value##", Convert.ToString(packageDetails.Amount)));
                    imageHtml.AppendLine(td.Replace("##Value##", Convert.ToString(packageDetails.Amount * packageDetails.Quantity)));
                    imageHtml.AppendLine("</tr>");
                }
                packageSerialize.PackageDetailsHtml = imageHtml.ToString().Remove(0, 4);
                packageSerialize.PackageDetailsHtml = "<tr id='trPackageDetail'>" + packageSerialize.PackageDetailsHtml;
            }
            return packageSerialize;
        }

        [WebMethod(EnableSession = true)]
        public static string ddlCaseTypeChange(string caseId, string countryId)
        {
            string charges = string.Empty;
            CurrentSession session = (CurrentSession)HttpContext.Current.Session["UserLoginSession"];
            if (!string.IsNullOrEmpty(caseId))
            {
                CaseCharge caseCharge = new CaseChargesEntity().GetDoctorCaseChargeByCaseId(Convert.ToInt64(caseId), session.EmailId);
                if (caseCharge != null)
                {
                    charges = caseCharge.Amount.ToString("0.00");
                }
            }
            if (!string.IsNullOrEmpty(countryId))
            {
                OrthoCharge orthoCharges = new OrthoChargesEntity().GetOrthoCharges(caseId, Convert.ToInt64(countryId));
                if (orthoCharges != null)
                    charges += "," + (orthoCharges.CaseShipmentCharge > 0 ? orthoCharges.CaseShipmentCharge.ToString("0.00") : orthoCharges.ExpressShipmentCharge.ToString("0.00"));
            }
            return charges;
        }

        [WebMethod(EnableSession = true)]
        public static string DiscountValidate(string couponCode, string currentCaseCharge, string caseTypeId)
        {
            if (!string.IsNullOrEmpty(couponCode) && !string.IsNullOrEmpty(caseTypeId) && !string.IsNullOrEmpty(currentCaseCharge))
            {
                CurrentSession session = (CurrentSession)HttpContext.Current.Session["UserLoginSession"];
                decimal newCaseCharge = 0;
                CaseCharge caseCharge = new CaseChargesEntity().GetDoctorCaseChargeByCaseId(Convert.ToInt64(caseTypeId), session.EmailId);
                if (caseCharge != null && caseCharge.DiscountMaster != null)
                {
                    if (caseCharge.DiscountMaster.CouponCode == couponCode && caseCharge.DiscountMaster.ExpiryDate > DateTime.Now)
                    {
                        if (caseCharge.DiscountMaster.IsFlat)
                            newCaseCharge = caseCharge.Amount - caseCharge.DiscountMaster.Amount;
                        else
                            newCaseCharge = caseCharge.Amount - (caseCharge.Amount * caseCharge.DiscountMaster.Amount / 100);
                        return newCaseCharge.ToString("0.00");
                    }
                }
            }
            return string.Empty;
        }
        #endregion

        protected void btnAddStage_Click(object sender, EventArgs e)
        {   
            try
            {
                Stage stage = stageEntity.Create();
                stage.PatientEmail = txtEmail.Text;
                stage.StageId = Convert.ToInt64(1);
                stage.StageAmount = Convert.ToDecimal(txtStageCharges.Text);
                stage.StageDescription = txtStageDescription.Text;
                stage.isPaymentByPatient = rbtnPaymentByPatient.Checked;
                stage.CreatedBy = Authentication.GetLoggedUserID();
                stage.CreatedDate = BaseEntity.GetServerDateTime;
                stage.LastUpdatedBy = stage.CreatedBy;
                stage.LastUpdatedDate = stage.CreatedDate;
                stage.IsDelete = false;
                stage.IsActive = true;

                lstStages.Add(stage);
                rptStageList.DataSource = lstStages;
                rptStageList.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void BindStageList(string patientEmail)
        {
            List<Stage> lstStage = new StageEntity().GetAllStage(patientEmail);
            if(lstStage!= null)
            {
                rptStageList.DataSource = lstStage;
                rptStageList.DataBind();
                rptStageList.Visible = true;
            }
        }

        public string GetStageStatus(string id)
        {            
            switch (id)
            {
                case "1":
                    return StageStatus.Submitted.ToString();
                case "2":
                    return StageStatus.InProcess.ToString();
                case "3":
                    return StageStatus.Completed.ToString();
                default:
                    return string.Empty;
            }
        }
    }

    #region Serializable Class
    [Serializable]
    public class PatientSerialize
    {
        public string PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string CreatedDate { get; set; }

        public bool IsConnectUser { get; set; }
    }

    [Serializable]
    public class PackageSerialize
    {
        public string Amount { get; set; }
        public string ImageHtml { get; set; }
        public string PackageDetailsHtml { get; set; }
    }
    #endregion
}