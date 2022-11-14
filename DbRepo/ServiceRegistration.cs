using Application.Interfaces;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbRepo
{
    public static class ServiceRegistration
    {
        public static void AddInfraDb(this IServiceCollection services)
        {
            services.AddSingleton<ILoginData, LoginData>();
            services.AddSingleton<IUsersData, UsersData>();
            services.AddSingleton<ISettingsData, SettingsData>();
            services.AddSingleton<ICMSProfile, CMSProfile>();
            services.AddSingleton<IUserRolesData, UserRolesData>();
            services.AddSingleton<IReportsCaseData, ReportsCaseData>();
            services.AddSingleton<IFiltersData, FiltersData>();
            //services.AddSingleton<ICaseEvent, CaseEventService>();
            services.AddSingleton<ICMSAPI, CMSAPI>();

            services.AddSingleton<ILookUpTables, LookUpTableService>();

        }
    }
}
