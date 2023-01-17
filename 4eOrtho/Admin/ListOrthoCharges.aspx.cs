using _4eOrtho.DAL;
using _4eOrtho.BAL;
using _4eOrtho.Helper;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.Utility;

namespace _4eOrtho.Admin
{
    public partial class ListOrthoCharges : PageBase
    {
        #region Declaration
        int totalRecordsCount;
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListOrthoCharges));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    ViewState["SortBy"] = "CreatedDate";
                    ViewState["AscDesc"] = "DESC";
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void odsOrthoCaseChargesList_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();
                e.InputParameters["searchText"] = txtSearchVal.Text.Trim();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void lvOrthoCaseCharges_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (lvOrthoCaseCharges.Items.Count > 0)
                    SetSortImage();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Get State list search by search parameter
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="searchField"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public List<GetAllOrthoCharges> GetOrthoCaseChargesList(string sortField, string sortDirection, int pageSize, int startRowIndex, string searchText)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;
                List<GetAllOrthoCharges> lstGetAllOrthoCharges = new OrthoChargesEntity().GetAllOrthoCharge(sortField, sortDirection, pageSize, pageIndex, searchText, out totalRecords);
                totalRecordsCount = totalRecords;
                return lstGetAllOrthoCharges;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return null;
            }
        }

        /// <summary>
        /// Get ortho case charges data count.
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>        
        /// <param name="searchText"></param>
        /// <returns></returns>
        public int GetOrthoCaseChargesDataCount(string sortField, string sortDirection, string searchText)
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

        /// <summary>

        /// <summary>
        /// for sorting
        /// </summary>
        private void SetSortImage()
        {
            try
            {
                (lvOrthoCaseCharges.FindControl("lnkCountryName") as LinkButton).Attributes.Add("class", "");
                (lvOrthoCaseCharges.FindControl("lnkShipmentAmount") as LinkButton).Attributes.Add("class", "");
                (lvOrthoCaseCharges.FindControl("lnkCaseShipmentCharge") as LinkButton).Attributes.Add("class", "");
                //(lvOrthoCaseCharges.FindControl("lnkBracesCaseAmount") as LinkButton).Attributes.Add("class", "");

                LinkButton lnkSortedColumn = null;
                if (ViewState["SortBy"] != null)
                {
                    switch (ViewState["SortBy"].ToString().ToLower())
                    {
                        case "countryname":
                            lnkSortedColumn = lvOrthoCaseCharges.FindControl("lnkCountryName") as LinkButton;
                            break;
                        case "shipmentamount":
                            lnkSortedColumn = lvOrthoCaseCharges.FindControl("lnkShipmentAmount") as LinkButton;
                            break;
                        case "caseshipmentcharge":
                            lnkSortedColumn = lvOrthoCaseCharges.FindControl("lnkCaseShipmentCharge") as LinkButton;
                            break;
                        //case "bracescaseamount":
                        //    lnkSortedColumn = lvOrthoCaseCharges.FindControl("lnkBracesCaseAmount") as LinkButton;
                        //    break;
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lvOrthoCaseCharges.DataBind();
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

                    lvOrthoCaseCharges.DataBind();
                }
                else
                {
                    OrthoCharge orthoCharges = new OrthoChargesEntity().GetOrthoChargeById(Convert.ToInt64(e.CommandArgument));
                    if (orthoCharges != null)
                    {
                        if (e.CommandName.ToUpper() == "STATUS")
                            orthoCharges.IsActive = !orthoCharges.IsActive;
                        else
                        {
                            orthoCharges.IsActive = false;
                            orthoCharges.IsDelete = true;
                            CommonHelper.ShowMessage(MessageType.Success, "DeleteState".ToString(), divMsg, lblMsg);
                        }

                        orthoCharges.LastUpdatedBy = Authentication.GetLoggedUserID();
                        new OrthoChargesEntity().Save(orthoCharges);
                        lvOrthoCaseCharges.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                divMsg.Attributes.Add("class", "errormsgbox");
                lblMsg.Text = string.Empty;
            }
        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            txtSearchVal.Text = string.Empty;
            lvOrthoCaseCharges.DataBind();
        }
    }
}