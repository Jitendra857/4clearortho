using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho
{
    public partial class AddEditPictureTemplate : PageBase
    {
        #region Declaration
        public int tabIndex = 0;
        string path = HttpContext.Current.Request.PhysicalApplicationPath + "PatientFiles\\slides\\";
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListPatientGallery));
        List<string> lstFileList = new List<string>();
        CurrentSession currentSession;
        public long caseId
        {
            get
            {
                try
                {
                    return Convert.ToInt64(Cryptography.DecryptStringAES(CommonLogic.QueryString("caseId"), CommonLogic.GetConfigValue("SharedSecret")));
                }
                catch
                {
                    return 0;
                }

            }
        }
        public long beforeId
        {
            get
            {
                try
                {
                    return Convert.ToInt64(Cryptography.DecryptStringAES(CommonLogic.QueryString("beforeId"), CommonLogic.GetConfigValue("SharedSecret")));
                }
                catch
                {
                    return 0;
                }

            }
        }
        public long afterId
        {
            get
            {
                try
                {
                    return Convert.ToInt64(Cryptography.DecryptStringAES(CommonLogic.QueryString("afterId"), CommonLogic.GetConfigValue("SharedSecret")));
                }
                catch
                {
                    return 0;
                }

            }
        }
        public string pemailid
        {
            get
            {
                try
                {
                    return Cryptography.DecryptStringAES(CommonLogic.QueryString("pemailid"), CommonLogic.GetConfigValue("SharedSecret"));
                }
                catch
                {
                    return string.Empty;
                }

            }
        }
        public string pname
        {
            get
            {
                try
                {
                    return Cryptography.DecryptStringAES(CommonLogic.QueryString("pname"), CommonLogic.GetConfigValue("SharedSecret"));
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
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
                if ((CurrentSession)Session["UserLoginSession"] != null)
                {
                    PageRight pageRight = CheckRights(this.Page.GetType().BaseType.Name);
                    if (pageRight != null)
                    {
                        PageRedirect(pageRight.RedirectPageName);
                    }
                }
                if (!Page.IsPostBack)
                {
                    if (beforeId > 0)
                        FillGallery();

                    if (!string.IsNullOrEmpty(CommonLogic.QueryString("view")))
                        ViewMode();
                    else if (!string.IsNullOrEmpty(hdnImages.Value))
                        tabIndex = 1;
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error Occured", ex);
            }
        }

        /// <summary>
        /// Event to submit picture template data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid && fuFile1.Enabled)
                {
                    TransactionScope scope = new TransactionScope();
                    using (scope)
                    {
                        SaveImage();
                        SavePhotoGallery(beforeId, caseId, "Before", pemailid);
                        SavePhotoGallery(afterId, caseId, "After", pemailid);
                        Session["lstFileList"] = null;
                        scope.Complete();
                    }
                    Response.Redirect("ListPictureTemplate.aspx", false);
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                Session["lstFileList"] = null;
                logger.Error("An error Occured", ex);
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Method to display gallery.
        /// </summary>
        private void FillGallery()
        {
            try
            {
                if (beforeId > 0)
                {
                    //patientGalleryMaster = new PatientGalleryMasterEntity().GetPatientGalleryById(beforeId);
                    //if (patientGalleryMaster != null)                    
                    //    chkIsActive.Checked = patientGalleryMaster.IsActive;

                    List<PatientGallery> lstPatientGallery = new PatientGalleryEntity().GetPatientGalleriesByGalleryId(beforeId);
                    if (lstPatientGallery != null && lstPatientGallery.Count > 0)
                    {
                        lbldate.Text = lstPatientGallery[0].CreatedDate.ToString("MM/dd/yyyy");
                        foreach (PatientGallery patientGallery in lstPatientGallery)
                            lstFileList.Add(patientGallery.FileName);
                    }
                }
                if (afterId > 0)
                {
                    //patientGalleryMaster = new PatientGalleryMasterEntity().GetPatientGalleryById(afterId);
                    //if (patientGalleryMaster != null)
                    //    chkIsActive.Checked = patientGalleryMaster.IsActive;

                    List<PatientGallery> lstPatientGallery = new PatientGalleryEntity().GetPatientGalleriesByGalleryId(afterId);
                    {
                        if (lstPatientGallery != null && lstPatientGallery.Count > 0)
                        {
                            lbldate2.Text = lstPatientGallery[0].CreatedDate.ToString("MM/dd/yyyy");
                            foreach (PatientGallery patientGallery in lstPatientGallery)
                                lstFileList.Add(patientGallery.FileName);
                        }
                    }
                }

                Session["lstFileList"] = lstFileList;

                hdnImages.Value = string.Join(",", lstFileList.ToArray());
                int j = 17;
                if (afterId == 0)
                    j = 9;

                for (int i = 1; i < j; i++)
                {
                    RequiredFieldValidator rfv;
                    rfv = (RequiredFieldValidator)pnlImage.FindControl("rfvFile" + i);
                    if (rfv == null)
                        rfv = (RequiredFieldValidator)pnlImage1.FindControl("rfvFile" + i);
                    if (rfv != null)
                    {
                        rfv.Enabled = false;
                        rfv.ValidationGroup = "";
                    }
                }
                currentSession = (CurrentSession)Session["UserLoginSession"];
                lblDoctorName.Text = lblDoctorName1.Text = "Dr. " + currentSession.DoctorName;
                lblPatientName.Text = lblPatientName1.Text = "Patient: " + pname;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured" + ex);
            }
        }

        /// <summary>
        /// Method to Save photo gallery.
        /// </summary>
        /// <param name="patientGalleryId"></param>
        /// <param name="CaseId"></param>
        /// <param name="sBeforeAfter"></param>
        /// <param name="patientEmailId"></param>
        private void SavePhotoGallery(long patientGalleryId, long CaseId, string sBeforeAfter, string patientEmailId)
        {
            try
            {
                PatientGalleryMasterEntity patientGalleryMasterEntity = new PatientGalleryMasterEntity();
                PatientGalleryMaster patientGalleryMaster;
                currentSession = (CurrentSession)Session["UserLoginSession"];

                if (patientGalleryId > 0)
                {
                    patientGalleryMaster = patientGalleryMasterEntity.GetPatientGalleryById(patientGalleryId);
                    patientGalleryMaster.LastUpdatedBy = Authentication.GetLoggedUserID();
                    patientGalleryMaster.LastUpdatedDate = BaseEntity.GetServerDateTime;
                }
                else
                {
                    patientGalleryMaster = patientGalleryMasterEntity.Create();
                    patientGalleryMaster.CreatedBy = Authentication.GetLoggedUserID();
                    patientGalleryMaster.CreatedDate = BaseEntity.GetServerDateTime;

                    patientGalleryMaster.PatientId = 0;
                    patientGalleryMaster.Treatment = sBeforeAfter;
                    patientGalleryMaster.CaseId = CaseId;
                    patientGalleryMaster.isTemplate = true;
                    patientGalleryMaster.DoctorEmail = currentSession.EmailId;
                    patientGalleryMaster.PatientEmail = patientEmailId;
                }

                patientGalleryMaster.BeforeGalleryId = sBeforeAfter == "After" ? beforeId : 0;
                patientGalleryId = patientGalleryMasterEntity.Save(patientGalleryMaster);

                PatientGalleryEntity patientGalleryEntity = new PatientGalleryEntity();
                patientGalleryEntity.RemoveGalleryIdFiles(patientGalleryId);
                PatientGallery patientGallery;

                foreach (string newFileName in lstFileList)
                {
                    if (sBeforeAfter == "Before" && lstFileList.IndexOf(newFileName) > 7)
                        break;
                    else if (sBeforeAfter == "After" && lstFileList.IndexOf(newFileName) <= 7)
                        continue;

                    patientGallery = patientGalleryEntity.Create();
                    patientGallery.CreatedBy = Authentication.GetLoggedUserID();
                    patientGallery.CreatedDate = BaseEntity.GetServerDateTime;
                    patientGallery.GalleryId = patientGalleryId;
                    patientGallery.FileName = newFileName;
                    patientGallery.IsActive = true;
                    patientGalleryEntity.Save(patientGallery);
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured" + ex);
            }
        }

        /// <summary>
        /// Method to save images on server.
        /// </summary>
        private void SaveImage()
        {
            try
            {
                if (Session["lstFileList"] != null)
                    lstFileList = (List<string>)Session["lstFileList"];

                FileUpload imgFile;
                for (int i = 1; i <= 16; i++)
                {
                    imgFile = new FileUpload();
                    imgFile = (FileUpload)pnlImage.FindControl("fuFile" + i);

                    if (imgFile == null)
                        imgFile = (FileUpload)pnlImage1.FindControl("fuFile" + i);

                    if (imgFile != null && imgFile.HasFile)
                    {
                        if (imgFile.PostedFile != null)
                        {
                            string photographName = imgFile.PostedFile.FileName;
                            String FileExtension = Path.GetExtension(photographName).ToLower();
                            Guid newName = Guid.NewGuid();
                            string[] phName = photographName.Split('.');
                            string newPhotoName = phName[0].ToString() + "_" + newName + "." + phName[1].ToString();
                            string newPhotoPath = path + newPhotoName;
                            Stream strm = imgFile.PostedFile.InputStream;
                            GenerateThumbnails(0.5, strm, newPhotoPath);

                            //imgFile.PostedFile.SaveAs(newPhotoPath);


                            if (lstFileList != null && i < lstFileList.Count)
                            {
                                if (File.Exists(path + lstFileList[i - 1]))
                                {
                                    File.Delete(path + lstFileList[i - 1]);
                                }
                                lstFileList[i - 1] = newPhotoName;
                            }
                            else
                                lstFileList.Add(newPhotoName);
                        }
                    }
                }
                Session["lstFileList"] = lstFileList;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured" + ex);
            }
        }

        /// <summary>
        /// Method to set view mode page.
        /// </summary>
        private void ViewMode()
        {
            fuFile1.Enabled = false;
            fuFile2.Enabled = false;
            fuFile3.Enabled = false;
            fuFile4.Enabled = false;
            fuFile5.Enabled = false;
            fuFile6.Enabled = false;
            fuFile7.Enabled = false;
            fuFile8.Enabled = false;
            fuFile9.Enabled = false;
            fuFile10.Enabled = false;
            fuFile11.Enabled = false;
            fuFile12.Enabled = false;
            fuFile13.Enabled = false;
            fuFile14.Enabled = false;
            fuFile15.Enabled = false;
            fuFile16.Enabled = false;
        }

        /// <summary>
        /// Method to compress and generate thumbnails image from file upload control.
        /// </summary>
        /// <param name="scaleFactor"></param>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        private void GenerateThumbnails(double scaleFactor, Stream sourcePath, string targetPath)
        {
            using (var image = System.Drawing.Image.FromStream(sourcePath))
            {
                var newWidth = (int)(image.Width * scaleFactor);
                var newHeight = (int)(image.Height * scaleFactor);
                var thumbnailImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath, image.RawFormat);
            }
        }

        #endregion
    }
}