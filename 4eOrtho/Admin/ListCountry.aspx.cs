using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;

namespace _4eOrtho.Admin
{
    public partial class ListCountry : PageBase
    {
        #region Decalration

        private int totalRecordsCount;
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListCountry));

        #endregion Decalration

        #region Events

        /// <summary>
        /// page load - viewstate value assign
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ViewState["SortBy"] = "CountryName";
                    ViewState["AscDesc"] = "ASC";
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// bind Listview Country on search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lvCountry.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// bind active and inactive image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvCountry_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            AllCountryDetail country = e.Item.DataItem as AllCountryDetail;
            Image img = e.Item.FindControl("imgStatus") as Image;

            if (Convert.ToBoolean(country.IsActive))
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
        /// set sort image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvCountry_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (lvCountry.Items.Count > 0)
                    SetSortImage();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        ///  view state value set to search parameter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsCountryList_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();

                if (ddlCountry.SelectedValue != "0")
                {
                    e.InputParameters["searchField"] = ddlCountry.SelectedValue;
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
                    lvCountry.DataBind();
                }
                else
                {
                    CountryEntity countryentity = new CountryEntity();
                    if (e.CommandName.ToUpper() == "CUSTOMDELETE")
                    {
                        int countryid = Convert.ToInt32(e.CommandArgument);
                        WSB_Country country = countryentity.GetCountryByCountryId(countryid);
                        if (countryid > 0)
                        {
                            try
                            {
                                country.IsActive = false;
                                countryentity.Save(country);
                                CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("DeleteCountry").ToString(), divMsg, lblMsg);
                            }
                            catch (UpdateException upEx)
                            {
                                logger.Error("Country Delete Error : " + upEx.Message + "<br>" + upEx.InnerException, upEx);
                                CommonHelper.ShowMessage(MessageType.Error, "You can not delete this record because it is in use.", divMsg, lblMsg);
                            }
                            catch (Exception ex)
                            {
                                logger.Error("Country Delete Error : " + ex.Message + "<br>" + ex.InnerException, ex);
                                CommonHelper.ShowMessage(MessageType.Error, "Country Delete Error" + ex.Message + "<br>" + ex.InnerException, divMsg, lblMsg);
                            }
                            finally
                            {
                                lvCountry.DataBind();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        #endregion Events

        #region Helper
        /// <summary>
        /// for sorting
        /// </summary>
        private void SetSortImage()
        {
            try
            {
                (lvCountry.FindControl("lnkCountryName") as LinkButton).Attributes.Add("class", "");

                LinkButton lnkSortedColumn = null;
                if (ViewState["SortBy"] != null)
                {
                    switch (ViewState["SortBy"].ToString().ToLower())
                    {
                        case "countryname":
                            lnkSortedColumn = lvCountry.FindControl("lnkCountryName") as LinkButton;
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
        /// Get all country list with search parameters
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="searchField"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public List<AllCountryDetail> GetCountryListBySearch(string sortField, string sortDirection, int pageSize, int startRowIndex, string searchField, string searchText)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;
                CountryEntity countryentity = new CountryEntity();

                List<AllCountryDetail> lstgetCountryDetail = countryentity.GetCountryDetail(sortField, sortDirection, pageSize, pageIndex, searchField, searchText, out totalRecords);
                totalRecordsCount = totalRecords;
                return lstgetCountryDetail;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return null;
            }
        }

        /// <summary>
        /// get country data Count
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="searchField"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public int GetCountryDataCount(string sortField, string sortDirection, string searchField, string searchText)
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

        #endregion Helper
    }
}