namespace DapperDAL;

public interface IReadOnlyRepository<T, Key> where T : class
{
    T? Get(Key key, int? commandTimeout = null);
    T? Get(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null);
    T? Get(object whereConditions, int? commandTimeout = null);
    T? Get(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null);

    IEnumerable<T> GetWhere(object? whereConditions = null, object? orderBy = null, int? commandTimeout = null);
    IEnumerable<T> GetWhere(IDbConnection connection, object? whereConditions = null, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null);
    IEnumerable<T> GetWhere(string whereConditions, object? orderBy = null, int? commandTimeout = null);
    IEnumerable<T> GetWhere(IDbConnection connection, string whereConditions, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null);


    int Count(object? whereConditions = null, int? commandTimeout = null);
    int Count(IDbConnection connection, object? whereConditions = null, IDbTransaction? transaction = null, int? commandTimeout = null);

    Task<T?> GetAsync(Key key, int? commandTimeout = null);
    Task<T?> GetAsync(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null);
    Task<T?> GetAsync(object whereConditions, int? commandTimeout = null);
    Task<T?> GetAsync(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null);

    Task<IEnumerable<T>> GetWhereAsync(object? whereConditions = null, object? orderBy = null, int? commandTimeout = null);
    Task<IEnumerable<T>> GetWhereAsync(IDbConnection connection, object? whereConditions = null, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null);
    Task<IEnumerable<T>> GetWhereAsync(string whereConditions, object? orderBy = null, int? commandTimeout = null);
    Task<IEnumerable<T>> GetWhereAsync(IDbConnection connection, string whereConditions, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null);

    Task<int> CountAsync(object? whereConditions = null, int? commandTimeout = null);
    Task<int> CountAsync(IDbConnection connection, object? whereConditions = null, IDbTransaction? transaction = null, int? commandTimeout = null);

}
