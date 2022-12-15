namespace DapperDAL;

public abstract class BaseRepository<T, Key> : IRepository<T, Key> where T : class
{
    #region Constructors
    private readonly DapperDALSettings _settings;

    protected BaseRepository() => _settings = DapperDALSettings.DefaultSettings;

    protected BaseRepository(DapperDALSettings settings) => _settings = settings;
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

    public async Task<int> CountAsync(object? whereConditions = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await CountAsync(connection, whereConditions, null, commandTimeout);
    }

    public Task<int> CountAsync(IDbConnection connection, object? whereConditions = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.CountAsync<T>(whereConditions, transaction, commandTimeout);
    #endregion

    #region Insert
    public Key Insert(T entity, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Insert(connection, entity, null, commandTimeout);
    }

    public Key Insert(IDbConnection connection, T entity, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        string sql = BuilderCache<T>.InsertStatement; //InsertBuilder<T>.BuildInsertStatement();
        return connection.ExecuteScalar<Key>(sql, entity, transaction, commandTimeout);
    }

    public async Task<Key> InsertAsync(T entity, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await InsertAsync(connection, entity, null, commandTimeout);
    }

    public Task<Key> InsertAsync(IDbConnection connection, T entity, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        string sql = BuilderCache<T>.InsertStatement; //InsertBuilder<T>.BuildInsertStatement();
        return connection.ExecuteScalarAsync<Key>(sql, entity, transaction, commandTimeout);
    }
    #endregion

    #region Update
    public int Update(T entity, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Update(connection, entity, null, commandTimeout);
    }

    public int Update(IDbConnection connection, T entity, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.Update<T>(entity, transaction, commandTimeout);

    public int Update(Key key, object set, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Update(connection, key, set, null, commandTimeout);
    }

    public int Update(IDbConnection connection, Key key, object set, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.Update<T, Key>(key, set, transaction, commandTimeout);

    public int UpdateWhere(object where, object set, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return UpdateWhere(connection, where, set, null, commandTimeout);
    }

    public int UpdateWhere(IDbConnection connection, object where, object set, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.UpdateWhere<T>(where, set, transaction, commandTimeout);

    public async Task<int> UpdateAsync(T entity, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await UpdateAsync(connection, entity, null, commandTimeout);
    }

    public Task<int> UpdateAsync(IDbConnection connection, T entity, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.UpdateAsync<T>(entity, transaction, commandTimeout);

    public Task<int> UpdateAsync(Key key, object set, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return UpdateAsync(connection, key, set, null, commandTimeout);
    }

    public Task<int> UpdateAsync(IDbConnection connection, Key key, object set, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.UpdateAsync<T, Key>(key, set, transaction, commandTimeout);

    public async Task<int> UpdateWhereAsync(object where, object set, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await UpdateWhereAsync(connection, where, set, null, commandTimeout);
    }

    public Task<int> UpdateWhereAsync(IDbConnection connection, object where, object set, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.UpdateWhereAsync<T>(where, set, transaction, commandTimeout);
    #endregion

    #region Delete
    public int Delete(Key id, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Delete(connection, id, null, commandTimeout);
    }

    public int Delete(IDbConnection connection, Key id, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        string sql = DeleteBuilder<T>.BuildDeleteIdStatement();
        var dynParameters = WhereBuilder<T>.GetIdParameters(id);
        return connection.Execute(sql, dynParameters, transaction, commandTimeout);
    }

    public int Delete(object whereConditions, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Delete(connection, whereConditions, null, commandTimeout);
    }

    public int Delete(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.DeleteWhere<T>(whereConditions, transaction, commandTimeout);

    public async Task<int> DeleteAsync(Key id, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await DeleteAsync(connection, id, null, commandTimeout);
    }

    public Task<int> DeleteAsync(IDbConnection connection, Key id, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        string sql = DeleteBuilder<T>.BuildDeleteIdStatement();
        var dynParameters = WhereBuilder<T>.GetIdParameters(id);
        return connection.ExecuteAsync(sql, dynParameters, transaction, commandTimeout);
    }

    public async Task<int> DeleteAsync(object whereConditions, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return await DeleteAsync(connection, whereConditions, null, commandTimeout);
    }

    public Task<int> DeleteAsync(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.DeleteWhereAsync<T>(whereConditions, transaction, commandTimeout);
    #endregion
}
