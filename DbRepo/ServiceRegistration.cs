using Application.Interfaces;
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
        }
    }
}
