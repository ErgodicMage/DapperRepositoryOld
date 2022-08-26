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
}
