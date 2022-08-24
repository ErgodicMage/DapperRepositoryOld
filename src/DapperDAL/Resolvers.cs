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
            if (!string.IsNullOrWhiteSpace(tableattr.Schema))
                return $"{tableattr.Schema}.{FinalTableName(tableattr.Name, tableattr.Alias)}";
            else
                return FinalTableName(tableattr.Name, tableattr.Alias);
        }

        return Encapsulate(type.Name);
    }

    public static string ResolveColumnName(PropertyInfo propertyInfo, string? alias = null)
    {
        var columnattr = propertyInfo.GetCustomAttributes(true).SingleOrDefault(attr => 
            attr.GetType().Name == typeof(ColumnAttribute).Name) as dynamic;
        if (columnattr is not null)
            return FinalColumnName(columnattr.Name, alias);
        return FinalColumnName(propertyInfo.Name, alias);
    }

    public static string ResolveCustomColumnName(PropertyInfo propertyInfo, string? alias = null)
    {
        if (propertyInfo.GetCustomAttributes(true).SingleOrDefault(attr => 
            attr.GetType().Name == typeof(ColumnAttribute).Name) == null)
            return string.Empty;

        return FinalColumnName(propertyInfo.Name, alias); 
    }

    internal static string FinalTableName(string name, string? alias)
    {
        if (name == null) return string.Empty;
        if (!string.IsNullOrWhiteSpace(alias)) return $"{Encapsulate(name)} as {alias}";
        return Encapsulate(name);
    }

    internal static string FinalColumnName(string name, string? alias)
    {
        if (name == null) return string.Empty;
        if (!string.IsNullOrWhiteSpace(alias)) return $"{alias}.{Encapsulate(name)}";
        return Encapsulate(name);
    }
}
