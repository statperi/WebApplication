using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Helpers;
using WebApplication.IoC;
using WebApplication.Models;
using WebApplication.Services.ValidationData;
using WebApplication.Services.Validators;

namespace WebApplication.Controllers
{
    public class CompanyController : Controller
    {
        private CompanyManager CompanyManager = UnityContainerManager.IoC.Resolve<CompanyManager>();

        public ActionResult Details()
        {
            ViewData["Departments"] = this.CompanyManager.GetAllDepartments(true);

            ViewBag.Title = "THE COMPANY";
            ViewBag.Message = "This is an application to handle your company departments and employees!";
            
            return View();
        }

        public ActionResult Departments()
        {
            ViewData["Departments"] = this.CompanyManager.GetAllDepartments();

            ViewBag.Title = "COMPANY DEPARTMENTS";
            ViewBag.Message = "Here you can handle the company's Departments.";
            ViewBag.NoResultsMessage = "No Departments Found";
            
            return View();
        }

        public ActionResult Employees()
        {
            ViewData["Employees"] = this.CompanyManager.GetAllEmployees(true);
            ViewData["Departments"] = this.CompanyManager.GetAllDepartments(true);

            ViewBag.Title = "COMPANY EMPLOYEES";
            ViewBag.Message = "Here you can handle the company's Employees.";
            ViewBag.NoResultsMessage = "No Employees Found";
            ViewBag.NoDepartmentsMessage = "No Departments Found. An employee must be asigned to a department so please create one first.";
            ViewBag.NoDepartmentsAvailableMessage = "No available Departments Found. An employee must be asigned to a department so please make room to the one you want for your employee.";
            

            return View();
        }
        

        public ActionResult DepartmentEditView(int id)
        {
            ViewData["Department"] = this.CompanyManager.GetDepartment(id);

            ViewBag.Title = "Edit Department";
            ViewBag.Message = "Here you can edit the department's details.";

            ViewBag.HideHeader = true;
            ViewBag.HideFooter = true;

            return View();
        }
        
        public ActionResult EmployeeEditView(int id)
        {
            ViewData["Employee"] = this.CompanyManager.GetEmployee(id);
            ViewData["Departments"] = this.CompanyManager.GetAllDepartments(true);

            ViewBag.Title = "Edit Employee";
            ViewBag.Message = "Here you can edit the employee's details.";

            ViewBag.HideHeader = true;
            ViewBag.HideFooter = true;

            return View();
        }



        public ActionResult CreateEmployee(string firstName, string lastName, string email, string birthdate, int depId)
        {
            try
            {
                //Validation
                var validationData = EmployeeValidationData.Create(firstName, lastName, email, Convert.ToDateTime(birthdate), depId);
                var validationResults = new EmployeeValidator().Validate(validationData);

                if (!validationResults.IsValid)
                {
                    return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.ValidationError, Message = "Validation Error" });
                }

                var employeeToCreate = new Employee()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    BirthdayDate = Convert.ToDateTime(birthdate),
                    DepartmentId = depId
                };

                CompanyManager.CreateEmployee(employeeToCreate);

                return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.OperationSuccesful, Message = "Employee Created Succesfully" });
            }
            catch (Exception e)
            {
                return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.GenericError, Message = "Something went wrong" });
            }
        }

        public ActionResult EmployeeEdit(int id, string firstName, string lastName, string email, string birthdate, int depId)
        {
            try
            {
                //Validation
                var validationData = EmployeeValidationData.Create(firstName, lastName, email, Convert.ToDateTime(birthdate), depId);
                var validationResults = new EmployeeValidator().Validate(validationData);

                if (!validationResults.IsValid)
                {
                    return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.ValidationError, Message = "Validation Error" });
                }

                var employeeToEdit = CompanyManager.GetEmployee(id);
                if (employeeToEdit != null)
                {
                    var newDepartment = CompanyManager.GetDepartment(depId);
                    if (newDepartment.IsFull() && employeeToEdit.DepartmentId != newDepartment.DepartmentId)
                    {
                        return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.GenericError, Message = "Department is full please pick another one." });
                    }

                    employeeToEdit.DepartmentId = depId;
                    employeeToEdit.FirstName = firstName;
                    employeeToEdit.LastName = lastName;
                    employeeToEdit.Email = email;
                    employeeToEdit.BirthdayDate = Convert.ToDateTime(birthdate);

                    CompanyManager.UpdateEmployee(employeeToEdit);
                   
                    return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.OperationSuccesful, Message = "Employee Edited Succesfully" });
                }
                
                return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.GenericError, Message = "Something went wrong" });
            }
            catch (Exception e)
            {
                return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.GenericError, Message = "Something went wrong" });
            }

        }

        public ActionResult DeleteEmployee(int id)
        {
            try
            {
                var employeeToDelete = CompanyManager.GetEmployee(id);
                if (employeeToDelete != null)
                {
                    CompanyManager.DeleteEmployee(employeeToDelete);
                    return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.OperationSuccesful, Message = "Employee Deleted Succesfully" });
                }

                return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.GenericError, Message = "Something went wrong" });
            }
            catch (Exception e)
            {
                return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.GenericError, Message = "Something went wrong" });
            }
        }


        public ActionResult CreateDepartment(string departmentName, int maxNumberOfEmployees)
        {
            try
            {
                //Validation
                var validationData = DepartmentValidationData.Create(departmentName, maxNumberOfEmployees);
                var validationResults = new DepartmentValidator().Validate(validationData);

                if (!validationResults.IsValid)
                {
                    return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.ValidationError, Message = "Validation Error" });
                }

                if (!string.IsNullOrEmpty(departmentName) && maxNumberOfEmployees > 0)
                {
                    var departmentToCreate = new Department() { DepartmentName = departmentName, MaxEmployees = maxNumberOfEmployees, Employees = new List<Employee>() };
                    CompanyManager.CreateDepartment(departmentToCreate);
                    return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.OperationSuccesful, Message = "Department Created Succesfully" });
                }

                return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.GenericError, Message = "Something went wrong" });
            }
            catch (Exception e)
            {
                return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.GenericError, Message = "Something went wrong" });
            }
        }

        public ActionResult DepartmentEdit(int id, string departmentName, int maxNumberOfEmployees)
        {
            try
            {
                var departmentToEdit = CompanyManager.GetDepartment(id);

                //extra validation for employees so the appropriate message shall return
                if (maxNumberOfEmployees < departmentToEdit.Employees.Count)
                { 
                    return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.GenericError, Message = "Cannot set max number of employees less than the current asigned employees" });
                }

                //Validation
                var validationData = DepartmentValidationData.Create(departmentName, maxNumberOfEmployees);
                var validationResults = new DepartmentValidator().Validate(validationData);

                if (!validationResults.IsValid)
                {
                    return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.ValidationError, Message = "Validation Error" });
                }

                if (departmentToEdit != null)
                {
                    departmentToEdit.DepartmentName = departmentName;
                    departmentToEdit.MaxEmployees = maxNumberOfEmployees;

                    CompanyManager.UpdateDepartment(departmentToEdit);
                    
                    return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.OperationSuccesful, Message = "Department Edited Succesfully" });
                }

                return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.GenericError, Message = "Something went wrong" });
            }
            catch (Exception e)
            {
                return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.GenericError, Message = "Something went wrong" });
            }

        }
        
        public ActionResult DeleteDepartment(int id)
        {
            try
            {
                var departmentToDelete = CompanyManager.GetDepartment(id);
                if (departmentToDelete != null)
                {
                    if (departmentToDelete.Employees.Any())
                    {
                        return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.GenericError, Message = "Department could not be deleted. Remove all of it's employees first." });
                    }

                    CompanyManager.DeleteDepartment(departmentToDelete);
                    return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.OperationSuccesful, Message = "Department Deleted Succesfully" });
                }

                return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.GenericError, Message = "Something went wrong" });
            }
            catch (Exception e)
            {
                return Json(new { Response = Services.ResponceTypes.ServiceResponce.ResponseTypes.GenericError, Message = "Something went wrong" });
            }
        }
    }
}