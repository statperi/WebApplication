using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public interface IDepartment
    {
        IList<Employee> Employees { get; set; }

        /// <summary>
        /// Creates the department and stores it in db
        /// </summary>
        void Create();

        /// <summary>
        /// Deletes the department
        /// </summary>
        void Delete();

        /// <summary>
        /// Updates the department's attributes
        /// </summary>
        void Edit();

        /// <summary>
        /// Adds Employee
        /// </summary>
        void AddEmployee(Employee employee);

        /// <summary>
        /// Removes Employee
        /// </summary>
        void RemoveEmployee(Employee employee);

        /// <summary>
        /// Indicates whether the department has a selected Employee
        /// </summary>
        /// <returns>flag for employee in department</returns>
        bool HasEmployee(Employee employee);

        /// <summary>
        /// Indicates whether the department is full
        /// </summary>
        /// <returns>flag for full department</returns>
        bool IsFull();
    }
}