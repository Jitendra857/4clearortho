using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4eOrtho.Utility;
using _4eOrtho.DAL;
using System.Data.Objects;
using System.IO;
using System.Net.Mail;

namespace _4eOrtho.BAL
{
    public class PackageGalleryEntity : BaseEntity
    {
        /// <summary>
        /// create object method
        /// </summary>
        /// <returns></returns>
        public PackageGallery Create()
        {
            return orthoEntities.PackageGalleries.CreateObject();
        }
        /// <summary>
        /// save record in condition gallery table
        /// </summary>
        /// <param name="state"></param>
        public void Save(PackageGallery gallery)
        {
            if (gallery.EntityState == System.Data.EntityState.Detached)
            {
                gallery.CreatedDate = BaseEntity.GetServerDateTime;
                gallery.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToPackageGalleries(gallery);
            }
            else
            {
                gallery.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
        }
        /// <summary>
        /// remove all gallery files from conditional gallery table
        /// </summary>
        /// <param name="galleryId"></param>
        public void RemoveGalleryIdFiles(long packageId)
        {
            List<PackageGallery> lstGallery = orthoEntities.PackageGalleries.Where(x => x.PackageId == packageId).ToList();
            foreach (PackageGallery gallery in lstGallery)
            {
                orthoEntities.DeleteObject(gallery);
                orthoEntities.SaveChanges();
            }
        }
        /// <summary>
        /// get package galleries by packageid
        /// </summary>
        /// <param name="packageId"></param>
        /// <returns></returns>
        public List<PackageGallery> GetPackageGalleriesByPackageId(long packageId)
        {
            return orthoEntities.PackageGalleries.Where(x => x.PackageId == packageId).ToList();
        }


    }
    /// <summary>
    /// Package gallery files
    /// </summary>
    public class PackageGalleryFiles
    {
        public string fileName { get; set; }
        public byte[] fileContent { get; set; }
        public bool IsNewUploaded { get; set; }
        public string PreviewUrl { get; set; }
    }
}
