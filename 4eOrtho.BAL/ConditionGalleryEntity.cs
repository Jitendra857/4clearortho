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
    public class ConditionGalleryEntity : BaseEntity
    {
        /// <summary>
        /// create object method
        /// </summary>
        /// <returns></returns>
        public ConditionGallery Create()
        {
            return orthoEntities.ConditionGalleries.CreateObject();
        }
        /// <summary>
        /// save record in condition gallery table
        /// </summary>
        /// <param name="state"></param>
        public long Save(ConditionGallery gallery)
        {
            if (gallery.EntityState == System.Data.EntityState.Detached)
            {
                gallery.CreatedDate = BaseEntity.GetServerDateTime;
                gallery.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToConditionGalleries(gallery);               
            }
            else
            {
                gallery.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
            return gallery.ConditionGalleryId;
        }
        /// <summary>
        /// remove all gallery files from conditional gallery table
        /// </summary>
        /// <param name="galleryId"></param>
        public void RemoveGalleryIdFiles(long galleryId)
        {
            List<ConditionGallery> lstGallery = orthoEntities.ConditionGalleries.Where(x => x.GalleryId == galleryId).ToList();
            foreach (ConditionGallery gallery in lstGallery)
            {
                orthoEntities.DeleteObject(gallery);
                orthoEntities.SaveChanges();
            }
        }
        public List<ConditionGallery> GetConditionGalleriesByGalleryId(long galleryId)
        {
            return orthoEntities.ConditionGalleries.Where(x => x.GalleryId == galleryId).ToList();
        }
          
    }
}
