using System.Threading;

namespace DapperRepository;

public abstract class BaseReadRepository<T, Key> : IReadRepository<T, Key> where T : class
{
    #region Constructors
    private readonly DapperRepositorySettings _settings;
    private readonly bool _hasLargeProperties;

    protected BaseReadRepository()
    {
        _settings = DapperRepositorySettings.DefaultSettings;
        _hasLargeProperties = BuilderCache<T>.LargeProperties.Length > 0;
    }

    protected BaseReadRepository(DapperRepositorySettings settings)
    {
        _settings = settings;
        _hasLargeProperties = BuilderCache<T>.LargeProperties.Length > 0;
    }
    #endregion

    #region Connection
    protected abstract IDbConnection GetConnection();

    protected string? GetConnectionString(string connectionStringName = "Default") => _settings.ConnectionString(connectionStringName);
    #endregion

    #region Timeout Handling
    public int? DefaultTimeout { get; set; }

    protected int? GetTimeout(int? timeout) => (timeout is not null) ? timeout : DefaultTimeout;
    #endregion

    #region Get
    public T? Get(Key key, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Get(connection, key, null, commandTimeout);
    }

    public T? Get(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null)
        => !_hasLargeProperties ? connection.GetId<T>(key, transaction, commandTimeout) :
                                 connection.GetIdLargeProperties<T>(key, transaction, GetTimeout(commandTimeout));

    public T? Get(object whereConditions, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Get(connection, whereConditions, null, commandTimeout);
    }

    public T? Get(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null)
        => !_hasLargeProperties ? connection.Get<T>(whereConditions, transaction, commandTimeout) :
                                 connection.GetLargeProperties<T>(whereConditions, transaction, GetTimeout(commandTimeout));

    public IEnumerable<T> GetWhere(object? whereConditions = null, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetWhere(connection, whereConditions, orderBy, null, commandTimeout);
    }

    public IEnumerable<T> GetWhere(IDbConnection connection, object? whereConditions = null, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => !_hasLargeProperties ? connection.GetWhere<T>(whereConditions, orderBy, transaction, commandTimeout) :
                                 connection.GetWhereLargeProperties<T>(whereConditions, orderBy, transaction, GetTimeout(commandTimeout));

    public IEnumerable<T> GetWhereStatement(string whereConditions, object parameters, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetWhereStatement(connection, whereConditions, parameters, orderBy, null, commandTimeout);
    }

    public IEnumerable<T> GetWhereStatement(IDbConnection connection, string whereConditions, object parameters, object? orderBy = null, IDbTransaction? transaction = null,
        int? commandTimeout = null)
        => !_hasLargeProperties ? connection.GetWhereStatement<T>(whereConditions, parameters, orderBy, transaction, commandTimeout) :
                                 connection.GetWhereStatementLargeProperties<T>(whereConditions, parameters, orderBy, transaction, GetTimeout(commandTimeout));


    public int Count(object? whereConditions = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Count(connection, whereConditions, null, commandTimeout);
    }
    public int Count(IDbConnection connection, object? whereConditions = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.Count<T>(whereConditions, transaction, GetTimeout(commandTimeout));
    #endregion

    #region Get Async
    public async Task<T?> GetAsync(Key key, int? commandTimeout = null, CancellationToken cancellationToken = default)
    {
        using var connection = GetConnection();
        return await GetAsync(connection, key, null, commandTimeout, cancellationToken);
    }

    public Task<T?> GetAsync(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null, 
        CancellationToken cancellationToken = default)
        => !_hasLargeProperties ? connection.GetIdAsync<T>(key, transaction, commandTimeout, cancellationToken) :
                                 connection.GetIdLargePropertiesAsync<T>(key, transaction, GetTimeout(commandTimeout), cancellationToken);

    public async Task<T?> GetAsync(object whereConditions, int? commandTimeout = null, CancellationToken cancellationToken = default)
    {
        using var connection = GetConnection();
        return await GetAsync(connection, whereConditions, null, commandTimeout, cancellationToken);
    }

    public Task<T?> GetAsync(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, 
        int? commandTimeout = null, CancellationToken cancellationToken = default)
        => !_hasLargeProperties ? connection.GetAsync<T>(whereConditions, transaction, commandTimeout, cancellationToken) :
                                 connection.GetLargePropertiesAsync<T>(whereConditions, transaction, GetTimeout(commandTimeout), cancellationToken);

    public async Task<IEnumerable<T>> GetWhereAsync(object? whereConditions = null, object? orderBy = null, int? commandTimeout = null,
        CancellationToken cancellationToken = default)
    {
        using var connection = GetConnection();
        return await GetWhereAsync(connection, whereConditions, orderBy, null, commandTimeout, cancellationToken);
    }

    public Task<IEnumerable<T>> GetWhereAsync(IDbConnection connection, object? whereConditions = null, object? orderBy = null, 
        IDbTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default)
        => !_hasLargeProperties ? connection.GetWhereAsync<T>(whereConditions, orderBy, transaction, commandTimeout, cancellationToken) :
                                 connection.GetWhereLargePropertiesAsync<T>(whereConditions, orderBy, transaction, GetTimeout(commandTimeout), cancellationToken);

    public async Task<IEnumerable<T>> GetWhereStatementAsync(string whereConditions, object parameters, object? orderBy = null, 
        int? commandTimeout = null, CancellationToken cancellationToken = default)
    {
        using var connection = GetConnection();
        return await GetWhereStatementAsync(connection, whereConditions, parameters, orderBy, null, commandTimeout, cancellationToken);
    }

    public Task<IEnumerable<T>> GetWhereStatementAsync(IDbConnection connection, string whereConditions, object parameters, object? orderBy = null,
        IDbTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default)
        => !_hasLargeProperties ? connection.GetWhereStatementAsync<T>(whereConditions, parameters, orderBy, transaction, commandTimeout, cancellationToken) :
                                 connection.GetWhereStatementLargePropertiesAsync<T>(whereConditions, parameters, orderBy, transaction, GetTimeout(commandTimeout), cancellationToken);

    public async Task<int> CountAsync(object? whereConditions = null, int? commandTimeout = null, CancellationToken cancellationToken = default)
    {
        using var connection = GetConnection();
        return await CountAsync(connection, whereConditions, null, commandTimeout, cancellationToken);
    }

    public Task<int> CountAsync(IDbConnection connection, object? whereConditions = null, IDbTransaction? transaction = null, 
        int? commandTimeout = null, CancellationToken cancellationToken = default)
        => connection.CountAsync<T>(whereConditions!, transaction, GetTimeout(commandTimeout), cancellationToken);
    #endregion
}
