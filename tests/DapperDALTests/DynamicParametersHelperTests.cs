namespace DapperDALTests;

public class DynamicParametersHelperTests
{
    private readonly ITestOutputHelper _output;
    private readonly DapperDALSettings _settings;

    public DynamicParametersHelperTests(ITestOutputHelper output)
    {
        TestingUtilities.TestNamespace = "DapperDALTests";
        TestingUtilities.LoadAppSettings();
        _settings = new DapperDALSettings(TestingUtilities.Configuration);

        _output = output;
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCODynamicParametersFromWhere()
    {
        var parameters = DynamicParametersHelper<POCO>.DynamicParametersFromWhere(new { Id = 1 });
        Assert.NotNull(parameters);
        Assert.Equal(1, parameters.ParameterNames?.Count());
        Assert.Equal(1, parameters.Get<int>("Id"));

        // pass back the id dynamic parameters and get a new DynamicParameters with the Id value
        parameters = DynamicParametersHelper<POCO>.DynamicParametersFromWhere(parameters);
        Assert.NotNull(parameters);
        Assert.Equal(1, parameters.ParameterNames?.Count());
        Assert.Equal(1, parameters.Get<int>("Id"));

        parameters = DynamicParametersHelper<POCO>.DynamicParametersFromWhere(new { Value1 = "value1", Value2 = "value2" });
        Assert.NotNull(parameters);
        Assert.Equal(2, parameters.ParameterNames?.Count());
        Assert.Equal("value1", parameters.Get<string>("Value1"));
        Assert.Equal("value2", parameters.Get<string>("Value2"));

        parameters = DynamicParametersHelper<POCO>.DynamicParametersFromWhere(new { Id = 1 });
        parameters = DynamicParametersHelper<POCO>.DynamicParametersFromWhere(parameters, new { Value1 = "value1", Value2 = "value2" });
        Assert.NotNull(parameters);
        Assert.Equal(3, parameters.ParameterNames?.Count());
        Assert.Equal(1, parameters.Get<int>("Id"));
        Assert.Equal("value1", parameters.Get<string>("Value1"));
        Assert.Equal("value2", parameters.Get<string>("Value2"));
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCODynamicParametersUpdate()
    {
        var parameters = DynamicParametersHelper<POCO>.DynamicParametersFromWhere(new { Id = 1 });
        Assert.NotNull(parameters);
        Assert.Equal(1, parameters.ParameterNames?.Count());
        Assert.Equal(1, parameters.Get<int>("Id"));

        parameters = DynamicParametersHelper<POCO>.DynamicParametersUpdate(new { Value1 = "value1" }, parameters);
        Assert.NotNull(parameters);
        Assert.Equal(2, parameters.ParameterNames?.Count());
        Assert.Equal(1, parameters.Get<int>("Id"));
        Assert.Equal("value1", parameters.Get<string>("Value1"));

        parameters = DynamicParametersHelper<POCO>.DynamicParametersUpdate(new { Id = 1, Value1 = "value1", Value2 = "value2" });
        Assert.NotNull(parameters);
        Assert.Equal(3, parameters.ParameterNames?.Count());
        Assert.Equal(1, parameters.Get<int>("Id"));
        Assert.Equal("value1", parameters.Get<string>("Value1"));
        Assert.Equal("value2", parameters.Get<string>("Value2"));

        // in this  case Id should not be added since it would be expected to be added when the orginial DynamicParamters is created
        parameters = DynamicParametersHelper<POCO>.DynamicParametersUpdate(new { Value1 = "value1", Value2 = "value2" });
        parameters = DynamicParametersHelper<POCO>.DynamicParametersUpdate(new { Id = 1 }, parameters);
        Assert.NotNull(parameters);
        Assert.Equal(2, parameters.ParameterNames?.Count());
        Assert.False(parameters.ParameterNames?.Contains("Id"));
        Assert.Equal("value1", parameters.Get<string>("Value1"));
        Assert.Equal("value2", parameters.Get<string>("Value2"));
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void POCODynamicParametersFromGet()
    {
        var parameters = DynamicParametersHelper<POCO>.DynamicParametersFromGet();
        Assert.NotNull(parameters);
        Assert.Equal(3, parameters.ParameterNames?.Count());
        Assert.True(parameters.ParameterNames?.Contains("Id"));
        Assert.True(parameters.ParameterNames?.Contains("Value1"));
        Assert.True(parameters.ParameterNames?.Contains("Value2"));

        parameters = DynamicParametersHelper<POCO>.DynamicParametersFromWhere(new { Id = 1 });
        Assert.NotNull(parameters);
        Assert.Single(parameters?.ParameterNames);
        Assert.Equal(1, parameters.Get<int>("Id"));

        parameters = DynamicParametersHelper<POCO>.DynamicParametersFromGet(parameters);
        Assert.NotNull(parameters);
        Assert.Equal(3, parameters.ParameterNames?.Count());
        Assert.True(parameters.ParameterNames?.Contains("Id"));
        Assert.True(parameters.ParameterNames?.Contains("Value1"));
        Assert.True(parameters.ParameterNames?.Contains("Value2"));
    }
}
