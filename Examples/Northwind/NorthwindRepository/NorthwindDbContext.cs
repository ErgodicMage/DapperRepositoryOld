namespace NorthwindRepository;

public class NorthwindDbContext
{
    #region Constructors
    public NorthwindDbContext(string connectionStringName)
    {
        _connectionStringName = connectionStringName;
    }

    private readonly string _connectionStringName;
    private string? ConnectionString => DapperDALSettings.ConnectionStrings(_connectionStringName);
    #endregion

    #region Repositories
    public ICategoriesRepository CategoryRepository() => new CategoryRepository(_connectionStringName);
    public ICustomerRepository CustomerRepository() => new CustomerRepository(_connectionStringName);
    public IEmployeeRepository EmployeeRepository() => new EmployeeRepository(_connectionStringName);
    public IEmployeeTerritoriesRepository EmployeeTerritoriesRepository() => new EmployeeTerritoriesRepository(_connectionStringName);
    public ITerritoryRepository TerritoryRepository() => new TerritoryRepository(_connectionStringName);
    public IOrderRepository OrderRepository() => new OrderRepository(_connectionStringName);
    public IOrderDetailRepository OrderDetailRepository() => new OrderDetailRepository(_connectionStringName);
    public IProductRepository ProductRepository() => new ProductRepository(_connectionStringName);
    public ISupplierRepository SupplierRepository() => new SupplierRepository(_connectionStringName);
    public IRegionRepository RegionRepository() => new RegionRepository(_connectionStringName);
    public IShipperRepository ShipperRepository() => new ShipperRepository(_connectionStringName);
    #endregion

    #region Category Functions
    public Category GetCategoryWithImage(int id)
    {
        var categoryRepository = CategoryRepository();
        var category = categoryRepository.Get(id);
        category.Image = categoryRepository.GetImage(id);
        return category;
    }

    public IEnumerable<Category> GetCategoryListWithImage(object whereConditions)
    {
        var categoryRepository = CategoryRepository();
        var categories = categoryRepository.GetList(whereConditions);
        foreach (var category in categories)
        {
            category.Image = categoryRepository.GetImage(category.CategoryId);
        }
        return categories;
    }

    public async Task<Category> GetCategoryWithImageAsync(int id)
    {
        var categoryRepository = CategoryRepository();
        var category = await categoryRepository.GetAsync(id);
        category.Image = await categoryRepository.GetImageAsync(id);
        return category;
    }

    public async Task<IEnumerable<Category>> GetCategoryListWithImageAsync(object whereConditions)
    {
        var categoryRepository = CategoryRepository();
        var categories = await categoryRepository.GetListAsync(whereConditions);
        foreach (var category in categories)
        {
            category.Image = await categoryRepository.GetImageAsync(category.CategoryId);
        }
        return categories;
    }
    #endregion

    #region Employee Territories Functions
    public Employee GetEmployeeWithTerritories(int employeeId)
    {
        var employeeRepository = EmployeeRepository();
        var employee = employeeRepository.Get(employeeId);
        if (employee is null)
            return null;

        var employeeTerritoriesRepository = EmployeeTerritoriesRepository();
        var employeeTerritories = employeeTerritoriesRepository.GetList(new { EmployeeId = employeeId });

        var territoryIds = employeeTerritories.Select(t => t.TerritoryId).ToList();

        var territoryRepository = TerritoryRepository();
        employee.Territories = territoryRepository.GetList( new {TerritoryId = territoryIds}).ToList();

        //var territoryRepository = TerritoryRepository();
        //employee.Territories = new List<Territory>();
        //foreach (var employeeTerritory in employeeTerritories)
        //{
        //    var territory = territoryRepository.Get(employeeTerritory.TerritoryId);
        //    if (territory is not null)
        //        employee.Territories.Add(territory);
        //}

        return employee;
    }

    public async Task<Employee> GetEmployeeWithTerritoriesAsync(int employeeId)
    {
        var employeeRepository = EmployeeRepository();
        var employee = await employeeRepository.GetAsync(employeeId);
        if (employee is null)
            return null;

        var employeeTerritoriesRepository = EmployeeTerritoriesRepository();
        var employeeTerritories = await employeeTerritoriesRepository.GetListAsync(new { EmployeeId = employeeId });

        var territoryIds = employeeTerritories.Select(t => t.TerritoryId).ToList();

        var territoryRepository = TerritoryRepository();
        var territories = await territoryRepository.GetListAsync(new { TerritoryId = territoryIds });
        employee.Territories = territories.ToList();

        return employee;
    }
    #endregion

    #region Order Functions
    public Order GetOrderWithDetailsExperiment(int orderId)
    {
        string sql = "select o.*, null as SEPERATOR, od.* from Orders as o inner join [Order Details] as od on o.OrderID = od.OrderID where o.orderID=@OrderID";

        using var connection = new SqlConnection(ConnectionString);

        Order order = null;
        _ = connection.Query<Order, OrderDetail, Order>(sql, (o, od) =>
            {
                order ??= o;
                order.OrderDetails ??= new List<OrderDetail>();
                order.OrderDetails.Add(od);
                return o;
            },
            splitOn: "SEPERATOR",
            param: new {OrderID = orderId }
            );

        return order;
    }

    public Order GetOrderWithDetails(int orderId)
    {
        var orderRepository = OrderRepository();
        var order = orderRepository.Get(orderId);
        if (order is null)
            return null;

        order.OrderDetails = GetOrderDetailsWithProduct(orderId).ToList();
        return order;
    }

    public Order GetOrderWithDetailsMutli(int orderId)
    {
        var whereCondition = new { OrderId = orderId };
        string orderSql = SelectBuilder<Order>.BuildSqlSelectIdString();
        string orderDetailSql = SelectBuilder<OrderDetail>.BuildSelectStatement(whereCondition);
        string sql = $"{orderSql} {orderDetailSql}";

        using var connection = new SqlConnection(ConnectionString);

        using var results = connection.QueryMultiple(sql, whereCondition);
        var order = results.Read<Order>().Single();

        if (order is null)
            return null;

        var orderDetail = results.Read<OrderDetail>();

        if (orderDetail.Any())
            order.OrderDetails = orderDetail.ToList();

        return order;
    }

    private string AddAliasToColumns(string sqlColumns, string alias)
    {
        string[] columns = sqlColumns.Split(',');
        StringBuilder sb = new StringBuilder();
        bool first = true;
        foreach (string column in columns)
        {
            if (!first)
                sb.Append(',');
            sb.Append(alias);
            sb.Append('.');
            sb.Append(column);
            first = false;
        }
        return sb.ToString();
    }

    private string MakeOnStatement(string firstColumn, string firstAlias, string secondColumn, string secondAlias )
    {
        return $" ON {firstAlias}.[{firstColumn}]={secondAlias}.[{secondColumn}]";
    }

    private string GetOrderInfoSql()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        string fixedColumns = AddAliasToColumns(BuilderCache<Order>.SelectColumns, "o");
        sb.Append(fixedColumns);
        sb.Append(", null as SEPERATOR, ");
        fixedColumns = AddAliasToColumns(BuilderCache<Customer>.SelectColumns, "c");
        sb.Append(fixedColumns);
        sb.Append(", null as SEPERATOR, ");
        fixedColumns = AddAliasToColumns(BuilderCache<Employee>.SelectColumns, "e");
        sb.Append(fixedColumns);
        sb.Append(" FROM ");
        sb.Append($"{BuilderCache<Order>.TableName} AS o");
        sb.Append(" LEFT JOIN ");
        sb.Append($"{BuilderCache<Customer>.TableName} AS c");
        sb.Append(MakeOnStatement("CustomerID", "o", "CustomerID", "c"));
        sb.Append(" LEFT JOIN ");
        sb.Append($"{BuilderCache<Employee>.TableName} AS e");
        sb.Append(MakeOnStatement("EmployeeID", "o", "EmployeeID", "e"));
        sb.Append($" WHERE {BuilderCache<Order>.WhereIdString}");
        return sb.ToString();
    }

    public Order GetOrderInformation(int orderId)
    {
        //string sql = "select o.*, null as SEPERATOR, c.*, null as SEPERATOR, e.* from Orders as o left join Customers as c on c.CustomerID=o.CustomerID left join Employees as e on e.EmployeeID=o.EmployeeID where o.OrderID=@OrderId";

        string sql = GetOrderInfoSql();

        using var connection = new SqlConnection(ConnectionString);
        var order = connection.Query<Order, Customer, Employee, Order>(sql,
            (o, c, e) =>
            {
                o.Customer = c;
                o.Employee = e;
                return o;
            },
            splitOn: "SEPERATOR",
            param: new {OrderId = orderId}
            )
            .First();

        if (order is null)
            return null;

        order.OrderDetails = GetOrderDetailsWithProduct(orderId).ToList();

        return order;
    }

    public async Task<Order> GetOrderWithDetailsAsync(int orderId)
    {
        var orderRepository = OrderRepository();
        var order = await orderRepository.GetAsync(orderId);
        if (order is null)
            return null;

        var orderDetails = await GetOrderDetailsWithProductAsync(orderId);
        order.OrderDetails = orderDetails.ToList();
        return order;
    }
    #endregion

    #region OrderDetail Functions
    public IEnumerable<OrderDetail> GetCompleteOrderDetailsOld(int orderId)
    {
        var orderDetailRepository = OrderDetailRepository();
        var orderDetails = orderDetailRepository.GetList(new { OrderId = orderId });

        foreach (var orderDetail in orderDetails)
            orderDetail.Product = GetCompleteProduct(orderDetail.ProductId);

        return orderDetails;
    }

    public IEnumerable<OrderDetail> GetOrderDetailsWithProduct(int orderId)
    {
        string sql = "select od.*, null as SEPERATOR, prod.* from [Order Details] as od left join Products as prod on od.ProductID = prod.ProductID where od.OrderID=10248";

        using var connection = new SqlConnection(ConnectionString);

        var orderDetails = connection.Query<OrderDetail, Product, OrderDetail>(sql, (o, p) => 
        { 
            o.Product = p;
            return o; 
        },
        splitOn: "SEPERATOR",
        param: new {OrderID = orderId}
        );

        return orderDetails;
    }

    public async Task<IEnumerable<OrderDetail>> GetOrderDetailsWithProductAsync(int orderId)
    {
        string sql = "select od.*, null as SEPERATOR, prod.* from [Order Details] as od left join Products as prod on od.ProductID = prod.ProductID where od.OrderID=10248";

        using var connection = new SqlConnection(ConnectionString);

        var orderDetails = await connection.QueryAsync<OrderDetail, Product, OrderDetail>(sql, (o, p) =>
        {
            o.Product = p;
            return o;
        },
        splitOn: "SEPERATOR",
        param: new { OrderID = orderId }
        );

        return orderDetails;
    }
    #endregion

    #region Product Functions
    public Product GetCompleteProductOld(int productId)
    {
        var productRepository = ProductRepository();
        var product = productRepository.Get(productId);
        if (product is null)
            return null;

        FillProductInfo(product);

        return product;
    }

    public Product GetCompleteProduct(int productId)
    {
        string sql = "select p.*, null as SEPERATOR, s.*, null as SEPERATOR, c.* from Products as p left join Categories as c on p.CategoryID=c.CategoryID left join Suppliers as s on p.SupplierID=s.SupplierID where p.ProductID=@ProductId";

        using var connection = new SqlConnection(ConnectionString);

        var products = connection.Query<Product, Supplier, Category, Product>(sql, (p, s, c) =>
            {
                p.Supplier = s;
                p.Category = c;
                return p;
            },
            splitOn: "SEPERATOR",
            param: new {ProductID = productId}
            );

        return products?.FirstOrDefault()!;
    }

    public void FillProductInfo(Product product)
    {
        if (product is null) throw new ArgumentNullException(nameof(product));

        if (product.CategoryId.HasValue)
        {
            var categoryRepository = CategoryRepository();
            product.Category = categoryRepository.Get(product.CategoryId.Value);
        }

        if (product.SupplierId.HasValue)
        {
            var supplierRepository = SupplierRepository();
            product.Supplier = supplierRepository.Get(product.SupplierId.Value);
        }
    }

    public async Task<Product> GetCompleteProductAsync(int productId)
    {
        var productRepository = ProductRepository();
        var product = await productRepository.GetAsync(productId);
        if (product is null)
            return null;

        await FillProductInfoAsync(product);

        return product;
    }

    public async Task FillProductInfoAsync(Product product)
    {
        if (product is null) throw new ArgumentNullException(nameof(product));

        if (product.CategoryId.HasValue)
        {
            var categoryRepository = CategoryRepository();
            product.Category = await categoryRepository.GetAsync(product.CategoryId.Value);
        }

        if (product.SupplierId.HasValue)
        {
            var supplierRepository = SupplierRepository();
            product.Supplier = await supplierRepository.GetAsync(product.SupplierId.Value);
        }
    }
    #endregion

    #region InsertOrder
    public bool InsertOrder(Order order)
    {
        if (order is null || order.OrderDetails is null || order.OrderDetails.Count == 0)
            return false;

        bool results = false;

        using var connection = new SqlConnection(ConnectionString);
        using var uow = new UnitOfWork(connection);

        try
        {
            uow.Begin();
            var orderRepository = OrderRepository();
            order.OrderId = orderRepository.Insert(connection, order, uow.Transaction);

            var orderDetailRepository = OrderDetailRepository();
            foreach (var orderDetail in order.OrderDetails)
            {
                orderDetail.OrderId = order.OrderId;
                orderDetailRepository.Insert(connection, orderDetail, uow.Transaction);
            }

            uow.Commit();
        }
        catch
        {
            uow.Rollback();
        }

        return results;
    }

    public async Task<bool> InsertOrderAsync(Order order)
    {
        if (order is null || order.OrderDetails is null || order.OrderDetails.Count == 0)
            return false;

        bool results = false;

        using var connection = new SqlConnection(ConnectionString);
        using var uow = new UnitOfWork(connection);

        try
        {
            await uow.BeginAsync();
            var orderRepository = OrderRepository();
            order.OrderId = await orderRepository.InsertAsync(connection, order, uow.Transaction);

            var orderDetailRepository = OrderDetailRepository();
            foreach (var orderDetail in order.OrderDetails)
            {
                orderDetail.OrderId = order.OrderId;
                await orderDetailRepository.InsertAsync(connection, orderDetail, uow.Transaction);
            }

            await uow.CommitAsync();
        }
        catch
        {
            await uow.RollbackAsync();
        }

        return results;
    }

    public bool InsertOrderDoWorkMethod(Order order)
    {
        if (order is null || order.OrderDetails is null || order.OrderDetails.Count == 0)
            return false;

        bool results = false;

        using var connection = new SqlConnection(ConnectionString);
        using var uow = new UnitOfWork(connection);

        results = uow.DoWork<Order>(DoWorkInsertOrder, order);

        return results;
    }

    public async Task<bool> InsertOrderDoWorkMethodAsync(Order order)
    {
        if (order is null || order.OrderDetails is null || order.OrderDetails.Count == 0)
            return false;

        bool results = false;

        using var connection = new SqlConnection(ConnectionString);
        using var uow = new UnitOfWork(connection);

        results = await uow.DoWorkAsync<Order>(DoWorkInsertOrderAsync, order);

        return results;
    }

    public bool DoWorkInsertOrder(IUnitOfWork uow, Order order)
    {
        var orderRepository = OrderRepository();
        order.OrderId = orderRepository.Insert(uow.Connection, order, uow.Transaction);

        var orderDetailRepository = OrderDetailRepository();
        foreach (var orderDetail in order.OrderDetails)
        {
            orderDetail.OrderId = order.OrderId;
            orderDetailRepository.Insert(uow.Connection, orderDetail, uow.Transaction);
        }

        return true;
    }

    public async Task<bool> DoWorkInsertOrderAsync(IUnitOfWork uow, Order order)
    {
        var orderRepository = OrderRepository();
        order.OrderId = await orderRepository.InsertAsync(uow.Connection, order, uow.Transaction);

        var orderDetailRepository = OrderDetailRepository();
        foreach (var orderDetail in order.OrderDetails)
        {
            orderDetail.OrderId = order.OrderId;
            await orderDetailRepository.InsertAsync(uow.Connection, orderDetail, uow.Transaction);
        }

        return true;
    }
    #endregion
}
