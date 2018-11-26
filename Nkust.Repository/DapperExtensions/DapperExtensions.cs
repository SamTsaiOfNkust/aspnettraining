using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nkust.Repository
{
    public static class DapperExtensions
    {
        /// <summary>
        /// Inserts the specified connection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">The connection.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="param">The param.</param>
        /// <returns></returns>
        public static T Insert<T>(this IDbConnection connection, string tableName, dynamic param)
        {
            IEnumerable<T> result = SqlMapper.Query<T>(connection, DynamicQuery.GetInsertQuery(tableName, param), param);
            return result.First();
        }

        /// <summary>
        /// Updates the specified connection.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="param">The param.</param>
        public static void Update(this IDbConnection connection, string tableName, dynamic param)
        {
            SqlMapper.Execute(connection, DynamicQuery.GetUpdateQuery(tableName, param), param);
        }
        /// <summary>
        /// 可以讀取一個類別某個欄位的某個Attribute資料  https://stackoverflow.com/questions/7027613/how-to-retrieve-data-annotations-from-code-programmatically
        /// 另一篇 https://www.codeproject.com/Articles/742461/Csharp-Using-Reflection-and-Custom-Attributes-to-M
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static T GetAttributeFrom<T>(this object instance, string propertyName) where T : Attribute
        {
            var attrType = typeof(T);
            var property = instance.GetType().GetProperty(propertyName);
            return (T)property.GetCustomAttributes(attrType, false).First();
        }
        /// <summary>
        /// 為了判斷ID的欄位名稱，利用讀取Data Annotation
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string  GetIDName(this object instance) 
        {
            var attrType = typeof(System.ComponentModel.DataAnnotations.KeyAttribute);
            foreach (var property in instance.GetType().GetProperties())
            {
                if  (property.GetCustomAttributes(attrType, false) != null)
                {
                    return property.Name;
                }
            }
            return string.Empty;
        }
    }
}