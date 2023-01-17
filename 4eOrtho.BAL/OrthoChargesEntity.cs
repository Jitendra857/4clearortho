using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4eOrtho.Utility;
using _4eOrtho.DAL;
using System.Data.Objects;

namespace _4eOrtho.BAL
{
    public class OrthoChargesEntity : BaseEntity
    {
        /// <summary>
        /// create object method
        /// </summary>
        /// <returns></returns>
        public OrthoCharge Create()
        {
            return orthoEntities.OrthoCharges.CreateObject();
        }

        /// <summary>
        /// save record in gallery table
        /// </summary>
        /// <param name="state"></param>
        public long Save(OrthoCharge orthoCharge)
        {
            if (orthoCharge.EntityState == System.Data.EntityState.Detached)
            {
                orthoCharge.CreatedDate = BaseEntity.GetServerDateTime;
                orthoCharge.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToOrthoCharges(orthoCharge);
            }
            else
            {
                orthoCharge.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
            return orthoCharge.Id;
        }

        public OrthoCharge GetOrthoChargeById(long Id)
        {
            return orthoEntities.OrthoCharges.Where(x => x.Id == Id).FirstOrDefault();
        }

        public OrthoCharge GetOrthoChargeByCountryId(long countryId)
        {
            return orthoEntities.OrthoCharges.Where(x => x.CountryId == countryId).FirstOrDefault();
        }

        public OrthoCharge GetOrthoCharges(string caseId, long countryId)
        {            
            List<OrthoCharge> lstOrthoCharge = orthoEntities.OrthoCharges.Where(x => x.CountryId == countryId).ToList();
            if (lstOrthoCharge != null)
            {
                if (lstOrthoCharge.Count > 0)
                {
                    foreach (OrthoCharge orthoCharge in lstOrthoCharge)
                    {
                        if (orthoCharge.CaseTypeIds.Split(',').ToList().Exists(x => x == caseId))
                            return orthoCharge;
                    }
                    return lstOrthoCharge[0];
                }                
            }
            return null;
        }

        public List<OrthoCharge> GetAllOrthoCharge()
        {
            return orthoEntities.OrthoCharges.Where(x => x.IsDelete == false).ToList();
        }

        public List<GetAllOrthoCharges> GetAllOrthoCharge(string sortField, string sortDirection, int pageSize, int pageIndex, string searchValue, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));
            List<GetAllOrthoCharges> lstGetAllOrthoCharge = orthoEntities.GetAllOrthoCharges(pageIndex, pageSize, sortField, sortDirection, searchValue, TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstGetAllOrthoCharge;
        }
    }
}
