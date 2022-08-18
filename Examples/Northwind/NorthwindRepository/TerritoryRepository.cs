namespace NorthwindRepository;

public class TerritoryRepository : GenericRepository<Territory, string>, ITerritoryRepository
{
    #region Constructor
    public TerritoryRepository(string connectionStringName)
    {
        _connectionStringName = connectionStringName;
    }
    #endregion

    #region Connection
    private readonly string _connectionStringName;
    private string? ConnectionString => DapperDALSettings.ConnectionStrings(_connectionStringName);

    protected override IDbConnection GetConnection() => new SqlConnection(ConnectionString);
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
