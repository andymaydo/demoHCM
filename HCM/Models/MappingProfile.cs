
using AutoMapper;
using HCM.Models;
using HCMApi;
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

        }
    }

}
