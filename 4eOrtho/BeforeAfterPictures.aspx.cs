using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;

namespace _4eOrtho
{    
    public partial class BeforeAfterPictures : PageBase
    {
        #region Declaration
        private static ILog logger = log4net.LogManager.GetLogger(typeof(BeforeAfterPictures));
        #endregion

        #region Events
        /// <summary>
        /// Page Pre Init Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserLoginSession"] != null)
                this.Page.MasterPageFile = "~/OrthoInnerMaster.master";
            else
                this.Page.MasterPageFile = "~/Ortho.master";
        }

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

                        hdnDoctorEmailId.Value = ((CurrentSession)Session["UserLoginSession"]).EmailId;
                    }
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
        /// WebMethod to get images paths by Category.
        /// </summary>
        /// <param name="doctoremailid"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<GalleryImageCategory> GetCategoryImagePaths(string doctoremailid)
        {
            try
            {
                List<DoctorGalleryMapping> lstDoctorGallery = null;

                if (!string.IsNullOrEmpty(doctoremailid))
                {
                    lstDoctorGallery = new DoctorGalleryMappingEntity().GetAllDoctorGalleryMappingByEmail(doctoremailid);
                }

                GalleryEntity galleryEntity = new GalleryEntity();
                List<Gallery> lstGallery = new List<Gallery>();
                lstGallery = galleryEntity.GetHomePageGallery().ToList();

                if (lstDoctorGallery != null && lstDoctorGallery.Count > 0)
                {
                    lstGallery = lstGallery.FindAll(x => !lstDoctorGallery.Exists(y => y.GalleryId == x.GalleryId));
                }

                List<GalleryImageCategory> lstImageGallery = new List<GalleryImageCategory>();
                ConditionGalleryEntity conditionGalleryEntity = new ConditionGalleryEntity();
                List<ConditionGallery> lstConditionGallery = new List<ConditionGallery>();

                if (lstGallery != null && lstGallery.Count > 0)
                {
                    foreach (Gallery gallery in lstGallery)
                    {
                        GalleryImageCategory imageGallery = new GalleryImageCategory();
                        imageGallery.Condition = gallery.Condition;
                        imageGallery.Path = conditionGalleryEntity.GetConditionGalleriesByGalleryId(gallery.GalleryId).Select(x => x.FileName).ToList();
                        lstImageGallery.Add(imageGallery);
                    }
                }
                //return new GalleryImageCategory { Category = new List<string> { "Cleaning", "Crowning" }, Path = new List<string> { "21cdc821-389a-4322-8178-c6c92b7044ad.jpg" } };
                return lstImageGallery;
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
                return null;
            }
        }

        ///// <summary>
        ///// WebMethod to get images paths.
        ///// </summary>
        ///// <returns></returns>
        //[WebMethod]
        //public static List<string> GetImagePaths()
        //{
        //    try
        //    {
        //        //here you can read the image folders and get the paths 
        //        //suppose it comes from folder
        //        GalleryEntity galleryEntity = new GalleryEntity();
        //        List<Gallery> lstGallery = galleryEntity.GetHomePageGallery().ToList();
        //        List<string> paths = new List<string>();
        //        string fileName = string.Empty;
        //        //List<string> paths = new List<string>() { "Content/images/examples/thumb-3.jpg", "Content/images/examples/thumb-4.jpg", "Content/images/examples//thumb-5.jpg" };
        //        //List<string> paths = new List<string>() { "21cdc821-389a-4322-8178-c6c92b7044ad.jpg" };
        //        foreach (Gallery gallery in lstGallery)
        //        {
        //            //fileName = gallery.FileName;
        //            //paths.Add(fileName);
        //        }
        //        return paths;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("An error occured.", ex);
        //        return null;
        //    }

        //}
        #endregion
    }
    public class GalleryImageCategory
    {
        public string Condition { get; set; }
        public List<string> Path { get; set; }
    }
}