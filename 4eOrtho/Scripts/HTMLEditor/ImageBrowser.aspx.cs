
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using VSL.CC.CMS.BAL;
using VSL.CC.CMS.DAL;


namespace CMS.Web.Scripts.HTMLEditor
{
    public partial class ImageBrowser : System.Web.UI.Page
    {
      

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

            try
            {             
          
                ////Get Custom Language
                //string languageName = Com.Comm100.Forum.Language.LanguageHelper.GetLanguageName((Com.Comm100.Language.EnumLanguage)Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["EnumLanguage"]));
                ////Set to cultureInfo
                //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(languageName);
                //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(languageName);
                
                #region Check Image File
                if (this.fileImage.HasFile == false)
                {
                    throw new Exception("Upload your upload file here");
                      //ExceptionHelper.ThrowPostImageFileNotExistException(fileImage.FileName);
                }
                if (this.fileImage.FileBytes.Length > (1024 * 1024 * 20))
                {
                    throw new Exception("The size of your upload file should be less than 2M");
                     //ExceptionHelper.ThrowPostImageFileSizeIsTooLargeException(this.fileImage.FileName);
                }

                string strFileExtension = System.IO.Path.GetExtension(this.fileImage.FileName).ToUpperInvariant();
                if ((strFileExtension != ".GIF") && (strFileExtension != ".JPG") && (strFileExtension != ".PNG") && (strFileExtension != ".JPEG")
                   && (strFileExtension != ".BMP") && (strFileExtension != ".TIFF") && (strFileExtension != ".RAW") && (strFileExtension != ".IMG"))
                {
                    throw new Exception("Your upload file should be in GIF, JPG, JPEG, PNG, or BMP format.");
                    //ExceptionHelper.ThrowPostImageFormatErrorException(this.fileImage.FileName);
                }
                #endregion

                string fileName = this.fileImage.FileName;
                string type = this.fileImage.PostedFile.ContentType;
                byte[] fileContent = this.fileImage.FileBytes;

                /***********************/
                //fileImage.SaveAs(@"G:\CMS\WebApplication1\Images\" + fileImage.FileName);
                //string imageId =  fileImage.FileName;
                /****************************/

                // Insert Image to databse
                FileManagementEntity fileManagementEntity = new FileManagementEntity();
                FileManagement fileManagement = new FileManagement();
                fileManagementEntity.Create();
                fileManagement.FileName = fileName;
                fileManagement.FileContent = fileContent;
                fileManagement.FileSize = fileContent.Length;
                fileManagement.FileContentType = type;
                fileManagementEntity.Save(fileManagement);
                long imageId = fileManagement.FileManagementID;

                string appPath = Request.ApplicationPath;
                if (appPath.Length > 1)
                {
                    appPath += "/";
                }
                //this.txtFileName.Text = "http://" + Request.Url.Authority + appPath + "Images/" + imageId;

                this.txtFileName.Text = Request.Url.Scheme  + "://" + Request.Url.Authority + appPath + "Scripts/HTMLEditor/Image.aspx?id=" + imageId;
                this.phScript.Controls.Add(new LiteralControl("<script language='javascript' type='text/javascript'>cancelUpload(false);</script>"));
            }
            catch (Exception exp)
            {
                string script = string.Format("<script>alert(\"{0}\")</script>", exp.Message.Replace("\r"," ").Replace("\n"," "));
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorScript", script);
            }

        }
    }
}
