namespace DapperDAL;

public abstract class BaseReadOnlyRepository<T, Key> : IReadOnlyRepository<T, Key> where T : class
{
    #region Constructors
    private readonly DapperDALSettings _settings;
    private readonly bool _hasLargeProperties;

    protected BaseReadOnlyRepository()
    {
        _settings = DapperDALSettings.DefaultSettings;
        _hasLargeProperties = BuilderCache<T>.LargeProperties.Length > 0;
    }

    protected BaseReadOnlyRepository(DapperDALSettings settings)
    {
        _settings = settings;
        _hasLargeProperties = BuilderCache<T>.LargeProperties.Length > 0;
    }
    #endregion

    #region Connection
    protected abstract IDbConnection GetConnection();

    protected string? GetConnectionString(string connectionStringName = "Default") => _settings.ConnectionString(connectionStringName);
    #endregion

    #region Get
    public T? Get(Key key, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Get(connection, key, null, commandTimeout);
    }

    public T? Get(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null)
        => !_hasLargeProperties ? connection.GetId<T>(key, transaction, commandTimeout) :
                                 connection.GetIdLargeProperties<T>(key, transaction, commandTimeout);

    public T? Get(object whereConditions, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Get(connection, whereConditions, null, commandTimeout);
    }

    public T? Get(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null)
        => !_hasLargeProperties ? connection.Get<T>(whereConditions, transaction, commandTimeout) :
                                 connection.GetLargeProperties<T>(whereConditions, transaction, commandTimeout);

    public IEnumerable<T> GetWhere(object? whereConditions = null, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetWhere(connection, whereConditions, orderBy, null, commandTimeout);
    }

    public IEnumerable<T> GetWhere(IDbConnection connection, object? whereConditions = null, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => !_hasLargeProperties ? connection.GetWhere<T>(whereConditions, orderBy, transaction, commandTimeout) :
                                 connection.GetWhereLargeProperties<T>(whereConditions, orderBy, transaction, commandTimeout);

    public IEnumerable<T> GetWhereStatement(string whereConditions, object parameters, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetWhereStatement(connection, whereConditions, parameters, orderBy, null, commandTimeout);
    }

    public IEnumerable<T> GetWhereStatement(IDbConnection connection, string whereConditions, object parameters, object? orderBy = null, IDbTransaction? transaction = null,
        int? commandTimeout = null)
        => !_hasLargeProperties ? connection.GetWhereStatement<T>(whereConditions, parameters, orderBy, transaction, commandTimeout) :
                                 connection.GetWhereStatementLargeProperties<T>(whereConditions, parameters, orderBy, transaction, commandTimeout);


    public int Count(object? whereConditions = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Count(connection, whereConditions, null, commandTimeout);
    }
    public int Count(IDbConnection connection, object? whereConditions = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.Count<T>(whereConditions, transaction, commandTimeout);


    public async Task<T?> GetAsync(Key key, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await GetAsync(connection, key, null, commandTimeout);
    }

    public Task<T?> GetAsync(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null)
        => !_hasLargeProperties ? connection.GetIdAsync<T>(key, transaction, commandTimeout) :
                                 connection.GetIdLargePropertiesAsync<T>(key, transaction, commandTimeout);

    public Task<T?> GetAsync(object whereConditions, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetAsync(connection, whereConditions, null, commandTimeout);
    }

    public Task<T?> GetAsync(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null)
        => !_hasLargeProperties ? connection.GetAsync<T>(whereConditions, transaction, commandTimeout) :
                                 connection.GetLargePropertiesAsync<T>(whereConditions, transaction, commandTimeout);

    public async Task<IEnumerable<T>> GetWhereAsync(object? whereConditions = null, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await GetWhereAsync(connection, whereConditions, orderBy, null, commandTimeout);
    }

    public Task<IEnumerable<T>> GetWhereAsync(IDbConnection connection, object? whereConditions = null, object? orderBy = null, IDbTransaction? transaction = null,
        int? commandTimeout = null)
        => !_hasLargeProperties ? connection.GetWhereAsync<T>(whereConditions, orderBy, transaction, commandTimeout) :
                                 connection.GetWhereLargePropertiesAsync<T>(whereConditions, orderBy, transaction, commandTimeout);

    public async Task<IEnumerable<T>> GetWhereStatementAsync(string whereConditions, object parameters, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await GetWhereStatementAsync(connection, whereConditions, parameters, orderBy, null, commandTimeout);
    }

    public Task<IEnumerable<T>> GetWhereStatementAsync(IDbConnection connection, string whereConditions, object parameters, object? orderBy = null,
        IDbTransaction? transaction = null, int? commandTimeout = null)
        => !_hasLargeProperties ? connection.GetWhereStatementAsync<T>(whereConditions, parameters, orderBy, transaction, commandTimeout) :
                                 connection.GetWhereStatementLargePropertiesAsync<T>(whereConditions, parameters, orderBy, transaction, commandTimeout);

    public async Task<int> CountAsync(object? whereConditions = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await CountAsync(connection, whereConditions, null, commandTimeout);
    }

    public Task<int> CountAsync(IDbConnection connection, object? whereConditions = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.CountAsync<T>(whereConditions, transaction, commandTimeout);
    #endregion
}
