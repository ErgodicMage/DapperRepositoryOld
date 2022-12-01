namespace NorthwindRepository;

public class RegionRepository : NorthwindGenericRepository<Region, int>, IRegionRepository
{
    #region Constructor
    public RegionRepository(string connectionStringName) : base(connectionStringName)
    { }

    public RegionRepository(DapperDALSettings settings, string connectionStringName) : base(settings, connectionStringName)
    { }
    #endregion
}
