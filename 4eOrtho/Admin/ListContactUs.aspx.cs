using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using _4eOrtho.Utility;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;

namespace _4eOrtho.Admin
{
    public partial class ListContactUs : PageBase
    {
        #region Decltration
        int totalRecordsCount;
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListContactUs));
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ViewState["SortBy"] = "IsResponded";
                    ViewState["AscDesc"] = "ASC";
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
        protected void lvContactUs_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (lvContactUs.Items.Count > 0)
                    SetSortImage();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// set parameter value to be search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsContactUs_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();

                if (ddlContactUs.SelectedValue != "0")
                {
                    e.InputParameters["searchField"] = ddlContactUs.SelectedValue;
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
                    lvContactUs.DataBind();
                }
                //else
                //{
                //    RecommendedDentistEntity recommendeddentistentity = new RecommendedDentistEntity();
                //    if (e.CommandName.ToLower() == "delete")
                //    {
                //        int RecommendId = Convert.ToInt32(e.CommandArgument);
                //        RecommendDentist recommendeddentist = recommendeddentistentity.GetRecommendedDentistById(RecommendId);
                //        if (RecommendId > 0)
                //        {
                //            recommendeddentistentity.Delete(recommendeddentist);
                //            lvrecommendedDentist.DataBind();

                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// bind contact us details to listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lvContactUs.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        #endregion

        #region Helpers
        /// <summary>
        /// for sorting
        /// </summary>
        private void SetSortImage()
        {
            try
            {

                (lvContactUs.FindControl("lnkName") as LinkButton).Attributes.Add("class", "");
                (lvContactUs.FindControl("lnkEmail") as LinkButton).Attributes.Add("class", "");

                LinkButton lnkSortedColumn = null;
                if (ViewState["SortBy"] != null)
                {
                    switch (ViewState["SortBy"].ToString().ToLower())
                    {
                        case "name":
                            lnkSortedColumn = lvContactUs.FindControl("lnkName") as LinkButton;
                            break;
                        case "email":
                            lnkSortedColumn = lvContactUs.FindControl("lnkEmail") as LinkButton;
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
        public List<AllContactUsList> GetContactUsListBySearch(string sortField, string sortDirection, int pageSize, int startRowIndex, string searchField, string searchText)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;
                ContactUsEntity contactusentity = new ContactUsEntity();

                List<AllContactUsList> lstgetcontactuslist = contactusentity.GetallContactUsDetail(sortField, sortDirection, pageSize, pageIndex, searchField, searchText, out totalRecords);
                totalRecordsCount = totalRecords;
                return lstgetcontactuslist;
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
        public int GetContactUsDataCount(string sortField, string sortDirection, string searchField, string searchText)
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

        protected void lvContactUs_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "responded")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                if (id > 0)
                {
                    ContactU contact = new ContactUsEntity().GetContactIdById(id);
                    CheckBox isResponded = (CheckBox)e.Item.FindControl("chkIsResponded");
                    contact.IsResponded = isResponded.Checked;
                    new ContactUsEntity().Save(contact);

                    ContactUsEntity contactUsentity = new ContactUsEntity();
                    List<ContactU> contactus = contactUsentity.GetContactUsNotification();

                    UserControl ucTopControl = (UserControl)this.Page.Master.FindControl("ucTopControl");
                    if (ucTopControl != null)
                    {
                        ImageButton imgResponse = (ImageButton)ucTopControl.FindControl("imgResponse");

                        if (contactus.Count > 0)
                        {
                            imgResponse.Visible = true;
                        }
                        else
                        {
                            imgResponse.Visible = false;
                        }
                    }
                }
            }
            lvContactUs.DataBind();
        }
    }
}