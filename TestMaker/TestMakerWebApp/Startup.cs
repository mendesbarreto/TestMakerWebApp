using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TestMakerWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            
            EnableStaticFiles(app, env);
            
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        private void EnableStaticFiles(IApplicationBuilder app, IHostingEnvironment environment)
        {
            // This line will call the Static files middleware to
            // let me use the static file stored on web root folder
            // such as CSS, JS and Images
            
            if (environment.IsDevelopment())
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    OnPrepareResponse = (context) =>
                    {
                        // Disable caching for all static files. 
                        context.Context.Response.Headers["Cache-Control"] = "no-cache, no-store";
                        context.Context.Response.Headers["Pragma"] = "no-cache";
                        context.Context.Response.Headers["Expires"] = "-1";
                    }
                });
            }
            else
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    OnPrepareResponse = (context) =>
                    {
                        // Disable caching for all static files. 
                        context.Context.Response.Headers["Cache-Control"] = 
                            Configuration["StaticFiles:Headers:Cache-Control"];
                        context.Context.Response.Headers["Pragma"] = 
                            Configuration["StaticFiles:Headers:Pragma"];
                        context.Context.Response.Headers["Expires"] = 
                            Configuration["StaticFiles:Headers:Expires"];
                    }
                });
            }
        }
    }
}