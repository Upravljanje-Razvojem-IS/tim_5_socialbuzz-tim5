using AutoMapper;
using BlockingService.Attributes;
using BlockingService.Dtos;
using BlockingService.Entities;
using BlockingService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlockingService.Controllers
{
    [Route("api/blocks")]
    [ApiController]
    public class BlocksController : ControllerBase
    {
        private readonly IBlockRepository _blockRepository;
        private readonly IBlockListRepository _blockListRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public BlocksController(
            IBlockRepository blockRepository,
            IBlockListRepository blockListRepository,
            IUserRepository userRepository,
            ILocationRepository locationRepository,
            IMapper mapper
        )
        {
            _blockRepository = blockRepository;
            _blockListRepository = blockListRepository;
            _userRepository = userRepository;
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates new Block.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="201">Creates new Block.</response>
        /// <response code="400">Invalid value in body.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns>New Block.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [IsAuthorized]
        public ActionResult<BlockReadDto> Create([FromBody] BlockCreateDto request)
        {
            BlockList blockList = _blockListRepository.Get(request.BlockListId);

            if (blockList == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Block list doesn't exist.");
            }

            if (blockList.Blocks.Any(e => e.Id == request.BlockedUserId))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "You already blocked this User.");
            }

            if (blockList.OwnerUserId == request.BlockedUserId)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "You cannot block yourself.");
            }

            if (_userRepository.Get(request.BlockedUserId) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "User doesn't exist.");
            }

            Block newEntity = _mapper.Map<Block>(request);

            newEntity.DateTime = DateTime.UtcNow;

            newEntity = _blockRepository.Create(newEntity);

            return StatusCode(StatusCodes.Status201Created, _mapper.Map<BlockReadDto>(newEntity));
        }

        /// <summary>
        /// Returns all Blocks.
        /// </summary>
        /// <response code="200">Blocks returned.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns>All Blocks.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult<IEnumerable<BlockReadDto>> Get()
        {
            List<Block> result = _blockRepository.Get().ToList();

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<IEnumerable<BlockReadDto>>(result));
        }

        /// <summary>
        /// Returns all Blocks for User.
        /// </summary>
        /// <param name="blockedUserId">Id of requested User.</param>
        /// <response code="200">Blocks returned.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns>All Blocks.</returns>
        [HttpGet("for-user/{blockedUserId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult<IEnumerable<BlockReadDto>> GetForUser(int blockedUserId)
        {
            if (_userRepository.Get(blockedUserId) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "User doesn't exist.");
            }

            List<Block> result = _blockRepository.Get().Where(e => e.BlockedUserId == blockedUserId).ToList();

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<IEnumerable<BlockReadDto>>(result));
        }

        /// <summary>
        /// Returns Block with provided id.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Block returned.</response>
        /// <response code="400">Non-existing Block.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns>Block by id.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult<BlockReadDto> Get(int id)
        {
            Block result = _blockRepository.Get(id);

            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Block doesn't exist.");
            }

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<BlockReadDto>(result));
        }

        /// <summary>
        /// Returns Block with provided id with User info.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Block returned.</response>
        /// <response code="400">Non-existing Block.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns>Block by id.</returns>
        [HttpGet("{id}/with-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult<BlockWithUserDto> GetWithUser(int id)
        {
            Block found = _blockRepository.Get(id);

            if (found == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Block doesn't exist.");
            }

            BlockWithUserDto result = _mapper.Map<BlockWithUserDto>(found);

            User user = _userRepository.Get(found.BlockedUserId);
            Location location = _locationRepository.Get(user.LocationId);

            result.User = _mapper.Map<UserReadDto>(user);
            result.User.Location = _mapper.Map<LocationReadDto>(location);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Deletes Block from BlockListId.
        /// </summary>
        /// <param name="blockListId">Id of requested Block's BlockList.</param>
        /// <param name="id">Id of requested Block.</param>
        /// <response code="204">Deletes Block with provided id.</response>
        /// <response code="400">Non-existing Block or it doesn't belong to BlockList with provided id.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns>
        [HttpDelete("{blockListId}/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult Delete(int blockListId, int id)
        {
            if (_blockListRepository.Get(id) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Block list doesn't exist.");
            }

            Block found = _blockRepository.Get(id);

            if (found == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Block doesn't exist.");
            }

            if (found.BlockListId != blockListId)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Block doesn't belong to provided Block list.");
            }

            _blockRepository.Delete(id);

            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// Deletes Blocks for User with provided id.
        /// </summary>
        /// <param name="blockedUserId">Id of requested User.</param>
        /// <response code="204">Deletes Blocks with provided blocked User' id.</response>
        /// <response code="400">Non-existing Block.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns>
        [HttpDelete("for-user/{blockedUserId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult DeleteForUser(int blockedUserId)
        {
            if (_userRepository.Get(blockedUserId) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "User doesn't exist.");
            }

            List<Block> found = _blockRepository.Get().Where(e => e.BlockedUserId == blockedUserId).ToList();

            foreach (Block entity in found)
            {
                _blockRepository.Delete(entity.Id);
            }

            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// Deletes all Blocks from BlockList.
        /// </summary>
        /// <param name="blockListId">Id of requested BlockList.</param>
        /// <response code="204">Deletes Blocks from BlockList with provided id.</response>
        /// <response code="400">Non-existing Block.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns>
        [HttpDelete("all-from-block-list/{blockListId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult DeleteAllFromBlockList(int blockListId)
        {
            if (_blockListRepository.Get(blockListId) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Block list doesn't exist.");
            }

            List<Block> found = _blockRepository.Get().Where(e => e.BlockListId == blockListId).ToList();

            foreach (Block entity in found)
            {
                _blockRepository.Delete(entity.Id);
            }

            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// Deletes Blocks with specified User ids from BlockList.
        /// </summary>
        /// <param name="blockListId">Id of requested BlockList.</param>
        /// <param name="request">Dto containing list of blocked User's ids.</param>
        /// <response code="204">Deletes Blocks with provided User ids from BlockList with provided id.</response>
        /// <response code="400">Non-existing User or BlockList.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns>
        [HttpDelete("specific-from-block-list/{blockListId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult DeleteSomeFromBlockList(int blockListId, [FromBody] BlockDeleteSpecificDto request)
        {
            if (_blockListRepository.Get(blockListId) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Block list doesn't exist.");
            }

            List<User> users = _userRepository.Get().ToList();

            List<int> invalidUserIds = request.BlockedUserIds.Except(users.Select(e => e.Id).ToList()).ToList();

            if (invalidUserIds.Count > 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "One or more Users don't exist.");
            }

            List<Block> found = _blockRepository.Get().Where(e => request.BlockedUserIds.Any(i => i == e.BlockedUserId)).ToList();

            foreach (Block entity in found)
            {
                _blockRepository.Delete(entity.Id);
            }

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
