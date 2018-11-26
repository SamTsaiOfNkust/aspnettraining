using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace KUAS.Dapper
{
    /// <summary>
    /// Protery 
    /// </summary>
    public class SqlPropertyMetadata
    {
        /// <summary>
        /// Metadata of Property
        /// </summary>
        public SqlPropertyMetadata(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;
            var alias = PropertyInfo.GetCustomAttribute<ColumnAttribute>();
            if (alias != null)
            {
                this.Alias = alias.Name;
                this.ColumnName = this.Alias;
            }
            else
            {
                this.ColumnName = PropertyInfo.Name;
            }
        }

        /// <summary>
        /// Alias of Column
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        ///  Column Name
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Name of Proerty
        /// </summary>
        public string Name => PropertyInfo.Name;

        /// <summary>
        ///  Info of Property
        /// </summary>
        public PropertyInfo PropertyInfo { get; }
    }
}