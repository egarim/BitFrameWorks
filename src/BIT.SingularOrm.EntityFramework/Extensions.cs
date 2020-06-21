using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Brevitas.AppFramework.DataAccess.EF
{
    public static class Extensions
    {
        public static List<PropertyInfo> GetDbSetProperties(this DbContext context)
        {
            var dbSetProperties = new List<PropertyInfo>();
            var properties = context.GetType().GetProperties();

            foreach (var property in properties)
            {
                var setType = property.PropertyType;

#if EF5 || EF6
            var isDbSet =
 setType.IsGenericType && (typeof (IDbSet<>).IsAssignableFrom(setType.GetGenericTypeDefinition()) || setType.GetInterface(typeof (IDbSet<>).FullName) != null);
#elif EF7
            var isDbSet =
 setType.IsGenericType && (typeof (DbSet<>).IsAssignableFrom(setType.GetGenericTypeDefinition()));
#endif
                var isDbSet = setType.IsGenericType &&
                              typeof(DbSet<>).IsAssignableFrom(setType.GetGenericTypeDefinition());
                if (isDbSet) dbSetProperties.Add(property);
            }

            return dbSetProperties;
        }
    }
}