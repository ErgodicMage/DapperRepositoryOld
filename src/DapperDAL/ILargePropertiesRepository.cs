namespace DapperDAL;

public interface ILargePropertiesRepository<T, Key> : IRepository<T, Key> where T : class
{
    new T? Get(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null);
    new T? Get(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null);
    new T? Get(Key key, int? commandTimeout = null);
    new T? Get(object whereConditions, int? commandTimeout = null);
    new IEnumerable<T> GetWhere(object? whereConditions = null, object? orderBy = null, int? commandTimeout = null);
    new IEnumerable<T> GetWhere(IDbConnection connection, object? whereConditions = null, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null);
    new IEnumerable<T> GetWhere(string whereConditions, object? orderBy = null, int? commandTimeout = null);
    new IEnumerable<T> GetWhere(IDbConnection connection,string whereConditions, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null);



    new Task<T?> GetAsync(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null);
    new Task<T?> GetAsync(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null);
    new Task<T?> GetAsync(Key key, int? commandTimeout = null);
    new Task<T?> GetAsync(object whereConditions, int? commandTimeout = null);
    new Task<IEnumerable<T>> GetWhereAsync(object? whereConditions = null, object? orderBy = null, int? commandTimeout = null);
    new Task<IEnumerable<T>> GetWhereAsync(IDbConnection connection, object? whereConditions = null, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null);
    new Task<IEnumerable<T>> GetWhereAsync(string whereConditions, object? orderBy = null, int? commandTimeout = null);
    new Task<IEnumerable<T>> GetWhereAsync(IDbConnection connection, string whereConditions, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null);

}