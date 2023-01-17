using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Web.UI;
using System.Collections.Generic;
using System.Threading;
using System.Web.UI.WebControls;
using System.Linq;

namespace _4eOrtho
{
    public partial class PaymentSuccess : PageBase
    {
        #region GLOBAL DECLARATION

        private ILog logger;

        decimal totalCaseCharges = 0;        
        public int totalCaseDetails = 0;

        #endregion

        #region Events

        /// <summary>
        /// Page Load Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            logger = log4net.LogManager.GetLogger(typeof(PaymentSuccess));

            CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];

            if (currentSession != null)
            {
                if (!Page.IsPostBack)
                    GetAndSetInitialValues();

                if (Session["RegistrationPaymentMessage"] != null && Convert.ToBoolean(Session["RegistrationPaymentMessage"]))
                {
                    if (!currentSession.IsCertified && currentSession.SourceType != "AAD")
                    {
                        PagesEntity pagesEntity = new PagesEntity();
                        PageDetail pageWithDetail = pagesEntity.GetPageDetailByMenuNameandLanguage("RegistrationSuccess", SessionHelper.LanguageId);
                        if (pageWithDetail != null)
                        {
                            ltrRegistrationMessage.Text = pageWithDetail.PageContent;
                            divGotoHome.Visible = divRegistrationMsg.Visible = true;
                            btnBack.Visible = false;
                        }
                    }
                    Session["RegistrationPaymentMessage"] = null;
                }
                else
                {
                    if (ViewState["AllowToShowSuccessMessage"] == null)
                        CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("lblthankResource1.Text").ToString(), divMsg, lblMsg);
                }
            }
            else
            {
                Response.Redirect("Home.aspx", false);
                return;
            }
        }

        /// <summary>
        /// Event to redirect previous page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["redirect"])))
                    Response.Redirect("PatientStageDetails.aspx");
                else
                    Response.Redirect("PatientStageDetails.aspx");
            }
            catch (ThreadAbortException) { }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region FUNCTIONS

        /// <summary>
        /// This function will get values from session.
        /// And set session values to controls
        /// </summary>
        private void GetAndSetInitialValues()
        {
            try
            {
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];

                if (SessionHelper.TransactionId != string.Empty && SessionHelper.TransactionId != "0")
                {
                    lblAmountValue.Text = "$ " + SessionHelper.PaymentAmount;
                    lblTransactionDateValue.Text = SessionHelper.TransactionTime.ToString();
                    lblTransactionIdValue.Text = SessionHelper.TransactionId;
                }
                else
                {
                    lblTransId.Visible = false;
                    lblTransactionIdValue.Visible = false;
                    lblTransactionDateValue.Visible = false;
                    lblTransDate.Visible = false;
                    lblpayamt.Visible = false;
                    lblAmountValue.Visible = false;
                }

                if (!string.IsNullOrEmpty(SessionHelper.CaseNo))
                {
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
                        lblName.Visible = false;
                        lblAddress.Visible = false;
                        lblContact.Visible = false;
                        lblLocalEmail.Visible = false;

                        PageDetail pageWithDetail = new PagesEntity().GetPageDetailByMenuNameandLanguage("4ClearOrtho-Address", SessionHelper.LanguageId);
                        if (pageWithDetail != null)
                            ltr4eDentalAddress.Text = pageWithDetail.PageContent;
                    }

                    PatientCaseDetailEntity casedetail = new PatientCaseDetailEntity();
                    if (casedetail.IsPatientCaseCashPaid(SessionHelper.CaseNo))
                    {
                        successmessage.Visible = false;
                        ViewState["AllowToShowSuccessMessage"] = "true";
                    }

                    lblNamed.Text = currentSession.DoctorName;

                    List<string> lstAddresss = new List<string>();
                    if (!string.IsNullOrEmpty(currentSession.DoctorStreet))
                        lstAddresss.Add(currentSession.DoctorStreet);
                    if (!string.IsNullOrEmpty(currentSession.DoctorCity))
                        lstAddresss.Add(currentSession.DoctorCity);
                    if (!string.IsNullOrEmpty(currentSession.DoctorState))
                        lstAddresss.Add(currentSession.DoctorState);
                    if (!string.IsNullOrEmpty(currentSession.DoctorZipcode))
                        lstAddresss.Add(currentSession.DoctorZipcode);
                    if (!string.IsNullOrEmpty(currentSession.DoctorCountry))
                        lstAddresss.Add(currentSession.DoctorCountry);

                    lblAddressd.Text = string.Join(", ", lstAddresss);
                    lblContactd.Text = "Phone : " + currentSession.DoctorMobile;
                    lblLocalEmaild.Text = currentSession.EmailId;

                    List<invoice_detail> lstInvoiceDetail = new List<invoice_detail>();
                    invoice_detail objInvoiceDetail = new invoice_detail();
                    objInvoiceDetail.description = SessionHelper.CaseType + " (Case No. : " + SessionHelper.CaseNo + ")";
                    objInvoiceDetail.qty = 1;
                    objInvoiceDetail.unit_price = objInvoiceDetail.unit_total_amount = Convert.ToDecimal(SessionHelper.CaseCharge);
                    lstInvoiceDetail.Add(objInvoiceDetail);
                    totalCaseCharges = Convert.ToDecimal(objInvoiceDetail.unit_total_amount);

                    if (SessionHelper.PackageId != 0)
                    {
                        PackageMaster packageMaster = new PackageMasterEntity().GetPackageByPackageId(SessionHelper.PackageId);
                        if (packageMaster != null)
                        {
                            SupplyOrder supplyOrder = new SupplyOrderEntity().GetSupplyOrderById(SessionHelper.SupplyOrderId);
                            objInvoiceDetail = new invoice_detail();
                            objInvoiceDetail.description = "Package Details : " + packageMaster.PackageName;
                            objInvoiceDetail.qty = supplyOrder.Quantity;
                            objInvoiceDetail.unit_price = supplyOrder.Amount;
                            objInvoiceDetail.unit_total_amount = supplyOrder.TotalAmount;
                            totalCaseCharges += Convert.ToDecimal(supplyOrder.TotalAmount);
                            lstInvoiceDetail.Add(objInvoiceDetail);

                            List<ProductPackageDetails> lstProductPackageMaster = new ProductPackageMasterEntity().GetProductPackageDetailsByPackageId(packageMaster.PackageId);
                            foreach (ProductPackageDetails item in lstProductPackageMaster)
                            {
                                objInvoiceDetail = new invoice_detail();
                                objInvoiceDetail.description = item.ProductName + " " + item.Quantity + " Qty. ";
                                //objInvoiceDetail.qty = item.Quantity;
                                //objInvoiceDetail.unit_price = item.Amount;
                                //objInvoiceDetail.unit_total_amount = item.Quantity * item.Amount;
                                //totalCaseCharges += Convert.ToDecimal(objInvoiceDetail.unit_total_amount);
                                lstInvoiceDetail.Add(objInvoiceDetail);
                            }

                            //packageDiscount = Math.Abs(supplyOrder.TotalAmount - lstProductPackageMaster.Sum(x => x.Quantity * x.Amount));
                        }
                    }

                    totalCaseDetails = lstInvoiceDetail.Count();
                    rptInvoiceDetails.DataSource = lstInvoiceDetail;
                    rptInvoiceDetails.DataBind();
                }
                else
                    divCaseDetail.Visible = false;

                totalCaseCharges += Convert.ToDecimal(SessionHelper.ShippingCharge);

                SessionHelper.TransactionId = null;
                SessionHelper.TransactionTime = null;
                SessionHelper.PaymentAmount = null;
            }
            catch (Exception exp)
            {
                logger.Error("Payment success page loading process", exp);
            }
        }

        #endregion

        protected void rptInvoiceDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                var lbl_shipping_charges = (Label)e.Item.FindControl("lbl_shipping_charges");
                if (lbl_shipping_charges != null)
                {
                    lbl_shipping_charges.Text = SessionHelper.ShippingCharge;
                }

                var lbl_total_charges = (Label)e.Item.FindControl("lbl_total_charges");
                if (lbl_total_charges != null)
                {
                    lbl_total_charges.Text = (totalCaseCharges + (Convert.ToDecimal(SessionHelper.ShippingCharge))).ToString("0.00");
                }

                var lbl_total_charges_paid = (Label)e.Item.FindControl("lbl_total_charges_paid");
                if (lbl_total_charges_paid != null)
                {
                    lbl_total_charges_paid.Text = (totalCaseCharges + (Convert.ToDecimal(SessionHelper.ShippingCharge))).ToString("0.00");
                }

                //if (SessionHelper.PackageId == 0 || packageDiscount == 0)
                //{
                //    e.Item.FindControl("trPackageDetails").Visible = false;
                //}
                //else
                //{
                //    var lblPackageDiscount = (Label)e.Item.FindControl("lblPackageDiscount");
                //    if (lblPackageDiscount != null)
                //    {
                //        lblPackageDiscount.Text = packageDiscount.ToString("0.00");
                //    }
                //}
            }
        }
    }

    public class invoice_detail
    {
        public string description { get; set; }
        public int? qty { get; set; }
        public decimal? unit_price { get; set; }
        public decimal? unit_total_amount { get; set; }

    }
}