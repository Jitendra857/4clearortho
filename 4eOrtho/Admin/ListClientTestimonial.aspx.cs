using System;
using System.Collections.Generic;
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
    public partial class ListClientTestimonial : PageBase
    {        
        #region Declaration
        int totalRecordsCount;
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListClientTestimonial));
        #endregion

        #region Events
        /// <summary>
        /// Page load method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        #endregion

        #region Helper
        /// <summary>
        /// objectdatasource selecting event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsClientTestimonialList_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();

                if (ddlClientTestimonial.SelectedValue != "0")
                {
                    e.InputParameters["searchField"] = ddlClientTestimonial.SelectedValue;
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
        /// client testimonail pre render
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvClientTestimonial_PreRender(object sender, EventArgs e)
        {
            try
            {

                if (lvClientTestimonial.Items.Count > 0)
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
                    lvClientTestimonial.DataBind();
                }
                else
                {
                    ClientTestimonialEntity clienttestimonialentity = new ClientTestimonialEntity();
                    if (e.CommandName.ToLower() == "delete")
                    {
                        
                        int ClientTestimonialId = Convert.ToInt32(e.CommandArgument);
                        ClientTestimonial clienttetimonial = clienttestimonialentity.GetClientTestimonialByID(ClientTestimonialId);
                        if (ClientTestimonialId > 0)
                        {
                            clienttestimonialentity.Delete(clienttetimonial);
                            lvClientTestimonial.DataBind();

                        }
                        CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("DeleteClientTestimonial").ToString(), divMsg, lblMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// for sorting
        /// </summary>
        private void SetSortImage()
        {
            try
            {

                (lvClientTestimonial.FindControl("lnkName") as LinkButton).Attributes.Add("class", "");
                (lvClientTestimonial.FindControl("lnkEmail") as LinkButton).Attributes.Add("class", "");

                LinkButton lnkSortedColumn = null;
                if (ViewState["SortBy"] != null)
                {
                    switch (ViewState["SortBy"].ToString().ToLower())
                    {

                        case "name":
                            lnkSortedColumn = lvClientTestimonial.FindControl("lnkName") as LinkButton;
                            break;
                        case "email":
                            lnkSortedColumn = lvClientTestimonial.FindControl("lnkEmail") as LinkButton;
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
        public List<AllClientTestimonial> GetClientTestimonialListBySearch(string sortField, string sortDirection, int pageSize, int startRowIndex, string searchField, string searchText)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;

                ClientTestimonialEntity clienttestmonialentity = new ClientTestimonialEntity();

                List<AllClientTestimonial> lstgetclienttestimonialDetail = clienttestmonialentity.GetClienttestimonialDetail(sortField, sortDirection, pageSize, pageIndex, searchField, searchText, out totalRecords);
                totalRecordsCount = totalRecords;
                return lstgetclienttestimonialDetail;
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
        public int GetClientTestimonialDataCount(string sortField, string sortDirection, string searchField, string searchText)
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
        /// item command event for active and inactive clienttestimonial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvClientTestimonial_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                ClientTestimonialEntity clienttestimonialentity = new ClientTestimonialEntity();
                if (e.CommandName.ToLower() == "active")
                {
                    int ClientTestimonialId = Convert.ToInt32(e.CommandArgument);
                    {
                        ClientTestimonial clienttetimonial = clienttestimonialentity.GetClientTestimonialByID(ClientTestimonialId);

                        if (ClientTestimonialId > 0)
                        {
                            ImageButton img = e.Item.FindControl("imginactive") as ImageButton;
                            if (clienttetimonial.IsActive == true)
                            {
                                img.ImageUrl = "Images/icon-inactive.gif";
                                clienttetimonial.IsActive = false;
                                CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("InactiveClientTestimonial").ToString(), divMsg, lblMsg);
                            }
                            else
                            {
                                img.ImageUrl = "Images/icon-active.gif";
                                clienttetimonial.IsActive = true;
                                CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("ActiveClientTestimonial").ToString(), divMsg, lblMsg);
                            }
                            clienttestimonialentity.Save(clienttetimonial);
                        }
                        lvClientTestimonial.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// bind client testimonail listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lvClientTestimonial.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// set icon active and inactive in item data bound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvClientTestimonial_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            AllClientTestimonial clientTestimonial = e.Item.DataItem as AllClientTestimonial;
            ImageButton img = e.Item.FindControl("imginactive") as ImageButton;

            if (Convert.ToBoolean(clientTestimonial.IsActive))
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

        private void ViewAllRecords()
        {
            try
            {
                ddlClientTestimonial.SelectedValue = "0";                
                txtSearchVal.Text = String.Empty;
            }
            catch (Exception ex)
            {
                logger.Error("View All Records", ex);
            }
        }
        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            ViewAllRecords();            
            lvClientTestimonial.DataBind();
            btnShowAll.Focus();
        }
    }
}