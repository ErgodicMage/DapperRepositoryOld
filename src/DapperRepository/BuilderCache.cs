namespace DapperRepository;

/// <summary>
/// Caches often used property information needed to build sql statements. 
/// The specific property (such as SelectColumn) is built the first time it is reference and all other references.
/// </summary>
/// <typeparam name="T">The Entity type</typeparam>
public static class BuilderCache<T> where T : class
{
    #region Cached Properties
    public static string SelectColumns => GetorSet("SelectColumns", SelectBuilder<T>.BuildSelectColumns);

    public static string TableName => GetorSet("TableName", Resolvers.ResolveTableName<T>);

    public static string WhereIdString => GetorSet("WhereId", WhereBuilder<T>.BuildIdWhereString);

    public static string InsertStatement => GetorSet("Insert", InsertBuilder<T>.BuildInsertStatement);

#pragma warning disable S2743 // Static fields should not be used in generic types
    public static PropertyInfo[] Properties => GetorSet("Properties", GetProperties);

    public static PropertyInfo[] IdProperties => GetorSet("IdProperties", GetIdProperties);
#pragma warning restore S2743 // Static fields should not be used in generic types

    public static PropertyInfo[] ScaffoldProperties => GetorSet("ScaffoldProperties", PropertiesHelper.GetScaffoldableProperties<T>);

    public static PropertyInfo[] LargeProperties => GetorSet("LargeProperties", PropertiesHelper.GetLargeProperties<T>);
    #endregion

    #region Cache Operations
    private static string KeyName(string key) => $"{typeof(T).Name}.{key}";

    private static TValue GetorSet<TValue>(string key, Func<TValue> create) =>
        DapperRepositoryCache.GetorSet<TValue>(KeyName(key), create);

    private static PropertyInfo[] GetProperties()
        => typeof(T).GetProperties();

    private static PropertyInfo[] GetIdProperties()
        => PropertiesHelper.GetIdProperties(typeof(T));

    #endregion
}
