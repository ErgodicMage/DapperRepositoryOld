namespace NorthwindRepository;

public class EmployeeTerritoriesRepository : NorthwindGenericRepository<EmployeeTerritories, int>, IEmployeeTerritoriesRepository
{
    #region Constructor
    public EmployeeTerritoriesRepository(string connectionStringName) : base(connectionStringName)
    { }

    public EmployeeTerritoriesRepository(DapperRepositorySettings settings, string connectionStringName) : base(settings, connectionStringName)
    { }
    #endregion
}
