using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Nkust.Repository
{
    // 參考 https://ithelp.ithome.com.tw/articles/10157484
    // https://garywoodfine.com/generic-repository-pattern-net-core/
    /// <summary>
    /// 實作Entity Framework Generic Repository 的 Class。
    /// </summary>
    /// <typeparam name="TEntity">EF Model 裡面的Type</typeparam>
    public class EFGenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public readonly IEFUnitOfWork _UnitOfWork;

        /// <summary>
        /// 建構EF一個Entity的Repository，需傳入負責Transaction的IUnitOfWork。
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork</param>
        public EFGenericRepository(IEFUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// 新增一筆資料到資料庫。
        /// </summary>
        /// <param name="entity">要新增到資料的庫的Entity</param>
        public void Create(TEntity entity)
        {
            _UnitOfWork.Context.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// 取得第一筆符合條件的內容。如果符合條件有多筆，也只取得第一筆。
        /// </summary>
        /// <param name="predicate">要取得的Where條件。</param>
        /// <returns>取得第一筆符合條件的內容。</returns>
        public IEnumerable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return _UnitOfWork.Context.Set<TEntity>().Where(predicate).AsEnumerable<TEntity>();
        }
        public IEnumerable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> OrderBy, int Skip = 0, int Take = -1)
        {
            if (Take == -1)
            {
                return _UnitOfWork.Context.Set<TEntity>().Where(predicate).OrderBy(OrderBy).Skip(Skip).AsEnumerable<TEntity>();
            }
            else
            {
                return _UnitOfWork.Context.Set<TEntity>().Where(predicate).OrderBy(OrderBy).Skip(Skip).Take(Take).AsEnumerable<TEntity>();
            }
        }
        public IEnumerable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, string>> OrderBy, int Skip = 0, int Take = -1)
        {
            if (Take == -1)
            {
                return _UnitOfWork.Context.Set<TEntity>().Where(predicate).OrderBy(OrderBy).Skip(Skip).AsEnumerable<TEntity>();
            }
            else
            {
                return _UnitOfWork.Context.Set<TEntity>().Where(predicate).OrderBy(OrderBy).Skip(Skip).Take(Take).AsEnumerable<TEntity>();
            }
        }

        /// <summary>
        /// 取得Entity全部筆數的IQueryable。
        /// </summary>
        /// <returns>Entity全部筆數的IQueryable。</returns>
        public IEnumerable<TEntity> Get()
        {
            return _UnitOfWork.Context.Set<TEntity>().AsEnumerable<TEntity>();
        }
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, int>> OrderBy, int Skip = 0, int Take = -1)
        {
            if (Take == -1)
            {
                return _UnitOfWork.Context.Set<TEntity>().OrderBy(OrderBy).Skip(Skip).AsEnumerable<TEntity>();
            }
            else
            {
                return _UnitOfWork.Context.Set<TEntity>().OrderBy(OrderBy).Skip(Skip).Take(Take).AsEnumerable<TEntity>();
            }
        }
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, string>> OrderBy, int Skip = 0, int Take = -1)
        {
            if (Take == -1)
            {
                return _UnitOfWork.Context.Set<TEntity>().OrderBy(OrderBy).Skip(Skip).AsEnumerable<TEntity>();
            }
            else
            {
                return _UnitOfWork.Context.Set<TEntity>().OrderBy(OrderBy).Skip(Skip).Take(Take).AsEnumerable<TEntity>();
            }
        }
        /// <summary>
        /// 更新一筆Entity內容。
        /// </summary>
        /// <param name="entity">要更新的內容</param>
        public void Update(TEntity entity)
        {
            _UnitOfWork.Context.Entry(entity).State = EntityState.Modified;
            //_UnitOfWork.Context.Set<TEntity>().Attach(entity); // 加了 SQL Server 反而不能更新，Oracle 沒有測試
        }

        /// <summary>
        /// 刪除一筆資料內容。
        /// </summary>
        /// <param name="entity">要被刪除的Entity。</param>
        public void Delete(TEntity entity)
        {
            _UnitOfWork.Context.Entry<TEntity>(entity).State = EntityState.Deleted;
            /*
            TEntity existing = _UnitOfWork.Context.Set<TEntity>().Find(entity); // 沒有設定Key ，這樣會出錯
            if (existing != null) _UnitOfWork.Context.Set<TEntity>().Remove(existing);
            */
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _UnitOfWork.Context.Set<TEntity>().RemoveRange(entities);
        }

        public int Count()
        {
            return _UnitOfWork.Context.Set<TEntity>().Count();
        }

    }
}