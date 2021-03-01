using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Disabling an account. Only an admin has the access to this controller.
        /// </summary>
        /// <param name="email">Email of an account is needed to disable it.</param>
        /// <returns>Returns a message.</returns>
        /// <response code="200">Returns a message that the account with the sent email has been disabled.</response>
        /// <response code="400">Found the user but couldn't disable the account.</response>
        /// <response code="404">Returns 404 if the user is not found.</response>
        [HttpPost]
        [Route(nameof(DisableAccount))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Enabling an account. Only an admin has the access to this controller.
        /// </summary>
        /// <param name="email">Email of an account is needed to enable it.</param>
        /// <returns>Returns a message.</returns>
        /// <response code="200">Returns a message that the account with the sent email has been enabled.</response>
        /// <response code="400">Found the user but couldn't enable the account.</response>
        /// <response code="404">Returns 404 if the user is not found.</response>
        [HttpPost]
        [Route(nameof(EnableAccount))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
