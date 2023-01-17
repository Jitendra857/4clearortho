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
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho
{
    public partial class AddEditPatientGallery : PageBase
    {
        #region Declaration
        public long patientGalleryId = 0;
        PatientGalleryMasterEntity patientGalleryMasterEntity;
        PatientGalleryMaster patientGalleryMaster;
        string path = HttpContext.Current.Request.PhysicalApplicationPath + "PatientFiles\\slides\\";
        private ILog logger = log4net.LogManager.GetLogger(typeof(AddEditPatientGallery));
        PatientGalleryEntity patientGalleryEntity;
        PatientGallery patientGallery;
        List<PatientGallery> patientGalleries;        
        List<string> lstFileList = new List<string>();
        long beforeId = 0;
        #endregion

        #region Events

        /// <summary>
        /// Bind Gallery and Patient list
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

                if (!String.IsNullOrEmpty(CommonLogic.GetSessionValue("PatientGalleryId")))
                    patientGalleryId = Convert.ToInt64(CommonLogic.GetSessionValue("PatientGalleryId"));
                if (!IsPostBack)
                {
                    PatientListBind();
                    if (patientGalleryId > 0)
                    {
                        if (Request.QueryString["preview"] != "1")
                            BindGallery();

                        lblHeader.Text = this.GetLocalResourceObject("lblHeaderResource2").ToString();
                        Page.Title = this.GetLocalResourceObject("PageResource2").ToString();
                    }
                    else
                    {
                        lblHeader.Text = this.GetLocalResourceObject("lblHeaderResource1.Text").ToString();
                        lbldate2.Text = lbldate.Text = DateTime.Now.ToString("MM/dd/yyyy");
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
        /// save patient gallery files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    TransactionScope scope = new TransactionScope();
                    using (scope)
                    {
                        if (rbtnIsTemplate.SelectedItem.Value == "2")
                        {
                            SavePhotoGallery();
                        }
                        else
                        {
                            SaveImage();
                            SavePhotoGallery("Before");
                            SavePhotoGallery("After");
                            Session["lstFileList"] = null;
                        }
                        scope.Complete();
                        //Response.Redirect("ListPatientGallery.aspx", false);
                        Response.Redirect("ListPictureTemplate.aspx", false);
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
        /// clear patient gallery files and galleryid  when back 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Session["PatientGalleryFiles"] = null;
                Session["GalleryId"] = null;
                Response.Redirect("~/Admin/ListPatientGallery.aspx", false);
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
        /// clear patient gallery files when reset 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Session["GalleryFiles"] = null;
                Response.Redirect("~/Admin/AddEditGallery.aspx");
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
        /// Radio selection change template view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbtnIsTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            phTwoImageTemplate.Visible = (rbtnIsTemplate.SelectedItem.Value == "2");
            phEightImageTemplate.Visible = !(rbtnIsTemplate.SelectedItem.Value == "2");
            lblPname2.Text = lblPName.Text = ddlPatient.SelectedItem.Text;

            if (phEightImageTemplate.Visible)
            {
                txtTreatment.Text = "Before After";
                txtTreatment.Enabled = false;
            }
            else
            {
                txtTreatment.Text = string.Empty;
                txtTreatment.Enabled = false;
            }
        }

        #endregion

        #region Helpers
        /// <summary>
        /// patient gallery bind
        /// </summary>
        public void BindGallery()
        {
            try
            {
                patientGalleryMaster = patientGalleryMasterEntity.GetPatientGalleryById(patientGalleryId);
                patientGalleries = patientGalleryEntity.GetPatientGalleriesByGalleryId(patientGalleryId);
                if (patientGalleryMaster != null)
                {
                    ddlPatient.SelectedValue = patientGalleryMaster.PatientEmail;
                    txtTreatment.Text = patientGalleryMaster.Treatment;
                    chkIsActive.Checked = patientGalleryMaster.IsActive;

                    foreach (PatientGallery patientGallery in patientGalleries)
                    {
                        hdnImages.Value += patientGallery.FileName + ",";
                    }
                    hdnImages.Value = hdnImages.Value.Remove(hdnImages.Value.Length - 1, 1);
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Maintain aspect ratio as per images
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        /// <param name="filename"></param>
        /// <param name="width"></param>
        protected void CreateResizedCopy(string sourcePath, string destinationPath, string filename, int width)
        {
            try
            {
                if (!Directory.Exists(Server.MapPath(destinationPath)))
                {
                    Directory.CreateDirectory(Server.MapPath(destinationPath));
                }
                if (!File.Exists(Server.MapPath(destinationPath + filename)))
                {
                    System.Drawing.Image sourceImage = System.Drawing.Image.FromFile(sourcePath);
                    int X = sourceImage.Width;
                    int Y = sourceImage.Height;
                    int height = (int)((width * Y) / X);
                    System.Drawing.Image.GetThumbnailImageAbort dummyCallback = new System.Drawing.Image.GetThumbnailImageAbort(ResizeAbort);
                    System.Drawing.Image fullSizeImg = System.Drawing.Image.FromFile(sourcePath);
                    System.Drawing.Image thumbImg = fullSizeImg.GetThumbnailImage(width, height, dummyCallback, IntPtr.Zero);
                    thumbImg.Save(Server.MapPath(destinationPath + filename));
                    thumbImg.Dispose();
                    sourceImage.Dispose();
                    fullSizeImg.Dispose();
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Resize abort when error
        /// </summary>
        /// <returns></returns>
        protected bool ResizeAbort()
        {
            lblMsg.Text += "Error creating thumb.<br/>";
            return false;
        }

        /// <summary>
        /// patient list bind to dropdown
        /// </summary>
        private void PatientListBind()
        {
            PatientEntity patientEntity = new PatientEntity();
            CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
            if (currentSession != null)
            {
                List<PatientListByDoctorEmail> lstPatient = patientEntity.GetAllPatientByDoctorEmail(currentSession.EmailId);
                if (lstPatient != null && lstPatient.Count > 0)
                {
                    ddlPatient.DataSource = lstPatient;
                    ddlPatient.DataTextField = "PatientName";
                    ddlPatient.DataValueField = "EmailId";
                    ddlPatient.DataBind();
                }
            }
            ddlPatient.Items.Insert(0, new ListItem("Select Patient", "0"));
        }

        /// <summary>
        /// Method to save images.
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
        /// Photo Save
        /// </summary>
        private void SavePhotoGallery()
        {
            try
            {
                if (Page.IsValid)
                {
                    HttpFileCollection uploadedFiles = Request.Files;
                    string photographName, FileExtension, newPhotoPath, newPhotoName = string.Empty;
                    Guid newName;
                    string[] phName = null;
                    List<string> lstGalleryFiles = new List<string>();
                    List<string> lstOldFile = hdnImages.Value.Length > 0 ? hdnImages.Value.Split(',').ToList() : null;

                    for (int i = 0; i < uploadedFiles.Count; i++)
                    {
                        HttpPostedFile userPostedFile = uploadedFiles[i];
                        try
                        {
                            if (userPostedFile.ContentLength > 0)
                            {
                                photographName = userPostedFile.FileName;
                                FileExtension = Path.GetExtension(photographName).ToLower();
                                newName = Guid.NewGuid();
                                phName = photographName.Split('.');
                                newPhotoName = phName[0].ToString() + "_" + newName + "." + phName[1].ToString();
                                newPhotoPath = path + newPhotoName;
                                Stream strm = userPostedFile.InputStream;
                                GenerateThumbnails(0.5, strm, newPhotoPath);                               
                                //userPostedFile.SaveAs(newPhotoPath);
                                CreateResizedCopy(newPhotoPath, "PatientFiles/thumbs/", newPhotoName, 200);
                                lstGalleryFiles.Add(newPhotoName);
                            }
                            else if (lstOldFile != null && lstOldFile.Count > 0)
                            {
                                lstGalleryFiles.Add(lstOldFile[i]);
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error("An error occured.", ex);
                        }
                    }

                    if (lstGalleryFiles != null && lstGalleryFiles.Count > 0)
                    {
                        CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                        patientGalleryMasterEntity = new PatientGalleryMasterEntity();

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
                        }

                        patientGalleryMaster.Treatment = txtTreatment.Text;
                        patientGalleryMaster.IsActive = chkIsActive.Checked;
                        patientGalleryMaster.DoctorEmail = currentSession.EmailId;
                        patientGalleryMaster.PatientEmail = ddlPatient.SelectedValue;
                        patientGalleryId = patientGalleryMasterEntity.Save(patientGalleryMaster);

                        //before add delete previous files of galleryid
                        patientGalleryEntity = new PatientGalleryEntity();
                        patientGalleryEntity.RemoveGalleryIdFiles(patientGalleryId);

                        foreach (string fileName in lstGalleryFiles)
                        {
                            patientGallery = patientGalleryEntity.Create();
                            patientGallery.CreatedBy = Authentication.GetLoggedUserID();
                            patientGallery.CreatedDate = BaseEntity.GetServerDateTime;
                            patientGallery.GalleryId = patientGalleryId;
                            patientGallery.FileName = fileName;
                            patientGallery.IsActive = true;
                            patientGalleryEntity.Save(patientGallery);
                        }
                    }
                    CommonLogic.RemoveSession("PatientGalleryId");
                }
                else
                {
                    CommonHelper.ShowMessage(MessageType.Error, "Please select picture before submit.", divMsg, lblMsg);
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Mothod to save photo gallery by before after.
        /// </summary>
        /// <param name="sBeforeAfter"></param>
        private void SavePhotoGallery(string sBeforeAfter)
        {
            try
            {
                PatientGalleryMasterEntity patientGalleryMasterEntity = new PatientGalleryMasterEntity();
                PatientGalleryMaster patientGalleryMaster;
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];

                //if (patientGalleryId > 0)
                //{
                //    patientGalleryMaster = patientGalleryMasterEntity.GetPatientGalleryById(patientGalleryId);
                //    patientGalleryMaster.LastUpdatedBy = Authentication.GetLoggedUserID();
                //    patientGalleryMaster.LastUpdatedDate = BaseEntity.GetServerDateTime;
                //}
                //else
                //{
                patientGalleryMaster = patientGalleryMasterEntity.Create();
                patientGalleryMaster.CreatedBy = Authentication.GetLoggedUserID();
                patientGalleryMaster.CreatedDate = BaseEntity.GetServerDateTime;

                patientGalleryMaster.PatientId = 0;
                patientGalleryMaster.Treatment = sBeforeAfter;//txtTreatment.Text;
                patientGalleryMaster.CaseId = 0;
                patientGalleryMaster.isTemplate = true;
                patientGalleryMaster.IsActive = true;
                patientGalleryMaster.DoctorEmail = currentSession.EmailId;
                patientGalleryMaster.PatientEmail = ddlPatient.SelectedItem.Value;
                patientGalleryMaster.BeforeGalleryId = beforeId;

                //}

                patientGalleryId = patientGalleryMasterEntity.Save(patientGalleryMaster);

                beforeId = patientGalleryId;

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