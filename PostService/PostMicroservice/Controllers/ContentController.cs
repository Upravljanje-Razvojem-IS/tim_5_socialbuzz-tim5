using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PostMicroservice.Data.ContentRepository;
using PostMicroservice.Entities;
using PostMicroservice.FakeLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Controllers
{
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


        public ContentController(IContentRepository contentRepository, LinkGenerator linkGenerator, IFakeLoggerRepository fakeLoggerRepository, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            this.contentRepository = contentRepository;
            this.linkGenerator = linkGenerator;
            this.fakeLoggerRepository = fakeLoggerRepository;
            this.contextAccessor = contextAccessor;
            this.mapper = mapper;
        }


        [Consumes("application/json")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Content> CreateContent([FromBody] Content content)
        {
            try
            {

                //Content contentEntity = mapper.Map<Picture>(picture);

                contentRepository.CreateContent(content);
                contentRepository.SaveChanges();
                //string location = linkGenerator.GetPathByAction("GetContentById", "Content", new { contentId = content.ContentId });
                return Created("", content);



            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }
        }
    }
}
