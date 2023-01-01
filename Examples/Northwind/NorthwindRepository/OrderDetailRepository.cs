namespace NorthwindRepository;

public class OrderDetailRepository : NorthwindGenericRepository<OrderDetail, int>, IOrderDetailRepository
{
    #region Constructor
    public OrderDetailRepository(string connectionStringName) : base(connectionStringName)
    { }

    public OrderDetailRepository(DapperRepositorySettings settings, string connectionStringName) : base(settings, connectionStringName)
    { }
    #endregion
}
