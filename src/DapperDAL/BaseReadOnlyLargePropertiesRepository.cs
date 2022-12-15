namespace DapperDAL;

public abstract class BaseReadOnlyLargePropertiesRepository<T, Key> : IReadOnlyLargePropertiesRepository<T, Key> where T : class
{
    #region Constructors
    private readonly DapperDALSettings _settings;

    protected BaseReadOnlyLargePropertiesRepository() => _settings = DapperDALSettings.DefaultSettings;

    protected BaseReadOnlyLargePropertiesRepository(DapperDALSettings settings) => _settings = settings;
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
        => connection.GetIdLargeProperties<T>(key, transaction, commandTimeout);

    public T? Get(object whereConditions, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Get(connection, whereConditions, null, commandTimeout);
    }

    public T? Get(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetLargeProperties<T>(whereConditions, transaction, commandTimeout);

    public IEnumerable<T> GetList(object? whereConditions = null, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetList(connection, whereConditions, orderBy, null, commandTimeout);
    }

    public IEnumerable<T> GetList(IDbConnection connection, object? whereConditions = null, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetListLargeProperties<T>(whereConditions, orderBy, transaction, commandTimeout);

    public IEnumerable<T> GetList(string whereConditions, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetList(connection, whereConditions, orderBy, null, commandTimeout);
    }

    public IEnumerable<T> GetList(IDbConnection connection, string whereConditions, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetListLargeProperties<T>(whereConditions, orderBy, transaction, commandTimeout);


    public async Task<T?> GetAsync(Key key, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await GetAsync(connection, key, null, commandTimeout);
    }

    public Task<T?> GetAsync(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetIdLargePropertiesAsync<T>(key, transaction, commandTimeout);

    public Task<T?> GetAsync(object whereConditions, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetAsync(connection, whereConditions, null, commandTimeout);
    }

    public Task<T?> GetAsync(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetLargePropertiesAsync<T>(whereConditions, transaction, commandTimeout);

    public async Task<IEnumerable<T>> GetListAsync(object? whereConditions = null, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await GetListAsync(connection, whereConditions, orderBy, null, commandTimeout);
    }

    public Task<IEnumerable<T>> GetListAsync(IDbConnection connection, object? whereConditions = null, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetListLargePropertiesAsync<T>(whereConditions, orderBy, transaction, commandTimeout);

    public async Task<IEnumerable<T>> GetListAsync(string whereConditions, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await GetListAsync(connection, whereConditions, orderBy, null, commandTimeout);
    }

    public Task<IEnumerable<T>> GetListAsync(IDbConnection connection, string whereConditions, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetListLargePropertiesAsync<T>(whereConditions, orderBy, transaction, commandTimeout);
    #endregion
}
