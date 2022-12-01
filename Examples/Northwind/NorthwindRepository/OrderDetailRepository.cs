namespace NorthwindRepository;

public class OrderDetailRepository : NorthwindGenericRepository<OrderDetail, int>, IOrderDetailRepository
{
    #region Constructor
    public OrderDetailRepository(string connectionStringName) : base(connectionStringName)
    { }

    public OrderDetailRepository(DapperDALSettings settings, string connectionStringName) : base(settings, connectionStringName)
    { }
    #endregion
}
