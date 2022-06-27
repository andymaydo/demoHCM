using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace VgsRepo
{
    public static class ServiceRegistration
    {
        public static void AddInfraVgs(this IServiceCollection services)
        {
            services.AddSingleton<ILicLimits, LicLimits>();

        }
    }
}
