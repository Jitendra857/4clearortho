using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Web;
using System.Web.UI;

namespace _4eOrtho
{
    public partial class PhotoUpload : PageBase
    {
        #region Declaration
        string photographName = "";
        string path = HttpContext.Current.Request.PhysicalApplicationPath + "Photograph\\";
        private ILog logger = log4net.LogManager.GetLogger(typeof(PhotoUpload));
        #endregion

        #region Event

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
                    if (CommonLogic.QueryString("pid") != null && CommonLogic.QueryString("pid") != "")
                        hdPatientId.Value = CommonLogic.QueryString("pid");
                    else if (CommonLogic.QueryString("did") != null && CommonLogic.QueryString("did") != "")
                        hdDoctorId.Value = CommonLogic.QueryString("did");
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
        /// save photo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //if ((hdPatientId.Value != null && hdPatientId.Value != ""))
                //{
                SavePhotoGraph();
                //}                
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        #endregion

        #region Helper

        /// <summary>
        /// save photo details
        /// </summary>
        private void SavePhotoGraph()
        {
            lblMessage.Text = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;

            if (fuploadPhoto.HasFile)
            {
                photographName = fuploadPhoto.FileName;
                String FileExtension = Path.GetExtension(photographName).ToLower();
                Guid newName = Guid.NewGuid();
                string[] phName = photographName.Split('.');
                string newPhotoName = phName[0].ToString() + "_" + newName + "." + phName[1].ToString();
                string newPhotoPath = path + newPhotoName;

                Stream strm = fuploadPhoto.PostedFile.InputStream;
                GenerateThumbnails(0.5, strm, newPhotoPath);

                //fuploadPhoto.PostedFile.SaveAs(newPhotoPath);
                String[] allowedExtensions = { ".png", ".bmp", ".jpg", ".gif", ".jpeg" };

                FileInfo finfo = new FileInfo(newPhotoPath);
                long fileInBytes = finfo.Length;
                double fileInKB = finfo.Length / 1024;
                double f = 0;
                f = fileInKB / 1024;

                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (FileExtension == allowedExtensions[i])
                    {
                        FileOK = true;
                        break;
                    }
                }

                if (FileOK)
                {
                    if (f <= 2.00)
                    {
                        FileSaved = true;
                    }
                    else
                    {
                        lblMessage.Text = "File size can't be more than 2MB !";
                        File.Delete(newPhotoPath);
                    }
                }
                else
                {
                    lblMessage.Text = "File format is wrong !";
                    File.Delete(newPhotoPath);
                }

                //if (FileSaved && !string.IsNullOrEmpty(hdPatientId.Value))
                //{
                //    PatientEntity patientEntity = new PatientEntity(SessionHelper.ConnectionString);
                //    EMR_Patient patient = patientEntity.GetPatientById(Convert.ToInt32(hdPatientId.Value));

                //    if (patient.PhotographName != null && patient.PhotographName != "")
                //    {
                //        string filepath = Server.MapPath("Photograph/");
                //        string filename = filepath + patient.PhotographName;
                //        FileInfo fileInfo = new FileInfo(filename);

                //        if (fileInfo.Exists)
                //        {
                //            File.Delete(filename);
                //        }
                //    }

                //    patient.PhotographName = newPhotoName;
                //    patient.LastUpdatedBy = Authentication.GetLoggedUserID();
                //    //patient.LastUpdatedDate = BaseEntity.GetServerDateTime;
                //    patientEntity.Save(patient);
                //    logger.Info("Photo uploaded successfully.");
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Close Photo Upload", "closePhotoUpload('" + Cryptography.EncryptStringAES(hdPatientId.Value, CommonLogic.GetConfigValue("sharedSecret")) + "')", true);
                //}
                //else 
                if (FileSaved && !string.IsNullOrEmpty(hdDoctorId.Value))
                {
                    DoctorEntity docotrEntity = new DoctorEntity();
                    Doctor doctor = docotrEntity.GetDoctorByUserId(Convert.ToInt32(hdDoctorId.Value));

                    if (doctor.PhotographName != null && doctor.PhotographName != "")
                    {
                        string filepath = Server.MapPath("Photograph/");
                        string filename = filepath + doctor.PhotographName;
                        FileInfo fileInfo = new FileInfo(filename);

                        if (fileInfo.Exists)
                        {
                            File.Delete(filename);
                        }
                    }

                    doctor.PhotographName = newPhotoName;
                    doctor.LastUpdatedBy = Authentication.GetLoggedUserID();
                    //patient.LastUpdatedDate = BaseEntity.GetServerDateTime;
                    docotrEntity.Save(doctor);
                    logger.Info("Photo uploaded successfully.");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Close Photo Upload", "closeDoctorPhotoUpload('" + newPhotoName + "')", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Close Photo Upload", "closeDoctorPhotoUpload('" + newPhotoName + "')", true);
                }
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