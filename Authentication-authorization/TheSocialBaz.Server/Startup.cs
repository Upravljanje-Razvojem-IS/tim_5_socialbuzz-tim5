using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TheSocialBaz.Server.Data;
using TheSocialBaz.Server.Data.Models;
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

            CreateRoles(serviceProvider).Wait();
        }

        // Have to put this in a separate file
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var _userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            // Create a super user who will maintain the web app
            var poweruser = new User
            {
                UserName = Configuration["ApplicationSettings:UserName"],
                Email = Configuration["ApplicationSettings:UserEmail"],
            };

            string userPWD = Configuration["ApplicationSettings:UserPassword"];
            var _user = await _userManager.FindByEmailAsync(Configuration["ApplicationSettings:AdminUserEmail"]);

            if (_user == null)
            {
                var createPowerUser = await _userManager.CreateAsync(poweruser, userPWD);
                if (createPowerUser.Succeeded)
                {
                    // Tie the new user to the role
                    await _userManager.AddClaimAsync(poweruser, new Claim(ClaimTypes.Role, "Admin"));
                }
            }
        }
    }
}
