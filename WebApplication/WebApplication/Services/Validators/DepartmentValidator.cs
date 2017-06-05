using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Services.ValidationData;

namespace WebApplication.Services.Validators
{
    public class DepartmentValidator : AbstractValidator<DepartmentValidationData>
    {
        #region Constructors
        
        public DepartmentValidator()
        {
            this.RuleFor(x => x.DepartmentName).NotEmpty();
            this.RuleFor(x => x.MaxNumberOfEmployees).NotEmpty().GreaterThan(0);
        }

        #endregion
    }
}