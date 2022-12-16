namespace DapperDAL;

public abstract class BaseLargePropertiesRepository<T, Key> : 
    BaseRepository<T, Key>, ILargePropertiesRepository<T, Key> where T : class
{
    #region Constructors
    protected BaseLargePropertiesRepository()
    { }

    protected BaseLargePropertiesRepository(DapperDALSettings settings) : base(settings) 
    { }
    #endregion

    #region Get
    public new T? Get(Key key, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Get(connection, key, null, commandTimeout);
    }

    public new T? Get(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetIdLargeProperties<T>(key, transaction, commandTimeout);

    public new T? Get(object whereConditions, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Get(connection, whereConditions, null, commandTimeout);
    }

    public new T? Get(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetLargeProperties<T>(whereConditions, transaction, commandTimeout);

    public new IEnumerable<T> GetWhere(object? whereConditions = null, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetWhere(connection, whereConditions, orderBy, null, commandTimeout);
    }

    public new IEnumerable<T> GetWhere(IDbConnection connection, object? whereConditions = null, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetWhereLargeProperties<T>(whereConditions, orderBy, transaction, commandTimeout);

    public new IEnumerable<T> GetWhereStatement(string whereConditions, object parameters, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetWhereStatement(connection, whereConditions, parameters, orderBy, null, commandTimeout);
    }

    public new IEnumerable<T> GetWhereStatement(IDbConnection connection, string whereConditions, object parameters, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetWhereStatementLargeProperties<T>(whereConditions, parameters, orderBy, transaction, commandTimeout);


    public new async Task<T?> GetAsync(Key key, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await GetAsync(connection, key, null, commandTimeout);
    }

    public new Task<T?> GetAsync(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetIdLargePropertiesAsync<T>(key, transaction, commandTimeout);

    public new Task<T?> GetAsync(object whereConditions, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetAsync(connection, whereConditions, null, commandTimeout);
    }

    public new Task<T?> GetAsync(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetLargePropertiesAsync<T>(whereConditions, transaction, commandTimeout);

    public new async Task<IEnumerable<T>> GetWhereAsync(object? whereConditions = null, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await GetWhereAsync(connection, whereConditions, orderBy, null, commandTimeout);
    }

    public new Task<IEnumerable<T>> GetWhereAsync(IDbConnection connection, object? whereConditions = null, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetWhereLargePropertiesAsync<T>(whereConditions, orderBy, transaction, commandTimeout);

    public new async Task<IEnumerable<T>> GetWhereStatementAsync(string whereConditions, object parameters, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await GetWhereStatementAsync(connection, whereConditions, parameters, orderBy, null, commandTimeout);
    }

    public new Task<IEnumerable<T>> GetWhereStatementAsync(IDbConnection connection, string whereConditions, object parameters, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetWhereStatementLargePropertiesAsync<T>(whereConditions, parameters, orderBy, transaction, commandTimeout);
    #endregion
}
