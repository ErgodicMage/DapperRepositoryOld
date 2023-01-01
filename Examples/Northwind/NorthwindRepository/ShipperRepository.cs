namespace NorthwindRepository;

public class ShipperRepository : NorthwindGenericRepository<Shipper, int>, IShipperRepository
{
    #region Constructor
    public ShipperRepository(string connectionStringName) : base(connectionStringName)
    { }

    public ShipperRepository(DapperRepositorySettings settings, string connectionStringName) : base(settings, connectionStringName)
    { }
    #endregion
}
