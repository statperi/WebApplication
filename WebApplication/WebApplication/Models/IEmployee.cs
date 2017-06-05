using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public interface IEmployee
    {
        /// <summary>
        /// Creates the employee and stores it in db
        /// </summary>
        void Create();

        /// <summary>
        /// Deletes the employee
        /// </summary>
        void Delete();

        /// <summary>
        /// Updates the employee's attributes
        /// </summary>
        void Edit();
    }
}