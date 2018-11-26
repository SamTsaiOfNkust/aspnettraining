using System;

namespace KUAS.Dapper
{
    /// <summary>
    /// Base JOIN for LEFT/INNER/RIGHT
    /// </summary>
    public abstract class JoinAttributeBase : Attribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        protected JoinAttributeBase()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        protected JoinAttributeBase(string tableName, string key, string externalKey)
        {
            TableName = tableName;
            Key = key;
            ExternalKey = externalKey;
        }

        /// <summary>
        /// External TableName
        /// </summary>
        public string ExternalKey { get; set; }

        /// <summary>
        /// Property for ForeignKey
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Current TableName
        /// </summary>
        public string TableName { get; set; }
    }
}