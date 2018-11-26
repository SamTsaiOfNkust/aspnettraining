using AutoMapper;
using Nkust.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Nkust.Services
{
    // 參考 https://ithelp.ithome.com.tw/articles/10158451
    /// <summary>
    /// 通用行的Service layer實作
    /// </summary>
    /// <typeparam name="T">主要的Entity形態，為了建立Generic Repository</typeparam>

    public abstract class GenericService<T> : IService<T>, IDisposable
        where T : class
    {
        protected IUnitOfWork _UnitOfWork {  get; }
        protected IRepository<T> _repos;
        protected DbContext _Context;
        public IRepository<T> _Repos
        {
            get
            {
                if (_repos == null)
                {
                    _repos = new EFGenericRepository<T>((IEFUnitOfWork)_UnitOfWork);
                }
                return _repos;
            }
        }
        protected IDbConnection _Conn
        {
            get
            {
                return ((IEFUnitOfWork)_UnitOfWork).Context.Database.Connection;
            }
        }

        protected GenericService(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
            _Context = ((IEFUnitOfWork)unitOfWork).Context;

        }
        protected GenericService(IUnitOfWork unitOfWork, IRepository<T> repos) : this(unitOfWork)
        {
            _repos = repos;
        }
        #region 主要Repository 預設的行為
        public virtual int Save(T entity)
        {
            _Repos.Create(entity);
            _UnitOfWork.SaveChanges();
            return 1;
        }


        public virtual int Delete(T entity)
        {
            _Repos.Delete(entity);
            _UnitOfWork.SaveChanges();
            return 1;
        }

        public virtual int Update(T entity)
        {
            _Repos.Update(entity);
            _UnitOfWork.SaveChanges();
            return 1;
        }


        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return _Repos.Get();
            else
                return _Repos.Get(predicate);
        }
        public abstract T Get(string Id); // 沒辦法在這裡實作
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // 偵測多餘的呼叫

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 處置受控狀態 (受控物件)。
                }

                // TODO: 釋放非受控資源 (非受控物件) 並覆寫下方的完成項。
                // TODO: 將大型欄位設為 null。


                _repos = null;
                disposedValue = true;
            }
        }

        // TODO: 僅當上方的 Dispose(bool disposing) 具有會釋放非受控資源的程式碼時，才覆寫完成項。
        // ~GenericService() {
        //   // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 加入這個程式碼的目的在正確實作可處置的模式。
        public void Dispose()
        {
            // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果上方的完成項已被覆寫，即取消下行的註解狀態。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }



    /// <summary>
    /// 通用行的Service layer實作
    /// </summary>
    /// <typeparam name="T">主要的Entity形態，為了建立Generic Repository</typeparam>
    /// <typeparam name="V">對應的ViewModel</typeparam>
    public abstract class GenericService<T, V> : GenericService<T>, IService<T, V>
        where T : class where V : class
    {
        static MapperConfiguration config;
        static IMapper mapper;

        static GenericService()
        {
            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<V, T>().ReverseMap(); // 可反轉
            });
            mapper = config.CreateMapper();
        }
        protected GenericService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        protected GenericService(IUnitOfWork unitOfWork, IRepository<T> repos) : base(unitOfWork, repos)
        {
        }

        protected virtual T ToEntity(V view)
        {
            //TODO 改成MapperUtility?
            var entity = mapper.Map<V, T>(view);
            return entity;
        }
        #region 主要Repository 預設的行為
        public virtual int Save(V view)
        {
            var entity = ToEntity(view);
            _Repos.Create(entity);
            _UnitOfWork.SaveChanges();
            return 1;
        }

        public virtual int Delete(V view)
        {
            var entity = ToEntity(view);
            _Repos.Delete(entity);
            _UnitOfWork.SaveChanges();
            return 1;
        }

        public virtual int Update(V view)
        {
            var entity = ToEntity(view);
            _Repos.Update(entity);
            _UnitOfWork.SaveChanges();
            return 1;
        }
        public abstract V GetViewModel(string Id); // 沒辦法在這裡實作

        public virtual IEnumerable<V> GetViewModel(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return _Repos.Get().Select(mapper.Map<T, V>);
            else
                return _Repos.Get(predicate).Select(mapper.Map<T, V>);
        }
        #endregion
    }
}