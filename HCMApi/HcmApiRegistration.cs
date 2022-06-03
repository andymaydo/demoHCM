using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMApi
{
    public static class HcmApiRegistration
    {
        public static void AddHcmApi(this IServiceCollection services)
        {
            services.AddSingleton<ICase, Case>();
            services.AddSingleton<ICaseEvent, CaseEvent>();
            services.AddSingleton<ICMSEvent, CMSEvent>();
            services.AddSingleton<ICaseDoc, CaseDoc>();
            //services.AddSingleton<ICaseContact, CaseContact>();
            services.AddSingleton<ICMSAPI, CMSAPI>();
            
        }
    }
}
