using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using PostMicroservice.Auth;
using PostMicroservice.Data;
using PostMicroservice.Data.ContentRepository;
using PostMicroservice.Data.PostRepository;
using PostMicroservice.Entities;
using PostMicroservice.FakeLogger;
using PostMicroservice.Models.PostDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Controllers
{
    /// <summary>
    /// Post controller to perform crud operations.
    /// </summary>
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
        private readonly IAuthService authService;
        private readonly IUserAccountMockRepository userMockRepository;
        private readonly IContentRepository contentRepository;



        public PostController(IPostRepository postRepository, LinkGenerator linkGenerator, IFakeLoggerRepository fakeLoggerRepository, IHttpContextAccessor contextAccessor, IMapper mapper,IAuthService authService, IUserAccountMockRepository userMockRepository, IContentRepository contentRepository)
        {
            this.postRepository = postRepository;
            this.linkGenerator = linkGenerator;
            this.fakeLoggerRepository = fakeLoggerRepository;
            this.contextAccessor = contextAccessor;
            this.mapper = mapper;
            this.authService = authService;
            this.userMockRepository = userMockRepository;
            this.contentRepository = contentRepository;
        }

        /// <summary>
        /// Returns list of all posts.
        /// </summary>
        /// <param name="dateOfPublication">Date of publication post</param>
        ///  <param name="userName">Username of the user who posted the post</param>
        /// <returns>List of all posts</returns>
        ///  /// <remarks> 
        /// Example of request \
        /// GET '/api/posts' \
        /// param  'dateOfPublication = 2021-08-26T16:45:16.9288332' \
        /// param  'userName = milica_despotovic' \
        /// </remarks>
        /// <response code="200">Success, returns list of all posts.</response>
        /// <response code="204">No posts found.</response>
        /// <response code="500">Server error.</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #nullable enable
        public ActionResult<List<PostDto>> GetPosts(DateTime? dateOfPublication,string? userName, [FromHeader] int accountID)
        {
            List<Post> posts;

            if (userName != null)
            {
                UserAccountDto user = userMockRepository.GetAccountByUserName(userName);
                int userID = user.UserAccountId;
                 posts = postRepository.GetPostByUser(userID,accountID);
            }
          
            else
            {
                 posts = postRepository.GetPosts(dateOfPublication);

            }
           

            if (posts == null || posts.Count == 0)
            {
                return NoContent();
            }
            fakeLoggerRepository.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get all posts", null);
            return Ok(mapper.Map<List<PostDto>>(posts));




        }


        /// <summary>
        /// Returns list of all posts.
        /// </summary>
        /// <returns>List of all posts</returns>
        ///  /// <remarks> 
        /// Example of request \
        /// GET '/api/posts' \
        /// </remarks>
        /// <response code="200">Success, returns list of all posts.</response>
        /// <response code="204">No posts found.</response>
        /// <response code="500">Server error.</response>
        [HttpGet("getPostByFollowedUser")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #nullable enable
        public ActionResult<List<PostDto>> GetPostsOfFollowingUser([FromQuery] int accountID)
        {

            var posts = postRepository.GetPostsByFollowingAccount(accountID);


            if (posts == null || posts.Count == 0)
            {
                return NoContent();
            }
            fakeLoggerRepository.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get all posts", null);
            return Ok(mapper.Map<List<PostDto>>(posts));




        }




        /// <summary>
        /// Returns a post based on the forwarded id.
        /// </summary>
        /// <param name="postId">ID of the post</param>
        /// <remarks>        
        /// Example of request \
        /// GET '/api/posts/' \
        /// param  'postId = 5cee7f04-b84b-480a-b930-08d9689a8b7c'
        /// </remarks>
        /// <response code="200">Success, returns the specified post.</response>
        /// <response code="404">A post with that ID does not exist.</response>
        /// <response code="500">Server error.</response>
        [HttpGet("{postId}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PostDto> GetPostById(Guid postId)
        {

            var post = postRepository.GetPostById(postId);

            if (post == null)
            {
                return NotFound();
            }

            fakeLoggerRepository.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get post by id", null);
            return Ok(mapper.Map<PostDto>(post));

        }


        /// /// <summary>
        /// Add new post.
        /// </summary>
        /// <param name="post">Model of post</param>
        /// <param name="key">Key for authorization user</param>
        /// <remarks>
        /// Example of request \
        /// POST /api/posts \
        /// header 'key: Bearer Milica' \
        /// {      \
        ///  "contentId" : "2959689a-c09f-4c0b-6ceb-08d96643ade7",  \
        ///   "userId" : 5  \
        ///  } \
        /// </remarks>
        /// <response code="201">Successfully added post.</response>
        /// <response code="400">Bad request, user or content with that id does not exists.</response>
        /// <response code="401">Unauthorized user.</response>
        /// <response code="500">Server error.</response>
        [Consumes("application/json")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PostConfirmationDto> CreatePost([FromBody] PostCreationDto post, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "The user is not authorized!");
            }
            if (userMockRepository.GetAccountByUserAccountID(post.UserId) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "User with that id does not exist!");

            }
            if (contentRepository.GetContentById(post.ContentId) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Content with that id does not exist!");

            }


            try
            {

                Post postEntity = mapper.Map<Post>(post);
                postEntity.DateOfPublication = DateTime.Now;
                postEntity.ExpiryDate = postEntity.DateOfPublication.AddMonths(1);
                postRepository.CreatePost(postEntity);
                postRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetPostById", "Post", new { postId = postEntity.PostId });
                fakeLoggerRepository.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Post created", null);

                return Created(location, mapper.Map<PostConfirmationDto>(postEntity));



            }
            catch (Exception ex)
            {
                fakeLoggerRepository.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Error while adding post", null);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }
        }


        /// <summary>
        /// Update post with forwarded ID.
        /// </summary>
        /// <param name="post">Model of the post to be updated.</param>
        /// <param name="key">Key for authorization user</param>
        /// <remarks>
        /// Example of request \
        /// PUT /api/posts \
        /// header 'key: Bearer Milica' \
        /// header 'accountId = 5'  \ 
        ///  {       \
        ///    "postId": "5cee7f04-b84b-480a-b930-08d9689a8b7c", \
        ///    "contentId": "2959689a-c09f-4c0b-6ceb-08d96643ade7", \
        ///     "userId": 5 \
        ///  }
        /// </remarks>
        /// <response code="200">Success, returns updated post.</response>
        /// <response code="400">Bad request, content or user with that ID does not exist.</response>
        /// <response code="401">Unauthorized user.</response>
        /// <response code="403">Not allowed</response>
        /// <response code="404">A post with that ID does not exist.</response>
        /// <response code="500">Server error.</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PostDto> UpdatePost(PostUpdateDto post, [FromHeader] int accountId,[FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "The user is not authorized!");
            }
            if (userMockRepository.GetAccountByUserAccountID(post.UserId) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "User with that id does not exist!");

            }
            if (contentRepository.GetContentById(post.ContentId) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Content with that id does not exist!");

            }

            if (post.UserId != accountId )
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Not allowed, user does not have permission for that action!");

            }


            try
            {
                var oldPost = postRepository.GetPostById(post.PostId);
                if (oldPost == null)
                {
                    return NotFound();
                }
                Post postEntity = mapper.Map<Post>(post);
                postEntity.DateOfPublication = oldPost.DateOfPublication;
                postEntity.ExpiryDate = oldPost.ExpiryDate;

                mapper.Map(postEntity, oldPost);

                postRepository.SaveChanges();
                fakeLoggerRepository.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Post updated", null);

                return Ok(mapper.Map<PostDto>(oldPost));





            }
            catch (Exception)
            {
                fakeLoggerRepository.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Error with updating post", null);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Delete post based on the forwarded ID.
        /// </summary>
        /// <param name="postID">ID of the post</param>
        /// <param name="key">User authorization key</param>
        /// <remarks>
        /// Example of request \
        /// DELETE '/api/posts/'\
        ///  header 'key: Bearer Milica' \
        ///  header 'accountId = 5'  \
        ///  param  'postId = d3c92e47-5518-4307-f3ea-08d9693f4f0e'  \
        /// </remarks>
        /// <response code="204">Success, deleted post.</response>
        /// <response code="401">Unauthorized user.</response>
        ///  <response code="403">Not allowed.</response>
        /// <response code="404">Post not found.</response>
        /// <response code="500">Server error.</response>
        [HttpDelete("{postId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePost(Guid postId, [FromHeader] string key, [FromHeader] int accountId)
        {
            try
            {

                if (!authService.Authorize(key))
                {
                return StatusCode(StatusCodes.Status401Unauthorized, "The user is not authorized!");
                }
           
            
                var post = postRepository.GetPostById(postId);

                if (post == null)
                {
                    return NotFound();
                }


                if (post.UserId != accountId/* && post != null*/)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "Not allowed, user does not have permission for that action!");

                }

               



               
               

                postRepository.DeletePost(postId);
                postRepository.SaveChanges();
                fakeLoggerRepository.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Post deleted", null);

                return NoContent();
            }
            catch (Exception)
            {
                fakeLoggerRepository.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Delete post error", null);

                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        /// <summary>
        /// Returns options for working with posts.
        /// </summary>
        /// <remarks>
        /// Example of request \
        /// OPTIONS '/api/posts'
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetPostOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }


    }


}
