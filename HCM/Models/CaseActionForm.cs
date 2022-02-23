using HCMApi;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HCM.Resources;
using System.Xml.Serialization;

namespace HCM.Models
{
    public class CaseActionForm
    {
        public string CaseSubject { get; set; }


        public bool WithMatchReport { get; set; }

        public bool NotifyAll { get; set; }

        public List<CaseContactSelectable> ProfileParticipants { get; set; }

        public List<CaseContactSelectable> CaseParticipants { get; set; }

        public CaseOriginatorSelectable Originator { get; set; }

    }

    public class AddNoteForm : CaseActionForm
    {

        [Display(Name = "Cases_AddMessage_Note", ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = "Cases_AddMessage_Validation_Note", ErrorMessageResourceType = typeof(SharedResources))]
        public string Note { get; set; }


        [RegularExpression("^$|^.*@.*\\..*$", ErrorMessageResourceName = "Cases_AddMessage_Validation_Email", ErrorMessageResourceType = typeof(SharedResources))]
        public string ExtEmail1 { get; set; }
    }

    public class ChangeStatusForm : CaseActionForm
    {
        [Display(Name = "Cases_ChangeStatus_Note", ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = "Cases_ChangeStatus_Validation_Note", ErrorMessageResourceType = typeof(SharedResources))]
        public string Note { get; set; }

        public int CurrentStatusId { get; set; }

        [Required(ErrorMessageResourceName = "Cases_ChangeStatus_Validation_Status", ErrorMessageResourceType = typeof(SharedResources))]
        public string NewStatusId { get; set; }
    }

    public class AddParticipantForm
    {
        public string NewCaseParticipantId { get; set; }

        public List<CaseContactSelectable> ProfileParticipants { get; set; }
        public List<CaseContactSelectable> EscalationParticipants { get; set; }
        public List<CaseContactSelectable> CaseParticipants { get; set; } 
    }
}
