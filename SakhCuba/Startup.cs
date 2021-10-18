﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SakhCuba.Models;
//using Pomelo.EntityFrameworkCore.MySql;
//using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using SakhCuba.Middleware;
using static SakhCuba.Middleware.RequestTimeMiddleware;

namespace SakhCuba
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<SakhCubaContext>(options =>
                options.UseSqlServer(connection));
            //services.AddDbContextPool<SakhCubaContext>(options => options
            //    .UseMySql(
            //    Configuration.GetConnectionString("MariaDB"),
            //    mySqlOptions => mySqlOptions.ServerVersion(new Version(10,6,4), ServerType.MariaDb)));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{action=Index}",
                    defaults: new { controller = "Home"});
            });
        }
    }
}
