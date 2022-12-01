namespace CompositionDAL.DAOs;

public class CompositionJobDAO
{
    #region Constructor
    private readonly string _connectionStringName;
    public readonly DapperDALSettings _settings;

    public CompositionJobDAO(string connectionStringName)
    {
        _connectionStringName = connectionStringName;
        _settings = DapperDALSettings.DefaultSettings;
    }

    public CompositionJobDAO(DapperDALSettings settings, string connectionStringName)
    { 
        _settings = settings;
        _connectionStringName = connectionStringName;
    }

    private string? ConnectionString => _settings.ConnectionString(_connectionStringName);
    #endregion

    #region Get
    public CompositionJob Get(int jobId)
    {
        using var connection = new SqlConnection(ConnectionString);
        return connection.GetLargeProperties<CompositionJob>(new { JobId = jobId });
    }

    public async Task<CompositionJob> GetAsync(int jobId)
    {
        using var connection = new SqlConnection(ConnectionString);
        return await connection.GetLargePropertiesAsync<CompositionJob>(new { JobId = jobId });
    }
    #endregion

    #region Insert
    public int Insert(CompositionJob compJob)
    {
        using var connection = new SqlConnection(ConnectionString);
        return connection.Insert<CompositionJob>(compJob);
    }

    public async Task<int> InsertAsync(CompositionJob compJob)
    {
        using var connection = new SqlConnection(ConnectionString);
        return await connection.InsertAsync<CompositionJob>(compJob);
    }
    #endregion

    #region Delete
    public int Delete(int jobId)
    {
        using var connection = new SqlConnection(ConnectionString);
        return connection.DeleteId<CompositionJob>(jobId);
    }

    public async Task<int> DeleteAsync(int jobId)
    {
        using var connection = new SqlConnection(ConnectionString);
        return await connection.DeleteIdAsync<CompositionJob>(jobId);
    }
    #endregion

}
