using Domain.Models;
using HCM.Resources;
using System.ComponentModel.DataAnnotations;

namespace HCM.Models
{
    public class AddUserForm
    {
        [Display(Name = "Admin_Users_UserNameLabel", ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = "Form_Field_Required", ErrorMessageResourceType = typeof(SharedResources))]
        public string LoginName { get; set; }

        [Display(Name = "Admin_Users_EmailLabel", ResourceType = typeof(SharedResources))]
        [RegularExpression("^$|^.*@.*\\..*$", ErrorMessageResourceName = "Form_Field_RegularExpression", ErrorMessageResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = "Form_Field_Required", ErrorMessageResourceType = typeof(SharedResources))]
        public string EMail { get; set; }

        [Display(Name = "Admin_Users_RealNameLabel", ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = "Form_Field_Required", ErrorMessageResourceType = typeof(SharedResources))]
        public string Name { get; set; }

        [Display(Name = "Admin_Users_AbteilungLabel", ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = "Form_Field_Required", ErrorMessageResourceType = typeof(SharedResources))]
        public string FunctionInFirma { get; set; }

        [Required(ErrorMessageResourceName = "Form_Field_Required", ErrorMessageResourceType = typeof(SharedResources))]
        [StringLength(50, MinimumLength = 6, ErrorMessageResourceName = "Form_Field_StringLength", ErrorMessageResourceType = typeof(SharedResources))]
        public string Password { get; set; }


        [Compare("Password", ErrorMessageResourceName = "Form_ConfirmPass_Error", ErrorMessageResourceType = typeof(SharedResources))]
        public string ConformPass { get; set; }
    }

    public class EditUserForm
    {        

        [Display(Name = "Admin_Users_UserNameLabel", ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = "Form_Field_Required", ErrorMessageResourceType = typeof(SharedResources))]
        public string LoginName { get; set; }

        [Display(Name = "Admin_Users_EmailLabel", ResourceType = typeof(SharedResources))]
        [RegularExpression("^$|^.*@.*\\..*$", ErrorMessageResourceName = "Form_Field_RegularExpression", ErrorMessageResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = "Form_Field_Required", ErrorMessageResourceType = typeof(SharedResources))]
        public string EMail { get; set; }

        [Display(Name = "Admin_Users_RealNameLabel", ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = "Form_Field_Required", ErrorMessageResourceType = typeof(SharedResources))]
        public string Name { get; set; }

        [Display(Name = "Admin_Users_AbteilungLabel", ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = "Form_Field_Required", ErrorMessageResourceType = typeof(SharedResources))]
        public string FunctionInFirma { get; set; }

        [Display(Name = "Admin_UsersIsApprovedLabel", ResourceType = typeof(SharedResources))]
        public bool Status { get; set; }

    }

    public class PassUserForm
    {
        [Required(ErrorMessageResourceName = "Form_Field_Required", ErrorMessageResourceType = typeof(SharedResources))]
        [StringLength(50, MinimumLength = 6, ErrorMessageResourceName = "Form_Field_StringLength", ErrorMessageResourceType = typeof(SharedResources))]
        public string Password { get; set; }


        [Compare("Password", ErrorMessageResourceName = "Form_ConfirmPass_Error", ErrorMessageResourceType = typeof(SharedResources))]
        public string ConformPass { get; set; }
    }

    public class UserRolesForm : UserRolesModel
    {
        public bool IsSelected { get; set; }
        public bool IsDisabled { get; set; }
    }

}
