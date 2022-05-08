using System.Reflection;

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

    private static T GetorSet<T>(string key, Func<T> create) =>
        DefaultCache.GetorSet<T>(KeyName(key), create);

    //private static T GetorSet<T>(string key, Func<T> create)
    //{
    //    T retObj;
    //    string name = KeyName(key);

    //    if (!DefaultCache.Cache.TryGetValue(name, out retObj))
    //        retObj = DefaultCache.Cache.Set(name, create());

    //    return retObj;
    //}
    #endregion
}
