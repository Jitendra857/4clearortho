using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using _4eOrtho.DAL;
namespace _4eOrtho.BAL
{
    public class BecomeProviderEntity : BaseEntity
    {
        public string GetBecomeProviderStatus(string emailId)
        {
            return Convert.ToString(orthoEntities.GetDoctorBecomeProviderStatus(emailId).ToList().SingleOrDefault().Status);
        }

        public StudentDetails GetStudentDetailsByEmailId(string emailId)
        {
            return orthoEntities.GetStudentDetails(emailId).SingleOrDefault();
        }

        public void UpdateStudenteDetails(string emailId)
        {
            orthoEntities.UpdateStudentDetails(emailId);
        }

        /// <summary>
        /// for save data in become provider
        /// </summary>
        /// <param name="recmdDentist"></param>
        public void Save(BecomeProvider provider)
        {
            if (provider.EntityState == System.Data.EntityState.Detached)
            {
                provider.CreatedDate = BaseEntity.GetServerDateTime;
                provider.LastUpdateDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToBecomeProviders(provider);
            }
            else
            {
                provider.LastUpdateDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
        }

        /// <summary>
        /// create instance of become provider
        /// </summary>
        /// <returns></returns>
        public BecomeProvider Create()
        {
            return orthoEntities.BecomeProviders.CreateObject();
        }
        
        /// <summary>
        /// method for bind list.
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<BecomeProviderDetails> GetBecomeProviderList(string sortField, string sortDirection, int pageSize, int pageIndex,string searchField,string searchValue, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));
            List<BecomeProviderDetails> lstBecomeProviderList = orthoEntities.GetBecomeProviderDetails(pageIndex, pageSize, sortField, sortDirection,searchField,searchValue,TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstBecomeProviderList;
        }
    }
}
