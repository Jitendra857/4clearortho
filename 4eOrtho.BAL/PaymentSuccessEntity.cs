using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4eOrtho.DAL;
using System.Data.Objects;
namespace _4eOrtho.BAL
{
    public class PaymentSuccessEntity : BaseEntity
    {
        /// <summary>
        ///  Method to create instance of new Payment
        /// </summary>
        /// <returns></returns>
        public PaymentSuccess Create()
        {
            return orthoEntities.PaymentSuccesses.CreateObject();
        }

        /// <summary>
        ///  Method to Insert new Payment
        /// </summary>
        /// <param name="newPayment"></param>
        public long Save(PaymentSuccess newPayment)
        {
            if (newPayment.EntityState == System.Data.EntityState.Detached)
            {
                orthoEntities.AddToPaymentSuccesses(newPayment);
            }
            orthoEntities.SaveChanges();
            return newPayment.PaymentId;
        }

        /// <summary>
        ///  Method to Insert new Payment
        /// </summary>
        /// <param name="newPayment"></param>
        public List<PaymentSuccess> GetPaymentSuccessListByWebURL(string webURL)
        {
            return orthoEntities.PaymentSuccesses.ToList();
        }

        public PaymentSuccess GetPaymentInfo(long supplyOrderId, long caseId)
        {
            return orthoEntities.PaymentSuccesses.Where(x => x.SupplyOrderId == supplyOrderId && x.CaseId == caseId).FirstOrDefault();
        }

        public bool IsFirstCasePaymentDone(string emailId, long lookupId)
        {
            return orthoEntities.PaymentSuccesses.Where(x => x.DoctorEmailId == emailId && x.LookupId == lookupId).FirstOrDefault() != null ? true : false;
        }

        public PaymentSuccess GetPaymentInfoByPaymentId(long paymentId)
        {
            return orthoEntities.PaymentSuccesses.Where(x => x.PaymentId == paymentId).FirstOrDefault();
        }

        public PaymentSuccess GetPaymentInfoByStageId(long StageId)
        {
            return orthoEntities.PaymentSuccesses.Where(x => x.StageId == StageId).FirstOrDefault();
        }

        /// <summary>
        /// method for bind list.
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<GetAllCashPaymentRecoredForPatientCase_Result> GetAllCashPaymentRecoredForPatientCase(string sortField, string sortDirection, int pageSize, int pageIndex, string searchValue, Nullable<DateTime> startdate, Nullable<DateTime> enddate, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));
            List<GetAllCashPaymentRecoredForPatientCase_Result> lstGetAllCashPaymentRecoredForPatientCase = orthoEntities.GetAllCashPaymentRecoredForPatientCase(pageIndex, pageSize, startdate, enddate, sortField, sortDirection, searchValue, TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstGetAllCashPaymentRecoredForPatientCase;
        }

        public List<GetPatientStagePaymentReport_Result> GetPatientStagePaymentReport(string sortField, string sortDirection, int pageSize, int pageIndex, string searchValue, Nullable<DateTime> startdate, Nullable<DateTime> enddate, out int totalRecords)
        {

            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));
            List<GetPatientStagePaymentReport_Result> lstGetAllCashPaymentRecoredForPatientCase = orthoEntities.GetPatientStagePaymentReport(pageIndex, pageSize, startdate, enddate, sortField, sortDirection, searchValue, TotalRecCount).ToList();
            totalRecords = lstGetAllCashPaymentRecoredForPatientCase.Count;// Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstGetAllCashPaymentRecoredForPatientCase;

            //var Result = orthoEntities.PaymentSuccesses.Where(r=>r.CreatedDate<=startdate || r.CreatedDate>=enddate).ToList();

            //var listofdate = Result;
           
        }
    }
}
