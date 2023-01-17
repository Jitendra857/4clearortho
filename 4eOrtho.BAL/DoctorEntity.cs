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
    /// File Name:  DoctorEntity.cs
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
    public class DoctorEntity : BaseEntity
    {
        /// <summary>
        /// create object method
        /// </summary>
        /// <returns></returns>
        public Doctor Create()
        {
            return orthoEntities.Doctors.CreateObject();
        }

        /// <summary>
        /// save record in gallery table
        /// </summary>
        /// <param name="state"></param>
        public long Save(Doctor doctor)
        {
            if (doctor.EntityState == System.Data.EntityState.Detached)
            {
                doctor.CreatedDate = BaseEntity.GetServerDateTime;
                doctor.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToDoctors(doctor);
            }
            else
            {
                doctor.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
            return doctor.DoctorId;
        }


        public List<CertifiedDoctorDetailsByFilterType> GetCertifiedDoctorDetailsByFilterType(string sortField, string sortDirection, int pageSize, int pageIndex, out int totalRecords, string SearchField, string SearchValue, string patientEmail)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));
            List<CertifiedDoctorDetailsByFilterType> lstDoctorDetails = orthoEntities.GetCertifiedDoctorDetailsByFilterType(pageIndex, pageSize, sortField, sortDirection, TotalRecCount, SearchField, SearchValue, patientEmail, false).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstDoctorDetails;
        }

        public List<NonCertifiedDoctorDetailsByFilterType> GetNonCertifiedDoctorDetailsByFilterType(string sortField, string sortDirection, int pageSize, int pageIndex, out int totalRecords, string SearchField, string SearchValue, string patientEmail)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));
            List<NonCertifiedDoctorDetailsByFilterType> lstDoctorDetails = orthoEntities.GetNonCertifiedDoctorDetailsByFilterType(pageIndex, pageSize, sortField, sortDirection, TotalRecCount, SearchField, SearchValue, patientEmail, false).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstDoctorDetails;
        }

        public List<GetDoctorListFromOrthoAndAAAD_Result> GetAllGetDoctorListFromOrthoAndAAAD()
        {
            return orthoEntities.GetDoctorListFromOrthoAndAAAD().ToList();
        }

        public AllDoctorDetailsByEmailId GetDoctorDetailsByEmailId(string emailId)
        {
            return orthoEntities.GetAllDoctorDetailsByEmailId(emailId).FirstOrDefault();
        }

        public DomainDoctorDetailsByEmail GetDoctorListByEmail(string emailId)
        {
            return orthoEntities.GetDomainDoctorDetailsByEmail(emailId).FirstOrDefault();
        }

        public DomainPatientByEmail GetPatientByEmail(string emailId)
        {
            return orthoEntities.GetDomainPatientByEmail(emailId).FirstOrDefault();
        }

        public List<PatientDetails> GetPatientList()
        {
            return orthoEntities.GetPatientDetails().ToList();
        }

        public void SendPatientAppointmentRequestMail(DomainPatientByEmail patientDetails, DomainDoctorDetailsByEmail doctorDetails, string doctorEmail, string patientEmail, string doctorName, string doctorFirstName, string doctorLastName, string patientName, string patientFirstname, string patientLastName, string description, string duration, string appointmentDate, string prefferedTime, string emailTemplatePath, string appointmentSubject)
        {
            if (File.Exists(emailTemplatePath))
            {
                string emailtemplateHTML = File.ReadAllText(emailTemplatePath);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientFirstName##", patientFirstname);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientLastName##", patientLastName);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorEmail##", doctorEmail);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorState##", doctorDetails.StateName);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorCountry##", doctorDetails.CountryName);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorMobile##", doctorDetails.Mobile);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorHome##", doctorDetails.HomeContact);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorWork##", doctorDetails.WorkContact);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientName##", patientLastName);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientEmail##", patientEmail);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientState##", patientDetails.StateName);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientCountry##", patientDetails.CountryName);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientMobile##", patientDetails.Mobile);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientHome##", patientDetails.HomeContact);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientWork##", patientDetails.WorkContact);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorFirstName##", doctorFirstName);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorLastname##", doctorLastName);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorName##", doctorFirstName);
                emailtemplateHTML = emailtemplateHTML.Replace("##Description##", description);
                emailtemplateHTML = emailtemplateHTML.Replace("##Duration##", duration);
                emailtemplateHTML = emailtemplateHTML.Replace("##AppointmentDate##", appointmentDate);
                emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));
                emailtemplateHTML = emailtemplateHTML.Replace("##PreferredTime##", prefferedTime);
                emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", System.DateTime.Now.Year.ToString());
                MailAddress fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("FromNoReplyEmail"), CommonLogic.GetConfigValue("FromNoReplyName"));
                MailAddress toMailAddress = new MailAddress(patientEmail, patientName);
                CommonLogic.SendMail(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, appointmentSubject);
            }

        }

        public void SendDoctorAppointmentRequestMail(DomainPatientByEmail patientDetails, DomainDoctorDetailsByEmail doctorDetails, string doctorEmail, string patientEmail, string doctorName, string doctorFirstName, string doctorLastName, string patientName, string patientFirstname, string patientLastName, string description, string duration, string appointmentDate, string prefferedTime, string emailTemplatePath, string appointmentSubject)
        {
            if (File.Exists(emailTemplatePath))
            {
                string emailtemplateHTML = File.ReadAllText(emailTemplatePath);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientFirstName##", patientFirstname);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientLastName##", patientLastName);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorEmail##", doctorEmail);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorState##", doctorDetails.StateName);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorCountry##", doctorDetails.CountryName);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorMobile##", doctorDetails.Mobile);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorHome##", doctorDetails.HomeContact);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorWork##", doctorDetails.WorkContact);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientName##", patientLastName);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientEmail##", patientEmail);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientState##", patientDetails.StateName);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientCountry##", patientDetails.CountryName);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientMobile##", patientDetails.Mobile);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientHome##", patientDetails.HomeContact);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientWork##", patientDetails.WorkContact);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorFirstName##", doctorFirstName);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorLastname##", doctorLastName);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorName##", doctorName);
                emailtemplateHTML = emailtemplateHTML.Replace("##Description##", description);
                emailtemplateHTML = emailtemplateHTML.Replace("##Duration##", duration);
                emailtemplateHTML = emailtemplateHTML.Replace("##AppointmentDate##", appointmentDate);
                emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));
                emailtemplateHTML = emailtemplateHTML.Replace("##PreferredTime##", prefferedTime);
                emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", System.DateTime.Now.Year.ToString());
                MailAddress fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("FromNoReplyEmail"), CommonLogic.GetConfigValue("FromNoReplyName"));
                MailAddress toMailAddress = new MailAddress(doctorEmail, doctorName);
                CommonLogic.SendMail(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, appointmentSubject);
            }
        }

        public Doctor GetDoctorByUserId(long doctorId)
        {
            return orthoEntities.Doctors.Where(x => x.DoctorId == doctorId).FirstOrDefault();
        }

        public WSB_UserDomainMaster GetUserDomain(string RoleType, string SourceType, string EmailId)
        {
            return orthoEntities.WSB_UserDomainMaster.Where(x => x.RoleType == RoleType && x.SourceType == SourceType && x.EmailID == EmailId).FirstOrDefault();
        }
        public WSB_UserDomainMaster GetUserDomain(string EmailId)
        {
            return orthoEntities.WSB_UserDomainMaster.Where(x => x.EmailID == EmailId && (x.RoleType.ToUpper() == "D" || x.RoleType.ToUpper() == "S")).FirstOrDefault();
        }

        public Int64 SubscribeAADStudent(string emailID, string password, string firstName, string lastName, string mI, string gender, DateTime dOB, string maritalStatus, string homeContact, string mobile, string workContact, long countryId, long stateId, string city, string street, string zipCode)
        {
            ObjectParameter StudentID = new ObjectParameter("StudentID", Type.GetType("System.Int64"));
            orthoEntities.SubscribeAADStudent(emailID, password, firstName, lastName, mI, gender, dOB, maritalStatus, homeContact, mobile, workContact, countryId, stateId, city, street, zipCode, StudentID);
            return Convert.IsDBNull(StudentID) ? 0 : (Int64)StudentID.Value; ;
        }

        public List<GetAllOrthoDoctor_Result> GetAllOrthoDoctor()
        {
            return orthoEntities.GetAllOrthoDoctor().ToList();
        }
    }
}
