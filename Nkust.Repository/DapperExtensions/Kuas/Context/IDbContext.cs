using System.Data;

namespace KUAS.Dapper
{
    public interface IDbContext
    {
        IDbConnection Connection { get; }
        EDbConnector DbConnector { get; set; }

        IDbTransaction BeginTransaction();

        void Dispose();

        void OpenConnection();
    }
}