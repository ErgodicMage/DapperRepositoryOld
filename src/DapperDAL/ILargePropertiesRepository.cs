namespace DapperDAL;

public interface ILargePropertiesRepository<T, Key> : IRepository<T, Key> where T : class
{
    new T Get(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null);
    new T Get(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null);
    new T Get(Key key, int? commandTimeout = null);
    new T Get(object whereConditions, int? commandTimeout = null);
    new IEnumerable<T> GetList(object? whereConditions = null, object? orderBy = null, int? commandTimeout = null);
    new IEnumerable<T> GetList(IDbConnection connection, object? whereConditions = null, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null);
    new IEnumerable<T> GetList(string whereConditions, object? orderBy = null, int? commandTimeout = null);
    new IEnumerable<T> GetList(IDbConnection connection,string whereConditions, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null);



    new Task<T> GetAsync(IDbConnection connection, Key key, IDbTransaction? transaction = null, int? commandTimeout = null);
    new Task<T> GetAsync(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null);
    new Task<T> GetAsync(Key key, int? commandTimeout = null);
    new Task<T> GetAsync(object whereConditions, int? commandTimeout = null);
    new Task<IEnumerable<T>> GetListAsync(object? whereConditions = null, object? orderBy = null, int? commandTimeout = null);
    new Task<IEnumerable<T>> GetListAsync(IDbConnection connection, object? whereConditions = null, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null);
    new Task<IEnumerable<T>> GetListAsync(string whereConditions, object? orderBy = null, int? commandTimeout = null);
    new Task<IEnumerable<T>> GetListAsync(IDbConnection connection, string whereConditions, object? orderBy = null, IDbTransaction? transaction = null, int? commandTimeout = null);

}