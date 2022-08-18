namespace NorthwindRepository;

public class NorthwindContext
{
    private readonly string _connectionStringName;
    #region Constructors
    public NorthwindContext(string connectionStringName)
    {
        _connectionStringName = connectionStringName;
    }
    #endregion

    #region Repositories
    ICategoriesRepository CategoryRepository() => new CategoriesRepository(_connectionStringName);
    ICustomerRepository CustomerRepository() => new CustomerRepository(_connectionStringName);
    IEmployeeRepository EmployeeRepository() => new EmployeeRepository(_connectionStringName);
    IEmployeeTerritoriesRepository EmployeeTerritoriesRepository() => new EmployeeTerritoriesRepository(_connectionStringName);
    ITerritoryRepository TerritoryRepository() => new TerritoryRepository(_connectionStringName);
    #endregion

    #region Category Functions
    public Category GetCategoryWithImage(int id)
    {
        ICategoriesRepository categoryRepository = CategoryRepository();
        Category category = categoryRepository.Get(id);
        category.Image = categoryRepository.GetImage(id);
        return category;
    }

    public IEnumerable<Category> GetCategoryListWithImage(object whereConditions)
    {
        ICategoriesRepository categoryRepository = CategoryRepository();
        IEnumerable<Category> categories = categoryRepository.GetList(whereConditions);
        foreach (Category category in categories)
        {
            category.Image = categoryRepository.GetImage(category.CategoryId);
        }
        return categories;
    }

    public async Task<Category> GetCategoryWithImageAsync(int id)
    {
        ICategoriesRepository categoryRepository = CategoryRepository();
        Category category = await categoryRepository.GetAsync(id);
        category.Image = await categoryRepository.GetImageAsync(id);
        return category;
    }

    public async Task<IEnumerable<Category>> GetCategoryListWithImageAsync(object whereConditions)
    {
        ICategoriesRepository categoryRepository = CategoryRepository();
        IEnumerable<Category> categories = await categoryRepository.GetListAsync(whereConditions);
        foreach (Category category in categories)
        {
            category.Image = await categoryRepository.GetImageAsync(category.CategoryId);
        }
        return categories;
    }
    #endregion

    #region Employee Territories Functions
    public Employee GetEmployeeWithTerritories(int employeeId)
    {
        IEmployeeRepository employeeRepository = EmployeeRepository();
        Employee employee = employeeRepository.Get(employeeId);

        IEmployeeTerritoriesRepository employeeTerritoriesRepository = EmployeeTerritoriesRepository();
        IEnumerable<EmployeeTerritories> employeeTerritories = employeeTerritoriesRepository.GetList(new { EmployeeId = employeeId });

        ITerritoryRepository territoryRepository = TerritoryRepository();
        employee.Territories = new List<Territory>();
        foreach (var employeeTerritory in employeeTerritories)
        {
            Territory territory = territoryRepository.Get(employeeTerritory.TerritoryId);
            if (territory is not null)
                employee.Territories.Add(territory);
        }

        return employee;
    }
    #endregion
}
