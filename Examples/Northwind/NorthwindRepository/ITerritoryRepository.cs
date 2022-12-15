namespace NorthwindRepository;

public interface ITerritoryRepository : IRepository<Territory, string>
{
    #region Get
    Territory GetByRegionId(int regionId, int? commandTimeout = null);
    Territory GetByRegionId(IDbConnection connection, int regionId, IDbTransaction? transaction = null, int? commandTimeout = null);

    Task<Territory> GetByRegionIdAsync(int regionId, int? commandTimeout = null);
    Task<Territory> GetByRegionIdAsync(IDbConnection connection, int regionId, IDbTransaction? transaction = null, int? commandTimeout = null);
    #endregion
}
