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
    public partial class ListReviewManagement : PageBase
    {
        #region Declaration
        ReviewEntity reviewEntity;
        Review review;
        int totalRecordCount;
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListPatientGallery));
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    ViewState["SortBy"] = "ReviewContent";
                    ViewState["AscDesc"] = "ASC";
                }
                this.Form.DefaultButton = this.btnSearch.UniqueID;
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
        /// input parameter set in datasource
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsReview_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                CurrentSession currentSession = new CurrentSession();
                currentSession = (CurrentSession)Session["UserLoginSession"];

                e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();
                e.InputParameters["doctorEmail"] = "";
                e.InputParameters["userType"] = UserType.A.ToString();
                if (ddlReviewManagmentType.SelectedValue != "0")
                {
                    e.InputParameters["searchField"] = ddlReviewManagmentType.SelectedValue;
                    e.InputParameters["searchText"] = txtSearchValue.Text.Trim();
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
        /// list view gallery pre render
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvReview_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (lvReview.Items.Count > 0)
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
        /// Search gallery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lvReview.DataBind();
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

                    lvReview.DataBind();
                }
                else if (e.CommandName.ToUpper() == "CUSTOMDELETE")
                {
                    reviewEntity = new ReviewEntity();
                    review = reviewEntity.GetReviewByReviewId(Convert.ToInt32(e.CommandArgument));
                    if (review != null)
                    {
                        try
                        {
                            review.IsDelete = true;
                            reviewEntity.Save(review);
                        }
                        finally
                        {
                            lvReview.DataBind();
                        }
                    }
                }
                else if (e.CommandName.ToUpper() == "CUSTOMEDIT")
                {
                    Response.Redirect("~/EditDoctorReview.aspx?reviewId=" + e.CommandArgument.ToString());
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
        #endregion

        #region Helpers
        /// <summary>
        /// update image on basis of ascending/descending sorting option
        /// </summary>
        private void SetSortImage()
        {
            try
            {
                (lvReview.FindControl("lnkReviewContent") as LinkButton).Attributes.Add("class", "");
                (lvReview.FindControl("lnkPatientName") as LinkButton).Attributes.Add("class", "");

                LinkButton lnkReviewContent = null;
                LinkButton lnkSortPatient = null;
                if (ViewState["SortBy"] != null)
                {
                    switch (ViewState["SortBy"].ToString().ToLower())
                    {
                        case "reviewcontent":
                            lnkReviewContent = lvReview.FindControl("lnkReviewContent") as LinkButton;
                            break;
                        case "patientname":
                            lnkSortPatient = lvReview.FindControl("lnkPatientName") as LinkButton;
                            break;
                    }
                }
                if (lnkSortPatient != null)
                {
                    if (ViewState["AscDesc"].ToString().ToLower() == "asc")
                    {
                        lnkSortPatient.Attributes.Add("class", "ascending");
                    }
                    else
                    {
                        lnkSortPatient.Attributes.Add("class", "descending");
                    }
                }
                if (lnkReviewContent != null)
                {
                    if (ViewState["AscDesc"].ToString().ToLower() == "asc")
                    {
                        lnkReviewContent.Attributes.Add("class", "ascending");
                    }
                    else
                    {
                        lnkReviewContent.Attributes.Add("class", "descending");
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
        public List<AllReviewDetail> GetAllReviewDetails(string sortField, string sortDirection, int pageSize, int startRowIndex, string searchField, string searchText, string doctorEmail, string userType)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;
                ReviewEntity reviewEntity = new ReviewEntity();
                List<AllReviewDetail> lstReviewDetails = reviewEntity.GetAllReviewDetails(sortField, sortDirection, pageSize, pageIndex, searchField, searchText, out totalRecords, doctorEmail, userType);
                totalRecordCount = totalRecords;
                return lstReviewDetails;
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
        public int GetTotalRowCount(string sortField, string sortDirection, string searchField, string searchText, string doctorEmail, string userType)
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
        protected void lvReview_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            AllReviewDetail reviewDetails = e.Item.DataItem as AllReviewDetail;
            Image img = e.Item.FindControl("imgStatus") as Image;

            if (Convert.ToBoolean(reviewDetails.IsActive))
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
                ddlReviewManagmentType.SelectedValue = "0";
                txtSearchValue.Text = String.Empty;
            }
            catch (Exception ex)
            {
                logger.Error("View All Records", ex);
            }
        }
        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            ViewAllRecords();
            lvReview.DataBind();
            btnShowAll.Focus();
        }
    }
}