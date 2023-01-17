using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;

namespace _4eOrtho.Admin
{
    public partial class ListGallery : PageBase
    {
        #region Declaration

        private GalleryEntity galleryEntity;
        private Gallery gallery;
        private int totalRecordCount;
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListGallery));

        #endregion Declaration

        #region Events

        /// <summary>
        /// set view state values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    ViewState["SortBy"] = "Condition";
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
        /// set parameter values to search list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsGallery_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();
                if (ddlGallery.SelectedValue != "0")
                {
                    e.InputParameters["searchField"] = ddlGallery.SelectedValue;
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
        /// set sort image 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvGallery_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (lvGallery.Items.Count > 0)
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
        /// bind gallery listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lvGallery.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Add page redirection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddGallery_Click(object sender, EventArgs e)
        {
            Session["GalleryId"] = null;
            Response.Redirect("~/Admin/AddEditGallery.aspx", false);
        }

        /// <summary>
        /// set active and inactive icond on item data bound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvGallery_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            AllGallleryDetail galleryDetails = e.Item.DataItem as AllGallleryDetail;
            Image img = e.Item.FindControl("imgStatus") as Image;

            if (Convert.ToBoolean(galleryDetails.IsActive))
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

        #endregion Events

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

                    lvGallery.DataBind();
                }
                else if (e.CommandName.ToUpper() == "CUSTOMDELETE")
                {
                    galleryEntity = new GalleryEntity();
                    gallery = galleryEntity.GetGalleryById(Convert.ToInt32(e.CommandArgument));
                    if (gallery != null)
                    {
                        try
                        {
                            gallery.IsActive = false;
                            galleryEntity.Save(gallery);
                            //galleryEntity.Delete(gallery);
                            CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("DeletedSuccessfully").ToString(), divMsg, lblMsg);
                        }
                        finally
                        {
                            lvGallery.DataBind();
                        }
                    }
                }
                else if (e.CommandName.ToUpper() == "CUSTOMEDIT")
                {
                    Session["GalleryId"] = e.CommandArgument.ToString();
                    Response.Redirect("~/Admin/AddEditGallery.aspx");
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
                (lvGallery.FindControl("lnkSortPatient") as LinkButton).Attributes.Add("class", "");

                LinkButton lnkSortedColumn = null;
                if (ViewState["SortBy"] != null)
                {
                    switch (ViewState["SortBy"].ToString().ToLower())
                    {
                        case "condition":
                            lnkSortedColumn = lvGallery.FindControl("lnkSortPatient") as LinkButton;
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
        /// used to fetch all gallery information on basis of parameters passed
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="startRowIndex"></param>
        /// <returns></returns>
        public List<AllGallleryDetail> GetGalleryDetails(string sortField, string sortDirection, int pageSize, int startRowIndex, string searchField, string searchText)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;
                GalleryEntity galleryEntity = new GalleryEntity();
                List<AllGallleryDetail> lstGallery = galleryEntity.GetAllGalleryDetails(sortField, sortDirection, pageSize, pageIndex, searchField, searchText, "Admin", out totalRecords);
                totalRecordCount = totalRecords;
                return lstGallery;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return null;
            }
        }

        /// <summary>
        /// gives total gallery record count
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