using HCMApi;
using HCMApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCM.Models
{
    public class CaseActionVM
    {
        public Case CurrentCase { get; set; }

        public List<CaseContact> CaseParticipants { get; set; } = new List<CaseContact>();
        public List<CaseContact> ProfileParticipants { get; set; } = new List<CaseContact>();
        public string ExtEmail1 { get; set; }
        public string ExtEmail2 { get; set; }


        protected CMSProfileModel ProfileModel { get; set; }
        public async Task InitializeAsync(Case myCase, CMSProfileModel myProfile)
        {
            CurrentCase = myCase;
            CaseParticipants = CurrentCase.ParticipantsAsList;

        }
        

    }
}
