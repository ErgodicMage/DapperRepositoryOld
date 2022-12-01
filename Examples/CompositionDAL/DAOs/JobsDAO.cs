namespace CompositionDAL.DAOs;

public class JobsDAO
{
    #region Constructor
    private readonly string _connectionStringName;
    public readonly DapperDALSettings _settings;

    public JobsDAO(string connectionStringName)
    {
        _connectionStringName = connectionStringName;
        _settings = DapperDALSettings.DefaultSettings;
    }

    public JobsDAO(DapperDALSettings settings, string connectionStringName)
    {
        _settings = settings;
        _connectionStringName = connectionStringName;
    }

    private string? ConnectionString => _settings.ConnectionString(_connectionStringName);
    #endregion

    #region Get
    public Job Get(int jobId)
    {
        using var connection = new SqlConnection(ConnectionString);
        return connection.GetId<Job>(jobId);
    }

    public async Task<Job> GetAsync(int jobId)
    {
        using var connection = new SqlConnection(ConnectionString);
        return await connection.GetIdAsync<Job>(jobId);
    }

    public Job GetByOrderId(int orderId)
    {
        using var connection = new SqlConnection(ConnectionString);
        return connection.Get<Job>(new { OrderId = orderId });
    }

    public async Task<Job> GetByOrderIdAsync(int orderId)
    {
        using var connection = new SqlConnection(ConnectionString);
        return await connection.GetAsync<Job>(new { OrderId = orderId });
    }

    public IEnumerable<Job> GetByStatus(string status)
    {
        using var connetion = new SqlConnection(ConnectionString);
        return connetion.GetList<Job>(new { Status = status });
    }

    public async Task<IEnumerable<Job>> GetByStatusAsync(string status)
    {
        using var connetion = new SqlConnection(ConnectionString);
        return await connetion.GetListAsync<Job>(new { Status = status });
    }

    public IEnumerable<Job> GetByWorkflow(string workflow)
    {
        using var connetion = new SqlConnection(ConnectionString);
        return connetion.GetList<Job>(new { Workflow = workflow }, new {Workflow = true, JobId = false});
    }

    public async Task<IEnumerable<Job>> GetByWorkflowAsync(string workflow)
    {
        using var connetion = new SqlConnection(ConnectionString);
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

    public IEnumerable<Job> GetWhere(string whereCondition, object? parameters = null, object? orderBy = null)
    {
        using var connection = new SqlConnection(ConnectionString);
        return connection.GetList<Job>(whereCondition, parameters, orderBy);
    }

    public async Task<IEnumerable<Job>> GetWhereAsync(string whereCondition, object? parameters = null, object? orderBy = null)
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

    public int UpdateStatusCompleted(int jobId, string status, DateTime? completedDate = null)
    {
        DateTime date = completedDate ?? DateTime.Now;
        using var connection = new SqlConnection(ConnectionString);
        return connection.Update<Job>(new { JobId = jobId, JobStatus = status, Completed = date });
    }

    public async Task<int> UpdateStatusCompletedAsync(int jobId, string status, DateTime? completedDate = null)
    {
        DateTime date = completedDate ?? DateTime.Now;
        using var connection = new SqlConnection(ConnectionString);
        return await connection.UpdateAsync<Job>(new { JobId = jobId, JobStatus = status, Completed = date });
    }

    public int UpdateComplete(int jobId)
    {
        using var connection = new SqlConnection(ConnectionString);
        return connection.Update<Job>(new { JobId = jobId, Status = Job.JobStatus.Complete, Completed = DateTime.Now});
    }

    public async Task<int> UpdateCompleteAsync(int jobId)
    {
        using var connection = new SqlConnection(ConnectionString);
        return await connection.UpdateAsync<Job>(new { JobId = jobId, Status = Job.JobStatus.Complete, Completed = DateTime.Now });
    }

    public int UpdateError(int jobId, string message)
    {
        using var connection = new SqlConnection(ConnectionString);
        return connection.Update<Job>(new { JobId = jobId, Status = Job.JobStatus.Error, Notes = message, Completed = DateTime.Now });
    }

    public async Task<int> UpdateErrorAsync(int jobId, string message)
    {
        using var connection = new SqlConnection(ConnectionString);
        return await connection.UpdateAsync<Job>(new { JobId = jobId, Status = Job.JobStatus.Error, Notes = message, Completed = DateTime.Now });
    }

    public int UpdateLastJobRun(int jobId, int lastJobRunId)
    {
        var connection = new SqlConnection(ConnectionString);
        return connection.Update<Job>(new { JobId = jobId, LastJobRunId = lastJobRunId });
    }

    public async Task<int> UpdateLastJobRunAsync(int jobId, int lastJobRunId)
    {
        var connection = new SqlConnection(ConnectionString);
        return await connection.UpdateAsync<Job>(new { JobId = jobId, LastJobRunId = lastJobRunId });
    }

    public int UpdatePageCount(int jobId, int pageCount)
    {
        using var connection = new SqlConnection(ConnectionString);
        return connection.Update<Job>(new { JobId = jobId, PageCount = pageCount });

    }

    public async Task<int> UpdatePageCountAsync(int jobId, int pageCount)
    {
        using var connection = new SqlConnection(ConnectionString);
        return await connection.UpdateAsync<Job>(new { JobId = jobId, PageCount = pageCount });

    }
    #endregion

    #region Delete
    public int Delete(int jobId)
    {
        using var connection = new SqlConnection(ConnectionString);
        return connection.DeleteId<Job>(jobId);
    }

    public async Task<int> DeleteAsync(int jobId)
    {
        using var connection = new SqlConnection(ConnectionString);
        return await connection.DeleteIdAsync<Job>(jobId);
    }
    #endregion
}
