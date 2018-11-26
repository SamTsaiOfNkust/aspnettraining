using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace KUAS.Dapper
{
    /// <summary>
    /// Universal SqlGenerator for Tables
    /// </summary>
    public interface ISqlGenerator<TEntity> where TEntity : class
    {
        /// <summary>
        ///Identity
        /// </summary>
        SqlPropertyMetadata IdentitySqlProperty { get; }

        /// <summary>
        ///Is Identity
        /// </summary>
        bool IsIdentity { get; }

        /// <summary>
        /// Key  Properties
        /// </summary>
        SqlPropertyMetadata[] KeySqlProperties { get; }

        /// <summary>
        ///logicalDelete
        /// </summary>
        bool logicalDelete { get; }

        /// <summary>
        /// logical Delete Value
        /// </summary>
        object logicalDeleteValue { get; }

        /// <summary>
        /// Sql Connector
        /// </summary>
        EDbConnector SqlConnector { get; set; }

        /// <summary>
        /// Sql Properties
        /// </summary>
        SqlPropertyMetadata[] SqlProperties { get; }

        /// <summary>
        /// Status 
        /// </summary>
        string StatusPropertyName { get; }

        /// <summary>
        ///Table
        /// </summary>
        string TableName { get; }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity">model</param>
        /// <returns>sql query</returns>
        SqlQuery GetDelete(TEntity entity);

        /// <summary>
        ///  batch delete  entities
        /// </summary>
        /// <param name="entities"> model collection</param>
        /// <returns> sql query</returns>
        SqlQuery GetDeleteMany(IEnumerable<TEntity> entities);
        /// <summary>
        ///Insert
        /// </summary>
        /// <param name="entity">model</param>
        /// <returns>sql query</returns>
        SqlQuery GetInsert(TEntity entity);
        /// <summary>
        ///  batch add  entities
        /// </summary>
        /// <param name="entities">model collection</param>
        /// <returns></returns>
        SqlQuery GetInsertMany(IEnumerable<TEntity> entities);
        /// <summary>
        /// Return All Result
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns>sql query</returns>
        SqlQuery GetSelectAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///  Return Result With Where Predicates  
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="btweenFiled"></param>
        /// <param name="predicate"></param>
        /// <returns>sql query</returns>
        SqlQuery GetSelectBetween(object from, object to, Expression<Func<TEntity, object>> btweenFiled, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Return First Row Result
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns>sql query</returns>
        SqlQuery GetSelectFirst(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>sql query</returns>
        SqlQuery GetUpdate(TEntity entity);

        /// <summary>
        ///  batch update
        /// </summary>
        /// <param name="entities">model collection</param>
        /// <returns>sql query</returns>
        SqlQuery GetUpdateMany(IEnumerable<TEntity> entities);


    }
}