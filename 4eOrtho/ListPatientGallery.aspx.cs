using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho
{
    public partial class ListPatientGallery : PageBase
    {
        #region Declaration
        PatientGalleryMasterEntity patientGalleryMasterEntity;
        PatientGalleryMaster patientGalleryMaster;
        int totalRecordCount;
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListPatientGallery));
        CurrentSession currentSession = null;
        #endregion

        #region Events
        /// <summary>
        /// Page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                currentSession = (CurrentSession)Session["UserLoginSession"];

                if (!Page.IsPostBack)
                {
                    if (currentSession != null)
                    {
                        PageRight pageRight = CheckRights(this.Page.GetType().BaseType.Name);
                        if (pageRight != null)
                        {
                            PageRedirect(pageRight.RedirectPageName);
                        }
                    }

                    ViewState["SortBy"] = "PatientName";
                    ViewState["AscDesc"] = "ASC";
                    Session["PatientGalleryFiles"] = null;
                    ListViewVisible();
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

        /// <summary>
        /// Set input parameter in gallery datasource
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsGallery_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                CurrentSession currentSession = new CurrentSession();
                currentSession = (CurrentSession)Session["UserLoginSession"];
                if (currentSession != null)
                {
                    e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                    e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();
                    e.InputParameters["doctorEmail"] = currentSession.EmailId;
                    e.InputParameters["listBy"] = rbtn2Image.Checked ? "2" : "8";

                    if (ddlGallery.SelectedValue != "0")
                    {
                        e.InputParameters["searchField"] = ddlGallery.SelectedValue;
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
        /// Search gallery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Commented by navik
            //lvGallery.DataBind();

            // Added By Navik
            ListViewVisible();
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
                switch (e.CommandName.ToUpper())
                {
                    case "CUSTOMSORT":
                        {
                            if (ViewState["AscDesc"] == null || ViewState["AscDesc"].ToString() == "")
                                ViewState["AscDesc"] = "DESC";
                            else
                                ViewState["AscDesc"] = (ViewState["AscDesc"].ToString() == "ASC") ? "DESC" : "ASC";

                            ViewState["SortBy"] = e.CommandArgument;
                            lvGallery.DataBind();
                            break;
                        }
                    case "CUSTOMDELETE":
                        {
                            List<string> lstIds = Convert.ToString(e.CommandArgument).Split(',').ToList();

                            if (lstIds != null && lstIds.Count > 0)
                            {
                                foreach (string id in lstIds)
                                {
                                    patientGalleryMasterEntity = new PatientGalleryMasterEntity();
                                    patientGalleryMaster = patientGalleryMasterEntity.GetPatientGalleryById(Convert.ToInt32(id));
                                    if (patientGalleryMaster != null)
                                    {
                                        patientGalleryMaster.IsActive = !patientGalleryMaster.IsActive;
                                        patientGalleryMasterEntity.Save(patientGalleryMaster);
                                    }
                                }
                            }
                            if (lvGallery.Visible)
                                lvGallery.DataBind();
                            else
                                lvBeforeGallery.DataBind();
                            break;
                        }
                    case "CUSTOMEDIT":
                        {
                            Session["PatientGalleryId"] = e.CommandArgument.ToString();
                            Response.Redirect("~/AddEditPatientGallery.aspx");
                            break;
                        }
                    case "CUSTOMPUBLICGALLERYACTIVE":
                        {
                            if ((CurrentSession)Session["UserLoginSession"] != null)
                            {
                                DoctorGalleryMapping gallery = new DoctorGalleryMapping();
                                gallery = new DoctorGalleryMappingEntity().GetDoctorGalleryById(Convert.ToInt64(e.CommandArgument), ((CurrentSession)Session["UserLoginSession"]).EmailId);

                                if (gallery != null)
                                {
                                    new DoctorGalleryMappingEntity().Delete(gallery);
                                }
                                else
                                {
                                    gallery = new DoctorGalleryMappingEntity().Create();
                                    gallery.DoctorEmailId = ((CurrentSession)Session["UserLoginSession"]).EmailId;
                                    gallery.GalleryId = Convert.ToInt64(e.CommandArgument);
                                    new DoctorGalleryMappingEntity().Save(gallery);
                                }
                                lvPublicGallery.DataBind();
                            }
                            break;
                        }
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

        /// <summary>
        /// clear session when add patient gallery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddGallery_Click(object sender, EventArgs e)
        {
            Session["PatientGalleryId"] = null;
            PageRedirect("AddEditPatientGallery.aspx");
        }

        /// <summary>
        /// set parameter values to search list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsPublicGallery_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["sortField"] = "Condition";
                e.InputParameters["sortDirection"] = "ASC";
                e.InputParameters["searchField"] = "0";
                e.InputParameters["searchText"] = string.Empty;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Public Gallery listview data bound event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvPublicGallery_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            GetGallleryDetailForDoctor galleryDetails = e.Item.DataItem as GetGallleryDetailForDoctor;
            Image img = e.Item.FindControl("imgbtnStatus") as Image;

            DoctorGalleryMapping gallery = new DoctorGalleryMapping();
            gallery = new DoctorGalleryMappingEntity().GetDoctorGalleryById(galleryDetails.GalleryId, ((CurrentSession)Session["UserLoginSession"]).EmailId);

            if (gallery != null)
            {
                img.ImageUrl = "content/Images/icon-inactive.gif";
                img.Attributes.Add("title", this.GetLocalResourceObject("InActive").ToString());
            }
            else
            {
                img.ImageUrl = "content/Images/icon-active.gif";
                img.Attributes.Add("title", this.GetLocalResourceObject("Active").ToString());
            }
        }

        /// <summary>
        /// BeforeAfter Gallery listview data bound event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvBeforeGallery_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            AllPatientGallleryDetail galleryMaster = e.Item.DataItem as AllPatientGallleryDetail;
            currentSession = (CurrentSession)Session["UserLoginSession"];
            if (galleryMaster != null)
            {
                HyperLink hlnkBefore = (HyperLink)e.Item.FindControl("hlnkBefore");
                HyperLink hlnkAfter = (HyperLink)e.Item.FindControl("hlnkAfter");
                ImageButton hypEdit = (ImageButton)e.Item.FindControl("hypEdit");
                ImageButton imgbtnStatus = e.Item.FindControl("imgbtnStatus") as ImageButton;

                hlnkBefore.Attributes.Add("onclick", "return ShowImgTemplate(" + galleryMaster.PatientGalleryId + ",'" + currentSession.DoctorName + "','" + galleryMaster.PatientName + "','Before')");
                hlnkAfter.Attributes.Add("onclick", "return ShowImgTemplate(" + (galleryMaster.AfterId) + ",'" + currentSession.DoctorName + "','" + galleryMaster.PatientName + "','After')");

                if (Convert.ToBoolean(galleryMaster.IsActive))
                {
                    imgbtnStatus.ImageUrl = "Content/images/icon-active.gif";
                    imgbtnStatus.Attributes.Add("title", this.GetLocalResourceObject("Active").ToString());
                }
                else
                {
                    imgbtnStatus.ImageUrl = "Content/images/icon-inactive.gif";
                    imgbtnStatus.Attributes.Add("title", this.GetLocalResourceObject("InActive").ToString());
                }
            }
        }

        /// <summary>
        /// Public Gallery listview data bound event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvGallery_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            AllPatientGallleryDetail galleryDetails = e.Item.DataItem as AllPatientGallleryDetail;
            ImageButton img = e.Item.FindControl("imgbtnStatus") as ImageButton;
            Image imgBefore = e.Item.FindControl("imgBefore") as Image;
            Image imgAfter = e.Item.FindControl("imgAfter") as Image;

            List<BeforeAfter> lstBeforeAfter = new List<BeforeAfter>();
            List<PatientGallery> lstPatientGallery = new PatientGalleryEntity().GetPatientGalleriesByGalleryId(galleryDetails.PatientGalleryId);

            if (lstPatientGallery != null && lstPatientGallery.Count > 0)
            {
                BeforeAfter objBeforeAfter = null;

                imgBefore.ImageUrl = "PatientFiles/thumbs/" + lstPatientGallery[0].FileName;
                imgAfter.ImageUrl = (lstPatientGallery.Count > 1) ? "PatientFiles/thumbs/" + lstPatientGallery[1].FileName : string.Empty;

                foreach (PatientGallery obj in lstPatientGallery)
                {
                    if ((lstPatientGallery.IndexOf(obj) + 1) % 2 != 0)
                    {
                        objBeforeAfter = new BeforeAfter();
                        objBeforeAfter.BeforeImagePath = "PatientFiles/slides/" + obj.FileName;
                    }
                    else
                    {
                        objBeforeAfter.AfterImagePath = "PatientFiles/slides/" + obj.FileName;
                        lstBeforeAfter.Add(objBeforeAfter);
                    }
                }
                Repeater rptSlider = e.Item.FindControl("rptSlider") as Repeater;
                if (rptSlider != null)
                {
                    rptSlider.DataSource = lstBeforeAfter;
                    rptSlider.DataBind();
                }
            }

            if (Convert.ToBoolean(galleryDetails.IsActive))
            {
                img.ImageUrl = "Content/images/icon-active.gif";
                img.Attributes.Add("title", this.GetLocalResourceObject("Active").ToString());
            }
            else
            {
                img.ImageUrl = "Content/images/icon-inactive.gif";
                img.Attributes.Add("title", this.GetLocalResourceObject("InActive").ToString());
            }
        }

        /// <summary>
        /// Radio Selection of Template change event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbtn2Image_CheckedChanged(object sender, EventArgs e)
        {
            ListViewVisible();
        }

        ///// <summary>
        ///// Set sort image of BeforeAfter Listview.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void lvBeforeGallery_PreRender(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (lvBeforeGallery.Items.Count > 0)
        //        {
        //            //SetSortImage();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("An error occured.", ex);
        //    }
        //}

        ///// <summary>
        ///// list view gallery pre render
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void lvGallery_PreRender(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (lvGallery.Items.Count > 0)
        //        {
        //            SetSortImage();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("An error occured.", ex);
        //    }
        //}

        ///// <summary>
        ///// set sort image 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void lvPublicGallery_PreRender(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (lvPublicGallery.Items.Count > 0)
        //        {
        //            SetSortImage();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("An error occured.", ex);
        //    }
        //}

        #endregion

        #region Helpers

        /// <summary>
        /// Method to get list of Patient Gallery details
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="searchField"></param>
        /// <param name="searchText"></param>
        /// <param name="doctorEmail"></param>
        /// <param name="listBy"></param>
        /// <returns></returns>
        public List<AllPatientGallleryDetail> GetPatientGalleryDetails(string sortField, string sortDirection, int pageSize, int startRowIndex, string searchField, string searchText, string doctorEmail, string listBy)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;
                PatientGalleryMasterEntity patientGalleryMasterEntity = new PatientGalleryMasterEntity();
                List<AllPatientGallleryDetail> lstPatientGallery = patientGalleryMasterEntity.GetAllPatientGalleryDetails(sortField, sortDirection, pageSize, pageIndex, searchField, searchText, doctorEmail, listBy, out totalRecords);
                totalRecordCount = totalRecords;
                return lstPatientGallery;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return null;
            }
        }

        /// <summary>
        /// Method to get total row count
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="searchField"></param>
        /// <param name="searchText"></param>
        /// <param name="doctorEmail"></param>
        /// <param name="listBy"></param>
        /// <returns></returns>
        public int GetTotalRowCount(string sortField, string sortDirection, string searchField, string searchText, string doctorEmail, string listBy)
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

        /// <summary>
        /// Method to get list of public gallery total row count
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="searchField"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public int PublicGalleryGetTotalRowCount(string sortField, string sortDirection, string searchField, string searchText)
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

        /// <summary>
        /// Method to get list of public gallery
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="searchField"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public List<GetGallleryDetailForDoctor> GetGalleryDetails(string sortField, string sortDirection, int pageSize, int startRowIndex, string searchField, string searchText)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;
                GalleryEntity galleryEntity = new GalleryEntity();
                List<GetGallleryDetailForDoctor> lstGallery = galleryEntity.GetAllGalleryDetails(sortField, sortDirection, pageSize, pageIndex, searchField, searchText, out totalRecords);
                totalRecordCount = totalRecords;
                return lstGallery;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return null;
            }
        }
        
        /// <summary>
        /// Method to visible true false of listview
        /// </summary>
        private void ListViewVisible()
        {
            if (rbtn2Image.Checked)
            {
                ViewState["listBy"] = "2";
                lvGallery.Visible = true;
                lvBeforeGallery.Visible = false;
                lvGallery.DataBind();
            }
            else
            {
                ViewState["listBy"] = "8";
                lvBeforeGallery.Visible = true;
                lvGallery.Visible = false;
                lvBeforeGallery.DataBind();
            }
        }

        ///// <summary>
        ///// Method to set sort image in listview
        ///// </summary>
        //private void SetSortImage()
        //{
        //    try
        //    {
        //        (lvGallery.FindControl("lnkSortPatient") as LinkButton).Attributes.Add("class", "");

        //        LinkButton lnkSortedColumn = null;
        //        if (ViewState["SortBy"] != null)
        //        {
        //            switch (ViewState["SortBy"].ToString().ToLower())
        //            {
        //                case "patientname":
        //                    lnkSortedColumn = lvGallery.FindControl("lnkSortPatient") as LinkButton;
        //                    break;
        //            }
        //        }
        //        if (lnkSortedColumn != null)
        //        {
        //            if (ViewState["AscDesc"].ToString().ToLower() == "asc")
        //            {
        //                lnkSortedColumn.Attributes.Add("class", "ascending");
        //            }
        //            else
        //            {
        //                lnkSortedColumn.Attributes.Add("class", "descending");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("An error occured.", ex);
        //    }
        //}
        #endregion
    }

    public class BeforeAfter
    {
        public string BeforeImagePath { get; set; }
        public string AfterImagePath { get; set; }
    }
}