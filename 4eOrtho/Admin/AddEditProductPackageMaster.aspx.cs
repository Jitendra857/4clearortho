using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System.Web.UI.HtmlControls;
using System.Transactions;
using AjaxControlToolkit;
using System.IO;
using System.Text;
namespace _4eOrtho.Admin
{
    public partial class AddEditProductPackageMaster : PageBase
    {

        #region Declaration
        [Serializable]
        public class ProductRepeater
        {
            public Guid RowId { get; set; }
            public string ProductName { get; set; }
            public long ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal TotalAmount { get; set; }
        }
        long packageId = 0;
        ProductPackageMasterEntity productPackageMasterEntity;
        PackageMasterEntity packageMasterEntity;
        ProductMasterEntity productMasterEntity;
        ProductPackageMaster productPackageMaster;
        PackageMaster packageMaster;
        ProductMaster productMaster;
        List<ProductRepeater> lstRepeater;
        List<PackageGalleryFiles> packageFiles;
        PackageGalleryEntity packageGalleryEntity;
        PackageGallery packageGallery;
        List<PackageGallery> lstPackageGallery;
        private ILog logger = log4net.LogManager.GetLogger(typeof(AddEditProductPackageMaster));
        string photographName = "";
        string path = HttpContext.Current.Request.PhysicalApplicationPath + "Files\\";
        public string newFileName = string.Empty;
        private static decimal amount = 0;
        #endregion

        #region Events
        /// <summary>
        /// Bind the data when page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(CommonLogic.GetSessionValue("PackageId")))
                    packageId = Convert.ToInt64(CommonLogic.GetSessionValue("PackageId"));
                if (!Page.IsPostBack & !AjaxFileUpload1.IsInFileUploadPostBack)
                {
                    if (!AjaxFileUpload1.IsInFileUploadPostBack && string.IsNullOrEmpty(Request.QueryString["preview"]))
                        Session["PackageFiles"] = null;
                    BindProductList();
                    if (packageId > 0)
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
                if (ViewState["ProductDetails"] != null)
                {
                    lstRepeater = (List<ProductRepeater>)ViewState["ProductDetails"];
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
        /// back click with clear session of packagefiles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Session["PackageFiles"] = null;
                Session["PackageId"] = null;
                Response.Redirect("~/Admin/ListProductPackageMaster.aspx");
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Session["PackageFiles"] = null;
                Response.Redirect("~/Admin/AddEditProductPackageMaster.aspx");
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
        }
        /// <summary>
        /// Save Product Pacakge Master
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["PackageFiles"] != null)
                {

                    if (lstRepeater.Count > 1)
                    {

                        TransactionScope scope = new TransactionScope();
                        using (scope)
                        {
                            packageMasterEntity = new PackageMasterEntity();
                            productPackageMasterEntity = new ProductPackageMasterEntity();

                            if (packageId > 0)
                            {
                                packageMaster = packageMasterEntity.GetPackageByPackageId(packageId);
                                packageMaster.LastUpdatedBy = Authentication.GetLoggedUserID();
                                packageMaster.LastUpdatedDate = BaseEntity.GetServerDateTime;
                                //Delete all package in edit mode
                                productPackageMasterEntity.DeleteAllProductPackage(packageId);
                            }
                            else
                            {
                                packageMaster = packageMasterEntity.Create();
                                packageMaster.CreatedBy = Authentication.GetLoggedUserID();
                                packageMaster.CreatedDate = BaseEntity.GetServerDateTime;
                            }
                            packageMaster.PackageName = txtPackageName.Text.Trim().ToString();
                            packageMaster.IsActive = chkIsActive.Checked;
                            packageMaster.IsCase = rbtnForCase.Checked;
                            packageMaster.Amount = Convert.ToDecimal(txtAmount.Text);
                            packageMaster.PackageDescription = txtDescription.Text;
                            packageId = packageMasterEntity.Save(packageMaster);

                            foreach (ProductRepeater items in lstRepeater)
                            {
                                productPackageMaster = productPackageMasterEntity.Create();
                                productPackageMaster.PackageId = packageId;
                                productPackageMaster.ProductId = items.ProductId;
                                productPackageMaster.Quantity = items.Quantity;
                                productPackageMaster.IsActive = true;
                                productPackageMasterEntity.Save(productPackageMaster);
                            }
                            packageFiles = new List<PackageGalleryFiles>();
                            packageFiles = (List<PackageGalleryFiles>)Session["PackageFiles"];

                            packageGalleryEntity = new PackageGalleryEntity();
                            packageGalleryEntity.RemoveGalleryIdFiles(packageId);

                            foreach (PackageGalleryFiles files in packageFiles)
                            {
                                newFileName = UploadFile(files.fileContent, files.fileName);
                                packageGallery = packageGalleryEntity.Create();
                                packageGallery.CreatedBy = Authentication.GetLoggedUserID();
                                packageGallery.CreatedDate = BaseEntity.GetServerDateTime;
                                packageGallery.PackageId = packageId;
                                packageGallery.FileName = newFileName;
                                packageGallery.IsActive = true;
                                packageGalleryEntity.Save(packageGallery);
                            }
                            if (packageId > 0)
                            {
                                CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("ProductPackageUpdate").ToString(), divMsg, lblMsg);
                            }
                            else
                            {
                                CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("ProductPackageSave").ToString(), divMsg, lblMsg);
                            }
                            Session["PackageFiles"] = null;
                            Session["GalleryId"] = null;
                            Response.Redirect("~/Admin/ListProductPackageMaster.aspx", false);
                            scope.Complete();
                        }
                    }
                    else
                    {
                        CommonHelper.ShowMessage(MessageType.Error, "Please insert more than one product to make package.", divMsg, lblMsg);
                    }
                }
                else
                {
                    CommonHelper.ShowMessage(MessageType.Error, this.GetLocalResourceObject("SavePatientGallery").ToString(), divMsg, lblMsg);
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
        /// Add Product to repeater from Viewstate list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (lstRepeater == null)
                    {
                        lstRepeater = new List<ProductRepeater>();
                    }


                    productMaster = new ProductMasterEntity().GetProductByProductId(Convert.ToInt64(ddlProductName.SelectedValue.ToString()));
                    amount = txtAmount.Text == "" ? 0 : Convert.ToDecimal(txtAmount.Text);
                    amount = amount + productMaster.Amount * Convert.ToInt32(txtQuantity.Text);
                    txtAmount.Text = Convert.ToString(amount);

                    ProductRepeater repeater = new ProductRepeater();
                    repeater.RowId = Guid.NewGuid();
                    repeater.ProductName = ddlProductName.SelectedItem.Text;
                    repeater.ProductId = Convert.ToInt32(ddlProductName.SelectedValue.ToString());
                    repeater.Quantity = Convert.ToInt32(txtQuantity.Text);
                    repeater.TotalAmount = Convert.ToInt32(productMaster.Amount) * Convert.ToInt32(txtQuantity.Text);
                    lstRepeater.Add(repeater);
                    ViewState["ProductDetails"] = lstRepeater;
                    rptrProductDetails.Visible = true;
                    rptrProductDetails.DataSource = lstRepeater;
                    rptrProductDetails.DataBind();
                    txtQuantity.Text = "";
                    ddlProductName.SelectedIndex = 0;
                    ddlProductName.Focus();

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
        /// used to check unique package name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void custxtPackageName_ServerValidate(object source, ServerValidateEventArgs e)
        {
            try
            {
                packageMasterEntity = new PackageMasterEntity();
                if (packageMasterEntity.IsDuplicatePackage(e.Value, packageId))
                {
                    e.IsValid = false;
                    Session["PackageFiles"] = null;
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
        /// <summary>
        /// used to check unique name of product 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void custxtProductName_ServerValidate(object source, ServerValidateEventArgs e)
        {
            try
            {
                int productId = Convert.ToInt32(e.Value);
                if (lstRepeater != null && lstRepeater.Count > 0)
                {
                    if (lstRepeater.Where(x => x.ProductId == productId).ToList().Count > 0)
                    {
                        e.IsValid = false;
                    }
                    else
                    {
                        e.IsValid = true;
                    }
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
        /// <summary>
        /// When there is no data, label will display
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptrProductDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (rptrProductDetails.Items.Count < 1)
                {
                    if (e.Item.ItemType == ListItemType.Footer)
                    {
                        HtmlTableRow row = (HtmlTableRow)e.Item.FindControl("trNoData");
                        row.Visible = true;
                        rptrProductDetails.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// delete when item command fired
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptrProductDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() == "delete")
                {
                    ProductRepeater productRepeater = new ProductRepeater();
                    string rowId = Convert.ToString(e.CommandArgument);
                    productRepeater = lstRepeater.Where(x => x.RowId.ToString() == rowId).SingleOrDefault();
                    lstRepeater.Remove(productRepeater);

                    rptrProductDetails.DataSource = lstRepeater;
                    rptrProductDetails.DataBind();

                    productMaster = new ProductMasterEntity().GetProductByProductId(Convert.ToInt64(productRepeater.ProductId));
                    amount = txtAmount.Text == "" ? 0 : Convert.ToDecimal(txtAmount.Text);
                    amount = amount - productMaster.Amount * Convert.ToInt32(productRepeater.Quantity);
                    txtAmount.Text = Convert.ToString(amount);
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
        /// <summary>
        /// file upload to physical location with ajax file upload control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="file"></param>
        protected void File_Upload(object sender, AjaxFileUploadEventArgs file)
        {
            // System.OutOfMemoryException will thrown if file is too big to be read.
            if (file.FileSize <= 1024 * 1024 * 4)
            {
                if (Session["PackageFiles"] == null)
                {
                    packageFiles = new List<PackageGalleryFiles>();
                }
                else
                {
                    packageFiles = (List<PackageGalleryFiles>)Session["PackageFiles"];
                }
                Session["fileContentType_" + file.FileId] = file.ContentType;
                Session["fileContents_" + file.FileId] = file.GetContents();
                packageFiles.Add(new PackageGalleryFiles { fileContent = file.GetContents(), fileName = file.FileName, IsNewUploaded = true, PreviewUrl = string.Format("?preview=1&fileId={0}", file.FileId) });
                Session["PackageFiles"] = packageFiles;
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
        /// Bind product list
        /// </summary>
        private void BindProductList()
        {
            try
            {
                productMasterEntity = new ProductMasterEntity();
                List<ProductMaster> lstProductMaster = new List<ProductMaster>();
                lstProductMaster = productMasterEntity.GetProductMasters();
                ddlProductName.DataSource = lstProductMaster;
                ddlProductName.DataBind();
                ddlProductName.Items.Insert(0, new ListItem(this.GetLocalResourceObject("SelectProduct").ToString(), "0"));
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
        /// used to fill controls on basis of packageId selected from list page
        /// </summary>
        private void BindData()
        {
            try
            {
                packageMasterEntity = new PackageMasterEntity();
                packageMaster = packageMasterEntity.GetPackageByPackageId(packageId);

                productPackageMasterEntity = new ProductPackageMasterEntity();
                List<ProductPackageDetails> lstProductPackageDetails = new List<ProductPackageDetails>();
                lstProductPackageDetails = productPackageMasterEntity.GetProductPackageDetailsByPackageId(packageId);

                if (packageMaster != null)
                {
                    txtPackageName.Text = packageMaster.PackageName;
                    txtAmount.Text = Convert.ToString(packageMaster.Amount);
                    txtDescription.Text = packageMaster.PackageDescription;
                    chkIsActive.Checked = packageMaster.IsActive;
                    rbtnForCase.Checked = packageMaster.IsCase;
                    rbtnForAll.Checked = !packageMaster.IsCase;

                    foreach (ProductPackageDetails item in lstProductPackageDetails)
                    {
                        if (lstRepeater == null)
                        {
                            lstRepeater = new List<ProductRepeater>();
                        }
                        ProductRepeater productRepeater = new ProductRepeater();
                        productRepeater.ProductId = item.ProductId;
                        productRepeater.ProductName = item.ProductName;
                        productRepeater.Quantity = item.Quantity;
                        productRepeater.TotalAmount = item.Amount;
                        productRepeater.RowId = (Guid)item.RowId;
                        lstRepeater.Add(productRepeater);
                    }
                    ViewState["ProductDetails"] = lstRepeater;
                    rptrProductDetails.Visible = true;
                    rptrProductDetails.DataSource = lstRepeater;
                    rptrProductDetails.DataBind();

                    packageGalleryEntity = new PackageGalleryEntity();
                    lstPackageGallery = packageGalleryEntity.GetPackageGalleriesByPackageId(packageId);
                    packageFiles = new List<PackageGalleryFiles>();
                    StringBuilder menuHTML = new StringBuilder();
                    byte[] content = null;
                    foreach (PackageGallery packageGallery in lstPackageGallery)
                    {
                        string filePath = "~/Files/" + packageGallery.FileName;

                        menuHTML.AppendLine(@"<div style='padding: 4px;display:inline-block;'>");
                        //menuHTML.AppendLine(@"<div>" + packageGallery.FileName + "</div>");
                        //menuHTML.AppendLine(@"<img style='width: 80px; height: 80px;margin-top: 10px;' src='../Files/" + packageGallery.FileName + "'></div>");
                        menuHTML.AppendLine(@"<a class='example-image-link' title='" + this.GetLocalResourceObject("ViewFullImage") + "' href='../Files/" + packageGallery.FileName + "' data-lightbox='example-1'><img class='example-image' style='width: 85px; height: 85px;' src='../Files/" + packageGallery.FileName + "'></a>");
                        menuHTML.AppendLine("<div style='text-align:center;'><span><a><img src='Images/delete.png' alt='Image' onclick=\"DeletePicture('" + packageGallery.FileName + "')\" /></a></span></div></div>");
                        content = ImageToByteArray(Server.MapPath(filePath));
                        packageFiles.Add(new PackageGalleryFiles { fileContent = content, fileName = packageGallery.FileName, IsNewUploaded = false, PreviewUrl = string.Empty });
                    }
                    Session["PackageFiles"] = packageFiles;
                    fileList.InnerHtml = menuHTML.ToString();
                }
                else
                {
                    CommonHelper.ShowMessage(MessageType.Error, this.GetLocalResourceObject("URLwashampered").ToString(), divMsg, lblMsg);
                    btnSubmit.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }
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
                CreateResizedCopy(newPhotoPath, "~/Files/thumbs/", newPhotoName, 200);
                if (File.Exists(newPhotoPath))
                    File.Copy(newPhotoPath, Server.MapPath("~/Files/slides/" + newPhotoName), true);
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
        public static void ByteArrayToImageFilebyMemoryStream(byte[] imageByte, string imagefilePath)
        {
            MemoryStream ms = new MemoryStream(imageByte);
            //Image image = Image.FromStream(ms);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            img.Save(imagefilePath);
        }
        public static byte[] ImageToByteArray(string imagefilePath)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(imagefilePath);
            byte[] imageByte = ImageToByteArraybyMemoryStream(image);
            return imageByte;
        }
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
                    List<PackageGalleryFiles> RemovepackageFiles = (List<PackageGalleryFiles>)Session["PackageFiles"];
                    if (RemovepackageFiles != null && RemovepackageFiles.Count > 0)
                    {
                        RemovepackageFiles.Remove(RemovepackageFiles.Where(x => x.fileName == hdnPictureName.Value).FirstOrDefault());
                        Session["PackageFiles"] = RemovepackageFiles;
                    }
                    StringBuilder menuHTML = new StringBuilder();
                    foreach (PackageGalleryFiles packageGallery in RemovepackageFiles)
                    {
                        string filePath = "~/Files/" + packageGallery.fileName;
                        menuHTML.AppendLine(@"<div style='padding: 4px;display:inline-block;'>");
                        if (packageGallery.IsNewUploaded)
                        {
                            menuHTML.AppendLine(@"<a class='example-image-link' title='" + this.GetLocalResourceObject("ViewFullImage") + "' href='" + packageGallery.PreviewUrl + "' data-lightbox='example-1'><img class='example-image' style='width: 85px; height: 85px;' src='" + packageGallery.PreviewUrl + "'></a>");
                        }
                        else
                        {
                            menuHTML.AppendLine(@"<a class='example-image-link' title='" + this.GetLocalResourceObject("ViewFullImage") + "' href='../Files/" + packageGallery.fileName + "' data-lightbox='example-1'><img class='example-image' style='width: 85px; height: 85px;' src='../Files/" + packageGallery.fileName + "'></a>");
                        }
                        menuHTML.AppendLine("<div style='text-align:center;'><span><a><img src='Images/delete.png' alt='Image' onclick=\"DeletePicture('" + packageGallery.fileName + "')\" /></a></span></div></div>");
                    }
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