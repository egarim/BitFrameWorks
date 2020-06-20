using System;
using System.Linq;

namespace BIT.Data.Extensions
{
    /// <summary>
    ///     Extension for a attribute class
    /// </summary>
    public static class AttributeExtensions
    {
        /// <summary>
        ///     Access the values of the properties of an attribute
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="type">type of attribute</param>
        /// <param name="valueSelector">Property name</param>
        /// <returns></returns>
        public static TValue GetAttributeValue<TAttribute, TValue>(
            this Type type,
            Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(
                typeof(TAttribute), true
            ).FirstOrDefault() as TAttribute;
            if (att != null) return valueSelector(att);
            return default;
        }
    }
}