using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Helpers
{
    public interface IFactory
    {
        IDepartment CreateDepartment();
        IEmployee CreateEmployee();
    }
}
