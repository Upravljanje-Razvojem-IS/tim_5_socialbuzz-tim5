using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PostMicroservice.Data.PostRepository;
using PostMicroservice.Entities;
using PostMicroservice.FakeLogger;
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

        [Consumes("application/json")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Post> CreatePost([FromBody] Post post)
        {
            try
            {

                //Content contentEntity = mapper.Map<Picture>(picture);

                postRepository.CreatePost(post);
                postRepository.SaveChanges();
                //string location = linkGenerator.GetPathByAction("GetPostById", "Post", new { postId = post.PostId });
                return Created("", post);



            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }
        }
    }
}
