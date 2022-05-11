namespace CompositionDAL.DAOs;

public class JobsDAO
{
    #region Constructor
    private readonly string _connectionStringName;

    public JobsDAO(string connectionStringName)
    {
        _connectionStringName = connectionStringName;
    }

    private string ConnectionString => DapperDALSettings.ConnectionStrings(_connectionStringName);
    #endregion

    #region Get
    public Job Get(int jobId)
    {
        var connection = new SqlConnection(ConnectionString);
        return connection.GetId<Job>(jobId);
    }

    public async Task<Job> GetAsync(int jobId)
    {
        var connection = new SqlConnection(ConnectionString);
        return await connection.GetIdAsync<Job>(jobId);
    }

    public Job GetByOrderId(int orderId)
    {
        var connection = new SqlConnection(ConnectionString);
        return connection.Get<Job>(new { OrderId = orderId });
    }

    public async Task<Job> GetByOrderIdAsync(int orderId)
    {
        var connection = new SqlConnection(ConnectionString);
        return await connection.GetAsync<Job>(new { OrderId = orderId });
    }

    public IEnumerable<Job> GetByStatus(string status)
    {
        var connetion = new SqlConnection(ConnectionString);
        return connetion.GetList<Job>(new { Status = status });
    }

    public async Task<IEnumerable<Job>> GetByStatusAsync(string status)
    {
        var connetion = new SqlConnection(ConnectionString);
        return await connetion.GetListAsync<Job>(new { Status = status });
    }

    public IEnumerable<Job> GetByWorkflow(string workflow)
    {
        var connetion = new SqlConnection(ConnectionString);
        return connetion.GetList<Job>(new { Workflow = workflow }, new {Workflow = true, JobId = false});
    }

    public async Task<IEnumerable<Job>> GetByWorkflowAsync(string workflow)
    {
        var connetion = new SqlConnection(ConnectionString);
        return await connetion.GetListAsync<Job>(new { Workflow = workflow }, new { Workflow = true, JobId = false });
    }

    public IEnumerable<Job> GetByStatusQueues(string status, IList<string> queues)
    {
        string whereCondition = "JOBSTATUS=@status AND REQQUEUE IN @queues";
        using var connection = new SqlConnection(ConnectionString);
        return connection.GetList<Job>(whereCondition, new { status = status, queues = queues }, new { RequestQueue = false });
    }

    public async Task<IEnumerable<Job>> GetByStatusQueuesAsync(string status, IList<string> queues)
    {
        string whereCondition = "JOBSTATUS=@status AND REQQUEUE IN @queues";
        using var connection = new SqlConnection(ConnectionString);
        return await connection.GetListAsync<Job>(whereCondition, new { status = status, queues = queues }, new { RequestQueue = false });
    }

    public IEnumerable<Job> GetWhere(string whereCondition, object parameters = null, object orderBy = null)
    {
        using var connection = new SqlConnection(ConnectionString);
        return connection.GetList<Job>(whereCondition, parameters, orderBy);
    }

    public async Task<IEnumerable<Job>> GetWhereAsync(string whereCondition, object parameters = null, object orderBy = null)
    {
        using var connection = new SqlConnection(ConnectionString);
        return await connection.GetListAsync<Job>(whereCondition, parameters, orderBy);
    }
    #endregion

    #region Insert
    public int Insert(Job job)
    {
        using var connection = new SqlConnection(ConnectionString);
        int id = connection.Insert<Job>(job);
        job.JobId = id;
        return id;
    }

    public async Task<int> InsertAsync(Job job)
    {
        using var connection = new SqlConnection(ConnectionString);
        int id = await connection.InsertAsync<Job>(job);
        job.JobId = id;
        return id;
    }
    #endregion

    #region Updates
    public int UpdateStatus(int jobId, string status)
    {
        using var connection = new SqlConnection(ConnectionString);
        return connection.Update<Job>(new { JobId = jobId, Status = status });
    }

    public async Task<int> UpdateStatusAsync(int jobId, string status)
    {
        using var connection = new SqlConnection(ConnectionString);
        return await connection.UpdateAsync<Job>(new { JobId = jobId, Status = status });
    }

    public int UpdateStatusOrderSystem(int jobId, string status, DateTime updateDate)
    {
        using var connection = new SqlConnection(ConnectionString);
        return connection.Update<Job>(new { JobId = jobId, JobStatus = status, UpdatedOrderSystem = updateDate });
    }

    public async Task<int> UpdateStatusOrderSystemAsync(int jobId, string status, DateTime updateDate)
    {
        using var connection = new SqlConnection(ConnectionString);
        return await connection.UpdateAsync<Job>(new { JobId = jobId, JobStatus = status, UpdatedOrderSystem = updateDate });
    }
    #endregion
}
