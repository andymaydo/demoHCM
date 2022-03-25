
using AutoMapper;
using AutoMapper.Internal;
using HCM.Models;
using HCMApi;
using HCMApi.Models;
using HCMModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HCMModels.CaseModel;

namespace HCM.Models
{
    public class MappingProfile : Profile
    {       
        public MappingProfile()
        {
            CreateMap<CaseContactSelectable, CaseContact>();
            CreateMap<CaseContact, CaseContactSelectable>();

            CreateMap<CaseOriginator, CaseOriginatorSelectable>();
            CreateMap<CaseOriginatorSelectable, CaseOriginator>();

            CreateMap<UsersModel, EditUserForm>();
            CreateMap<UsersModel, PassUserForm>();

            CreateMap<UserRolesModel, UserRolesForm>();

            CreateMap<ProfileGenInfoForm, CMSProfileModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CMSProfileModel, ProfileGenInfoForm>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<ProfileContactsForm, CMSProfileModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CMSProfileModel, ProfileContactsForm>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            
            CreateMap<ProfileEscalationContactsForm, CMSProfileModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CMSProfileModel, ProfileEscalationContactsForm>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            CreateMap<ProfileEscalationRulesForm, CMSProfileModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CMSProfileModel, ProfileEscalationRulesForm>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));



            CreateMap<ProfileEscalationContactsForm, ProfileEscalationContactsForm>();
            CreateMap<ProfileEscalationRulesForm, ProfileEscalationRulesForm>();

            CreateMap<SettingsModel, SettingsSMTPForm>().ReverseMap();

        }
    }

}
