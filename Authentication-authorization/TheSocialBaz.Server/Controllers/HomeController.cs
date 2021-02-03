using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

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

           // var claim = currentUser.Claims.First(c => c.Type == ClaimTypes.Role).Value;
            
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
                return Ok("Corporate");
            }

            return Ok("Works");
        }
    }
}
