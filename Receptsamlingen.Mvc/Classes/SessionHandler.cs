﻿using System.Web;
using System;
using System.Collections.Generic;
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
				return Get(Globals.UserSessionString) as User;
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

		//public static int CurrentRecipeId
		//{
		//	get
		//	{
		//		return Convert.ToInt32(Get(Globals.CurrentRecipeIdSessionString));
		//	}
		//	set
		//	{
		//		Set(Globals.CurrentRecipeIdSessionString, value); 
		//	}
		//}

		//public static IList<int> RecipeIdList
		//{
		//	get
		//	{
		//		return Get(Globals.RecipeIdListSessionString) as IList<int>;
		//	}
		//	set
		//	{
		//		Set(Globals.RecipeIdListSessionString, value);
		//	}
		//}

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