namespace DapperDAL;

public interface IReadOnlyRepository<T, Key> where T : class
{
    T Get(Key key, int? commandTimeout = null);
    T Get(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null);
    T Get(object whereConditions, int? commandTimeout = null);
    T Get(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null);

    IEnumerable<T> GetList(object? whereConditions = null, object? orderBy = null, int? commandTimeout = null);
    IEnumerable<T> GetList(IDbConnection connection, object? whereConditions = null, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null);
    IEnumerable<T> GetList(string whereConditions, object? orderBy = null, int? commandTimeout = null);
    IEnumerable<T> GetList(IDbConnection connection, string whereConditions, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null);


    int Count(object? whereConditions = null, int? commandTimeout = null);
    int Count(IDbConnection connection, object? whereConditions = null, IDbTransaction? transaction = null, int? commandTimeout = null);

    Task<T> GetAsync(Key key, int? commandTimeout = null);
    Task<T> GetAsync(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null);
    Task<T> GetAsync(object whereConditions, int? commandTimeout = null);
    Task<T> GetAsync(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null);

    Task<IEnumerable<T>> GetListAsync(object? whereConditions = null, object? orderBy = null, int? commandTimeout = null);
    Task<IEnumerable<T>> GetListAsync(IDbConnection connection, object? whereConditions = null, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null);
    Task<IEnumerable<T>> GetListAsync(string whereConditions, object? orderBy = null, int? commandTimeout = null);
    Task<IEnumerable<T>> GetListAsync(IDbConnection connection, string whereConditions, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null);

    Task<int> CountAsync(object? whereConditions = null, int? commandTimeout = null);
    Task<int> CountAsync(IDbConnection connection, object? whereConditions = null, IDbTransaction? transaction = null, int? commandTimeout = null);

}
