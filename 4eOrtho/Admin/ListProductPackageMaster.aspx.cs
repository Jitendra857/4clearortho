using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;

namespace _4eOrtho.Admin
{
    public partial class ListProductPackageMaster : PageBase
    {
        #region Declaration

        private PackageMasterEntity packageMasterEntity;
        private PackageMaster packageMaster;
        private int totalRecordCount;
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListProductPackageMaster));

        #endregion Declaration

        #region Events

        /// <summary>
        /// add product master
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddProductMaster_Click(object sender, EventArgs e)
        {
            try
            {
                Session["PackageId"] = null;
                Session["PackageFiles"] = null;
                Response.Redirect("~/Admin/AddEditProductPackageMaster.aspx");
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
        }

        /// <summary>
        /// page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    ViewState["SortBy"] = "PackageName";
                    ViewState["AscDesc"] = "ASC";
                    lvPackageMaster.Items.Clear();
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
        /// set parameter for search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsPackageMaster_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();

                if (ddlProductPackageMaster.SelectedValue != "0")
                {
                    e.InputParameters["searchField"] = ddlProductPackageMaster.SelectedValue;
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
        /// list view package master sorting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvPackageMaster_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (lvPackageMaster.Items.Count > 0)
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
        /// set active icon and inactive icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvPackageMaster_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            PackageMasterDetails packageMasterDetails = e.Item.DataItem as PackageMasterDetails;
            Image img = e.Item.FindControl("imgStatus") as Image;

            if (Convert.ToBoolean(packageMasterDetails.IsActive))
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

        /// <summary>
        /// bind product package master listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lvPackageMaster.DataBind();
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
                    {
                        if (ViewState["AscDesc"].ToString() == "ASC")
                            ViewState["AscDesc"] = "DESC";
                        else
                            ViewState["AscDesc"] = "ASC";
                    }
                    ViewState["SortBy"] = e.CommandArgument;

                    lvPackageMaster.DataBind();
                }
                else if (e.CommandName.ToUpper() == "CUSTOMDELETE")
                {
                    packageMasterEntity = new PackageMasterEntity();
                    packageMaster = packageMasterEntity.GetPackageByPackageId(Convert.ToInt32(e.CommandArgument));
                    if (packageMaster != null)
                    {
                        try
                        {
                            packageMaster.IsActive = false;
                            packageMasterEntity.Save(packageMaster);
                            packageMasterEntity.Delete(packageMaster);
                            CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("DeletedSuccessfully").ToString(), divMsg, lblMsg);
                        }
                        finally
                        {
                            lvPackageMaster.DataBind();
                        }
                    }
                }
                else if (e.CommandName.ToUpper() == "CUSTOMEDIT")
                {
                    Session["PackageId"] = e.CommandArgument.ToString();
                    Response.Redirect("~/Admin/AddEditProductPackageMaster.aspx");
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

        #endregion Events

        #region Helpers

        /// <summary>
        /// update image on basis of ascending/descending sorting option
        /// </summary>
        private void SetSortImage()
        {
            try
            {
                (lvPackageMaster.FindControl("lnkSortPackageName") as LinkButton).Attributes.Add("class", "");

                LinkButton lnkSortedColumn = null;
                if (ViewState["SortBy"] != null)
                {
                    switch (ViewState["SortBy"].ToString().ToLower())
                    {
                        case "packagename":
                            lnkSortedColumn = lvPackageMaster.FindControl("lnkSortPackageName") as LinkButton;
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
                lnkSortedColumn.EnableViewState = false;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// used to fetch all countries information on basis of parameters passed
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="startRowIndex"></param>
        /// <returns></returns>
        public List<PackageMasterDetails> GetPackageMasterDetails(string sortField, string sortDirection, int pageSize, int startRowIndex, string searchField, string searchText)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;
                PackageMasterEntity packageMasterEntity = new PackageMasterEntity();
                List<PackageMasterDetails> lstPackage = packageMasterEntity.GetAllPackageMaster(sortField, sortDirection, pageSize, pageIndex, searchField, searchText, out totalRecords);
                totalRecordCount = totalRecords;
                return lstPackage;
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

        #endregion Helpers
    }
}