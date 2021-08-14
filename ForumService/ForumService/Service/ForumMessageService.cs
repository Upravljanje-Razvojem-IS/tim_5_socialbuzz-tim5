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
    public class ForumMessageService : IForumMessageService
    {
        private readonly IForumMessageRepository _forumMessageRepository;
        private readonly IUserMockRepository _userMockRepository;
        private readonly IMapper mapper;

        public ForumMessageService(IForumMessageRepository forumMessageRepository, IUserMockRepository userMockRepository, IMapper mapper) {
            this._forumMessageRepository = forumMessageRepository;
            this._userMockRepository = userMockRepository;
            this.mapper = mapper;
        }

        public ForumMessageDTO CreateForumMessage(ForumMessageDTO newForumMessage)
        {
            ForumMessage entity = mapper.Map<ForumMessage>(newForumMessage);
            var user = _userMockRepository.GetUserByID(entity.SenderID);

            if (user == null) {
                throw new NotFoundException("There is no user with that ID!");
            }

            try
            {
                var forumMessage = _forumMessageRepository.CreateForumMessage(entity);
                _forumMessageRepository.SaveChanges();
                return mapper.Map<ForumMessageDTO>(forumMessage);
            }
            catch (Exception ex)
            {
                throw new ErrorOccurException(ex.Message);
            }
        }

        public void DeleteForumMessage(int forumMessageID)
        {
            var forumMessage = _forumMessageRepository.GetForumMessageByID(forumMessageID);

            if (forumMessage == null)
            {
                throw new NotFoundException("There is no forum message with that ID!");
            }
            try
            {
                _forumMessageRepository.DeleteForumMessage(forumMessageID);
                _forumMessageRepository.SaveChanges();
            }

            catch (Exception ex)
            {
                throw new ErrorOccurException("Error deleting forum message: " + ex.Message);
            }
        }

        public List<ForumMessageDTO> GetAllForumMessages()
        {
            var forumMessages = _forumMessageRepository.GetAllForumMessages();
            return mapper.Map<List<ForumMessageDTO>>(forumMessages);
        }

        public ForumMessageDTO GetForumMessageByID(int forumMessageID)
        {
            var forumMessage = _forumMessageRepository.GetForumMessageByID(forumMessageID);

            if (forumMessage == null)
            {
                throw new NotFoundException("No forum message with that ID found...");
            }

            return mapper.Map<ForumMessageDTO>(forumMessage);
        }

        public ForumMessageDTO GetForumMessageByTitle(string title)
        {
            var forumMessage = _forumMessageRepository.GetForumMessageByTitle(title);

            if (forumMessage == null)
            {
                throw new NotFoundException("No forum message with that title found...");
            }

            return mapper.Map<ForumMessageDTO>(forumMessage);
        }

        public List<ForumMessageDTO> GetForumMessagesByForumID(int forumId)
        {
            var forumMessages = _forumMessageRepository.GetForumMessagesByForumID(forumId);

            if (forumMessages == null)
            {
                throw new NotFoundException("No forum message in forum found...");
            }

            return mapper.Map<List<ForumMessageDTO>>(forumMessages);
        }

        public List<ForumMessageDTO> GetForumMessagesBySender(int senderId)
        {
            var userId = _userMockRepository.GetUserByID(senderId);

            if (userId == null)
            {
                throw new NotFoundException("There is no user with that ID ...");
            }

            var forumMessages = _forumMessageRepository.GetForumMessagesBySender(senderId);

            if (forumMessages == null || forumMessages.Count == 0)
            {
                throw new ErrorOccurException("This user has not yet sent any message...");
            }

            return mapper.Map<List<ForumMessageDTO>>(forumMessages);
        }

        public void UpdateForumMessage(ForumMessageDTO updatedForumMessage)
        {
            if (_forumMessageRepository.GetForumMessageByID(updatedForumMessage.ForumMessageID) == null)
            {
                throw new NotFoundException("Forum message with that ID does not exist!");
            }

            var oldForumMess = _forumMessageRepository.GetForumMessageByID(updatedForumMessage.ForumMessageID);
            var newForumMess = mapper.Map<ForumMessage>(updatedForumMessage);

            if (oldForumMess.ForumMessageID != newForumMess.ForumMessageID)
            {
                throw new ErrorOccurException("Forum message ID can not be changed!");
            }

            try
            {
                newForumMess.SenderID = oldForumMess.SenderID;

                mapper.Map(newForumMess, oldForumMess);
                _forumMessageRepository.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new ErrorOccurException("Error updating forum message: " + ex.Message);

            }
        }
    }
}
