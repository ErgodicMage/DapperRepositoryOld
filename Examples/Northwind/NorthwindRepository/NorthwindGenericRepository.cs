namespace NorthwindRepository;

public class NorthwindGenericRepository<T, Key> : GenericRepository<T, Key> where T : class
{
    #region Constructors
    private readonly DapperDALSettings _settings;
    private readonly string _connectionStringName;

    public NorthwindGenericRepository(string connectionStringName)
    {
        _settings = DapperDALSettings.DefaultSettings;
        _connectionStringName = connectionStringName;
    }

    public NorthwindGenericRepository(DapperDALSettings settings, string connectionStringName)
    {
        _settings = settings;
        _connectionStringName = connectionStringName;
    }
    #endregion

    #region Connection
    private string? ConnectionString => _settings.ConnectionString(_connectionStringName);

    protected override IDbConnection GetConnection() => new SqlConnection(ConnectionString);
    #endregion
}
