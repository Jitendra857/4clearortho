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
    public partial class ListCaseCharges : System.Web.UI.Page
    {
        #region Declaration
        int totalRecordCount;
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListCaseCharges));
        #endregion

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

        protected void odsCaseCharge_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();
                e.InputParameters["searchValue"] = string.Empty; // txtSearchVal.Text.Trim();
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
                    lvCaseCharges.DataBind();
                }
                else if (e.CommandName.ToUpper() == "CUSTOMDISCOUNTISACTIVE")
                {
                    DiscountMaster discount = new DiscountMasterEntity().GetDiscountMaster(Convert.ToInt64(e.CommandArgument));
                    if (discount != null)
                    {
                        discount.IsActive = !discount.IsActive;
                        new DiscountMasterEntity().Save(discount);
                        lvCaseCharges.DataBind();
                    }                    
                }
                else if (e.CommandName.ToUpper() == "CUSTOMISACTIVE")
                {
                    CaseCharge caseCharge = new CaseChargesEntity().GetCaseCharge(Convert.ToInt64(e.CommandArgument));
                    if (caseCharge != null)
                    {
                        caseCharge.IsActive = !caseCharge.IsActive;
                        new CaseChargesEntity().Save(caseCharge);
                        lvCaseCharges.DataBind();
                    }                    
                }
            }
            catch (System.Threading.ThreadAbortException) { }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void lvCaseCharges_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (lvCaseCharges.Items.Count > 0)
                {
                    SetSortImage();
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        #region Helper

        /// <summary>
        /// update image on basis of ascending/descending sorting option
        /// </summary>
        private void SetSortImage()
        {
            try
            {
                (lvCaseCharges.FindControl("lnkLookupName") as LinkButton).Attributes.Add("class", "");
                (lvCaseCharges.FindControl("lnkAmount") as LinkButton).Attributes.Add("class", "");
                (lvCaseCharges.FindControl("lnkCouponCode") as LinkButton).Attributes.Add("class", "");
                //(lvCaseCharges.FindControl("lnkIsFlat") as LinkButton).Attributes.Add("class", "");
                (lvCaseCharges.FindControl("lnkDiscountValue") as LinkButton).Attributes.Add("class", "");

                LinkButton lnkSortedColumn = null;
                if (ViewState["SortBy"] != null)
                {
                    switch (ViewState["SortBy"].ToString().ToLower())
                    {
                        case "lookupname":
                            lnkSortedColumn = lvCaseCharges.FindControl("lnkLookupName") as LinkButton;
                            break;
                        case "amount":
                            lnkSortedColumn = lvCaseCharges.FindControl("lnkAmount") as LinkButton;
                            break;
                        case "couponcode":
                            lnkSortedColumn = lvCaseCharges.FindControl("lnkCouponCode") as LinkButton;
                            break;
                        //case "isflat":
                        //    lnkSortedColumn = lvCaseCharges.FindControl("lnkPaymentAmount") as LinkButton;
                        //    break;
                        case "discountvalue":
                            lnkSortedColumn = lvCaseCharges.FindControl("lnkDiscountValue") as LinkButton;
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

        public List<GetAllCaseCharges> GetAllCaseCharges(string sortField, string sortDirection, string searchValue, int pageSize, int startRowIndex)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;
                List<GetAllCaseCharges> lstCaseCharges = new CaseChargesEntity().GetAllCaseCharges(pageIndex, pageSize, sortField, sortDirection, searchValue, out totalRecords);
                totalRecordCount = totalRecords;
                return lstCaseCharges;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return null;
            }
        }

        public int GetTotalRowCount(string sortField, string sortDirection, string searchValue)
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