using System;
using System.Web;
using Logger;
using System.Xml;
using System.Web.Routing;

namespace Receptsamlingen.Web
{
	public class Global : HttpApplication
	{
		void Application_Start(object sender, EventArgs e)
		{
			// log4net
			LogHandler.Configure();

			// URL Routing
			var routeDoc = new XmlDocument();
			routeDoc.Load(Server.MapPath("~/Routes.xml"));
			RegisterRoutes(RouteTable.Routes, routeDoc);
		}

		public static void RegisterRoutes(RouteCollection routes, XmlDocument routeDoc)
		{
			try
			{
				var routeList = routeDoc.GetElementsByTagName("route");
				foreach (XmlNode route in routeList)
				{
					if (route != null)
					{
						var routeUrl = route["url"].InnerText;
						var file = route["file"].InnerText;
						routes.RouteExistingFiles = true;     
						routes.MapPageRoute("", routeUrl, file, true);
					}
				}
			}
			catch (Exception error)
			{
				// Log routing errors
			}
		}

		void Application_End(object sender, EventArgs e)
		{
			// Code that runs on application shutdown
		}

		void Application_Error(object sender, EventArgs e)
		{
			var ex = Server.GetLastError();
			var exception = ex.InnerException ?? ex.GetBaseException();
			if (exception.GetType() == typeof(HttpException))
			{
				var httpException = exception as HttpException;
				if (httpException != null && httpException.GetHttpCode() == 404)
				{
					if (!HttpContext.Current.Request.Path.EndsWith("/404", StringComparison.InvariantCultureIgnoreCase))
					{
						Response.Redirect("~/404");
					}
				}
			}
			else
			{
				LogHandler.Log(LogType.Error, exception);
				if (!HttpContext.Current.Request.Path.EndsWith("/fel", StringComparison.InvariantCultureIgnoreCase))
				{
					Response.Redirect("~/fel");
				}
				
			}
			Server.ClearError();
		}

		void Session_Start(object sender, EventArgs e)
		{
			// Code that runs when a new session is started
		}

		void Session_End(object sender, EventArgs e)
		{
			// Code that runs when a session ends. 
			// Note: The Session_End event is raised only when the sessionstate mode
			// is set to InProc in the Web.config file. If session mode is set to StateServer 
			// or SQLServer, the event is not raised.
		}
	}
}