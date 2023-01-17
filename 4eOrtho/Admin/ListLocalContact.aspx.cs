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
using System.Transactions;

namespace _4eOrtho.Admin
{
    public partial class ListLocalContact : PageBase
    {
        #region Declaration
        int totalRecordsCount;
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListLocalContact));
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

        protected void lvLocalContact_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (lvLocalContact.Items.Count > 0)
                    SetSortImage();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
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

                    lvLocalContact.DataBind();
                }
                else
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        LocalContact localContact = new LocalContactEntity().GetLocalContactById(Convert.ToInt64(e.CommandArgument));

                        if (localContact != null)
                        {
                            User user = new UserEntity().GetUserByUserId(localContact.UserId);

                            if (e.CommandName.ToUpper() == "STATUS")
                            {
                                localContact.IsActive = !localContact.IsActive;
                            }
                            else
                            {
                                localContact.IsActive = false;
                                localContact.IsDelete = true;
                                CommonHelper.ShowMessage(MessageType.Success, "DeleteState".ToString(), divMsg, lblMsg);
                            }

                            user.IsActive = localContact.IsActive;
                            new UserEntity().Save(user);

                            localContact.LastUpdatedBy = Authentication.GetLoggedUserID();
                            new LocalContactEntity().Save(localContact);
                            lvLocalContact.DataBind();
                        }
                        scope.Complete();
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

        protected void odsLocalContactList_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
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
        public List<GetAllLocalContacts> GetLocalContactList(string sortField, string sortDirection, int pageSize, int startRowIndex, string searchText)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;
                List<GetAllLocalContacts> lstGetAllLocalContacts = new LocalContactEntity().GetAllLocalContacts(sortField, sortDirection, pageSize, pageIndex, searchText, out totalRecords);
                totalRecordsCount = totalRecords;
                return lstGetAllLocalContacts;
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
        public int GetLocalContactDataCount(string sortField, string sortDirection, string searchText)
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
                (lvLocalContact.FindControl("lnkOrganisationName") as LinkButton).Attributes.Add("class", "");
                (lvLocalContact.FindControl("lnkFirstName") as LinkButton).Attributes.Add("class", "");
                (lvLocalContact.FindControl("lnkLastName") as LinkButton).Attributes.Add("class", "");
                (lvLocalContact.FindControl("lnkEmailID") as LinkButton).Attributes.Add("class", "");
                (lvLocalContact.FindControl("lnkCountryName") as LinkButton).Attributes.Add("class", "");

                LinkButton lnkSortedColumn = null;
                if (ViewState["SortBy"] != null)
                {
                    switch (ViewState["SortBy"].ToString().ToLower())
                    {
                        case "organizationname":
                            lnkSortedColumn = lvLocalContact.FindControl("lnkOrganisationName") as LinkButton;
                            break;
                        case "firstname":
                            lnkSortedColumn = lvLocalContact.FindControl("lnkFirstName") as LinkButton;
                            break;
                        case "lastname":
                            lnkSortedColumn = lvLocalContact.FindControl("lnkLastName") as LinkButton;
                            break;
                        case "emailid":
                            lnkSortedColumn = lvLocalContact.FindControl("lnkEmailID") as LinkButton;
                            break;
                        case "countryname":
                            lnkSortedColumn = lvLocalContact.FindControl("lnkCountryName") as LinkButton;
                            break;
                    }
                }
                if (lnkSortedColumn != null)
                {
                    if (ViewState["AscDesc"].ToString().ToLower() == "asc")
                        lnkSortedColumn.Attributes.Add("class", "ascending");
                    else
                        lnkSortedColumn.Attributes.Add("class", "descending");
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lvLocalContact.DataBind();
        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            txtSearchVal.Text = string.Empty;
            lvLocalContact.DataBind();
        }
    }
}