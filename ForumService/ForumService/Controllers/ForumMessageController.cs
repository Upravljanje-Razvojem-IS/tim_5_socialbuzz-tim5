using ForumService.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/forumMessage")]
    [ApiController]
    public class ForumNessageController : ControllerBase
    {



        /// <summary>
        /// Vraca sve postojece ocene na objavama.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva Get All Ratings
        /// GET 'https://localhost:44303/api/forumMessage/' \
        ///     --header 'Authorization: Bearer URIS2021'
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <response code="200">Uspesno vracena lista svih poruka u forum grupi.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Nisu pronadjene ocena ili ne postoji nijedna ocena korisnika na objavama.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<ForumMessageDTO>> GetAllForumMessages([FromHeader] string key) { 
            


        }
    }
}
