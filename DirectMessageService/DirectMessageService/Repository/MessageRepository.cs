using DirectMessageService.Entity;
using DirectMessageService.Repository.BlockMock;
using DirectMessageService.Repository.FollowingMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectMessageService.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext contextDB;
        private readonly IBlockMockRepository blockingMockRepository;
        private readonly IFollowingMockRepository followingMockRepository;

        public MessageRepository(DataContext context, IFollowingMockRepository followingMockRepository, IBlockMockRepository blockingMockRepository)
        {
            this.contextDB = context;
            this.followingMockRepository = followingMockRepository;
            this.blockingMockRepository = blockingMockRepository;
        }
        public bool CheckDidIBlockUser(int userID, int blockedID)
        {
            return blockingMockRepository.CheckDidIBlockUser(userID, blockedID);
        }

        public bool CheckDoIFollowUser(int userID, int followingID)
        {
            return followingMockRepository.CheckDoIFollowUser(userID, followingID);
        }

        public Message CreateMessage(Message newMessage)
        {
            var mess = contextDB.Messages.Add(newMessage);
            return mess.Entity;
        }

        public void DeleteMessage(int messageID)
        {

            var toDelete = GetMessageByID(messageID);
            contextDB.Messages.Remove(toDelete);
        }

        public List<Message> GetAllMessages()
        {
            return contextDB.Messages.ToList();
        }

        public Message GetMessageByID(int messageID)
        {
            return contextDB.Messages.FirstOrDefault(e => e.MessageID == messageID);
        }

        public List<Message> GetMessagesByReceiver(int receiverID)
        {
            return contextDB.Messages.ToList()
            .Select(x => new Message
            {
                MessageID = x.MessageID,
                SenderID = x.SenderID,
                ReceiverID = x.ReceiverID,
                Body = x.Body,
                IsSent = x.IsSent
            })
            .Where(x => x.ReceiverID == receiverID).ToList();
        }

        public List<Message> GetMessagesBySender(int senderID)
        {
            return contextDB.Messages.ToList()
               .Select(x => new Message
               {
                   MessageID = x.MessageID,
                   SenderID = x.SenderID,
                   ReceiverID = x.ReceiverID,
                   Body = x.Body,
                   IsSent = x.IsSent
               })
               .Where(x => x.SenderID == senderID).ToList();
        }

        public List<Message> GetMessagesForUser(int userID)
        {
            List<Message> sended = contextDB.Messages.ToList()
               .Select(x => new Message
               {
                   MessageID = x.MessageID,
                   SenderID = x.SenderID,
                   ReceiverID = x.ReceiverID,
                   Body = x.Body,
                   IsSent = x.IsSent
               })
               .Where(x => x.SenderID == userID).ToList();

            List<Message> received = contextDB.Messages.ToList()
              .Select(x => new Message
              {
                  MessageID = x.MessageID,
                  SenderID = x.SenderID,
                  ReceiverID = x.ReceiverID,
                  Body = x.Body,
                  IsSent = x.IsSent
              })
              .Where(x => x.ReceiverID == userID).ToList();

            List<Message> all = sended.Concat(received).ToList();

            return all;
        }

        public bool SaveChanges()
        {
            return contextDB.SaveChanges() > 0;
        }

        public void UpdateMessage(Message updatedMessage)
        {
            throw new NotImplementedException();
        }
    }
}
