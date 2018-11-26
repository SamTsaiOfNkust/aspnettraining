using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Dapper;

namespace Nkust.Repository
{
    // 參考 http://www.bradoncode.com/blog/2012/12/creating-data-repository-using-dapper.html
    // 另一個 https://www.codeproject.com/Articles/1186566/Dapper-Generic-Repository
    // 很難實作
    public class DapperGenericRepository<T> : IRepository<T>  where T : class
    {

        /// <summary>
        /// The _table name
        /// </summary>
        private readonly string _tableName;

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        internal IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["SmsQuizConnection"].ConnectionString);
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}" /> class.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        protected DapperGenericRepository(string tableName)
        {
            _tableName = tableName;
        }
        /// <summary>
        /// Mapping the object to the insert/update columns.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The parameters with values.</returns>
        /// <remarks>In the default case, we take the object as is with no custom mapping.</remarks>
        internal virtual dynamic Mapping(T item)
        {
            return item;
            /*    範例
            return new
            {
                ClosingDate = item.ClosingDate,
                CompetitionKey = item.CompetitionKey,
                CreatedDate = item.CreatedDate,
                Question = item.Question,
                CreatedByID = item.CreatedBy.ID,
                WinnerID = (item.Winner != null) ? item.Winner.ID : Guid.Empty,
                Status = item.State.Status,
                ID = item.ID
            };
             */
        }

        public void Create(T entity)
        {
            using (IDbConnection cn = Connection)
            {
                var parameters = (object)Mapping(entity);
                cn.Open();
                //entity.ID = cn.Insert<Guid>(_tableName, parameters);
                cn.Insert<Guid>(_tableName, parameters);
            }
        }
        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Delete(T entity)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Execute("DELETE FROM " + _tableName + " WHERE ID=@ID", new { ID = entity.GetIDName() });
            }
        }
        /// <summary>
        /// Finds by ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public virtual T FindByID(Guid id)
        {
            T item;

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                item = cn.Query<T>("SELECT * FROM " + _tableName + " WHERE ID=@ID", new { ID = id }).SingleOrDefault();
            }

            return item;
        }
        /// <summary>
        /// Finds the specified predicate.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="param">The param.</param>
        /// <returns>
        /// A list of items
        /// </returns>
        public virtual IEnumerable<T> Find(string query, dynamic param)
        {
            IEnumerable<T> items = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                items = cn.Query<T>("SELECT * FROM " + _tableName + " WHERE " + query, (object)param);
            }

            return items;
        }
        /// <summary>
        /// Finds the specified param.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns></returns>
        public virtual IEnumerable<T> Find(dynamic param)
        {
            return Find(DynamicQuery.GetWhereQuery(param), param);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Update(T entity)
        {
            using (IDbConnection cn = Connection)
            {
                var parameters = (object)Mapping(entity);
                cn.Open();
                cn.Update(_tableName, parameters);
            }
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Get()
        {
            IQueryable<T> items = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                items = cn.Query<T>("SELECT * FROM " + _tableName).AsQueryable<T>();
            }
            return items;
        }

        public IEnumerable<T> Get(Expression<Func<T, int>> OrderBy, int Skip = 0, int Take = -1)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Get(Expression<Func<T, string>> OrderBy, int Skip = 0, int Take = -1)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate, Expression<Func<T, int>> OrderBy, int Skip = 0, int Take = -1)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate, Expression<Func<T, string>> OrderBy, int Skip = 0, int Take = -1)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}