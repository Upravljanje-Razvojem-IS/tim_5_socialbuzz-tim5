using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TheSocialBaz.Server.Controllers
{
    public class HomeController : ApiController
    {
        /// <summary>
        /// Home page for the SocialBaz
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Member")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Get()
        {
            return Ok("Works");
        }
    }
}
