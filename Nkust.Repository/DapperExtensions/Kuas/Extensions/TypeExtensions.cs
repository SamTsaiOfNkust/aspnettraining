using System;
using System.Reflection;

namespace KUAS.Dapper
{
    /// <summary>
        /// Column  Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
    public static class TypeExtensions
    {
        /// <summary>
        /// boolean
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsBool(this Type type)
        {
            return type == typeof(bool);
        }
        /// <summary>
        /// Enum
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEnum(this Type type)
        {
            return type.GetTypeInfo().IsEnum;
        }
        /// <summary>
        /// GenericType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsGenericType(this Type type)
        {
            return type.GetTypeInfo().IsGenericType;
        }
        /// <summary>
        /// ValueType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsValueType(this Type type)
        {
            return type.GetTypeInfo().IsValueType;
        }
    }
}