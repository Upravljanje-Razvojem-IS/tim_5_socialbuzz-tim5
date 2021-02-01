using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TheSocialBaz.Server.Controllers
{
    public class HomeController : ApiController
    {
        /// <summary>
        /// Home page for the SocialBaz
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Member, Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Get()
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

            return Ok("Works");
        }
    }
}
