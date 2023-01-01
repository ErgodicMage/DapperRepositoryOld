namespace NorthwindRepository;

public class TerritoryRepository : NorthwindGenericRepository<Territory, string>, ITerritoryRepository
{
    #region Constructor
    public TerritoryRepository(string connectionStringName) : base(connectionStringName)
    { }

    public TerritoryRepository(DapperRepositorySettings settings, string connectionStringName) : base(settings, connectionStringName)
    { }
    #endregion

    #region Get
    public Territory GetByRegionId(int regionId, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetByRegionId(connection, regionId, null, commandTimeout);
    }

    public Territory GetByRegionId(IDbConnection connection, int regionId, IDbTransaction? transaction = null, int? commandTimeout = null)
        => connection.Get<Territory>(new { RegionId = regionId }, transaction, commandTimeout);

    public Task<Territory> GetByRegionIdAsync(int regionId, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetByRegionIdAsync(connection, regionId, null, commandTimeout);
    }

    public Task<Territory> GetByRegionIdAsync(IDbConnection connection, int regionId, IDbTransaction? transaction = null, int? commandTimeout = null)
    => connection.GetAsync<Territory>(new { RegionId = regionId }, transaction, commandTimeout);

    #endregion
}
