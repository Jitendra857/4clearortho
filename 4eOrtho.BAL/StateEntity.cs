using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using _4eOrtho.DAL;
using _4eOrtho.Utility;

namespace _4eOrtho.BAL
{
    /// <summary>
    /// File Name:  StateEntity.cs
    /// File Description: this page contains business logic for State Table related data.
    /// ----------------------------------------------------------------------------------------------------------
    /// Date Created    : 21-07-2014
    /// Author		    : Piyush Makvana, Verve Systems PVT LTD
    /// ----------------------------------------------------------------------------------------------------------
    /// Change History
    /// Date Changed  	Changed By		Description
    /// ----------------------------------------------------------------------------------------------------------
    /// 
    /// </summary>
    public class StateEntity : BaseEntity
    {
        /// <summary>
        /// create object method
        /// </summary>
        /// <returns></returns>
        public WSB_State Create()
        {
            return orthoEntities.WSB_State.CreateObject();
        }
        /// <summary>
        /// save record in state table
        /// </summary>
        /// <param name="state"></param>
        public void Save(WSB_State state)
        {
            if (state.EntityState == System.Data.EntityState.Detached)
            {
                state.CreatedDate = BaseEntity.GetServerDateTime;
                state.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToWSB_State(state);
            }
            else
            {
                state.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
        }
        /// <summary>
        /// Method to Get State By countryId
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public List<WSB_State> GetStateByCountryId(long countryId)
        {
            return orthoEntities.WSB_State.Where(x => x.CountryId == countryId).OrderBy(x => x.StateName).ToList();
        }
        /// <summary>
        /// method to get state by stateid
        /// </summary>
        /// <param name="StateId"></param>
        /// <returns></returns>
        public WSB_State GetStateByStateId(int StateId)
        {
            return orthoEntities.WSB_State.Where(x => x.StateId == StateId).FirstOrDefault();
        }
        /// <summary>
        /// bind statelist.
        /// </summary>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<AllStateDetail> GetStateDetail(string sortField, string sortDirection, int pageSize, int pageIndex, string searchField, string searchValue, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));

            List<AllStateDetail> lstGetCountryDetail = orthoEntities.GetAllStateDetail(pageIndex, pageSize, sortField, sortDirection, searchField, searchValue, TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstGetCountryDetail;
        }        
        /// <summary>
        /// Method to get state by name
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        public long GetStateIdByNameAndCountry(long stateId, string stateName, int countryId)
        {
            WSB_State state;
            try
            {
                if (stateId == 0)
                    state = orthoEntities.WSB_State.Where(x => x.StateName.Equals(stateName) && x.CountryId == countryId).FirstOrDefault();
                else
                    state = orthoEntities.WSB_State.Where(x => x.StateName.Equals(stateName) && x.CountryId == countryId && x.StateId != stateId).FirstOrDefault();

                return state != null ? state.StateId : 0;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return 0;
            }
        }
    }
}
