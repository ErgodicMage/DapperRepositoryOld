namespace DapperRepositoryTests;

public class SelectBuilderTests
{
    private readonly ITestOutputHelper _output;
    private readonly DapperRepositorySettings _settings;

    public SelectBuilderTests(ITestOutputHelper output)
    {
        TestingUtilities.TestNamespace = "DapperDALTests";
        TestingUtilities.LoadAppSettings();
        _settings = new DapperRepositorySettings(TestingUtilities.Configuration);

        _output = output;
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOBuildSelectString()
    {
        string expected = "SELECT [Id],[Value1],[Value2] FROM [POCO]";
        string sql = SelectBuilder<POCO>.BuildSelectStatement();
        Assert.Equal(expected, sql);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOBuildSqlSelectIdString()
    {
        string expected = "SELECT [Id],[Value1],[Value2] FROM [POCO] WHERE [Id]=@Id";
        string sql = SelectBuilder<POCO>.BuildSqlSelectIdString();
        Assert.Equal(expected, sql);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOBuildSelectStatementValue1()
    {
        string expected = "SELECT [Id],[Value1],[Value2] FROM [POCO] WHERE [Value1]=@Value1";
        string sql = SelectBuilder<POCO>.BuildSelectStatement(new {Value1 = "value1"});
        Assert.Equal(expected, sql);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOBuildSelectStatementBothValues()
    {
        string expected = "SELECT [Id],[Value1],[Value2] FROM [POCO] WHERE [Value1]=@Value1 AND [Value2]=@Value2";
        string sql = SelectBuilder<POCO>.BuildSelectStatement(new { Value1 = "value1", Value2 = "value2" });
        Assert.Equal(expected, sql);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOBuildSelectStatementOrderById()
    {
        string expected = "SELECT [Id],[Value1],[Value2] FROM [POCO] ORDER BY [Id]";
        string sql = SelectBuilder<POCO>.BuildSelectStatement(null, new { Id = true });
        Assert.Equal(expected, sql);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOBuildSelectStatementOrderByIdDesc()
    {
        string expected = "SELECT [Id],[Value1],[Value2] FROM [POCO] ORDER BY [Id] DESC";
        string sql = SelectBuilder<POCO>.BuildSelectStatement(null, new { Id = false });
        Assert.Equal(expected, sql);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOBuildSelectStatementOrderByIdValue1()
    {
        string expected = "SELECT [Id],[Value1],[Value2] FROM [POCO] ORDER BY [Id],[Value1] DESC";
        string sql = SelectBuilder<POCO>.BuildSelectStatement(null, new { Id = true, Value1 = false });
        Assert.Equal(expected, sql);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOBuildSelectStringDistinct()
    {
        string expected = "SELECT DISTINCT [Id],[Value1],[Value2] FROM [POCO]";
        string sql = SelectBuilder<POCO>.BuildSelectStatement(distinct: true);
        Assert.Equal(expected, sql);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOBuildSelectStringTop()
    {
        string expected = "SELECT TOP 5 [Id],[Value1],[Value2] FROM [POCO]";
        string sql = SelectBuilder<POCO>.BuildSelectStatement(maxRows: 5);
        Assert.Equal(expected, sql);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOBuildSelectStatementAllParameters()
    {
        string expected = "SELECT TOP 5 DISTINCT [Id],[Value1],[Value2] FROM [POCO] WHERE [Value1]=@Value1 ORDER BY [Id],[Value1] DESC";
        string sql = SelectBuilder<POCO>.BuildSelectStatement(new {Value1 = "value1"}, new { Id = true, Value1 = false }, 5, true);
        Assert.Equal(expected, sql);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void PersonBuildSelectString()
    {
        string expected =
            "SELECT [Id],[First_Name] as [FirstName],[Middle_Name] as [MiddleName],[Last_Name] as [LastName]," +
            "[Gender],[Age],[Address],[City],[State],[ZipCode],[EmailAddress] as [Email]," +
            "[MotherId],[FatherId],[IgnoreInsert],[IgnoreUpdate] FROM [People]";
        string sql = SelectBuilder<Person>.BuildSelectStatement();
        Assert.Equal(expected, sql);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void PersonBuildSqlSelectIdString()
    {
        string expected = 
            "SELECT [Id],[First_Name] as [FirstName],[Middle_Name] as [MiddleName],[Last_Name] as [LastName]," +
            "[Gender],[Age],[Address],[City],[State],[ZipCode],[EmailAddress] as [Email]," +
            "[MotherId],[FatherId],[IgnoreInsert],[IgnoreUpdate] " +
            "FROM [People] WHERE [Id]=@Id";
        string sql = SelectBuilder<Person>.BuildSqlSelectIdString();
        Assert.Equal(expected, sql);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void PersonBuildSelectStatementGender()
    {
        string expected =
            "SELECT [Id],[First_Name] as [FirstName],[Middle_Name] as [MiddleName],[Last_Name] as [LastName]," +
            "[Gender],[Age],[Address],[City],[State],[ZipCode],[EmailAddress] as [Email]," +
            "[MotherId],[FatherId],[IgnoreInsert],[IgnoreUpdate] " +
            "FROM [People] WHERE [Gender]=@Gender";
        string sql = SelectBuilder<Person>.BuildSelectStatement(new { Gender = "M" });
        Assert.Equal(expected, sql);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void PersonBuildSelectStatementMotherFather()
    {
        // Even though Mother and Father are [ReadOnly] you can still use them to query
        string expected =
            "SELECT [Id],[First_Name] as [FirstName],[Middle_Name] as [MiddleName],[Last_Name] as [LastName]," +
            "[Gender],[Age],[Address],[City],[State],[ZipCode],[EmailAddress] as [Email]," +
            "[MotherId],[FatherId],[IgnoreInsert],[IgnoreUpdate] " +
            "FROM [People] WHERE [Mother]=@Mother AND [Father]=@Father";
        string sql = SelectBuilder<Person>.BuildSelectStatement(new { Mother = 1, Father = 2 } );
        Assert.Equal(expected, sql);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void PersonBuildSelectStatementOrderByLastFirstMiddleName()
    {
        // Even though Mother and Father are [ReadOnly] you can still use them to query
        string expected =
            "SELECT [Id],[First_Name] as [FirstName],[Middle_Name] as [MiddleName],[Last_Name] as [LastName]," +
            "[Gender],[Age],[Address],[City],[State],[ZipCode],[EmailAddress] as [Email]," +
            "[MotherId],[FatherId],[IgnoreInsert],[IgnoreUpdate] " +
            "FROM [People] ORDER BY [LastName],[FirstName],[MiddleName]";
        string sql = SelectBuilder<Person>.BuildSelectStatement(null, new { LastName = true, FirstName = true, MiddleName = true});
        Assert.Equal(expected, sql);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void PersonBuildSelectStringDistinct()
    {
        string expected = "SELECT DISTINCT [Id],[First_Name] as [FirstName],[Middle_Name] as [MiddleName],[Last_Name] as [LastName]," +
            "[Gender],[Age],[Address],[City],[State],[ZipCode],[EmailAddress] as [Email]," +
            "[MotherId],[FatherId],[IgnoreInsert],[IgnoreUpdate] FROM [People]";
        string sql = SelectBuilder<Person>.BuildSelectStatement(distinct: true);
        Assert.Equal(expected, sql);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void PersonBuildSelectStringTop10City()
    {
        string expected = "SELECT TOP 10 [Id],[First_Name] as [FirstName],[Middle_Name] as [MiddleName],[Last_Name] as [LastName]," +
            "[Gender],[Age],[Address],[City],[State],[ZipCode],[EmailAddress] as [Email]," +
            "[MotherId],[FatherId],[IgnoreInsert],[IgnoreUpdate] FROM [People]";
        string sql = SelectBuilder<Person>.BuildSelectStatement(maxRows: 10);
        Assert.Equal(expected, sql);
    }
}
