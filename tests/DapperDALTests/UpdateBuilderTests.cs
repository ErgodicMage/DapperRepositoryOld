namespace DapperDALTests;

public class UpdateBuilderTests
{
    private readonly ITestOutputHelper _output;
    private readonly DapperDALSettings _settings;

    public UpdateBuilderTests(ITestOutputHelper output)
    {
        TestingUtilities.TestNamespace = "DapperDALTests";
        TestingUtilities.LoadAppSettings();
        _settings = new DapperDALSettings(TestingUtilities.Configuration);

        _output = output;
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOUpdateEntityStatement()
    {
        string expected = "UPDATE [POCO] SET [Id]=@Id, [Value1]=@Value1, [Value2]=@Value2 WHERE [Id]=@Id";
        string update = UpdateBuilder<POCO>.BuildUpdateEntityStatement();
        Assert.False(string.IsNullOrWhiteSpace(update));
        Assert.Equal(expected, update);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOUpdateIdStatement()
    {
        string expected = "UPDATE [POCO] SET [Value1]=@Value1, [Value2]=@Value2 WHERE [Id]=@Id";
        string update = UpdateBuilder<POCO>.BuildUpdateIdStatement(new { Value1 = "value1", Value2 = "value2"});
        Assert.False(string.IsNullOrWhiteSpace(update));
        Assert.Equal(expected, update);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOUpdateStatement()
    {
        string expected = "UPDATE [POCO] SET [Value2]=@Value2 WHERE [Value1]=@Value1";
        string update = UpdateBuilder<POCO>.BuildUpdateStatement(new { Value1 = "value1" }, new {Value2 = "value2"});
        Assert.False(string.IsNullOrWhiteSpace(update));
        Assert.Equal(expected, update);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void PersonUpdateEntityStatement()
    {
        string expected = "UPDATE [People] SET [First_Name]=@FirstName, [Middle_Name]=@MiddleName, [Last_Name]=@LastName, " +
            "[Gender]=@Gender, [Age]=@Age, [Address]=@Address, [City]=@City, [State]=@State, [ZipCode]=@ZipCode, " +
            "[EmailAddress]=@Email, [IgnoreSelect]=@IgnoreSelect, [IgnoreInsert]=@IgnoreInsert WHERE [Id]=@Id";
        string update = UpdateBuilder<Person>.BuildUpdateEntityStatement();
        Assert.False(string.IsNullOrWhiteSpace(update));
        Assert.Equal(expected, update);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void PersonUpdateStatementAddress()
    {
        string expected = "UPDATE [People] SET [First_Name]=@FirstName, [Last_Name]=@LastName, " +
            "[Address]=@Address, [City]=@City, [State]=@State, [ZipCode]=@ZipCode WHERE [Id]=@Id";
        var setAddress = new
        {
            FirstName = "Nick",
            LastName = "Zentner",
            Address = "123 New Street",
            City = "Ellensburg",
            State = "Washingtop",
            ZipCode = "98926",
        };
        string update = UpdateBuilder<Person>.BuildUpdateStatement(new { Id = 1 }, setAddress);
        Assert.False(string.IsNullOrWhiteSpace(update));
        Assert.Equal(expected, update);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void PersonUpdateStatementDonotInclude()
    {
        // This turns into an invalid update statement which in this test it should
        string expected = "UPDATE [People] SET  WHERE [Id]=@Id";
        var setNotIncluded = new
        {
            MotherId = 1,
            FatherId = 2,
            ChildrenId = new List<int>() { 3, 4 },
            NotEditable = "Not",
            IgnoreUpdate = "Ignore"
        };
        string update = UpdateBuilder<Person>.BuildUpdateStatement(new { Id = 1 }, setNotIncluded);
        Assert.False(string.IsNullOrWhiteSpace(update));
        Assert.Equal(expected, update);
    }
}
