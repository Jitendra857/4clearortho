using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4eOrtho.Utility;
using _4eOrtho.DAL;
using System.Data.Objects;

namespace _4eOrtho.BAL
{
    public class AppointmentRequestDetails
    {
        public long PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientMobile { get; set; }
        public string PatientEmail { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime PreferedTime { get; set; }
        public string Duration { get; set; }
        public long DoctorId { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string DatabaseName { get; set; }
    }

    /// <summary>
    /// File Name:  AppointmentRequestEntity.cs
    /// File Description: this page contains business logic for doctor related data.
    /// ----------------------------------------------------------------------------------------------------------
    /// Date Created    : 28-07-2014
    /// Author		    : Bhargav Kukadiya, Verve Systems PVT LTD
    /// ----------------------------------------------------------------------------------------------------------
    /// Change History
    /// Date Changed  	Changed By		Description
    /// ----------------------------------------------------------------------------------------------------------
    /// 
    /// </summary>
    public class AppointmentRequestEntity : BaseEntity
    {

        public void CreateAppointmentRequest(AppointmentRequestDetails req)
        {
            orthoEntities.CreateAppointmentRequest(req.PatientId, req.PatientName, req.PatientMobile, req.PatientEmail, req.AppointmentDate, req.PreferedTime, req.Duration, req.DoctorId, req.Description, req.CreatedBy, req.CreatedDate, req.LastUpdatedBy,req.LastUpdatedDate,req.DatabaseName);        
        }
        /// <summary>
        /// Method to save appointment in add 
        /// </summary>
        /// <param name="emrCountry"></param>
        public void Save(AppointmentRequest appointmentRequest)
        {
            if (appointmentRequest.EntityState == System.Data.EntityState.Detached)
            {
                appointmentRequest.CreatedDate = BaseEntity.GetServerDateTime;
                appointmentRequest.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToAppointmentRequests(appointmentRequest);
            }
            else
            {
                appointmentRequest.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
        }
        /// <summary>
        ///  Method to create instance of Appointment Request 
        /// </summary>
        /// <returns></returns>
        public AppointmentRequest Create()
        {
            return orthoEntities.AppointmentRequests.CreateObject();
        }
    }
}
