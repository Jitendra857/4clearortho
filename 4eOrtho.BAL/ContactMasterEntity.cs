using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using _4eOrtho.BAL;
using _4eOrtho.DAL;

namespace _4eOrtho.BAL
{
    /// <summary>
    /// 1. Patient/Doctor Contact Details
    /// 2. Contact Infomration ID will Stored in Patient Details
    /// </summary>
    public class ContactMasterEntity : BaseEntity
    {       
        /// <summary>
        ///  Method to save Contact in add and edit condition
        /// </summary>
        /// <param name="customer"></param>
        public long Save(ContactMaster contact)
        {
            if (contact.EntityState == System.Data.EntityState.Detached)
            {
                contact.CreatedDate = BaseEntity.GetServerDateTime;
                contact.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToContactMasters(contact);
            }
            else
            {
                contact.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
            return contact.ContactId;
        }

        /// <summary>
        ///  Method to create instance of Contact
        /// </summary>
        /// <returns></returns>
        public ContactMaster Create()
        {
            return orthoEntities.ContactMasters.CreateObject();
        }

        /// <summary>
        /// Method to check valid EmailID on edit 
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool IsEditEmailIDExist(string emailID, long contactID)
        {
            if (orthoEntities.ContactMasters.Where(x => x.EmailID.Equals(emailID.Trim(), StringComparison.OrdinalIgnoreCase) && x.ContactId != contactID).FirstOrDefault() != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method to check Email Exist on update
        /// </summary>
        /// <param name="emailID"></param>
        /// <param name="contactID"></param>
        /// <returns></returns>
        public bool IsEditedEmailIDExist(string emailID, long contactID)
        {
            if (orthoEntities.ContactMasters.Where(x => x.EmailID.Equals(emailID, StringComparison.OrdinalIgnoreCase) && x.ContactId != contactID).Count() != 0)
            {
                return true;
            }            
            return false;
        }


        /// <summary>
        /// Method to check valid EmailID on Add 
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool IsAddEmailIDExist(string emailID)
        {
            if (orthoEntities.ContactMasters.Any(x => x.EmailID == emailID))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Method to get contact information by contact id
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public ContactMaster GetContactById(long? contactId)
        {
            return orthoEntities.ContactMasters.Where(x => x.ContactId == contactId).FirstOrDefault();
        }

        /// <summary>
        /// Method to get All Contacts.
        /// </summary>
        /// <returns></returns>
        public List<ContactMaster> GetAllContact()
        {
            return orthoEntities.ContactMasters.OrderBy(x => x.ContactId).ToList();
        }
    }
}
