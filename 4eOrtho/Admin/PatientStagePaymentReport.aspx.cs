using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho.Admin
{
    public partial class PatientStagePaymentReport : System.Web.UI.Page
    {
        #region Declaration
        int totalRecordsCount;
        private ILog logger = log4net.LogManager.GetLogger(typeof(PatientStagePaymentReport));
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    ViewState["SortBy"] = "Name";
                    ViewState["AscDesc"] = "ASC";
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// client testimonail pre render
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvPatientCaseCashReport_PreRender(object sender, EventArgs e)
        {
            try
            {

                if (lvPatientCaseCashReport.Items.Count > 0)
                    SetSortImage();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }


        /// <summary>
        /// for sorting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Custom_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToUpper() == "CUSTOMSORT")
                {
                    if (ViewState["AscDesc"] == null || ViewState["AscDesc"].ToString() == "")
                        ViewState["AscDesc"] = "DESC";
                    else
                    {
                        if (ViewState["AscDesc"].ToString() == "ASC")
                            ViewState["AscDesc"] = "DESC";
                        else
                            ViewState["AscDesc"] = "ASC";
                    }
                    ViewState["SortBy"] = e.CommandArgument;
                    lvPatientCaseCashReport.DataBind();
                }
                else
                {
                    PaymentSuccessEntity paymentSuccessEntity = new PaymentSuccessEntity();
                    if (e.CommandName.ToLower() == "receive")
                    {
                        long paymentid = Convert.ToInt64(e.CommandArgument);
                        _4eOrtho.DAL.PaymentSuccess paymentsuccess = paymentSuccessEntity.GetPaymentInfoByPaymentId(paymentid);
                        if (paymentsuccess != null)
                        {
                            paymentsuccess.Status = "RECEIVED";
                            paymentSuccessEntity.Save(paymentsuccess);
                            lvPatientCaseCashReport.DataBind();
                            CommonHelper.ShowMessage(MessageType.Success, "Payment Successfully Received For Patient.", divMsg, lblMsg);
                        }

                        // clienttetimonial = clienttestimonialentity.GetClientTestimonialByID(ClientTestimonialId);
                        //if (ClientTestimonialId > 0)
                        //{
                        //    clienttestimonialentity.Delete(clienttetimonial);
                        //    lvPatientCaseCashReport.DataBind();

                        //}                        
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lvPatientCaseCashReport.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void ddlPaymentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lvPatientCaseCashReport.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void btnshowall_Click(object sender, EventArgs e)
        {
            try
            {
                txtendate.Text = txtstartdate.Text = string.Empty;
                ddlPaymentStatus.SelectedIndex = 0;
                lvPatientCaseCashReport.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// objectdatasource selecting event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsPatientCaseCashReport_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();

                if (ddlPaymentStatus.SelectedValue != "0")
                {
                    e.InputParameters["searchField"] = ddlPaymentStatus.SelectedValue;
                    e.InputParameters["searchText"] = ddlPaymentStatus.SelectedValue;
                }
                else
                {
                    e.InputParameters["searchField"] = "ALL";
                    e.InputParameters["searchText"] = string.Empty;
                }
                e.InputParameters["startdate"] = !string.IsNullOrEmpty(txtstartdate.Text) ? txtstartdate.Text : null;
                e.InputParameters["enddate"] = !string.IsNullOrEmpty(txtendate.Text) ? txtendate.Text : null;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        #endregion

        #region Helper

        /// <summary>
        /// for sorting
        /// </summary>
        private void SetSortImage()
        {
            try
            {

                (lvPatientCaseCashReport.FindControl("lnkName") as LinkButton).Attributes.Add("class", "");
                (lvPatientCaseCashReport.FindControl("lnkEmail") as LinkButton).Attributes.Add("class", "");

                LinkButton lnkSortedColumn = null;
                if (ViewState["SortBy"] != null)
                {
                    switch (ViewState["SortBy"].ToString().ToLower())
                    {

                        case "name":
                            lnkSortedColumn = lvPatientCaseCashReport.FindControl("lnkName") as LinkButton;
                            break;
                        case "email":
                            lnkSortedColumn = lvPatientCaseCashReport.FindControl("lnkEmail") as LinkButton;
                            break;
                    }
                }
                if (lnkSortedColumn != null)
                {
                    if (ViewState["AscDesc"].ToString().ToLower() == "asc")
                    {
                        lnkSortedColumn.Attributes.Add("class", "ascending");
                    }
                    else
                    {
                        lnkSortedColumn.Attributes.Add("class", "descending");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// method for bind list.
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="startRowIndex"></param>
        /// <returns></returns>
        public List<GetPatientStagePaymentReport_Result> GetPatientCaseCashReport(string sortField, string sortDirection, int pageSize, int startRowIndex, string searchField, string searchText, Nullable<DateTime> startdate, Nullable<DateTime> enddate)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;

                PaymentSuccessEntity paymentSuccessEntity = new PaymentSuccessEntity();

               

                List<GetPatientStagePaymentReport_Result> lstAllCashPaymentRecoredForPatientCase = paymentSuccessEntity.GetPatientStagePaymentReport(sortField, sortDirection, pageSize, pageIndex, searchText, startdate, enddate, out totalRecords);

              

                totalRecordsCount = totalRecords;
                return lstAllCashPaymentRecoredForPatientCase;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return null;
            }
        }
        /// <summary>
        /// total record count from stored procedure.
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public int GetPatientCaseCashReportCount(string sortField, string sortDirection, string searchField, string searchText, DateTime startdate, DateTime enddate)
        {
            try
            {
                return totalRecordsCount;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return 0;
            }
        }
        #endregion
    }
}