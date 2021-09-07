using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PorukaService.DTOs;
using PorukaService.Interfaces;
using System;

namespace PorukaService.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _repository;

        public MessageController(IMessageRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all messages
        /// </summary>
        /// <returns>List of messages</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public ActionResult GetAll()
        {
            var entities = _repository.Get();

            if (entities.Count == 0)
                return NoContent();

            return Ok(entities);
        }

        /// <summary>
        /// Get message by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>message</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            var entity = _repository.Get(id);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        /// <summary>
        /// Create new message
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>New message</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public ActionResult Postmessage([FromBody] MessageCreateDto dto)
        {
            var entity = _repository.Create(dto);

            return Ok(entity);
        }

        /// <summary>
        /// Update message
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>Updated message</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public ActionResult Putmessage(Guid id, MessageCreateDto dto)
        {
            var entity = _repository.Update(id, dto);

            return Ok(entity);
        }

        /// <summary>
        /// Delete message
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{id}")]
        public ActionResult Deletemessage(Guid id)
        {
            _repository.Delete(id);

            return NoContent();
        }

        /// <summary>
        /// Get messages by two users
        /// </summary>
        /// <param name="userOne"></param>
        /// <param name="userTwo"></param>
        /// <returns>List of messages</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public ActionResult GetByUsers([FromQuery] int userOne, [FromQuery] int userTwo)
        {
            var list = _repository.MessagesBetweenTwoUsers(userOne, userTwo);

            if (list.Count == 0)
                return NoContent();

            return Ok(list);
        }

        /// <summary>
        /// Get all sent by user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>List of messages</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public ActionResult GetSentByUser([FromQuery] int user)
        {
            var list = _repository.AllSentByUser(user);

            if (list.Count == 0)
                return NoContent();

            return Ok(list);
        }
    }
}
