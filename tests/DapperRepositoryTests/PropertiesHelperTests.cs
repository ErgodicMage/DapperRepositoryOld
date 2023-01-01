using System.Reflection;

namespace DapperRepositoryTests;

public class PropertiesHelperTests
{
    private readonly ITestOutputHelper _output;
    private readonly DapperRepositorySettings _settings;

    public PropertiesHelperTests(ITestOutputHelper output)
    {
        TestingUtilities.TestNamespace = "DapperDALTests";
        TestingUtilities.LoadAppSettings();
        _settings = new DapperRepositorySettings(TestingUtilities.Configuration);

        _output = output;
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOGetAllProperties()
    {
        var poco = new POCO();
        PropertyInfo[] properties = PropertiesHelper.GetAllProperties<POCO>(poco);
        Assert.NotNull(properties);
        Assert.Equal(3, properties.Length);

        Assert.Equal("Id", properties[0].Name);
        Assert.Equal("Value1", properties[1].Name);
        Assert.Equal("Value2", properties[2].Name);

        Assert.Equal(typeof(int), properties[0].PropertyType);
        Assert.Equal(typeof(string), properties[1].PropertyType);
        Assert.Equal(typeof(string), properties[2].PropertyType);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOGetIdProperties()
    {
        var poco = new POCO();
        var properties = PropertiesHelper.GetIdProperties(poco);
        Assert.NotNull(properties);
        Assert.Single(properties);
        Assert.Equal("Id", properties[0].Name);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOGetScaffoldableProperties()
    {
        var properties = PropertiesHelper.GetScaffoldableProperties<POCO>();
        Assert.NotNull(properties);
        Assert.Equal(3, properties.Length);
        Assert.Equal("Id", properties[0].Name);
        Assert.Equal("Value1", properties[1].Name);
        Assert.Equal("Value2", properties[2].Name);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOGetUpdateableProperties()
    {
        var properties = PropertiesHelper.GetUpdateableProperties<POCO>();
        Assert.NotNull(properties);
        Assert.Equal(2, properties.Length);
        Assert.Equal("Value1", properties[0].Name);
        Assert.Equal("Value2", properties[1].Name);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOGetLargeProperties()
    {
        var properties = PropertiesHelper.GetLargeProperties<POCO>();
        Assert.NotNull(properties);
        Assert.False(properties.Any());
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void PersonGetAllProperties()
    {
        var person = new Person();
        PropertyInfo[] properties = PropertiesHelper.GetAllProperties<Person>(person);
        Assert.NotNull(properties);
        Assert.Equal(18, properties.Length);

        Assert.Equal("Id", properties[0].Name);
        Assert.Equal(typeof(int), properties[0].PropertyType);

        Assert.Equal("FirstName", properties[1].Name);
        Assert.Equal(typeof(string), properties[1].PropertyType);

        Assert.Equal("MiddleName", properties[2].Name);
        Assert.Equal(typeof(string), properties[2].PropertyType);

        Assert.Equal("LastName", properties[3].Name);
        Assert.Equal(typeof(string), properties[3].PropertyType);

        Assert.Equal("Gender", properties[4].Name);
        Assert.Equal(typeof(char?), properties[4].PropertyType);

        Assert.Equal("Age", properties[5].Name);
        Assert.Equal(typeof(int?), properties[5].PropertyType);

        Assert.Equal("Address", properties[6].Name);
        Assert.Equal(typeof(string), properties[6].PropertyType);

        Assert.Equal("City", properties[7].Name);
        Assert.Equal(typeof(string), properties[7].PropertyType);

        Assert.Equal("State", properties[8].Name);
        Assert.Equal(typeof(string), properties[8].PropertyType);

        Assert.Equal("ZipCode", properties[9].Name);
        Assert.Equal(typeof(string), properties[9].PropertyType);

        Assert.Equal("Email", properties[10].Name);
        Assert.Equal(typeof(string), properties[10].PropertyType);

        Assert.Equal("MotherId", properties[11].Name);
        Assert.Equal(typeof(int?), properties[11].PropertyType);

        Assert.Equal("FatherId", properties[12].Name);
        Assert.Equal(typeof(int?), properties[12].PropertyType);

        Assert.Equal("ChildrenId", properties[13].Name);
        Assert.Equal(typeof(IList<int>), properties[13].PropertyType);

        Assert.Equal("NotMapped", properties[14].Name);
        Assert.Equal(typeof(string), properties[14].PropertyType);

        Assert.Equal("IgnoreSelect", properties[15].Name);
        Assert.Equal(typeof(string), properties[15].PropertyType);

        Assert.Equal("IgnoreInsert", properties[16].Name);
        Assert.Equal(typeof(string), properties[16].PropertyType);

        Assert.Equal("IgnoreUpdate", properties[17].Name);
        Assert.Equal(typeof(string), properties[17].PropertyType);

    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void PersonGetScaffoldableProperties()
    {
        var properties = PropertiesHelper.GetScaffoldableProperties<Person>();
        Assert.NotNull(properties);
        Assert.Equal(17, properties.Length);
        Assert.Equal("Id", properties[0].Name);
        Assert.Equal("FirstName", properties[1].Name);
        Assert.Equal("MiddleName", properties[2].Name);
        Assert.Equal("LastName", properties[3].Name);
        Assert.Equal("Gender", properties[4].Name);
        Assert.Equal("Age", properties[5].Name);
        Assert.Equal("Address", properties[6].Name);
        Assert.Equal("City", properties[7].Name);
        Assert.Equal("State", properties[8].Name);
        Assert.Equal("ZipCode", properties[9].Name);
        Assert.Equal("Email", properties[10].Name);
        Assert.Equal("MotherId", properties[11].Name);
        Assert.Equal("FatherId", properties[12].Name);
        Assert.Equal("NotMapped", properties[13].Name);
        Assert.Equal("IgnoreSelect", properties[14].Name);
        Assert.Equal("IgnoreInsert", properties[15].Name);
        Assert.Equal("IgnoreUpdate", properties[16].Name);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void PersonGetLargeProperties()
    {
        var properties = PropertiesHelper.GetLargeProperties<Person>();
        Assert.NotNull(properties);
        Assert.True(properties.Any());
    }
}
