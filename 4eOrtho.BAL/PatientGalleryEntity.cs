using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4eOrtho.DAL;

namespace _4eOrtho.BAL
{
    public class PatientGalleryEntity : BaseEntity
    {
        /// <summary>
        /// create object method
        /// </summary>
        /// <returns></returns>
        public PatientGallery Create()
        {
            return orthoEntities.PatientGalleries.CreateObject();
        }
        /// <summary>
        /// save record in condition patient gallery table
        /// </summary>
        /// <param name="state"></param>
        public void Save(PatientGallery gallery)
        {
            if (gallery.EntityState == System.Data.EntityState.Detached)
            {
                gallery.CreatedDate = BaseEntity.GetServerDateTime;
                gallery.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToPatientGalleries(gallery);
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
        public void RemoveGalleryIdFiles(long galleryId)
        {
            List<PatientGallery> lstGallery = orthoEntities.PatientGalleries.Where(x => x.GalleryId == galleryId).ToList();
            foreach (PatientGallery gallery in lstGallery)
            {
                orthoEntities.DeleteObject(gallery);
                orthoEntities.SaveChanges();
            }
        }
        /// <summary>
        /// get patient gallery by gallery id
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        public List<PatientGallery> GetPatientGalleriesByGalleryId(long galleryId)
        {
            return orthoEntities.PatientGalleries.Where(x => x.GalleryId == galleryId).ToList();
        }
        /// <summary>
        /// get patient galleries by patient email
        /// </summary>
        /// <param name="patientEmail"></param>
        /// <returns></returns>
        public List<PatientGalleryDetails> GetPatientGalleriesByPatientEmail(string patientEmail)
        {
            return orthoEntities.GetPatientGalleryDetails(patientEmail).ToList();
        }

        

          
    }
}
