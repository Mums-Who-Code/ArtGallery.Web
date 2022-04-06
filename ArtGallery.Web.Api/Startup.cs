// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using ArtGallery.Web.Api.Brokers.API;
using ArtGallery.Web.Api.Models.Configurations;
using RESTFulSense.Clients;

namespace ArtGallery.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            AddHttpClient(services);
            AddRootDirectory(services);
            services.AddScoped<IApiBroker, ApiBroker>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private static void AddRootDirectory(IServiceCollection services)
        {
            services.AddRazorPages(options =>
                options.RootDirectory = "/Views/Pages");
        }

        private void AddHttpClient(IServiceCollection services)
        {
            services.AddHttpClient<IRESTFulApiFactoryClient, RESTFulApiFactoryClient>(Client =>
            {
                LocalConfigurations localConfigurations = Configuration.Get<LocalConfigurations>();
                String apiUrl = localConfigurations.ApiConfigurations.Url;
                Client.BaseAddress = new Uri(apiUrl);
            });
        }
    }
}