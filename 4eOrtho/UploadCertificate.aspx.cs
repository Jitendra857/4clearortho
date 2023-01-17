using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using System.Web.UI.HtmlControls;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using System.Net.Mail;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace _4eOrtho
{
    public partial class UploadCertificate : PageBase
    {
        #region Declaration
        string path = HttpContext.Current.Request.PhysicalApplicationPath + "DoctorCertificate\\";
        private ILog logger = log4net.LogManager.GetLogger(typeof(UploadCertificate));
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
                if (!IsPostBack)
                {
                    if ((CurrentSession)Session["UserLoginSession"] != null)
                    {
                        PageRight pageRight = CheckRights(this.Page.GetType().BaseType.Name);
                        if (pageRight != null)
                        {
                            PageRedirect(pageRight.RedirectPageName);
                        }
                    }

                    BindData();
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
        /// Event to submit certificate details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuUploadCertificate.HasFile)
                {
                    if (fuUploadCertificate.PostedFile != null)
                    {
                        CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                        UserConfigEntity userConfigEntity = new UserConfigEntity();
                        UserConfig userConfig = userConfigEntity.GetUserByEmailAddress(currentSession.EmailId);
                        if (userConfig != null)
                        {
                            userConfig.UpdatedDate = DateTime.Now;
                            //userConfig.UpdatedBy = SessionHelper.LoggedAdminUserID;
                            if (!string.IsNullOrEmpty(userConfig.CertificateFileName))
                            {
                                if (File.Exists(path + userConfig.CertificateFileName))
                                {
                                    File.Delete(path + userConfig.CertificateFileName);
                                }
                            }
                            userConfig.CertificateFileName = SaveImage();
                            userConfig.Comment = txtComment.Text.Trim();
                        }
                        else
                        {
                            userConfig = new UserConfig();
                            //userConfig.CreatedBy = userConfig.UpdatedBy = SessionHelper.LoggedAdminUserID;
                            userConfig.CreatedDate = userConfig.UpdatedDate = DateTime.Now;
                            userConfig.EmailId = currentSession.EmailId;
                            userConfig.CertificateFileName = SaveImage();
                            userConfig.Comment = txtComment.Text.Trim();
                        }
                        userConfigEntity.Save(userConfig);
                        hdnFileName.Value = "DoctorCertificate/" + userConfig.CertificateFileName;
                        SendUploadSuccessEmail(userConfig.CertificateFileName);

                        CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("UploadSuccessMsg").ToString(), divMsg, lblMsg);
                    }
                }
                BindData();                
            }
            catch (Exception ex)
            {
                logger.Error("An error occured" + ex);
                CommonHelper.ShowMessage(MessageType.Error, this.GetLocalResourceObject("FailedMessage").ToString(), divMsg, lblMsg);
            }
        }

        #endregion

        #region Helper

        /// <summary>
        /// Method to get certificate details.
        /// </summary>
        private void BindData()
        {
            CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
            UserConfigEntity userConfigEntity = new UserConfigEntity();
            UserConfig userConfig = userConfigEntity.GetUserByEmailAddress(currentSession.EmailId);
            if (userConfig != null)
            {
                hdnFileName.Value = (!string.IsNullOrEmpty(userConfig.CertificateFileName)) ? "DoctorCertificate/" + userConfig.CertificateFileName : string.Empty;
                txtComment.Text = userConfig.Comment;
            }
        }

        /// <summary>
        /// Method to save image to server.
        /// </summary>
        /// <returns></returns>
        private string SaveImage()
        {
            string photographName = fuUploadCertificate.PostedFile.FileName;
            String FileExtension = Path.GetExtension(photographName).ToLower();
            Guid newName = Guid.NewGuid();
            string[] phName = photographName.Split('.');
            string newPhotoName = phName[0].ToString() + "_" + newName + "." + phName[1].ToString();
            string newPhotoPath = path + newPhotoName;

            if (FileExtension.ToLower() != ".pdf")
            {
                Stream strm = fuUploadCertificate.PostedFile.InputStream;
                GenerateThumbnails(0.5, strm, newPhotoPath);
            }
            else
                fuUploadCertificate.PostedFile.SaveAs(newPhotoPath);

            return newPhotoName;
        }

        /// <summary>
        /// Method to send upload certificate email.
        /// </summary>
        /// <param name="filePath"></param>
        private void SendUploadSuccessEmail(string filePath)
        {
            string emailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("UploadCertificate")).ToString();

            if (File.Exists(emailTemplatePath))
            {
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];

                string filePaths = Server.MapPath("~/DoctorCertificate/" + filePath);
                string emailtemplateHTML = File.ReadAllText(emailTemplatePath);
                emailtemplateHTML = emailtemplateHTML.Replace("##Name##", currentSession.DoctorName);
                emailtemplateHTML = emailtemplateHTML.Replace("##Comment##", txtComment.Text.Trim());
                emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", DateTime.Now.Year.ToString());
                emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));

                MailAddress fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("ToMail"));
                CommonLogic.SendMailWithAttachment(fromMailAddress, fromMailAddress, null, null, emailtemplateHTML, "4ClearOrtho – Certificate Uploaded", filePaths, "Certificate");
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