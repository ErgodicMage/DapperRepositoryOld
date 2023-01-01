namespace NorthwindRepository;

public class RegionRepository : NorthwindGenericRepository<Region, int>, IRegionRepository
{
    #region Constructor
    public RegionRepository(string connectionStringName) : base(connectionStringName)
    { }

    public RegionRepository(DapperRepositorySettings settings, string connectionStringName) : base(settings, connectionStringName)
    { }
    #endregion
}
