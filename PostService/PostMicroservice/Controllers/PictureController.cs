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

        public PictureController(IPictureRepository pictureRepository,LinkGenerator linkGenerator,IFakeLoggerRepository fakeLoggerRepository,IHttpContextAccessor contextAccessor,IMapper mapper,IAuthService authService)
        {
            this.pictureRepository = pictureRepository;
            this.linkGenerator = linkGenerator;
            this.fakeLoggerRepository = fakeLoggerRepository;
            this.contextAccessor = contextAccessor;
            this.mapper = mapper;
            this.authService = authService;
        }

        /// <summary>
        /// Returns list of all pictures
        /// </summary>
        /// <param name="postID">ID of the post</param>
        /// <returns>List of all pictures</returns>
        ///  /// <remarks> 
        /// Example of request \
        /// GET 'https://localhost:44200/api/pictures' \
        /// </remarks>
        /// <response code="200">Success, returns list of all pictures.</response>
        /// <response code="404">No pictures found.</response>
        /// <response code="500">Server error.</response>
        [HttpGet]
        [HttpHead]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<PictureDTO>> GetPictures(Guid? postID)
        {

            var pictures = pictureRepository.GetPictures(postID);
            if (pictures == null || pictures.Count == 0)
            {
                return NoContent();
            }
            fakeLoggerRepository.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get all pictures", null);
            return Ok(mapper.Map<List<PictureDTO>>(pictures));




        }



        /// <summary>
        /// Returns a picture based on the forwarded id.
        /// </summary>
        /// <param name="pictureId">ID of the picture</param>
        ///  /// <remarks>        
        /// Example of request \
        /// GET 'https://localhost:44200/api/pictures/' \
        ///     --param  'pictureId = f684f7ae-b1b6-4dfa-a01c-7edc54c689db'
        /// </remarks>
        /// <response code="200">Success, returns the specified picture.</response>
        /// <response code="404">A photo with that ID does not exist.</response>
        /// <response code="500">Server error.</response>
        [HttpGet("{pictureId}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PictureDTO> GetPictureById(Guid pictureId)
        {

            var picture = pictureRepository.GetPictureById(pictureId);

            if (picture == null)
            {
                return NotFound();
            }

            fakeLoggerRepository.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get picture by id", null);
            return Ok(mapper.Map<PictureDTO>(picture));

        }


        /// /// <summary>
        /// Add new picture.
        /// </summary>
        /// <param name="picture">Model of picture</param>
        /// <param name="key">Key for authorization user</param>
        /// <remarks>
        /// Example of request \
        /// POST /api/pictures \
        /// --header 'key: Bearer Milica' \
        /// {     \
        ///     "ImageId" : "F684F7AE-B1B6-4DFA-A01C-7EDC54C689DB", \
        ///     "Url" : "url",\
        ///     "PostID" : "EA96AEA9-27B9-44E6-B46A-B735F559F538" \
        ///}
        /// </remarks>
        /// <response code="200">Successfully added photo.</response>
        /// <response code="401">Unauthorized user.</response>
        /// <response code="500">Server error.</response>
        [Consumes("application/json")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PictureConfirmationDTO> CreatePicture([FromBody] PictureCreationDTO picture, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "The user is not authorized!");
            }

            try
            {

                Picture pictureEntity = mapper.Map<Picture>(picture);
                pictureRepository.CreatePicture(pictureEntity);
                pictureRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetPictureById", "Picture", new { pictureId = picture.ImageId });
                fakeLoggerRepository.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Picture created", null);

                return Created(location, mapper.Map<PictureConfirmationDTO>(pictureEntity));



            }
            catch (Exception ex)
            {
                fakeLoggerRepository.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Error while adding picture", null);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }
        }


        /// <summary>
        /// Update photo with forwarded ID.
        /// </summary>
        /// <param name="picture">Model of the photo to be updated.</param>
        /// <param name="key">Key for authorization user</param>
        /// <remarks>
        /// Example of request \
        /// PUT /api/pictures \
        /// --header 'key: Bearer Milica' \
        /// {     \
        ///     "ImageId" : "F684F7AE-B1B6-4DFA-A01C-7EDC54C689DB", \
        ///     "Url" : "url",\
        ///     "PostID" : "EA96AEA9-27B9-44E6-B46A-B735F559F538" \
        ///}
        /// </remarks>
        /// <response code="200">Success, returns updated picture.</response>
        /// <response code="401">Unauthorized user.</response>
        /// <response code="404">A photo with that ID does not exist.</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PictureDTO> UpdatePicture(PictureUpdateDTO picture, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "The user is not authorized!");
            }

            try
            {
                var oldPicture = pictureRepository.GetPictureById(picture.ImageId);
                if (oldPicture == null)
                {
                    return NotFound(); 
                }
                Picture pictureEntity = mapper.Map<Picture>(picture);

                mapper.Map(pictureEntity, oldPicture);             

                pictureRepository.SaveChanges();
                fakeLoggerRepository.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Picture updated", null);

                return Ok(mapper.Map<PictureDTO>(oldPicture));


               


            }
            catch (Exception)
            {
                fakeLoggerRepository.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Error with updating picture", null);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Deletes the picture based on the forwarded ID.
        /// </summary>
        /// <param name="pictureId">Picture ID</param>
        /// <param name="key">Key for authorization user</param>
        /// <remarks>
        /// Example of request \
        /// DELETE 'https://localhost:44200/api/pictures/'\
        ///  --header 'key: Bearer Milica' \
        ///  --param  'pictureId = f684f7ae-b1b6-4dfa-a01c-7edc54c689db' -> change this for testing\
        /// </remarks>
        /// <response code="204">Success, photo deleted.</response>
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
        /// Returns options for working with photos.
        /// </summary>
        /// <remarks>
        /// Example of request \
        /// OPTIONS 'https://localhost:44200/api/pictures'
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
