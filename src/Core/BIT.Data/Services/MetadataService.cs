using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BIT.Data.Services
{
    /// <summary>
    ///     Contains the necessary functions to inspect and get information about assemblies and types
    /// </summary>
    public class MetadataService
    {
        /// <summary>
        ///     Get all child types (derived types) from a type
        /// </summary>
        /// <param name="assembly">The assembly where you want to find the child types</param>
        /// <param name="BaseType">The parent class of the subtypes your are looking for</param>
        /// <param name="IncludeAbstract">
        ///     Optional parameter to include abstract descendants of the base type,by default is set to
        ///     true
        /// </param>
        /// <returns>An IEnumerable<Type> containing the descendants types</returns>
        public static IEnumerable<Type> GetSubTypes(Assembly assembly, Type BaseType, bool IncludeAbstract = true)
        {
            var AllSubTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(BaseType));
            if (IncludeAbstract)
                return AllSubTypes;
            return AllSubTypes.Where(st => st.IsAbstract == false);
        }

        /// <summary>
        ///     Get all types that implements and attribute
        /// </summary>
        /// <param name="assembly">The assembly where you want to find the types</param>
        /// <param name="Attribute">The attribute type that you are looking for</param>
        /// <returns>An IEnumerable<Type> containing the types that implements the attribute</returns>
        public static IEnumerable<Type> GetTypesWithAttribute(Assembly assembly, Type Attribute)
        {
            foreach (var type in assembly.GetTypes())
                if (type.GetCustomAttributes(Attribute, true).Length > 0)
                    yield return type;
        }

        /// <summary>
        ///     Get all types that implements and attribute
        /// </summary>
        /// <param name="CurrentType">The type where the attribute might exists</param>
        /// <param name="Attribute">The attribute type that you are looking for</param>
        /// <returns>true if the type implements the attribute otherwise false</returns>
        public static bool DoesTypeImplementsAttribute(Type CurrentType, Type Attribute)
        {
            return CurrentType.GetCustomAttributes(Attribute, true).Length > 0;
        }

        /// <summary>
        ///     Get all types that implements and attribute
        /// </summary>
        /// <param name="Attribute">The attribute type that you are looking for</param>
        /// <param name="Types">An array of types that might implement the attribute that you are looking for</param>
        /// <returns>An IEnumerable<Type> containing the types that implements the attribute</returns>
        public static IEnumerable<Type> GetTypesWithAttribute(Type Attribute, params Type[] Types)
        {
            foreach (var type in Types)
                if (type.GetCustomAttributes(Attribute, true).Length > 0)
                    yield return type;
        }

        /// <summary>
        ///     Get the attribute instance from an instance of an object
        /// </summary>
        /// <param name="CurrentObject">The instance of an object</param>
        /// <param name="Attribute">The type of the attribute you are looking for</param>
        /// <returns>If found return the instance of the attribute otherwise it will return null</returns>
        public static Attribute GetSingleAttribute(object CurrentObject, Type Attribute)
        {
            if (CurrentObject == null)
                return null;

            return CurrentObject.GetType().GetCustomAttributes().Where(p => p.GetType() == Attribute).FirstOrDefault();
        }

        /// <summary>
        ///     Get the attribute instance from an instance of a PropertyInfo
        /// </summary>
        /// <param name="Property">A property info instance</param>
        /// <param name="Attribute">The type of the attribute you are looking for</param>
        /// <returns>If found return the instance of the attribute otherwise it will return null</returns>
        public static Attribute GetSingleAttribute(PropertyInfo Property, Type Attribute)
        {
            if (Property == null)
                return null;

            return Property.GetCustomAttributes().Where(p => p.GetType() == Attribute).FirstOrDefault();
        }

        /// <summary>
        ///     Get the attribute instance from a type
        /// </summary>
        /// <param name="ObjectType">The type of an object</param>
        /// <param name="Attribute">The type of the attribute you are looking for</param>
        /// <returns>If found return the instance of the attribute otherwise it will return null</returns>
        public static Attribute GetSingleAttribute(Type ObjectType, Type Attribute)
        {
            return ObjectType.GetCustomAttributes().Where(p => p.GetType() == Attribute).FirstOrDefault();
        }

        public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute(Type CurrentType, Type Attribute)
        {
            return CurrentType.GetProperties().Where(p => p.GetCustomAttributes(Attribute, true).Length > 0);
        }

        public static IList<PropertyInfo> GetPropertiesWithoutAttribute(Type CurrentType, Type Attribute,
            bool Inherited = true)
        {
            return CurrentType.GetProperties().Where(p => p.GetCustomAttributes(Attribute, Inherited).Length == 0)
                .DefaultIfEmpty().ToList();
        }
    }
}