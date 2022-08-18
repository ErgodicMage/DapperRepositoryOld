namespace NorthwindRepository;

public class EmployeeTerritoriesRepository : GenericRepository<EmployeeTerritories, int>, IEmployeeTerritoriesRepository
{
    #region Constructor
    public EmployeeTerritoriesRepository(string connectionStringName)
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
