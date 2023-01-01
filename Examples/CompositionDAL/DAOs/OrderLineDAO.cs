namespace CompositionDAL.DAOs;

public class OrderLineDAO
{
    #region Constructor
    private readonly string _connectionStringName;
    public readonly DapperRepositorySettings _settings;

    public OrderLineDAO(string connectionStringName)
    {
        _connectionStringName = connectionStringName;
        _settings = DapperRepositorySettings.DefaultSettings;
    }

    public OrderLineDAO(DapperRepositorySettings settings, string connectionStringName)
    {
        _settings = settings;
        _connectionStringName = connectionStringName;
    }

    private string? ConnectionString => _settings.ConnectionString(_connectionStringName);
    #endregion

    #region Get
    public OrderLine Get(int jobId)
    {
        var connection = new SqlConnection(ConnectionString);
        return connection.Get<OrderLine>(new { JobId = jobId });
    }

    public async Task<OrderLine> GetAsync(int jobId)
    {
        var connection = new SqlConnection(ConnectionString);
        return await connection.GetAsync<OrderLine>(new { JobId = jobId });
    }
    #endregion

    #region Insert
    public int Insert(OrderLine orderLine)
    {
        var connection = new SqlConnection(ConnectionString);
        return connection.Insert<OrderLine, int>(orderLine);
    }

    public async Task<int> InsertAsync(OrderLine orderLine)
    {
        var connection = new SqlConnection(ConnectionString);
        return await connection.InsertAsync<OrderLine, int>(orderLine);
    }
    #endregion

    #region Delete
    public int Delete(int jobId)
    {
        using var connection = new SqlConnection(ConnectionString);
        return connection.DeleteId<OrderLine>(jobId);
    }

    public async Task<int> DeleteAsync(int jobId)
    {
        using var connection = new SqlConnection(ConnectionString);
        return await connection.DeleteIdAsync<OrderLine>(jobId);
    }
    #endregion
}
