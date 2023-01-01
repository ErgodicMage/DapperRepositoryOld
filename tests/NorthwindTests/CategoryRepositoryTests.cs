using Dapper;
using System.Data.SqlClient;
namespace NorthwindTests;

public class CategoryRepositoryTests
{
    private readonly ITestOutputHelper _output;
    private readonly string _connectionStringName = "NorthwindDd";
    private NorthwindDbContext _context;

    public CategoryRepositoryTests(ITestOutputHelper output)
    {
        TestingUtilities.TestNamespace = "Northwind";
        TestingUtilities.LoadAppSettings();

        _output = output;

        DapperRepositorySettings settings = new DapperRepositorySettings();
        settings.Initialize(TestingUtilities.Configuration);
        DapperRepositorySettings.DefaultSettings = settings;

        _context = new NorthwindDbContext(settings, _connectionStringName);
    }

    [Fact]
    [Trait("Category", TestCategories.IntegrationTest)]
    public void CategoryDapperDAL()
    {
        string connectionString = DapperRepositorySettings.DefaultSettings.ConnectionString(_connectionStringName);
        using var connection = new SqlConnection(connectionString);
        Category category = connection.GetId<Category>(1);
        Assert.NotNull(category);
    }

    [Fact]
    [Trait("Category", TestCategories.IntegrationTest)]
    public async Task CategoryDapperDALAsync()
    {
        string connectionString = DapperRepositorySettings.DefaultSettings.ConnectionString(_connectionStringName);
        using var connection = new SqlConnection(connectionString);
        Category category = await connection.GetIdAsync<Category>(1);
        Assert.NotNull(category);
    }

    [Fact]
    [Trait("Category", TestCategories.IntegrationTest)]
    public void GetOneCategoryCheckValues()
    {
        Category expectedCategory = new() { CategoryId = 1, CategoryName = "Beverages", Description = "Soft drinks, coffees, teas, beers, and ales" };
        ICategoriesRepository repository = _context.CategoryRepository();
        var category = repository.Get(1);
        
        Assert.NotNull(category);
        Assert.Equal(expectedCategory.CategoryId, category.CategoryId);
        Assert.Equal(expectedCategory.CategoryName, category.CategoryName);
        Assert.Equal(expectedCategory.Description, category.Description);
        Assert.Equal(expectedCategory.Image, category.Image);
        Assert.NotStrictEqual(expectedCategory, category);
    }

    [Fact]
    [Trait("Category", TestCategories.IntegrationTest)]
    public async Task GetOneCategoryCheckValuesAsync()
    {
        Category expectedCategory = new() { CategoryId = 1, CategoryName = "Beverages", Description = "Soft drinks, coffees, teas, beers, and ales" };
        ICategoriesRepository repository = _context.CategoryRepository();
        var category = await repository.GetAsync(1);

        Assert.NotNull(category);
        Assert.Equal(expectedCategory.CategoryId, category.CategoryId);
        Assert.Equal(expectedCategory.CategoryName, category.CategoryName);
        Assert.Equal(expectedCategory.Description, category.Description);
        Assert.Equal(expectedCategory.Image, category.Image);
        Assert.NotStrictEqual(expectedCategory, category);
    }

    [Theory]
    [Trait("Category", TestCategories.IntegrationTest)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    public void GetCategory(int categoryId)
    {
        ICategoriesRepository repository = _context.CategoryRepository();
        var category = repository.Get(categoryId);
        Assert.NotNull(category);
        Assert.Equal(categoryId, category.CategoryId);
    }

    [Theory]
    [Trait("Category", TestCategories.IntegrationTest)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    public async Task GetCategoryAsync(int categoryId)
    {
        ICategoriesRepository repository = _context.CategoryRepository();
        var category = await repository.GetAsync(categoryId);
        Assert.NotNull(category);
        Assert.Equal(categoryId, category.CategoryId);
    }

    [Fact]
    [Trait("Category", TestCategories.IntegrationTest)]
    public void GetAllCategtories()
    {
        ICategoriesRepository categoryRepository = _context.CategoryRepository();
        var categories = categoryRepository.GetWhere();
        Assert.NotNull(categories);
        Assert.Equal(8, categories.Count());
    }

    [Fact]
    [Trait("Category", TestCategories.IntegrationTest)]
    public async Task GetAllCategtoriesAsync()
    {
        ICategoriesRepository categoryRepository = _context.CategoryRepository();
        var categories = await categoryRepository.GetWhereAsync();
        Assert.NotNull(categories);
        Assert.Equal(8, categories.Count());
    }

    [Fact]
    [Trait("Category", TestCategories.IntegrationTest)]
    public void GetAllCategoriesSortedByCategoryNameDesc()
    {
        ICategoriesRepository categoryRepository = _context.CategoryRepository();
        var categories = categoryRepository.GetWhere( orderBy : new {CategoryName = false});
        Assert.NotNull(categories);
        Assert.Equal(8, categories.Count());

        foreach( var category in categories)
            _output.WriteLine($"{category?.CategoryId} {category?.CategoryName}");
    }

    [Fact]
    [Trait("Category", TestCategories.IntegrationTest)]
    public async Task GetAllCategoriesSortedByCategoryNameDescAsync()
    {
        ICategoriesRepository categoryRepository = _context.CategoryRepository();
        var categories = await categoryRepository.GetWhereAsync(orderBy: new { CategoryName = false });
        Assert.NotNull(categories);
        Assert.Equal(8, categories.Count());

        foreach (var category in categories)
            _output.WriteLine($"{category?.CategoryId} {category?.CategoryName}");
    }

    [Theory]
    [Trait("Category", TestCategories.IntegrationTest)]
    [MemberData(nameof(CategoryNames))]
    public void GetCategoryByName(string categoryName)
    {
        ICategoriesRepository categoryRepository = _context.CategoryRepository();
        var categories = categoryRepository.GetWhere(new {CategoryName = categoryName});
        Assert.NotNull(categories);
        Assert.Single(categories);
        Assert.Equal(categoryName, categories.First().CategoryName);
    }

    [Theory]
    [Trait("Category", TestCategories.IntegrationTest)]
    [MemberData(nameof(CategoryNames))]
    public async Task GetCategoryByNameAsync(string categoryName)
    {
        ICategoriesRepository categoryRepository = _context.CategoryRepository();
        var categories = await categoryRepository.GetWhereAsync(new { CategoryName = categoryName });
        Assert.NotNull(categories);
        Assert.Single(categories);
        Assert.Equal(categoryName, categories.First().CategoryName);
    }

    public static IEnumerable<object[]> CategoryNames()
    {
        yield return new object[] { "Beverages" };
        yield return new object[] { "Condiments" };
        yield return new object[] { "Confections" };
        yield return new object[] { "Dairy Products" };
        yield return new object[] { "Grains/Cereals" };
        yield return new object[] { "Meat/Poultry" };
        yield return new object[] { "Produce" };
        yield return new object[] { "Seafood" };
    }

    [Fact]
    [Trait("Category", TestCategories.IntegrationTest)]
    public void InsertCategory()
    {
        var category = new Category()
        {
            CategoryName = "Soups",
            Description = "Soups and Broths"
        };

        var categoryRepository = _context.CategoryRepository();
        int id = categoryRepository.Insert(category);
        Assert.True(id > 0);
        Assert.Equal(id, category.CategoryId);

        var getCategory = categoryRepository.Get(id);

        Assert.NotNull(getCategory);
        Assert.Equal(id, getCategory?.CategoryId);
        Assert.Equal(category.CategoryName, getCategory?.CategoryName);
        Assert.Equal(category.Description, getCategory?.Description);
    }

    [Fact]
    [Trait("Category", TestCategories.IntegrationTest)]
    public async Task InsertCategoryAsync()
    {
        var category = new Category()
        {
            CategoryName = "Soups",
            Description = "Soups and Broths"
        };

        var categoryRepository = _context.CategoryRepository();
        int id = await categoryRepository.InsertAsync(category);
        Assert.True(id > 0);
        Assert.Equal(id, category.CategoryId);

        var getCategory = await categoryRepository.GetAsync(id);

        Assert.NotNull(getCategory);
        Assert.Equal(id, getCategory?.CategoryId);
        Assert.Equal(category.CategoryName, getCategory?.CategoryName);
        Assert.Equal(category.Description, getCategory?.Description);
    }
}
