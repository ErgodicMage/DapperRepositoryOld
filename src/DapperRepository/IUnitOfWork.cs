using System.Data.Common;

namespace DapperRepository;

public interface IUnitOfWork : IDisposable
{
    DbConnection Connection { get; }
    DbTransaction Transaction { get; }

    void Begin();
    void Commit();
    void Rollback();

    Task BeginAsync();
    Task CommitAsync();
    Task RollbackAsync();

    bool DoWork(Func<IUnitOfWork, bool> work);
    bool DoWork<P>(Func<IUnitOfWork, P, bool> work, P parameter);
    bool DoWork<P1, P2>(Func<IUnitOfWork, P1, P2, bool> work, P1 p1, P2 p2);
    bool DoWork<P1, P2, P3>(Func<IUnitOfWork, P1, P2, P3, bool> work, P1 p1, P2 p2, P3 p3);

    Task<bool> DoWorkAsync(Func<IUnitOfWork, Task<bool>> work);
    Task<bool> DoWorkAsync<P>(Func<IUnitOfWork, P, Task<bool>> work, P parameter);
    Task<bool> DoWorkAsync<P1, P2>(Func<IUnitOfWork, P1, P2, Task<bool>> work, P1 p1, P2 p2);
    Task<bool> DoWorkAsync<P1, P2, P3>(Func<IUnitOfWork, P1, P2, P3, Task<bool>> work, P1 p1, P2 p2, P3 p3);
}
