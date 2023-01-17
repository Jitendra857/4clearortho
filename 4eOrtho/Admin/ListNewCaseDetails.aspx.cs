using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System.Web.UI.HtmlControls;

namespace _4eOrtho.Admin
{
    public partial class ListNewCaseDetails : PageBase
    {
        #region Declaration
        int totalRecordCount;
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListNewCaseDetails));
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    BindDoctorList();
                    ViewState["SortBy"] = "PaymentDate";
                    ViewState["AscDesc"] = "DESC";
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured .", ex);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lvNewCase.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("An Error Occured:" + ex);
            }
        }

        protected void lvNewCase_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                HiddenField hdnStatus = e.Item.FindControl("hdnStatus") as HiddenField;
                Label lblTrackStatus = e.Item.FindControl("lblTrackStatus") as Label;
                //Image imgStatus = e.Item.FindControl("imgStatus") as Image;

                switch (hdnStatus.Value)
                {
                    case "1": lblTrackStatus.Text = TrackingStatus.Submitted.ToString();
                        break;
                    case "2": lblTrackStatus.Text = TrackingStatus.Acknowledged.ToString();
                        break;
                    case "3": lblTrackStatus.Text = TrackingStatus.InProcess.ToString();
                        break;
                    case "4": lblTrackStatus.Text = TrackingStatus.Shipped.ToString();
                        break;
                    case "5": lblTrackStatus.Text = "<b>" + TrackingStatus.Received.ToString() + "</b>";
                        break;
                }

                LinkButton lnkUpdateStatus = e.Item.FindControl("lnkUpdateStatus") as LinkButton;
                CaseAndTrackDetailForSA itemdata = e.Item.DataItem as CaseAndTrackDetailForSA;
                if (itemdata != null)
                {
                    if (itemdata.Status == "5")
                        lnkUpdateStatus.Text = "";
                }
                //imgStatus.ToolTip = (itemdata.IsActive) ? Convert.ToString(this.GetLocalResourceObject("Active")) : Convert.ToString(this.GetLocalResourceObject("NotActive"));

                HtmlTableCell thTotalAmount = (HtmlTableCell)lvNewCase.FindControl("thTotalAmount");
                HtmlTableCell tdTotalAmount = (HtmlTableCell)e.Item.FindControl("tdTotalAmount");
                HtmlTableCell thDiscountAmount = (HtmlTableCell)lvNewCase.FindControl("thDiscountAmount");
                HtmlTableCell tdDiscountAmount = (HtmlTableCell)e.Item.FindControl("tdDiscountAmount");
                thTotalAmount.Visible = tdTotalAmount.Visible = thDiscountAmount.Visible = tdDiscountAmount.Visible = chkDiscounted.Checked;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured .", ex);
            }
        }

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

                    lvNewCase.DataBind();
                }

                else if (e.CommandName.ToUpper() == "STAGE")
                {
                Patient patient;
                PatientCaseDetailEntity patientCaseDetailEntity = new PatientCaseDetailEntity();
                PatientCaseDetail patientCase = patientCaseDetailEntity.GetPatientCaseById(Convert.ToInt64(e.CommandArgument));

                if (patientCase != null)
                {
                    patient = new PatientEntity().GetPatientById(patientCase.PatientId);
                    Session["PatientId"] = patientCase.PatientId;
                    Session["PatientEmail"] = patientCase.SharedDoctorEmailId;
                    // SessionHelper.PatientId = patientCase.PatientId;

                }
                Response.Redirect(string.Format("PatientStageList.aspx"));

              
            }


                else if (e.CommandName.ToUpper() == "UPDATESTATUS")
                {
                    Session["TrackStatusId"] = e.CommandArgument.ToString();
                    Response.Redirect("UpdateTrackStatus.aspx");
                }
                else if (e.CommandName.ToUpper() == "SHARE")
                {
                    Session["CaseId"] = e.CommandArgument.ToString();
                    Response.Redirect("PatientCaseDetails.aspx");
                }

            }
            catch (System.Threading.ThreadAbortException) { }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void odsNewCase_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();
                //e.InputParameters["DateFrom"] = "";
                //e.InputParameters["DateTo"] = "";
                if (ddlFilter.SelectedValue == "0")
                {
                    e.InputParameters["searchField"] = "0";
                    e.InputParameters["searchValue"] = string.Empty;
                }
                else if (ddlFilter.SelectedValue == "Doctor")
                {
                    e.InputParameters["searchField"] = ddlFilter.SelectedValue;
                    e.InputParameters["searchValue"] = ddlDoctor.SelectedValue;
                }
                else if (ddlFilter.SelectedValue == "TrackStatus")
                {
                    e.InputParameters["searchField"] = ddlFilter.SelectedValue;
                    e.InputParameters["searchValue"] = ddlTrackStatus.SelectedValue;
                }
                else if (ddlFilter.SelectedValue == "CreatedDate")
                {
                    e.InputParameters["searchField"] = ddlFilter.SelectedValue;
                    e.InputParameters["searchValue"] = txtDateSelect.Text;
                    //e.InputParameters["DateFrom"] = "";
                    //e.InputParameters["DateTo"] = "";
                }
                else
                {
                    e.InputParameters["searchField"] = ddlFilter.SelectedValue;
                    e.InputParameters["searchValue"] = txtSearchVal.Text.Trim();
                }
                e.InputParameters["isCompleted"] = chkCompleted.Checked;
                e.InputParameters["isDiscounted"] = chkDiscounted.Checked;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured .", ex);
            }
        }

        protected void lvNewCase_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (lvNewCase.Items.Count > 0)
                {
                    SetSortImage();
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        #endregion

        #region Helper

        /// <summary>
        /// update image on basis of ascending/descending sorting option
        /// </summary>
        private void SetSortImage()
        {
            try
            {
                //(lvNewCase.FindControl("lnkFirstName") as LinkButton).Attributes.Add("class", "");
                //(lvNewCase.FindControl("lnkLastName") as LinkButton).Attributes.Add("class", "");
                (lvNewCase.FindControl("lnkPatientName") as LinkButton).Attributes.Add("class", "");
                //(lvNewCase.FindControl("lnkDateofBirth") as LinkButton).Attributes.Add("class", "");

                (lvNewCase.FindControl("lnkRegistrationDate") as LinkButton).Attributes.Add("class", "");
                (lvNewCase.FindControl("lnkPaymentAmount") as LinkButton).Attributes.Add("class", "");
                (lvNewCase.FindControl("lnkPaymentDate") as LinkButton).Attributes.Add("class", "");
                //(lvNewCase.FindControl("lnkPaymentStatus") as LinkButton).Attributes.Add("class", "");
                (lvNewCase.FindControl("lnkDoctorName") as LinkButton).Attributes.Add("class", "");

                (lvNewCase.FindControl("lnkTotalAmount") as LinkButton).Attributes.Add("class", "");
                (lvNewCase.FindControl("lnkDiscountAmt") as LinkButton).Attributes.Add("class", "");

                LinkButton lnkSortedColumn = null;
                if (ViewState["SortBy"] != null)
                {
                    switch (ViewState["SortBy"].ToString().ToLower())
                    {
                        //case "firstname":
                        //    lnkSortedColumn = lvNewCase.FindControl("lnkFirstName") as LinkButton;
                        //    break;
                        //case "lastname":
                        //    lnkSortedColumn = lvNewCase.FindControl("lnkLastName") as LinkButton;
                        //    break;
                        case "patientname":
                            lnkSortedColumn = lvNewCase.FindControl("lnkPatientName") as LinkButton;
                            break;
                        case "doctorname":
                            lnkSortedColumn = lvNewCase.FindControl("lnkDoctorName") as LinkButton;
                            break;
                        //case "dateofbirth":
                        //    lnkSortedColumn = lvNewCase.FindControl("lnkDateofBirth") as LinkButton;
                        //    break;

                        case "casedate":
                            lnkSortedColumn = lvNewCase.FindControl("lnkRegistrationDate") as LinkButton;
                            break;
                        case "paymentamount":
                            lnkSortedColumn = lvNewCase.FindControl("lnkPaymentAmount") as LinkButton;
                            break;
                        case "paymentdate":
                            lnkSortedColumn = lvNewCase.FindControl("lnkPaymentDate") as LinkButton;
                            break;
                        case "paymentstatus":
                            lnkSortedColumn = lvNewCase.FindControl("lnkPaymentStatus") as LinkButton;
                            break;
                        case "totalamount":
                            lnkSortedColumn = lvNewCase.FindControl("lnkTotalAmount") as LinkButton;
                            break;
                        case "discountamount":
                            lnkSortedColumn = lvNewCase.FindControl("lnkDiscountAmt") as LinkButton;
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

        public List<GetCaseAndTrackDetailForSA_Test_Result> GetAllNewCase(string sortField, string sortDirection, string searchField, string searchValue, int pageSize, int startRowIndex, bool isDiscounted, bool isCompleted)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;
                List<GetCaseAndTrackDetailForSA_Test_Result> lstAllNewCase = new PatientCaseDetailEntity().GetCaseAndTrackDetailForSA(pageIndex, pageSize, sortField, sortDirection, searchField, searchValue, isDiscounted, isCompleted, out totalRecords);
                totalRecordCount = totalRecords;
                return lstAllNewCase;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return null;
            }
        }

        public int GetTotalRowCount(string sortField, string sortDirection, string searchField, string searchValue, bool isDiscounted, bool isCompleted)
        {
            try
            {
                return totalRecordCount;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return 0;
            }
        }

        private void BindDoctorList()
        {
            try
            {
                List<AllDoctorByNewCase> lstDoctor = new PatientCaseDetailEntity().GetDoctorListByNewCase();
                if (lstDoctor != null && lstDoctor.Count > 0)
                {
                    ddlDoctor.DataSource = lstDoctor;
                    ddlDoctor.DataTextField = "FullName";
                    ddlDoctor.DataValueField = "EmailID";
                    ddlDoctor.DataBind();
                }
                ddlDoctor.Items.Insert(0, new ListItem("- Select Doctor -", "0"));
            }
            catch (Exception ex)
            {
                logger.Error("An error occured", ex);
            }
        }

        #endregion

        protected void rbtnType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvNewCase.DataBind();
        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            txtDateSelect.Text = txtSearchVal.Text = string.Empty;
            ddlFilter.SelectedIndex = ddlDoctor.SelectedIndex = ddlTrackStatus.SelectedIndex = 0;
            chkDiscounted.Checked = chkCompleted.Checked = false;
            lvNewCase.DataBind();
        }

        protected void chkCompleted_CheckedChanged(object sender, EventArgs e)
        {
            lvNewCase.DataBind();
        }        
    }
}