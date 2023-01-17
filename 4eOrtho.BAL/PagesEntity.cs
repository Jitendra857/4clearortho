using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4eOrtho.DAL;

namespace _4eOrtho.BAL
{
    /// <summary>
    /// File Name       :   PagesEntity.cs
    /// File Description:	Entity is used for ContenetPage Management.
    /// ----------------------------------------------------------------------------------------------------------
    /// Date Created    :	18-07-2014
    /// Author		    :	Piyush Makvana. Verve Systems Pvt. Ltd..
    /// ----------------------------------------------------------------------------------------------------------
    /// Change History
    /// Date Changed    Changed By          Description
    /// ----------------------------------------------------------------------------------------------------------
    /// 
    /// </summary>
    public class PagesEntity : BaseEntity
    {
        /// <summary>
        /// Description : Method to Create Object
        /// </summary>
        /// <returns></returns>
        public Page Create()
        {
            return orthoEntities.Pages.CreateObject();
        }
        /// <summary>
        /// Description : Method to save object
        /// </summary>
        /// <param name="page"></param>
        public long Save(Page page)
        {
            if (page.EntityState == System.Data.EntityState.Detached)
            {
                page.DisplayOrder = 1000;
                page.CreatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToPages(page);
            }
            else
            {
                page.LastUpdatedDate = DateTime.Now;
            }
            orthoEntities.SaveChanges();
            return page.PageID;
        }
        /// <summary>
        /// Description : Method to delete object
        /// </summary>
        /// <param name="page"></param>
        public void Delete(Page page)
        {
            orthoEntities.DeleteObject(page);
            orthoEntities.SaveChanges();
        }
        /// <summary>
        /// Description : Method to Get page details by id
        /// </summary>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public Page GetPageByID(long? pageID)
        {
            return orthoEntities.Pages.Where(x => x.PageID == pageID).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageID"></param>
        /// <param name="languageID"></param>
        /// <returns></returns>
        public PageWithDetail GetPageWithDetailByID(long pageID, int languageID)
        {
            return orthoEntities.GetPageWithDetail(languageID, pageID).FirstOrDefault();
        }
        /// <summary>
        /// Description : Method to GetPageWithDetailByID
        /// </summary>
        /// <param name="languageID"></param>
        /// <returns></returns>
        public List<PageWithLevel> GetPageWithLevel(int languageID)
        {
            return orthoEntities.GetPageWithLevel(languageID).ToList();
        }
        public List<MenuPageWithLevel> GetMenuPage(int languageID, string role)
        {
            return orthoEntities.GetMenuPageWithLevel(languageID, role).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="languageID"></param>
        /// <returns></returns>
        public PageWithLevel GetPageWithLevelById(long pageId, int languageID)
        {
            return orthoEntities.GetPageWithLevel(languageID).Where(x => x.PageID == pageId).FirstOrDefault();
        }
        /// <summary>
        /// Description : Method to GetFirstLevel page
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="languageID"></param>
        /// <returns></returns>
        public List<PageWithLevel> GetFirstLevelPageFromAnyItem(long? pageId, int languageID)
        {
            return orthoEntities.GetPageWithLevel(languageID).Where(x => x.ParentID == pageId).OrderBy(x => x.DisplayOrder).ThenBy(x => x.PageID).ToList();
        }
        /// <summary>
        /// Description : Method to get Page Name
        /// </summary>
        /// <param name="pagename"></param>
        /// <returns></returns>
        public PageDetail GetByPageName(string pagename)
        {
            return orthoEntities.PageDetails.Where(x => x.PageTitle == pagename).FirstOrDefault();
        }
        public bool GetMenuitem(string menuitem)
        {
            if (orthoEntities.PageDetails.Where(x => x.MenuItem == menuitem).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool GetMenuitem(string menuitem, long menuId)
        {
            if (orthoEntities.PageDetails.Where(x => x.MenuItem == menuitem && x.PageID != menuId && x.IsActive == true).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Page GetPageByIDandLanguageID(long pageID, int languageID)
        {
            return orthoEntities.Pages.Where(x => x.PageID == pageID).FirstOrDefault();
        }
        public PageDetail GetPageDetailByMenuNameandLanguage(string menuName, int languageID)
        {
            return orthoEntities.PageDetails.Where(x => x.URLName == menuName && x.LanguageID == languageID).FirstOrDefault();
        }
        public PageDetail GetPageDetailByMenuNameandLanguageById(long pageID, int languageID)
        {
            return orthoEntities.PageDetails.Where(x => x.PageID == pageID && x.LanguageID == languageID).FirstOrDefault();
        }
        public bool IsDuplicateMenu(string menuName, long pageId, int languageID)
        {
            if (orthoEntities.PageDetails.Where(x => x.MenuItem.Equals(menuName, StringComparison.OrdinalIgnoreCase) && x.PageID != pageId && x.LanguageID == languageID).FirstOrDefault() != null)
            {
                return true;
            }
            return false;
        }
    }   
}
