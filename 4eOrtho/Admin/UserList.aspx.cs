using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Utility;
using _4eOrtho.Helper;

namespace _4eOrtho.Admin
{
    public partial class UserList : PageBase
    {
        #region Global Declaration
        private ILog logger;
        int totalRecordsCount;
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            logger = log4net.LogManager.GetLogger(typeof(UserList));
            try
            {
                if (!Page.IsPostBack)
                {
                    ViewState["SortBy"] = "FirstName";
                    ViewState["AscDesc"] = "ASC";
                    //BindGrid();
                }
            }
            catch (Exception ex)
            {
                logger.Error("User List page loding", ex);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            btnSearch.Attributes.Add("onclick", "return SearchValidation('" + ddlSearchField.ClientID + "','" + txtSearchVal.ClientID + "');");
        }
        protected void lvUser_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (lvUser.Items.Count > 0)
                    SetSortImage();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        //protected void lvUser_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{
        //    try
        //    {
        //        WSB_Users users = e.Item.DataItem as WSB_Users;
        //        Literal ltrStatus = e.Item.FindControl("ltrStatus") as Literal;

        //        if (ltrStatus != null)
        //        {
        //            if (ltrStatus.Text.Trim().ToLower() == "true")
        //                ltrStatus.Text = "<img src='Images/icon-active.gif' title='Active' style='border-width:0px'/>";
        //            else
        //                ltrStatus.Text = "<img src='Images/icon-inactive.gif' title='In-Active' style='border-width:0px'/>";
        //        }

        //        ImageButton imgbtnDelete = e.Item.FindControl("imgbtnDelete") as ImageButton;
        //        if (imgbtnDelete != null)
        //        {
        //            if (users.IsSuperAdmin)
        //                imgbtnDelete.Visible = false;
        //            else
        //                imgbtnDelete.OnClientClick = "return confirm('Are you sure you want to delete this record?');";
        //        }
        //        if (SessionHelper.IsSuperAdmin == false && users.ID != SessionHelper.LoggedAdminUserID)
        //        {
        //            ((HyperLink)e.Item.FindControl("hypEdit")).Visible = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Country Row data bound process", ex);
        //    }
        //}

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //BindGrid();
                lvUser.DataBind();
                if (ddlSearchField.SelectedValue == "Status")
                    ddlSearchVal.Focus();
                else
                    txtSearchVal.Focus();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            ViewAllRecords();
            //BindGrid();
            lvUser.DataBind();
            btnShowAll.Focus();
        }

        //protected void btnAddNew_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("AddUser.aspx", false);
        //}

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindGrid();
            lvUser.DataBind();
        }

        protected void lvUser_ItemCommand(object source, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() == "deleteuser")
                {
                    long deletePageId = Convert.ToInt32(e.CommandArgument);
                    if (deletePageId > 0)
                    {
                        UserEntity usersEntity = new UserEntity();
                        usersEntity.Delete(deletePageId);
                        CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("Recorddeletedsuccessfully").ToString(), divMsg, lblMsg);
                        //BindGrid();
                        lvUser.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Item command", ex);
            }
        }

        protected void odsUserList_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
            e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();
            if (ddlSearchField.SelectedValue.ToUpper() == "STATUS")
            {
                e.InputParameters["searchField"] = "Status";
                e.InputParameters["searchText"] = ddlSearchVal.SelectedValue;
            }
            else if (ddlSearchField.SelectedValue != "0")
            {
                e.InputParameters["searchField"] = ddlSearchField.SelectedValue;
                e.InputParameters["searchText"] = txtSearchVal.Text.Trim();
            }
            else
            {
                e.InputParameters["searchField"] = string.Empty;
                e.InputParameters["searchText"] = string.Empty;
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
                    lvUser.DataBind();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Custom_Command", ex);
            }
        }
        #endregion

        #region Functions

        private void ViewAllRecords()
        {
            try
            {
                ddlSearchField.SelectedValue = "0";
                ddlSearchVal.SelectedValue = "1";
                txtSearchVal.Text = String.Empty;
            }
            catch (Exception ex)
            {
                logger.Error("View All Records", ex);
            }
        }

        private void SetSortImage()
        {
            (lvUser.FindControl("lnkFirstName") as LinkButton).Attributes.Add("class", "");
            (lvUser.FindControl("lnkLastName") as LinkButton).Attributes.Add("class", "");
            (lvUser.FindControl("lbtnEmailAddress") as LinkButton).Attributes.Add("class", "");
            (lvUser.FindControl("lblRegisteredDate") as LinkButton).Attributes.Add("class", "");
            LinkButton lnkSortedColumn = null;
            if (ViewState["SortBy"] != null)
            {
                switch (ViewState["SortBy"].ToString().ToLower())
                {
                    case "firstname":
                        lnkSortedColumn = lvUser.FindControl("lnkFirstName") as LinkButton;
                        break;
                    case "lastname":
                        lnkSortedColumn = lvUser.FindControl("lnkLastName") as LinkButton;
                        break;
                    case "emailaddress":
                        lnkSortedColumn = lvUser.FindControl("lbtnEmailAddress") as LinkButton;
                        break;
                    case "registereddate":
                        lnkSortedColumn = lvUser.FindControl("lblRegisteredDate") as LinkButton;
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

        //private void BindGrid()
        //{
        //    //int pageSize = 0;
        //    try
        //    {
        //        if (ddlPageSize.SelectedValue == "DL")
        //        {
        //            pageSize = Global.PageSize;
        //        }
        //        else
        //        {
        //            pageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
        //        }
        //        string searchField = "";
        //        string searchText = "";
        //        if (ddlSearchField.SelectedValue.ToUpper() == "STATUS")
        //        {
        //            searchField = "Status";
        //            searchText = ddlSearchVal.SelectedValue;
        //        }
        //        else if (ddlSearchField.SelectedValue != "0")
        //        {
        //            searchField = ddlSearchField.SelectedValue;
        //            searchText = txtSearchVal.Text.Trim();
        //        }
        //        UsersEntity usersEntity = new UsersEntity();
        //        List<WSB_Users> users = usersEntity.GetUserListByFilter(searchField, searchText);
        //        if (users != null && users.Count > 0)
        //        {
        //            lvUser.DataSource = users;
        //            lvUser.DataBind();
        //        }
        //        else
        //        {
        //            lvUser.DataSource = users;
        //            lvUser.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Bind list view", ex);
        //    }
        //}

        /// <summary>
        /// This function will User List data with sorting order and search text
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="searchField"></param>
        /// <param name="searchText"></param>
        /// <param name="pageSize"></param>
        /// <param name="startRowIndex"></param>
        /// <returns></returns>
        public List<GetUserDetail> GetUserListBySearch(string sortField, string sortDirection, string searchField, string searchText, int pageSize, int startRowIndex)
        {
            int pageIndex = startRowIndex / pageSize;
            int totalRecords;
            UserEntity userEntity = new UserEntity();
            List<GetUserDetail> lstGetUserDetail = userEntity.GetUserDetail(sortField, sortDirection, searchField, searchText, pageSize, pageIndex, out totalRecords);
            totalRecordsCount = totalRecords;
            return lstGetUserDetail;
        }

        /// <summary>
        /// This function will get total row count of user list
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="searchField"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public int GetUserDataCount(string sortField, string sortDirection, string searchField, string searchText)
        {
            return totalRecordsCount;
        }
        #endregion
    }
}