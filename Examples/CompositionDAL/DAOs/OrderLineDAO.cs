namespace CompositionDAL.DAOs;

public class OrderLineDAO
{
    #region Constructor
    private readonly string _connectionStringName;

    public OrderLineDAO(string connectionStringName)
    {
        _connectionStringName = connectionStringName;
    }

    private string? ConnectionString => DapperDALSettings.ConnectionStrings(_connectionStringName);
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
        return connection.Insert<OrderLine>(orderLine);
    }

    public async Task<int> InsertAsync(OrderLine orderLine)
    {
        var connection = new SqlConnection(ConnectionString);
        return await connection.InsertAsync<OrderLine>(orderLine);
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
