using System.Web;
using System;
using System.Collections.Generic;
using Receptsamlingen.Repository;

namespace Receptsamlingen.Web.Classes
{
    public static class SessionHandler
	{

		#region Members

		private const string CurrentIdString = "CurrentId";
	    private const string CurrentGuidString = "CurrentGuid";
		private const string IdListString = "IdList";
		private const string UserString = "User";

		#endregion

		#region Properties

		public static User User
		{
			get
			{
				return GetSession(UserString) as User;
			}
			set
			{
				SetSession(UserString, value);
			}
		}

		public static int CurrentId
		{
			get
			{ 
				return Convert.ToInt32(GetSession(CurrentIdString));
			}
			set
			{ 
				SetSession(CurrentIdString, value); 
			}
		}

		public static string CurrentGuid
		{
			get
			{
				return GetSession(CurrentGuidString).ToString();
			}
			set
			{
				SetSession(CurrentGuidString, value);
			}
		}

		public static IList<int> RecipeIdList
		{
			get
			{
				return GetSession(IdListString) as IList<int>;
			}
			set
			{
				SetSession(IdListString, value);
			}
		}

		#endregion

		#region Session Functions with failsafe handling

		private static object GetSession(string name)
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

		private static void SetSession(string name, object value)
		{
			if (HttpContext.Current.Session != null)
			{
				HttpContext.Current.Session[name] = value;
			}
		}

		public static void RemoveSession(string name)
		{
			if (HttpContext.Current.Session[name] != null)
			{
				HttpContext.Current.Session.Remove(name);
			}
		}

		#endregion

    }
}