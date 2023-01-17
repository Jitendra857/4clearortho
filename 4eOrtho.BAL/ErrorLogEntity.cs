using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4eOrtho.Utility;
using _4eOrtho.DAL;
using System.Data.Objects;

namespace _4eOrtho.BAL
{
    public class ErrorLogEntity : BaseEntity
    {
        public List<usp_GetAllErrorlog_Result> GetAllGalleryDetails(int pageSize, int pageIndex, string searchValue, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));

            List<usp_GetAllErrorlog_Result> lstGetAllError = orthoEntities.usp_GetAllErrorlog(pageIndex, pageSize, searchValue, TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstGetAllError;
        }

        public void DeleteError(List<Int64> lstIds)
        {
            foreach (Int64 id in lstIds)
            {
                GenericLog obj = orthoEntities.GenericLogs.Where(x => x.Id == id).FirstOrDefault();
                if (obj != null)
                {
                    orthoEntities.GenericLogs.DeleteObject(obj);
                }
            }
            orthoEntities.SaveChanges();           
        }
    }
}
