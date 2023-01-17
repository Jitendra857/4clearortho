using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4eOrtho.Utility;
using _4eOrtho.DAL;
using System.Data.Objects;

namespace _4eOrtho.BAL
{
    public class LocalContactEntity : BaseEntity
    {
        /// <summary>
        /// create object method
        /// </summary>
        /// <returns></returns>
        public LocalContact Create()
        {
            return orthoEntities.LocalContacts.CreateObject();
        }

        /// <summary>
        /// save record in gallery table
        /// </summary>
        /// <param name="state"></param>
        public long Save(LocalContact localContact)
        {
            if (localContact.EntityState == System.Data.EntityState.Detached)
            {
                localContact.CreatedDate = BaseEntity.GetServerDateTime;
                localContact.User.UpdatedDate = localContact.AddressMaster.LastUpdatedDate = localContact.ContactMaster.LastUpdatedDate = localContact.LastUpdatedDate = BaseEntity.GetServerDateTime;
                localContact.User.CreatedDate = localContact.AddressMaster.CreatedDate = localContact.ContactMaster.CreatedDate = localContact.CreatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToLocalContacts(localContact);
            }
            else
            {
                localContact.User.UpdatedDate = localContact.AddressMaster.LastUpdatedDate = localContact.ContactMaster.LastUpdatedDate = localContact.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
            return localContact.Id;
        }

        public LocalContact GetLocalContactById(long Id)
        {
            return orthoEntities.LocalContacts.Where(x => x.Id == Id).FirstOrDefault();
        }

        public LocalContact GetLocalContact(string city, long stateId, long countryId)
        {
            LocalContact localContact = null;
            if (!string.IsNullOrEmpty(city))
                localContact = orthoEntities.LocalContacts.Where(x => x.AddressMaster.City.ToLower().Contains(city) && x.AddressMaster.StateId == stateId && x.AddressMaster.CountryId == countryId).FirstOrDefault();
            if (localContact == null)
                localContact = orthoEntities.LocalContacts.Where(x => x.AddressMaster.StateId == stateId).FirstOrDefault();
            if (localContact == null)
                localContact = orthoEntities.LocalContacts.Where(x => x.AddressMaster.CountryId == countryId).FirstOrDefault();
            return localContact;
        }

        public bool IsAddEmailValid(string emailId)
        {
            var localContact = (from LC in orthoEntities.LocalContacts
                                join CM in orthoEntities.ContactMasters on LC.ContactId equals CM.ContactId
                                where CM.EmailID == emailId
                                select LC.Id);

            return localContact != null && Convert.ToInt64(localContact.FirstOrDefault()) > 0 ? false : true;
        }

        public bool IsEditEmailValid(string emailId, long id)
        {
            var localContact = (from LC in orthoEntities.LocalContacts
                                join CM in orthoEntities.ContactMasters on LC.ContactId equals CM.ContactId
                                where CM.EmailID == emailId && LC.Id == id
                                select LC.Id);

            if (localContact != null && Convert.ToInt64(localContact.FirstOrDefault()) > 0)
            {
                return true;
            }
            else
            {
                return IsAddEmailValid(emailId);
            }
        }

        public List<GetAllLocalContacts> GetAllLocalContacts(string sortField, string sortDirection, int pageSize, int pageIndex, string searchText, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));
            List<GetAllLocalContacts> lstGetAllLocalContacts = orthoEntities.GetAllLocalContacts(pageIndex, pageSize, sortField, sortDirection, searchText, TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstGetAllLocalContacts;
        }
    }
}
