using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMDataAccess
{
    public static class HcmDataAccessRegistration
    {
        public static void AddHcmDataAccess(this IServiceCollection services)
        {
            services.AddSingleton<ISqlDataAccess, SqlDataAccess>();

            services.AddSingleton<IReportsCaseData, ReportsCaseData>();
            services.AddSingleton<IFiltersData, FiltersData>();
            services.AddSingleton<ILoginData, LoginData>();
            services.AddSingleton<ISettingsData, SettingsData>();
            services.AddSingleton<IUsersData, UsersData>();
            services.AddSingleton<IUserRolesData, UserRolesData>();
        }
    }
}
