using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace _4eOrtho
{
    public partial class PatientBeforeAfterPictures : PageBase
    {
        #region Declaration
        private static ILog logger = log4net.LogManager.GetLogger(typeof(PatientBeforeAfterPictures));
        private static string emaiId = string.Empty;
        int totalRecordCount;
        #endregion

        #region Events

        /// <summary>
        /// Page Load Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                if (currentSession != null)
                {
                    PageRight pageRight = CheckRights(this.Page.GetType().BaseType.Name);
                    if (pageRight != null)
                    {
                        PageRedirect(pageRight.RedirectPageName);
                    }

                    emaiId = currentSession.EmailId;
                    //rep8Image.DataSource = new PatientGalleryMasterEntity().GetEightImageTemplate(currentSession.EmailId);
                    //rep8Image.DataBind();
                    //dvRepEmpty.Visible = (rep8Image != null && rep8Image.Items.Count > 0) ? false : true;
                }
            }
            //if (!string.IsNullOrEmpty(emaiId))
            //{
            //    List<PatientGalleryMaster> lstGallery = new PatientGalleryMasterEntity().GetPatientGalleryMasterByEmail(emaiId, true);

            //    if (lstGallery != null && lstGallery.Count > 0)
            //    {
            //        lstGallery = lstGallery.FindAll(x => x.Treatment.Equals("before", StringComparison.InvariantCultureIgnoreCase));

            //        lvImageTemplate.DataSource = lstGallery;
            //        lvImageTemplate.DataBind();

            //        //AllDoctorDetailsByEmailId doctor = new DoctorEntity().GetDoctorDetailsByEmailId(lstGallery[0].DoctorEmail);
            //        //PatientGalleryMaster gallery = lstGallery.Find(f => f.IsActive == true && f.Treatment.Equals("Before", StringComparison.InvariantCultureIgnoreCase));
            //        //dvBefore.Visible = gallery != null;
            //        //gallery = lstGallery.Find(f => f.IsActive == true && f.Treatment.Equals("after", StringComparison.InvariantCultureIgnoreCase));
            //        //dvAfter.Visible = gallery != null;
            //    }
            //}
        }

        /// <summary>
        /// Event to bind data to image template listview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvImageTemplate_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            GetPatientDoctorGallery galleryMaster = e.Item.DataItem as GetPatientDoctorGallery;

            if (galleryMaster != null)
            {
                HyperLink hlnkBefore = (HyperLink)e.Item.FindControl("hlnkBefore");
                HyperLink hlnkAfter = (HyperLink)e.Item.FindControl("hlnkAfter");

                hlnkBefore.Attributes.Add("onclick", "return ShowImgTemplate(" + galleryMaster.PatientGalleryId + ",'" + galleryMaster.DName + "','Before')");
                if (galleryMaster.AfterId > 0)
                    hlnkAfter.Attributes.Add("onclick", "return ShowImgTemplate(" + (galleryMaster.AfterId) + ",'" + galleryMaster.DName + "','After')");
                else
                    hlnkAfter.Visible = false;
            }
        }

        /// <summary>
        /// Event to set sort image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvBeforeGallery_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (lvBeforeGallery.Items.Count > 0)
                {
                    //SetSortImage();
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Event to bind eight image tempate data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rep8Image_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            GetEightImageTemplate galleryMaster = e.Item.DataItem as GetEightImageTemplate;

            if (galleryMaster != null)
            {
                Label lblTitle = (Label)e.Item.FindControl("lblTitle");
                HyperLink hlnkBefore = (HyperLink)e.Item.FindControl("hlnkBefore");
                HyperLink hlnkAfter = (HyperLink)e.Item.FindControl("hlnkAfter");

                if (hlnkBefore != null)
                    hlnkBefore.Attributes.Add("onclick", "return ShowImgTemplate(" + galleryMaster.PatientGalleryId + ",'" + galleryMaster.DName + "','Before')");
                if (hlnkAfter != null)
                    hlnkAfter.Attributes.Add("onclick", "return ShowImgTemplate(" + (galleryMaster.AfterId) + ",'" + galleryMaster.DName + "','After')");
                if (lblTitle != null)
                    lblTitle.Text = galleryMaster.Treatment;
            }
        }

        /// <summary>
        /// Event to set sortField, sortDirection, doctorEmail, patientEmail parameter for filter record list.
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
                    e.InputParameters["sortField"] = "DoctorName";
                    e.InputParameters["sortDirection"] = "ASC";
                    e.InputParameters["doctorEmail"] = string.Empty;
                    e.InputParameters["patientEmail"] = currentSession.EmailId;
                    //if (ddlGallery.SelectedValue != "0")
                    //{
                    //    e.InputParameters["searchField"] = ddlGallery.SelectedValue;
                    //    e.InputParameters["searchText"] = txtSearchVal.Text.Trim();
                    //}
                    //else
                    //{
                    e.InputParameters["searchField"] = "0";
                    e.InputParameters["searchText"] = string.Empty;
                    //}
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Method to get patient image paths.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<PatientGalleryImageCategory> GetPatientImagePaths()
        {
            try
            {

                if (!string.IsNullOrEmpty(emaiId))
                {
                    List<PatientGalleryMaster> lstPatientGalleryMaster = new List<PatientGalleryMaster>();
                    PatientGalleryMasterEntity patientGalleryMasterEntity = new PatientGalleryMasterEntity();
                    PatientGalleryEntity patientGalleryEntity = new PatientGalleryEntity();
                    List<PatientGalleryImageCategory> lstPatientGalleryImageCategory = new List<PatientGalleryImageCategory>();
                    lstPatientGalleryMaster = patientGalleryMasterEntity.GetPatientGalleryMasterByEmail(emaiId, false);
                    foreach (PatientGalleryMaster galleryMaster in lstPatientGalleryMaster)
                    {
                        if (galleryMaster.Treatment != "Before" && galleryMaster.Treatment != "After")
                        {
                            PatientGalleryImageCategory imageGallery = new PatientGalleryImageCategory();
                            imageGallery.Treatment = galleryMaster.Treatment;
                            imageGallery.Path = patientGalleryEntity.GetPatientGalleriesByPatientEmail(galleryMaster.PatientEmail).Where(x => x.GalleryId == galleryMaster.PatientGalleryId).Select(x => x.FileName).ToList();
                            lstPatientGalleryImageCategory.Add(imageGallery);
                        }
                    }
                    return lstPatientGalleryImageCategory;
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return null;
            }
        }

        /// <summary>
        /// Method to get before gallery details
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
        /// Method to get total row count of list.
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
    }
    public class PatientGalleryImageCategory
    {
        public string Treatment { get; set; }
        public List<string> Path { get; set; }
    }
}