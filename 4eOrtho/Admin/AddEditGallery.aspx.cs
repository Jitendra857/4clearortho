using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using AjaxControlToolkit;
using log4net;
using System.Drawing.Drawing2D;

namespace _4eOrtho.Admin
{
    public partial class AddEditGallery : PageBase
    {
        #region Declaration
        public long galleryId = 0;
        GalleryEntity galleryEntity;
        Gallery gallery;
        string photographName = "";
        string path = HttpContext.Current.Request.PhysicalApplicationPath + "Photograph\\";
        private ILog logger = log4net.LogManager.GetLogger(typeof(AddEditGallery));
        List<GalleryFiles> galleryFiles;
        ConditionGalleryEntity conditionGalleryEntity;
        ConditionGallery conditionGallery;
        List<ConditionGallery> conditionGalleries;
        public string newFileName = string.Empty;        
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
                if (!String.IsNullOrEmpty(CommonLogic.GetSessionValue("GalleryId")))
                    galleryId = Convert.ToInt64(CommonLogic.GetSessionValue("GalleryId"));

                if (!IsPostBack)//& !AjaxFileUpload1.IsInFileUploadPostBack)                
                {
                    if (galleryId > 0)
                    {
                        //lblHeader.Visible = false;
                        //lblHeaderEdit.Visible = true;
                        if (Request.QueryString["preview"] != "1")
                            BindGallery();

                        lblHeader.Text = this.GetLocalResourceObject("lblHeaderResource2").ToString();
                        Page.Title = this.GetLocalResourceObject("PageResource2").ToString();
                    }
                    else
                    {
                        lblHeader.Text = this.GetLocalResourceObject("lblHeaderResource1.Text").ToString();
                    }
                }
                //if (AjaxFileUpload1.IsInFileUploadPostBack)
                //{
                //    // do for ajax file upload partial postback request
                //}
                //else
                //{
                //    if (Request.QueryString["preview"] == "1" && !string.IsNullOrEmpty(Request.QueryString["fileId"]))
                //    {
                //        var fileId = Request.QueryString["fileId"];
                //        string fileContentType = null;
                //        byte[] fileContents = null;
                //        fileContents = (byte[])Session["fileContents_" + fileId];
                //        fileContentType = (string)Session["fileContentType_" + fileId];
                //        if (fileContents != null)
                //        {
                //            Response.Clear();
                //            Response.ContentType = fileContentType;
                //            Response.BinaryWrite(fileContents);
                //            Response.End();
                //        }
                //    }
                //}
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
        /// Save Photo Gallary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    SavePhotosGallery();
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
        /// clear gallery files and galleryid  when back 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Session["GalleryFiles"] = null;
                Session["GalleryId"] = null;
                Response.Redirect("~/Admin/ListGallery.aspx", false);
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
        }
        /// <summary>
        /// clear galleryfiles when reset 
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
        }
        /// <summary>
        /// File upload using ajax upload controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="file"></param>
        protected void File_Upload(object sender, AjaxFileUploadEventArgs file)
        {
            // System.OutOfMemoryException will thrown if file is too big to be read.
            if (file.FileSize <= 1024 * 1024 * 4)
            {
                if (Session["GalleryFiles"] == null)
                {
                    galleryFiles = new List<GalleryFiles>();
                }
                else
                {
                    galleryFiles = (List<GalleryFiles>)Session["GalleryFiles"];
                }
                Session["fileContentType_" + file.FileId] = file.ContentType;
                Session["fileContents_" + file.FileId] = file.GetContents();
                galleryFiles.Add(new GalleryFiles { fileContent = file.GetContents(), fileName = file.FileName });
                Session["GalleryFiles"] = galleryFiles;
                // Set PostedUrl to preview the uploaded file.         
                file.PostedUrl = string.Format("?preview=1&fileId={0}", file.FileId);
            }
            else
            {
                file.PostedUrl = "../Content/images/fileTooBig.gif";
            }

        }
        #endregion

        #region Helpers
        /// <summary>
        /// Save photo gallery method
        /// </summary>
        private void SavePhotosGallery()
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
                            CreateResizedCopy(newPhotoPath, "~/Photograph/thumbs/", newPhotoName, 200);
                            lstGalleryFiles.Add(newPhotoName);
                        }
                        else if (lstOldFile != null && lstOldFile.Count > 0)
                        {
                            lstGalleryFiles.Add(lstOldFile[i]);
                        }
                    }
                    catch
                    {
                        //Label1.ForeColor = System.Drawing.Color.Red;
                        //Label1.Text = "Please try again later";
                    }
                }

                try
                {
                    if (lstGalleryFiles != null && lstGalleryFiles.Count > 0)
                    {

                        galleryEntity = new GalleryEntity();

                        if (galleryId > 0)
                        {
                            gallery = galleryEntity.GetGalleryById(galleryId);
                            gallery.LastUpdatedBy = Authentication.GetLoggedUserID();
                            gallery.LastUpdatedDate = BaseEntity.GetServerDateTime;

                        }
                        else
                        {
                            gallery = galleryEntity.Create();
                            gallery.CreatedBy = Authentication.GetLoggedUserID();
                            gallery.CreatedDate = BaseEntity.GetServerDateTime;
                        }

                        //gallery.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                        gallery.Condition = txtCondition.Text;
                        //gallery.FileName = Convert.ToString(ViewState["FileName"]);
                        gallery.IsActive = chkIsActive.Checked;
                        gallery.DoctorEmail = SessionHelper.LoggedAdminEmailAddress;
                        gallery.IsHomeDisplay = chkHomePageDisplay.Checked;
                        galleryId = galleryEntity.Save(gallery);


                        //before add delete previous files of galleryid
                        conditionGalleryEntity = new ConditionGalleryEntity();
                        conditionGalleryEntity.RemoveGalleryIdFiles(galleryId);

                        foreach (string fileName in lstGalleryFiles)
                        {
                            //newFileName = UploadFile(files.fileContent, files.fileName);
                            conditionGallery = conditionGalleryEntity.Create();
                            conditionGallery.CreatedBy = Authentication.GetLoggedUserID();
                            conditionGallery.CreatedDate = BaseEntity.GetServerDateTime;
                            conditionGallery.GalleryId = galleryId;
                            conditionGallery.FileName = fileName;
                            conditionGallery.IsActive = true;
                            conditionGalleryEntity.Save(conditionGallery);
                        }

                        Session["GalleryFiles"] = null;
                        //ddlPatient.SelectedIndex = 0;
                        //imgPatientGallery.ImageUrl = "~/Content/images/noGallery.jpg";
                        Session["GalleryId"] = null;
                        Response.Redirect("~/Admin/ListGallery.aspx", false);
                    }
                    else
                    {
                        CommonHelper.ShowMessage(MessageType.Error, this.GetLocalResourceObject("SavePAtientGallery").ToString(), divMsg, lblMsg);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("An error occured.", ex);
                }
            }
        }
        /// <summary>
        /// Gallery bind when edit
        /// </summary>
        public void BindGallery()
        {
            try
            {
                galleryEntity = new GalleryEntity();
                gallery = galleryEntity.GetGalleryById(galleryId);
                conditionGalleryEntity = new ConditionGalleryEntity();
                conditionGalleries = conditionGalleryEntity.GetConditionGalleriesByGalleryId(galleryId);
                //StringBuilder menuHTML = new StringBuilder();
                if (gallery != null)
                {
                    txtCondition.Text = gallery.Condition;
                    //List<GalleryFiles> lstGalleryFiles = new List<GalleryFiles>();
                    //byte[] content = null;
                    foreach (ConditionGallery conditionGallery in conditionGalleries)
                    {
                        hdnImages.Value += conditionGallery.FileName + ",";
                        //    string filePath = "~/Photograph/" + conditionGallery.FileName;

                        //    menuHTML.AppendLine(@"<div style='padding: 4px; border: 1px solid rgb(211, 211, 211);'>");
                        //    menuHTML.AppendLine(@"<div>" + conditionGallery.FileName + "</div>");
                        //    menuHTML.AppendLine(@"<img style='width: 80px; height: 80px;margin-top: 10px;' src='../Photograph/" + conditionGallery.FileName + "'></div>");
                        //    content = ImageToByteArray(Server.MapPath(filePath));
                        //    lstGalleryFiles.Add(new GalleryFiles { fileContent = content, fileName = conditionGallery.FileName });
                    }
                    //Session["GalleryFiles"] = lstGalleryFiles;
                    //fileList.InnerHtml = menuHTML.ToString();                    
                    chkIsActive.Checked = gallery.IsActive;
                    chkHomePageDisplay.Checked = gallery.IsHomeDisplay;
                    hdnImages.Value = hdnImages.Value.Remove(hdnImages.Value.Length - 1, 1);
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// Photo gallery save
        /// </summary>
        private void SavePhotoGallery()
        {
            try
            {
                if (Session["GalleryFiles"] != null)
                {

                    galleryEntity = new GalleryEntity();

                    if (galleryId > 0)
                    {
                        gallery = galleryEntity.GetGalleryById(galleryId);
                        gallery.LastUpdatedBy = Authentication.GetLoggedUserID();
                        gallery.LastUpdatedDate = BaseEntity.GetServerDateTime;

                    }
                    else
                    {
                        gallery = galleryEntity.Create();
                        gallery.CreatedBy = Authentication.GetLoggedUserID();
                        gallery.CreatedDate = BaseEntity.GetServerDateTime;
                    }
                    galleryFiles = new List<GalleryFiles>();
                    galleryFiles = (List<GalleryFiles>)Session["GalleryFiles"];


                    //gallery.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                    gallery.Condition = txtCondition.Text;
                    //gallery.FileName = Convert.ToString(ViewState["FileName"]);
                    gallery.IsActive = chkIsActive.Checked;
                    gallery.DoctorEmail = SessionHelper.LoggedAdminEmailAddress;
                    gallery.IsHomeDisplay = chkHomePageDisplay.Checked;
                    galleryId = galleryEntity.Save(gallery);


                    //before add delete previous files of galleryid
                    conditionGalleryEntity = new ConditionGalleryEntity();
                    conditionGalleryEntity.RemoveGalleryIdFiles(galleryId);

                    foreach (GalleryFiles files in galleryFiles)
                    {
                        newFileName = UploadFile(files.fileContent, files.fileName);
                        conditionGallery = conditionGalleryEntity.Create();
                        conditionGallery.CreatedBy = Authentication.GetLoggedUserID();
                        conditionGallery.CreatedDate = BaseEntity.GetServerDateTime;
                        conditionGallery.GalleryId = galleryId;
                        conditionGallery.FileName = newFileName;
                        conditionGallery.IsActive = true;
                        conditionGalleryEntity.Save(conditionGallery);
                    }

                    Session["GalleryFiles"] = null;
                    //ddlPatient.SelectedIndex = 0;
                    //imgPatientGallery.ImageUrl = "~/Content/images/noGallery.jpg";
                    Session["GalleryId"] = null;
                    Response.Redirect("~/Admin/ListGallery.aspx", false);
                }
                else
                {
                    CommonHelper.ShowMessage(MessageType.Error, this.GetLocalResourceObject("SavePAtientGallery").ToString(), divMsg, lblMsg);
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
        /// Upload file from byte to image
        /// </summary>
        /// <param name="imageByte"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string UploadFile(byte[] imageByte, string fileName)
        {
            try
            {

                photographName = fileName;
                String FileExtension = Path.GetExtension(photographName).ToLower();
                Guid newName = Guid.NewGuid();
                string[] phName = photographName.Split('.');
                string newPhotoName = newName + "." + phName[phName.Length - 1].ToString();
                string newPhotoPath = path + newPhotoName;
                //fupPictures.PostedFile.SaveAs(newPhotoPath);
                ByteArrayToImageFilebyMemoryStream(imageByte, newPhotoPath);
                CreateResizedCopy(newPhotoPath, "~/Photograph/thumbs/", newPhotoName, 200);
                if (File.Exists(newPhotoPath))
                    File.Copy(newPhotoPath, Server.MapPath("~/Photograph/slides/" + newPhotoName), true);
                return newPhotoName;

            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return string.Empty;
            }
        }
        /// <summary>
        /// Byte to Stream
        /// </summary>
        /// <param name="imageByte"></param>
        /// <param name="imagefilePath"></param>
        public static void ByteArrayToImageFilebyMemoryStream(byte[] imageByte, string imagefilePath)
        {
            MemoryStream ms = new MemoryStream(imageByte);
            //Image image = Image.FromStream(ms);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            img.Save(imagefilePath);
        }
        /// <summary>
        /// Image to byte arreay
        /// </summary>
        /// <param name="imagefilePath"></param>
        /// <returns></returns>
        public static byte[] ImageToByteArray(string imagefilePath)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(imagefilePath);
            byte[] imageByte = ImageToByteArraybyMemoryStream(image);
            return imageByte;
        }
        /// <summary>
        /// Image to Stream
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private static byte[] ImageToByteArraybyMemoryStream(System.Drawing.Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }
        /// <summary>
        /// Convert string to byte array
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
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