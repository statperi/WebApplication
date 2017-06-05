using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.IoC
{
    public class UnityContainerManager
    {
        public static IUnityContainer IoC
        {
            get
            {
                return new UnityContainer();
            }
        }
    }
}