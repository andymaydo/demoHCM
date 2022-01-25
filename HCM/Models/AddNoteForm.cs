using HCMApi;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HCM.Resources;

namespace HCM.Models
{
    public class AddNoteForm
    {
        public string CaseSubject { get; set; }


        [Display(Name = "Cases_AddMessage_Note", ResourceType = typeof(SharedResources))]
        [Required(ErrorMessageResourceName = "Cases_AddMessage_Validation_Note", ErrorMessageResourceType = typeof(SharedResources))]
        public string Note { get; set; }

        
        public bool WithMatchReport { get; set; }
      
        public bool NotifyAll { get; set; }
      
        public List<CaseContactSelectable> ProfileParticipants { get; set; }
        
        public List<CaseContactSelectable> CaseParticipants { get; set; }
       
        public CaseOriginatorSelectable Originator { get; set; }

        
      
        [RegularExpression("^$|^.*@.*\\..*$", ErrorMessageResourceName = "Cases_AddMessage_Validation_Email", ErrorMessageResourceType = typeof(SharedResources))]
        public string ExtEmail1 { get; set; }
        public string ExtEmail2 { get; set; }
    }


    public class CaseContactSelectable : CaseContact
    {
        public bool Selected { get; set; }
    }

    public class CaseOriginatorSelectable : CaseOriginator
    {
        public bool Selected { get; set; }
    }
}
