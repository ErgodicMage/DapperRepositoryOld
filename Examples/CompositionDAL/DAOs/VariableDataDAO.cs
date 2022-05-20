namespace CompositionDAL.DAOs;

public class VariableDataDAO
{
    #region Constructor
    private readonly string _connectionStringName;

    public VariableDataDAO(string connectionStringName)
    {
        _connectionStringName = connectionStringName;
    }

    private string? ConnectionString => DapperDALSettings.ConnectionStrings(_connectionStringName);
    #endregion

    #region Get
    public VariableData Get(int jobId)
    {
        using var connection = new SqlConnection(ConnectionString);
        return connection.GetLargeProperties<VariableData>(new { JobId = jobId });
    }

    public async Task<VariableData> GetAsync(int jobId)
    {
        using var connection = new SqlConnection(ConnectionString);
        return await connection.GetLargePropertiesAsync<VariableData>(new { JobId = jobId });
    }
    #endregion

    #region Insert
    public int Insert(VariableData varData)
    {
        using var connection = new SqlConnection(ConnectionString);
        return connection.Insert<VariableData>(varData);
    }

    public async Task<int> InsertAsync(VariableData varData)
    {
        using var connection = new SqlConnection(ConnectionString);
        return await connection.InsertAsync<VariableData>(varData);
    }
    #endregion

    #region Update
    public int UpdateBillofMaterial(int jobId, string billofMaterial)
    {
        using var connection = new SqlConnection(ConnectionString);
        return connection.Update<VariableData>(new { JobId = jobId }, new { BillofMaterial = billofMaterial });
    }

    public async Task<int> UpdateBillofMaterialAsync(int jobId, string billofMaterial)
    {
        using var connection = new SqlConnection(ConnectionString);
        return await connection.UpdateAsync<VariableData>(new { JobId = jobId }, new { BillofMaterial = billofMaterial });
    }
    #endregion

    #region Delete
    public int Delete(int jobId)
    {
        using var connection = new SqlConnection(ConnectionString);
        return connection.DeleteId<VariableData>(jobId);
    }

    public async Task<int> DeleteAsync(int jobId)
    {
        using var connection = new SqlConnection(ConnectionString);
        return await connection.DeleteIdAsync<VariableData>(jobId);
    }
    #endregion

}
