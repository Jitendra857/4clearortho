using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using _4eOrtho.DAL;

namespace _4eOrtho.BAL
{
    public class BaseEntity
    {
        public OrthoEntities orthoEntities = null;

        public BaseEntity()
        {
            if (orthoEntities == null)
                orthoEntities = ObjectContextFactory.GetWebRequestScopedDataContext<OrthoEntities>("BasePageKey");
        }

        public BaseEntity(string connectionString)
        {
            if (orthoEntities == null)
                orthoEntities = ObjectContextFactory.GetWebRequestScopedDataContext<OrthoEntities>("BasePageKey", connectionString);
        }

        /// <summary>
        /// Get Server DateTime
        /// </summary>
        public static DateTime GetServerDateTime
        {
            get
            {
                BaseEntity context = new BaseEntity();
                return context.orthoEntities.GetServerDateTime().FirstOrDefault().Value;
            }
        }
    }



    public static class ExtensionMethods
    {

        /// <summary>
        /// Perform custom paging using LINQ to SQL
        /// </summary>
        /// <typeparam name="T">Type of the Datasource to be paged</typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="obj">Object to be paged through</param>
        /// <param name="page">Page Number to fetch</param>
        /// <param name="pageSize">Number of rows per page</param>
        /// <param name="keySelector">Sorting Expression</param>
        /// <param name="asc">Sort ascending if true. Otherwise descending</param>
        /// <param name="rowsCount">Output parameter hold total number of rows</param>
        /// <returns>Page of result from the paged object</returns>
        public static IQueryable<T> Page<T, TResult>(this IQueryable<T> obj,
                        int page, int pageSize,
                        Expression<Func<T, TResult>> keySelector,
                        bool asc, out int rowsCount)
        {
            rowsCount = obj.Count();
            int innerRows = rowsCount - (page * pageSize);
            if (asc)
                return obj.OrderByDescending(keySelector)
                          .Take(innerRows)
                          .OrderBy(keySelector)
                          .Take(pageSize)
                          .AsQueryable();
            else
                return obj.OrderBy(keySelector)
                          .Take(innerRows)
                          .OrderByDescending(keySelector)
                          .Take(pageSize)
                          .AsQueryable();
        }

        /// <summary>
        /// Perform custom paging using LINQ to SQL without number
        /// </summary>
        /// <typeparam name="T">Type of the Datasource to be paged</typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="obj">Object to be paged through</param>
        /// <param name="page">Page Number to fetch</param>
        /// <param name="pageSize">Number of rows per page</param>
        /// <param name="keySelector">Sorting Expression</param>
        /// <param name="asc">Sort ascending if true. Otherwise descending</param>
        /// <returns>Page of result from the paged object</returns>
        public static IQueryable<T> Page<T, TResult>(this IQueryable<T> obj,
                        int page, int pageSize,
                        Expression<Func<T, TResult>> keySelector,
                        bool asc)
        {
            int rowsCount = obj.Count();
            int innerRows = rowsCount - (page * pageSize);
            if (asc)
                return obj.OrderByDescending(keySelector)
                          .Take(innerRows)
                          .OrderBy(keySelector)
                          .Take(pageSize)
                          .AsQueryable();
            else
                return obj.OrderBy(keySelector)
                          .Take(innerRows)
                          .OrderByDescending(keySelector)
                          .Take(pageSize)
                          .AsQueryable();
        }

    }
}
