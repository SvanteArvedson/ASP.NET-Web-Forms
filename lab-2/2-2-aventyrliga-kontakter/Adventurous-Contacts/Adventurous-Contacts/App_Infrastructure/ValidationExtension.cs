using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Adventurous_Contacts.App_Infrastructure
{
    public static class ValidationExtension
    {
        public static bool Validate(this Object instance, out ICollection<ValidationResult> validationResults)
        {
            var validationContext = new ValidationContext(instance);
            validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(instance, validationContext, validationResults, true);
        }
    }
}