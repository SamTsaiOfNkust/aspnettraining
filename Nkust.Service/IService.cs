using Nkust.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nkust.Services
{
    public interface IService<T> where T : class 
    {
        #region 一些Repository 預設要有的行為
        int Save(T entity);
        int Delete(T entity);
        int Update(T entity);
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate = null);
        T Get(string id);

        #endregion 
    }
    /// <summary>
    /// Service服務層內容的Interface
    /// </summary>
    /// <typeparam name="V">就是ViewModel</typeparam>
    public interface IService<T,V> : IService<T>  where T:class where V : class 
    {
        #region 一些Repository 預設要有的行為
        int Save(V view);
        int Delete(V view);
        int Update(V view);
        IEnumerable<V> GetViewModel(Expression<Func<T, bool>> predicate = null);
        V GetViewModel(string id);
        #endregion 
    }
}
