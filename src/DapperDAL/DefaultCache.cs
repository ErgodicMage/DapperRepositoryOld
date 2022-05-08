using Microsoft.Extensions.Caching.Memory;

namespace DapperDAL;

// This needs more work to prevent race conditions
public static class DefaultCache
{
    private static MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
    public static MemoryCache Cache { get => _cache; }

    public static T GetorSet<T>(string key, Func<T> create)
    {
        T retObj;

        if (!DefaultCache.Cache.TryGetValue(key, out retObj))
            return retObj;

        retObj = DefaultCache.Cache.Set(key, create());

        return retObj;
    }
}