using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication.IoC;

namespace WebApplication.Controllers
{
    public class CompanyControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            try
            {
                if (controllerType == null)
                    throw new ArgumentNullException("controllerType");

                if (!typeof(IController).IsAssignableFrom(controllerType))
                    throw new ArgumentNullException(string.Format("Type requested is not a controller: {0}", controllerType.Name), "controllerType");

                return MvcUnityContainer.Container.Resolve(controllerType) as IController;
            }
            catch
            {
                return null;
            }
        }
    }
}