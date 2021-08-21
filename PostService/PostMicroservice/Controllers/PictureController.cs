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


        public PictureController(IPictureRepository pictureRepository,LinkGenerator linkGenerator,IFakeLoggerRepository fakeLoggerRepository,IHttpContextAccessor contextAccessor,IMapper mapper)
        {
            this.pictureRepository = pictureRepository;
            this.linkGenerator = linkGenerator;
            this.fakeLoggerRepository = fakeLoggerRepository;
            this.contextAccessor = contextAccessor;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returns list of all pictures
        /// </summary>
        /// <returns>List of pictures</returns>
        /// <response code="200">Success, returns list of all pictures</response>
        /// <response code="404">No photos found</response>
        /// <response code="500">Server error</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<PictureDTO>> GetPictures()
        {

            var pictures = pictureRepository.GetPictures();
            if (pictures == null || pictures.Count == 0)
            {
                return NoContent();
            }
            fakeLoggerRepository.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get all pictures", null);
            return Ok(mapper.Map<List<PictureDTO>>(pictures));




        }



        /// <summary>
        /// Returns a photo based on the forwarded id.
        /// </summary>
        /// <param name="pictureId">ID of the picture</param>
        /// <returns></returns>
        /// <response code="200">Success, returns the specified picture</response>
        /// <response code="404">A photo with that ID does not exist</response>
        /// <response code="500">Server error</response>
        [HttpGet("{pictureId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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


        /* /// <summary>
         /// Add new picture.
         /// </summary>
         /// <param name="picture">Model of picture</param>
          /// <remarks>
         /// Example of request \
         /// POST 'https://localhost:44386/api/pictures/'\
         /// Example: \
         /// { \
         ///     "ImageId" : "F684F7AE-B1B6-4DFA-A01C-7EDC54C689DB", \
         ///     "Url" : "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.b92.net%2Fautomobili%2Faktuelno.php%3Fyyyy%3D2019%26mm%3D03%26nav_id%3D1512759&psig=AOvVaw1Hyozs6evTc8pGcGrpfp6l&ust=1629464928590000&source=images&cd=vfe&ved=0CAgQjRxqFwoTCMDVx-GUvfICFQAAAAAdAAAAABAD",\
         ///     "PostID" : "EA96AEA9-27B9-44E6-B46A-B735F559F538" \
         /// }
         /// </remarks>
         /// <response code="201">Successfully added photo.</response>
         /// <response code="500">Server error.</response>*/

        /// /// <summary>
        /// Add new picture.
        /// </summary>
        /// <param name="picture">Model of picture</param>
        /// <remarks>
        /// Example of request \
        /// POST /api/pictures \
        /// {     \
        ///     "ImageId" : "F684F7AE-B1B6-4DFA-A01C-7EDC54C689DB", \
        ///     "Url" : "url",\
        ///     "PostID" : "EA96AEA9-27B9-44E6-B46A-B735F559F538" \
        ///}
        /// </remarks>
        /// <response code="200">Successfully added photo.</response>
        /// <response code="500">Server error.</response>

        [Consumes("application/json")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PictureConfirmationDTO> CreatePicture([FromBody] PictureCreationDTO picture)
        {
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
        /// <response code="200">Success, returns updated picture.</response>
        /// <response code="400">The picture to be updated was not found.</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PictureDTO> UpdatePicture(PictureUpdateDTO picture)
        {
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
                return Ok(mapper.Map<PictureDTO>(oldPicture));


               


            }
            catch (Exception)
            {
                fakeLoggerRepository.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Error with updating picture", null);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Deletes the photo based on the forwarded ID.
        /// </summary>
        /// <param name="pictureId">Picture ID</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Success, photo deleted.</response>
        /// <response code="404">Picture to deleted, not found.</response>
        /// <response code="500">Server error.</response>
        [HttpDelete("{pictureId}")]
        public IActionResult DeletePicture(Guid pictureId)
        {
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
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous] 
        public IActionResult GetPictureOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }


    }
}
