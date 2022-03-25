using HCM.Models.CustomValidators;
using HCM.Resources;
using System.ComponentModel.DataAnnotations;

namespace HCM.Models
{
    public class SettingsSMTPForm
    {
        [Display(Name = "SMTP_2", ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = "Form_Field_Required", ErrorMessageResourceType = typeof(SharedResources))]
        public string SMTPServer { get; set; }

        [Display(Name = "SMTP_3", ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = "Form_Field_Required", ErrorMessageResourceType = typeof(SharedResources))]
        public string SMTPServerPort { get; set; }


       
        public bool SMTPServerAuth { get; set; }

        [Display(Name = "SMTP_5", ResourceType = typeof(SharedResources))]
        [RequiredIf(nameof(SMTPServerAuth), true, ErrorMessageResourceName = "Form_Field_Required", ErrorMessageResourceType = typeof(SharedResources))]
        public string SMTPServerUser { get; set; }


        [Display(Name = "SMTP_6", ResourceType = typeof(SharedResources))]
        [RequiredIf(nameof(SMTPServerAuth), true, ErrorMessageResourceName = "Form_Field_Required", ErrorMessageResourceType = typeof(SharedResources))]
        public string SMTPServerPass { get; set; }

    }
}
