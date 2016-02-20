using System.Collections.Generic;
using System.Web;
using Receptsamlingen.Repository;

namespace Receptsamlingen.Mvc.Classes
{
    public static class SessionHandler
	{

		#region Properties

		public static User User
		{
			get
			{
				return Get(Globals.UserSessionString) != null ? Get(Globals.UserSessionString) as User : null;
			}
			set
			{
				Set(Globals.UserSessionString, value);
			}
		}

		public static bool IsAuthenticated
		{
			get
			{
				return Get(Globals.IsAuthenticatedSessionString) != null && (bool)Get(Globals.IsAuthenticatedSessionString);
			}
			set
			{
				Set(Globals.IsAuthenticatedSessionString, value);
			}
		}

		public static string CurrentGuid
		{
			get
			{
				return Get(Globals.CurrentGuidString) != null ? Get(Globals.CurrentGuidString).ToString() : null;
			}
			set
			{
				Set(Globals.CurrentGuidString, value);
			}
		}

		public static string CurrentId
		{
			get
			{
				return Get(Globals.CurrentIdString) != null ? Get(Globals.CurrentIdString).ToString() : null;
			}
			set
			{
				Set(Globals.CurrentIdString, value);
			}
		}

		public static IList<int> RecipeIdList
		{
			get
			{
				return Get(Globals.IdListString) as IList<int>;
			}
			set
			{
				Set(Globals.IdListString, value);
			}
		}

		public static bool FailedLogin
		{
			get
			{
				return Get(Globals.FailedLoginSessionString) != null ? (bool) Get(Globals.FailedLoginSessionString) : false;
			}
			set
			{
				Set(Globals.FailedLoginSessionString, value);
			}
		}

		#endregion

		#region Session Functions with failsafe handling

		private static object Get(string name)
		{
			object result = null;
			if (HttpContext.Current.Session != null)
			{
				if (HttpContext.Current.Session[name] != null)
				{
					result = HttpContext.Current.Session[name];
				}
			}
			return result;
		}

		private static void Set(string name, object value)
		{
			if (HttpContext.Current.Session != null)
			{
				HttpContext.Current.Session[name] = value;
			}
		}

		public static void Remove(string name)
		{
			if (HttpContext.Current.Session[name] != null)
			{
				HttpContext.Current.Session.Remove(name);
			}
		}

		#endregion

    }
}