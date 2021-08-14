using AutoMapper;
using ForumService.DTO;
using ForumService.Entity;
using ForumService.Exceptions;
using ForumService.Repository;
using ForumService.Repository.UserMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Service
{
    public class ForumService : IForumService
    {
        private readonly IForumRepository _forumRepository;
        private readonly IUserMockRepository _userMockRepository;
        private readonly IMapper mapper;

        public ForumService(IForumRepository forumRepository, IUserMockRepository userMockRepository, IMapper mapper)
        {
            this._forumRepository = forumRepository;
            this._userMockRepository = userMockRepository;
            this.mapper = mapper;
        }

        public ForumDTO CreateForum(ForumDTO newForum)
        {
            Forum entity = mapper.Map<Forum>(newForum);

            try {
                var forum = _forumRepository.CreateForum(entity);
                _forumRepository.SaveChanges();
                return mapper.Map<ForumDTO>(forum);
            }
            catch (Exception ex)
            {
                throw new ErrorOccurException(ex.Message);
            }
        }

        public void DeleteForum(int forumID)
        {
            var forum = _forumRepository.GetForumByID(forumID);

            if (forum == null) {
                throw new NotFoundException("There is no forum with that ID!");
            }
            try
            {
                _forumRepository.DeleteForum(forumID);
                _forumRepository.SaveChanges();
            }

            catch (Exception ex)
            {
                throw new ErrorOccurException("Error deleting forum: " + ex.Message);
            }
        }

        public List<ForumDTO> GetAllForums()
        {
            var forums = _forumRepository.GetAllForums();
            return mapper.Map<List<ForumDTO>>(forums);
        }

        public ForumDTO GetForumByID(int forumID)
        {
            var forum = _forumRepository.GetForumByID(forumID);

            if (forum == null) {
                throw new NotFoundException("No forum with that ID found...");
            }

            return mapper.Map<ForumDTO>(forum);
        }

        public List<ForumDTO> GetForumsByOwner(int ownerID)
        {
            var userId = _userMockRepository.GetUserByID(ownerID);

            if (userId == null)
            {
                throw new NotFoundException("There is no user with that ID ...");
            }

            var forums = _forumRepository.GetForumsByOwner(ownerID);

            if (forums == null || forums.Count == 0) {
                throw new ErrorOccurException("This user has not yet created any forum...");
            }

            return mapper.Map<List<ForumDTO>>(forums);
        }

        public void UpdateForum(ForumDTO updatedForum)
        {
            if (_forumRepository.GetForumByID(updatedForum.ForumID) == null) {
                throw new NotFoundException("Forum with that ID does not exist!");
            }

            var oldForum = _forumRepository.GetForumByID(updatedForum.ForumID);
            var newForum = mapper.Map<Forum>(updatedForum);

            if (oldForum.ForumID != newForum.ForumID) {
                throw new ErrorOccurException("Forum ID can not be changed!");
            }

            try
            {
                newForum.UserID = oldForum.UserID;

                mapper.Map(newForum, oldForum);
                _forumRepository.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new ErrorOccurException("Error updating forum: " + ex.Message);

            }
        }
    }
}
