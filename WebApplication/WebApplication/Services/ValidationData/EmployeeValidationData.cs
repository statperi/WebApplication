using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Services.ValidationData
{
    public class EmployeeValidationData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public int DepartmentId { get; set; }

        public static EmployeeValidationData Create(string firstName, string lastName, string email, DateTime birthdate, int departmentId)
        {
            return new EmployeeValidationData()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Birthdate = birthdate,
                DepartmentId = departmentId
            };
        }

    }
}