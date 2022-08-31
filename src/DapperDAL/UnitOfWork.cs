using System.ComponentModel;
using System.Data.Common;

namespace DapperDAL;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbConnection? _connection;
    private DbTransaction? _transaction;

    public UnitOfWork(DbConnection connection)
        => _connection = connection;

    public UnitOfWork(IDbConnection connection)
        => _connection = connection as DbConnection;

    public DbConnection Connection { get => _connection; }

    public DbTransaction Transaction { get => _transaction; }


    public void Begin() => _transaction = _connection.BeginTransaction();

     public void Commit() => _transaction.Commit();

    public void Rollback() => _transaction.Rollback();


    public async Task BeginAsync() => _transaction = await _connection.BeginTransactionAsync();


    public async Task CommitAsync() => await _transaction.CommitAsync();

    public async Task RollbackAsync() => await _transaction.RollbackAsync();

    #region IDisposable
    public void Dispose()
    {
        if (_transaction != null)
            _transaction.Dispose();

        _transaction = null;
    }
    #endregion

    #region DoWork
    public bool DoWork(Func<IUnitOfWork, bool> work)
    {
        bool result = false;

        try
        {
            Begin();
            result = work(this);
            if (result)
                Commit();
            else
                Rollback();
        }
        catch
        {
            Rollback();
            throw;
        }

        return result;
    }

    public bool DoWork<P>(Func<IUnitOfWork, P, bool> work, P parameter)
    {
        bool result = false;

        try
        {
            Begin();
            result = work(this, parameter);
            if (result)
                Commit();
            else
                Rollback();
        }
        catch
        {
            Rollback();
            throw;
        }

        return result;
    }

    public bool DoWork<P1, P2>(Func<IUnitOfWork, P1, P2, bool> work, P1 p1, P2 p2)
    {
        bool result = false;

        try
        {
            Begin();
            result = work(this, p1, p2);
            if (result)
                Commit();
            else
                Rollback();
        }
        catch
        {
            Rollback();
            throw;
        }

        return result;
    }

    public bool DoWork<P1, P2, P3>(Func<IUnitOfWork, P1, P2, P3, bool> work, P1 p1, P2 p2, P3 p3)
    {
        bool result = false;

        try
        {
            Begin();
            result = work(this, p1, p2, p3);
            if (result)
                Commit();
            else
                Rollback();
        }
        catch
        {
            Rollback();
            throw;
        }

        return result;
    }

    public async Task<bool> DoWorkAsync(Func<IUnitOfWork, Task<bool>> work)
    {
        bool result = false;

        try
        {
            await BeginAsync();
            result = await work(this);
            if (result)
                await CommitAsync();
            else
                await RollbackAsync();
        }
        catch
        {
            await RollbackAsync();
            throw;
        }

        return result;
    }

    public async Task<bool> DoWorkAsync<P>(Func<IUnitOfWork, P, Task<bool>> work, P parameter)
    {
        bool result = false;

        try
        {
            await BeginAsync();
            result = await work(this, parameter);
            if (result)
                await CommitAsync();
            else
                await RollbackAsync();
        }
        catch
        {
            await RollbackAsync();
            throw;
        }

        return result;
    }

    public async Task<bool> DoWorkAsync<P1, P2>(Func<IUnitOfWork, P1, P2, Task<bool>> work, P1 p1, P2 p2)
    {
        bool result = false;

        try
        {
            await BeginAsync();
            result = await work(this, p1, p2);
            if (result)
                await CommitAsync();
            else
                await RollbackAsync();
        }
        catch
        {
            Rollback();
            throw;
        }

        return result;
    }

    public async Task<bool> DoWorkAsync<P1, P2, P3>(Func<IUnitOfWork, P1, P2, P3, Task<bool>> work, P1 p1, P2 p2, P3 p3)
    {
        bool result = false;

        try
        {
            await BeginAsync();
            result = await work(this, p1, p2, p3);
            if (result)
                await CommitAsync();
            else
                await RollbackAsync();
        }
        catch
        {
            await RollbackAsync();
            throw;
        }

        return result;
    }
    #endregion
}
