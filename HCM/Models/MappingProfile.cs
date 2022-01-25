﻿
using AutoMapper;
using HCM.Models;
using HCMApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCM.Models
{
    public class MappingProfile : Profile
    {       
        public MappingProfile()
        {
            CreateMap<CaseContactSelectable, CaseContact>();
            CreateMap<CaseContact, CaseContactSelectable>();

        }
    }

}
