using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TheSocialBaz.Server.Data;
using TheSocialBaz.Server.Infrastructure;

namespace TheSocialBaz.Server
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
            // ServiceCollectionExtensions
            // ConfigurationExtensions parameters for functions
            services
              .AddDbContext<ApplicationDbContext>(options => options
                  .UseSqlServer(Configuration.GetDefaultConnectionString()))
                  .AddIdentity()
                  .AddJwtAuthentication(services.GetAppSettings(Configuration))
                  .AddSwagger()
                  .AddControllers();
            
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // ApplicationBuilderExtensions
            app
                .UseSwaggerUI()
                .UseRouting()
                .UseCors(x => x
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader())
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            })
                .ApplyMigrations();

            // Create the super user who will maintain the web app
            CreateAdminTaskExtension.CreateRoles(serviceProvider, Configuration).Wait();
        }
    }
}
