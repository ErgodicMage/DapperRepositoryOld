using System.Reflection;

namespace DapperDAL;

/// <summary>
/// Caches often used property information needed to build sql statements. 
/// The specific property (such as SelectColumn) is built the first time it is reference and all other references.
/// Note: should use ObjectCache (or similar) internally
/// </summary>
/// <typeparam name="T">The Entity type</typeparam>
public static class BuilderCacheOld<T> where T : class
{
    #region Cached Properties
    private static string _selectColumns;
    public static string SelectColumns { get { BuildSelectColumns(); return _selectColumns; } }

    private static string _tableName;
    public static string TableName { get { BuildTableName(); return _tableName; } }

    private static string _whereIdString;
    public static string WhereIdString { get { BuildIdWhereString(); return _whereIdString; }  }

    private static string _insertStatement;
    public static string InsertStatement { get { BuildInsertStatement(); return _insertStatement; } }

    private static PropertyInfo[] _properties;
    public static PropertyInfo[] Properties { get { BuildProperties(); return _properties; } }

    private static PropertyInfo[] _idProperties;
    public static PropertyInfo[] IdProperties { get { BuildIdProperties(); return _idProperties; } }

    private static PropertyInfo[] _scaffoldProperties;
    public static PropertyInfo[] ScaffoldProperties { get { BuildScaffoldProperties(); return _scaffoldProperties; } }
    #endregion

    /// <summary>
    /// Builds the columns which goes into a select string.
    /// The form will be SELECT [DbColumnName] if there is no Column attributes.
    /// If there is a Column attribute then it will be SELECT [DBColumnName] as [EntityProperty].
    /// </summary>
    public static void BuildSelectColumns() => _selectColumns ??= SelectBuilder<T>.BuildSelectColumns();

    /// <summary>
    /// Builds the Where string with parameters based upon either the entity's Id property or the Key attribute.
    /// The general form is [Id]=@[Id] or if Key attribute [DbColumnName]=@[EntityProperty]
    /// </summary>
    public static void BuildIdWhereString() => _whereIdString ??= WhereBuilder<T>.BuildIdWhereString();

    /// <summary>
    /// The name of the database table as [TableName]. If the entity that has the Table attribute that value will be used.
    /// </summary>
    public static void BuildTableName() => _tableName ??= Resolvers.ResolveTableName(typeof(T));

    /// <summary>
    /// Builds the INSERT statment which goes into a insert string.
    /// The form will be INSERT INTO [TABLENAME] ([DbColumnName], ...) OUTPUT (...) VALUES(@...).
    /// </summary>
    public static void BuildInsertStatement() => _insertStatement ??= InsertBuilder<T>.BuildInsertStatement();


    /// <summary>
    /// Builds the Entity's properties that are either Id or have the Key attribute
    /// </summary>
    public static void BuildIdProperties() => _idProperties ??= PropertiesHelper.GetIdProperties(typeof(T)).ToArray();

    /// <summary>
    /// Builds all of the properties of the Entity
    /// </summary>
    public static void BuildProperties() => _properties ??= typeof(T).GetProperties();

    /// <summary>
    /// Builds all of the Entity's properties that do not have Editable attribute set to false [Editable(false)]
    /// </summary>
    public static void BuildScaffoldProperties() => _scaffoldProperties ??= PropertiesHelper.GetScaffoldableProperties<T>().ToArray();

}
