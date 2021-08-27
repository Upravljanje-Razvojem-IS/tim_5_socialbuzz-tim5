using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostMicroservice.Data;
using PostMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using PostMicroservice.Data.Image;
using PostMicroservice.FakeLogger;
using Microsoft.Extensions.Logging;
using AutoMapper;
using PostMicroservice.Models;
using Microsoft.AspNetCore.Authorization;
using PostMicroservice.Auth;
using PostMicroservice.Data.PostRepository;

namespace PostMicroservice.Controllers
{
    /// <summary>
    /// Picture controller to perform crud operations.
    /// </summary>
    [ApiController]
    [Route("api/pictures")]
    [Produces("application/json", "application/xml")]
    public class PictureController : ControllerBase
    {
        private readonly LinkGenerator linkGenerator;
        private readonly IPictureRepository pictureRepository;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IFakeLoggerRepository fakeLoggerRepository;
        private readonly IMapper mapper;
        private readonly IAuthService authService;
        private readonly IPostRepository postRepository;

        public PictureController(IPictureRepository pictureRepository,LinkGenerator linkGenerator,IFakeLoggerRepository fakeLoggerRepository,IHttpContextAccessor contextAccessor,IMapper mapper,IAuthService authService, IPostRepository postRepository)
        {
            this.pictureRepository = pictureRepository;
            this.linkGenerator = linkGenerator;
            this.fakeLoggerRepository = fakeLoggerRepository;
            this.contextAccessor = contextAccessor;
            this.mapper = mapper;
            this.authService = authService;
            this.postRepository = postRepository;
        }

        /// <summary>
        /// Returns list of all pictures.
        /// </summary>
        /// <param name="postID">ID of the post</param>
        /// <returns>List of all pictures</returns>
        ///  /// <remarks> 
        /// Example of request \
        /// GET '/api/pictures' \
        ///   param  'postId = 5cee7f04-b84b-480a-b930-08d9689a8b7c'
        /// </remarks>
        /// <response code="200">Success, returns list of all pictures.</response>
        /// <response code="204">No pictures found.</response>
        /// <response code="500">Server error.</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<PictureDto>> GetPictures(Guid? postID)
        {

            var pictures = pictureRepository.GetPictures(postID);
            if (pictures == null || pictures.Count == 0)
            {
                return NoContent();
            }
            fakeLoggerRepository.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get all pictures", null);
            return Ok(mapper.Map<List<PictureDto>>(pictures));




        }



        /// <summary>
        /// Returns a picture based on the forwarded id.
        /// </summary>
        /// <param name="pictureId">ID of the picture</param>
        ///  /// <remarks>        
        /// Example of request \
        /// GET 'https://localhost:44200/api/pictures/' \
        ///     param  'pictureId = d0e5b759-f1bf-490c-a7fa-08d968a6710c'
        /// </remarks>
        /// <response code="200">Success, returns the specified picture.</response>
        /// <response code="404">A photo with that ID does not exist.</response>
        /// <response code="500">Server error.</response>
        [HttpGet("{pictureId}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PictureDto> GetPictureById(Guid pictureId)
        {

            var picture = pictureRepository.GetPictureById(pictureId);

            if (picture == null)
            {
                return NotFound();
            }

            fakeLoggerRepository.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get picture by id", null);
            return Ok(mapper.Map<PictureDto>(picture));

        }


        /// /// <summary>
        /// Add new picture.
        /// </summary>
        /// <param name="picture">Model of picture</param>
        /// <param name="key">Key for authorization user</param>
        /// <remarks>
        /// Example of request \
        /// POST /api/pictures \
        /// header 'key: Bearer Milica' \
        /// {     \
        ///     "url" : "url",\
        ///     "postID" : "5cee7f04-b84b-480a-b930-08d9689a8b7c" \
        ///}
        /// </remarks>
        /// <response code="201">Successfully added photo.</response>
        /// <response code="400">Bad request, post with that ID does not exist.</response>
        /// <response code="401">Unauthorized user.</response>
        /// <response code="500">Server error.</response>
        [Consumes("application/json")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PictureConfirmationDto> CreatePicture([FromBody] PictureCreationDto picture, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "The user is not authorized!");
            }
            if (postRepository.GetPostById(picture.PostID) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Post with that id does not exist!");

            }


            try
            {

                Picture pictureEntity = mapper.Map<Picture>(picture);
                pictureRepository.CreatePicture(pictureEntity);
                pictureRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetPictureById", "Picture", new { pictureId = pictureEntity.PictureId });
                fakeLoggerRepository.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Picture created", null);

                return Created(location, mapper.Map<PictureConfirmationDto>(pictureEntity));



            }
            catch (Exception ex)
            {
                fakeLoggerRepository.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Error while adding picture", null);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }
        }


        /// <summary>
        /// Update picture with forwarded ID.
        /// </summary>
        /// <param name="picture">Model of the picture to be updated.</param>
        /// <param name="key">Key for authorization user</param>
        /// <remarks>
        /// Example of request \
        /// PUT /api/pictures \
        /// header 'key: Bearer Milica' \
        /// {     \
        ///    "pictureId": "d0e5b759-f1bf-490c-a7fa-08d968a6710c",\
        /// "url": "URL1",\
        /// "postID": "5cee7f04-b84b-480a-b930-08d9689a8b7c"\
        ///}
        /// </remarks>
        /// <response code="200">Success, returns updated picture.</response>
        ///  <response code="400">Bad request, post with that ID does not exist.</response>
        /// <response code="401">Unauthorized user.</response>
        /// <response code="404">A picture with that ID does not exist.</response>
        /// <response code="500">Server error.</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PictureDto> UpdatePicture(PictureUpdateDto picture, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "The user is not authorized!");
            }
            if (postRepository.GetPostById(picture.PostID) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Post with that id does not exist!");

            }

            try
            {
                var oldPicture = pictureRepository.GetPictureById(picture.PictureId);
                if (oldPicture == null)
                {
                    return NotFound(); 
                }
                Picture pictureEntity = mapper.Map<Picture>(picture);

                mapper.Map(pictureEntity, oldPicture);             

                pictureRepository.SaveChanges();
                fakeLoggerRepository.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Picture updated", null);

                return Ok(mapper.Map<PictureDto>(oldPicture));


               


            }
            catch (Exception)
            {
                fakeLoggerRepository.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Error with updating picture", null);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Delete picture based on the forwarded ID.
        /// </summary>
        /// <param name="pictureId">ID of the picture</param>
        /// <param name="key">User authorization key</param>
        /// <remarks>
        /// Example of request \
        /// DELETE '/api/pictures/'\
        ///  header 'key: Bearer Milica' \
        ///  param  'pictureId = 0663ca84-4664-42c2-37f9-08d9693d4228' 
        /// </remarks>
        /// <response code="204">Success, deleted picture.</response>
        /// <response code="401">Unauthorized user.</response>
        /// <response code="404">Picture not found.</response>
        /// <response code="500">Server error.</response>
        [HttpDelete("{pictureId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePicture(Guid pictureId, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "The user is not authorized!");
            }

            try
            {
                var picture = pictureRepository.GetPictureById(pictureId);

                if (picture == null)
                {
                    return NotFound();
                }

                pictureRepository.DeletePicture(pictureId);
                pictureRepository.SaveChanges();
                fakeLoggerRepository.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Picture deleted", null);

                return NoContent();
            }
            catch (Exception)
            {
                fakeLoggerRepository.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Delete picture error", null);

                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        /// <summary>
        /// Returns options for working with pictures.
        /// </summary>
        /// <remarks>
        /// Example of request \
        /// OPTIONS '/api/pictures'
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpOptions]
        [AllowAnonymous] 
        public IActionResult GetPictureOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }


    }
}
