using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.Helpers
{
    public class Factory : IFactory
    {
        public IDepartment CreateDepartment()
        {
            return new Department();
        }

        public IEmployee CreateEmployee()
        {
            return new Employee();
        }
    }
}