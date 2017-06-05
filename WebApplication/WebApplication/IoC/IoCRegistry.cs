using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Helpers;
using WebApplication.Models;

namespace WebApplication.IoC
{
    public class IocRegistry
    {
        public static void RegisterAll()
        {
            //register..
            UnityContainerManager.IoC.RegisterType<ICompanyManager, CompanyManager>(new ContainerControlledLifetimeManager());
        }
    }
}