using NorthwindModel;
using System.Text.RegularExpressions;

namespace NorthwindRepository;

public class NorthwindContext
{
    private readonly string _connectionStringName;
    #region Constructors
    public NorthwindContext(string connectionStringName)
    {
        _connectionStringName = connectionStringName;
    }
    #endregion

    #region Repositories
    ICategoriesRepository CategoryRepository() => new CategoryRepository(_connectionStringName);
    ICustomerRepository CustomerRepository() => new CustomerRepository(_connectionStringName);
    IEmployeeRepository EmployeeRepository() => new EmployeeRepository(_connectionStringName);
    IEmployeeTerritoriesRepository EmployeeTerritoriesRepository() => new EmployeeTerritoriesRepository(_connectionStringName);
    ITerritoryRepository TerritoryRepository() => new TerritoryRepository(_connectionStringName);
    IOrderRepository OrderRepository() => new OrderRepository(_connectionStringName);
    IOrderDetailRepository OrderDetailRepository() => new OrderDetailRepository(_connectionStringName);
    IProductRepository ProductRepository() => new ProductRepository(_connectionStringName);
    ISupplierRepository SupplierRepository() => new SupplierRepository(_connectionStringName);
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

        var territoryRepository = TerritoryRepository();
        employee.Territories = new List<Territory>();
        foreach (var employeeTerritory in employeeTerritories)
        {
            var territory = territoryRepository.Get(employeeTerritory.TerritoryId);
            if (territory is not null)
                employee.Territories.Add(territory);
        }

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

        var territoryRepository = TerritoryRepository();
        employee.Territories = new List<Territory>();
        foreach (var employeeTerritory in employeeTerritories)
        {
            var territory = await territoryRepository.GetAsync(employeeTerritory.TerritoryId);
            if (territory is not null)
                employee.Territories.Add(territory);
        }

        return employee;
    }
    #endregion

    #region Order Functions
    public Order GetCompleteOrder(int orderId)
    {
        var orderRepository = OrderRepository();
        var order = orderRepository.Get(orderId);
        if (order is null) 
            return null;

        order.OrderDetails = GetCompleteOrderDetail(orderId).ToList();
        return order;
    }

    public async Task<Order> GetCompleteOrderAsync(int orderId)
    {
        var orderRepository = OrderRepository();
        var order = await orderRepository.GetAsync(orderId);
        if (order is null)
            return null;

        var orderDetails = await GetCompleteOrderDetailAsync(orderId);
        order.OrderDetails = orderDetails.ToList();
        return order;
    }
    #endregion

    #region OrderDetail Functions
    public IEnumerable<OrderDetail> GetCompleteOrderDetail(int orderId)
    {
        var orderDetailRepository = OrderDetailRepository();
        var orderDetails = orderDetailRepository.GetList(new { OrderId = orderId });
        return orderDetails;
    }

    public async Task<IEnumerable<OrderDetail>> GetCompleteOrderDetailAsync(int orderId)
    {
        var orderDetailRepository = OrderDetailRepository();
        var orderDetails = await orderDetailRepository.GetListAsync(new { OrderId = orderId });
        return orderDetails;
    }
    #endregion

    #region Product Functions
    public Product GetCompleteProduct(int productId)
    {
        var productRepository = ProductRepository();
        var product = productRepository.Get(productId);
        if (product is null)
            return null;

        FillProductInfo(product);

        return product;
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
}
