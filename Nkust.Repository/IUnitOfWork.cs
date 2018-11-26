using System;
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
    /// 實作Unit Of Work的interface。
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; }
        /// <summary>
        /// 儲存所有異動。
        /// </summary>
        int SaveChanges();

    }
}
