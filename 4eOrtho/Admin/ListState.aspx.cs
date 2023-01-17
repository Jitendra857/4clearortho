using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.Utility;
using _4eOrtho.DAL;
using _4eOrtho.BAL;
using log4net;
using _4eOrtho.Helper;

namespace _4eOrtho.Admin
{
    public partial class ListState : PageBase
    {        
        #region Declaration
        int totalRecordsCount;
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListState));
        #endregion

        #region Events
        /// <summary>
        /// pageload method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    ViewState["SortBy"] = "StateName";
                    ViewState["AscDesc"] = "ASC";

                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// listview state sort image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvState_PreRender(object sender, EventArgs e)
        {
            try
            {

                if (lvState.Items.Count > 0)
                    SetSortImage();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// Search state with parameter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lvState.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// set parameter to search listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsStateList_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();
                if (ddlState.SelectedValue != "0")
                {
                    e.InputParameters["searchField"] = ddlState.SelectedValue;
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

                    lvState.DataBind();
                }
                else
                {
                    StateEntity stateenetity = new StateEntity();
                    if (e.CommandName.ToLower() == "delete")
                    {
                        int stateid = Convert.ToInt32(e.CommandArgument);
                        WSB_State state = stateenetity.GetStateByStateId(stateid);
                        if (stateid > 0)
                        {
                            state.IsActive = false;
                            stateenetity.Save(state);
                            //stateenetity.Delete(state);
                            lvState.DataBind();
                        }
                        CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("DeleteState").ToString(), divMsg, lblMsg);
                    }
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
        /// on item data bound bind active and inactive icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvState_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            AllStateDetail stateDetail = e.Item.DataItem as AllStateDetail;
            Image img = e.Item.FindControl("imgStatus") as Image;

            if (Convert.ToBoolean(stateDetail.IsActive))
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
     
        /// <summary>
        /// for sorting
        /// </summary>
        private void SetSortImage()
        {
            try
            {
               
                (lvState.FindControl("lnkStateName") as LinkButton).Attributes.Add("class", "");
                (lvState.FindControl("lnkCountryName") as LinkButton).Attributes.Add("class", "");



                LinkButton lnkSortedColumn = null;
                if (ViewState["SortBy"] != null)
                {
                    switch (ViewState["SortBy"].ToString().ToLower())
                    {
                        
                        case "statename":
                            lnkSortedColumn = lvState.FindControl("lnkStateName") as LinkButton;
                            break;
                        case "countryname":
                            lnkSortedColumn = lvState.FindControl("lnkCountryName") as LinkButton;
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
        /// Get State list search by search parameter
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="searchField"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public List<AllStateDetail> GetStateListBySearch(string sortField, string sortDirection, int pageSize, int startRowIndex, string searchField, string searchText)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;
                StateEntity statentity = new StateEntity();

                List<AllStateDetail> lstgetAgentDetail = statentity.GetStateDetail(sortField, sortDirection, pageSize, pageIndex, searchField, searchText, out totalRecords);
                totalRecordsCount = totalRecords;
                return lstgetAgentDetail;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return null;
            }
        }

        /// <summary>
        /// get state data count
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="searchField"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public int GetStateDataCount(string sortField, string sortDirection, string searchField, string searchText)
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


  
        #endregion
    }
}