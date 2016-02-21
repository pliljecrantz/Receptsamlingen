using System.Web;

namespace Receptsamlingen.Mvc.Classes
{
	public static class CacheHandler
	{
		#region Cache Functions with failsafe handling

		public static object Get(string cacheKey)
		{
			object result = null;
			if (HttpRuntime.Cache != null)
			{
				if (HttpRuntime.Cache[cacheKey] != null)
				{
                    result = HttpRuntime.Cache[cacheKey];
				}
			}
			return result;
		}

		public static void Set(string cacheKey, object value)
		{
			if (HttpRuntime.Cache != null)
			{
				HttpRuntime.Cache[cacheKey] = value;
			}
		}

		public static void Remove(string cacheKey)
		{
			if (HttpRuntime.Cache[cacheKey] != null)
			{
				HttpRuntime.Cache.Remove(cacheKey);
			}
		}

		#endregion
	}
}