using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Department : IDepartment
    {

        #region fields

        public ICompanyContext companyContext { get; set; }

        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int MaxEmployees { get; set; }
        public IList<Employee> Employees { get; set; }
        

        #endregion

        public void Create()
        {
            using (var ctx = (CompanyContext)companyContext)
            {
                ctx.Departments.Add(this);
                ctx.SaveChanges();
            }
        }

        public void Delete()
        {
            //can't delete a department with employees
            if (!this.Employees.Any())
            {
                using (var ctx = (CompanyContext)companyContext)
                {
                    var department = ctx.Departments.Find(this.DepartmentId);
                    if (department != null)
                    {
                        ctx.Departments.Remove(department);
                        ctx.SaveChanges();
                    }
                }
            }
        }

        public void Edit()
        {
            using (var ctx = (CompanyContext)companyContext)
            {
                ctx.Departments.Attach(this);
                
                //this is not idiot proof but it gets the job done with one query
                //if we want to make it safer, we have to Find() the original record first compare it with the new and then do the update.
                ctx.Entry(this).State = System.Data.Entity.EntityState.Modified;

                ctx.SaveChanges();
            }
        }

        public void AddEmployee(Employee employee)
        {
            if (!Employees.Contains(employee))
                Employees.Add(employee);
        }

        public void RemoveEmployee(Employee employee)
        {
            Employees.Remove(employee);
        }

        public bool HasEmployee(Employee employee)
        {
            return Employees.Contains(employee);
        }

        public bool IsFull()
        {
            if (this.Employees == null) return false;

            return this.MaxEmployees <= this.Employees.Count;
        }
    }
}