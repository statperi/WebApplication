using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.IoC
{
    public static class MvcUnityContainer
    {
        public static UnityContainer Container { get; set; }
    }
}