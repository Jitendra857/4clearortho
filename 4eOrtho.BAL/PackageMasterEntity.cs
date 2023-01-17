using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Objects;
using _4eOrtho.DAL;
namespace _4eOrtho.BAL
{
    public class PackageMasterEntity : BaseEntity
    {
        public PackageMasterEntity()
            : base()
        {

        }
        /// <summary>
        /// Method to Get All list of Package Master with paging
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public List<PackageMasterDetails> GetAllPackageMaster(string sortField, string sortDirection, int pageSize, int pageIndex, string searchField, string searchValue, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));
            List<PackageMasterDetails> lstPackageDetail = orthoEntities.GetPackageMaster(pageIndex, pageSize, sortField, sortDirection, searchField, searchValue, TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount.Value) ? 0 : (int)TotalRecCount.Value;
            return lstPackageDetail;
        }
        /// <summary>
        /// Get package by packageId
        /// </summary>
        /// <param name="packageId"></param>
        /// <returns></returns>
        public PackageMaster GetPackageByPackageId(long packageId)
        {
            return orthoEntities.PackageMasters.Where(x => x.PackageId == packageId).SingleOrDefault();
        }
        /// <summary>
        /// Get all package details
        /// </summary>
        /// <returns></returns>
        public List<PackageMaster> GetPackageMaster()
        {
            return orthoEntities.PackageMasters.Where(x => x.IsActive).ToList();
        }

        /// <summary>
        /// Check Duplicate Product Name
        /// </summary>
        /// <param name="countryName"></param>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public bool IsDuplicatePackage(string packageName, long packageId)
        {
            if (orthoEntities.PackageMasters.Where(x => x.PackageName.Equals(packageName, StringComparison.OrdinalIgnoreCase) && x.PackageId != packageId).FirstOrDefault() != null)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        ///  Method to create instance of Product Master 
        /// </summary>
        /// <returns></returns>
        public PackageMaster Create()
        {
            return orthoEntities.PackageMasters.CreateObject();
        }
        /// <summary>
        /// Method to save country in add and edit condition
        /// </summary>
        /// <param name="emrCountry"></param>
        public long Save(PackageMaster packageMaster)
        {
            if (packageMaster.EntityState == System.Data.EntityState.Detached)
            {
                packageMaster.CreatedDate = BaseEntity.GetServerDateTime;
                packageMaster.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToPackageMasters(packageMaster);
            }
            else
            {
                packageMaster.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
            return packageMaster.PackageId;
        }
        /// <summary>
        /// Method to Delete Product 
        /// </summary>
        /// <param name="emrCountry"></param>
        public void Delete(PackageMaster packageMaster)
        {
            orthoEntities.DeleteObject(packageMaster);
            orthoEntities.SaveChanges();
        }
        public List<PackageDetails> GetPackageDetailsByPackageId(long packageId)
        {
            return orthoEntities.GetPackageDetails(packageId).ToList();
        }
    }
}
