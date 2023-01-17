using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace _4eOrtho
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

        /// <summary>
        /// page load events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {               
                if (!Page.IsPostBack)
                {
                    if ((CurrentSession)Session["UserLoginSession"] != null)
                    {
                        PageRight pageRight = CheckRights(this.Page.GetType().BaseType.Name);
                        if (pageRight != null)
                        {
                            PageRedirect(pageRight.RedirectPageName);
                        }
                    }

                    ViewState["SortBy"] = "SupplyName";
                    ViewState["AscDesc"] = "ASC";
                }
                this.Form.DefaultButton = this.btnSearch.UniqueID;
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// datasource order supply selecting parameters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsOrderSupply_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                if (currentSession != null)
                {
                    e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                    e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();
                    if (ddlSupplyOrder.SelectedValue != "0")
                    {
                        e.InputParameters["searchField"] = ddlSupplyOrder.SelectedValue;
                        e.InputParameters["searchValue"] = txtSearchValue.Text.Trim();
                        e.InputParameters["doctorEmail"] = currentSession.EmailId;
                    }
                    else
                    {
                        e.InputParameters["searchField"] = "0";
                        e.InputParameters["searchValue"] = string.Empty;
                        e.InputParameters["doctorEmail"] = currentSession.EmailId;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// list view supply order pre render
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// listview item data bound for Dispatch and Received div dispay accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvSupplyOrder_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    ListViewDataItem listViewDataItem = (ListViewDataItem)e.Item;
                    bool IsDispatch = (bool)DataBinder.Eval(listViewDataItem.DataItem, "IsDispatch");
                    bool IsRecieved = (bool)DataBinder.Eval(listViewDataItem.DataItem, "IsRecieved");
                    HtmlGenericControl dvEdit = new HtmlGenericControl();
                    dvEdit = (HtmlGenericControl)e.Item.FindControl("dvEdit");

                    HtmlGenericControl dvDelete = new HtmlGenericControl();
                    dvDelete = (HtmlGenericControl)e.Item.FindControl("dvDelete");

                    HtmlGenericControl dvEditDispatch = new HtmlGenericControl();
                    dvEditDispatch = (HtmlGenericControl)e.Item.FindControl("dvEditDispatch");

                    HtmlGenericControl dvDeleteDispatch = new HtmlGenericControl();
                    dvDeleteDispatch = (HtmlGenericControl)e.Item.FindControl("dvDeleteDispatch");

                    HtmlGenericControl dvDeleteRecieve = new HtmlGenericControl();
                    dvDeleteRecieve = (HtmlGenericControl)e.Item.FindControl("dvDeleteRecieve");

                    HtmlGenericControl dvEditRecieve = new HtmlGenericControl();
                    dvEditRecieve = (HtmlGenericControl)e.Item.FindControl("dvEditRecieve");

                    HyperLink imgEditDispatch = new HyperLink();
                    imgEditDispatch = (HyperLink)e.Item.FindControl("imgEditDispatch");

                    Image imgEditReceived = new Image();
                    imgEditReceived = (Image)e.Item.FindControl("imgEditReceived");

                    HyperLink imgDeleteDispatch = new HyperLink();
                    imgDeleteDispatch = (HyperLink)e.Item.FindControl("imgDeleteDispatch");

                    Image imgDeleteReceived = new Image();
                    imgDeleteReceived = (Image)e.Item.FindControl("imgDeleteReceived");

                    imgEditDispatch.ToolTip = this.GetLocalResourceObject("Dispatched").ToString();
                    imgEditReceived.ToolTip = this.GetLocalResourceObject("Received").ToString();
                    imgDeleteDispatch.ToolTip = this.GetLocalResourceObject("Dispatched").ToString();
                    imgDeleteReceived.ToolTip = this.GetLocalResourceObject("Received").ToString();

                    if (IsDispatch)
                    {
                        dvEdit.Visible = false;
                        dvDelete.Visible = false;
                        dvEditDispatch.Visible = true;
                        dvDeleteDispatch.Visible = true;
                        dvDeleteRecieve.Visible = false;
                        dvEditRecieve.Visible = false;


                    }
                    if (IsRecieved)
                    {
                        dvEdit.Visible = false;
                        dvDelete.Visible = false;
                        dvEditDispatch.Visible = false;
                        dvDeleteDispatch.Visible = false;
                        dvDeleteRecieve.Visible = true;
                        dvEditRecieve.Visible = true;
                    }
                    if (!IsDispatch && !IsRecieved)
                    {
                        dvEdit.Visible = true;
                        dvDelete.Visible = true;
                        dvEditDispatch.Visible = false;
                        dvDeleteDispatch.Visible = false;
                        dvDeleteRecieve.Visible = false;
                        dvEditRecieve.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }

        }

        /// <summary>
        /// Search Order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        ViewState["AscDesc"] = (ViewState["AscDesc"].ToString() == "ASC") ? "DESC" : "ASC";

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
                            CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("DeletedSccessfully").ToString(), divMsg, lblMsg);
                        }
                        catch (Exception ex)
                        {
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

        #endregion

        #region Helpers

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
        public List<SupplyOrderDetails> GetSupplyOrderDetails(string sortField, string sortDirection, int pageSize, int startRowIndex, string searchField, string searchValue, string doctorEmail)
        {
            try
            {

                int pageIndex = startRowIndex / pageSize;
                int totalRecords;
                SupplyOrderEntity supplyOrderEntity = new SupplyOrderEntity();
                List<SupplyOrderDetails> lstSupplyOrder = supplyOrderEntity.GetAllSupplyOrder(sortField, sortDirection, pageSize, pageIndex, searchField, searchValue, doctorEmail, out totalRecords);
                totalRecordCount = totalRecords;
                return lstSupplyOrder;
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