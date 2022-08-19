namespace NorthwindRepository;

public class ProductRepository : GenericRepository<Product, int>, IProductRepository
{
    #region Constructor
    public ProductRepository(string connectionStringName)
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
