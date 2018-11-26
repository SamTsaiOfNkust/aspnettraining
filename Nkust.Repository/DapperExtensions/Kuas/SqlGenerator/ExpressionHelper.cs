using System;
using System.Linq.Expressions;
using System.Reflection;

namespace KUAS.Dapper
{ 
    public static class ExpressionHelper
    {
        /// <summary>
        /// Binary Expression
        /// </summary>
        /// <param name="expression">Lambda Expression</param>
        /// <returns> BinaryExpression</returns>
        public static BinaryExpression GetBinaryExpression(Expression expression)
        {
            var binaryExpression = expression as BinaryExpression;
            var body = binaryExpression ?? Expression.MakeBinary(ExpressionType.Equal, expression, Expression.Constant(true));
            return body;
        }
        /// <summary>
        /// Get Primitive Properties Predicate
        /// </summary>
        /// <returns>The property. </returns>
        public static Func<PropertyInfo, bool> GetPrimitivePropertiesPredicate()
        {
            return p => p.CanWrite &&
            (p.PropertyType.IsValueType() 
            || p.PropertyType.Name.Equals("String", StringComparison.OrdinalIgnoreCase));
        }
        /// <summary>
        /// Get Property
        /// </summary>
        /// <param name="lambda">BinaryExpression</param>
        /// <returns>The property  for the property expression.</returns>
        public static MemberInfo GetProperty(BinaryExpression lambda)
        {
            Expression expression = lambda;
            for (;;)
            {
                switch (expression.NodeType)
                {
                    case ExpressionType.Lambda:
                        expression = ((LambdaExpression)expression).Body;
                        break;

                    case ExpressionType.Convert:
                        expression = ((UnaryExpression)expression).Operand;
                        break;

                    case ExpressionType.MemberAccess:
                        MemberExpression memberExpression = (MemberExpression)expression;
                        MemberInfo memberInfo = memberExpression.Member;
                        return memberInfo;

                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <returns>The property name for the property expression.</returns>
        public static string GetPropertyName(BinaryExpression body)
        {
            MemberExpression expression = body.Left as MemberExpression;
            return expression?.Member.Name;

        }
        /// <summary>
        /// Get Property Name
        /// </summary>
        /// <typeparam name="TSource">expresspn</typeparam>
        /// <typeparam name="TField">operator</typeparam>
        /// <param name="field"></param>
        /// <returns>The property name for the property expression.</returns>
        public static string GetPropertyName<TSource, TField>(Expression<Func<TSource, TField>> field)
        {
            if (Equals(field, null))
            {
                throw new NullReferenceException("Field is required");
            }

            if (field.Body is MemberExpression)
            {
                return ((MemberExpression)field.Body).Member.Name;
            }

            if (field.Body is UnaryExpression)
            {
                var expression = ((UnaryExpression)field.Body);
                BinaryExpression operand = expression.Operand as BinaryExpression;
                return ((MemberExpression)operand.Left).Member.Name;
            }

            throw new ArgumentException($"Expression '{field}' not supported.", nameof(field));

           
        }
        /// <summary>
        ///  Get Sql Operator
        /// </summary>
        /// <param name="type">operator</param>
        /// <returns></returns>
        public static string GetSqlOperator(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.Equal:
                    return "=";

                case ExpressionType.NotEqual:
                    return "!=";

                case ExpressionType.LessThan:
                    return "<";

                case ExpressionType.LessThanOrEqual:
                    return "<=";

                case ExpressionType.GreaterThan:
                    return ">";

                case ExpressionType.GreaterThanOrEqual:
                    return ">=";

                case ExpressionType.AndAlso:
                case ExpressionType.And:
                    return "AND";

                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return "OR";

                case ExpressionType.Not:
                    return "= false";

                case ExpressionType.Default:
                    return string.Empty;

                default:
                    throw new NotImplementedException();
            }
        }
        /// <summary>
        /// Get Value
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static object GetValue(Expression member)
        {
            var objectMember = Expression.Convert(member, typeof(object));
            var getterLambda = Expression.Lambda<Func<object>>(objectMember);
            var getter = getterLambda.Compile();
            return getter();
        }
    }
}