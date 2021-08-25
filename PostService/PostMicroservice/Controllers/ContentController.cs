using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using PostMicroservice.Auth;
using PostMicroservice.Data;
using PostMicroservice.Data.ContentRepository;
using PostMicroservice.Entities;
using PostMicroservice.FakeLogger;
using PostMicroservice.Models.ContentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Controllers
{
    /// <summary>
    /// Content controller to perform crud operations.
    /// </summary>
    [ApiController]
    [Route("api/contents")]
    [Produces("application/json", "application/xml")]
    public class ContentController : ControllerBase
    {
        private readonly LinkGenerator linkGenerator;
        private readonly IContentRepository contentRepository;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IFakeLoggerRepository fakeLoggerRepository;
        private readonly IMapper mapper;
        private readonly IAuthService authService;
        private readonly IObjectForSaleMockRepository objectForSaleMockRepository;


        public ContentController(IContentRepository contentRepository, LinkGenerator linkGenerator, IFakeLoggerRepository fakeLoggerRepository, IHttpContextAccessor contextAccessor, IMapper mapper,IAuthService authService, IObjectForSaleMockRepository objectForSaleMockRepository)
        {
            this.contentRepository = contentRepository;
            this.linkGenerator = linkGenerator;
            this.fakeLoggerRepository = fakeLoggerRepository;
            this.contextAccessor = contextAccessor;
            this.mapper = mapper;
            this.authService = authService;
            this.objectForSaleMockRepository = objectForSaleMockRepository;
        }

        /// <summary>
        /// Returns list of all contents.
        /// </summary>
        /// <param name="title">Title of the content</param>
        /// <returns>List of all contents</returns>
        ///  /// <remarks> 
        /// Example of request \
        /// GET '/api/contents' \
        /// </remarks>
        /// <response code="200">Success, returns list of all contents.</response>
        /// <response code="204">No contents found.</response>
        /// <response code="500">Server error.</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<ContentDTO>> GetContents(string title)
        {

            var contents = contentRepository.GetContents(title);
            if (contents == null || contents.Count == 0)
            {
                return NoContent();
            }
            fakeLoggerRepository.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get all contents", null);
            return Ok(mapper.Map<List<ContentDTO>>(contents));




        }



        /// <summary>
        /// Returns a content based on the forwarded id.
        /// </summary>
        /// <param name="contentId">ID of the content</param>
        ///  /// <remarks>        
        /// Example of request \
        /// GET '/api/contents/' \
        /// param  'contentId = 2959689a-c09f-4c0b-6ceb-08d96643ade7'
        /// </remarks>
        /// <response code="200">Success, returns the specified content.</response>
        /// <response code="404">A content with that ID does not exist.</response>
        /// <response code="500">Server error.</response>
        [HttpGet("{contentId}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ContentDTO> GetContentById(Guid contentId)
        {

            var content = contentRepository.GetContentById(contentId);

            if (content == null)
            {
                return NotFound();
            }

            fakeLoggerRepository.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get content by id", null);
            return Ok(mapper.Map<ContentDTO>(content));

        }


        /// /// <summary>
        /// Add new content.
        /// </summary>
        /// <param name="content">Model of content</param>
        /// <param name="key">Key for authorization user</param>
        /// <remarks>
        /// Example of request \
        /// POST /api/contents \
        /// header 'key: Bearer Milica' \
        /// {     \
        ///     "title" : "Prodaja rolera",\
        ///     "text" : "Prodajem rolere stare 6 mjeseci, u odličnom stanju.",\
        ///     "replacement" : false,\
        ///     "state" : "odlicno",\
        ///     "itemForSaleID" : 5 \
        ///}
        /// </remarks>
        /// <response code="201">Successfully added content.</response>
        /// <response code="400">Bad request, object for sale with that ID does not exist.</response>
        /// <response code="401">Unauthorized user.</response>
        /// <response code="500">Server error.</response>
        [Consumes("application/json")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ContentConfirmationDTO> CreateContent([FromBody] ContentCreationDTO content, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "The user is not authorized!");
            }
            if (objectForSaleMockRepository.GetObjectForSaleByID(content.ItemForSaleID) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Object for sale with that id does not exist!");

            }

            try
            {

                Content contentEntity = mapper.Map<Content>(content);
                contentRepository.CreateContent(contentEntity);
                contentRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetContentById", "Content", new { contentId = contentEntity.ContentId });
                fakeLoggerRepository.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Content created", null);

                return Created(location, mapper.Map<ContentConfirmationDTO>(contentEntity));



            }
            catch (Exception ex)
            {
                fakeLoggerRepository.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Error while adding content", null);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }
        }


        /// <summary>
        /// Update content with forwarded ID.
        /// </summary>
        /// <param name="content">Model of the content to be updated.</param>
        /// <param name="key">Key for authorization user</param>
        /// <remarks>
        /// Example of request \
        /// PUT /api/contents \
        /// --header 'key: Bearer Milica' \
        ///  {     \
        ///     "contentID" : "2959689a-c09f-4c0b-6ceb-08d96643ade7",\
        ///     "title" : "Prodaja rolera",\
        ///     "text" : "Prodajem rolere stare 6 mjeseci, u odličnom stanju.",\
        ///     "replacement" : false,\
        ///     "state" : "odlicno",\
        ///     "itemForSaleID" : 5 \
        ///}
        /// </remarks>
        /// <response code="200">Success, returns updated content.</response>
        ///  <response code="400">Bad request, object for sale with that ID does not exist.</response>
        /// <response code="401">Unauthorized user.</response>
        /// <response code="404">A content with that ID does not exist.</response>
        /// <response code="500">Server error.</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ContentDTO> UpdateContent(ContentUpdateDTO content, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "The user is not authorized!");
            }
            if (objectForSaleMockRepository.GetObjectForSaleByID(content.ItemForSaleID) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Object for sale with that id does not exist!");

            }


            try
            {
                var oldContent = contentRepository.GetContentById(content.ContentId);
                if (oldContent == null)
                {
                    return NotFound();
                }
                Content contentEntity = mapper.Map<Content>(content);

                mapper.Map(contentEntity, oldContent);

                contentRepository.SaveChanges();
                fakeLoggerRepository.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Content updated", null);

                return Ok(mapper.Map<ContentDTO>(oldContent));





            }
            catch (Exception)
            {
                fakeLoggerRepository.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Error with updating content", null);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Delete content based on the forwarded ID.
        /// </summary>
        /// <param name="contentId">ID of the content</param>
        /// <param name="key">User authorization key</param>
        /// <remarks>
        /// Example of request \
        /// DELETE '/api/contents/'\
        ///  header 'key: Bearer Milica' \
        ///  param  'contentId = 2959689a-c09f-4c0b-6ceb-08d96643ade7' 
        /// </remarks>
        /// <response code="204">Success, deleted content.</response>
        /// <response code="401">Unauthorized user.</response>
        /// <response code="404">Content not found.</response>
        /// <response code="500">Server error.</response>
        [HttpDelete("{contentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteContent(Guid contentId, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "The user is not authorized!");
            }

            try
            {
                var content = contentRepository.GetContentById(contentId);

                if (content == null)
                {
                    return NotFound();
                }

                contentRepository.DeleteContent(contentId);
                contentRepository.SaveChanges();
                fakeLoggerRepository.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Content deleted", null);

                return NoContent();
            }
            catch (Exception)
            {
                fakeLoggerRepository.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Delete content error", null);

                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        /// <summary>
        /// Returns options for working with contents.
        /// </summary>
        /// <remarks>
        /// Example of request \
        /// OPTIONS '/api/contents'
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetContentOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
