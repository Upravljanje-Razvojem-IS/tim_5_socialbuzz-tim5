using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TheSocialBaz.Server.Data.Models;

namespace TheSocialBaz.Server.Infrastructure
{
    public static class CreateAdminTaskExtension
    {

        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var _userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            // Create a super user who will maintain the web app
            var poweruser = new User
            {
                UserName = configuration["ApplicationSettings:UserName"],
                Email = configuration["ApplicationSettings:UserEmail"],
            };

            string userPWD = configuration["ApplicationSettings:UserPassword"];
            var _user = await _userManager.FindByEmailAsync(configuration["ApplicationSettings:AdminUserEmail"]);

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
