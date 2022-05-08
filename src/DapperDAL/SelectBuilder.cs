namespace DapperDAL;

public static class SelectBuilder<T> where T : class
{
    public static string BuildSqlSelectIdString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append(BuilderCache<T>.SelectColumns);
        sb.Append(" FROM ");
        sb.Append(BuilderCache<T>.TableName);

        sb = sb.Append(" WHERE ");
        sb.Append(BuilderCache<T>.WhereIdString);

        return sb.ToString();
    }

    public static string BuildSelectStatement(object whereConditions = null, object order = null, int? maxRows = null, bool? distinct = null)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");

        if (maxRows.HasValue && maxRows.Value > 0)
            sb.Append($"TOP {maxRows.Value} ");

        if (distinct.HasValue && distinct.Value)
            sb.Append("DISTINCT ");

        sb.Append(BuilderCache<T>.SelectColumns);

        sb.Append(" FROM ");
        sb.Append(BuilderCache<T>.TableName);

        if (whereConditions != null)
        {
            sb = sb.Append(" WHERE ");
            WhereBuilder<T>.BuildWhereString(sb, whereConditions);
        }

        if (order != null)
        {
            sb.Append(" ORDER BY ");
            BuildOrderBy(sb, order);
        }

        return sb.ToString();
    }

    public static string BuildSelectStatement(string whereConditions, object order = null, int? maxRows = null, bool? distinct = null)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");

        if (maxRows.HasValue)
            sb.Append($"TOP {maxRows.Value} ");

        if (distinct.HasValue && distinct.Value)
            sb.Append("DISTINCT ");

        sb.Append(BuilderCache<T>.SelectColumns);

        sb.Append(" FROM ");
        sb.Append(BuilderCache<T>.TableName);

        if (!string.IsNullOrEmpty(whereConditions))
            sb = sb.Append($" WHERE {whereConditions}");

        if (order != null)
        {
            sb.Append(" ORDER BY ");
            BuildOrderBy(sb, order);
        }

        return sb.ToString();
    }

    public static void BuildOrderBy(StringBuilder sb, object order)
    {
        var orderProperties = order.GetType().GetProperties();

        bool first = true;
        foreach (var property in orderProperties)
        {          
            if (!first)
                sb.Append(",");
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
        StringBuilder sb = new StringBuilder();

        var first = true;
        foreach (var property in BuilderCache<T>.ScaffoldProperties)
        {
            if (property.GetCustomAttributes(true).Any(attr => attr.GetType().Name == typeof(IgnoreSelectAttribute).Name
                    || attr.GetType().Name == typeof(NotMappedAttribute).Name))
                continue;

            if (!first)
                sb.Append(',');
            sb.Append(Resolvers.ResolveColumnName(property));

            string customColumnName = Resolvers.ResolveCustomColumnName(property);
            if (!string.IsNullOrEmpty(customColumnName))
                sb.Append($" as {customColumnName}");

            first = false;
        }

        return sb.ToString();
    }
}
