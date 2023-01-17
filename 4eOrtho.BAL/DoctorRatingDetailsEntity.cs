using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using _4eOrtho.DAL;
using _4eOrtho.Utility;

namespace _4eOrtho.BAL
{
    /// <summary>
    /// File Name:  DoctorRatingDetailsEntity.cs
    /// File Description: this page contains business logic for doctor rating details Table related data.
    /// ----------------------------------------------------------------------------------------------------------
    /// Date Created    : 05-09-2014
    /// Author		    : Bhargav Kukadiya, Verve Systems PVT LTD
    /// ----------------------------------------------------------------------------------------------------------
    /// Change History
    /// Date Changed  	Changed By		Description
    /// ----------------------------------------------------------------------------------------------------------
    /// 
    /// </summary>
    public class DoctorRatingDetailsEntity : BaseEntity
    {
        /// <summary>
        /// create object method
        /// </summary>
        /// <returns></returns>
        public DoctorRatingDetail Create()
        {
            return orthoEntities.DoctorRatingDetails.CreateObject();
        }
        /// <summary>
        /// save record in doctorratingdetail table
        /// </summary>
        /// <param name="state"></param>
        public void Save(DoctorRatingDetail doctorRatingDetail)
        {
            if (doctorRatingDetail.EntityState == System.Data.EntityState.Detached)
            {
                doctorRatingDetail.CreatedDate = BaseEntity.GetServerDateTime;
                doctorRatingDetail.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToDoctorRatingDetails(doctorRatingDetail);
            }
            else
            {
                doctorRatingDetail.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
        }

        public List<DoctorRatingDetailsByPatientEmail> GetDoctorDetails(string patientEmail, string doctorEmail)
        {
            return orthoEntities.GetDoctorRatingDetailsByPatientEmail(patientEmail, doctorEmail).ToList();
        }

        public DoctorRatingDetailsByDoctorEmail GetDoctorDetailsByDoctorEmail(string doctorEmail)
        {
            return orthoEntities.GetDoctorRatingDetailsByDoctorEmail(doctorEmail).SingleOrDefault();
        }

        public void DeleteDoctorDetailsByPatientEmail(string patientEmail, long doctorRatingId)
        {
            orthoEntities.DeleteDoctorRatingByPatientEmail(patientEmail, doctorRatingId);
        }
    }
}
