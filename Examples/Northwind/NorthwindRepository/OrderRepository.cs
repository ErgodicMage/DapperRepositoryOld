namespace NorthwindRepository;

public class OrderRepository : NorthwindGenericRepository<Order, int>, IOrderRepository
{
    #region Constructor
    public OrderRepository(string connectionStringName) : base(connectionStringName)
    { }

    public OrderRepository(DapperRepositorySettings settings, string connectionStringName) : base(settings, connectionStringName)
    { }
    #endregion
}
