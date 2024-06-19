using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuperSimpleScheduler_Backend.Constants
{
    public class DateInFuture : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return "Deadline should be in the future";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null){
                return ValidationResult.Success!;
            }

            var dateValue = value as DateTime? ?? new DateTime();
            if (dateValue < DateTime.Now){
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success!;
        }
    }
}