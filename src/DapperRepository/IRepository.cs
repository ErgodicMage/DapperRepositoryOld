﻿using System.Threading;

namespace DapperRepository;

public interface IRepository<T, Key> : IReadRepository<T, Key> where T : class
{
    #region Insert
    Key Insert(T entity, int? commandTimeout = null);
    Key Insert(IDbConnection connection, T entity, IDbTransaction? transaction = null, int? commandTimeout = null);

    Task<Key> InsertAsync(T entity, int? commandTimeout = null, CancellationToken cancellationToken = default);
    Task<Key> InsertAsync(IDbConnection connection, T entity, IDbTransaction? transaction = null, int? commandTimeout = null, 
        CancellationToken cancellationToken = default);
    #endregion

    #region Update
    int Update(T entity, int? commandTimeout = null);
    int Update(IDbConnection connection, T entity, IDbTransaction? transaction = null, int? commandTimeout = null);

    int Update(Key key, object set, int? commandTimeout = null);
    int Update(IDbConnection connection, Key key, object set, IDbTransaction? transaction = null, int? commandTimeout = null);

    int UpdateWhere(object where, object set, int? commandTimeout = null);
    int UpdateWhere(IDbConnection connection, object where, object set, IDbTransaction? transaction = null, int? commandTimeout = null);


    Task<int> UpdateAsync(T entity, int? commandTimeout = null, CancellationToken cancellationToken = default);
    Task<int> UpdateAsync(IDbConnection connection, T entity, IDbTransaction? transaction = null, int? commandTimeout = null, 
        CancellationToken cancellationToken = default);

    Task<int> UpdateAsync(Key key, object set, int? commandTimeout = null, CancellationToken cancellationToken = default);
    Task<int> UpdateAsync(IDbConnection connection, Key key, object set, IDbTransaction? transaction = null, int? commandTimeout = null, 
        CancellationToken cancellationToken = default);

    Task<int> UpdateWhereAsync(object where, object set, int? commandTimeout = null, CancellationToken cancellationToken = default);
    Task<int> UpdateWhereAsync(IDbConnection connection, object where, object set, IDbTransaction? transaction = null, 
        int? commandTimeout = null, CancellationToken cancellationToken = default);
    #endregion

    #region Delete
    int Delete(Key id, int? commandTimeout = null);
    int Delete(IDbConnection connection, Key id, IDbTransaction? transaction = null, int? commandTimeout = null);

    int Delete(object whereConditions, int? commandTimeout = null);
    int Delete(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, int? commandTimeout = null);

    Task<int> DeleteAsync(Key id, int? commandTimeout = null, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(IDbConnection connection, Key id, IDbTransaction? transaction = null, int? commandTimeout = null, 
        CancellationToken cancellationToken = default);

    Task<int> DeleteAsync(object whereConditions, int? commandTimeout = null, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(IDbConnection connection, object whereConditions, IDbTransaction? transaction = null, 
        int? commandTimeout = null, CancellationToken cancellationToken = default);
    #endregion
}
