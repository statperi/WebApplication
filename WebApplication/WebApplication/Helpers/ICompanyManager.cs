using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.Helpers
{
    public interface ICompanyManager
    {
        void CreateDepartment(Department department);

        void DeleteDepartment(Department department);

        void UpdateDepartment(Department department);

        List<Department> GetAllDepartments(bool includeEmployees);

        void CreateEmployee(Employee employee);

        void DeleteEmployee(Employee employee);

        void UpdateEmployee(Employee employee);


        List<Employee> GetAllEmployees(bool includeDepartment);
    }
}