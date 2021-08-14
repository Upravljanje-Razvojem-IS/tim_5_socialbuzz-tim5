using AutoMapper;
using ForumService.Authorization;
using ForumService.DTO;
using ForumService.Logger;
using ForumService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Controllers
{
    /// <summary>
    /// Forum Kontroler izvrsava CRUD operacije nad podacima />.
    /// </summary>
    [Produces("application/json", "application/xml")]
    [Route("api/forum")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private readonly IForumService _forumService;
        private readonly ILoggerRepository<ForumController> logger;
        private readonly LinkGenerator linkGenerator;
        private readonly IAuthorization _authorizationService;
        private readonly IMapper mapper;

        public ForumController(IForumService forumService, ILoggerRepository<ForumController> loggerRepository,
                                IAuthorization authorization, IMapper mapper, LinkGenerator linkGenerator) {
            this._forumService = forumService;
            this._authorizationService = authorization;
            this.logger = loggerRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        /// <summary>
        /// Vraca sve postojece forume.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva Get All Forums
        /// GET 'https://localhost:44378/api/forum/' \
        ///     --header 'Authorization: Bearer URIS2021'
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <response code="200">Uspesno vracena lista svih foruma.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Nisu pronadjeni forumi ili ne postoji nijedna kreirani forum.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<ForumDTO>> GetAllForums([FromHeader] string key)
        {
            if (!_authorizationService.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            var forums = _forumService.GetAllForums();

            if (forums == null || forums.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "No forums found...");
            }

            logger.LogInformation("Successfully returned list of all forums.");

            return Ok(forums);
        }

        /// <summary>
        /// Vraca forume na osnovu prosledjenog ID-a.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva Get Forum By ID
        /// GET 'https://localhost:44378/api/forum/forumID/forumID' \
        ///     --header 'Authorization: Bearer URIS2021'
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="forumID">ID foruma</param>
        /// <response code="200">Uspesno vracen forum na osnovu ID-a.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Nije pronadjen nijedan forum sa zadatim ID-jem.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("forumID/{forumID}")]
        public ActionResult<ForumDTO> GetForumByID([FromHeader] string key, int forumID)
        {
            if (!_authorizationService.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            try
            {
                var forum = _forumService.GetForumByID(forumID);

                logger.LogInformation("Successfully returned forum on ForumID.");

                return Ok(forum);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Vraca forume na osnovu prosledjenog ID-a vlasnika foruma.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva Get Forum By Owner
        /// GET 'https://localhost:44378/api/forum/owner/ownerID' \
        ///     --header 'Authorization: Bearer URIS2021'
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="ownerID">ID posiljaoca poruke</param>
        /// <response code="200">Uspesno vracen forum na osnovu ID-a vlasnika foruma.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Nije pronadjen nijedan forum sa zadatim ID-jem vlasnika.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("owner/{ownerID}")]
        public ActionResult<ForumDTO> GetForumByOwner([FromHeader] string key, int ownerID)
        {
            if (!_authorizationService.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            try
            {
                var forum = _forumService.GetForumsByOwner(ownerID);

                logger.LogInformation("Successfully returned forum on ownerID.");

                return Ok(forum);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Kreira novi forum za razmenu poruka.
        /// </summary>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="newForum">Parametri foruma koji se kreira</param>
        /// 
        /// <returns></returns>
        /// <remarks>
        /// POST 'https://localhost:44378/api/forum/' \
        /// Primer zahteva za uspesno dodavanje nove ocene \
        ///  --header 'Authorization: Bearer URIS2021' \
        /// {     \
        ///  "forumID": 3, \
        ///  "userID": 3, \
        ///  "forumName": "Makeup", \
        ///  "forumDescription": "Forum about makeup" ,\
        ///  "logoLink": "linktologo", \
        ///  "isOpen": true \
        /// } \
        /// </remarks>
        /// <response code="201">Vraca kreiran forum.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="500">Greska na serveru.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<ForumDTO> CreateForum([FromHeader] string key, [FromBody] ForumDTO newForum)
        {
            if (!_authorizationService.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            try
            {

                var created = _forumService.CreateForum(newForum);


                string location = linkGenerator.GetPathByAction("GetForumByID", "Forum", new { forumID = created.ForumID });

                return Created(location, created);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating new forum: " + ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Vrši brisanje foruma na osnovu ID-ja foruma
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva za brisanje ocene
        /// DELETE 'https://localhost:44378/api/forum/forumID' \
        ///     --header 'Authorization: Bearer URIS2021' \
        ///     --param  'forumID = 3'
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="forumID">ID foruma koji se brise</param>
        /// <response code="204">Uspesno obrisan froum.</response>
        /// <response code="401" > Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Korisnik pokusava da obrise nepostojeci forum.</response>
        /// <response code="500">Greska na serveru.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{forumID}")]
        public IActionResult DeleteForum([FromHeader] string key, int forumID)
        {
            if (!_authorizationService.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            try
            {
                _forumService.DeleteForum(forumID);

                return NoContent();
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting forum: " + ex.Message);

                if (ex.Message.Contains("There is no forum with that ID!"))
                {
                    return StatusCode(StatusCodes.Status404NotFound, ex.Message);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting forum!");
            }
        }
    }
}
