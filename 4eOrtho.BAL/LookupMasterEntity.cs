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
    public class LookupMasterEntity : BaseEntity
    {
        /// <summary>
        /// create object method
        /// </summary>
        /// <returns></returns>
        public LookupMaster Create()
        {
            return orthoEntities.LookupMasters.CreateObject();
        }

        /// <summary>
        /// save record in gallery table
        /// </summary>
        /// <param name="state"></param>
        public long Save(LookupMaster lookupMaster)
        {
            if (lookupMaster.EntityState == System.Data.EntityState.Detached)
            {
                lookupMaster.CreatedDate = BaseEntity.GetServerDateTime;
                lookupMaster.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToLookupMasters(lookupMaster);
            }
            else
            {
                lookupMaster.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
            return lookupMaster.LookupId;
        }

        public LookupMaster GetLookupMasterDetails(string lookupName)
        {
            return orthoEntities.LookupMasters.Where(x => x.LookupName == lookupName).FirstOrDefault();
        }

        public List<LookUpDetailsByLookupType> GetLookUpDetails(string lookupTypeName)
        {
            return orthoEntities.GetLookUpDetailsByLookupType(lookupTypeName).ToList();
        }

        public LookupMaster GetLookupMasterByDesc(string lookupDesc)
        {
            return orthoEntities.LookupMasters.Where(x => x.LookupDescription.ToUpper() == lookupDesc.ToUpper()).FirstOrDefault();
        }

        public LookupMaster GetLookupMasterById(long lookupId)
        {
            return orthoEntities.LookupMasters.Where(x => x.LookupId == lookupId).FirstOrDefault();
        }

        public void UpdateLookup(LookupMaster lookupMaster)
        {
            orthoEntities.SaveChanges();
        }

        public LookupMaster GetLookupMasterByLookUpTypeId(long lookupTypeId)
        {
            return orthoEntities.LookupMasters.Where(x => x.LookupTypeId == lookupTypeId).FirstOrDefault();
        }

        public string GetStudentCourseSubscribeLinkFromLookUpMaster()
        {
            LookupTypeMaster info = orthoEntities.LookupTypeMasters.Where(x => x.LookupType == "StudentCourseSubscribeLink").FirstOrDefault();
            return orthoEntities.LookupMasters.Where(x => x.LookupTypeId == info.LookupTypeId).FirstOrDefault().LookupName;
        }

        public List<GetCaseType> GetCaseType(string lookupTypeName)
        {
            return orthoEntities.GetCaseType(lookupTypeName).ToList();
        }
    }
}
