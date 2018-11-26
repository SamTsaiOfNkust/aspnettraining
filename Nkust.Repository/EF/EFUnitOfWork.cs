using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nkust.Repository
{
    // https://garywoodfine.com/generic-repository-pattern-net-core/
    /// <summary>
    /// 實作Entity Framework Unit Of Work的class
    /// </summary>
    public class EFUnitOfWork : IEFUnitOfWork
    {
        public DbContext Context
        {
            get;
            private set;
        }

        public IDbConnection Connection {
            get
            {
                return Context.Database.Connection;
            }
        }

        private bool _disposed;

        /// <summary>
        /// 設定此Unit of work(UOF)的Context。
        /// </summary>
        /// <param name="context">設定UOF的context</param>
        public EFUnitOfWork(DbContext context)
        {
            Context = context;
        }
        
        /// <summary>
        /// 儲存所有異動。
        /// </summary>
        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        /// <summary>
        /// 清除此Class的資源。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 清除此Class的資源。
        /// </summary>
        /// <param name="disposing">是否在清理中？</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }

            _disposed = true;
        }
    }
}
