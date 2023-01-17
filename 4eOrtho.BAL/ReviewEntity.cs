using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Utility;

namespace _4eOrtho.BAL
{
    /// <summary>
    /// File Name:  ReviewEntity.cs
    /// File Description: entity used for Review Entity table
    /// ----------------------------------------------------------------------------------------------------------
    /// Date Created    : 22-07-2014
    /// Author		    : Piyush Makvana, Verve Systems PVT LTD
    /// ----------------------------------------------------------------------------------------------------------
    /// Change History
    /// Date Changed  	Changed By		Description
    /// ----------------------------------------------------------------------------------------------------------
    /// 
    /// </summary>
    public class ReviewEntity : BaseEntity
    {

        /// <summary>
        /// method for  create object.
        /// </summary>
        /// <returns></returns>
        public Review Create()
        {
            return orthoEntities.Reviews.CreateObject();
        }
        /// <summary>
        /// method for save record.
        /// </summary>
        /// <param name="clienttestimonial"></param>
        public void Save(Review review)
        {
            if (review.EntityState == System.Data.EntityState.Detached)
            {
                review.LastUpdatedDate = BaseEntity.GetServerDateTime;
                review.CreatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToReviews(review);
            }
            else
            {
                review.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
        }
        /// <summary>
        /// get review by id
        /// </summary>
        /// <param name="clienttestimonialId"></param>
        /// <returns></returns>
        public Review GetReviewByReviewId(int reviewId)
        {
            return orthoEntities.Reviews.Where(x => x.ReviewId == reviewId).FirstOrDefault();
        }
        /// <summary>
        /// method for bind list.
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<AllReviewDetail> GetAllReviewDetails(string sortField, string sortDirection, int pageSize, int pageIndex, string searchField, string searchValue, out int totalRecords, string doctorEmail,string userType)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));
            List<AllReviewDetail> lstAllReviewDetail = orthoEntities.GetAllReviewDetail(pageIndex, pageSize, sortField, sortDirection, searchField, searchValue, doctorEmail, TotalRecCount,userType).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstAllReviewDetail;
        }

        /// <summary>
        /// Get all patient review
        /// </summary>
        /// <returns></returns>
        public List<AllPatientReviewByDoctorEmail> GetAllPatientReviews(string emailId)
        {
            return orthoEntities.GetAllPatientReviewByDoctorEmail(emailId).ToList();
        }
        /// <summary>
        /// Method to delete object
        /// </summary>
        /// <param name="pageDetail"></param>
        public void Delete(Review review)
        {
            orthoEntities.DeleteObject(review);
            orthoEntities.SaveChanges();
        }
        /// <summary>
        /// get review details by review id
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        public ReviewByReviewId GetReviewByReviewId(long reviewId)
        {
            return orthoEntities.GetReviewByReviewId(reviewId).FirstOrDefault();
        }
        /// <summary>
        /// Get review by review id
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        public Review GetReviewById(long reviewId)
        {
            return orthoEntities.Reviews.Where(x => x.ReviewId == reviewId).FirstOrDefault();
        }
        /// <summary>
        /// Send doctor mail
        /// </summary>
        /// <param name="patientEmail"></param>
        /// <param name="doctorEmail"></param>
        /// <param name="doctorLastName"></param>
        /// <param name="doctorFirstName"></param>
        /// <param name="patientName"></param>
        /// <param name="doctorName"></param>
        /// <param name="review"></param>
        /// <param name="emailtemplatePath"></param>
        public void SendDoctorMail(string patientEmail,string doctorEmail,string doctorLastName,string doctorFirstName, string patientName,string doctorName, string review, string emailtemplatePath)
        {

            if (File.Exists(emailtemplatePath))
            {
                string emailtemplateHTML = File.ReadAllText(emailtemplatePath);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientName##", patientName);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorLastname##", doctorLastName);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorFirstName##", doctorFirstName);
                emailtemplateHTML = emailtemplateHTML.Replace("##EmailId##", patientEmail);
                emailtemplateHTML = emailtemplateHTML.Replace("##Review##", review);
                emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", System.DateTime.Now.Year.ToString());
                emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));
                MailAddress fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("ToMail"));
                MailAddress toMailAddress = new MailAddress(doctorEmail, doctorName);
                CommonLogic.SendMail(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, "4ClearOrtho - Doctor Review");
            }
        }
    }
}
