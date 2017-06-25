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
        public static void RegisterAll(IUnityContainer container)
        {
            //register..
            container.RegisterType<ICompanyManager, CompanyManager>();
            container.RegisterType<IFactory, Factory>();
            container.RegisterType<IDepartment, Department>();
            container.RegisterType<IEmployee, Employee>();
        }
    }
}