//using Sybase.Data.AseClient;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace KUAS.Dapper
{
    public class DbContext : IDbContext
    {
        /// <summary>
        /// DB Connection for internal use
        /// </summary>
        protected readonly IDbConnection InnerConnection;

        private IDbConnection _connection;

        /// <summary>
        /// Constructor
        /// </summary>
        public DbContext(string connectionName, EDbConnector dbConnector)
        {
            string connectionStrings = string.IsNullOrEmpty(connectionName) ?
                ConfigurationManager.ConnectionStrings["Default"].ConnectionString :
                ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;

            InitDbConnector(connectionStrings, dbConnector);
            InnerConnection = _connection;
            DbConnector = dbConnector;
        }

        public DbContext(string connectionName)
        {
            string connectionStrings = string.IsNullOrEmpty(connectionName) ?
                   ConfigurationManager.ConnectionStrings["Default"].ConnectionString :
                   ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;

            InitDbConnector(connectionStrings, EDbConnector.Mssql);
            InnerConnection = _connection;
        }

        /// <summary>
        /// Get opened DB Connection
        /// </summary>
        public virtual IDbConnection Connection
        {
            get
            {
                OpenConnection();
                return InnerConnection;
            }
        }

        public EDbConnector DbConnector { get; set; }

        /// <summary>
        /// Open DB connection and Begin transaction
        /// </summary>
        public virtual IDbTransaction BeginTransaction()
        {
            return Connection.BeginTransaction();
        }

        /// <summary>
        /// Close DB connection
        /// </summary>
        public void Dispose()
        {
            if (InnerConnection != null && InnerConnection.State != ConnectionState.Closed)
                InnerConnection.Close();
        }

        /// <summary>
        /// Open DB connection
        /// </summary>
        public void OpenConnection()
        {
            if (InnerConnection.State != ConnectionState.Open && InnerConnection.State != ConnectionState.Connecting)
                InnerConnection.Open();
        }

        private void InitDbConnector(string connectionString, EDbConnector dbConnector)
        {
            switch (dbConnector)
            {
                case EDbConnector.Mssql:
                    _connection = new SqlConnection(connectionString);
                    break;

                //case EDbConnector.Sybase:
                    //_connection = new AseConnection(connectionString);
                    //break;

                default:
                    throw new System.ArgumentOutOfRangeException(nameof(dbConnector));
            }
        }
    }
}