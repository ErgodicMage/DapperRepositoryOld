namespace NorthwindRepository;

public class EmployeeRepository : NorthwindGenericRepository<Employee, int>, IEmployeeRepository
{
    #region Constructor
    public EmployeeRepository(string connectionStringName) : base(connectionStringName)
    { }

    public EmployeeRepository(DapperRepositorySettings settings, string connectionStringName) : base(settings, connectionStringName)
    { }
    #endregion
}
