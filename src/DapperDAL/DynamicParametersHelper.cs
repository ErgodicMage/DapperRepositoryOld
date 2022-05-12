namespace DapperDAL;

public static class DynamicParametersHelper<T> where T : class
{
    public static DynamicParameters DynamicParametersFromWhere(object parameters)
    {
        DynamicParameters returnParameters = new DynamicParameters();

        foreach (var property in parameters.GetType().GetProperties())
        {
            var useProperty = BuilderCache<T>.Properties.FirstOrDefault(p => p.Name == property.Name) ?? property;

            object v = property.GetValue(parameters, null);

            returnParameters.Add($"@{useProperty.Name}", v);
        }

        return returnParameters;
    }

    /// <summary>
    /// This function generates the DynamicProperties for Get operations. 
    /// Note: Use this function for types that have strings longer than 4000 (Dapper's default). If not then use the normal properties.
    /// </summary>
    /// <param name="passedParameters">The parameter values of the entity</param>
    /// <param name="whereParameters">Optional whereParameters if previously generated.</param>
    /// <returns>The parameters to use in a Get operation.</returns>
    public static DynamicParameters DynamicParametersFromGet(DynamicParameters whereParameters = null)
    {
        DynamicParameters returnParameters = whereParameters ?? new DynamicParameters();

        foreach (var property in BuilderCache<T>.Properties)
        {
            if (whereParameters != null && whereParameters.ParameterNames.Contains(property.Name))
                continue;

            // if we have where parameters ignore Id types since they will already be included
            if (whereParameters != null && property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                continue;

            var attributes = property.GetCustomAttributes(true);

            // if we have where parameters ignore [Key] types since they will already be included
            if (whereParameters != null && attributes.Any(attr => attr.GetType().Name == typeof(KeyAttribute).Name))
                continue;

            // don't include [IgnorSelect] or [NotMapped]
            if (attributes.Any(attr => 
                                attr.GetType().Name == typeof(IgnoreSelectAttribute).Name ||
                                attr.GetType().Name == typeof(NotMappedAttribute).Name))
                continue;

            int stringLength = 0;

            dynamic columnAttribute = attributes.FirstOrDefault(attr => attr.GetType().Name == typeof(ColumnAttribute).Name);
            if (columnAttribute != null)
                stringLength = columnAttribute.Length;

            string name = $"@{property.Name}";

            if (stringLength == 0)
                returnParameters.Add(name, null);
            else
                returnParameters.Add(name,null, null, ParameterDirection.Output, stringLength);
        }

        return returnParameters;
    }

    public static DynamicParameters DynamicParametersInsert(T entity)
    {
        DynamicParameters returnParameters = new();

        foreach (var property in typeof(T).GetProperties())
        {
            // if we have where parameters ignore Id types since they will already be included
            if (property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                continue;

            var attributes = property.GetCustomAttributes(true);

            // if we have where parameters ignore [Key] types since they will already be included
            if (attributes.Any(attr => attr.GetType().Name == typeof(KeyAttribute).Name))
                continue;

            // don't include [IgnoreInsert] or [NotMapped]
            if (attributes.Any(attr =>
                                attr.GetType().Name == typeof(IgnoreInsertAttribute).Name ||
                                attr.GetType().Name == typeof(NotMappedAttribute).Name))
                continue;

            int stringLength = 0;

            dynamic columnAttribute = attributes.FirstOrDefault(attr => attr.GetType().Name == typeof(ColumnAttribute).Name);
            if (columnAttribute != null)
                stringLength = columnAttribute.Length;

            string name = $"@{property.Name}";
            object v = property.GetValue(entity, null);

            if (stringLength == 0)
                returnParameters.Add(name, v);
            else
                returnParameters.Add(name, v, null, ParameterDirection.Input, stringLength);
        }

        return returnParameters;
    }

    public static DynamicParameters DynamicParametersUpdate(object parameters, DynamicParameters whereParameters = null)
    {
        DynamicParameters returnParameters = whereParameters ?? new DynamicParameters();

        foreach (var property in parameters.GetType().GetProperties())
        {
            if (whereParameters != null && whereParameters.ParameterNames.Contains(property.Name))
                continue;

            var useProperty = BuilderCache<T>.Properties.FirstOrDefault(p => p.Name == property.Name) ?? property;

            int stringLength = 0;

            var attributes = useProperty.GetCustomAttributes(true);
            dynamic columnAttribute = attributes.FirstOrDefault(attr => attr.GetType().Name == typeof(ColumnAttribute).Name);
            if (columnAttribute != null)
                stringLength = columnAttribute.Length;

            string name = $"@{property.Name}";
            object v = property.GetValue(parameters, null);

            if (stringLength == 0)
                returnParameters.Add(name, v);
            else
                returnParameters.Add(name, v, null, ParameterDirection.Input, stringLength);
        }

        return returnParameters;
    }
}
