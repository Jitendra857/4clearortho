using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4eOrtho.DAL;

namespace _4eOrtho.BAL
{
    /// <summary>
    /// File Name       :   PageDetailsEntiy.cs
    /// File Description:	Entity Is used for ContentPageManagement.
    /// ----------------------------------------------------------------------------------------------------------
    /// Date Created    :	18-07-2014
    /// Author		    :	Piyush Makvana. Verve Systems Pvt. Ltd..
    /// ----------------------------------------------------------------------------------------------------------
    /// Change History
    /// Date Changed    Changed By          Description
    /// ----------------------------------------------------------------------------------------------------------
    /// 
    /// </summary>


    public class PageDetailsEntity : BaseEntity
    {
        /// <summary>
        /// Description : Method to create object
        /// </summary>
        /// <returns></returns>
        public PageDetail Create()
        {
            return orthoEntities.PageDetails.CreateObject();
        }
        /// <summary>
        /// Description : Method to get PageDetail by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private PageDetail GetPageDetailById(int id)
        {
            return orthoEntities.PageDetails.Where(x => x.PageDetailID == id).FirstOrDefault();
        }
        /// <summary>
        /// Description : Method to save object
        /// </summary>
        /// <param name="pageDetail"></param>
        public void Save(PageDetail pageDetail)
        {
            if (pageDetail.EntityState == System.Data.EntityState.Detached)
            {
                pageDetail.CreatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToPageDetails(pageDetail);
            }
            else
            {
                pageDetail.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
        }
        /// <summary>
        /// Description : Method to delete object
        /// </summary>
        /// <param name="pageDetail"></param>
        public void Delete(PageDetail pageDetail)
        {
            orthoEntities.DeleteObject(pageDetail);
            orthoEntities.SaveChanges();
        }
        /// <summary>
        /// Description : Method to get PagedetailsbyPage and LanguageId
        /// </summary>
        /// <param name="pageID"></param>
        /// <param name="languageID"></param>
        /// <returns></returns>
        public PageDetail GetPageDetailsByPageIdAndLanguageID(long pageID, int languageID)
        {
            return orthoEntities.PageDetails.Where(x => x.PageID == pageID && x.LanguageID == languageID).FirstOrDefault();
        }

        /// <summary>
        /// Description : Method to get PagedetailsbyPageDetailID
        /// </summary>
        /// <param name="pageID"></param>
        /// <param name="languageID"></param>
        /// <returns></returns>
        public PageDetail GetPageDetailsByPageDetailID(long pageDetailID)
        {
            return orthoEntities.PageDetails.Where(x => x.PageDetailID == pageDetailID).FirstOrDefault();
        }

        public PageDetail GetPageDetailByPageId(long pageId)
        {
            return orthoEntities.PageDetails.Where(x => x.PageID == pageId).FirstOrDefault();
        }

        public List<PageDetail> GetPageDetailsByRole(string role1, string role2, int languageId)
        {
            return orthoEntities.PageDetails.Where(x => (x.Role == role1 || x.Role == role2) && x.LanguageID == languageId && x.IsActive == true).ToList();
        }
        public List<PageDetail> GetPageDetailsByRole(string role, int languageId)
        {
            return orthoEntities.PageDetails.Where(x => x.Role.Contains(role) && x.LanguageID == languageId && x.IsActive == true).ToList();
        }
    }
}
