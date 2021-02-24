using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSocialBaz.Server.Data.Models;

namespace TheSocialBaz.Server.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : ApiController
    {
        private readonly UserManager<User> _userManager;
        public AdminController(
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [Route(nameof(DisableAccount))]
        public async Task<ActionResult> DisableAccount(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound("User is not found.");
            }

            // Disable the user
            user.IsDisabled = true;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok("Account with email " + email + " has been disabled.");
            }
            else
                return BadRequest("Couldn't update the user.");
        }

        [HttpPost]
        [Route(nameof(EnableAccount))]
        public async Task<ActionResult> EnableAccount(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound("User is not found.");
            }

            // Enable the user
            user.IsDisabled = false;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok("Account with email " + email + " has been enabled.");
            }
            else
                return BadRequest("Couldn't update the user.");
        }
    }
}
