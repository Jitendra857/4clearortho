using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using AjaxControlToolkit;
using log4net;
namespace _4eOrtho.Admin
{
    public partial class AddEditProductMaster : PageBase
    {
        #region Declaration
        long productId = 0;
        string photographName = "";
        string path = HttpContext.Current.Request.PhysicalApplicationPath + "ProductFiles\\";
        ProductMasterEntity productMasterEntity;
        ProductMaster productMaster;
        private ILog logger = log4net.LogManager.GetLogger(typeof(AddEditProductMaster));
        List<ProductGalleryFiles> productGalleryFiles;
        ProductGalleryEntity productGalleryEntity;
        ProductGallery productGallery;
        List<ProductGallery> productGalleries;
        public string newFileName = string.Empty;
        public static bool isfirsttimeload = true;
        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(CommonLogic.GetSessionValue("ProductId")))
                    productId = Convert.ToInt64(CommonLogic.GetSessionValue("ProductId"));
                if (!Page.IsPostBack & !AjaxFileUpload1.IsInFileUploadPostBack)
                {
                    if (!AjaxFileUpload1.IsInFileUploadPostBack && string.IsNullOrEmpty(Request.QueryString["preview"]))
                    {
                        Session["SessionProductFiles"] = null;
                    }                        
                    if (productId > 0)
                    {
                        if (Request.QueryString["preview"] != "1")
                            BindData();
                        //lblHeader.Visible = false;
                        //lblHeaderEdit.Visible = true;
                        lblHeader.Text = this.GetLocalResourceObject("lblHeaderResource2").ToString();
                        Page.Title = this.GetLocalResourceObject("PageResource2").ToString();
                    }
                    else
                    {
                        lblHeader.Text = this.GetLocalResourceObject("lblHeaderResource1.Text").ToString();
                    }
                }
                if (AjaxFileUpload1.IsInFileUploadPostBack)
                {
                    // do for ajax file upload partial postback request
                }
                else
                {
                    if (Request.QueryString["preview"] == "1" && !string.IsNullOrEmpty(Request.QueryString["fileId"]))
                    {
                        var fileId = Request.QueryString["fileId"];
                        string fileContentType = null;
                        byte[] fileContents = null;


                        fileContents = (byte[])Session["fileContents_" + fileId];
                        fileContentType = (string)Session["fileContentType_" + fileId];


                        if (fileContents != null)
                        {
                            Response.Clear();
                            Response.ContentType = fileContentType;
                            Response.BinaryWrite(fileContents);
                            Response.End();
                        }

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
        /// when back redirect to list product master
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Session["SessionProductFiles"] = null;
                Session["ProductId"] = null;
                Response.Redirect("~/Admin/ListProductMaster.aspx", false);
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// Save Prodcut Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (Session["SessionProductFiles"] != null && ((List<ProductGalleryFiles>)Session["SessionProductFiles"]).Count > 0)
                    {
                        productMasterEntity = new ProductMasterEntity();

                        if (productId > 0)
                        {
                            productMaster = productMasterEntity.GetProductByProductId(productId);
                            productMaster.LastUpdatedBy = Authentication.GetLoggedUserID();
                            productMaster.LastUpdatedDate = BaseEntity.GetServerDateTime;

                        }
                        else
                        {
                            productMaster = productMasterEntity.Create();
                            productMaster.CreatedBy = Authentication.GetLoggedUserID();
                            productMaster.CreatedDate = BaseEntity.GetServerDateTime;
                        }
                        productMaster.ProductName = txtProductName.Text.Trim().ToString();
                        productMaster.Amount = Convert.ToDecimal(txtAmount.Text);
                        productMaster.ProductDescription = txtDescription.Text;
                        productMaster.IsActive = chkIsActive.Checked;
                        productId = productMasterEntity.Save(productMaster);


                        productGalleryFiles = new List<ProductGalleryFiles>();
                        productGalleryFiles = (List<ProductGalleryFiles>)Session["SessionProductFiles"];
                        //before add delete previous files of galleryid
                        productGalleryEntity = new ProductGalleryEntity();
                        productGalleryEntity.RemoveGalleryIdFiles(productId);

                        foreach (ProductGalleryFiles files in productGalleryFiles)
                        {
                            newFileName = UploadFile(files.fileContent, files.fileName);
                            productGallery = productGalleryEntity.Create();
                            productGallery.CreatedBy = Authentication.GetLoggedUserID();
                            productGallery.CreatedDate = BaseEntity.GetServerDateTime;
                            productGallery.ProductId = productId;
                            productGallery.FileName = newFileName;
                            productGallery.IsActive = true;
                            productGalleryEntity.Save(productGallery);
                        }

                        Session["SessionProductFiles"] = null;
                        Session["ProductId"] = null;

                        if (productId > 0)
                        {
                            CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("Productupdatesuccessfully").ToString(), divMsg, lblMsg);
                        }
                        else
                        {
                            CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("Productsavesuccessfully").ToString(), divMsg, lblMsg);
                        }
                        Response.Redirect("ListProductMaster.aspx", false);
                    }
                    else
                    {
                        CommonHelper.ShowMessage(MessageType.Error, this.GetLocalResourceObject("SavePatientGallery").ToString(), divMsg, lblMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// File upload using ajax controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="file"></param>
        protected void File_Upload(object sender, AjaxFileUploadEventArgs file)
        {
            // System.OutOfMemoryException will thrown if file is too big to be read.
            if (file.FileSize <= 1024 * 1024 * 4)
            {
                if (Session["SessionProductFiles"] == null)
                {
                    productGalleryFiles = new List<ProductGalleryFiles>();
                }
                else
                {
                    productGalleryFiles = (List<ProductGalleryFiles>)Session["SessionProductFiles"];
                }
                Session["fileContentType_" + file.FileId] = file.ContentType;
                Session["fileContents_" + file.FileId] = file.GetContents();
                productGalleryFiles.Add(new ProductGalleryFiles { fileContent = file.GetContents(), fileName = file.FileName, IsNewUploaded = true, PreviewUrl = string.Format("?preview=1&fileId={0}", file.FileId) });
                Session["SessionProductFiles"] = productGalleryFiles;
                // Set PostedUrl to preview the uploaded file.         
                file.PostedUrl = string.Format("?preview=1&fileId={0}", file.FileId);
            }
            else
            {
                file.PostedUrl = "../Content/images/fileTooBig.gif";
            }

        }

        /// <summary>
        /// Clear Product files and reset all values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Session["SessionProductFiles"] = null;
            Response.Redirect("~/Admin/AddEditProductMaster.aspx", false);
        }

        /// <summary>
        /// used to check unique product name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void custxtProductName_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            try
            {
                productMasterEntity = new ProductMasterEntity();
                if (productMasterEntity.IsDuplicateProduct(e.Value, productId))
                {
                    e.IsValid = false;
                    Session["SessionProductFiles"] = null;
                }
                else
                {
                    e.IsValid = true;
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
        /// used to fill controls on basis of productId selected from list page
        /// </summary>
        private void BindData()
        {
            try
            {
                productMasterEntity = new ProductMasterEntity();
                productMaster = productMasterEntity.GetProductByProductId(productId);
                productGalleryEntity = new ProductGalleryEntity();
                productGalleries = productGalleryEntity.GetProductGalleriesByProductId(productId);
                StringBuilder menuHTML = new StringBuilder();
                if (productMaster != null)
                {
                    txtProductName.Text = productMaster.ProductName;
                    List<ProductGalleryFiles> lstGalleryFiles = new List<ProductGalleryFiles>();

                    byte[] content = null;
                    foreach (ProductGallery productGallery in productGalleries)
                    {
                        string filePath = "~/ProductFiles/" + productGallery.FileName;

                        menuHTML.AppendLine(@"<div style='padding: 4px;'>");
                        //menuHTML.AppendLine(@"<div>" + productGallery.FileName + "</div>");
                        menuHTML.AppendLine(@"<a class='example-image-link' title='" + this.GetLocalResourceObject("ViewFullImage") + "' href='../ProductFiles/" + productGallery.FileName + "' data-lightbox='example-1'><img class='example-image' style='width: 85px; height: 85px;' src='../ProductFiles/" + productGallery.FileName + "'></a>");
                        menuHTML.AppendLine("<div style='text-align:center;'><span><a><img src='Images/delete.png' alt='Image' onclick=\"DeletePicture('" + productGallery.FileName + "')\" /></a></span></div></div>");
                        content = ImageToByteArray(Server.MapPath(filePath));
                        lstGalleryFiles.Add(new ProductGalleryFiles { fileContent = content, fileName = productGallery.FileName, IsNewUploaded = false, PreviewUrl = string.Empty });
                    }
                    Session["SessionProductFiles"] = lstGalleryFiles;
                    fileList.InnerHtml = menuHTML.ToString();
                    txtAmount.Text = Convert.ToString(productMaster.Amount);
                    txtDescription.Text = productMaster.ProductDescription;
                    chkIsActive.Checked = productMaster.IsActive;

                }
                else
                {
                    CommonHelper.ShowMessage(MessageType.Error, "URL was hampered", divMsg, lblMsg);
                    btnSubmit.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// image byte to file name
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
                CreateResizedCopy(newPhotoPath, "~/ProductFiles/thumbs/", newPhotoName, 200);
                if (File.Exists(newPhotoPath))
                    File.Copy(newPhotoPath, Server.MapPath("~/ProductFiles/slides/" + newPhotoName), true);
                return newPhotoName;

            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return string.Empty;
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
        /// Convert Byte to Image
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
        /// Convert Image path to byte
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
        /// Convert Image to Byte array
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private static byte[] ImageToByteArraybyMemoryStream(System.Drawing.Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }
        #endregion

        protected void btnRemovePicture_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdnPictureName.Value))
                {
                    List<ProductGalleryFiles> RemovepackageFiles = (List<ProductGalleryFiles>)Session["SessionProductFiles"];
                    if (RemovepackageFiles != null && RemovepackageFiles.Count > 0)
                    {
                        RemovepackageFiles.Remove(RemovepackageFiles.Where(x => x.fileName == hdnPictureName.Value).FirstOrDefault());
                        Session["SessionProductFiles"] = RemovepackageFiles;
                    }
                    else
                    {
                        logger.Error("RemovepackageFiles is null  : ");
                    }                    
                    StringBuilder menuHTML = new StringBuilder();
                    foreach (ProductGalleryFiles packageGallery in RemovepackageFiles)
                    {
                        string filePath = "~/ProductFiles/" + packageGallery.fileName;
                        menuHTML.AppendLine(@"<div style='padding: 4px;display:inline-block;'>");
                        if (packageGallery.IsNewUploaded)
                        {
                            menuHTML.AppendLine(@"<a class='example-image-link' title='" + this.GetLocalResourceObject("ViewFullImage") + "' href='" + packageGallery.PreviewUrl + "' data-lightbox='example-1'><img class='example-image' style='width: 85px; height: 85px;' src='" + packageGallery.PreviewUrl + "'></a>");
                        }
                        else
                        {
                            menuHTML.AppendLine(@"<a class='example-image-link' title='" + this.GetLocalResourceObject("ViewFullImage") + "' href='../ProductFiles/" + packageGallery.fileName + "' data-lightbox='example-1'><img class='example-image' style='width: 85px; height: 85px;' src='../ProductFiles/" + packageGallery.fileName + "'></a>");
                        }
                        menuHTML.AppendLine("<div style='text-align:center;'><span><a><img src='Images/delete.png' alt='Image' onclick=\"DeletePicture('" + packageGallery.fileName + "')\" /></a></span></div></div>");
                    }
                    logger.Error("menuHTML is : " + menuHTML);
                    fileList.InnerHtml = menuHTML.ToString();
                    hdnPictureName.Value = string.Empty;
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

    }
}