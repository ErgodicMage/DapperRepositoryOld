namespace NorthwindRepository;

public class CustomerRepository : NorthwindGenericRepository<Customer, string>, ICustomerRepository
{
    #region Constructor
    public CustomerRepository(string connectionStringName) : base(connectionStringName)
    { }

    public CustomerRepository(DapperDALSettings settings, string connectionStringName) : base(settings, connectionStringName)
    { }
    #endregion
}
