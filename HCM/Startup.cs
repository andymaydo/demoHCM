using HCM.Data;
using HCMApi;
using HCMDataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.Blazor;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;

namespace HCM
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            services.AddTransient<ISqlDataAccess, SqlDataAccess>();
            services.AddTransient<IReportsCaseData, ReportsCaseData>();
            services.AddTransient<IFiltersData, FiltersData>();
            services.AddTransient<ICase, Case>();
            services.AddTransient<ICaseContact, CaseContact>();
            services.AddTransient<ICMSAPI, CMSAPI>();
            services.AddTransient<ILoginData, LoginData>();
            services.AddTransient<ILocalUserData, LocalUserData>();
            services.AddTransient<ISettingsData, SettingsData>();
            services.AddTransient<IUsersData, UsersData>();
            services.AddTransient<IUserRolesData, UserRolesData>();

            services.AddBlazoredLocalStorage();
            services.AddOptions();
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, LocalAuthenticationStateProvider>();

            services.AddSyncfusionBlazor();

        }

        private RequestLocalizationOptions GetLocalizationOptions()
        {
            var cultures = Configuration.GetSection("Cultures")
                .GetChildren().ToDictionary(x => x.Key, x => x.Value);

            var supportedCultures = cultures.Keys.ToArray();

            var localizationOptions = new RequestLocalizationOptions()
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            return localizationOptions;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzY2NDYxQDMxMzgyZTMzMmUzMFZpZTdpa1NYK1c3blZLRlMwa0FCMldaK2grcUhSemVBY1hoaDZSZlc1ME09");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRequestLocalization(GetLocalizationOptions());

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
