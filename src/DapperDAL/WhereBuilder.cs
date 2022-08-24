namespace DapperDAL;

/// <summary>
/// Builds where statements for generating sql.
/// </summary>
/// <typeparam name="T">The Entity type</typeparam>
public static class WhereBuilder<T> where T : class
{
    public static void BuildWhereString(StringBuilder sb, object whereConditions)
    {
        var whereProperties = PropertiesHelper.GetAllProperties(whereConditions);
        var tableattr = typeof(T).GetCustomAttributes(true).SingleOrDefault(attr =>
            attr.GetType().Name == typeof(TableAttribute).Name) as dynamic;
        string? alias = tableattr?.Alias;

        bool first = true;

        foreach (var property in whereProperties)
        {
            var useProperty = property;
            bool useIsNull = false;

            foreach(var sourceProperty in BuilderCache<T>.ScaffoldProperties)
            {
                if (sourceProperty.Name == useProperty.Name)
                {
                    if (whereConditions != null && useProperty.CanRead && 
                        (useProperty.GetValue(whereConditions, null) is null || 
                        useProperty.GetValue(whereConditions, null) == DBNull.Value))
                    {
                        useIsNull = true;
                    }

                    useProperty = sourceProperty;
                    break;
                }
            }

            if (!first)
                sb.Append(" AND ");
            first = false;

            if (useIsNull)
                sb.Append($"{Resolvers.ResolveColumnName(useProperty)} IS NULL");
            else
            {
                string op = "=";
                Type propertyType = property.PropertyType;
                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                    op = " IN ";

                sb.Append($"{Resolvers.ResolveColumnName(useProperty, alias)}{op}@{useProperty.Name}");
            }
        }
    }

    public static DynamicParameters GetIdParameters(object id)
    {
        var dynamicParameters = new DynamicParameters();
        if (BuilderCache<T>.IdProperties.Length == 1)
            dynamicParameters.Add("@" + BuilderCache<T>.IdProperties.First().Name, id);
        else
        {
            foreach (var prop in BuilderCache<T>.IdProperties)
                dynamicParameters.Add("@" + prop.Name, id?.GetType()?.GetProperty(prop.Name)?.GetValue(id, null));
        }
        return dynamicParameters;
    }

    /// <summary>
    /// Builds the Where statement for an id of type T.
    /// Note: rarely should this be used directly, instead use BuilderCache<T>.WhereIdString for the cached value;
    /// </summary>
    /// <returns>string containing the where clause</returns>
    public static string BuildIdWhereString()
    {
        StringBuilder sb = new StringBuilder();
        var tableattr = typeof(T).GetCustomAttributes(true).SingleOrDefault(attr =>
            attr.GetType().Name == typeof(TableAttribute).Name) as dynamic;
        string? alias = tableattr?.Alias;

        bool first = true;
        foreach (PropertyInfo property in BuilderCache<T>.IdProperties)
        {
            if (!first)
                sb.Append(" AND ");

            sb.Append($"{Resolvers.ResolveColumnName(property, alias)}=@{property.Name}");
            first = false;
        }

        return sb.ToString();
    }
}
