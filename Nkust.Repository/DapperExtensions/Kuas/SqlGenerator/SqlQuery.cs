using System.Collections.Generic;
using System.Text;

namespace KUAS.Dapper
{
    /// <summary>
    /// A object with the generated sql and dynamic params.
    /// </summary>
    public class SqlQuery
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="param">The param.</param>
        public SqlQuery(string sql, dynamic param)
        {
            this.Parameters = param;
            this.Sql = sql;
        }

        /// <summary>
        /// Gets the param, for Select
        /// </summary>
        /// <value>
        /// The param.
        /// </value>
        public object Parameters { get; private set; }

        /// <summary>
        /// Gets the SQL.
        /// </summary>
        /// <value>
        /// The SQL.
        /// </value>
        public string Sql { get; private set; }

        /// <summary>
        /// Append string in current SQL query
        /// </summary>
        public void AppendToSql(string sql)
        {
            var SqlBuilder = new StringBuilder(this.Sql);
            SqlBuilder.AppendLine(sql);
            this.Sql = SqlBuilder.ToString();
        }

        /// <summary>
        /// Append string in current SQL query
        /// </summary>
        public void AppendToSql(IEnumerable<string> sqlStrings)
        {
            var sqlBuilder = new StringBuilder(this.Sql);
            foreach (var s in sqlStrings)
            {
                sqlBuilder.AppendLine(s);
            }
            this.Sql = sqlBuilder.ToString();
        }

        /// <summary>
        /// Set alternative param
        /// </summary>
        /// <param name="parameters"></param>
        public void SetParameters(dynamic parameters)
        {
            this.Parameters = parameters;
        }
    }
}