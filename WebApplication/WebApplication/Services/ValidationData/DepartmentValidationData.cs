using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.Services.ValidationData
{
    public class DepartmentValidationData
    {
        public string DepartmentName { get; set; }
        public int MaxNumberOfEmployees { get; set; }

        public static DepartmentValidationData Create(string departmentName, int maxNumberOfEmployees)
        {
            return new DepartmentValidationData()
            {
                DepartmentName = departmentName,
                MaxNumberOfEmployees = maxNumberOfEmployees
            };
        }

    }
}