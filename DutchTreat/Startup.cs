using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DutchTreat.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DutchTreat
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DutchContext>();
            //EF Configuration
            services.AddDbContextPool<DutchContext>(
                cfg => cfg.UseSqlServer(_config.GetConnectionString("DutchConnectionString")));


            services.AddTransient<IMailService, NullMailService>();
            //Support for real mail service
            //We can add 3 types to add services
            //1. services.AddTransient  ==It dont have any data on themselfs, just messgae to do things,
            //2. services.AddScoped ==It is for adding services that are a liitle bit more expensive to create.
            //They are actually kept on different scopes but default scope is ASP.net core project.
            //3.services.AddTransient   ==Needed for the services that are created once and are kept for the lifetime of the server being up.

            services.AddMvc();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();

            //app.UseMvc(cfg =>
            //{
            //    cfg.MapRoute("Default", "{controller}/{action}/{id?}",
            //        new { Controller = "App", Action = "Index" });
            //});

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context => 
                //{
                //    await context.Response.WriteAsync("<html><body><h1>Hello World!</h1></body></html>");
                //});
                endpoints.MapControllerRoute(
                        "default", "{controller=App}/{action=Index}"
                    );
            });


        }
    }
}
