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

namespace _4eOrtho.Admin
{
    public partial class ListDiscountedCase : PageBase
    {
        #region Declaration
        int totalRecordCount;
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListDiscountedCase));
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    ViewState["SortBy"] = "FirstName";
                    ViewState["AscDesc"] = "ASC";
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

        protected void odsNewCase_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();
                
                if (ddlFilter.SelectedValue == "0")
                {
                    e.InputParameters["searchField"] = "0";
                    e.InputParameters["searchValue"] = string.Empty;
                }                
                else if (ddlFilter.SelectedValue == "CreatedDate")
                {
                    e.InputParameters["searchField"] = ddlFilter.SelectedValue;
                    e.InputParameters["searchValue"] = txtDateSelect.Text;                    
                }
                else
                {
                    e.InputParameters["searchField"] = ddlFilter.SelectedValue;
                    e.InputParameters["searchValue"] = txtSearchVal.Text.Trim();
                }
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
            }
            catch (System.Threading.ThreadAbortException) { }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
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
        #endregion

        #region Helper

        /// <summary>
        /// update image on basis of ascending/descending sorting option
        /// </summary>
        private void SetSortImage()
        {
            try
            {
                (lvNewCase.FindControl("lnkCreatedDate") as LinkButton).Attributes.Add("class", "");
                (lvNewCase.FindControl("lnkFirstName") as LinkButton).Attributes.Add("class", "");
                (lvNewCase.FindControl("lnkLastName") as LinkButton).Attributes.Add("class", "");
                (lvNewCase.FindControl("lnkPaymentAmount") as LinkButton).Attributes.Add("class", "");
                (lvNewCase.FindControl("lnkPaymentDate") as LinkButton).Attributes.Add("class", "");
                (lvNewCase.FindControl("lnkDiscountAmt") as LinkButton).Attributes.Add("class", "");


                LinkButton lnkSortedColumn = null;
                if (ViewState["SortBy"] != null)
                {
                    switch (ViewState["SortBy"].ToString().ToLower())
                    {
                        case "createddate":
                            lnkSortedColumn = lvNewCase.FindControl("lnkCreatedDate") as LinkButton;
                            break;
                        case "firstname":
                            lnkSortedColumn = lvNewCase.FindControl("lnkFirstName") as LinkButton;
                            break;
                        case "lastname":
                            lnkSortedColumn = lvNewCase.FindControl("lnkLastName") as LinkButton;
                            break;
                        case "paymentamount":
                            lnkSortedColumn = lvNewCase.FindControl("lnkPaymentAmount") as LinkButton;
                            break;
                        case "timestamp":
                            lnkSortedColumn = lvNewCase.FindControl("lnkPaymentDate") as LinkButton;
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

        public List<GetAllCaseWithDiscounted> GetAllNewCase(string sortField, string sortDirection, string searchField, string searchValue, int pageSize, int startRowIndex)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;
                List<GetAllCaseWithDiscounted> lstAllNewCase = new PatientCaseDetailEntity().GetAllCaseWithDiscounted(pageIndex, pageSize, sortField, sortDirection, searchField, searchValue, string.Empty, out totalRecords);
                totalRecordCount = totalRecords;
                return lstAllNewCase;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return null;
            }
        }

        public int GetTotalRowCount(string sortField, string sortDirection, string searchField, string searchValue)
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
                
        #endregion
    }
}