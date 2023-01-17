using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using _4eOrtho.DAL;
namespace _4eOrtho.BAL
{
    public class CourseEntity : BaseEntity
    {
        /// <summary>
        /// method for bind list.
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<TakeCourseDetails> GetTakeCourseDetails(string sortField, string sortDirection, int pageSize, int pageIndex, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));
            List<TakeCourseDetails> lstTakeCourseDetails = orthoEntities.GetTakeCourseDetails(pageIndex, pageSize, sortField, sortDirection, TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstTakeCourseDetails;
        }

        /// <summary>
        /// method for Getting All Cource For Admin.
        /// </summary>
        public List<GetAllCourceForAdmin> GetAllCourceForAdmin()
        {
            return orthoEntities.GetAllCourceForAdmin().ToList();
        }

        /// <summary>
        /// method for Getting All sub Cource For Admin.
        /// </summary>
        public List<GetAllSubCourceForAdmin> GetAllSubCourceForAdmin(long id)
        {
            return orthoEntities.GetAllSubCourceForAdmin(id).ToList();
        }

    }
}
