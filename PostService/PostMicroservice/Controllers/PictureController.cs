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
                return Created(location, mapper.Map<PictureConfirmationDTO>(pictureEntity));



            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }
        }
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

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
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }
        [HttpOptions]
        [AllowAnonymous] 
        public IActionResult GetPictureOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }


    }
}
