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
    /// <summary>
    /// File Name:  GalleryEntity.cs
    /// File Description: this page contains business logic for doctor related data.
    /// ----------------------------------------------------------------------------------------------------------
    /// Date Created    : 31-07-2014
    /// Author		    : Bhargav Kukadiya, Verve Systems PVT LTD
    /// ----------------------------------------------------------------------------------------------------------
    /// Change History
    /// Date Changed  	Changed By		Description
    /// ----------------------------------------------------------------------------------------------------------
    /// 
    /// </summary>
    public class GalleryEntity : BaseEntity
    {
        /// <summary>
        /// create object method
        /// </summary>
        /// <returns></returns>
        public Gallery Create()
        {
            return orthoEntities.Galleries.CreateObject();
        }
        /// <summary>
        /// save record in gallery table
        /// </summary>
        /// <param name="state"></param>
        public long Save(Gallery gallery)
        {
            if (gallery.EntityState == System.Data.EntityState.Detached)
            {
                gallery.CreatedDate = BaseEntity.GetServerDateTime;
                gallery.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToGalleries(gallery);
            }
            else
            {
                gallery.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
            return gallery.GalleryId;
        }

        public Gallery GetGalleryById(long gallaryId)
        {
            return orthoEntities.Galleries.Where(x => x.GalleryId == gallaryId).SingleOrDefault();
        }

        public List<AllGallleryDetail> GetAllGalleryDetails(string sortField, string sortDirection, int pageSize, int pageIndex, string searchField, string searchValue, string userEmail, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));

            List<AllGallleryDetail> lstGetGalleryDetail = orthoEntities.GetAllGallleryDetail(pageIndex, pageSize, sortField, sortDirection, searchField, searchValue, userEmail, TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstGetGalleryDetail;
        }

        public List<GetGallleryDetailForDoctor> GetAllGalleryDetails(string sortField, string sortDirection, int pageSize, int pageIndex, string searchField, string searchValue, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));

            List<GetGallleryDetailForDoctor> lstGetGalleryDetail = orthoEntities.GetGallleryDetailForDoctor(pageIndex, pageSize, sortField, sortDirection, searchField, searchValue, TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstGetGalleryDetail;
        }

        public List<Gallery> GetHomePageGallery()
        {
            return orthoEntities.Galleries.Where(x => x.IsHomeDisplay && x.IsActive).OrderByDescending(x => x.GalleryId).ToList();
        }

        /// <summary>
        /// Method to Delete Gallery 
        /// </summary>
        /// <param name="emrCountry"></param>
        public void Delete(Gallery gallery)
        {
            orthoEntities.DeleteObject(gallery);
            orthoEntities.SaveChanges();
        }
    }
    public class DoctorGalleryMappingEntity : BaseEntity
    {
        public DoctorGalleryMapping Create()
        {
            return orthoEntities.DoctorGalleryMappings.CreateObject();
        }
        public long Save(DoctorGalleryMapping gallery)
        {
            if (gallery.EntityState == System.Data.EntityState.Detached)
            {
                orthoEntities.AddToDoctorGalleryMappings(gallery);
            }
            orthoEntities.SaveChanges();
            return gallery.GalleryId;
        }
        public void Delete(DoctorGalleryMapping gallery)
        {
            orthoEntities.DeleteObject(gallery);
            orthoEntities.SaveChanges();
        }

        public List<DoctorGalleryMapping> GetAllDoctorGalleryMappingByEmail(string emailId)
        {
            return orthoEntities.DoctorGalleryMappings.Where(x => x.DoctorEmailId.Equals(emailId, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public DoctorGalleryMapping GetDoctorGalleryById(long gallaryId, string doctorEmailId)
        {
            return orthoEntities.DoctorGalleryMappings.Where(x => x.GalleryId == gallaryId && x.DoctorEmailId.Equals(doctorEmailId, StringComparison.InvariantCultureIgnoreCase)).SingleOrDefault();
        }
    }

    public class GalleryFiles
    {
        public string fileName { get; set; }
        public byte[] fileContent { get; set; }
    }
}
