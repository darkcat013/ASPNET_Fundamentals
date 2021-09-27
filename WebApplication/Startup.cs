using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication
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
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            logger.LogInformation("Start");

            app.UseStaticFiles();
            app.Map("/map1", Map1);
            app.Map("/map2", Map2);
            app.Map("/custom", b => b.UseCustomMiddleware());

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Begin use  ");
                await next.Invoke();
                await context.Response.WriteAsync("  End use");
            });
            app.Run(async context => await context.Response.WriteAsync("Startup Run()"));

            logger.LogInformation("End");
        }

        private void Map1(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("Map1 Run()"));
        }
        private void Map2(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("Map2 Run()"));
        }
    }
}
