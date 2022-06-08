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

            
            
            
            
            
            
        }
    }
}
