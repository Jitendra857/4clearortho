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
    /// File Name:  StateEntity.cs
    /// File Description: this page contains business logic for doctor rating Table related data.
    /// ----------------------------------------------------------------------------------------------------------
    /// Date Created    : 09-05-2014
    /// Author		    : Bhargav Kukadiya, Verve Systems PVT LTD
    /// ----------------------------------------------------------------------------------------------------------
    /// Change History
    /// Date Changed  	Changed By		Description
    /// ----------------------------------------------------------------------------------------------------------
    /// 
    /// </summary>
    public class DoctorRatingEntity : BaseEntity
    {
        /// <summary>
        /// create object method
        /// </summary>
        /// <returns></returns>
        public DoctorRating Create()
        {
            return orthoEntities.DoctorRatings.CreateObject();
        }
        /// <summary>
        /// save record in doctorrating table
        /// </summary>
        /// <param name="state"></param>
        public long Save(DoctorRating doctorRating)
        {
            if (doctorRating.EntityState == System.Data.EntityState.Detached)
            {
                doctorRating.CreatedDate = BaseEntity.GetServerDateTime;
                doctorRating.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToDoctorRatings(doctorRating);
            }
            else
            {
                doctorRating.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
            return doctorRating.DoctorRatingId;
        }

        public DoctorRating GetDoctorRatingById(long doctorRatingId)
        {
            return orthoEntities.DoctorRatings.Where(x => x.DoctorRatingId == doctorRatingId).SingleOrDefault();
        }

        public List<DoctorRating> GetDoctorRatingByEmail(string doctorEmail)
        {
            return orthoEntities.DoctorRatings.Where(x => x.DoctorEmail == doctorEmail).ToList();
        }
        public DoctorRating GetDoctorRatingByPatientEmail(string patientEmail,string doctorEmail)
        {
            return orthoEntities.DoctorRatings.Where(x => x.PatientEmail == patientEmail && x.DoctorEmail == doctorEmail).SingleOrDefault();
        }
        
    }
}
