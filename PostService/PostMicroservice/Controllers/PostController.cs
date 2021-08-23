using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using PostMicroservice.Data.PostRepository;
using PostMicroservice.Entities;
using PostMicroservice.FakeLogger;
using PostMicroservice.Models.PostDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Controllers
{
    [ApiController]
    [Route("api/posts")]
    [Produces("application/json", "application/xml")]
    public class PostController : ControllerBase
    {
        private readonly LinkGenerator linkGenerator;
        private readonly IPostRepository postRepository;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IFakeLoggerRepository fakeLoggerRepository;
        private readonly IMapper mapper;


        public PostController(IPostRepository postRepository, LinkGenerator linkGenerator, IFakeLoggerRepository fakeLoggerRepository, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            this.postRepository = postRepository;
            this.linkGenerator = linkGenerator;
            this.fakeLoggerRepository = fakeLoggerRepository;
            this.contextAccessor = contextAccessor;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returns list of all posts
        /// </summary>
        /// <param name="dateOfPublication">Date of publication post</param>
        /// <returns>List of all posts</returns>
        ///  /// <remarks> 
        /// Example of request \
        /// GET '/api/posts' \
        /// </remarks>
        /// <response code="200">Success, returns list of all posts.</response>
        /// <response code="404">No posts found.</response>
        /// <response code="500">Server error.</response>
        [HttpGet]
        [HttpHead]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<PostDTO>> GetPosts(DateTime dateOfPublication)
        {

            var pictures = postRepository.GetPosts(dateOfPublication);
            if (pictures == null || pictures.Count == 0)
            {
                return NoContent();
            }
            fakeLoggerRepository.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get all posts", null);
            return Ok(mapper.Map<List<PostDTO>>(pictures));




        }



        /// <summary>
        /// Returns a post based on the forwarded id.
        /// </summary>
        /// <param name="postId">ID of the post</param>
        ///  /// <remarks>        
        /// Example of request \
        /// GET 'https://localhost:44200/api/posts/' \
        ///     --param  'postsId = f684f7ae-b1b6-4dfa-a01c-7edc54c689db'
        /// </remarks>
        /// <response code="200">Success, returns the specified post.</response>
        /// <response code="404">A post with that ID does not exist.</response>
        /// <response code="500">Server error.</response>
        [HttpGet("{postId}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PostDTO> GetPostById(Guid postId)
        {

            var post = postRepository.GetPostById(postId);

            if (post == null)
            {
                return NotFound();
            }

            fakeLoggerRepository.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get post by id", null);
            return Ok(mapper.Map<PostDTO>(post));

        }


}
