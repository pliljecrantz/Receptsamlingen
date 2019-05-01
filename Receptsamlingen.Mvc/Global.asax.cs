using Logger;
using System;
using System.Diagnostics;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Receptsamlingen.Mvc
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            if (!Debugger.IsAttached)
            {
                var ex = Server.GetLastError();
                Response.Clear();
                var exception = ex.InnerException ?? ex.GetBaseException();
                if (exception.GetType() == typeof(HttpException))
                {
                    var httpException = exception as HttpException;
                    if (httpException != null)
                    {
                        LogHandler.Log(nameof(GlobalConfiguration), LogType.Error, string.Format("Stack trace: {0}\tMessage: {1}", httpException.StackTrace, httpException.Message));
                        if (httpException.GetHttpCode() == 404)
                        {
                            if (!HttpContext.Current.Request.Path.EndsWith("/404", StringComparison.InvariantCultureIgnoreCase))
                            {
                                Context.Server.TransferRequest("~/404.aspx", true);
                            }
                        }
                        else if (httpException.GetHttpCode() == 500)
                        {
                            if (!HttpContext.Current.Request.Path.EndsWith("/Error", StringComparison.InvariantCultureIgnoreCase))
                            {
                                Context.Server.TransferRequest("~/Error.aspx", true);
                            }
                        }
                    }
                }
                else
                {
                    LogHandler.Log(nameof(GlobalConfiguration), LogType.Error, string.Format("Stack trace: {0}\tMessage: {1}", exception.StackTrace, exception.Message));
                    if (!HttpContext.Current.Request.Path.EndsWith("/Error", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Context.Server.TransferRequest("~/Error.aspx", true);
                    }
                }
                Server.ClearError();
            }
        }
    }
}