using AutoMapper;
using ForumService.DTO;
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
            throw new NotImplementedException();
        }

        public void DeleteForumMessage(int forumMessageID)
        {
            throw new NotImplementedException();
        }

        public List<ForumMessageDTO> GetAllForumMessages()
        {
            throw new NotImplementedException();
        }

        public ForumMessageDTO GetForumMessageByID(int forumMessageID)
        {
            throw new NotImplementedException();
        }

        public ForumMessageDTO GetForumMessageByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public List<ForumMessageDTO> GetForumMessagesByForumID(int forumId)
        {
            throw new NotImplementedException();
        }

        public List<ForumMessageDTO> GetForumMessagesBySender(int senderId)
        {
            throw new NotImplementedException();
        }

        public void UpdateForumMessage(ForumMessageDTO updatedForumMessage)
        {
            throw new NotImplementedException();
        }
    }
}
