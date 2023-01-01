﻿namespace DapperRepository;

public static class UpdateBuilder<T> where T : class
{
    public static string BuildUpdateIdStatement(object updates)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE ");
        sb.Append(BuilderCache<T>.TableName);

        sb.Append(" SET ");
        BuildUpdateSetStatement(sb, updates);

        sb = sb.Append(" WHERE ");
        sb.Append(BuilderCache<T>.WhereIdString);
        return sb.ToString();
    }

    public static string BuildUpdateEntityStatement()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE ");
        sb.Append(BuilderCache<T>.TableName);

        sb.Append(" SET ");
        BuildUpdateEntitySetStatement(sb);

        sb = sb.Append(" WHERE ");
        sb.Append(BuilderCache<T>.WhereIdString);
        return sb.ToString();
    }

    public static string BuildUpdateStatement(object where, object updates)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE ");
        sb.Append(BuilderCache<T>.TableName);

        sb.Append(" SET ");
        BuildUpdateSetStatement(sb, updates);

        sb = sb.Append(" WHERE ");
        WhereBuilder<T>.BuildWhereString(sb, where);
        return sb.ToString();
    }

    public static void BuildUpdateSetStatement(StringBuilder sb, object updates)
    {
        var tableattr = typeof(T).GetCustomAttributes(true).SingleOrDefault(attr =>
            attr.GetType().Name == typeof(TableAttribute).Name) as dynamic;
        string? alias = tableattr?.Alias;

        bool first = true;
        foreach (var property in updates.GetType().GetProperties())
        {
            var useProperty = BuilderCache<T>.Properties.FirstOrDefault(p => p.Name == property.Name);
            if (useProperty == null)
                useProperty = property;

            if (!CanUpdate(useProperty))
                continue;

            if (!first)
                sb.Append(", ");
            first = false;

            sb.Append(Resolvers.ResolveColumnName(useProperty, alias));
            sb.Append("=@");
            sb.Append(property.Name);
        }
    }

    public static void BuildUpdateEntitySetStatement(StringBuilder sb)
    {
        var tableattr = typeof(T).GetCustomAttributes(true).SingleOrDefault(attr =>
            attr.GetType().Name == typeof(TableAttribute).Name) as dynamic;
        string? alias = tableattr?.Alias;

        bool first = true;
        foreach (var property in BuilderCache<T>.ScaffoldProperties)
        {
            if (!CanUpdate(property))
                continue;

            if (!first)
                sb.Append(", ");
            first= false;

            sb.Append(Resolvers.ResolveColumnName(property, alias));
            sb.Append("=@");
            sb.Append(property.Name);
        }
    }

    public static DynamicParameters GetUpdateParameters(object updates)
    {
        DynamicParameters dynamicParameters = new DynamicParameters();

        foreach (var property in updates.GetType().GetProperties())
        {
            if (BuilderCache<T>.Properties.Any(p => p.Name == property.Name))
                dynamicParameters.Add($"@{property.Name}", property.GetValue(updates, null));
        }

        return dynamicParameters;
    }

    public static bool CanUpdate(PropertyInfo property)
    {
        if (!property.PropertyType.IsSimpleType())
            return false;

        var customAttributes = property.GetCustomAttributes(true);
        foreach (var attr in customAttributes)
        {
            if (attr.GetType().Name == typeof(KeyAttribute).Name)
                return false;

            if (attr.GetType().Name == typeof(ReadOnlyAttribute).Name && PropertiesHelper.IsReadOnly(property))
                return false;

            if (attr.GetType().Name == typeof(IgnoreUpdateAttribute).Name)
                return false;

            if (attr.GetType().Name == typeof(NotMappedAttribute).Name)
                return false;

            if (attr.GetType().Name == typeof(EditableAttribute).Name && !PropertiesHelper.IsEditable(property))
                return false;
        }

        return true;
    }
}
