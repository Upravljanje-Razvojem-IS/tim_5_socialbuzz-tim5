using AutoMapper;
using FriendsService.Attributes;
using FriendsService.Dtos;
using FriendsService.Entities;
using FriendsService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FriendsService.Controllers
{
    [Route("api/friend-lists")]
    [ApiController]
    public class FriendListsController : ControllerBase
    {
        private readonly IFriendListRepository _friendListRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public FriendListsController(
            IFriendListRepository friendListRepository,
            IUserRepository userRepository,
            IMapper mapper
        )
        {
            _friendListRepository = friendListRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates new FriendList.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="201">Creates new FriendList.</response>
        /// <response code="400">Non-existing User.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns>New FriendList.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [IsAuthorized]
        public ActionResult<FriendListReadDto> Create([FromBody] FriendListCreateDto request)
        {
            if (_userRepository.Get(request.OwnerUserId) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "User doesn't exist.");
            }

            FriendList newEntity = _mapper.Map<FriendList>(request);

            newEntity = _friendListRepository.Create(newEntity);

            return StatusCode(StatusCodes.Status201Created, _mapper.Map<FriendListReadDto>(newEntity));
        }

        /// <summary>
        /// Returns all FriendLists.
        /// </summary>
        /// <response code="200">FriendLists returned.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns>All FriendLists.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult<IEnumerable<FriendListReadDto>> Get()
        {
            List<FriendList> result = _friendListRepository.Get().ToList();

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<IEnumerable<FriendListReadDto>>(result));
        }

        /// <summary>
        /// Returns FriendList with provided id.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">FriendList returned.</response>
        /// <response code="400">Non-existing FriendList.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns>FriendList by id.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult<FriendListReadDto> Get(int id)
        {
            FriendList result = _friendListRepository.Get(id);

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<FriendListReadDto>(result));
        }

        /// <summary>
        /// Returns FriendList for User with provided id.
        /// </summary>
        /// <param name="userId"></param>
        /// <response code="200">FriendList returned.</response>
        /// <response code="400">Non-existing User's FriendList.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns>FriendList by id.</returns>
        [HttpGet("for-user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult<FriendListReadDto> GetForUser(int userId)
        {
            FriendList result = _friendListRepository.Get().Where(e => e.OwnerUserId == userId).FirstOrDefault();

            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "User doesn't have Friend list.");
            }

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<FriendListReadDto>(result));
        }

        /// <summary>
        /// Deletes FriendList.
        /// </summary>
        /// <param name="id">Id of requested FriendList.</param>
        /// <response code="204">Deletes FriendList with provided id.</response>
        /// <response code="400">Non-existing FriendList.</response>
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
            _friendListRepository.Delete(id);

            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// Deletes FriendList for User with provided id.
        /// </summary>
        /// <param name="userId">Id of requested FriendList.</param>
        /// <response code="204">Deletes User's FriendList.</response>
        /// <response code="400">Non-existing User's FriendList.</response>
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
            FriendList result = _friendListRepository.Get().Where(e => e.OwnerUserId == userId).FirstOrDefault();

            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "User doesn't have Friend list.");
            }

            _friendListRepository.Delete(result.Id);

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
