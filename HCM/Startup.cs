
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
using HCM.Options;
using System.Configuration;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Localization;
using blazorInputs;
using BlazorDownloadFile;
using System;

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
            var cmsSettings = new CMSSettings();
            Configuration.Bind(nameof(cmsSettings), cmsSettings);
            services.AddSingleton(cmsSettings);

            services.AddControllers();
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddSyncfusionBlazor();
            //services.Configure<RequestLocalizationOptions>(options =>
            //{
            //    // define the list of cultures your app will support
            //    var supportedCultures = new List<CultureInfo>()
            //    {
            //        new CultureInfo("de")
            //    };
            //    // set the default culture
            //    options.DefaultRequestCulture = new RequestCulture("de");
            //    options.SupportedCultures = supportedCultures;
            //    options.SupportedUICultures = supportedCultures;
            //    options.RequestCultureProviders = new List<IRequestCultureProvider>() {
            //     new QueryStringRequestCultureProvider() // Here, You can also use other localization provider
            //    };
            //});
            //services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));


            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
            services.AddSingleton<IReportsCaseData, ReportsCaseData>();
            services.AddSingleton<IFiltersData, FiltersData>();
            services.AddSingleton<ICase, Case>();
            services.AddSingleton<ICaseEvent, CaseEvent>();
            services.AddSingleton<ICMSEvent, CMSEvent>();
             services.AddSingleton<ICaseDoc, CaseDoc>();
            services.AddSingleton<ICaseContact, CaseContact>();
            services.AddSingleton<ICMSAPI, CMSAPI>();
            services.AddSingleton<ILoginData, LoginData>();
            services.AddSingleton<ISettingsData, SettingsData>();
            services.AddSingleton<IUsersData, UsersData>();
            services.AddSingleton<IUserRolesData, UserRolesData>();
            services.AddSingleton<ICMSProfile, CMSProfile>();

            services.AddSingleton<AppState, AppState>();

            services.AddBlazoredLocalStorage();
            services.AddOptions();
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, LocalAuthenticationStateProvider>();
            services.AddBlazorDownloadFile(ServiceLifetime.Scoped);
            

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

            AppDomain.CurrentDomain.SetData("ContentRootPath", env.ContentRootPath);
            AppDomain.CurrentDomain.SetData("WebRootPath", env.WebRootPath);
        }
    }
}
