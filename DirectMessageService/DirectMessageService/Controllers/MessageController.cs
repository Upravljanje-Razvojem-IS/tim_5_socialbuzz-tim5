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
        

        public MessageController(IMessageService messageService, ILoggerRepository<MessageController> logger, 
            LinkGenerator linkGenerator, IAuthorization authorization) {
            this._messageService = messageService;
            this.logger = logger;
            this.linkGenerator = linkGenerator;
            this._authorizationService = authorization;
            
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
            if (!key.Equals("Bearer URIS2021"))
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

        /// <summary>
        /// Vraca poruke na osnovu prosledjenog ID-a.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva Get Message By ID
        /// GET 'https://localhost:44378/api/message/messageID/messageID' \
        ///     --header 'Authorization: Bearer URIS2021'
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="messageID">ID poruke</param>
        /// <response code="200">Uspesno vracena poruka na osnovu ID-a.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Nije pronadjen nijedna poruka sa zadatim ID-jem.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("messageID/{messageID}")]
        public ActionResult<MessageDTO> GetMessageByID([FromHeader] string key, int messageID)
        {
            if (!key.Equals("Bearer URIS2021"))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            try
            {
                var mess = _messageService.GetMessageByID(messageID);

                logger.LogInformation("Successfully returned message by messageID.");

                return Ok(mess);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Vraca poruke na osnovu prosledjenog ID-a posiljaoca.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva Get Message By Sender
        /// GET 'https://localhost:44378/api/message/sender/senderID' \
        ///     --header 'Authorization: Bearer URIS2021'
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="senderID">ID posiljaoca</param>
        /// <response code="200">Uspesno vracena poruka na osnovu ID-a posiljaoca.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Nije pronadjen nijedna poruka sa zadatim ID-jem.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("sender/{senderID}")]
        public ActionResult<MessageDTO> GetMessagesBySender([FromHeader] string key, int senderID)
        {
            if (!key.Equals("Bearer URIS2021"))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            try
            {
                var mess = _messageService.GetMessagesBySender(senderID);

                logger.LogInformation("Successfully returned messages by senderID.");

                return Ok(mess);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Vraca poruke na osnovu prosledjenog ID-a primaoca.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva Get Message By Receiver
        /// GET 'https://localhost:44378/api/message/receiver/receiverID' \
        ///     --header 'Authorization: Bearer URIS2021'
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="receiverID">ID primaoca</param>
        /// <response code="200">Uspesno vracena poruka na osnovu ID-a primaoca.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Nije pronadjen nijedna poruka sa zadatim ID-jem.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("receiver/{receiverID}")]
        public ActionResult<MessageDTO> GetMessagesByReceiver([FromHeader] string key, int receiverID)
        {
            if (!key.Equals("Bearer URIS2021"))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            try
            {
                var mess = _messageService.GetMessagesByReceiver(receiverID);

                logger.LogInformation("Successfully returned messages by receiverID.");

                return Ok(mess);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Vraca sve poruke za odredjenog korisnika.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva Get Message By Receiver
        /// GET 'https://localhost:44378/api/message/receiver/receiverID' \
        ///     --logovanje korisnika - 'Authorization: Bearer 1' , 'Authorization: Bearer 2', 'Authorization: Bearer 3', 'Authorization: Bearer 4'
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <response code="200">Uspesno vracena poruka na osnovu ID-a primaoca.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Nije pronadjena nijedna poruka sa zadatim ID-jem.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("user")]
        public ActionResult<MessageDTO> GetMessagesForUser([FromHeader] string key)
        {
            if (!_authorizationService.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }
            string keyOnly = key[(key.IndexOf("Bearer") + 7)..];
            int userID = Convert.ToInt32(keyOnly);

            try
            {
                var mess = _messageService.GetMessagesForUser(userID);

                if (!mess.Any()) {
                    return StatusCode(StatusCodes.Status200OK, "User still has no messages!");
                }

                logger.LogInformation("Successfully returned messages for user.");

                return Ok(mess);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Slanje nove poruke.
        /// </summary>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="newMessage">Parametri poruke koja se kreira</param>
        /// 
        /// <returns></returns>
        /// <remarks>
        /// POST 'https://localhost:44378/api/message/' \
        /// Primer zahteva za uspesno slanje poruke \
        ///     --logovanje korisnika - 'Authorization: Bearer 1' , 'Authorization: Bearer 2', 'Authorization: Bearer 3', 'Authorization: Bearer 4'
        ///  
        /// {     \
        ///  "receiverID": 3, \
        ///  "Body": "Ovo je moja nova poruka" \
        /// } 
        /// 
        /// Ako se uloguje korisnik 1 i pokusa da posalje korisniku 4 poruku, nece biti dozvoljeno jer je blokiran.
        /// Ako se uloguje korisnik 3 i pokusa da posalje korusniku 1 poruku, nece biti dozvoljeno jer korisnik 3 ne prati korisnika 1.
        /// </remarks>
        /// <response code="201">Vraca poslatu poruku.</response>
        /// <response code="401">Neuspesna autorizacija korisnika.</response>
        /// <response code="500">Greska na serveru.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<MessageDTO> CreateMessage([FromHeader] string key, [FromBody] MessageCreateDTO newMessage)
        {
            if (!_authorizationService.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            try
            {
                string keyOnly = key[(key.IndexOf("Bearer") + 7)..];
                int userID = Convert.ToInt32(keyOnly);

                if (newMessage.ReceiverID == userID) {
                
                    return StatusCode(StatusCodes.Status400BadRequest, "You can not send a message to yourself!");

                }

                var created = _messageService.CreateMessage(newMessage,userID);

                string location = linkGenerator.GetPathByAction("GetMessageByID", "Message", new { messageID = created.MessageID });

                return Created(location, created);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error sending message: " + ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Vrši brisanje poruke na osnovu ID-ja poruke. 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Svaki korisnik moze da obrise samo onu poruku koju je on poslao!
        /// 
        /// Primer zahteva za brisanje foruma
        /// DELETE 'https://localhost:44378/api/message/messageID' \
        ///     --header 'Authorization: Bearer URIS2021' \
        ///     --param  'messageID = 5'
        /// </remarks>
        /// <param name="key">Authorization Header Bearer Key Value</param>
        /// <param name="messageID">ID poruke koja se brise</param>
        /// <response code="204">Uspesno obrisana poruka.</response>
        /// <response code="401" > Neuspesna autorizacija korisnika.</response>
        /// <response code="404">Korisnik pokusava da obrise nepostojecu poruku.</response>
        /// <response code="500">Greska na serveru.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{messageID}")]
        public IActionResult DeleteMessage([FromHeader] string key, int messageID)
        {
            if (!_authorizationService.AuthorizeUser(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "User authorization failed!");
            }

            string keyOnly = key[(key.IndexOf("Bearer") + 7)..];
            int userID = Convert.ToInt32(keyOnly);

            var message = _messageService.GetMessageByID(messageID);

            if (message.SenderID != userID) {
                return StatusCode(StatusCodes.Status401Unauthorized, "You can not delete message you did not send!");
            }

            try
            {
                _messageService.DeleteMessage(messageID);

                return NoContent();
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting message: " + ex.Message);

                if (ex.Message.Contains("There is no message with that ID!"))
                {
                    return StatusCode(StatusCodes.Status404NotFound, ex.Message);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting forum!");
            }
        }

        /// <summary>
        /// Prikaz HTTP metoda koje korisnik moze da pozove.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Primer zahteva za prikaz dostupnih HTTP metoda
        /// OPTIONS 'https://localhost:44378/api/message/' \
        /// </remarks>
        /// <response code="200">Uspesno prikazane dostupne metode.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpOptions]
        public IActionResult GetReactionsOpstions()
        {
            Response.Headers.Add("Allow", " GET,  POST,  DELETE");
            return Ok();
        }

    }
}
