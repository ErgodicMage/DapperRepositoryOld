namespace NorthwindRepository;

public class CustomerRepository : GenericRepository<Customer, string>, ICustomerRepository
{
    #region Constructor
    public CustomerRepository(string connectionStringName)
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
