using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestTask.Core.Contracts;
using TestTask.DAL.DataBase.Initializer;
using TestTask.DAL.Repositories;
using TestTask.Services.Services;
using TestTask.Services.Services.Contracts;
using TestTask.Web.AppStart;

namespace TestTask.Web
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
            services.AddDatabaseContext(Configuration);
            services.AddAutoMapperCustom();
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddControllers();

            services.AddOpenApiDocument(options =>
            {
                options.Title = "PromoCode Factory API Doc";
                options.Version = "1.0";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbInitializer initializer)
        {
            app.UseSwagger();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            else
            {
                app.UseHsts();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3(x =>
            {
                x.DocExpansion = "list";
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            initializer.Initialize();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
