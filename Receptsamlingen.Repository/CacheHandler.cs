using System.Collections.Generic;
using System.Web;
using System.Web.Caching;

namespace Receptsamlingen.Repository
{
    public static class CacheHandler
    {
        private const string cacheKeyCollectionName = "CacheKeyCollection";

        #region Cache Functions with failsafe handling

        public static object Get(string cacheKey)
        {
            object result = null;
            if (HttpRuntime.Cache != null && HttpRuntime.Cache[cacheKey] != null)
            {
                result = HttpRuntime.Cache[cacheKey];
            }
            return result;
        }

        public static void Set(string cacheKey, object value)
        {
            if (HttpRuntime.Cache != null && HttpRuntime.Cache[cacheKey] != null)
            {
                Remove(cacheKey);
            }
            HttpRuntime.Cache.Add(cacheKey, value, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }

        public static void Remove(string cacheKey)
        {
            if (HttpRuntime.Cache != null && HttpRuntime.Cache[cacheKey] != null)
            {
                HttpRuntime.Cache.Remove(cacheKey);
            }
        }

        public static void AddCacheKeyToCollection(string key)
        {
            var collection = Get(cacheKeyCollectionName) as List<string>;
            if (collection == null)
            {
                collection = new List<string>();
            }
            collection.Add(key);
            Set(cacheKeyCollectionName, collection);
        }

        public static IList<string> GetCacheKeyCollection()
        {
            return Get(cacheKeyCollectionName) as List<string>;
        }

        public static void PurgeCache()
        {
            var collection = GetCacheKeyCollection();
            if (collection != null && collection.Count > 0)
            {
                foreach (var item in collection)
                {
                    Remove(item);
                }
            }
        }

        #endregion
    }
}