using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4eOrtho.DAL;

namespace _4eOrtho.BAL
{
    public class ProductPackageMasterEntity : BaseEntity
    {
        public ProductPackageMasterEntity()
            : base()
        {

        }
        /// <summary>
        /// Get Product Package by productpackageId
        /// </summary>
        /// <param name="productPackageId"></param>
        /// <returns></returns>
        public ProductPackageMaster GetProductPackageByProductId(long productPackageId)
        {
            return orthoEntities.ProductPackageMasters.Where(x => x.ProductPackageId == productPackageId).SingleOrDefault();
        }
        /// <summary>
        /// Get Product Package details by product pacakageId
        /// </summary>
        /// <param name="packageId"></param>
        /// <returns></returns>
        public List<ProductPackageDetails> GetProductPackageDetailsByPackageId(long packageId)
        {
            return orthoEntities.GetProductPackageDetails(packageId).ToList();
        }
        /// <summary>
        /// Get Product Package List
        /// </summary>
        /// <returns></returns>
        public List<ProductPackageMaster> GetProductPackage()
        {
            return orthoEntities.ProductPackageMasters.Where(x=>x.IsActive).ToList();
        }
        /// <summary>
        ///  Method to create instance of Product Package Master 
        /// </summary>
        /// <returns></returns>
        public ProductPackageMaster Create()
        {
            return orthoEntities.ProductPackageMasters.CreateObject();
        }

        /// <summary>
        /// Method to save country in add and edit condition
        /// </summary>
        /// <param name="emrCountry"></param>
        public void Save(ProductPackageMaster productPackageMaster)
        {
            if (productPackageMaster.EntityState == System.Data.EntityState.Detached)
            {
                productPackageMaster.CreatedDate = BaseEntity.GetServerDateTime;
                productPackageMaster.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToProductPackageMasters(productPackageMaster);
            }
            else
            {
                productPackageMaster.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
        }
        /// <summary>
        /// Method to Delete Product 
        /// </summary>
        /// <param name="emrCountry"></param>
        public void Delete(ProductPackageMaster productPackageMaster)
        {
            orthoEntities.DeleteObject(productPackageMaster);
            orthoEntities.SaveChanges();
        }
        /// <summary>
        /// Delete All Prodcut Package as per packageId
        /// </summary>
        /// <param name="packageId"></param>
        public void DeleteAllProductPackage(long packageId)
        {
            List<ProductPackageMaster> lstProductPackage = new List<ProductPackageMaster>();
            lstProductPackage = orthoEntities.ProductPackageMasters.Where(x => x.PackageId == packageId).ToList();
            foreach (ProductPackageMaster item in lstProductPackage)
            {
                orthoEntities.DeleteObject(item);
                orthoEntities.SaveChanges();
            }
        }
     
    }
}
