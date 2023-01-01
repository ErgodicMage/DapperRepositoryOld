namespace DapperRepositoryTests;

public class ResolversTests
{
    private readonly ITestOutputHelper _output;
    private readonly DapperRepositorySettings _settings;

    public ResolversTests(ITestOutputHelper output)
    {
        TestingUtilities.TestNamespace = "DapperDALTests";
        TestingUtilities.LoadAppSettings();
        _settings = new DapperRepositorySettings(TestingUtilities.Configuration);

        _output = output;
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOResolveTableName()
    {
        string tableName = Resolvers.ResolveTableName<POCO>();
        Assert.Equal("[POCO]", tableName);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOResolveColumnName()
    {
        var properties = typeof(POCO).GetProperties();
        Assert.Equal("[Id]", Resolvers.ResolveColumnName(properties[0]));
        Assert.Equal("[Value1]", Resolvers.ResolveColumnName(properties[1]));
        Assert.Equal("[Value2]", Resolvers.ResolveColumnName(properties[2]));
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void PersonResolveTableName()
    {
        string tableName = Resolvers.ResolveTableName<Person>();
        Assert.Equal("[People]", tableName);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void PersonResolveColumnName()
    {
        var properties = BuilderCache<Person>.Properties;
        Assert.Equal("[Id]", Resolvers.ResolveColumnName(properties[0]));
        Assert.Equal("[First_Name]", Resolvers.ResolveColumnName(properties[1]));
        Assert.Equal("[Middle_Name]", Resolvers.ResolveColumnName(properties[2]));
        Assert.Equal("[Last_Name]", Resolvers.ResolveColumnName(properties[3]));
        Assert.Equal("[Gender]", Resolvers.ResolveColumnName(properties[4]));
        Assert.Equal("[Age]", Resolvers.ResolveColumnName(properties[5]));
        Assert.Equal("[Address]", Resolvers.ResolveColumnName(properties[6]));
        Assert.Equal("[City]", Resolvers.ResolveColumnName(properties[7]));
        Assert.Equal("[State]", Resolvers.ResolveColumnName(properties[8]));
        Assert.Equal("[ZipCode]", Resolvers.ResolveColumnName(properties[9]));
        Assert.Equal("[EmailAddress]", Resolvers.ResolveColumnName(properties[10]));
        Assert.Equal("[MotherId]", Resolvers.ResolveColumnName(properties[11]));
        Assert.Equal("[FatherId]", Resolvers.ResolveColumnName(properties[12]));
        Assert.Equal("[ChildrenId]", Resolvers.ResolveColumnName(properties[13]));
        Assert.Equal("[NotMapped]", Resolvers.ResolveColumnName(properties[14]));
        Assert.Equal("[IgnoreSelect]", Resolvers.ResolveColumnName(properties[15]));
        Assert.Equal("[IgnoreInsert]", Resolvers.ResolveColumnName(properties[16]));
        Assert.Equal("[IgnoreUpdate]", Resolvers.ResolveColumnName(properties[17]));
    }

}
