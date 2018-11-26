using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Nkust.Repository
{
    // 參考 https://ithelp.ithome.com.tw/articles/10157484
    // https://garywoodfine.com/generic-repository-pattern-net-core/
    /// <summary>
    /// 代表一個Repository的interface。
    /// </summary>
    /// <typeparam name="T">任意model的class</typeparam>
    public interface IRepository<T> where T: class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>取得所有資料</returns>
        IEnumerable<T> Get();
        IEnumerable<T> Get(Expression<Func<T, int>> OrderBy, int Skip = 0, int Take = -1);
        IEnumerable<T> Get(Expression<Func<T, string>> OrderBy, int Skip = 0, int Take = -1);
        /// <summary>
        /// 取得第一筆符合條件的內容。如果符合條件有多筆，也只取得第一筆。   
        /// </summary>
        /// <param name="predicate">要取得的Where條件。</param>
        /// <returns>取得符合條件的內容。</returns>
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate, Expression<Func<T, int>> OrderBy, int Skip = 0, int Take = -1);
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate, Expression<Func<T, string>> OrderBy, int Skip = 0, int Take = -1);
        /// <summary>
        /// 新增一筆資料。
        /// </summary>
        /// <param name="entity">要新增到的Entity</param>
        void Create(T entity);
        /// <summary>
        /// 更新一筆資料的內容。
        /// </summary>
        /// <param name="entity">要更新的內容</param>
        void Update(T entity);
        /// <summary>
        /// 刪除一筆資料內容。
        /// </summary>
        /// <param name="entity">要被刪除的Entity。</param>
        void Delete(T entity);
        void RemoveRange(IEnumerable<T> entities);

        int Count();
    }
}