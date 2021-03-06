using Encryption_Project_LIB.DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encryption_Project_API
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

            services.AddControllers();
            services.AddScoped<Encryption_Project_LIB.Interfaces.IAuthentication, Encryption_Project_API.Auth.Authentication>();
            services.AddScoped<Encryption_Project_LIB.Interfaces.IHashAndSalting, Encryption_Project_LIB.Encryption.HashingAndSalting>();
            services.AddScoped<Encryption_Project_LIB.Interfaces.IConverter, Encryption_Project_LIB.BLL.Converters>();
            services.AddScoped<Encryption_Project_LIB.Interfaces.IHashAndSalting, Encryption_Project_LIB.Encryption.HashingAndSalting>();
            services.AddScoped<Encryption_Project_API.Repositories.IEncryptedUserService, Encryption_Project_API.Repositories.MockingRepository<EncryptedUser>>();
            services.AddScoped<Encryption_Project_LIB.Interfaces.IRfc2898Encryption, Encryption_Project_LIB.Encryption.Rfc2898Encryption>();
            services.AddDbContext<Encryption_Project_API.DataBases.EncryptedUsersDatabase>(options =>
            options.UseSqlServer(
            Configuration.GetConnectionString("DefaultConnection")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Encryption_Project_API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Encryption_Project_API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<MiddleWare.JWTMiddleWare>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
