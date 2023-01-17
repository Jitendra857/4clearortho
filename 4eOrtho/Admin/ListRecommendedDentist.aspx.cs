using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;

namespace _4eOrtho.Admin
{
    public partial class ListRecommendedDentist : PageBase
    {
        #region Declration

        private int totalRecordsCount;
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListClientTestimonial));

        #endregion Declration

        #region Events

        /// <summary>
        /// page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
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

        /// <summary>
        /// listview prerender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvrecommendedDentist_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (lvrecommendedDentist.Items.Count > 0)
                    SetSortImage();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// recommednded dentist set parameter to search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsrecommendedDentist_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();

                if (ddlRecommendeddentist.SelectedValue != "0")
                {
                    e.InputParameters["searchField"] = ddlRecommendeddentist.SelectedValue;
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
        /// bind listview with search parameter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lvrecommendedDentist.DataBind();
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
                    lvrecommendedDentist.DataBind();
                }
                else
                {
                    RecommendedDentistEntity recommendeddentistentity = new RecommendedDentistEntity();
                    if (e.CommandName.ToLower() == "delete")
                    {
                        int RecommendId = Convert.ToInt32(e.CommandArgument);
                        RecommendDentist recommendeddentist = recommendeddentistentity.GetRecommendedDentistById(RecommendId);
                        if (RecommendId > 0)
                        {
                            recommendeddentistentity.Delete(recommendeddentist);
                            lvrecommendedDentist.DataBind();
                        }
                        CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("DeleteRecommendedDentist").ToString(), divMsg, lblMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        #endregion Events

        #region Helpers

        /// <summary>
        /// for sorting
        /// </summary>
        private void SetSortImage()
        {
            try
            {
                (lvrecommendedDentist.FindControl("lnkName") as LinkButton).Attributes.Add("class", "");
                (lvrecommendedDentist.FindControl("lnkEmail") as LinkButton).Attributes.Add("class", "");

                LinkButton lnkSortedColumn = null;
                if (ViewState["SortBy"] != null)
                {
                    switch (ViewState["SortBy"].ToString().ToLower())
                    {
                        case "name":
                            lnkSortedColumn = lvrecommendedDentist.FindControl("lnkName") as LinkButton;
                            break;

                        case "email":
                            lnkSortedColumn = lvrecommendedDentist.FindControl("lnkEmail") as LinkButton;
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
        public List<AllRecommendedDentist> GetRecommendedDentistListBySearch(string sortField, string sortDirection, int pageSize, int startRowIndex, string searchField, string searchText)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;

                RecommendedDentistEntity recommendeddentistentity = new RecommendedDentistEntity();

                List<AllRecommendedDentist> lstgetrecommendeddetntist = recommendeddentistentity.GetRecommendedDentistDetail(sortField, sortDirection, pageSize, pageIndex, searchField, searchText, out totalRecords);
                totalRecordsCount = totalRecords;
                return lstgetrecommendeddetntist;
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
        public int GetRecommendedDentistDataCount(string sortField, string sortDirection, string searchField, string searchText)
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

        #endregion Helpers
    }
}