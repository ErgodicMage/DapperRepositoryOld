using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace NorthwindTests;

public class NorthwindRepositoryTests
{
	private readonly ITestOutputHelper _output;
    private readonly string _connectionStringName = "NorthwindDd";
    private NorthwindDbContext _context;

    public NorthwindRepositoryTests(ITestOutputHelper output)
	{
		_output = output;

        TestingUtilities.TestNamespace = "Northwind";
        TestingUtilities.LoadAppSettings();

        DapperDALSettings.Initialize(TestingUtilities.Configuration);

        _context = new NorthwindDbContext(_connectionStringName);
    }

    [Fact]
    [Trait("Category", TestCategories.IntegrationTest)]
    public void GetOrderWithOrderDetails()
    {
        var order = _context.GetOrderWithDetails(10248);
        Assert.NotNull(order);
        Assert.Equal(10248, order.OrderId);
        Assert.NotNull(order.OrderDetails);
        Assert.Equal(3, order.OrderDetails.Count);

        foreach (var orderDetail in order.OrderDetails)
        {
            Assert.NotNull(orderDetail.Product);
            Assert.Equal(orderDetail.ProductId, orderDetail.Product.ProductId);
        }
    }

    [Fact]
    [Trait("Category", TestCategories.IntegrationTest)]
    public async Task GetOrderWithOrderDetailsAsync()
    {
        var order = await _context.GetOrderWithDetailsAsync(10248);
        Assert.NotNull(order);
        Assert.Equal(10248, order.OrderId);
        Assert.NotNull(order.OrderDetails);
        Assert.Equal(3, order.OrderDetails.Count);

        foreach (var orderDetail in order.OrderDetails)
        {
            Assert.NotNull(orderDetail.Product);
            Assert.Equal(orderDetail.ProductId, orderDetail.Product.ProductId);
        }
    }

    [Fact]
    [Trait("Category", TestCategories.IntegrationTest)]
    public void GetOrderInfo()
    {
        var order = _context.GetOrderInformation(10248);
        Assert.NotNull(order);
        Assert.Equal(10248, order.OrderId);
        Assert.NotNull(order.OrderDetails);
        Assert.Equal(3, order.OrderDetails.Count);
        Assert.NotNull(order.Customer);
        Assert.Equal(order.CustomerId, order.Customer.CustomerId);
        Assert.NotNull(order.Employee);
        Assert.Equal(order.EmployeeId, order.Employee.EmployeeId);

        foreach (var orderDetail in order.OrderDetails)
        {
            Assert.NotNull(orderDetail.Product);
            Assert.Equal(orderDetail.ProductId, orderDetail.Product.ProductId);
        }
    }

    [Fact]
    [Trait("Category", TestCategories.IntegrationTest)]
    public void InsertOrder()
    {
        var order = CreatOrder();

        bool results = _context.InsertOrder(order);
        Assert.True(results);
        Assert.NotEqual(0, order.OrderId);

        var checkOrder = _context.GetOrderWithDetails(order.OrderId);
        Assert.NotNull(checkOrder);
        Assert.Equal(order.OrderId, checkOrder.OrderId);
        Assert.NotNull(checkOrder.OrderDetails);
        Assert.Equal(order.OrderDetails.Count, checkOrder.OrderDetails.Count);
        Assert.Equal(order.CustomerId, checkOrder.CustomerId);
        Assert.Equal(order.EmployeeId, checkOrder.EmployeeId);
        Assert.Equal(order.OrderDetails[0].OrderId, checkOrder.OrderDetails[0].OrderId);
        Assert.Equal(order.OrderDetails[1].OrderId, checkOrder.OrderDetails[1].OrderId);
        Assert.Equal(order.OrderDetails[0].ProductId, checkOrder.OrderDetails[0].ProductId);
        Assert.Equal(order.OrderDetails[1].ProductId, checkOrder.OrderDetails[1].ProductId);
    }

    [Fact]
    [Trait("Category", TestCategories.IntegrationTest)]
    public async Task InsertOrderAsync()
    {
        var order = CreatOrder();

        bool results = await _context.InsertOrderAsync(order);
        Assert.True(results);
        Assert.NotEqual(0, order.OrderId);

        var checkOrder = await _context.GetOrderWithDetailsAsync(order.OrderId);
        Assert.NotNull(checkOrder);
        Assert.Equal(order.OrderId, checkOrder.OrderId);
        Assert.NotNull(checkOrder.OrderDetails);
        Assert.Equal(order.OrderDetails.Count, checkOrder.OrderDetails.Count);
        Assert.Equal(order.CustomerId, checkOrder.CustomerId);
        Assert.Equal(order.EmployeeId, checkOrder.EmployeeId);
        Assert.Equal(order.OrderDetails[0].OrderId, checkOrder.OrderDetails[0].OrderId);
        Assert.Equal(order.OrderDetails[1].OrderId, checkOrder.OrderDetails[1].OrderId);
        Assert.Equal(order.OrderDetails[0].ProductId, checkOrder.OrderDetails[0].ProductId);
        Assert.Equal(order.OrderDetails[1].ProductId, checkOrder.OrderDetails[1].ProductId);
    }

    [Fact]
    [Trait("Category", TestCategories.IntegrationTest)]
    public void InsertOrderDoWorkMethod()
    {
        var order = CreatOrder();

        bool results = _context.InsertOrderDoWorkMethod(order);
        Assert.True(results);
        Assert.NotEqual(0, order.OrderId);

        var checkOrder = _context.GetOrderWithDetails(order.OrderId);
        Assert.NotNull(checkOrder);
        Assert.Equal(order.OrderId, checkOrder.OrderId);
        Assert.NotNull(checkOrder.OrderDetails);
        Assert.Equal(order.OrderDetails.Count, checkOrder.OrderDetails.Count);
        Assert.Equal(order.CustomerId, checkOrder.CustomerId);
        Assert.Equal(order.EmployeeId, checkOrder.EmployeeId);
        Assert.Equal(order.OrderDetails[0].OrderId, checkOrder.OrderDetails[0].OrderId);
        Assert.Equal(order.OrderDetails[1].OrderId, checkOrder.OrderDetails[1].OrderId);
        Assert.Equal(order.OrderDetails[0].ProductId, checkOrder.OrderDetails[0].ProductId);
        Assert.Equal(order.OrderDetails[1].ProductId, checkOrder.OrderDetails[1].ProductId);
    }

    [Fact]
    [Trait("Category", TestCategories.IntegrationTest)]
    public async Task InsertOrderDoWorkMethodAsync()
    {
        var order = CreatOrder();

        bool results = await _context.InsertOrderDoWorkMethodAsync(order);
        Assert.True(results);
        Assert.NotEqual(0, order.OrderId);

        var checkOrder = await _context.GetOrderWithDetailsAsync(order.OrderId);
        Assert.NotNull(checkOrder);
        Assert.Equal(order.OrderId, checkOrder.OrderId);
        Assert.NotNull(checkOrder.OrderDetails);
        Assert.Equal(order.OrderDetails.Count, checkOrder.OrderDetails.Count);
        Assert.Equal(order.CustomerId, checkOrder.CustomerId);
        Assert.Equal(order.EmployeeId, checkOrder.EmployeeId);
        Assert.Equal(order.OrderDetails[0].OrderId, checkOrder.OrderDetails[0].OrderId);
        Assert.Equal(order.OrderDetails[1].OrderId, checkOrder.OrderDetails[1].OrderId);
        Assert.Equal(order.OrderDetails[0].ProductId, checkOrder.OrderDetails[0].ProductId);
        Assert.Equal(order.OrderDetails[1].ProductId, checkOrder.OrderDetails[1].ProductId);
    }

    protected Order CreatOrder()
    {
        var order = new Order()
        {
            CustomerId = "ALFKI",
            EmployeeId = 1,
            OrderDate = DateTime.Today,
            RequiredDate = DateTime.Today.AddDays(7),
            ShipVia = 1,
            Freight = 10.15,
            ShipName = "Ergodic Name",
            ShipAddress = "My address",
            ShipCity = "My City",
            ShipCountry = "US",
            ShipPostalCode = "90124"
        };

        order.OrderDetails = new List<OrderDetail>();

        var orderDetail = new OrderDetail()
        {
            ProductId = 1,
            UnitPrice = 12.00M,
            Quantity = 10,
            Discount = 0
        };
        order.OrderDetails.Add(orderDetail);

        orderDetail = new OrderDetail()
        {
            ProductId = 2,
            UnitPrice = 8.00M,
            Quantity = 5,
            Discount = 0
        };
        order.OrderDetails.Add(orderDetail);

        return order;
    }
}
