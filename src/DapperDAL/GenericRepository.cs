namespace DapperDAL;

public abstract class GenericRepository<T, Key> : IGenericRepository<T, Key> where T : class
{
    protected abstract IDbConnection GetConnection();

    #region Get
    public T Get(Key key, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Get(connection, key, null, commandTimeout);
    }

    public T Get(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetId<T>(key, transaction, commandTimeout);

    public IEnumerable<T> GetList(object whereConditions, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetList(connection, whereConditions, orderBy, null, commandTimeout);
    }

    public IEnumerable<T> GetList(IDbConnection connection, object whereConditions, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetList<T>(whereConditions, orderBy, transaction, commandTimeout);


    public int Count(object? whereConditions = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Count(connection, whereConditions, null, commandTimeout);
    }
    public int Count(IDbConnection connection, object? whereConditions = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.Count<T>(whereConditions, transaction, commandTimeout);

    public Task<T> GetAsync(Key key, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetAsync(connection, key, null, commandTimeout);
    }

    public Task<T> GetAsync(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetIdAsync<T>(key, transaction, commandTimeout);

    public Task<IEnumerable<T>> GetListAsync(object whereConditions, object? orderBy = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetListAsync(connection, whereConditions, orderBy, null, commandTimeout);
    }

    public Task<IEnumerable<T>> GetListAsync(IDbConnection connection, object whereConditions, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.GetListAsync<T>(whereConditions, orderBy, transaction, commandTimeout);

    public Task<int> CountAsync(object? whereConditions = null, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return CountAsync(connection, whereConditions, null, commandTimeout);
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
        string sql = InsertBuilder<T>.BuildInsertStatement();
        return connection.ExecuteScalar<Key>(sql, entity, transaction, commandTimeout);
    }

    public Task<Key> InsertAsync(T entity, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return InsertAsync(connection, entity, null, commandTimeout);
    }

    public Task<Key> InsertAsync(IDbConnection connection, T entity, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        string sql = InsertBuilder<T>.BuildInsertStatement();
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

    public int Update(object where, object set, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return Update(connection, where, set, null, commandTimeout);
    }

    public int Update(IDbConnection connection, object where, object set, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.Update<T>(where, set, transaction, commandTimeout);

    public Task<int> UpdateAsync(T entity, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return UpdateAsync(connection, entity, null, commandTimeout);
    }

    public Task<int> UpdateAsync(IDbConnection connection, T entity, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.UpdateAsync<T>(entity, transaction, commandTimeout);

    public Task<int> UpdateAsync(object where, object set, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return UpdateAsync(connection, where, set, null, commandTimeout);
    }

    public Task<int> UpdateAsync(IDbConnection connection, object where, object set, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.UpdateAsync<T>(where, set, transaction, commandTimeout);
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

    public Task<int> DeleteAsync(Key id, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return DeleteAsync(connection, id, null, commandTimeout);
    }

    public Task<int> DeleteAsync(IDbConnection connection, Key id, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        string sql = DeleteBuilder<T>.BuildDeleteIdStatement();
        var dynParameters = WhereBuilder<T>.GetIdParameters(id);
        return connection.ExecuteAsync(sql, dynParameters, transaction, commandTimeout);
    }

    public Task<int> DeleteAsync(object whereConditions, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return DeleteAsync(connection, whereConditions, null, commandTimeout);
    }

    public Task<int> DeleteAsync(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.DeleteWhereAsync<T>(whereConditions, transaction, commandTimeout);
    #endregion
}
