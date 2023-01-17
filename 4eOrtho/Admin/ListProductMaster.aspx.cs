using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;

namespace _4eOrtho.Admin
{
    public partial class ListProductMaster : PageBase
    {
        #region Declaration
        ProductMasterEntity productMasterEntity;
        ProductMaster productMaster;
        int totalRecordCount;
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListProductMaster));
        #endregion

        #region Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    ViewState["SortBy"] = "productname";
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lvProductMaster.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// set parameter product master 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsProductMaster_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();

                if (ddlProductMaster.SelectedValue != "0")
                {
                    e.InputParameters["searchField"] = ddlProductMaster.SelectedValue;
                    e.InputParameters["searchText"] = txtSearchVal.Text.Trim();
                }
                else
                {
                    e.InputParameters["searchField"] = "0";
                    e.InputParameters["searchText"] = string.Empty;
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// listview product prerender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvProduct_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (lvProductMaster.Items.Count > 0)
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
        /// add product master 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddProductMaster_Click(object sender, EventArgs e)
        {
            Session["ProductId"] = null;
            Response.Redirect("~/Admin/AddEditProductMaster.aspx", false);
        }
        protected void lvProductMaster_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ProductMasterDetails productMasterDetails = e.Item.DataItem as ProductMasterDetails;
            Image img = e.Item.FindControl("imgStatus") as Image;

            if (Convert.ToBoolean(productMasterDetails.IsActive))
            {
                img.ImageUrl = "Images/icon-active.gif";
                img.Attributes.Add("title", this.GetLocalResourceObject("Active").ToString());
            }
            else
            {
                img.ImageUrl = "Images/icon-inactive.gif";
                img.Attributes.Add("title", this.GetLocalResourceObject("InActive").ToString());
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

                    lvProductMaster.DataBind();
                }
                else if (e.CommandName.ToUpper() == "CUSTOMDELETE")
                {
                    productMasterEntity = new ProductMasterEntity();
                    productMaster = productMasterEntity.GetProductByProductId(Convert.ToInt32(e.CommandArgument));
                    if (productMaster != null)
                    {
                        try
                        {
                            productMaster.IsActive = false;
                            productMasterEntity.Save(productMaster);
                            productMasterEntity.Delete(productMaster);
                            CommonHelper.ShowMessage(MessageType.Success,this.GetLocalResourceObject("DeletedSuccessfully").ToString(), divMsg, lblMsg);
                        }
                        catch (UpdateException upEx)
                        {
                            logger.Error(this.GetLocalResourceObject("Product Delete Error : ").ToString() + " " + upEx.Message + "<br>" + upEx.InnerException, upEx);
                            CommonHelper.ShowMessage(MessageType.Error, this.GetLocalResourceObject("CanNotDeleteRecordItIsInUse").ToString(), divMsg, lblMsg);
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Product Delete Error : " + ex.Message + "<br>" + ex.InnerException, ex);
                            CommonHelper.ShowMessage(MessageType.Error, this.GetLocalResourceObject("DeleteError").ToString() + ex.Message + "<br>" + ex.InnerException, divMsg, lblMsg);
                        }
                        finally
                        {
                            lvProductMaster.DataBind();
                        }
                    }
                }
                else if (e.CommandName.ToUpper() == "CUSTOMEDIT")
                {
                    Session["ProductId"] = e.CommandArgument.ToString();
                    Response.Redirect("~/Admin/AddEditProductMaster.aspx");
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

        /// <summary>
        /// update image on basis of ascending/descending sorting option
        /// </summary>
        private void SetSortImage()
        {
            try
            {
                (lvProductMaster.FindControl("lnkSortProduct") as LinkButton).Attributes.Add("class", "");

                LinkButton lnkSortedColumn = null;
                if (ViewState["SortBy"] != null)
                {
                    switch (ViewState["SortBy"].ToString().ToLower())
                    {
                        case "productname":
                            lnkSortedColumn = lvProductMaster.FindControl("lnkSortProduct") as LinkButton;
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
        /// used to fetch all products information on basis of parameters passed
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="startRowIndex"></param>
        /// <returns></returns>
        public List<ProductMasterDetails> GetProductMasterDetails(string sortField, string sortDirection, int pageSize, int startRowIndex, string searchField, string searchText)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;
                ProductMasterEntity productMasterEntity = new ProductMasterEntity();
                List<ProductMasterDetails> lstCountry = productMasterEntity.GetAllProductMaster(sortField, sortDirection, pageSize, pageIndex, searchField, searchText, out totalRecords);
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
        /// gives total countries record count
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public int GetTotalRowCount(string sortField, string sortDirection, string searchField, string searchText)
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