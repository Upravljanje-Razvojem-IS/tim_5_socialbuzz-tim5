using AutoMapper;
using FriendsService.Attributes;
using FriendsService.Dtos;
using FriendsService.Entities;
using FriendsService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FriendsService.Controllers
{
    [Route("api/friends")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly IFriendRepository _friendRepository;
        private readonly IFriendListRepository _friendListRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public FriendsController(
            IFriendRepository friendRepository,
            IFriendListRepository friendListRepository,
            IUserRepository userRepository,
            ILocationRepository locationRepository,
            IMapper mapper
        )
        {
            _friendRepository = friendRepository;
            _friendListRepository = friendListRepository;
            _userRepository = userRepository;
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates new Friend.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="201">Creates new Friend.</response>
        /// <response code="400">Invalid value in body.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns>New Friend.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [IsAuthorized]
        public ActionResult<FriendReadDto> Create([FromBody] FriendCreateDto request)
        {
            FriendList friendList = _friendListRepository.Get(request.FriendListId);

            if (friendList == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Friend list doesn't exist.");
            }

            if (friendList.Friends.Any(e => e.Id == request.FriendUserId))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "You already addes this User as your Friend.");
            }

            if (friendList.OwnerUserId == request.FriendUserId)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "You cannot add yourself as a Friend.");
            }

            if (_userRepository.Get(request.FriendUserId) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "User doesn't exist.");
            }

            Friend newEntity = _mapper.Map<Friend>(request);

            newEntity.DateTime = DateTime.UtcNow;

            newEntity = _friendRepository.Create(newEntity);

            return StatusCode(StatusCodes.Status201Created, _mapper.Map<FriendReadDto>(newEntity));
        }

        /// <summary>
        /// Returns all Friends.
        /// </summary>
        /// <response code="200">Friends returned.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns>All Friends.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult<IEnumerable<FriendReadDto>> Get()
        {
            List<Friend> result = _friendRepository.Get().ToList();

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<IEnumerable<FriendReadDto>>(result));
        }

        /// <summary>
        /// Returns all Friends for User.
        /// </summary>
        /// <param name="friendUserId">Id of requested User.</param>
        /// <response code="200">Friends returned.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns>All Friends.</returns>
        [HttpGet("for-user/{friendUserId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult<IEnumerable<FriendReadDto>> GetForUser(int friendUserId)
        {
            if (_userRepository.Get(friendUserId) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "User doesn't exist.");
            }

            List<Friend> result = _friendRepository.Get().Where(e => e.FriendUserId == friendUserId).ToList();

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<IEnumerable<FriendReadDto>>(result));
        }

        /// <summary>
        /// Returns Friend with provided id.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Friend returned.</response>
        /// <response code="400">Non-existing Friend.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns>Friend by id.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult<FriendReadDto> Get(int id)
        {
            Friend result = _friendRepository.Get(id);

            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Friend doesn't exist.");
            }

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<FriendReadDto>(result));
        }

        /// <summary>
        /// Returns Friend with provided id with User info.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Friend returned.</response>
        /// <response code="400">Non-existing Friend.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns>Friend by id.</returns>
        [HttpGet("{id}/with-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult<FriendWithUserDto> GetWithUser(int id)
        {
            Friend found = _friendRepository.Get(id);

            if (found == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Friend doesn't exist.");
            }

            FriendWithUserDto result = _mapper.Map<FriendWithUserDto>(found);

            User user = _userRepository.Get(found.FriendUserId);
            Location location = _locationRepository.Get(user.LocationId);

            result.User = _mapper.Map<UserReadDto>(user);
            result.User.Location = _mapper.Map<LocationReadDto>(location);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Deletes Friend from FriendListId.
        /// </summary>
        /// <param name="friendListId">Id of requested Friend's FriendList.</param>
        /// <param name="id">Id of requested Friend.</param>
        /// <response code="204">Deletes Friend with provided id.</response>
        /// <response code="400">Non-existing Friend or it doesn't belong to FriendList with provided id.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns>
        [HttpDelete("{friendListId}/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult Delete(int friendListId, int id)
        {
            if (_friendListRepository.Get(id) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Friend list doesn't exist.");
            }

            Friend found = _friendRepository.Get(id);

            if (found == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Friend doesn't exist.");
            }

            if (found.FriendListId != friendListId)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Friend doesn't belong to provided Friend list.");
            }

            _friendRepository.Delete(id);

            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// Deletes Friends for User with provided id.
        /// </summary>
        /// <param name="friendUserId">Id of requested User.</param>
        /// <response code="204">Deletes Friends with provided friend User' id.</response>
        /// <response code="400">Non-existing Friend.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns>
        [HttpDelete("for-user/{friendUserId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult DeleteForUser(int friendUserId)
        {
            if (_userRepository.Get(friendUserId) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "User doesn't exist.");
            }

            List<Friend> found = _friendRepository.Get().Where(e => e.FriendUserId == friendUserId).ToList();

            foreach (Friend entity in found)
            {
                _friendRepository.Delete(entity.Id);
            }

            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// Deletes all Friends from FriendList.
        /// </summary>
        /// <param name="friendListId">Id of requested FriendList.</param>
        /// <response code="204">Deletes Friends from FriendList with provided id.</response>
        /// <response code="400">Non-existing Friend.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns>
        [HttpDelete("all-from-friend-list/{friendListId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult DeleteAllFromFriendList(int friendListId)
        {
            if (_friendListRepository.Get(friendListId) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Friend list doesn't exist.");
            }

            List<Friend> found = _friendRepository.Get().Where(e => e.FriendListId == friendListId).ToList();

            foreach (Friend entity in found)
            {
                _friendRepository.Delete(entity.Id);
            }

            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// Deletes Friends with specified User ids from FriendList.
        /// </summary>
        /// <param name="friendListId">Id of requested FriendList.</param>
        /// <param name="request">Dto containing list of friend User's ids.</param>
        /// <response code="204">Deletes Friends with provided User ids from FriendList with provided id.</response>
        /// <response code="400">Non-existing User or FriendList.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <returns></returns>
        [HttpDelete("specific-from-friend-list/{friendListId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [IsAuthorized]
        public ActionResult DeleteSomeFromFriendList(int friendListId, [FromBody] FriendDeleteSpecificDto request)
        {
            if (_friendListRepository.Get(friendListId) == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Friend list doesn't exist.");
            }

            List<User> users = _userRepository.Get().ToList();

            List<int> invalidUserIds = request.FriendUserIds.Except(users.Select(e => e.Id).ToList()).ToList();

            if (invalidUserIds.Count > 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "One or more Users don't exist.");
            }

            List<Friend> found = _friendRepository.Get().Where(e => request.FriendUserIds.Any(i => i == e.FriendUserId)).ToList();

            foreach (Friend entity in found)
            {
                _friendRepository.Delete(entity.Id);
            }

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
