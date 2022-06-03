
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
using BlazorDownloadFile;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using AutoMapper;
using HCM.Models;
using HCM.Resources;
using AliasManager.Interfaces;
using AliasManager.Services;
using DbRepo;

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
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpContextAccessor();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                  .AddCookie(cookieOptions =>
                  {
                      cookieOptions.Cookie.Name = "HCM_ClaimIdentity";
                      cookieOptions.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                      cookieOptions.SlidingExpiration = true;
                      cookieOptions.LoginPath = "/Login";
                      cookieOptions.Cookie.IsEssential = true;
                  });

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization(Options =>
            {
                Options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    var assemblyName = new AssemblyName(typeof(SharedResources).GetTypeInfo().Assembly.FullName);
                    return factory.Create(nameof(SharedResources), assemblyName.Name);
                };
            });
            services.AddSingleton<CommonLocalizationService>();

            services.AddAutoMapper(typeof(MappingProfile));
            
            services.AddSyncfusionBlazor();                     
            services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));


            services.AddHcmDataAccess();
            services.AddInfraDb();
            services.AddHcmApi();

            services.AddTransient<ProfileWizardVM>();

            //AliasManager
            services.AddAmHcmForVgs(Configuration.GetSection("ConnectionStrings:VGS").Value);
            

            services.AddOptions();
            services.AddAuthorizationCore();            
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
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzY2NDYxQDMxMzgyZTMzMmUzMFZpZTdpa1NYK1c3blZLRlMwa0FCMldaK2grcUhSemVBY1hoaDZSZlc1ME09");
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBMAY9C3t2VVhhQlFaclhJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRdkFiWH5ecnNXR2FfUEQ=;NjEyODI3QDMyMzAyZTMxMmUzMGpSeERCWVl4Y2dUYlhSOEdLTFZtNzZlaTNzODF0TFcxeEJyWXFvMFR5dE09");
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
            app.UseAuthentication();
            app.UseAuthorization();

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
