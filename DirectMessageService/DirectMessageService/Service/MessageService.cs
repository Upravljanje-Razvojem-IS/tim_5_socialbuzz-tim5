using AutoMapper;
using DirectMessageService.DTO;
using DirectMessageService.Entity;
using DirectMessageService.Exceptions;
using DirectMessageService.Repository;
using DirectMessageService.Repository.UserMock;
using ForumService.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectMessageService.Service
{
    public class MessageService : IMessageService
    {
        private readonly IUserMockRepository _userMockRepository;
        private readonly IMapper mapper;
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository message, IMapper mapper, IUserMockRepository userMockRepository) {
            this._userMockRepository = userMockRepository;
            this.mapper = mapper;
            this._messageRepository = message;
        }

        public bool CheckDidIBlockUser(int userID, int blockedID)
        {
            return _messageRepository.CheckDidIBlockUser(userID, blockedID);
        }

        public bool CheckDoIFollowUser(int userID, int followingID)
        {
            return _messageRepository.CheckDoIFollowUser(userID, followingID);
        }

        public MessageDTO CreateMessage(MessageCreateDTO newMessage, int senderID)
        {
            var user = _userMockRepository.GetUserByID(senderID);

            if (user == null)
            {
                throw new NotFoundException("User with passed ID is not found...");
            }

            Message entity = mapper.Map<Message>(newMessage);
            entity.SenderID = senderID;

            if (_messageRepository.CheckDidIBlockUser(senderID, entity.ReceiverID))
            {
                throw new BlockingException("You have blocked this user and you can not send him message.");
            }

            if (!_messageRepository.CheckDoIFollowUser(senderID, entity.ReceiverID))
            {
                throw new BlockingException("You are not following this user and you can not send him message.");
            }

            try
            {
                entity.IsSent = true;
                var message = _messageRepository.CreateMessage(entity);
                _messageRepository.SaveChanges();
                return mapper.Map<MessageDTO>(message);
            }
            catch (Exception ex)
            {
                throw new ErrorOccurException(ex.Message);
            }
        }

        public void DeleteMessage(int messageID)
        {
            var mess = _messageRepository.GetMessageByID(messageID);

            if (mess == null)
            {
                throw new NotFoundException("There is no message with that ID!");
            }
            try
            {
                _messageRepository.DeleteMessage(messageID);
                _messageRepository.SaveChanges();
            }

            catch (Exception ex)
            {
                throw new ErrorOccurException("Error deleting message: " + ex.Message);
            }
        }

        public List<MessageDTO> GetAllMessages()
        {
            var messages = _messageRepository.GetAllMessages();
            return mapper.Map<List<MessageDTO>>(messages);
        }

        public MessageDTO GetMessageByID(int messageID)
        {
            var message = _messageRepository.GetMessageByID(messageID);

            if (message == null)
            {
                throw new NotFoundException("No message with that ID found...");
            }

            return mapper.Map<MessageDTO>(message);
        }

        public List<MessageDTO> GetMessagesByReceiver(int receiverID)
        {
            var message = _messageRepository.GetMessagesByReceiver(receiverID);

            if (message == null)
            {
                throw new NotFoundException("No message for receiver found...");
            }

            return mapper.Map<List<MessageDTO>>(message);
        }

        public List<MessageDTO> GetMessagesBySender(int senderID)
        {
            var message = _messageRepository.GetMessagesBySender(senderID);

            if (message == null)
            {
                throw new NotFoundException("No message for sender found...");
            }

            return mapper.Map<List<MessageDTO>>(message);
        }

        public List<MessageDTO> GetMessagesForUser(int userID)
        {
            var message = _messageRepository.GetMessagesForUser(userID);

            if (message == null)
            {
                throw new NotFoundException("No message for user found...");
            }

            return mapper.Map<List<MessageDTO>>(message);
        }

        
        public void UpdateMessage(MessageDTO updatedMessage)
        {
            throw new NotImplementedException();
        }
    }
}
