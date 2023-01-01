namespace DapperRepository;

public static class SelectBuilder<T> where T : class
{
    public static string BuildSqlSelectIdString()
    {
        var sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append(BuilderCache<T>.SelectColumns);
        sb.Append(" FROM ");
        sb.Append(BuilderCache<T>.TableName);

        sb.Append(" WHERE ");
        sb.Append(BuilderCache<T>.WhereIdString);

        return sb.ToString();
    }

    public static string BuildSelectStatement(object? whereConditions = null, object? order = null, int? maxRows = null, bool? distinct = null)
    {
        var sb = new StringBuilder();
        sb.Append("SELECT ");

        if (maxRows.HasValue && maxRows.Value > 0)
            sb.Append($"TOP {maxRows.Value} ");

        if (distinct.HasValue && distinct.Value)
            sb.Append("DISTINCT ");

        sb.Append(BuilderCache<T>.SelectColumns);

        sb.Append(" FROM ");
        sb.Append(BuilderCache<T>.TableName);

        if (whereConditions is not null)
        {
            sb.Append(" WHERE ");
            WhereBuilder<T>.BuildWhereString(sb, whereConditions);
        }

        if (order is not null)
        {
            sb.Append(" ORDER BY ");
            BuildOrderBy(sb, order);
        }

        return sb.ToString();
    }

    public static string BuildSelectStatement(string whereConditions, object? order = null, int? maxRows = null, bool? distinct = null)
    {
        var sb = new StringBuilder();
        sb.Append("SELECT ");

        if (maxRows.HasValue)
            sb.Append($"TOP {maxRows.Value} ");

        if (distinct.HasValue && distinct.Value)
            sb.Append("DISTINCT ");

        sb.Append(BuilderCache<T>.SelectColumns);

        sb.Append(" FROM ");
        sb.Append(BuilderCache<T>.TableName);

        if (!string.IsNullOrWhiteSpace(whereConditions))
            sb.Append($" WHERE {whereConditions}");

        if (order is not null)
        {
            sb.Append(" ORDER BY ");
            BuildOrderBy(sb, order);
        }

        return sb.ToString();
    }

    public static string BuildCountStatement(object? whereConditions = null)
    {
        var sb = new StringBuilder();
        sb.Append("SELECT COUNT(1) FROM ");
        sb.Append(BuilderCache<T>.TableName);

        if (whereConditions is not null)
        {
            sb.Append(" WHERE ");
            WhereBuilder<T>.BuildWhereString(sb, whereConditions);
        }

        return sb.ToString();
    }

    public static string BuildCountStatement(string whereConditions)
    {
        var sb = new StringBuilder();
        sb.Append("SELECT COUNT(1) FROM ");
        sb.Append(BuilderCache<T>.TableName);

        if (!string.IsNullOrWhiteSpace(whereConditions))
            sb.Append($" WHERE {whereConditions}");

        return sb.ToString();
    }

    public static void BuildOrderBy(StringBuilder sb, object order)
    {
        var orderProperties = order.GetType().GetProperties();

        bool first = true;
        foreach (var property in orderProperties)
        {          
            if (!first)
                sb.Append(',');
            first = false;

            sb.Append(Resolvers.ResolveColumnName(property));

            bool? value = property.GetValue(order) as bool?;
            if (value.HasValue && !value.Value)
                sb.Append(" DESC");
        }
    }

    /// <summary>
    /// Builds the SelectColumns used in a select statement for T.
    /// Note: rarely should this be used directly, instead use BuilderCache<T>.SelectColumns for the cached value;
    /// </summary>
    /// <returns>string containing the columns to be used in a select statement</returns>
    public static string BuildSelectColumns()
    {
        var sb = new StringBuilder();

        var tableattr = typeof(T).GetCustomAttributes(true).SingleOrDefault(attr =>
            attr.GetType().Name == typeof(TableAttribute).Name) as dynamic;
        string? alias = tableattr?.Alias;

        var first = true;
        foreach (var property in BuilderCache<T>.ScaffoldProperties)
        {
            if (property.GetCustomAttributes(true).Any(attr => attr.GetType().Name == typeof(IgnoreSelectAttribute).Name
                    || attr.GetType().Name == typeof(NotMappedAttribute).Name))
                continue;

            if (!first)
                sb.Append(',');
            string propertyName = Resolvers.ResolveColumnName(property, alias);
            sb.Append(propertyName);

            string customColumnName = Resolvers.ResolveCustomColumnName(property);
            if (!string.IsNullOrWhiteSpace(customColumnName) && customColumnName != propertyName)
                sb.Append($" as {customColumnName}");

            first = false;
        }

        return sb.ToString();
    }
}
