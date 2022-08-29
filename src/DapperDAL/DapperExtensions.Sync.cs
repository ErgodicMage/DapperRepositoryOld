namespace DapperDAL;

public static partial class DapperExtensions
{
    #region Get Functions
    /// <summary>
    /// Gets the entity from the passed primary key or id.
    /// This is an extension function for the IDBConnection class
    /// </summary>
    /// <typeparam name="T">The Entity type</typeparam>
    /// <param name="connection">The IDbConnetion to use</param>
    /// <param name="id">The id to return</param>
    /// <param name="transaction">The transaction if in one.</param>
    /// <param name="commandTimeout">The timeout value, default none.</param>
    /// <returns>The entity for the id if found otherwise null</returns>
    public static T GetId<T>(this IDbConnection connection, object id, 
        IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = SelectBuilder<T>.BuildSqlSelectIdString();
        var dynParameters = WhereBuilder<T>.GetIdParameters(id);
        return connection.QueryFirstOrDefault<T>(sql, dynParameters, transaction, commandTimeout);
    }

    /// <summary>
    /// Gets the entity from the passed primary key or id for T that has large property strings.
    /// This is an extension function for the IDBConnection class
    /// </summary>
    /// <typeparam name="T">The Entity type</typeparam>
    /// <param name="connection">The IDbConnetion to use</param>
    /// <param name="id">The id to return</param>
    /// <param name="transaction">The transaction if in one.</param>
    /// <param name="commandTimeout">The timeout value, default none.</param>
    /// <returns>The entity for the id if found otherwise null</returns>
    public static T GetIdLargeProperties<T>(this IDbConnection connection, object id,
        IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = SelectBuilder<T>.BuildSqlSelectIdString();
        var dynParameters = WhereBuilder<T>.GetIdParameters(id);
        dynParameters = DynamicParametersHelper<T>.DynamicParametersFromGet(dynParameters);
        return connection.QueryFirstOrDefault<T>(sql, dynParameters, transaction, commandTimeout);
    }

    /// <summary>
    /// Gets the single entity of a query
    /// </summary>
    /// <typeparam name="T">The Entity type</typeparam>
    /// <param name="connection">The IDbConnetion to use</param>
    /// <param name="whereConditions">The where conditions to query</param>
    /// <param name="transaction">The transaction if in one.</param>
    /// <param name="commandTimeout">The timeout value, default none.</param>
    /// <returns>The entity for the where conditions if found otherwise null</returns>
    public static T Get<T>(this IDbConnection connection, object whereConditions,
        IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = SelectBuilder<T>.BuildSelectStatement(whereConditions);
        return connection.QueryFirstOrDefault<T>(sql, whereConditions, transaction, commandTimeout);
    }

    /// <summary>
    /// Gets the single entity of a query for T that has large property strings.
    /// </summary>
    /// <typeparam name="T">The Entity type</typeparam>
    /// <param name="connection">The IDbConnetion to use</param>
    /// <param name="whereConditions">The where conditions to query</param>
    /// <param name="transaction">The transaction if in one.</param>
    /// <param name="commandTimeout">The timeout value, default none.</param>
    /// <returns>The entity for the where conditions if found otherwise null</returns>
    public static T GetLargeProperties<T>(this IDbConnection connection, object whereConditions,
        IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = SelectBuilder<T>.BuildSelectStatement(whereConditions);
        var parameters = DynamicParametersHelper<T>.DynamicParametersFromWhere(whereConditions);
        parameters = DynamicParametersHelper<T>.DynamicParametersFromGet(parameters);
        return connection.QueryFirstOrDefault<T>(sql, parameters, transaction, commandTimeout);
    }

    /// <summary>
    /// Gets the results of a query from the entity.
    /// This is an extension function for the IDBConnection class
    /// </summary>
    /// <typeparam name="T">The Entity type</typeparam>
    /// <param name="connection">The IDbConnetion to use</param>
    /// <param name="whereConditions">The where conditions as an object, usually an anonymous type.</param>
    /// <param name="orderBy">The order by values in the form Column=true/false. True if ascending, false if descending.</param>
    /// <param name="transaction">The transaction if in one.</param>
    /// <param name="commandTimeout">The timeout value, default none.</param>
    /// <returns>An Enumerable of the entities found.</returns>
    public static IEnumerable<T> GetList<T>(this IDbConnection connection, object? whereConditions = null, object? orderBy = null,
        IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = SelectBuilder<T>.BuildSelectStatement(whereConditions, orderBy);
        return connection.Query<T>(sql, whereConditions, transaction, true, commandTimeout);
    }

    /// <summary>
    /// Gets the results of a query from the entity for T that has large property strings..
    /// This is an extension function for the IDBConnection class
    /// </summary>
    /// <typeparam name="T">The Entity type</typeparam>
    /// <param name="connection">The IDbConnetion to use</param>
    /// <param name="whereConditions">The direct where clause</param>
    /// <param name="parameters">The parameters to populate in the where clause</param>
    /// <param name="orderBy">The order by values in the form Column=true/false. True if ascending, false if descending.</param>
    /// <param name="transaction">The transaction if in one.</param>
    /// <param name="commandTimeout">The timeout value, default none.</param>
    /// <returns></returns>
    public static IEnumerable<T> GetList<T>(this IDbConnection connection, string whereConditions, object? parameters = null,
        object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = SelectBuilder<T>.BuildSelectStatement(whereConditions, orderBy);
        return connection.Query<T>(sql, parameters, transaction, true, commandTimeout);
    }

    /// <summary>
    /// Gets the results of a query from the entit for T that has large property strings.
    /// This is an extension function for the IDBConnection class
    /// </summary>
    /// <typeparam name="T">The Entity type</typeparam>
    /// <param name="connection">The IDbConnetion to use</param>
    /// <param name="whereConditions">The where conditions as an object, usually an anonymous type.</param>
    /// <param name="orderBy">The order by values in the form Column=true/false. True if ascending, false if descending.</param>
    /// <param name="transaction">The transaction if in one.</param>
    /// <param name="commandTimeout">The timeout value, default none.</param>
    /// <returns>An Enumerable of the entities found.</returns>
    public static IEnumerable<T> GetListLargeProperties<T>(this IDbConnection connection, object? whereConditions = null, 
        object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = SelectBuilder<T>.BuildSelectStatement(whereConditions, orderBy);
        var parameters = whereConditions is null ? new DynamicParameters() : DynamicParametersHelper<T>.DynamicParametersFromWhere(whereConditions);
        parameters = DynamicParametersHelper<T>.DynamicParametersFromGet(parameters);
        return connection.Query<T>(sql, parameters, transaction, true, commandTimeout);
    }

    /// <summary>
    /// Gets the results of a query from the entity for T that has large property strings.
    /// This is an extension function for the IDBConnection class
    /// </summary>
    /// <typeparam name="T">The Entity type</typeparam>
    /// <param name="connection">The IDbConnetion to use</param>
    /// <param name="whereConditions">The direct where clause</param>
    /// <param name="parameters">The parameters to populate in the where clause</param>
    /// <param name="orderBy">The order by values in the form Column=true/false. True if ascending, false if descending.</param>
    /// <param name="transaction">The transaction if in one.</param>
    /// <param name="commandTimeout">The timeout value, default none.</param>
    /// <returns></returns>
    public static IEnumerable<T> GetListLargeProperties<T>(this IDbConnection connection, string whereConditions, object? parameters = null, 
        object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = SelectBuilder<T>.BuildSelectStatement(whereConditions, orderBy);
        var dynParameters = DynamicParametersHelper<T>.DynamicParametersFromWhere(whereConditions);
        dynParameters = DynamicParametersHelper<T>.DynamicParametersFromGet(dynParameters);
        return connection.Query<T>(sql, dynParameters, transaction, true, commandTimeout);
    }


    /// <summary>
    /// Returns the number of rows in the table that holds T
    /// </summary>
    /// <typeparam name="T">The Entity type</typeparam>
    /// <param name="connection">The IDbConnetion to use</param>
    /// <param name="transaction">The transaction if in one.</param>
    /// <param name="commandTimeout">The timeout value, default none.</param>
    /// <returns></returns>
    public static int Count<T>(this IDbConnection connection, IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = SelectBuilder<T>.BuildCountStatement();
        return connection.QueryFirst<int>(sql, null, transaction, commandTimeout);
    }

    /// <summary>
    /// Returns the number of rows in the table that holds T based upon teh where condition
    /// </summary>
    /// <typeparam name="T">The Entity type</typeparam>
    /// <param name="connection">The IDbConnetion to use</param>
    /// <param name="whereConditions">The where conditions to query</param>
    /// <param name="transaction">The transaction if in one.</param>
    /// <param name="commandTimeout">The timeout value, default none.</param>
    /// <returns></returns>
    public static int Count<T>(this IDbConnection connection, object whereConditions,
        IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = SelectBuilder<T>.BuildCountStatement(whereConditions);
        var parameters = DynamicParametersHelper<T>.DynamicParametersFromWhere(whereConditions);
        return connection.QueryFirst<int>(sql, parameters, transaction, commandTimeout);
    }

    /// <summary>
    /// Returns the number of rows in the table that holds T based upon a where clause
    /// </summary>
    /// <typeparam name="T">The Entity type</typeparam>
    /// <param name="connection">The IDbConnetion to use</param>
    /// <param name="whereConditions">The direct where clause</param>
    /// <param name="parameters">The parameters to populate in the where clause</param>
    /// <param name="transaction">The transaction if in one.</param>
    /// <param name="commandTimeout">The timeout value, default none.</param>
    /// <returns></returns>
    public static int Count<T>(IDbConnection connection, string whereConditions, object? parameters = null,
        IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = SelectBuilder<T>.BuildCountStatement(whereConditions);
        var dynParameters = DynamicParametersHelper<T>.DynamicParametersFromWhere(whereConditions);
        return connection.QueryFirst<int>(sql, dynParameters, transaction, commandTimeout);
    }
    #endregion

    #region Insert Function
    public static int Insert<T>(this IDbConnection connection, T entity, IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = BuilderCache<T>.InsertStatement; //InsertBuilder<T>.BuildInsertStatement();
        return connection.ExecuteScalar<int>(sql, entity, transaction, commandTimeout);
    }

    public static string InsertReturnString<T>(this IDbConnection connection, T entity, IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = BuilderCache<T>.InsertStatement; // InsertBuilder<T>.BuildInsertStatement();
        return connection.ExecuteScalar<string>(sql, entity, transaction, commandTimeout);
    }
    #endregion

    #region Update functions
    public static int Update<T>(this IDbConnection connection, object parameters, IDbTransaction? transaction = null, 
        int? commandTimeout = null) where T : class
    {
        string sql = UpdateBuilder<T>.BuildUpdateIdStatement(parameters);
        var dynParameters = UpdateBuilder<T>.GetUpdateParameters(parameters);
        return connection.Execute(sql, dynParameters, transaction, commandTimeout);
    }

    public static int Update<T>(this IDbConnection connection, object where, object set, IDbTransaction? transaction = null,
        int? commandTimeout = null) where T : class
    {
        string sql = UpdateBuilder<T>.BuildUpdateStatement(where, set);
        var whereParameters = DynamicParametersHelper<T>.DynamicParametersFromWhere(where);
        var dynParameters = DynamicParametersHelper<T>.DynamicParametersUpdate(set, whereParameters);
        return connection.Execute(sql, dynParameters, transaction, commandTimeout);
    }

    public static int Update<T>(this IDbConnection connection, T entity, IDbTransaction? transaction = null,
    int? commandTimeout = null) where T : class
    {
        string sql = UpdateBuilder<T>.BuildUpdateEntityStatement();
        return connection.Execute(sql, entity, transaction, commandTimeout);
    }
    #endregion

    #region Delete functions
    public static int DeleteId<T>(this IDbConnection connection, int id, 
        IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = DeleteBuilder<T>.BuildDeleteIdStatement();
        var dynParameters = WhereBuilder<T>.GetIdParameters(id);
        return connection.Execute(sql, dynParameters, transaction, commandTimeout);
    }

    public static int DeleteWhere<T>(this IDbConnection connection, object whereConditions,
        IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = DeleteBuilder<T>.BuildDeleteStatement(whereConditions);
        return connection.Execute(sql, whereConditions, transaction, commandTimeout);
    }
    #endregion
}
