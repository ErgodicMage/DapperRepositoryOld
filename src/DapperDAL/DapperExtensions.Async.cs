﻿namespace DapperDAL;

public static partial class DapperExtensions
{
    #region Get functions
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
    public static Task<T> GetIdAsync<T>(this IDbConnection connection, object id, 
        IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = SelectBuilder<T>.BuildSqlSelectIdString();
        var dynParameters = WhereBuilder<T>.GetIdParameters(id);
        return connection.QueryFirstOrDefaultAsync<T>(sql, dynParameters, transaction, commandTimeout);
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
    public static Task<T> GetIdLargePropertiesAsync<T>(this IDbConnection connection, object id,
        IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = SelectBuilder<T>.BuildSqlSelectIdString();
        var dynParameters = WhereBuilder<T>.GetIdParameters(id);
        dynParameters = DynamicParametersHelper<T>.DynamicParametersFromGet(dynParameters);
        return connection.QueryFirstOrDefaultAsync<T>(sql, dynParameters, transaction, commandTimeout);
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
    public static Task<T> GetAsync<T>(this IDbConnection connection, object whereConditions,
        IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = SelectBuilder<T>.BuildSelectStatement(whereConditions);
        return connection.QueryFirstOrDefaultAsync<T>(sql, whereConditions, transaction, commandTimeout);
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
    public static Task<T> GetLargePropertiesAsync<T>(this IDbConnection connection, object whereConditions,
        IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = SelectBuilder<T>.BuildSelectStatement(whereConditions);
        var parameters = DynamicParametersHelper<T>.DynamicParametersFromWhere(whereConditions);
        parameters = DynamicParametersHelper<T>.DynamicParametersFromGet(parameters);
        return connection.QueryFirstOrDefaultAsync<T>(sql, parameters, transaction, commandTimeout);
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
    public static Task<IEnumerable<T>> GetListAsync<T>(this IDbConnection connection, object whereConditions, object? orderBy = null,
        IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = SelectBuilder<T>.BuildSelectStatement(whereConditions, orderBy);
        return connection.QueryAsync<T>(sql, whereConditions, transaction, commandTimeout);
    }

    /// <summary>
    /// Gets the results of a query from the entity.
    /// This is an extension function for the IDBConnection class
    /// </summary>
    /// <typeparam name="T">The Entity type</typeparam>
    /// <param name="connection">The IDbConnetion to use</param>
    /// <param name="whereConditions">The direct where clause</param>
    /// <param name="parameters">The parameters to populate in the where clause</param>
    /// <param name="orderBy">The order by values in the form Column=true/false. True if ascending, false if descending.</param>
    /// <param name="transaction">The transaction if in one.</param>
    /// <param name="commandTimeout">The timeout value, default none.</param>
    /// <returns>An Enumerable of the entities found.</returns>
    public static Task<IEnumerable<T>> GetListAsync<T>(this IDbConnection connection, string whereConditions, object? parameters = null,
        object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = SelectBuilder<T>.BuildSelectStatement(whereConditions, orderBy);
        return connection.QueryAsync<T>(sql, parameters, transaction, commandTimeout);
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
    public static Task<IEnumerable<T>> GetListLargePropertiesAsync<T>(this IDbConnection connection, object whereConditions, object? orderBy = null,
        IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = SelectBuilder<T>.BuildSelectStatement(whereConditions, orderBy);
        var parameters = DynamicParametersHelper<T>.DynamicParametersFromWhere(whereConditions);
        parameters = DynamicParametersHelper<T>.DynamicParametersFromGet(parameters);
        return connection.QueryAsync<T>(sql, parameters, transaction, commandTimeout);
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
    public static Task<IEnumerable<T>> GetListLargePropertiesAsync<T>(this IDbConnection connection, string whereConditions, object? parameters = null,
        object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = SelectBuilder<T>.BuildSelectStatement(whereConditions, orderBy);
        var dynParameters = DynamicParametersHelper<T>.DynamicParametersFromWhere(whereConditions);
        dynParameters = DynamicParametersHelper<T>.DynamicParametersFromGet(dynParameters);
        return connection.QueryAsync<T>(sql, dynParameters, transaction, commandTimeout);
    }

    /// <summary>
    /// Returns the number of rows in the table that holds T
    /// </summary>
    /// <typeparam name="T">The Entity type</typeparam>
    /// <param name="connection">The IDbConnetion to use</param>
    /// <param name="transaction">The transaction if in one.</param>
    /// <param name="commandTimeout">The timeout value, default none.</param>
    /// <returns></returns>
    public static Task<int> CountAsync<T>(this IDbConnection connection, IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = SelectBuilder<T>.BuildCountStatement();
        return connection.QueryFirstAsync<int>(sql, null, transaction, commandTimeout);
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
    public static Task<int> CountAsync<T>(this IDbConnection connection, object whereConditions,
        IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = SelectBuilder<T>.BuildCountStatement(whereConditions);
        var parameters = DynamicParametersHelper<T>.DynamicParametersFromWhere(whereConditions);
        return connection.QueryFirstAsync<int>(sql, parameters, transaction, commandTimeout);
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
    public static Task<int> CountAsync<T>(IDbConnection connection, string whereConditions, object? parameters = null,
        IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = SelectBuilder<T>.BuildCountStatement(whereConditions);
        var dynParameters = DynamicParametersHelper<T>.DynamicParametersFromWhere(whereConditions);
        return connection.QueryFirstAsync<int>(sql, dynParameters, transaction, commandTimeout);
    }
    #endregion

    #region Insert function
    public static Task<int> InsertAsync<T>(this IDbConnection connection, T entity, IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = InsertBuilder<T>.BuildInsertStatement();
        return connection.ExecuteScalarAsync<int>(sql, entity, transaction, commandTimeout);
    }

    public static Task<string> InsertReturnStringAsync<T>(this IDbConnection connection, T entity, IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = InsertBuilder<T>.BuildInsertStatement();
        return connection.ExecuteScalarAsync<string>(sql, entity, transaction, commandTimeout);
    }
    #endregion

    #region Update functions
    public static Task<int> UpdateAsync<T>(this IDbConnection connection, object parameters, IDbTransaction? transaction = null,
        int? commandTimeout = null) where T : class
    {
        string sql = UpdateBuilder<T>.BuildUpdateIdStatement(parameters);
        var dynParameters = UpdateBuilder<T>.GetUpdateParameters(parameters);
        return connection.ExecuteAsync(sql, dynParameters, transaction, commandTimeout);
    }

    public static Task<int> UpdateAsync<T>(this IDbConnection connection, object where, object set, IDbTransaction? transaction = null,
        int? commandTimeout = null) where T : class
    {
        string sql = UpdateBuilder<T>.BuildUpdateStatement(where, set);
        var whereParameters = DynamicParametersHelper<T>.DynamicParametersFromWhere(where);
        var dynParameters = DynamicParametersHelper<T>.DynamicParametersUpdate(set, whereParameters);
        return connection.ExecuteAsync(sql, dynParameters, transaction, commandTimeout);
    }

    public static Task<int> UpdateAsync<T>(this IDbConnection connection, T entity, object updates, IDbTransaction? transaction = null,
        int? commandTimeout = null) where T : class
    {
        string sql = UpdateBuilder<T>.BuildUpdateEntityStatement();
        return connection.ExecuteAsync(sql, entity, transaction, commandTimeout);
    }
    #endregion

    #region Delete functions
    public static Task<int> DeleteIdAsync<T>(this IDbConnection connection, int id, IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = DeleteBuilder<T>.BuildDeleteIdStatement();
        var dynParameters = WhereBuilder<T>.GetIdParameters(id);
        return connection.ExecuteAsync(sql, dynParameters, transaction, commandTimeout);
    }

    public static Task<int> DeleteWhereAsync<T>(this IDbConnection connection, object whereConditions,
    IDbTransaction? transaction = null, int? commandTimeout = null) where T : class
    {
        string sql = DeleteBuilder<T>.BuildDeleteStatement(whereConditions);
        return connection.ExecuteAsync(sql, whereConditions, transaction, commandTimeout);
    }
    #endregion
}
