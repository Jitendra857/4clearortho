using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class ListSupplyOrder : PageBase
    {
        #region Declaration
        SupplyOrderEntity supplyOrderEntity;
        SupplyOrder supplyOrder;
        int totalRecordCount;
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListSupplyOrder));
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    ViewState["SortBy"] = "SupplyName";
                    ViewState["AscDesc"] = "ASC";
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void odsOrderSupply_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {

                e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();
                if (ddlSupplyOrder.SelectedValue != "0")
                {
                    if (ddlSupplyOrder.SelectedValue == "Is Dispatched?" || ddlSupplyOrder.SelectedValue == "Is Received?")
                    {
                        e.InputParameters["searchField"] = ddlSupplyOrder.SelectedValue;
                        e.InputParameters["searchValue"] = rblYesNo.SelectedValue;
                        e.InputParameters["doctorEmail"] = "";
                    }
                    else
                    {
                        e.InputParameters["searchField"] = ddlSupplyOrder.SelectedValue;
                        e.InputParameters["searchValue"] = txtSearchValue.Text.Trim();
                        e.InputParameters["doctorEmail"] = "";
                    }
                }
                else
                {
                    e.InputParameters["searchField"] = "0";
                    e.InputParameters["searchValue"] = string.Empty;
                    e.InputParameters["doctorEmail"] = "";

                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }

        }

        protected void lvSupplyOrder_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (lvSupplyOrder.Items.Count > 0)
                {
                    SetSortImage();
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
                lvSupplyOrder.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        protected void lvSupplyOrder_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            AllSupplyOrderAdmin allSupplyOrderAdmin = e.Item.DataItem as AllSupplyOrderAdmin;
            Image img = e.Item.FindControl("imgStatus") as Image;
            HyperLink hypEdit = e.Item.FindControl("hypEdit") as HyperLink;
            if (Convert.ToBoolean(allSupplyOrderAdmin.IsActive))
            {
                img.ImageUrl = "Images/icon-active.gif";
                img.Attributes.Add("title", this.GetLocalResourceObject("Active").ToString());
            }
            else
            {
                img.ImageUrl = "Images/icon-inactive.gif";
                img.Attributes.Add("title", this.GetLocalResourceObject("InActive").ToString());
                hypEdit.ImageUrl = "Images/icon-inactive.gif";
                hypEdit.NavigateUrl = "";
                hypEdit.ToolTip = this.GetLocalResourceObject("InActive").ToString();
            }
        }
        #endregion

        #region Helpers

        /// <summary>
        /// for sorting and deletion
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

                    lvSupplyOrder.DataBind();
                }
                else if (e.CommandName.ToUpper() == "CUSTOMDELETE")
                {
                    supplyOrderEntity = new SupplyOrderEntity();
                    supplyOrder = supplyOrderEntity.GetSupplyOrderById(Convert.ToInt64(e.CommandArgument));
                    if (supplyOrder != null)
                    {
                        try
                        {
                            supplyOrder.IsActive = false;
                            supplyOrderEntity.Save(supplyOrder);
                            //supplyOrderEntity.Delete(supplyOrder);
                            CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("DeletedSuccessfully").ToString(), divMsg, lblMsg);
                        }
                        catch (UpdateException upEx)
                        {
                            logger.Error(this.GetLocalResourceObject("Order Delete Error : ").ToString() + " " + upEx.Message + "<br>" + upEx.InnerException, upEx);
                            CommonHelper.ShowMessage(MessageType.Error, this.GetLocalResourceObject("CanNotDeleteRecordItIsInUse").ToString(), divMsg, lblMsg);
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Product Delete Error : " + ex.Message + "<br>" + ex.InnerException, ex);
                            CommonHelper.ShowMessage(MessageType.Error, this.GetLocalResourceObject("DeleteError").ToString() + ex.Message + "<br>" + ex.InnerException, divMsg, lblMsg);
                        }
                        finally
                        {
                            lvSupplyOrder.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// update image on basis of ascending/descending sorting option
        /// </summary>
        private void SetSortImage()
        {
            try
            {
                (lvSupplyOrder.FindControl("lnkSortOrderSupply") as LinkButton).Attributes.Add("class", "");

                LinkButton lnkSortedColumn = null;
                if (ViewState["SortBy"] != null)
                {
                    switch (ViewState["SortBy"].ToString().ToLower())
                    {
                        case "supplyname":
                            lnkSortedColumn = lvSupplyOrder.FindControl("lnkSortOrderSupply") as LinkButton;
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
        /// used to fetch all orders information on basis of parameters passed
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="startRowIndex"></param>
        /// <returns></returns>
        public List<AllSupplyOrderAdmin> GetSupplyOrderDetails(string sortField, string sortDirection, int pageSize, int startRowIndex, string searchField, string searchValue, string doctorEmail)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;
                SupplyOrderEntity supplyOrderEntity = new SupplyOrderEntity();
                List<AllSupplyOrderAdmin> lstCountry = supplyOrderEntity.GetAllSupplyOrderAdmin(sortField, sortDirection, pageSize, pageIndex, searchField, searchValue, doctorEmail, out totalRecords);
                totalRecordCount = totalRecords;
                return lstCountry;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return null;
            }
        }

        /// <summary>
        /// gives total orders record count
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public int GetTotalRowCount(string sortField, string sortDirection, string searchField, string searchValue, string doctorEmail)
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