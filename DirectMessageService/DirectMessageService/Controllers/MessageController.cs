using AutoMapper;
using DirectMessageService.DTO;
using DirectMessageService.Logger;
using DirectMessageService.Service;
using ForumService.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectMessageService.Controllers
{
    /// <summary>
    /// Message Kontroler izvrsava CRUD operacije nad podacima />.
    /// </summary>
    [Produces("application/json", "application/xml")]
    [Route("api/message")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly ILoggerRepository<MessageController> logger;
        private readonly LinkGenerator linkGenerator;
        private readonly IAuthorization _authorizationService;
        private readonly IMapper mapper;

        public MessageController(IMessageService messageService, ILoggerRepository<MessageController> logger, 
            LinkGenerator linkGenerator, IAuthorization authorization, IMapper mapper) {
            this._messageService = messageService;
            this.logger = logger;
            this.linkGenerator = linkGenerator;
            this._authorizationService = authorization;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraca sve postojece poruke.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva Get All Messages
        /// GET 'https://localhost:44378/api/message/' \
        ///     --header 'Authorization: Bearer URIS2021'
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <response code="200">Uspesno vracena lista svih poruka.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Nisu pronadjene poruke ili ne postoji nijedna kreirana poruka.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<MessageDTO>> GetAllMessages([FromHeader] string key)
        {
            if (!_authorizationService.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            var mess = _messageService.GetAllMessages();

            if (mess == null || mess.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "No messages found...");
            }

            logger.LogInformation("Successfully returned list of all messsages.");

            return Ok(mess);
        }
    }
}
