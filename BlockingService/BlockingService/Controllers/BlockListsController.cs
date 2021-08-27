using AutoMapper;
using BlockingService.Attributes;
using BlockingService.Dtos;
using BlockingService.Entities;
using BlockingService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BlockingService.Controllers
{
    [Route("api/block-lists")]
    [ApiController]
    public class BlockListsController : ControllerBase
    {
        private readonly IBlockListRepository _blockListRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public BlockListsController(
            IBlockListRepository repository,
            IUserRepository userRepository,
            IMapper mapper
        )
        {
            _blockListRepository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates new BlockList.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="201">Creates new BlockList.</response>
        /// <response code="400">Non-existing User.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns>New BlockList.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [IsAuthorized]
        public ActionResult<BlockListReadDto> Create([FromBody] BlockListCreateDto request)
        {
            if (_userRepository.Get(request.OwnerUserId) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "User doesn't exist.");
            }

            BlockList newEntity = _mapper.Map<BlockList>(request);

            newEntity = _blockListRepository.Create(newEntity);

            return StatusCode(StatusCodes.Status201Created, _mapper.Map<BlockListReadDto>(newEntity));
        }

        /// <summary>
        /// Returns all BlockLists.
        /// </summary>
        /// <response code="200">BlockLists returned.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns>All BlockLists.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult<IEnumerable<BlockListReadDto>> Get()
        {
            List<BlockList> result = _blockListRepository.Get().ToList();

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<IEnumerable<BlockListReadDto>>(result));
        }

        /// <summary>
        /// Returns BlockList with provided id.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">BlockList returned.</response>
        /// <response code="400">Non-existing BlockList.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns>BlockList by id.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult<BlockListReadDto> Get(int id)
        {
            BlockList result = _blockListRepository.Get(id);

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<BlockListReadDto>(result));
        }

        /// <summary>
        /// Returns BlockList for User with provided id.
        /// </summary>
        /// <param name="userId"></param>
        /// <response code="200">BlockList returned.</response>
        /// <response code="400">Non-existing User's BlockList.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns>BlockList by id.</returns>
        [HttpGet("for-user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult<BlockListReadDto> GetForUser(int userId)
        {
            BlockList result = _blockListRepository.Get().Where(e => e.OwnerUserId == userId).FirstOrDefault();

            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "User doesn't have Block list.");
            }

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<BlockListReadDto>(result));
        }

        /// <summary>
        /// Deletes BlockList.
        /// </summary>
        /// <param name="id">Id of requested BlockList.</param>
        /// <response code="204">Deletes BlockList with provided id.</response>
        /// <response code="400">Non-existing BlockList.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult Delete(int id)
        {
            _blockListRepository.Delete(id);

            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// Deletes BlockList for User with provided id.
        /// </summary>
        /// <param name="userId">Id of requested BlockList.</param>
        /// <response code="204">Deletes User's BlockList.</response>
        /// <response code="400">Non-existing User's BlockList.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns>
        [HttpDelete("for-user/{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult DeleteForUser(int userId)
        {
            BlockList result = _blockListRepository.Get().Where(e => e.OwnerUserId == userId).FirstOrDefault();

            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "User doesn't have Block list.");
            }

            _blockListRepository.Delete(result.Id);

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
