using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TheSocialBaz.Server.Data.Models;

namespace TheSocialBaz.Server.Controllers
{
    public class HomeController : ApiController
    {
        private readonly UserManager<User> _userManager;
        public HomeController(
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Home page for authenticated users.
        /// </summary>
        /// <returns>Home page.</returns>
        /// <response code="200">Returns the home page if the user has a valid token.</response>
        /// <response code="401">Unauthorized access, token is not valid.</response>
        [Authorize(Roles = "Member, Admin, Corporate")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<object>> Get()
        {
            var currentUser = HttpContext.User;
            
            if (currentUser.IsInRole("Member"))
            {
                return Ok("Member");
            }
            else if (currentUser.IsInRole("Admin"))
            {
                return Ok("Admin");
            }
            else if (currentUser.IsInRole("Corporate"))
            {
                var corporateHolderId = currentUser.FindFirstValue("Claim.UserId");
                var corporateHolder = await _userManager.FindByIdAsync(corporateHolderId);
                var corporateHolderClaims = await _userManager.GetClaimsAsync(corporateHolder);

                // Get a claim
                // This is how I would access properties of the corporate holder/owner
                var corporationName = currentUser.FindFirstValue(ClaimTypes.Name);
                var corporateHolderName = corporateHolderClaims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();

                return Ok("Corporation name: " + corporationName.ToString() +
                    " " + "Corporation holder name: " + corporateHolderName.ToString());
            }

            return Ok("Works");
        }
    }
}
