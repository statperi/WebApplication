using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication.Controllers;
using WebApplication.Helpers;
using Newtonsoft.Json;
using WebApplication.IoC;

namespace WebApplication.Tests.Controllers
{
    [TestClass]
    public class CompanyControllerTest
    {
        [TestMethod]
        public void EmployeeEdit()
        {
            var companyManager = new CompanyManager();

            var empl = companyManager.GetAllEmployees(false).First();

            // Arrange
            CompanyController controller = new CompanyController();

            // Act
            JsonResult result = 
                controller.EmployeeEdit(empl.EmployeeId, "new name", "new last name", "newemail@newemail.com", 
                DateTime.Now.AddYears(-25).ToString("dd-MM-yyyy"), 80) 
                as JsonResult;
            
            //need to serailize bad shape of json data
            string json = JsonConvert.SerializeObject(result.Data);
            ResData res = JsonConvert.DeserializeObject<ResData>(json);

            //0 is the operation success response
            Assert.AreEqual(0, res.Response);
        }
    }
}
