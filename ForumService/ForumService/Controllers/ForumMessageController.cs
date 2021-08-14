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
    [Produces("application/json", "application/xml")]
    [Route("api/forumMessage")]
    [ApiController]
    public class ForumNessageController : ControllerBase
    {
        private readonly IForumMessageService _forumMessageService;
        private readonly ILoggerRepository<ForumNessageController> logger;
        private readonly LinkGenerator linkGenerator;
        private readonly IAuthorization _authorizationService;
        private readonly IMapper mapper;

        public ForumNessageController(IForumMessageService forumMessageService, ILoggerRepository<ForumNessageController> loggerRepository,
                                IAuthorization authorization, LinkGenerator linkGenerator,IMapper mapper)
        {
            this._forumMessageService = forumMessageService;
            this._authorizationService = authorization;
            this.logger = loggerRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        /// <summary>
        /// Vraca sve postojece poruke u forumima.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva Get All Forum Messages
        /// GET 'https://localhost:44378/api/forumMessage/' \
        ///     --header 'Authorization: Bearer URIS2021'
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <response code="200">Uspesno vracena lista svih poruka na forumima.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Nisu pronadjene poruke ili ne postoji nijedna kreirana poruka.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<ForumMessageDTO>> GetAllForumMessages([FromHeader] string key)
        {
            if (!_authorizationService.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            var forumMess = _forumMessageService.GetAllForumMessages();

            if (forumMess == null || forumMess.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "No forum messages found...");
            }

            logger.LogInformation("Successfully returned list of all messages.");

            return Ok(forumMess);
        }

        /// <summary>
        /// Vraca forum poruke na osnovu prosledjenog ID-a.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva Get Forum Message By ID
        /// GET 'https://localhost:44378/api/forumMessage/forumMessageID/forumMessageID' \
        ///     --header 'Authorization: Bearer URIS2021'
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="forumMessageID">ID foruma</param>
        /// <response code="200">Uspesno vracene forum poruke na osnovu ID-a.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Nije pronadjen nijedna forum poruka sa zadatim ID-jem.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("forumMessageID/{forumMessageID}")]
        public ActionResult<ForumMessageDTO> GetForumMessageByID([FromHeader] string key, int forumMessageID)
        {
            if (!_authorizationService.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            try
            {
                var forumMess = _forumMessageService.GetForumMessageByID(forumMessageID);

                logger.LogInformation("Successfully returned forum message on message ID.");

                return Ok(forumMess);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Vraca forum poruke na osnovu prosledjenog ID-a foruma.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva Get Forum Message By ID
        /// GET 'https://localhost:44378/api/forumMessage/forumMessageID/forumMessageID' \
        ///     --header 'Authorization: Bearer URIS2021'
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="forumID">ID foruma</param>
        /// <response code="200">Uspesno vracene forum poruke na osnovu ID-a.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Nije pronadjen nijedna forum poruka sa zadatim ID-jem.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("forum/{forumID}")]
        public ActionResult<ForumMessageDTO> GetForumMessagesByForumID([FromHeader] string key, int forumID)
        {
            if (!_authorizationService.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            try
            {
                var forumMess = _forumMessageService.GetForumMessagesByForumID(forumID);

                logger.LogInformation("Successfully returned forum message on forum ID.");

                return Ok(forumMess);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Vraca forum poruke na osnovu prosledjenog naziva poruke.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva Get Forum Message By Title
        /// GET 'https://localhost:44378/api/forumMessage/title/title' \
        ///     --header 'Authorization: Bearer URIS2021'
        ///     --title primer "This is first message in forum...."
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="title">Naziv poruke</param>
        /// <response code="200">Uspesno vracena forum poruka na osnovu naslova.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Nije pronadjena nijedna forum poruka sa zadatim naslovom.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("title/{title}")]
        public ActionResult<ForumMessageDTO> GetForumMessageByTitle([FromHeader] string key, String title)
        {
            if (!_authorizationService.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            try
            {
                var forumMess = _forumMessageService.GetForumMessageByTitle(title);

                logger.LogInformation("Successfully returned forum message on title.");

                return Ok(forumMess);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Vraca forum poruke na osnovu prosledjenog ID-ja posiljaoca.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva Get Forum Message By Sender
        /// GET 'https://localhost:44378/api/forumMessage/sender/senderID' \
        ///     --header 'Authorization: Bearer URIS2021'
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="senderID">ID posiljaoca</param>
        /// <response code="200">Uspesno vracena forum poruka na osnovu ID-ja posiljaoca.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Nije pronadjena nijedna forum poruka sa zadatim ID-jem.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("sender/{senderID}")]
        public ActionResult<ForumMessageDTO> GetForumMessageByTitle([FromHeader] string key, int senderID)
        {
            if (!_authorizationService.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            try
            {
                var forumMess = _forumMessageService.GetForumMessagesBySender(senderID);

                logger.LogInformation("Successfully returned forum message on sender ID.");

                return Ok(forumMess);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Kreira novu poruku na forumu.
        /// </summary>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="newForumMessage">Parametri poruke koji se kreira</param>
        /// 
        /// <returns></returns>
        /// <remarks>
        /// POST 'https://localhost:44378/api/forumMessage/' \
        /// Primer zahteva za uspesno dodavanje nove ocene \
        ///  --header 'Authorization: Bearer URIS2021' \
        /// {     \
        ///  "forumID": 1, \
        ///  "senderID": 3, \
        ///  "title": "Makeup", \
        ///  "body": "Forum about makeup" \
        /// } \
        /// </remarks>
        /// <response code="201">Vraca kreiranu forum poruku.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="500">Greska na serveru.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<ForumMessageDTO> CreateForumMessage([FromHeader] string key, [FromBody] ForumMessageDTO newForumMessage)
        {
            if (!_authorizationService.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }
             
            var newmess = newForumMessage;

            if (newmess.ForumID == 0 || newmess.Body == "" || _forumMessageService.GetForumMessagesByForumID(newmess.ForumID) == null) {
                return StatusCode(StatusCodes.Status400BadRequest, "Unvalid message! Check your data!");
            }

            try
            {

                var created = _forumMessageService.CreateForumMessage(newForumMessage);


                string location = linkGenerator.GetPathByAction("GetForumMessageByID", "ForumMessage", new { forumMessageID = created.ForumMessageID });

                return Created(location, created);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating new forum message: " + ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Vrši brisanje poruke u forumu na osnovu ID-ja poruke
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva za brisanje ocene
        /// DELETE 'https://localhost:44378/api/forumMessage/forumMessageID' \
        ///     --header 'Authorization: Bearer URIS2021' \
        ///     --param  'forumMessageID = 5'
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="forumMessageID">ID poruke koji se brise</param>
        /// <response code="204">Uspesno obrisan froum.</response>
        /// <response code="401" > Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Korisnik pokusava da obrise nepostojecu poruku.</response>
        /// <response code="500">Greska na serveru.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{forumMessageID}")]
        public IActionResult DeleteForumMessage([FromHeader] string key, int forumMessageID)
        {
            if (!_authorizationService.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            try
            {
                _forumMessageService.DeleteForumMessage(forumMessageID);

                return NoContent();
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting forum message: " + ex.Message);

                if (ex.Message.Contains("There is no forum message with that ID!"))
                {
                    return StatusCode(StatusCodes.Status404NotFound, ex.Message);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting forum message!");
            }
        }
    }
}
