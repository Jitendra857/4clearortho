using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.Utility;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using _4eOrtho.DAL;

namespace CMS.Web.Scripts.HTMLEditor
{
    public partial class Image : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            int width = 0;
            int height = 0;
            try
            {
                //TODO: PostImageWithPermissionCheck
                int fileId = 0;
                if (!string.IsNullOrEmpty(CommonLogic.QueryString("id")))
                {
                    fileId = Convert.ToInt32(CommonLogic.QueryString("id"));
                }

                if (!string.IsNullOrEmpty(CommonLogic.QueryString("width")))
                {
                    width = Convert.ToInt32(CommonLogic.QueryString("width"));
                }

                if (!string.IsNullOrEmpty(CommonLogic.QueryString("height")))
                {
                    height = Convert.ToInt32(CommonLogic.QueryString("height"));
                }                

                FileManagement fileManagement = new FileManagementEntity().GetFileManagementByID(fileId);
                if (fileManagement != null && fileManagement.FileContent.Length > 0)
                {
                    byte[] FileContent = fileManagement.FileContent;

                    if (width > 0 && height > 0)
                    {
                        System.Drawing.Imaging.ImageFormat imageFormat;
                        FileContent = BitmapToBytes(ResizeImage(BytesToBitmap(fileManagement.FileContent, out imageFormat), width, height, true), imageFormat);
                    }

                    Response.ClearContent();
                    Response.ContentType = fileManagement.FileContentType;
                    Response.BinaryWrite(FileContent);
                }
                else
                {
                    string noImageName = string.Empty;
                    if (!string.IsNullOrEmpty(CommonLogic.QueryString("noImageName")))
                    {
                        noImageName = CommonLogic.QueryString("noImageName");
                    }
                    if (!string.IsNullOrEmpty(noImageName))
                    {
                        string noImagePath = Server.MapPath("../../Images/" + noImageName);
                        if (File.Exists(noImagePath))
                        {


                            byte[] FileContent = File.ReadAllBytes(noImagePath);
                            if (width > 0 && height > 0)
                            {
                                System.Drawing.Imaging.ImageFormat imageFormat;
                                FileContent = BitmapToBytes(ResizeImage(BytesToBitmap(FileContent, out imageFormat), width, height, true), imageFormat);
                            }

                            Response.ClearContent();
                            Response.ContentType = GetMimeType(noImagePath);
                            Response.BinaryWrite(FileContent);
                        }
                    }
                }
            }
            catch
            {

            }

        }

        public static Bitmap BytesToBitmap(byte[] byteArray, out System.Drawing.Imaging.ImageFormat imageFormat)
        {
            imageFormat = null;
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

                if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                {
                    imageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
                }
                else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
                {
                    imageFormat = System.Drawing.Imaging.ImageFormat.Gif;
                }
                else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                {
                    imageFormat = System.Drawing.Imaging.ImageFormat.Png;
                }
                else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                {
                    imageFormat = System.Drawing.Imaging.ImageFormat.Bmp;
                }
                else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff))
                {
                    imageFormat = System.Drawing.Imaging.ImageFormat.Tiff;
                }
                else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Icon))
                {
                    imageFormat = System.Drawing.Imaging.ImageFormat.Icon;
                }

                Bitmap bitmapImg = (Bitmap)image;
                return bitmapImg;
            }
        }

        public static byte[] BitmapToBytes(Bitmap bitmap, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                if (imageFormat == null)
                {
                    imageFormat = System.Drawing.Imaging.ImageFormat.Png;
                }
                bitmap.Save(ms, imageFormat);
                return ms.ToArray();
            }
        }


        public Bitmap ResizeImage(Bitmap lcFile, Int32 lnWidth, Int32 lnHeight, bool IsThumbWithRatio)
        {
            System.Drawing.Bitmap bmpOut = null;

            try
            {
                Bitmap loBMP = lcFile;
                ImageFormat loFormat = loBMP.RawFormat;

                //*** If the image is smaller than a thumbnail just return it
                if (loBMP.Width < lnWidth && loBMP.Height < lnHeight)
                {
                    Bitmap bmp = new Bitmap(loBMP, new System.Drawing.Size(loBMP.Width, loBMP.Height));
                    loBMP.Dispose();
                    loBMP = bmp;
                    return loBMP;
                }

                Decimal lnRatio;
                Int32 lnNewWidth = 0;
                Int32 lnNewHeight = 0;

                if (IsThumbWithRatio == true)
                {
                    if (loBMP.Width > loBMP.Height)
                    {
                        lnRatio = (Decimal)lnWidth / loBMP.Width;
                        lnNewWidth = lnWidth;
                        Decimal lnTemp = loBMP.Height * lnRatio;
                        lnNewHeight = (Int32)lnTemp;
                    }
                    else
                    {
                        lnRatio = (Decimal)lnHeight / loBMP.Height;
                        lnNewHeight = lnHeight;
                        Decimal lnTemp = loBMP.Width * lnRatio;
                        lnNewWidth = (Int32)lnTemp;
                    }
                }
                else
                {
                    lnNewWidth = lnWidth;
                    lnNewHeight = lnHeight;
                }
                // System.Drawing.Image imgOut =
                //      loBMP.GetThumbnailImage(lnNewWidth,lnNewHeight,
                //                              null,IntPtr.Zero);
                // *** This code creates cleaner (though bigger) thumbnails and properly
                // *** and handles GIF files better by generating a white background for
                // *** transparent images (as opposed to black)

                bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
                Graphics g = Graphics.FromImage(bmpOut);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);
                loBMP.Dispose();
            }
            catch
            {
                return null;
            }
            return bmpOut;
        }

        private string GetMimeType(string filePath)
        {
            string fileExtension = System.IO.Path.GetExtension(filePath).ToLower();
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(fileExtension);
            if (rk != null && rk.GetValue("Content Type") != null)
                return rk.GetValue("Content Type").ToString();
            return null;
        }
    }
}
