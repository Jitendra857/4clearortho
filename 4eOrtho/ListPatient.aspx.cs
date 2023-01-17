using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;

namespace _4eOrtho
{
    public partial class ListPatient : PageBase
    {
        #region Declaration        
        int totalRecordCount;
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListPatient));
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if ((CurrentSession)Session["UserLoginSession"] != null)
                    {
                        PageRight pageRight = CheckRights(this.Page.GetType().BaseType.Name);
                        if (pageRight != null)
                        {
                            PageRedirect(pageRight.RedirectPageName);
                        }
                    }

                    ViewState["SortBy"] = "FirstName";
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
                if (currentSession != null)
                {
                    e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                    e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();
                    e.InputParameters["userType"] = "P";
                    e.InputParameters["DoctorEmailId"] = currentSession.EmailId;
                    if (ddlReview.SelectedValue != "0")
                    {
                        e.InputParameters["searchField"] = ddlReview.SelectedValue;
                        e.InputParameters["searchText"] = txtSearchVal.Text.Trim();
                    }
                    else
                    {
                        e.InputParameters["searchField"] = "0";
                        e.InputParameters["searchText"] = string.Empty;
                    }
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
        protected void lvPatient_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (lvPatient.Items.Count > 0)
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
            lvPatient.DataBind();
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

                    lvPatient.DataBind();
                }
                else if (e.CommandName.ToUpper() == "PATIENTEDIT")
                {
                    string[] ids = e.CommandArgument.ToString().Split('&');
                    if (ids.Count() > 1)
                    {
                        Session["PatientAndDoctorId"] = e.CommandArgument.ToString();
                        PageRedirect("~/EditPatient.aspx");
                    }
                }
                //else if (e.CommandName.ToUpper() == "CUSTOMDELETE")
                //{
                //    reviewEntity = new ReviewEntity();
                //    review = reviewEntity.GetReviewByReviewId(Convert.ToInt32(e.CommandArgument));
                //    if (review != null)
                //    {
                //        try
                //        {
                //            review.IsDelete = true;
                //            reviewEntity.Save(review);
                //        }
                //        finally
                //        {
                //            lvPatient.DataBind();
                //        }
                //    }
                //}
                //else 
                
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
                (lvPatient.FindControl("lnkSortFirstName") as LinkButton).Attributes.Add("class", "");

                LinkButton lnkSortedColumn = null;
                if (ViewState["SortBy"] != null)
                {
                    switch (ViewState["SortBy"].ToString().ToLower())
                    {
                        case "firstname":
                            lnkSortedColumn = lvPatient.FindControl("lnkSortFirstName") as LinkButton;
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
        public List<GetAllPatientOrDoctor> GetAllPatientDetails(string sortField, string sortDirection, int pageSize, int startRowIndex, string searchField, string searchText, string userType, string DoctorEmailId)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;
                PatientEntity patientEntity = new PatientEntity();
                List<GetAllPatientOrDoctor> lstpatientEntity = patientEntity.GetAllPatientOrDoctor(sortField, sortDirection, pageSize, pageIndex, searchField, searchText, out totalRecords, userType, DoctorEmailId);
                totalRecordCount = totalRecords;
                return lstpatientEntity;
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
        public int GetTotalRowCount(string sortField, string sortDirection, string searchField, string searchText, string userType, string DoctorEmailId)
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
        protected void lvPatient_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //AllReviewDetail reviewDetails = e.Item.DataItem as AllReviewDetail;
            //Image img = e.Item.FindControl("imgStatus") as Image;

            //if (Convert.ToBoolean(reviewDetails.IsActive))
            //{
            //    img.ImageUrl = "Content/images/icon-active.gif";
            //    img.Attributes.Add("title", this.GetLocalResourceObject("Active").ToString());
            //}
            //else
            //{
            //    img.ImageUrl = "Content/images/icon-inactive.gif";
            //    img.Attributes.Add("title", this.GetLocalResourceObject("InActive").ToString());
            //}
        }
        #endregion

    }
}