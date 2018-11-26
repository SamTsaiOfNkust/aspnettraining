using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nkust.Repository
{
    public interface IEFUnitOfWork : IUnitOfWork
    {
        DbContext Context { get; } 
    }
}
