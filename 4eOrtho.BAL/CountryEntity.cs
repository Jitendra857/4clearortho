using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4eOrtho.Utility;
using _4eOrtho.DAL;
using System.Data.Objects;

namespace _4eOrtho.BAL
{
    /// <summary>
    /// File Name:  StateEntity.cs
    /// File Description: this page contains business logic for Country Table related data.
    /// ----------------------------------------------------------------------------------------------------------
    /// Date Created    : 21-07-2014
    /// Author		    : Piyush Makvana, Verve Systems PVT LTD
    /// ----------------------------------------------------------------------------------------------------------
    /// Change History
    /// Date Changed  	Changed By		Description
    /// ----------------------------------------------------------------------------------------------------------
    /// 
    /// </summary>
    public class CountryEntity : BaseEntity
    {
        public WSB_Country Create()
        {
            return orthoEntities.WSB_Country.CreateObject();
        }

        public List<WSB_Country> GetAllCountry()
        {
            return orthoEntities.WSB_Country.Where(x => x.IsActive == true).ToList();
        }

        public void Save(WSB_Country country)
        {
            if (country.EntityState == System.Data.EntityState.Detached)
            {
                country.CreatedDate = BaseEntity.GetServerDateTime;
                country.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToWSB_Country(country);
            }
            else
            {
                country.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
        }

        public int GetCountryIdByName(int id, string countryName)
        {
            WSB_Country country;
            try
            {
                if (id == 0)
                    country = orthoEntities.WSB_Country.Where(x => x.CountryName.Equals(countryName)).FirstOrDefault();
                else
                    country = orthoEntities.WSB_Country.Where(x => x.CountryName.Equals(countryName) && x.CountryId != id).FirstOrDefault();

                return country != null ? Convert.ToInt32(country.CountryId) : 0;
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return 0;
            }
        }        

        public List<AllCountryDetail> GetCountryDetail(string sortField, string sortDirection,int pageSize, int pageIndex,string searchField, string searchValue, out int totalRecords)
        {
            ObjectParameter TotalRecCount = new ObjectParameter("TotalRecCount", Type.GetType("System.Int32"));

            List<AllCountryDetail> lstGetCountryDetail = orthoEntities.GetAllCountryDetail(pageIndex, pageSize, sortField, sortDirection, searchField, searchValue, TotalRecCount).ToList();
            totalRecords = Convert.IsDBNull(TotalRecCount) ? 0 : (int)TotalRecCount.Value;
            return lstGetCountryDetail;
        }

        public WSB_Country GetCountryByCountryId(int CountryId)
        {
            return orthoEntities.WSB_Country.Where(x => x.CountryId == CountryId).FirstOrDefault();
        }       
    }
}
