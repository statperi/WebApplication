using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication.Controllers;
using WebApplication.Helpers;
using Newtonsoft.Json;
using WebApplication.IoC;
using WebApplication.Models;

namespace WebApplication.Tests.Controllers
{
    [TestClass]
    public class CompanyControllerTest
    {
        [TestMethod]
        public void EmployeeEdit()
        {
            var companyManager = new CompanyManager();
            var factory = new Factory();

            var empl = companyManager.GetAllEmployees(false).First();
            var dep = companyManager.GetAllDepartments(true).First(x => !x.IsFull());

            if (dep == null || empl == null) return;

            // Arrange
            CompanyController controller = new CompanyController(companyManager, factory);

            // Act
            //add new values to the first employee availabe to the first department available
            JsonResult result = 
                controller.EmployeeEdit(empl.EmployeeId, "new name", "new last name", "newemail@newemail.com", 
                DateTime.Now.AddYears(-25).ToString("dd-MM-yyyy"), dep.DepartmentId) 
                as JsonResult;

            //need to serialize JsonResult to json data and deserialize it in proper class object
            string json = JsonConvert.SerializeObject(result.Data);
            ResData res = JsonConvert.DeserializeObject<ResData>(json);

            //0 is the operation success response
            Assert.AreEqual(0, res.Response);
        }
    }
}
