namespace NorthwindRepository;

public class ShipperRepository : GenericRepository<Shipper, int>, IShipperRepository
{
    #region Constructor
    public ShipperRepository(string connectionStringName)
    {
        _connectionStringName = connectionStringName;
    }
    #endregion

    #region Connection
    private readonly string _connectionStringName;
    private string? ConnectionString => DapperDALSettings.ConnectionStrings(_connectionStringName);

    protected override IDbConnection GetConnection() => new SqlConnection(ConnectionString);
    #endregion
}
