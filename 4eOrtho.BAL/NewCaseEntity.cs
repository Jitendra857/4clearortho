using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4eOrtho.DAL;
using System.Data.Objects;

namespace _4eOrtho.BAL
{
    public class NewCaseEntity : BaseEntity
    {
        /// <summary>
        /// for save data in become provider
        /// </summary>
        /// <param name="recmdDentist"></param>
        public void Save(NewCase newcase)
        {
            if (newcase.EntityState == System.Data.EntityState.Detached)
            {
                newcase.CreatedDate = BaseEntity.GetServerDateTime;
                newcase.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToNewCases(newcase);
            }
            else
            {
                newcase.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
        }
        /// <summary>
        /// create instance of become provider
        /// </summary>
        /// <returns></returns>
        public NewCase Create()
        {
            return orthoEntities.NewCases.CreateObject();
        }

        public List<NewCaseDetails> GetNewCaseDetails(int pageIndex, int pageSize, string sortField, string sortDirection, string searchField, string searchValue, string emailId, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));
            List<NewCaseDetails> lstNewCaseDetails = orthoEntities.GetNewCaseDetails(pageIndex, pageSize, sortField, sortDirection, searchField, searchValue, emailId, TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstNewCaseDetails;
        }

        public NewCase GetCaseById(long caseId)
        {
            return orthoEntities.NewCases.Where(x => x.CaseId == caseId).FirstOrDefault();
        }

        public List<AllDoctorByNewCase> GetDoctorListByNewCase()
        {
            return orthoEntities.GetAllDoctorByNewCase().ToList();
        }
    }
}
