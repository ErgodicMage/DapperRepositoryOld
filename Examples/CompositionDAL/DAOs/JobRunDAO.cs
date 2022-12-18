namespace CompositionDAL.DAOs;

public class JobRunDAO
{
    #region Constructor
    private readonly string _connectionStringName;
    public readonly DapperDALSettings _settings;

    public JobRunDAO(string connectionStringName)
    {
        _connectionStringName = connectionStringName;
        _settings = DapperDALSettings.DefaultSettings;
    }

    public JobRunDAO(DapperDALSettings settings, string connectionStringName)
    {
        _settings = settings;
        _connectionStringName = connectionStringName;
    }

    private string? ConnectionString => _settings.ConnectionString(_connectionStringName);
    #endregion

    #region Get
    public JobRun Get(int jobRunId)
    {
        var connection = new SqlConnection(ConnectionString);
        return connection.GetId<JobRun>(jobRunId);
    }

    public async Task<JobRun> GetAsync(int jobRunId)
    {
        var connection = new SqlConnection(ConnectionString);
        return await connection.GetIdAsync<JobRun>(jobRunId);
    }

    public IEnumerable<JobRun> GetByJobId(int jobId)
    {
        var connection = new SqlConnection(ConnectionString);
        return connection.GetWhere<JobRun>(new {JobId = jobId});
    }

    public async Task<IEnumerable<JobRun>> GetByJobIdAsync(int jobId)
    {
        var connection = new SqlConnection(ConnectionString);
        return await connection.GetWhereAsync<JobRun>(new { JobId = jobId });
    }

    public JobRun GetServerJobRun(string serverCode)
    {
        var where = new { ServerCode = serverCode, ServerJobStatus = "NEW" };
        string sql = SelectBuilder<JobRun>.BuildSelectStatement(where, new { Priority = true, JobDate = true }, 1);
        using var connection = new SqlConnection(ConnectionString);
        return connection.QueryFirstOrDefault<JobRun>(sql, where);
    }

    public async Task<JobRun> GetServerJobRunAsync(string serverCode)
    {
        var where = new { ServerCode = serverCode, ServerJobStatus = "NEW" };
        string sql = SelectBuilder<JobRun>.BuildSelectStatement(where, new { Priority = true, JobDate = true }, 1);
        using var connection = new SqlConnection(ConnectionString);
        return await connection.QueryFirstOrDefaultAsync<JobRun>(sql, where);
    }

    public IEnumerable<JobRun> GetJobRunsByStatusForServer(string sServerCode, IEnumerable<string> serverStatus)
    {
        var where = new { ServerCode = sServerCode, JobStatus = serverStatus };
        string sql = SelectBuilder<JobRun>.BuildSelectStatement(where, new { JobDate = false });
        using var connection = new SqlConnection(ConnectionString);
        return connection.Query<JobRun>(sql, where);
    }

    public async Task<IEnumerable<JobRun>> GetJobRunsByStatusForServerAsync(string sServerCode, IEnumerable<string> serverStatus)
    {
        var where = new { ServerCode = sServerCode, JobStatus = serverStatus };
        string sql = SelectBuilder<JobRun>.BuildSelectStatement(where, new { JobDate = false });
        using var connection = new SqlConnection(ConnectionString);
        return await connection.QueryAsync<JobRun>(sql, where);
    }

    public IEnumerable<JobRun> GetNewJobRunsForServer(string server)
    {
        var where = new { ServerCode = server, JobStatus = JobRun.JobRunStatus.New };
        using var connection = new SqlConnection(ConnectionString);
        return connection.GetWhere<JobRun>(where, new { JobDate = false });
    }

    public IEnumerable<JobRun> GetNewJobsByQueue(string queue)
    {
        var connection = new SqlConnection(ConnectionString);
        return connection.GetWhere<JobRun>(new { Status = JobRun.JobRunStatus.New, RequestQueue = queue });
    }

    public async Task<IEnumerable<JobRun>> GetNewJobsByQueueAsync(string queue)
    {
        var connection = new SqlConnection(ConnectionString);
        return await connection.GetWhereAsync<JobRun>(new { Status = JobRun.JobRunStatus.New, RequestQueue = queue });
    }
    #endregion

    #region Insert
    public int Insert(JobRun jobRun)
    {
        using var connection = new SqlConnection(ConnectionString);
        int id = connection.Insert<JobRun, int>(jobRun);
        jobRun.JobRunId = id;
        return id;
    }

    public async Task<int> InsertAsync(JobRun jobRun)
    {
        using var connection = new SqlConnection(ConnectionString);
        int id = await connection.InsertAsync<JobRun, int>(jobRun);
        jobRun.JobRunId = id;
        return id;
    }
    #endregion

    #region Update
    public int UpdateStatus(int jobRunId, string status)
    {
        using var connection = new SqlConnection(ConnectionString);
        return connection.Update<JobRun>(new { JobRunId = jobRunId, Status = status });
    }

    public async Task<int> UpdateStatusAsync(int jobRunId, string status)
    {
        using var connection = new SqlConnection(ConnectionString);
        return await connection.UpdateAsync<JobRun>(new { JobRunId = jobRunId, Status = status });
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
