using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using log4net;

namespace _4eOrtho.Admin
{
    public partial class ListBecomeProvider : PageBase
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
                    ViewState["SortBy"] = "DoctorName";
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
        protected void odsBecomeProvider_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();
                e.InputParameters["searchField"] = ddlSearchType.SelectedValue.ToString();
                e.InputParameters["searchValue"] = txtSearchVal.Text.Trim();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void lvBecomeProvider_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (lvBecomeProvider.Items.Count > 0)
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
                    lvBecomeProvider.DataBind();
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
                (lvBecomeProvider.FindControl("lnkDocotorName") as LinkButton).Attributes.Add("class", "");

                LinkButton lnkSortedColumn = null;
                if (ViewState["SortBy"] != null)
                {
                    switch (ViewState["SortBy"].ToString().ToLower())
                    {
                        case "doctorname":
                            lnkSortedColumn = lvBecomeProvider.FindControl("lnkDocotorName") as LinkButton;
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
        public List<BecomeProviderDetails> GetBecomeProverDetails(string sortField, string sortDirection, int pageSize, int startRowIndex, string searchField, string searchValue)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;
                BecomeProviderEntity becomeProviderEntity = new BecomeProviderEntity();
                List<BecomeProviderDetails> lstBecomeProvider = becomeProviderEntity.GetBecomeProviderList(sortField, sortDirection, pageSize, pageIndex,searchField,searchValue, out totalRecords);
                totalRecordsCount = totalRecords;
                return lstBecomeProvider;
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
        public int GetBecomeProverDetailsDataCount(string sortField, string sortDirection, string searchField, string searchValue)
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lvBecomeProvider.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
    }
}