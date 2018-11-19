using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ArandukApp.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;

namespace ArandukApp.API
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
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "ArandukApp";
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            }).AddCookie("ArandukApp").AddGoogle(options =>
            {
                options.ClientId = "601559891225-cu2vb8rkcjvqe5bafv5v5su83ki4dtpl.apps.googleusercontent.com";
                options.ClientSecret = "OyNVqojrX9E1f-Zsh6-_RD9s";
                options.CallbackPath = new PathString("/signin-google");
                options.SignInScheme = "ArandukApp";
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<ArandukAppContext>(options =>
                options.UseSqlite("Data Source=ArandukApp.db"));

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                       Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "arandukapp", "files")),
                RequestPath = "/arandukapp/files",
                EnableDirectoryBrowsing = true
            });

            // app.UseDefaultFiles();
            // app.UseStaticFiles();
            app.UseFileServer();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
