namespace KUAS.Dapper
{
    /// <summary>
    /// Class that Model the data structure in coverting the expression tree into SQL and Params.
    /// </summary>
    internal class QueryParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryParameter" /> class.
        /// </summary>
        /// <param name="linkingOperator">The linking operator.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="queryOperator">The query operator.</param>
        internal QueryParameter(string linkingOperator, string propertyName, object propertyValue, string queryOperator)
        {
            this.LinkingOperator = linkingOperator;
            this.PropertyName = propertyName;
            this.PropertyValue = propertyValue;
            this.QueryOperator = queryOperator;
        }
        /// <summary>
        /// Linking Operator
        /// </summary>
        public string LinkingOperator { get; set; }
        /// <summary>
        ///  Property Name
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        ///  PropertyValue
        /// </summary>
        public object PropertyValue { get; set; }
        /// <summary>
        /// Query Operator
        /// </summary>
        public string QueryOperator { get; set; }
    }
}