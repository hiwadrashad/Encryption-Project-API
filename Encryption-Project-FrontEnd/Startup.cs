using Encryption_Project_LIB.DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encryption_Project_FrontEnd
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
            services.AddScoped<Encryption_Project_LIB.Interfaces.IAuthentication, Encryption_Project_API.Auth.Authentication>();
            services.AddScoped<Encryption_Project_LIB.Interfaces.IHashAndSalting, Encryption_Project_LIB.Encryption.HashingAndSalting>();
            services.AddScoped<Encryption_Project_LIB.Interfaces.IConverter, Encryption_Project_LIB.BLL.Converters>();
            services.AddScoped<Encryption_Project_LIB.Interfaces.IHashAndSalting, Encryption_Project_LIB.Encryption.HashingAndSalting>();
            services.AddScoped<Encryption_Project_API.Repositories.IEncryptedUserService, Encryption_Project_API.Repositories.MockingRepository<EncryptedUser>>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Authentication}/{action=Login}/{id?}");
            });
        }
    }
}
