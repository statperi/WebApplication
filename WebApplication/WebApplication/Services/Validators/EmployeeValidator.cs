using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Services.ValidationData;

namespace WebApplication.Services.Validators
{
    public class EmployeeValidator : AbstractValidator<EmployeeValidationData>
    {
        #region Constructors
        
        public EmployeeValidator()
        {
            this.RuleFor(x => x.FirstName).NotEmpty();
            this.RuleFor(x => x.LastName).NotEmpty();
            this.RuleFor(x => x.Email).NotEmpty().EmailAddress();
            this.RuleFor(x => x.Birthdate).NotEmpty().LessThan(p => DateTime.Now).GreaterThan(s => DateTime.Now.AddYears(-100));
            this.RuleFor(x => x.DepartmentId).NotEmpty().GreaterThan(x => 0);
        }

        #endregion
    }
}