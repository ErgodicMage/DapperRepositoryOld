namespace NorthwindRepository
{
    public interface ICategoriesRepository : IGenericRepository<Category, int>
    {
        byte[]? GetImage(IDbConnection connection, int id, IDbTransaction? transaction = null, int? commandTimeout = null);
        byte[]? GetImage(int id, int? commandTimeout = null);
        Task<byte[]?> GetImageAsync(IDbConnection connection, int id, IDbTransaction? transaction = null, int? commandTimeout = null);
        Task<byte[]?> GetImageAsync(int id, int? commandTimeout = null);
        int UpdateDescription(IDbConnection connection, int id, string description, IDbTransaction? transaction = null, int? commandTimeout = null);
        int UpdateDescription(int id, string description, int? commandTimeout = null);
        Task<int> UpdateDescriptionAync(IDbConnection connection, int id, string description, IDbTransaction? transaction = null, int? commandTimeout = null);
        Task<int> UpdateDescriptionAync(int id, string description, int? commandTimeout = null);
        int UpdateImage(IDbConnection connection, int id, byte[] image, IDbTransaction? transaction = null, int? commandTimeout = null);
        int UpdateImage(int id, byte[] image, int? commandTimeout = null);
        Task<int> UpdateImageAync(IDbConnection connection, int id, byte[] image, IDbTransaction? transaction = null, int? commandTimeout = null);
        Task<int> UpdateImageAync(int id, byte[] image, int? commandTimeout = null);
    }
}