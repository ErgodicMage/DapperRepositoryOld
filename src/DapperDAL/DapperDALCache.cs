using Microsoft.Extensions.Caching.Memory;

namespace DapperDAL;

// This needs more work to prevent race conditions
public static class DapperDALCache
{
    private static IMemoryCache _cache;
    public static IMemoryCache Cache { get => _cache; internal set { _cache = value; } }

    public static T GetorSet<T>(string key, Func<T> create)
    {
        T retObj;

        if (_cache.TryGetValue(key, out retObj))
            return retObj;

        retObj = _cache.Set(key, create());

        return retObj;
    }

    public static string GetorSet(string key, string obj)
    {
        string retObj;

        if (_cache.TryGetValue(key, out retObj))
            return retObj;

        retObj = _cache.Set(key, obj);

        return retObj;
    }
}