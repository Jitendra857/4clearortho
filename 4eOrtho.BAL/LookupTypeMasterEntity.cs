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
    /// File Name:  LookupMasterEntity.cs
    /// File Description: this page contains business logic for lookup related data.
    /// ----------------------------------------------------------------------------------------------------------
    /// Date Created    : 05-09-2014
    /// Author		    : Bhargav Kukadiya, Verve Systems PVT LTD
    /// ----------------------------------------------------------------------------------------------------------
    /// Change History
    /// Date Changed  	Changed By		Description
    /// ----------------------------------------------------------------------------------------------------------
    /// 
    /// </summary>
    public class LookupTypeMasterEntity : BaseEntity
    {

        public List<LookupTypeMaster> GetLookupMasterDetails(string lookupTypeName)
        {
            return orthoEntities.LookupTypeMasters.Where(x => x.LookupType == lookupTypeName).ToList();
        }
    }
}
