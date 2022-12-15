namespace DapperDAL;

public abstract class BaseLargePropertiesRepository<T, Key> : 
    BaseRepository<T, Key>, ILargePropertiesRepository<T, Key> where T : class
{
    #region Constructors
    private readonly DapperDALSettings _settings;

    protected BaseLargePropertiesRepository() => _settings = DapperDALSettings.DefaultSettings;

    protected BaseLargePropertiesRepository(DapperDALSettings settings) : base(settings) 
    { }
    #endregion

    #region Get
    public new T Get(Key key, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Get(connection, key, null, commandTimeout);
    }

    public new T Get(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetIdLargeProperties<T>(key, transaction, commandTimeout);

    public new T Get(object whereConditions, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Get(connection, whereConditions, null, commandTimeout);
    }

    public new T Get(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetLargeProperties<T>(whereConditions, transaction, commandTimeout);

    public new IEnumerable<T> GetList(object? whereConditions = null, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetList(connection, whereConditions, orderBy, null, commandTimeout);
    }

    public new IEnumerable<T> GetList(IDbConnection connection, object? whereConditions = null, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetListLargeProperties<T>(whereConditions, orderBy, transaction, commandTimeout);

    public new IEnumerable<T> GetList(string whereConditions, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetList(connection, whereConditions, orderBy, null, commandTimeout);
    }

    public new IEnumerable<T> GetList(IDbConnection connection,string whereConditions, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetListLargeProperties<T>(whereConditions, orderBy, transaction, commandTimeout);


    public new async Task<T> GetAsync(Key key, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await GetAsync(connection, key, null, commandTimeout);
    }

    public new Task<T> GetAsync(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetIdLargePropertiesAsync<T>(key, transaction, commandTimeout);

    public new Task<T> GetAsync(object whereConditions, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetAsync(connection, whereConditions, null, commandTimeout);
    }

    public new Task<T> GetAsync(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetLargePropertiesAsync<T>(whereConditions, transaction, commandTimeout);

    public new async Task<IEnumerable<T>> GetListAsync(object? whereConditions = null, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await GetListAsync(connection, whereConditions, orderBy, null, commandTimeout);
    }

    public new Task<IEnumerable<T>> GetListAsync(IDbConnection connection, object? whereConditions = null, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetListLargePropertiesAsync<T>(whereConditions, orderBy, transaction, commandTimeout);

    public new async Task<IEnumerable<T>> GetListAsync(string whereConditions, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await GetListAsync(connection, whereConditions, orderBy, null, commandTimeout);
    }

    public new Task<IEnumerable<T>> GetListAsync(IDbConnection connection, string whereConditions, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetListLargePropertiesAsync<T>(whereConditions, orderBy, transaction, commandTimeout);
    #endregion

}
