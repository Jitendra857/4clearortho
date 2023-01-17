using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho
{
    public partial class ListPictureTemplate : PageBase
    {
        #region Declaration
        PatientGalleryMaster patientGalleryMaster;
        int totalRecordCount;
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListPatientGallery));
        #endregion

        #region Events

        /// <summary>
        /// Page Load Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                    ViewState["8SortBy"] = ViewState["2SortBy"] = "CreatedDate";
                    ViewState["2AscDesc"] = ViewState["8AscDesc"] = "DESC";
                    ViewState["SortBy"] = "Condition";
                    ViewState["AscDesc"] = "ASC";
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
        /// Event to search in listview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindListviews();
        }

        /// <summary>
        /// Event to custom comman on listview action.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Custom_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToUpper() == "CUSTOMSORT")
                {
                    if (lvBeforeGallery.Visible)
                    {
                        if (ViewState["8AscDesc"] == null || ViewState["8AscDesc"].ToString() == "")
                            ViewState["8AscDesc"] = "DESC";
                        else
                            ViewState["8AscDesc"] = (ViewState["8AscDesc"].ToString() == "ASC") ? "DESC" : "ASC";

                        ViewState["8SortBy"] = e.CommandArgument;
                    }
                    else if (lvTwoImageGallery.Visible)
                    {
                        if (ViewState["2AscDesc"] == null || ViewState["2AscDesc"].ToString() == "")
                            ViewState["2AscDesc"] = "DESC";
                        else
                            ViewState["2AscDesc"] = (ViewState["2AscDesc"].ToString() == "ASC") ? "DESC" : "ASC";

                        ViewState["2SortBy"] = e.CommandArgument;
                    }
                    else
                    {
                        if (ViewState["AscDesc"] == null || ViewState["AscDesc"].ToString() == "")
                            ViewState["AscDesc"] = "DESC";
                        else
                            ViewState["AscDesc"] = (ViewState["AscDesc"].ToString() == "ASC") ? "DESC" : "ASC";

                        ViewState["SortBy"] = e.CommandArgument;
                    }
                }
                else if (e.CommandName.ToUpper() == "CUSTOMDELETE")
                {
                    long beforeGalleryId = Convert.ToInt64(e.CommandArgument.ToString().Split(',')[0]);
                    long afterGalleryId = Convert.ToInt64(e.CommandArgument.ToString().Split(',')[1]);

                    patientGalleryMaster = new PatientGalleryMasterEntity().GetPatientGalleryById(beforeGalleryId);
                    if (patientGalleryMaster != null)
                    {
                        patientGalleryMaster.IsActive = !patientGalleryMaster.IsActive;
                        new PatientGalleryMasterEntity().Save(patientGalleryMaster);
                    }
                    patientGalleryMaster = new PatientGalleryMasterEntity().GetPatientGalleryById(afterGalleryId);
                    if (patientGalleryMaster != null)
                    {
                        patientGalleryMaster.IsActive = !patientGalleryMaster.IsActive;
                        new PatientGalleryMasterEntity().Save(patientGalleryMaster);
                    }
                }
                else if (e.CommandName.ToUpper() == "CUSTOMPUBLICGALLERYACTIVE")
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
                }

                BindListviews();
            }
            catch (System.Threading.ThreadAbortException) { }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Tab event for change list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnEightImage_Click(object sender, EventArgs e)
        {
            if (ViewState["2SearchText"] != null)
                txtSearchVal.Text = Convert.ToString(ViewState["8SearchText"]);
            else
                txtSearchVal.Text = string.Empty;

            lbtnEightImage.CssClass = lbtnEightImage.CssClass == "tabHed-active" ? "tabHed-deactive" : "tabHed-active";
            lbtntwoImage.CssClass = lbtntwoImage.CssClass == "tabHed-active" ? "tabHed-deactive" : "tabHed-active";
            lbtnEightImage.Enabled = false;
            lbtntwoImage.Enabled = true;
            lvTwoImageGallery.Visible = false;
            lvBeforeGallery.Visible = true;
        }

        /// <summary>
        /// Tab event for change list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtntwoImage_Click(object sender, EventArgs e)
        {
            if (ViewState["2SearchText"] != null)
                txtSearchVal.Text = Convert.ToString(ViewState["2SearchText"]);
            else
                txtSearchVal.Text = string.Empty;

            lbtntwoImage.CssClass = lbtntwoImage.CssClass == "tabHed-active" ? "tabHed-deactive" : "tabHed-active";
            lbtnEightImage.CssClass = lbtnEightImage.CssClass == "tabHed-active" ? "tabHed-deactive" : "tabHed-active";
            lbtntwoImage.Enabled = false;
            lbtnEightImage.Enabled = true;
            lvTwoImageGallery.Visible = true;
            lvBeforeGallery.Visible = false;
        }
        #endregion

        #region Helper

        /// <summary>
        /// Method to sort images in listview column header.
        /// </summary>
        private void SetSortImage()
        {
            try
            {
                (lvBeforeGallery.FindControl("lnkSortPatient") as LinkButton).Attributes.Add("class", "");

                LinkButton lnkSortedColumn = null;
                if (ViewState["SortBy"] != null)
                {
                    switch (ViewState["SortBy"].ToString().ToLower())
                    {
                        case "patientname":
                            lnkSortedColumn = lvBeforeGallery.FindControl("lnkSortPatient") as LinkButton;
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
        /// Method to bind listviews
        /// </summary>
        private void BindListviews()
        {
            if (lvBeforeGallery.Visible)
                lvBeforeGallery.DataBind();
            else if (lvTwoImageGallery.Visible)
                lvTwoImageGallery.DataBind();
            else
                lvPublicGallery.DataBind();
        }
        #endregion

        #region Eight Image Template List

        /// <summary>
        /// Event to set sort image of listview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvBeforeGallery_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (lvBeforeGallery.Items.Count > 0)
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
        /// Sets sortField, sortDirection, patientEmail, doctoremail, searchField, searchText parameters before binding list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsBeforeGallery_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                CurrentSession currentSession = new CurrentSession();
                currentSession = (CurrentSession)Session["UserLoginSession"];
                if (currentSession != null)
                {
                    e.InputParameters["sortField"] = ViewState["8SortBy"].ToString();
                    e.InputParameters["sortDirection"] = ViewState["8AscDesc"].ToString();
                    e.InputParameters["doctorEmail"] = currentSession.EmailId;
                    e.InputParameters["patientEmail"] = string.Empty;
                    e.InputParameters["searchText"] = ViewState["8SearchText"] = txtSearchVal.Text.Trim();
                    e.InputParameters["searchField"] = "0";
                }
                lvBeforeGallery.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Event to bind data in listview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvBeforeGallery_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            GetPatientDoctorGallery galleryMaster = e.Item.DataItem as GetPatientDoctorGallery;

            if (galleryMaster != null)
            {
                HyperLink hlnkBefore = (HyperLink)e.Item.FindControl("hlnkBefore");
                HyperLink hlnkAfter = (HyperLink)e.Item.FindControl("hlnkAfter");
                ImageButton hypEdit = (ImageButton)e.Item.FindControl("hypEdit");
                ImageButton imgbtnStatus = e.Item.FindControl("imgbtnStatus") as ImageButton;

                string caseId = Cryptography.EncryptStringAES(galleryMaster.CaseId.ToString(), CommonLogic.GetConfigValue("SharedSecret"));
                string beforeId = Cryptography.EncryptStringAES(galleryMaster.PatientGalleryId.ToString(), CommonLogic.GetConfigValue("SharedSecret"));
                string afterId = Cryptography.EncryptStringAES(galleryMaster.AfterId.ToString(), CommonLogic.GetConfigValue("SharedSecret"));
                string pemailid = Cryptography.EncryptStringAES(galleryMaster.PatientEmail.ToString(), CommonLogic.GetConfigValue("SharedSecret"));
                string pname = Cryptography.EncryptStringAES(galleryMaster.PName.ToString(), CommonLogic.GetConfigValue("SharedSecret"));

                if (galleryMaster.ImageCount > 0)
                    hlnkBefore.Attributes.Add("onclick", "return ShowImgTemplate(" + galleryMaster.PatientGalleryId + ",'" + galleryMaster.DName + "','" + galleryMaster.PName + "','Before')");
                else
                {
                    hlnkBefore.ImageUrl = "Content/images/add.png";
                    hlnkBefore.ToolTip = Convert.ToString(this.GetLocalResourceObject("AddAfterTemplate"));
                    hlnkBefore.NavigateUrl = "AddEditPictureTemplate.aspx?caseId=" + caseId + "&beforeId=" + beforeId + "&afterId=" + afterId + "&pemailid=" + pemailid + "&pname=" + pname;
                }                   

                hypEdit.PostBackUrl = "AddEditPictureTemplate.aspx?caseId=" + caseId + "&beforeId=" + beforeId + "&afterId=" + afterId + "&pemailid=" + pemailid + "&pname=" + pname + "&view=true";

                if (galleryMaster.AfterId > 0)
                {
                    hlnkAfter.Attributes.Add("onclick", "return ShowImgTemplate(" + (galleryMaster.AfterId) + ",'" + galleryMaster.DName + "','" + galleryMaster.PName + "','After')");
                }
                else
                {
                    hlnkAfter.ImageUrl = "Content/images/add.png";
                    hlnkAfter.ToolTip = Convert.ToString(this.GetLocalResourceObject("AddAfterTemplate"));
                    hlnkAfter.NavigateUrl = "AddEditPictureTemplate.aspx?caseId=" + caseId + "&beforeId=" + beforeId + "&afterId=" + afterId + "&pemailid=" + pemailid + "&pname=" + pname;
                }

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
        /// Method to get list of before gallery details
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="searchField"></param>
        /// <param name="searchText"></param>
        /// <param name="doctorEmail"></param>
        /// <param name="patientEmail"></param>
        /// <returns></returns>
        public List<GetPatientDoctorGallery> GetBeforeGalleryDetails(string sortField, string sortDirection, int pageSize, int startRowIndex, string searchField, string searchText, string doctorEmail, string patientEmail)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;
                PatientGalleryMasterEntity patientGalleryMasterEntity = new PatientGalleryMasterEntity();
                List<GetPatientDoctorGallery> lstPatientGallery = patientGalleryMasterEntity.GetPatientDoctorGallery(sortField, sortDirection, pageSize, pageIndex, searchField, searchText, doctorEmail, patientEmail, true, out totalRecords);
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
        /// Method to get total row count in listview.
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="searchField"></param>
        /// <param name="searchText"></param>
        /// <param name="doctorEmail"></param>
        /// <param name="patientEmail"></param>
        /// <returns></returns>
        public int GetBeforeTotalRowCount(string sortField, string sortDirection, string searchField, string searchText, string doctorEmail, string patientEmail)
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

        #endregion

        #region Two Image Template List

        /// <summary>
        /// Sets sortField, sortDirection, patientEmail, doctoremail, searchField, searchText parameters before binding list. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsTwoImageGallery_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                CurrentSession currentSession = new CurrentSession();
                currentSession = (CurrentSession)Session["UserLoginSession"];
                if (currentSession != null)
                {
                    e.InputParameters["sortField"] = ViewState["2SortBy"].ToString();
                    e.InputParameters["sortDirection"] = ViewState["2AscDesc"].ToString();
                    e.InputParameters["doctorEmail"] = currentSession.EmailId;
                    e.InputParameters["listBy"] = "2";
                    e.InputParameters["searchText"] = ViewState["2SearchText"] = txtSearchVal.Text.Trim();
                    e.InputParameters["searchField"] = "0";
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

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
        public List<AllPatientGallleryDetail> GetTwoImageGallery(string sortField, string sortDirection, int pageSize, int startRowIndex, string searchField, string searchText, string doctorEmail, string listBy)
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
        public int GetTwoImageGalleryTotalRowCount(string sortField, string sortDirection, string searchField, string searchText, string doctorEmail, string listBy)
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
        /// Event to bind data in two image gallery listview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvTwoImageGallery_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            AllPatientGallleryDetail galleryDetails = e.Item.DataItem as AllPatientGallleryDetail;
            ImageButton img = e.Item.FindControl("imgbtnStatus") as ImageButton;
            //Image imgBefore = e.Item.FindControl("imgBefore") as Image;
            //Image imgAfter = e.Item.FindControl("imgAfter") as Image;

            List<BeforeAfter> lstBeforeAfter = new List<BeforeAfter>();
            List<PatientGallery> lstPatientGallery = new PatientGalleryEntity().GetPatientGalleriesByGalleryId(galleryDetails.PatientGalleryId);

            if (lstPatientGallery != null && lstPatientGallery.Count > 0)
            {
                BeforeAfter objBeforeAfter = null;

                //imgBefore.ImageUrl = "PatientFiles/thumbs/" + lstPatientGallery[0].FileName;
                //imgAfter.ImageUrl = (lstPatientGallery.Count > 1) ? "PatientFiles/thumbs/" + lstPatientGallery[1].FileName : string.Empty;

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

        #endregion

        #region Public Gallery List

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
        /// set parameter values to search list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsPublicGallery_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["sortField"] = ViewState["SortBy"];
                e.InputParameters["sortDirection"] = ViewState["AscDesc"];
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

        #endregion

        protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
        {
            if (lvBeforeGallery.Visible)
            {
                ViewState["8SearchText"] = null;
                txtSearchVal.Text = string.Empty;
            }
            else
            {
                ViewState["2SearchText"] = null;
                txtSearchVal.Text = string.Empty;
            }
            txtSearchVal.Text = string.Empty;
            BindListviews();
        }
    }
}