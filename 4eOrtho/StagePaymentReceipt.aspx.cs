using _4eOrtho.BAL;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho
{
 

    public partial class StagePaymentReceipt : System.Web.UI.Page
    {

        #region GLOBAL DECLARATION

        private ILog logger;

        decimal totalCaseCharges = 0;
        public int totalCaseDetails = 0;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            logger = log4net.LogManager.GetLogger(typeof(StagePaymentReceipt));

            CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
           
            if (currentSession != null)
            {
               
                if (!Page.IsPostBack)
                    GetAndSetInitialValues();

               
            }
            else
            {
                Response.Redirect("Home.aspx", false);
                return;
            }
        }
        public void GetAndSetInitialValues()
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);

            PaymentSuccessEntity payment = new PaymentSuccessEntity();
            StageEntity stage = new StageEntity();
            PatientEntity patient = new PatientEntity();

            var getStagePaymentDetails = payment.GetPaymentInfoByStageId(id);
            var getStageDetails = stage.GetStageById(id);
            var getPatientDetails = patient.GetPatientById(Convert.ToInt16(getStageDetails.PatientId));

            Guid g = Guid.NewGuid();
            invoice_detail objInvoiceDetail = new invoice_detail();
            string amount = "";
            if (getStageDetails.StageAmount.ToString().Contains('.'))
            {
                int index = getStageDetails.StageAmount.ToString().IndexOf('.');
                string result = getStageDetails.StageAmount.ToString().Substring(0, index);
                amount = "$" + result;
                // Console.WriteLine("result: " + result);
            }

            objInvoiceDetail.stagename = getStageDetails.StageName;
            lblstagename.Text = getStageDetails.StageName;
            lblpatientname.Text = getPatientDetails.FirstName+" "+ getPatientDetails.LastName;
            lblemail.Text = getStagePaymentDetails.PatientEmailId;
            lblpaymentdate.Text = getStagePaymentDetails.CreatedDate.ToString("dd MMMM yyyy");
            lblacknowledgement.Text = getStagePaymentDetails.AcknowledgementNumber;// Guid.NewGuid().ToString().Substring(0,10);
            lblprice.Text = amount;
            lbltotalprice.Text = amount;
            lblgranttotal.Text = amount;
            lbltransactionid.Text = getStagePaymentDetails.TransactionId;



            //objInvoiceDetail = new invoice_detail();
            //objInvoiceDetail.description = "Package Details : " + packageMaster.PackageName;
            //objInvoiceDetail.qty = supplyOrder.Quantity;
            //objInvoiceDetail.unit_price = supplyOrder.Amount;
            //objInvoiceDetail.unit_total_amount = supplyOrder.TotalAmount;
            //totalCaseCharges += Convert.ToDecimal(supplyOrder.TotalAmount);
            //lstInvoiceDetail.Add(objInvoiceDetail);
        }

        public class invoice_detail
        {
            public string stagename { get; set; }
            public string description { get; set; }
            public int? qty { get; set; }
            public decimal? unit_price { get; set; }
            public decimal? unit_total_amount { get; set; }

        }
    }
}