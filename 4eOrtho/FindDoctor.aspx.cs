using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho
{
    public partial class FindDoctor : PageBase
    {
        #region Declaration
        //private StateEntity stateEntity;
        //private DoctorEntity doctorEntity;
        private int totalRecordCount;
        private ILog logger = log4net.LogManager.GetLogger(typeof(FindDoctor));
        #endregion Declaration

        #region Events

        /// <summary>
        /// set master page as per user session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreInit(object sender, EventArgs e)
        {

            if (Session["UserLoginSession"] != null)
            {
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                if (currentSession.UserType.ToString() == UserType.D.ToString() || currentSession.UserType.ToString() == UserType.S.ToString() || currentSession.UserType.ToString() == UserType.P.ToString())
                {
                    this.MasterPageFile = "~/OrthoInnerMaster.Master";
                }
                else
                {
                    this.MasterPageFile = "~/Ortho.Master";
                }
            }
            else
            {
                this.MasterPageFile = "~/Ortho.Master";
            }
        }

        /// <summary>/// bind state list and initialize viewstate
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

                    BindStateList();
                }
                this.Form.DefaultButton = this.btnSearch.UniqueID;
                Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Certified doctor object data resource selecting events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsCertifiedDoctor_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                e.InputParameters["patientEmail"] = (currentSession != null) ? currentSession.EmailId : string.Empty;
                e.InputParameters["SearchField"] = ddlSearchBy.SelectedValue;
                e.InputParameters["SearchValue"] = txtSearch.Text.Trim();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Search Doctor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lvCertifiedDoctor.DataBind();
                lvNonCertifiedDoctor.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Get Non Certified doctor object resource events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsNonCertifiedDoctor_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                e.InputParameters["patientEmail"] = (currentSession != null) ? currentSession.EmailId : string.Empty;
                e.InputParameters["SearchField"] = ddlSearchBy.SelectedValue;
                e.InputParameters["SearchValue"] = txtSearch.Text.Trim();                
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Refersh doctor list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = "";
                ddlSearchBy.SelectedIndex = 0;
                lvCertifiedDoctor.DataBind();
                //lvNonCertifiedDoctor.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Take appointment command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkAddAppoinment_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Appointment")
                {
                    string[] arg = new string[2];
                    arg = e.CommandArgument.ToString().Split(';');
                    string doctorEmailId = arg[0];
                    string doctorName = arg[1];
                    //SendAppointmentRequestMail(doctorEmailId, doctorName);
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// bind non certified doctor profile picture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvNonCertifiedDoctor_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            NonCertifiedDoctorDetailsByFilterType nonCertifiedDoctorDetailsByFilterType = e.Item.DataItem as NonCertifiedDoctorDetailsByFilterType;
            Image imgDoctorProfile = e.Item.FindControl("imgDoctorProfile") as Image;

            if (nonCertifiedDoctorDetailsByFilterType.SourceType == SourceType.EMR.ToString())
            {
                if (!string.IsNullOrEmpty(nonCertifiedDoctorDetailsByFilterType.PhotographName))
                {
                    imgDoctorProfile.ImageUrl = nonCertifiedDoctorDetailsByFilterType.DomainURL + "/Photograph/" + nonCertifiedDoctorDetailsByFilterType.PhotographName;
                    //   imgDoctorProfile.ImageUrl = CommonLogic.GetConfigValue("EMR_DoctorPath") + nonCertifiedDoctorDetailsByFilterType.PhotographName;
                }
                else
                {

                    imgDoctorProfile.ImageUrl = "Content/images/male_sm.jpg";
                }
            }
            else if (nonCertifiedDoctorDetailsByFilterType.SourceType == SourceType.AAAD.ToString())
            {
                if (!string.IsNullOrEmpty(nonCertifiedDoctorDetailsByFilterType.PhotographName))
                {
                    imgDoctorProfile.ImageUrl = CommonLogic.GetConfigValue("AAAD_DoctorPath") + nonCertifiedDoctorDetailsByFilterType.PhotographName;
                }
                else
                {
                    imgDoctorProfile.ImageUrl = "Content/images/male_sm.jpg";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(nonCertifiedDoctorDetailsByFilterType.PhotographName))
                    imgDoctorProfile.ImageUrl = nonCertifiedDoctorDetailsByFilterType.DomainURL + "/Photograph/" + nonCertifiedDoctorDetailsByFilterType.PhotographName;
                else
                    imgDoctorProfile.ImageUrl = "Content/images/male_sm.jpg";
            }
        }

        /// <summary>
        /// bind certified doctor profile pictures
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvCertifiedDoctor_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            CertifiedDoctorDetailsByFilterType certifiedDoctorDetailsByFilterType = e.Item.DataItem as CertifiedDoctorDetailsByFilterType;
            Image imgDoctorProfile = e.Item.FindControl("imgDoctorProfile") as Image;
            if (!string.IsNullOrEmpty(certifiedDoctorDetailsByFilterType.PhotographerName))
            {
                imgDoctorProfile.ImageUrl = CommonLogic.GetConfigValue("AAAD_DoctorPath") + certifiedDoctorDetailsByFilterType.PhotographerName;
            }
            else
            {
                imgDoctorProfile.ImageUrl = "Content/images/male_sm.jpg";
            }
        }

        #endregion Events

        #region Helpers

        /// <summary>
        /// Bind state list
        /// </summary>
        private void BindStateList()
        {
            try
            {
                //stateEntity = new StateEntity();
                //List<StateDetails> lstState = new List<StateDetails>();
                //lstState = stateEntity.GetAllStateDetails();
                //ddlState.DataSource = lstState;
                //ddlState.DataTextField = "StateName";
                //ddlState.DataValueField = "StateName";
                //ddlState.DataBind();
                //ddlState.Items.Insert(0, new ListItem("Select State", "0"));
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
        /// used to fetch all doctor information on basis of parameters passed
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="startRowIndex"></param>
        /// <returns></returns>
        public List<CertifiedDoctorDetailsByFilterType> GetCertifiedDoctorDetailsByFilterType(string sortField, string sortDirection, int pageSize, int startRowIndex, string SearchField, string SearchValue, string patientEmail)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;
                DoctorEntity doctorEntity = new DoctorEntity();
                List<CertifiedDoctorDetailsByFilterType> lstDoctor = doctorEntity.GetCertifiedDoctorDetailsByFilterType(sortField, sortDirection, pageSize, pageIndex, out totalRecords, SearchField, SearchValue, patientEmail);
                totalRecordCount = totalRecords;
                return lstDoctor;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return null;
            }
        }

        /// <summary>
        /// Get Non Certificate Doctor Details
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="nonCertiState"></param>
        /// <param name="nonCertiZipCode"></param>
        /// <returns></returns>
        public List<NonCertifiedDoctorDetailsByFilterType> GetNonCertifiedDoctorDetailsByFilterType(string sortField, string sortDirection, int pageSize, int startRowIndex, string SearchField, string SearchValue, string patientEmail)
        {
            try
            {
                int pageIndex = startRowIndex / pageSize;
                int totalRecords;
                DoctorEntity doctorEntity = new DoctorEntity();
                List<NonCertifiedDoctorDetailsByFilterType> lstDoctor = doctorEntity.GetNonCertifiedDoctorDetailsByFilterType(sortField, sortDirection, pageSize, pageIndex, out totalRecords, SearchField, SearchValue, patientEmail);
                totalRecordCount = totalRecords;
                return lstDoctor;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return null;
            }
        }

        /// <summary>
        /// gives total doctor record count
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public int GetTotalRowCertifiedCount(string sortField, string sortDirection, string SearchField, string SearchValue, string patientEmail)
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
        /// gives total doctor record count
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public int GetTotalRowNonCertifiedCount(string sortField, string sortDirection, string SearchField, string SearchValue, string patientEmail)
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

        #endregion Helpers
    }
}