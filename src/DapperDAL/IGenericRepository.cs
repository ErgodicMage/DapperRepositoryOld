namespace DapperDAL;

public interface IGenericRepository<T, Key> where T : class
{
    #region Get
    T Get(Key key, int? commandTimeout = null);
    T Get(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null);

    IEnumerable<T> GetList(object whereConditions, object? orderBy = null, int? commandTimeout = null);
    IEnumerable<T> GetList(IDbConnection connection, object whereConditions, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null);


    int Count(object? whereConditions = null, int? commandTimeout = null);
    int Count(IDbConnection connection, object? whereConditions = null, IDbTransaction? transaction = null, int? commandTimeout = null);

    Task<T> GetAsync(Key key, int? commandTimeout = null);
    Task<T> GetAsync(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null);

    Task<IEnumerable<T>> GetListAsync(object whereConditions, object? orderBy = null, int? commandTimeout = null);
    Task<IEnumerable<T>> GetListAsync(IDbConnection connection, object whereConditions, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null);

    Task<int> CountAsync(object? whereConditions = null, int? commandTimeout = null);
    Task<int> CountAsync(IDbConnection connection, object? whereConditions = null, IDbTransaction? transaction = null, int? commandTimeout = null);
    #endregion

    #region Insert
    Key Insert(T entity, int? commandTimeout = null);
    Key Insert(IDbConnection connection, T entity, IDbTransaction? transaction = null, int? commandTimeout = null);

    Task<Key> InsertAsync(T entity, int? commandTimeout = null);
    Task<Key> InsertAsync(IDbConnection connection, T entity, IDbTransaction? transaction = null, int? commandTimeout = null);
    #endregion

    #region Update
    int Update(T entity, int? commandTimeout = null);
    int Update(IDbConnection connection, T entity, IDbTransaction? transaction = null, int? commandTimeout = null);

    int Update(object where, object set, int? commandTimeout = null);
    int Update(IDbConnection connection, object where, object set, IDbTransaction? transaction = null, int? commandTimeout = null);


    Task<int> UpdateAsync(T entity, int? commandTimeout = null);
    Task<int> UpdateAsync(IDbConnection connection, T entity, IDbTransaction? transaction = null, int? commandTimeout = null);

    Task<int> UpdateAsync(object where, object set, int? commandTimeout = null);
    Task<int> UpdateAsync(IDbConnection connection, object where, object set, IDbTransaction? transaction = null, int? commandTimeout = null);
    #endregion

    #region Delete
    int Delete(Key id, int? commandTimeout = null);
    int Delete(IDbConnection connection, Key id, IDbTransaction? transaction = null, int? commandTimeout = null);

    int Delete(object whereConditions, int? commandTimeout = null);
    int Delete(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null);

    Task<int> DeleteAsync(Key id, int? commandTimeout = null);
    Task<int> DeleteAsync(IDbConnection connection, Key id, IDbTransaction? transaction = null, int? commandTimeout = null);

    Task<int> DeleteAsync(object whereConditions, int? commandTimeout = null);
    Task<int> DeleteAsync(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null);
    #endregion
}
