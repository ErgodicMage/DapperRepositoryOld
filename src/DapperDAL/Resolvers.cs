namespace DapperDAL;

internal static class Resolvers
{
    public static string Encapsulate(string v) => $"[{v}]";

    public static string ResolveTableName<T>() => ResolveTableName(typeof(T));
    public static string ResolveTableName(Type type)
    {
        var tableattr = type.GetCustomAttributes(true).SingleOrDefault(attr => 
            attr.GetType().Name == typeof(TableAttribute).Name) as dynamic;
        if (tableattr is not null)
        {
            if (!string.IsNullOrEmpty(tableattr.Schema))
                return $"{Encapsulate(tableattr.Schema)}.{Encapsulate(tableattr.Name)}";
            else
                return Encapsulate(tableattr.Name);
        }

        return Encapsulate(type.Name);
    }

    public static string ResolveColumnName(PropertyInfo propertyInfo)
    {
        var columnattr = propertyInfo.GetCustomAttributes(true).SingleOrDefault(attr => 
            attr.GetType().Name == typeof(ColumnAttribute).Name) as dynamic;
        if (columnattr is not null)
            return Encapsulate(columnattr.Name);
        return Encapsulate(propertyInfo.Name);
    }

    public static string ResolveCustomColumnName(PropertyInfo propertyInfo)
    {
        if (propertyInfo.GetCustomAttributes(true).SingleOrDefault(attr => 
            attr.GetType().Name == typeof(ColumnAttribute).Name) == null)
            return string.Empty;

        return Encapsulate(propertyInfo.Name); 
    }
}
