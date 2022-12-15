namespace DapperDAL;

public abstract class BaseReadOnlyRepository<T, Key> : IReadOnlyRepository<T, Key> where T : class
{
    #region Constructors
    private readonly DapperDALSettings _settings;

    protected BaseReadOnlyRepository() => _settings = DapperDALSettings.DefaultSettings;

    protected BaseReadOnlyRepository(DapperDALSettings settings) => _settings = settings;
    #endregion

    #region Connection
    protected abstract IDbConnection GetConnection();

    protected string? GetConnectionString(string connectionStringName = "Default") => _settings.ConnectionString(connectionStringName);
    #endregion

    #region Get
    public T Get(Key key, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Get(connection, key, null, commandTimeout);
    }

    public T Get(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetId<T>(key, transaction, commandTimeout);

    public T Get(object whereConditions, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Get(connection, whereConditions, null, commandTimeout);
    }

    public T Get(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.Get<T>(whereConditions, transaction, commandTimeout);

    public IEnumerable<T> GetList(object? whereConditions = null, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetList(connection, whereConditions, orderBy, null, commandTimeout);
    }

    public IEnumerable<T> GetList(IDbConnection connection, object? whereConditions = null, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetList<T>(whereConditions, orderBy, transaction, commandTimeout);

    public IEnumerable<T> GetList(string whereConditions, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetList(connection, whereConditions, orderBy, null, commandTimeout);
    }

    public IEnumerable<T> GetList(IDbConnection connection, string whereConditions, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetList<T>(whereConditions, orderBy, transaction, commandTimeout);


    public int Count(object? whereConditions = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Count(connection, whereConditions, null, commandTimeout);
    }
    public int Count(IDbConnection connection, object? whereConditions = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.Count<T>(whereConditions, transaction, commandTimeout);

    public async Task<T> GetAsync(Key key, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await GetAsync(connection, key, null, commandTimeout);
    }

    public Task<T> GetAsync(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetIdAsync<T>(key, transaction, commandTimeout);

    public Task<T> GetAsync(object whereConditions, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetAsync(connection, whereConditions, null, commandTimeout);
    }

    public Task<T> GetAsync(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetAsync<T>(whereConditions, transaction, commandTimeout);

    public async Task<IEnumerable<T>> GetListAsync(object? whereConditions = null, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await GetListAsync(connection, whereConditions, orderBy, null, commandTimeout);
    }

    public Task<IEnumerable<T>> GetListAsync(IDbConnection connection, object? whereConditions = null, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetListAsync<T>(whereConditions, orderBy, transaction, commandTimeout);

    public async Task<IEnumerable<T>> GetListAsync(string whereConditions, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await GetListAsync(connection, whereConditions, orderBy, null, commandTimeout);
    }

    public Task<IEnumerable<T>> GetListAsync(IDbConnection connection, string whereConditions, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetListAsync<T>(whereConditions, orderBy, transaction, commandTimeout);

    public async Task<int> CountAsync(object? whereConditions = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await CountAsync(connection, whereConditions, null, commandTimeout);
    }

    public Task<int> CountAsync(IDbConnection connection, object? whereConditions = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.CountAsync<T>(whereConditions, transaction, commandTimeout);
    #endregion
}
