using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Objects;
using _4eOrtho.DAL;
namespace _4eOrtho.BAL
{
    public class ProductMasterEntity : BaseEntity
    {
        public ProductMasterEntity()
            : base()
        {

        }
        /// <summary>
        /// Method to Get List of Product Master
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public List<ProductMasterDetails> GetAllProductMaster(string sortField, string sortDirection, int pageSize, int pageIndex, string searchField, string searchValue, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));
            List<ProductMasterDetails> lstCountryDetail = orthoEntities.GetProductMaster(pageIndex, pageSize, sortField, sortDirection, searchField, searchValue, TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount.Value) ? 0 : (int)TotalRecCount.Value;
            return lstCountryDetail;
        }
        /// <summary>
        /// Get Product by product Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductMaster GetProductByProductId(long productId)
        {
            return orthoEntities.ProductMasters.Where(x => x.ProductId == productId).SingleOrDefault();
        }
        /// <summary>
        /// Get Product Master Details
        /// </summary>
        /// <returns></returns>
        public List<ProductMaster> GetProductMasters()
        {
            return orthoEntities.ProductMasters.Where(x => x.IsActive).ToList();
        }

        /// <summary>
        /// Check Duplicate Product Name
        /// </summary>
        /// <param name="countryName"></param>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public bool IsDuplicateProduct(string productName, long productId)
        {
            if (orthoEntities.ProductMasters.Where(x => x.ProductName.Equals(productName, StringComparison.OrdinalIgnoreCase) && x.ProductId != productId).FirstOrDefault() != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///  Method to create instance of Product Master 
        /// </summary>
        /// <returns></returns>
        public ProductMaster Create()
        {
            return orthoEntities.ProductMasters.CreateObject();
        }

        /// <summary>
        /// Method to save product in add and edit condition
        /// </summary>
        /// <param name="emrCountry"></param>
        public long Save(ProductMaster productMaster)
        {
            if (productMaster.EntityState == System.Data.EntityState.Detached)
            {
                productMaster.CreatedDate = BaseEntity.GetServerDateTime;
                productMaster.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToProductMasters(productMaster);
            }
            else
            {
                productMaster.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
            return productMaster.ProductId;
        }
      
        /// <summary>
        /// Method to Delete Product 
        /// </summary>
        /// <param name="emrCountry"></param>
        public void Delete(ProductMaster productMaster)
        {
            orthoEntities.DeleteObject(productMaster);
            orthoEntities.SaveChanges();
        }
    }
}
