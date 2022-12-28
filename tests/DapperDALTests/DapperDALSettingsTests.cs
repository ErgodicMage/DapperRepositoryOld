using Microsoft.Extensions.Caching.Memory;

namespace DapperDALTests;

public class DapperDALSettingsTests
{
    private readonly ITestOutputHelper _output;

    public DapperDALSettingsTests(ITestOutputHelper output)
    {
        TestingUtilities.TestNamespace = "DapperDALTests";
        TestingUtilities.LoadAppSettings();

        _output = output;
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void AddConnectionStrings()
    {
        var connectionStrings = new Dictionary<string, string>();
        connectionStrings.Add("Default", "Server=DefaultServer;Database=Default;Integrated Security=True");
        connectionStrings.Add("Test", "Server=TestServer;Database=Test;Integrated Security=True");
        connectionStrings.Add("NorthwindDd", "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Development\\EM\\Databases\\Northwind\\northwnd.mdf;Integrated Security=True;Connect Timeout=30");

        DapperDALSettings settings = new DapperDALSettings(connectionStrings);

        // Check DapperDALSettings ConnectionStrings
        Assert.Equal("Server=DefaultServer;Database=Default;Integrated Security=True", settings.ConnectionString("Default"));
        Assert.Equal("Server=TestServer;Database=Test;Integrated Security=True", settings.ConnectionString("Test"));
        Assert.Equal("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Development\\EM\\Databases\\Northwind\\northwnd.mdf;Integrated Security=True;Connect Timeout=30", settings.ConnectionString("NorthwindDd"));
        Assert.Null(settings.ConnectionString("ABC"));

        // check DapperDal internal cache
        Assert.Equal("Server=DefaultServer;Database=Default;Integrated Security=True", DapperDALCache.Cache.Get("ConnectionString.Default"));
        Assert.Equal("Server=TestServer;Database=Test;Integrated Security=True", DapperDALCache.Cache.Get("ConnectionString.Test"));
        Assert.Equal("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Development\\EM\\Databases\\Northwind\\northwnd.mdf;Integrated Security=True;Connect Timeout=30", DapperDALCache.Cache.Get("ConnectionString.NorthwindDd"));
        Assert.Null(DapperDALCache.Cache.Get("ConnectionString.ABC"));
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void AddConnectionStringsIConfig()
    {
        var settings = new DapperDALSettings(TestingUtilities.Configuration);

        // Check DapperDALSettings ConnectionStrings
        Assert.Equal("Server=DefaultServer;Database=Default;Integrated Security=True", settings.ConnectionString("Default"));
        Assert.Equal("Server=TestServer;Database=Test;Integrated Security=True", settings.ConnectionString("Test"));
        Assert.Equal("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Development\\EM\\Databases\\Northwind\\northwnd.mdf;Integrated Security=True;Connect Timeout=30", settings.ConnectionString("NorthwindDd"));
        Assert.Null(settings.ConnectionString("ABC"));
    }
}