using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.Helpers
{
    public class CompanyManager : ICompanyManager
    {
        private ICompanyContext companyContext { get { return new CompanyContext(); } }

        public Department GetDepartment(int id)
        {
            Department department;

            using (var ctx = (CompanyContext)companyContext)
            {
                department = ctx.Departments.Include("Employees").FirstOrDefault(dep => dep.DepartmentId == id);
            }

            return department;
        }

        public void CreateDepartment(Department department)
        {
            department.companyContext = this.companyContext;
            department.Create();
        }

        public void DeleteDepartment(Department department)
        {
            department.companyContext = this.companyContext;
            department.Delete();
        }

        public void UpdateDepartment(Department department)
        {
            department.companyContext = this.companyContext;
            department.Edit();
        }
        
        public List<Department> GetAllDepartments(bool includeEmployees = false)
        {
            var departmentsList = new List<Department>();

            using (var ctx = (CompanyContext)companyContext)
            {
                if (includeEmployees)
                    departmentsList.AddRange(ctx.Departments.Include("Employees"));
                else
                    departmentsList.AddRange(ctx.Departments);
            }

            return departmentsList;
        }


        public Employee GetEmployee(int id)
        {
            Employee employee;

            using (var ctx = (CompanyContext)companyContext)
            {
                employee = ctx.Employees.Include("Department").FirstOrDefault(emp => emp.EmployeeId == id);
            }

            return employee;
        }

        public void CreateEmployee(Employee employee)
        {
            employee.companyContext = this.companyContext;
            employee.Create();
        }

        public void DeleteEmployee(Employee employee)
        {
            employee.companyContext = this.companyContext;
            employee.Delete();
        }
        
        public void UpdateEmployee(Employee employee)
        {
            employee.companyContext = this.companyContext;
            employee.Edit();
        }

        public List<Employee> GetAllEmployees(bool includeDepartment = false)
        {
            var employeeList = new List<Employee>();

            using (var ctx = (CompanyContext)companyContext)
            {
                if (includeDepartment)
                    employeeList.AddRange(ctx.Employees.Include("Department"));
                else
                    employeeList.AddRange(ctx.Employees);
            }

            return employeeList;
        }
    }
}