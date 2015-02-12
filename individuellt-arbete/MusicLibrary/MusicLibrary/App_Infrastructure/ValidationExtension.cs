using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicLibrary
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