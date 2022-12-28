using System.Text;

namespace DapperDALTests;

public class WhereBuilderTests
{
    private readonly ITestOutputHelper _output;
    private readonly DapperDALSettings _settings;

    public WhereBuilderTests(ITestOutputHelper output)
    {
        TestingUtilities.TestNamespace = "DapperDALTests";
        TestingUtilities.LoadAppSettings();
        _settings = new DapperDALSettings(TestingUtilities.Configuration);

        _output = output;
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOBuildIdWhereString()
    {
        string expected = "[Id]=@Id";
        string where = WhereBuilder<POCO>.BuildIdWhereString();
        Assert.Equal(expected, where);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOGetIdParameters()
    {
        DynamicParameters where = WhereBuilder<POCO>.GetIdParameters(1);
        Assert.Equal(1, where.Get<int>("Id"));
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCOBuildWhereString()
    {
        StringBuilder sb = new();
        WhereBuilder<POCO>.BuildWhereString(sb, null);
        Assert.Equal(string.Empty, sb.ToString());

        sb = new();
        string expected = "[Value1]=@Value1";
        WhereBuilder<POCO>.BuildWhereString(sb, new { Value1 = "value1"}); ;
        Assert.Equal(expected, sb.ToString());

        sb = new();
        expected = "[Value1]=@Value1 AND [Value2]=@Value2";
        WhereBuilder<POCO>.BuildWhereString(sb, new {Value1 = "value1", Value2 = "value2"}); ;
        Assert.Equal(expected, sb.ToString());
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void PersonBuildIdWhereString()
    {
        string expected = "[Id]=@Id";
        string where = WhereBuilder<Person>.BuildIdWhereString();
        Assert.Equal(expected, where);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void PersonGetIdParameters()
    {
        DynamicParameters where = WhereBuilder<Person>.GetIdParameters(1);
        Assert.Equal(1, where.Get<int>("Id"));
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void PersonBuildWhereString()
    {
        StringBuilder sb = new();
        WhereBuilder<Person>.BuildWhereString(sb, null);
        Assert.Equal(string.Empty, sb.ToString());

        sb = new();
        string expected = "[City]=@City";
        WhereBuilder<Person>.BuildWhereString(sb, new { City = "Chicago" });
        Assert.Equal(expected, sb.ToString());

        sb = new();
        expected = "[First_Name]=@FirstName AND [Last_Name]=@LastName";
        WhereBuilder<Person>.BuildWhereString(sb, new { FirstName = "Nick", LastName = "Zentner" });
        Assert.Equal(expected, sb.ToString());
    }

}
