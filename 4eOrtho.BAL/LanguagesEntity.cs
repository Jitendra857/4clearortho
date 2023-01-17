using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4eOrtho.DAL;

namespace _4eOrtho.BAL
{
    public class LanguagesEntity : BaseEntity
    {
        /// <summary>
        /// File Name       :   LanguagesEntity.cs
        /// File Description:	Entity is used for LanguageManagement.
        /// ----------------------------------------------------------------------------------------------------------
        /// Date Created    :	18-07-2014
        /// Author		    :	Piyush Makvana. Verve Systems Pvt. Ltd..
        /// ----------------------------------------------------------------------------------------------------------
        /// Change History
        /// Date Changed    Changed By          Description
        /// ----------------------------------------------------------------------------------------------------------
        /// 
        /// </summary>

        /// <summary>
        /// Description : Method to Create Object
        /// </summary>
        /// <returns></returns>
        public Language Crate()
        {
         return orthoEntities.Languages.CreateObject();
        }
        /// <summary>
        /// Get all Languages
        /// </summary>
        /// <returns></returns>
        public List<Language> GetAllLanguages()
        {
            return orthoEntities.Languages.Where(x => x.IsActive == true).ToList();
        }
            
    }
}
