using HCM.Data;
using HCMApi;
using HCMDataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
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
            services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
            services.AddSingleton<IReportsCaseData, ReportsCaseData>();
            services.AddSingleton<IFiltersData, FiltersData>();
            services.AddSingleton<ICase, Case>();
            services.AddSingleton<ICaseContact, CaseContact>();
            services.AddSingleton<ICMSAPI, CMSAPI>();
            services.AddSingleton<ILoginData, LoginData>();
            services.AddScoped<ILocalUserData, LocalUserData>();
            services.AddSingleton<ISettingsData, SettingsData>();
            services.AddSingleton<IUsersData, UsersData>();
            services.AddSingleton<IUserRolesData, UserRolesData>();
            services.AddSingleton<ICMSProfile, CMSProfile>();

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
