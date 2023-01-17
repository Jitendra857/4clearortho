using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4eOrtho.Utility;
using _4eOrtho.DAL;
using System.Data.Objects;

namespace _4eOrtho.BAL
{
    public class CaseChargesEntity : BaseEntity
    {
        /// <summary>
        /// Cretae case charge entity object.
        /// </summary>
        /// <returns></returns>
        public CaseCharge Create()
        {
            return orthoEntities.CaseCharges.CreateObject();
        }

        /// <summary>
        /// Method to save case charge record.
        /// </summary>
        /// <param name="caseCharge"></param>
        public void Save(CaseCharge caseCharge)
        {
            if (caseCharge.EntityState == System.Data.EntityState.Detached)
            {
                caseCharge.CreatedDate = caseCharge.LastUpdatedDate = BaseEntity.GetServerDateTime;
                if (caseCharge.DiscountMaster != null)
                    caseCharge.DiscountMaster.CreatedDate = caseCharge.DiscountMaster.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToCaseCharges(caseCharge);
            }
            else
            {
                caseCharge.LastUpdatedDate = BaseEntity.GetServerDateTime;
                if (caseCharge.DiscountMaster != null)
                    caseCharge.DiscountMaster.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
        }

        public CaseCharge GetCaseCharge(long caseChargeId)
        {
            return orthoEntities.CaseCharges.Where(x => x.CaseChargeId == caseChargeId).FirstOrDefault();
        }

        public CaseCharge GetCaseChargeByCaseId(long caseId)
        {
            return orthoEntities.CaseCharges.Where(x => x.LookupCaseId == caseId && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
        }

        public CaseCharge GetDoctorCaseChargeByCaseId(long caseId, string doctorEmailid)
        {
            CaseCharge specialdoctorcase = orthoEntities.CaseCharges.Where(x => x.LookupCaseId == caseId && x.DoctorEmailId == doctorEmailid && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            if (specialdoctorcase != null)
            {
                return specialdoctorcase;
            }
            return orthoEntities.CaseCharges.Where(x => x.LookupCaseId == caseId && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
        }

        public CaseCharge GetSpecialCaseForDoctor(long caseId, string doctorEmailid, long CaseChargeId)
        {
            return orthoEntities.CaseCharges.Where(x => x.LookupCaseId == caseId && x.DoctorEmailId == doctorEmailid && x.CaseChargeId != CaseChargeId && x.IsActive == true && x.IsDelete == false).FirstOrDefault();                        
        }

        public List<CaseCharge> GetAllCaseCharge()
        {
            return orthoEntities.CaseCharges.Where(x => x.IsDelete == false).ToList();
        }

        public List<GetAllCaseCharges> GetAllCaseCharges(int pageIndex, int pageSize, string sortField, string sortDirection, string searchValue, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));
            List<GetAllCaseCharges> lstCaseCharges = orthoEntities.GetAllCaseCharges(pageIndex, pageSize, sortField, sortDirection, searchValue, TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstCaseCharges;
        }
    }
}
