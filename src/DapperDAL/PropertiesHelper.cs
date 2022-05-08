using System.Reflection;

namespace DapperDAL;

public static class PropertiesHelper
{
    public static IEnumerable<PropertyInfo> GetAllProperties<T>(T entity) where T : class
    {
        if (entity == null) return Enumerable.Empty<PropertyInfo>();
        return entity.GetType().GetProperties();
    }

    public static IEnumerable<PropertyInfo> GetScaffoldableProperties<T>() where T : class
    {
        IEnumerable<PropertyInfo> props = typeof(T).GetProperties();

        props = props.Where(p => !p.GetCustomAttributes(true).Any(attr => 
            attr.GetType().Name == typeof(EditableAttribute).Name && !IsEditable(p)));

        return props.Where(p => p.PropertyType.IsSimpleType() || IsEditable(p));
    }

    public static IEnumerable<PropertyInfo> GetIdProperties(object entity) => GetIdProperties(entity.GetType());

    public static IEnumerable<PropertyInfo> GetIdProperties(Type type)
    {
        var tp = type.GetProperties().Where(p => p.GetCustomAttributes(true).Any(attr =>
            attr.GetType().Name == typeof(KeyAttribute).Name));
        if (!tp.Any())
            tp = type.GetProperties().Where(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
        return tp;
    }

    public static IEnumerable<PropertyInfo> GetUpdateableProperties<T>() where T : class
    {
        var updateableProperties = BuilderCache<T>.ScaffoldProperties.AsEnumerable();
        //remove ones with ID
        updateableProperties = updateableProperties.Where(p => !p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
        //remove ones with key attribute
        updateableProperties = updateableProperties.Where(p => !p.GetCustomAttributes(true).Any(attr => 
            attr.GetType().Name == typeof(KeyAttribute).Name));
        //remove ones that are readonly
        updateableProperties = updateableProperties.Where(p => !p.GetCustomAttributes(true).Any(attr => 
            (attr.GetType().Name == typeof(ReadOnlyAttribute).Name) && IsReadOnly(p)));
        //remove ones with IgnoreUpdate attribute
        updateableProperties = updateableProperties.Where(p => !p.GetCustomAttributes(true).Any(attr => 
            attr.GetType().Name == typeof(IgnoreUpdateAttribute).Name));
        //remove ones that are not mapped
        updateableProperties = updateableProperties.Where(p => !p.GetCustomAttributes(true).Any(attr => 
            attr.GetType().Name == typeof(NotMappedAttribute).Name));

        return updateableProperties;
    }

    public static bool IsEditable(PropertyInfo pi)
    {
        var attributes = pi.GetCustomAttributes(false);
        if (attributes.Length > 0)
        {
            dynamic write = attributes.FirstOrDefault(x => x.GetType().Name == typeof(EditableAttribute).Name);
            if (write != null)
            {
                return write.AllowEdit;
            }
        }
        return false;
    }

    public static bool IsReadOnly(PropertyInfo pi)
    {
        var attributes = pi.GetCustomAttributes(false);
        if (attributes.Length > 0)
        {
            dynamic write = attributes.FirstOrDefault(x => x.GetType().Name == typeof(ReadOnlyAttribute).Name);
            if (write != null)
            {
                return write.IsReadOnly;
            }
        }
        return false;
    }

}
