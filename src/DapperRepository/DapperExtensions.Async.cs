using System.Security.Cryptography;
using System.Threading;
using static Dapper.SqlMapper;

namespace DapperRepository;

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
    public static Task<T?> GetIdAsync<T>(this IDbConnection connection, object id, 
        IDbTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
    {
        string sql = SelectBuilder<T>.BuildSqlSelectIdString();
        var dynParameters = WhereBuilder<T>.GetIdParameters(id);
        CommandDefinition command = new(sql, dynParameters, transaction, commandTimeout, CommandType.Text, 
            CommandFlags.Buffered, cancellationToken);
        return connection.QueryFirstOrDefaultAsync<T?>(command);
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
    public static Task<T?> GetIdLargePropertiesAsync<T>(this IDbConnection connection, object id,
        IDbTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
    {
        string sql = SelectBuilder<T>.BuildSqlSelectIdString();
        var dynParameters = WhereBuilder<T>.GetIdParameters(id);
        dynParameters = DynamicParametersHelper<T>.DynamicParametersFromGet(dynParameters);
        CommandDefinition command = new(sql, dynParameters, transaction, commandTimeout, CommandType.Text,
            CommandFlags.Buffered, cancellationToken);
        return connection.QueryFirstOrDefaultAsync<T?>(command);
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
    public static Task<T?> GetAsync<T>(this IDbConnection connection, object whereConditions,
        IDbTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
    {
        string sql = SelectBuilder<T>.BuildSelectStatement(whereConditions);
        CommandDefinition command = new(sql, null, transaction, commandTimeout, CommandType.Text,
            CommandFlags.Buffered, cancellationToken);
        return connection.QueryFirstOrDefaultAsync<T?>(command);
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
    public static Task<T?> GetLargePropertiesAsync<T>(this IDbConnection connection, object whereConditions,
        IDbTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
    {
        string sql = SelectBuilder<T>.BuildSelectStatement(whereConditions);
        var parameters = DynamicParametersHelper<T>.DynamicParametersFromWhere(whereConditions);
        parameters = DynamicParametersHelper<T>.DynamicParametersFromGet(parameters);
        CommandDefinition command = new(sql, parameters, transaction, commandTimeout, CommandType.Text,
            CommandFlags.Buffered, cancellationToken);
        return connection.QueryFirstOrDefaultAsync<T?>(command);
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
    public static Task<IEnumerable<T>> GetWhereAsync<T>(this IDbConnection connection, object? whereConditions = null, 
        object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null, 
        CancellationToken cancellationToken = default) where T : class
    {
        string sql = SelectBuilder<T>.BuildSelectStatement(whereConditions, orderBy);
        CommandDefinition command = new(sql, null, transaction, commandTimeout, CommandType.Text,
            CommandFlags.Buffered, cancellationToken);
        return connection.QueryAsync<T>(command);
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
    public static Task<IEnumerable<T>> GetWhereStatementAsync<T>(this IDbConnection connection, string whereConditions, object? parameters = null,
        object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null,
        CancellationToken cancellationToken = default) where T : class
    {
        string sql = SelectBuilder<T>.BuildSelectStatement(whereConditions, orderBy);
        CommandDefinition command = new(sql, null, transaction, commandTimeout, CommandType.Text,
            CommandFlags.Buffered, cancellationToken);
        return connection.QueryAsync<T>(command);
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
    public static Task<IEnumerable<T>> GetWhereLargePropertiesAsync<T>(this IDbConnection connection, object? whereConditions = null,
        object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null,
        CancellationToken cancellationToken = default) where T : class
    {
        string sql = SelectBuilder<T>.BuildSelectStatement(whereConditions, orderBy);
        var parameters = whereConditions is null ? new DynamicParameters() : DynamicParametersHelper<T>.DynamicParametersFromWhere(whereConditions);
        parameters = DynamicParametersHelper<T>.DynamicParametersFromGet(parameters);
        CommandDefinition command = new(sql, parameters, transaction, commandTimeout, CommandType.Text,
            CommandFlags.Buffered, cancellationToken);
        return connection.QueryAsync<T>(command);
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
    public static Task<IEnumerable<T>> GetWhereStatementLargePropertiesAsync<T>(this IDbConnection connection, string whereConditions, 
        object? parameters = null, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null, 
        CancellationToken cancellationToken = default) where T : class
    {
        string sql = SelectBuilder<T>.BuildSelectStatement(whereConditions, orderBy);
        var dynParameters = string.IsNullOrEmpty(whereConditions) ? new DynamicParameters() : DynamicParametersHelper<T>.DynamicParametersFromWhere(whereConditions);
        dynParameters = DynamicParametersHelper<T>.DynamicParametersFromGet(dynParameters);
        CommandDefinition command = new(sql, dynParameters, transaction, commandTimeout, CommandType.Text,
            CommandFlags.Buffered, cancellationToken);
        return connection.QueryAsync<T>(command);
    }

    /// <summary>
    /// Returns the number of rows in the table that holds T
    /// </summary>
    /// <typeparam name="T">The Entity type</typeparam>
    /// <param name="connection">The IDbConnetion to use</param>
    /// <param name="transaction">The transaction if in one.</param>
    /// <param name="commandTimeout">The timeout value, default none.</param>
    /// <returns></returns>
    public static Task<int> CountAsync<T>(this IDbConnection connection, IDbTransaction? transaction = null, int? commandTimeout = null, 
        CancellationToken cancellationToken = default) where T : class
    {
        string sql = SelectBuilder<T>.BuildCountStatement();
        CommandDefinition command = new(sql, null, transaction, commandTimeout, CommandType.Text,
            CommandFlags.Buffered, cancellationToken);
        return connection.QueryFirstAsync<int>(command);
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
        IDbTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
    {
        string sql = SelectBuilder<T>.BuildCountStatement(whereConditions);
        var parameters = DynamicParametersHelper<T>.DynamicParametersFromWhere(whereConditions);
        CommandDefinition command = new(sql, parameters, transaction, commandTimeout, CommandType.Text,
            CommandFlags.Buffered, cancellationToken);
        return connection.QueryFirstAsync<int>(command);
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
        IDbTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
    {
        string sql = SelectBuilder<T>.BuildCountStatement(whereConditions);
        var dynParameters = DynamicParametersHelper<T>.DynamicParametersFromWhere(whereConditions);
        CommandDefinition command = new(sql, dynParameters, transaction, commandTimeout, CommandType.Text,
            CommandFlags.Buffered, cancellationToken);
        return connection.QueryFirstAsync<int>(command);
    }
    #endregion

    #region Insert function
    public async static Task<Key> InsertAsync<T, Key>(this IDbConnection connection, T entity, IDbTransaction? transaction = null, 
        int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
    {
        string sql = InsertBuilder<T>.BuildInsertStatement();
        CommandDefinition command = new(sql, entity, transaction, commandTimeout, CommandType.Text, CommandFlags.None, cancellationToken);
        Key? key = await connection.ExecuteScalarAsync<Key>(command);
        if (key is not null)
            SetKey<T, Key>(entity, key);
        return key!;
    }

    public async static Task<string> InsertReturnStringAsync<T>(this IDbConnection connection, T entity, IDbTransaction? transaction = null, 
        int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
    {
        string sql = InsertBuilder<T>.BuildInsertStatement();
        CommandDefinition command = new(sql, entity, transaction, commandTimeout, CommandType.Text, CommandFlags.None, cancellationToken);
        string? key = await connection.ExecuteScalarAsync<string>(command);
        if (!string.IsNullOrWhiteSpace(key)) SetKey<T, string>(entity, key);
        return key!;
    }
    #endregion

    #region Update functions
    public static Task<int> UpdateAsync<T>(this IDbConnection connection, object parameters, IDbTransaction? transaction = null,
        int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
    {
        string sql = UpdateBuilder<T>.BuildUpdateIdStatement(parameters);
        var dynParameters = UpdateBuilder<T>.GetUpdateParameters(parameters);
        CommandDefinition command = new(sql, dynParameters, transaction, commandTimeout, CommandType.Text, CommandFlags.None, cancellationToken);
        return connection.ExecuteAsync(command);
    }

    public static Task<int> UpdateAsync<T, Key>(this IDbConnection connection, Key key, object set, IDbTransaction? transaction = null,
        int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
    {
        string sql = UpdateBuilder<T>.BuildUpdateIdStatement(set);
        var whereParameters = WhereBuilder<T>.GetIdParameters(key!);
        var dynParameters = DynamicParametersHelper<T>.DynamicParametersUpdate(set, whereParameters);
        CommandDefinition command = new(sql, dynParameters, transaction, commandTimeout, CommandType.Text, CommandFlags.None, cancellationToken);
        return connection.ExecuteAsync(command);
    }

    public static Task<int> UpdateWhereAsync<T>(this IDbConnection connection, object where, object set, IDbTransaction? transaction = null,
        int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
    {
        string sql = UpdateBuilder<T>.BuildUpdateStatement(where, set);
        var whereParameters = DynamicParametersHelper<T>.DynamicParametersFromWhere(where);
        var dynParameters = DynamicParametersHelper<T>.DynamicParametersUpdate(set, whereParameters);
        CommandDefinition command = new(sql, dynParameters, transaction, commandTimeout, CommandType.Text, CommandFlags.None, cancellationToken);
        return connection.ExecuteAsync(command);
    }

    public static Task<int> UpdateAsync<T>(this IDbConnection connection, T entity, object updates, IDbTransaction? transaction = null,
        int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
    {
        string sql = UpdateBuilder<T>.BuildUpdateEntityStatement();
        CommandDefinition command = new(sql, entity, transaction, commandTimeout, CommandType.Text, CommandFlags.None, cancellationToken);
        return connection.ExecuteAsync(command);
    }
    #endregion

    #region Delete functions
    public static Task<int> DeleteIdAsync<T>(this IDbConnection connection, int id, IDbTransaction? transaction = null, 
        int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
    {
        string sql = DeleteBuilder<T>.BuildDeleteIdStatement();
        var dynParameters = WhereBuilder<T>.GetIdParameters(id);
        CommandDefinition command = new(sql, dynParameters, transaction, commandTimeout, CommandType.Text, CommandFlags.None, cancellationToken);
        return connection.ExecuteAsync(command);
    }

    public static Task<int> DeleteWhereAsync<T>(this IDbConnection connection, object whereConditions,
    IDbTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where T : class
    {
        string sql = DeleteBuilder<T>.BuildDeleteStatement(whereConditions);
        CommandDefinition command = new(sql, whereConditions, transaction, commandTimeout, CommandType.Text, CommandFlags.None, cancellationToken);
        return connection.ExecuteAsync(command);
    }
    #endregion
}
