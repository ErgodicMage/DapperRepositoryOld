namespace DapperRepositoryTests;

public class DeleteBuilderTests
{
    private readonly ITestOutputHelper _output;
    private readonly DapperRepositorySettings _settings;

    public DeleteBuilderTests(ITestOutputHelper output)
    {
        TestingUtilities.TestNamespace = "DapperDALTests";
        TestingUtilities.LoadAppSettings();
        _settings = new DapperRepositorySettings(TestingUtilities.Configuration);

        _output = output;
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCODeleteStatement()
    {
        string expected = "DELETE FROM [POCO] WHERE [Id]=@Id";
        string delete = DeleteBuilder<POCO>.BuildDeleteIdStatement();
        Assert.False(string.IsNullOrWhiteSpace(delete));
        Assert.Equal(expected, delete);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCODeleteWhereStatement()
    {
        string expected = "DELETE FROM [POCO] WHERE [Value1]=@Value1";
        string delete = DeleteBuilder<POCO>.BuildDeleteStatement(new { Value1 = "value1" });
        Assert.False(string.IsNullOrWhiteSpace(delete));
        Assert.Equal(expected, delete);
    }
}
