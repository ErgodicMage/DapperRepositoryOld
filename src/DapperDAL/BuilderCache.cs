namespace DapperDAL;

/// <summary>
/// Caches often used property information needed to build sql statements. 
/// The specific property (such as SelectColumn) is built the first time it is reference and all other references.
/// </summary>
/// <typeparam name="T">The Entity type</typeparam>
public class BuilderCache<T> where T : class
{
    #region Cached Properties
    public static string SelectColumns => GetorSet("SelectColumns", SelectBuilder<T>.BuildSelectColumns);

    public static string TableName => GetorSet("TableName", Resolvers.ResolveTableName<T>);

    public static string WhereIdString => GetorSet("WhereId", WhereBuilder<T>.BuildIdWhereString);

    public static string InsertStatement => GetorSet("Insert", InsertBuilder<T>.BuildInsertStatement);

    public static PropertyInfo[] Properties => GetorSet("Properties", typeof(T).GetProperties().ToArray);

    public static PropertyInfo[] IdProperties => GetorSet("IdProperties", PropertiesHelper.GetIdProperties(typeof(T)).ToArray);

    public static PropertyInfo[] ScaffoldProperties => GetorSet("ScaffoldProperties", PropertiesHelper.GetScaffoldableProperties<T>().ToArray);
    #endregion

    #region Cache Operations
    private static string KeyName(string key) => $"{typeof(T).Name}.{key}";

    private static TValue GetorSet<TValue>(string key, Func<TValue> create) =>
        DapperDALCache.GetorSet<TValue>(KeyName(key), create);

    #endregion
}
