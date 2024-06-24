using CommentService.Data.Interfaces;
using CommentService.Logger;
using CommentService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using CommentService.DTOs;

namespace CommentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly LoggerL _logger;

        public CommentsController(IMapper mapper, ICommentRepository commentRepository, LoggerL logger, IAccountRepository accountRepository)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
            _logger = logger;
            _accountRepository = accountRepository;
           
        }

        /// <summary>
        /// Creates a new comment
        /// </summary>
        /// <response code="200">Comment created</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">User not authorized</response>
        /// <response code="500">Internal server error</response>
        /// <param name="commentCreateDto"></param>
        /// <returns>New Comment</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<CommentReadDto> Create([FromBody] CommentCreateDto commentCreateDto)
        {
            var account = _accountRepository.Get(commentCreateDto.AccountUid);
            if (account == null)
            {
                return BadRequest("Account not found");
            }

            Comment comment = _mapper.Map<Comment>(commentCreateDto);
            comment.TimeAndDate = DateTime.UtcNow;
            _commentRepository.Create(comment);
            _logger.Log("Comment created");
            return Ok(_mapper.Map<CommentReadDto>(comment));
        }

        /// <summary>
        /// Returns all comments
        /// </summary>
        /// <response code="200">Comments returned</response>
        /// <response code="401">User not authorized</response>
        /// <response code="500">Internal server error</response>
        /// <returns>Comments</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public ActionResult<IEnumerable<CommentReadDto>> Get()
        {
            List<Comment> result = _commentRepository.Get().ToList();
            _logger.Log("Get comments");
            return Ok(_mapper.Map<IEnumerable<CommentReadDto>>(result));
        }

        /// <summary>
        /// Returns an comment
        /// </summary>
        /// <response code="200">Comment returned</response>
        /// <response code="400">Comment doesn't exist</response>
        /// <response code="401">User not authorized</response>
        /// <response code="500">Internal server error</response>
        /// <returns>Comment</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public ActionResult<CommentReadDto> Get(int id)
        {
            if (_commentRepository.Get(id) == null)
            {
                return BadRequest("Comment with that Id doesn't exist.");
            }

            Comment result = _commentRepository.Get(id);
            _logger.Log("Get comment");
            return Ok(_mapper.Map<CommentReadDto>(result));
        }

        /// <summary>
        /// Get users comment.
        /// </summary>
        /// <response code="200">Returns comments for ad.</response>
        /// <response code="400">User doesn't exist or user doesn't have an comment</response>
        /// <response code="401">User not authorized</response>
        /// <response code="500">Internal server error</response>
        /// <param name="userId"></param>
        /// <returns>Comment</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("post/{adId}")]
        public ActionResult<CommentReadDto> GetForAd(int adId)
        {
            var comments = _commentRepository.Get()
                .Where(comment => comment.AdId == adId).ToList();
            _logger.Log("Get comments for post");
            return Ok(_mapper.Map<List<CommentReadDto>>(comments));
        }

        /// <summary>
        /// Updates comment.
        /// </summary>
        /// <response code="200">Comment updated</response>
        /// <response code="400">Comment doesn't exist or bad request</response>
        /// <response code="401">User not authorized</response>
        /// <response code="500">Internal server error</response>
        /// <param name="commentUpdateDto"></param>
        /// <returns>Comment</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public ActionResult<CommentReadDto> Update(CommentUpdateDto commentUpdateDto)
        {
            Comment comment = _commentRepository.Get(commentUpdateDto.Id);
            if (comment == null)
            {
                return BadRequest("Comment with that id doesn't exist.");
            }

            comment = _mapper.Map(commentUpdateDto, comment);
            _commentRepository.Update(comment);
            _logger.Log("Update comment");
            return Ok(comment);
        }

        /// <summary>
        /// Deletes an comment.
        /// </summary>
        /// <response code="200">Comment deleted</response>
        /// <response code="400">Comment doesn't exist</response>
        /// <response code="401">User not authorized</response>
        /// <response code="500">Internal server error</response>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public ActionResult<CommentReadDto> Delete(int id)
        {
            Comment comment = _commentRepository.Get(id);
            if (comment == null)
            {
                return BadRequest("Comment with that id doesn't exit");
            }
            _commentRepository.Delete(comment);
            _logger.Log("Delete Comment");
            return Ok();
        }
    }
}
