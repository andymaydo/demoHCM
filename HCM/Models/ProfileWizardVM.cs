using AutoMapper;
using HCMApi;
using HCMApi.Models;
using System.Threading.Tasks;

namespace HCM.Models
{
    public class ProfileWizardVM
    {
        public bool IsCompleted { get; set; }
        public CMSProfileModel CurrentProfile { get; set; }
        public ProfileGenInfoForm GenInfoForm { get; set; } 
        public ProfileContactsForm ContactsForm { get; set; }
        public ProfileEscalationContactsForm EscalationContactsForm { get; set;}
        public ProfileEscalationRulesForm EscalationRulesForm { get; set; }

        private ICMSProfile _profileService;
        private IMapper _mapper;

        public ProfileWizardVM(ICMSProfile profileService, IMapper mapper)
        {
            IsCompleted = false;
            CurrentProfile = new CMSProfileModel();
            GenInfoForm = new ProfileGenInfoForm();
            ContactsForm = new ProfileContactsForm();
            EscalationContactsForm = new ProfileEscalationContactsForm();
            EscalationRulesForm = new ProfileEscalationRulesForm();

            _profileService = profileService;
            _mapper = mapper;
        }



        public async Task AddNewProfileAsync(int CreatorId, string prName, string prDescr, string prLang)
        {
            CurrentProfile =  await _profileService.CreateNew(CreatorId, 1, prName, prDescr, prLang);
            IsCompleted = false;
            InitForms();
        }

        public async Task CopyProfileAsync(int sourceId, int creatorId)
        {
            CurrentProfile = await _profileService.ProfilCopy(sourceId, creatorId);
            IsCompleted = (CurrentProfile.profileParticipants?.Count > 0);
            InitForms();
        }

        public async Task StatusUpdate(int statusId, int modifierId)
        {
            CurrentProfile.ModifiedBy = modifierId;
            CurrentProfile.profileStatusID = statusId;
            await _profileService.UpdateStatusAsync(CurrentProfile);
        }

        public  void LoadProfile(int profileId)
        {
            CurrentProfile = _profileService.Load(profileId);
            IsCompleted = (CurrentProfile.profileParticipants?.Count > 0);
            InitForms();
        }

        
        public async Task SaveGenInfo(int modifierId)
        {
            CurrentProfile.ModifiedBy = modifierId;
            _mapper.Map(GenInfoForm, CurrentProfile);
            await _profileService.UpdateGeneralInfoAsync(CurrentProfile);
        }

        public async Task SaveContacts(int modifierId)
        {
            CurrentProfile.ModifiedBy = modifierId;
            _mapper.Map(ContactsForm, CurrentProfile);
            await _profileService.UpdateParticipantsAsync(CurrentProfile);
        }


        public async Task SaveEscalation(int modifierId)
        {
            CurrentProfile.ModifiedBy = modifierId;
            _mapper.Map(EscalationContactsForm, CurrentProfile);
            _mapper.Map(EscalationRulesForm, CurrentProfile);

            await _profileService.UpdateEscalationAsync(CurrentProfile);
        }

        public void InitForms()
        {
            _mapper.Map(CurrentProfile, GenInfoForm);
            _mapper.Map(CurrentProfile, ContactsForm);
            _mapper.Map(CurrentProfile, EscalationContactsForm);
            _mapper.Map(CurrentProfile, EscalationRulesForm);
        }
    }
}
