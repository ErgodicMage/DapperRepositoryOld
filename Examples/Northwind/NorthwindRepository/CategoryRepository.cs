namespace NorthwindRepository;

public class CategoryRepository : GenericRepository<Category, int>, ICategoriesRepository
{
    #region Constructor
    public CategoryRepository(string connectionStringName)
    {
        _connectionStringName = connectionStringName;
    }
    #endregion

    #region Connection
    private readonly string _connectionStringName;
    private string? ConnectionString => DapperDALSettings.ConnectionStrings(_connectionStringName);

    protected override IDbConnection GetConnection() => new SqlConnection(ConnectionString);
    #endregion

    public byte[]? GetImage(int id, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetImage(connection, id, null, commandTimeout);
    }

    public byte[]? GetImage(IDbConnection connection, int id, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        var tableattr = typeof(Category).GetCustomAttributes(true).SingleOrDefault(attr =>
            attr.GetType().Name == typeof(TableAttribute).Name) as dynamic;
        string column = string.IsNullOrWhiteSpace(tableattr?.Alias) ? "[Picture]" : $"{tableattr?.Alias}.[Picture]";
        string sql = $"SELECT {column} from {BuilderCache<Category>.TableName} WHERE {BuilderCache<Category>.WhereIdString}";

        var prop = WhereBuilder<Category>.GetIdParameters(id);
        return connection.ExecuteScalar<byte[]>(sql, prop, transaction, commandTimeout);
    }

    public Task<byte[]?> GetImageAsync(int id, int? commandTimeout = null)
    {
        using var connection = GetConnection();
        return GetImageAsync(connection, id, null, commandTimeout);
    }

    public Task<byte[]?> GetImageAsync(IDbConnection connection, int id, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        var tableattr = typeof(Category).GetCustomAttributes(true).SingleOrDefault(attr =>
            attr.GetType().Name == typeof(TableAttribute).Name) as dynamic;
        string column = string.IsNullOrWhiteSpace(tableattr?.Alias) ? "[Picture]" : $"{tableattr?.Alias}.[Picture]";
        string sql = $"SELECT {column} from {BuilderCache<Category>.TableName} WHERE {BuilderCache<Category>.WhereIdString}";

        var prop = WhereBuilder<Category>.GetIdParameters(id);
        return connection.ExecuteScalarAsync<byte[]?>(sql, prop, transaction, commandTimeout);
    }

    public int UpdateImage(int id, byte[] image, int? commandTimeout = null)
    {
        return Update(id, new { Picture = image }, commandTimeout);
    }

    public int UpdateImage(IDbConnection connection, int id, byte[] image, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        return Update(connection, id, new { Picture = image }, transaction, commandTimeout);
    }

    public Task<int> UpdateImageAync(int id, byte[] image, int? commandTimeout = null)
    {
        return UpdateAsync(id, new { Picture = image }, commandTimeout);
    }

    public Task<int> UpdateImageAync(IDbConnection connection, int id, byte[] image, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        return UpdateAsync(connection, id, new { Picture = image }, transaction, commandTimeout);
    }

    public int UpdateDescription(int id, string description, int? commandTimeout = null)
    {
        return Update(id, new { Description = description }, commandTimeout);
    }

    public int UpdateDescription(IDbConnection connection, int id, string description, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        return Update(connection, id, new { Description = description }, transaction, commandTimeout);
    }

    public Task<int> UpdateDescriptionAync(int id, string description, int? commandTimeout = null)
    {
        return UpdateAsync(id, new { Description = description }, commandTimeout);
    }

    public Task<int> UpdateDescriptionAync(IDbConnection connection, int id, string description, IDbTransaction? transaction = null, int? commandTimeout = null)
    {
        return UpdateAsync(connection, id, new { Description = description }, transaction, commandTimeout);
    }
}
