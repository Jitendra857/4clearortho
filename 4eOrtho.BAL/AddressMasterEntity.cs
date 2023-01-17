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
    /// 1. Patient Address Details
    /// 2. Address Infomration ID will Stored in Patient Details
    /// </summary>
    public class AddressMasterEntity : BaseEntity
    {       

        /// <summary>
        ///  Method to save Address in add and edit condition
        /// </summary>
        /// <param name="customer"></param>
        public long Save(AddressMaster address)
        {
            if (address.EntityState == System.Data.EntityState.Detached)
            {
                address.CreatedDate = BaseEntity.GetServerDateTime;
                address.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToAddressMasters(address);
            }
            else
            {
                address.LastUpdatedDate = BaseEntity.GetServerDateTime;

            }
            orthoEntities.SaveChanges();
            return address.AddressId;
        }

        /// <summary>
        ///  Method to create instance of Address
        /// </summary>
        /// <returns></returns>
        public AddressMaster Create()
        {
            return orthoEntities.AddressMasters.CreateObject();
        }

        /// <summary>
        /// Method to get address by addressid
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public AddressMaster GetAddressbyId(long? addressId)
        {
            return orthoEntities.AddressMasters.Where(x => x.AddressId == addressId).FirstOrDefault();
        }

        ///// <summary>
        ///// Method to get all address 
        ///// </summary>
        ///// <returns></returns>
        //public List<AddressMaster> GetAllAddress()
        //{
        //    return orthoEntities.AddressMasters.OrderBy(x => x.AddressId).ToList();
        //}
    }
}
