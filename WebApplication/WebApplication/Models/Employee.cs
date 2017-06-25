using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApplication.Helpers;

namespace WebApplication.Models
{
    public class Employee : IEmployee
    {
        internal Employee() { }

        #region fields
        
        public ICompanyContext companyContext { get; set; }

        [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthdayDate { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        
        public Department Department { get; set; }

        #endregion

        public void Create()
        {
            using (var ctx = (CompanyContext)companyContext)
            {
                ctx.Employees.Add(this);
                ctx.SaveChanges();
            }
        }

        public void Delete()
        {
            using (var ctx = (CompanyContext)companyContext)
            {
                var employee = ctx.Employees.First(x => x.EmployeeId == this.EmployeeId);
                if (employee != null)
                {
                    ctx.Employees.Remove(employee);
                    ctx.SaveChanges();
                }
            }
        }

        public void Edit()
        {
            using (var ctx = (CompanyContext)companyContext)
            {
                var original = ctx.Employees.Find(this.EmployeeId);
                ctx.Entry(original).CurrentValues.SetValues(this);
                ctx.SaveChanges();
            }
        }
        
    }
}