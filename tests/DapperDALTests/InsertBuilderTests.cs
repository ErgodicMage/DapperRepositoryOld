namespace DapperDALTests;

public class InsertBuilderTests
{
    private readonly ITestOutputHelper _output;
    private readonly DapperDALSettings _settings;


    public InsertBuilderTests(ITestOutputHelper output)
    {
        TestingUtilities.TestNamespace = "DapperDALTests";
        TestingUtilities.LoadAppSettings();
        _settings = new DapperDALSettings(TestingUtilities.Configuration);

        _output = output;
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOInsertStatement()
    {
        string expected = "INSERT INTO [POCO]([Value1], [Value2])  VALUES (@Value1, @Value2)";
        string insert = InsertBuilder<POCO>.BuildInsertStatement();
        Assert.Equal(expected, insert);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void PersonInsertStatement()
    {
        string expected = "INSERT INTO [People]([First_Name], [Middle_Name], [Last_Name], [Gender], [Age], [Address], " +
            "[City], [State], [ZipCode], [EmailAddress], [IgnoreSelect], [IgnoreUpdate]) OUTPUT INSERTED.[Id] " +
            "VALUES (@FirstName, @MiddleName, @LastName, @Gender, @Age, @Address, @City, @State, @ZipCode, @Email, " +
            "@IgnoreSelect, @IgnoreUpdate)";
        string insert = InsertBuilder<Person>.BuildInsertStatement();
        Assert.Equal(expected, insert);
    }
}
