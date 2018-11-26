using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nkust.Repository
{
    public class DBUnitOfWork : IUnitOfWork
    {
        public IDbConnection Connection { get; private set; }
        protected IDbTransaction Trans;
        public DBUnitOfWork(IDbConnection conn)
        {
            Connection = conn;
            Trans = Connection.BeginTransaction();
        }

        public int SaveChanges()
        {
            Trans.Commit();
            return 1;
        }

        private bool _disposed;
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
                    if (Trans != null)
                        Trans.Dispose();
                    // TODO  需要?
                    //Connection.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
