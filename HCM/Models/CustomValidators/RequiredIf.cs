using System.ComponentModel.DataAnnotations;

namespace HCM.Models.CustomValidators
{
    public class RequiredIfAttribute : ValidationAttribute
    {
        public string PropertyName { get; set; }
        public object Value { get; set; }



        public RequiredIfAttribute(string propertyName, object value, string errorMessage = "")
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
            Value = value;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var instance = validationContext.ObjectInstance;
            var type = instance.GetType();
            var proprtyvalue = type.GetProperty(PropertyName).GetValue(instance);
            if (proprtyvalue?.ToString() == Value?.ToString() && string.IsNullOrEmpty(value?.ToString()))
            {
                return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
            }
            return ValidationResult.Success;
        }
    }

}
