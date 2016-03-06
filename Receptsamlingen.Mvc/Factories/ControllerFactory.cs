using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace Receptsamlingen.Mvc.Factories
{
    public class ControllerFactory : IControllerFactory
    {
        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            string controllerType = string.Empty;

            controllerType = ConfigurationManager.AppSettings[controllerName];

            if (controllerType == null)
            {
                throw new ConfigurationErrorsException("Assembly not configured for controller " + controllerName);
            }
            var controller = Type.GetType(string.Concat("Receptsamlingen.Mvc.Controllers", ".", controllerName, "Controller")); // Type.GetType(controllerType);

            var result = Resolve(controller) as IController;
            return result;
        }

        private object Resolve(Type controller)
        {
            var constructor = controller.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault();
            //if(constructor != null)
            {
                var constructorParameters = constructor.GetParameters();
                if (constructorParameters.Count() == 0)
                {
                    return Activator.CreateInstance(controller) as IController;
                }
                var parameters = new List<object>();
                foreach (var parameterInfo in constructorParameters)
                {
                    parameters.Add(Resolve(parameterInfo.ParameterType));
                }
                return constructor.Invoke(parameters.ToArray());
            }
            //return null;
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Disabled;
        }

        public void ReleaseController(IController controller)
        {
            var disposable = controller as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}