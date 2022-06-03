using Domain.Models;
using HCM.Resources;
using HCMApi;
using HCMModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HCM.Models
{
 

    public class ProfileBase
    {
        public int appID { get; set; }
        public int profileID { get; set; }
        public int ModifiedBy { get; set; }
    }
    public class ProfileGenInfoForm 
    {
        [Display(Name = "Profile_ProfileName", ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = "Form_Field_Required", ErrorMessageResourceType = typeof(SharedResources))]
        public string profileName { get; set; }

        [Display(Name = "Profile_Email_Language", ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = "Form_Field_Required", ErrorMessageResourceType = typeof(SharedResources))]
        public string NotificationLang { get; set; }

        [Display(Name = "Profile_Description", ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = "Form_Field_Required", ErrorMessageResourceType = typeof(SharedResources))]
        public string profilDescr { get; set; }
  
    }

    public class ProfileContactsForm 
    {
        public bool NotifyAllProfileParticipants { get; set; }
        public List<Contact> profileParticipantsList{ get; set; } = new List<Contact>();

        [Display(Name = "Cases_ChangeUsers_FullUserList", ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = "Form_Field_Required", ErrorMessageResourceType = typeof(SharedResources))]
        public string NewContactId { get; set; }


        [Display(Name = "Cases_ChangeUsers_List_Role", ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = "Form_Field_Required", ErrorMessageResourceType = typeof(SharedResources))]
        public string NewContactRole { get; set; }
    }

    public class ProfileEscalationContactsForm
    {

        public List<Contact> escalationUsersList { get; set; } = new List<Contact>();    

        [Display(Name = "Cases_ChangeUsers_FullUserList", ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = "Form_Field_Required", ErrorMessageResourceType = typeof(SharedResources))]
        public string NewContactId { get; set; }


        [Display(Name = "Cases_ChangeUsers_List_Role", ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = "Form_Field_Required", ErrorMessageResourceType = typeof(SharedResources))]
        public string NewContactRole { get; set; }
    }

    public class ProfileEscalationRulesForm
    {  
        public List<CaseRule> escalationRulesList { get; set; } = new List<CaseRule>();


        [Display(Name = "Profile_Step_Escalation_RuleList_Name", ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = "Form_Field_Required", ErrorMessageResourceType = typeof(SharedResources))]
        public string NewCaseStatusID { get; set; }
        public string NewCaseStatusName { get; set; }


        [Display(Name = "Profile_Step_Escalation_RuleList_Days", ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = "Form_Field_Required", ErrorMessageResourceType = typeof(SharedResources))]
        public int NewCaseStatusDays { get; set; }
    }

    public class Language
    {
        public string Key { get; set; }
        public string Description { get; set; } 
    }

    public class ProfileRole
    {
        public string Key { get; set; }
        public string Description { get; set; }
    }

   
}
