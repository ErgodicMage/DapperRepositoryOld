namespace NorthwindRepository;

public class SupplierRepository : GenericRepository<Supplier, int>, ISupplierRepository
{
    #region Constructor
    public SupplierRepository(string connectionStringName)
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
