using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Utility;

namespace _4eOrtho.BAL
{
    /// <summary>
    /// File Name:  ClientTestimonialEntity.cs
    /// File Description: entity used for clientTestimonial table
    /// ----------------------------------------------------------------------------------------------------------
    /// Date Created    : 22-07-2014
    /// Author		    : Piyush Makvana, Verve Systems PVT LTD
    /// ----------------------------------------------------------------------------------------------------------
    /// Change History
    /// Date Changed  	Changed By		Description
    /// ----------------------------------------------------------------------------------------------------------
    /// 
    /// </summary>
    public class ClientTestimonialEntity : BaseEntity
    {
        /// <summary>
        ///mwthod for  crete object.
        /// </summary>
        /// <returns></returns>
        public ClientTestimonial Create()
        {
            return orthoEntities.ClientTestimonials.CreateObject();
        }
        /// <summary>
        /// method for save record.
        /// </summary>
        /// <param name="clienttestimonial"></param>
        public void Save(ClientTestimonial clienttestimonial)
        {
            if (clienttestimonial.EntityState == System.Data.EntityState.Detached)
            {
                clienttestimonial.LastUpdatedDate = BaseEntity.GetServerDateTime;
                clienttestimonial.CreatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToClientTestimonials(clienttestimonial);
            }
            else
            {
                clienttestimonial.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
        }
        /// <summary>
        /// get clienttestimoniealid by id
        /// </summary>
        /// <param name="clienttestimonialId"></param>
        /// <returns></returns>
        public ClientTestimonial GetClientTestimonialByID(int clienttestimonialId)
        {
            return orthoEntities.ClientTestimonials.Where(x => x.ClientTestimonialId == clienttestimonialId).FirstOrDefault();
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
        public List<AllClientTestimonial> GetClienttestimonialDetail(string sortField, string sortDirection, int pageSize, int pageIndex, string searchField, string searchValue, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));

            List<AllClientTestimonial> lstGetClienttestimonialDetail = orthoEntities.GetAllClientTestimonial(pageIndex, pageSize, sortField, sortDirection, searchField, searchValue, TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstGetClienttestimonialDetail;
        }
        /// <summary>
        /// Description : Method to delete object
        /// </summary>
        /// <param name="pageDetail"></param>
        public void Delete(ClientTestimonial clienttetimonial)
        {
            orthoEntities.DeleteObject(clienttetimonial);
            //clienttetimonial.IsDelete = true;
            orthoEntities.SaveChanges();
        }
        public List<ClientTestimonial> GetAllActiveTestimonial(string userType)
        {
            return orthoEntities.ClientTestimonials.Where(x => x.IsActive == true && x.UserType == userType).ToList();
        }
    }
}
