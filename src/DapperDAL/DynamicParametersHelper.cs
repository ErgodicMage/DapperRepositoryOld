namespace DapperDAL;

internal static class DynamicParametersHelper<T> where T : class
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

    public static DynamicParameters DynamicParametersUpdate(object setParameters, DynamicParameters? whereParameters = null)
    {
        DynamicParameters returnParameters = whereParameters ?? new DynamicParameters();

        foreach(var property in setParameters.GetType().GetProperties())
        {
            if (whereParameters is not null && whereParameters.ParameterNames.Contains(property.Name))
                continue;

            // if we have where parameters ignore Id types since they will already be included
            if (whereParameters is not null && property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                continue;

            var attributes = property.GetCustomAttributes(true);

            // if we have where parameters ignore [Key] types since they will already be included
            if (whereParameters is not null && attributes.Any(attr => attr.GetType().Name == typeof(KeyAttribute).Name))
                continue;

            // don't include [IgnoreUpdate] or [NotMapped]
            if (attributes.Any(attr =>
                                attr.GetType().Name == typeof(IgnoreUpdateAttribute).Name ||
                                attr.GetType().Name == typeof(NotMappedAttribute).Name))
                continue;

            var useProperty = BuilderCache<T>.Properties.FirstOrDefault(p => p.Name == property.Name) ?? property;

            object? v = property.GetValue(setParameters, null);

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
    public static DynamicParameters DynamicParametersFromGet(DynamicParameters? whereParameters = null)
    {
        DynamicParameters returnParameters = whereParameters ?? new DynamicParameters();

        foreach (var property in BuilderCache<T>.Properties)
        {
            if (whereParameters is not null && whereParameters.ParameterNames.Contains(property.Name))
                continue;

            // if we have where parameters ignore Id types since they will already be included
            if (whereParameters is not null && property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                continue;

            var attributes = property.GetCustomAttributes(true);

            // if we have where parameters ignore [Key] types since they will already be included
            if (whereParameters is not null && attributes.Any(attr => (attr.GetType().Name == typeof(KeyAttribute).Name) || 
                                                                  (attr.GetType().Name == typeof(NonAutoKeyAttribute).Name)))
                continue;

            // don't include [IgnoreSelect] or [NotMapped]
            if (attributes.Any(attr => 
                                attr.GetType().Name == typeof(IgnoreSelectAttribute).Name ||
                                attr.GetType().Name == typeof(NotMappedAttribute).Name))
                continue;

            int stringLength = 0;

            dynamic columnAttribute = attributes.FirstOrDefault(attr => attr.GetType().Name == typeof(ColumnAttribute).Name);
            if (columnAttribute is not null)
                stringLength = columnAttribute.Length;

            string name = $"@{property.Name}";

            if (stringLength == 0)
                returnParameters.Add(name, null);
            else
                returnParameters.Add(name,null, null, ParameterDirection.Output, stringLength);
        }

        return returnParameters;
    }
}
