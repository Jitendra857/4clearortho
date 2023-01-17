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
    /// File Name:  PatientGalleryMasterEntity.cs
    /// File Description: this page contains business logic for doctor related data.
    /// ----------------------------------------------------------------------------------------------------------
    /// Date Created    : 01-09-2014
    /// Author		    : Bhargav Kukadiya, Verve Systems PVT LTD
    /// ----------------------------------------------------------------------------------------------------------
    /// Change History
    /// Date Changed  	Changed By		Description
    /// ----------------------------------------------------------------------------------------------------------
    /// 
    /// </summary>
    public class PatientGalleryMasterEntity : BaseEntity
    {
        /// <summary>
        /// create object method
        /// </summary>
        /// <returns></returns>
        public PatientGalleryMaster Create()
        {
            return orthoEntities.PatientGalleryMasters.CreateObject();
        }
        /// <summary>
        /// save record in patient gallery table
        /// </summary>
        /// <param name="state"></param>
        public long Save(PatientGalleryMaster patientGallery)
        {
            if (patientGallery.EntityState == System.Data.EntityState.Detached)
            {
                patientGallery.CreatedDate = BaseEntity.GetServerDateTime;
                patientGallery.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToPatientGalleryMasters(patientGallery);
            }
            else
            {
                patientGallery.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
            return patientGallery.PatientGalleryId;
        }
        /// <summary>
        /// get patient gallery by gallery id
        /// </summary>
        /// <param name="gallaryId"></param>
        /// <returns></returns>
        public PatientGalleryMaster GetPatientGalleryById(long patientGalleryId)
        {
            return orthoEntities.PatientGalleryMasters.Where(x => x.PatientGalleryId == patientGalleryId).SingleOrDefault();
        }

        /// <summary>
        /// get patient gallery by gallery id
        /// </summary>
        /// <param name="gallaryId"></param>
        /// <returns></returns>
        public List<PatientGalleryMaster> GetPatientGalleryMasterByPatientId(long patientId)
        {
            return orthoEntities.PatientGalleryMasters.Where(x => x.PatientId == patientId).ToList();
        }

        /// <summary>
        /// get patient gallery by email Id
        /// </summary>
        /// <param name="gallaryId"></param>
        /// <returns></returns>
        public List<PatientGalleryMaster> GetPatientGalleryMasterByEmail(string emailId, bool isTemplate)
        {
            return orthoEntities.PatientGalleryMasters.Where(x => x.PatientEmail == emailId && x.isTemplate == isTemplate && x.IsActive == true).ToList();
        }

        /// <summary>
        /// Get all patient gallery detail by search field
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="searchField"></param>
        /// <param name="searchValue"></param>
        /// <param name="userEmail"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<AllPatientGallleryDetail> GetAllPatientGalleryDetails(string sortField, string sortDirection, int pageSize, int pageIndex, string searchField, string searchValue, string userEmail, string listBy, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));
            List<AllPatientGallleryDetail> lstGetGalleryDetail = orthoEntities.GetAllPatientGallleryDetail(pageIndex, pageSize, sortField, sortDirection, searchField, searchValue, userEmail, listBy, TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstGetGalleryDetail;
        }

        /// <summary>
        /// get all patient gallery
        /// </summary>
        /// <returns></returns>
        public List<PatientGalleryMaster> GetGalleryList()
        {
            return orthoEntities.PatientGalleryMasters.Where(x => x.IsActive).ToList();
        }

        /// <summary>
        /// method to Delete Gallery 
        /// </summary>
        /// <param name="emrCountry"></param>
        public void Delete(PatientGalleryMaster patientGallery)
        {
            orthoEntities.DeleteObject(patientGallery);
            orthoEntities.SaveChanges();
        }

        /// <summary>
        /// Get Patient Gallery By Case Id
        /// </summary>
        /// <param name="CaseId"></param>
        public List<PatientGalleryMaster> GetPatientGalleryByCaseId(long CaseId)
        {
            return orthoEntities.PatientGalleryMasters.Where(x => x.CaseId == CaseId).ToList();
        }

        public List<GetPatientDoctorGallery> GetPatientDoctorGallery(string sortField, string sortDirection, int pageSize, int pageIndex, string searchField, string searchValue, string doctorEmail, string patientEmail, bool isTemplate, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));
            List<GetPatientDoctorGallery> lstGetGalleryDetail = orthoEntities.GetPatientDoctorGallery(pageIndex, pageSize, sortField, sortDirection, searchField, searchValue, doctorEmail, patientEmail, isTemplate, TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstGetGalleryDetail;
        }

        public PatientGalleryMaster GetGalleryByBeforeId(long BeforeId)
        {
            return orthoEntities.PatientGalleryMasters.Where(x => x.BeforeGalleryId == BeforeId).SingleOrDefault();
        }

        public List<GetEightImageTemplate> GetEightImageTemplate(string patientEmail)
        {
            return orthoEntities.GetEightImageTemplate(patientEmail).ToList();
        }
    }
    /// <summary>
    /// patient gallery files
    /// </summary>
    public class PatientGalleryFiles
    {
        public string fileName { get; set; }
        public byte[] fileContent { get; set; }
    }

}
