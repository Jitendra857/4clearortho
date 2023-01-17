using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4eOrtho.Utility;
using _4eOrtho.DAL;
using System.Data.Objects;

namespace _4eOrtho.BAL
{
    public class DiscountMasterEntity : BaseEntity
    {
        /// <summary>
        /// Cretae case charge entity object.
        /// </summary>
        /// <returns></returns>
        public DiscountMaster Create()
        {
            return orthoEntities.DiscountMasters.CreateObject();
        }

        /// <summary>
        /// Method to save case charge record.
        /// </summary>
        /// <param name="discountMaster"></param>
        public void Save(DiscountMaster discountMaster)
        {
            if (discountMaster.EntityState == System.Data.EntityState.Detached)
            {
                discountMaster.CreatedDate = BaseEntity.GetServerDateTime;
                discountMaster.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToDiscountMasters(discountMaster);
            }
            else
            {
                discountMaster.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
        }

        public DiscountMaster GetDiscountMaster(long discountId)
        {
            return orthoEntities.DiscountMasters.Where(x => x.DiscountId == discountId).FirstOrDefault();
        }
        public DiscountMaster GetDiscountMaster(string couponCode)
        {
            return orthoEntities.DiscountMasters.Where(x => x.CouponCode == couponCode).FirstOrDefault();
        }
    }
}
