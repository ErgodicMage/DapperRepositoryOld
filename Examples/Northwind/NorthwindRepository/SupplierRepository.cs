namespace NorthwindRepository;

public class SupplierRepository : NorthwindGenericRepository<Supplier, int>, ISupplierRepository
{
    #region Constructor
    public SupplierRepository(string connectionStringName) : base(connectionStringName)
    { }

    public SupplierRepository(DapperRepositorySettings settings, string connectionStringName) : base(settings, connectionStringName)
    { }
    #endregion
}
