namespace NorthwindRepository;

public class ProductRepository : NorthwindGenericRepository<Product, int>, IProductRepository
{
    #region Constructor
    public ProductRepository(string connectionStringName) : base(connectionStringName)
    { }

    public ProductRepository(DapperDALSettings settings, string connectionStringName) : base(settings, connectionStringName)
    { }
    #endregion
}
