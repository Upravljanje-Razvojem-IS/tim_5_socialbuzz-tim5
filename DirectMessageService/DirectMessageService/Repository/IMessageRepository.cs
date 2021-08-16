using DirectMessageService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectMessageService.Repository
{
    public interface IMessageRepository
    {
        List<Message> GetAllMessages();
        Message GetMessageByID(int messageID);

        List<Message> GetMessagesBySender(int senderID);
        List<Message> GetMessagesByReceiver(int receiverID);

        List<Message> GetMessagesForUser(int userID);

        Message CreateMessage(Message newMessage);

        void UpdateMessage(Message updatedMessage);

        void DeleteMessage(int messageID);

        bool CheckDoIFollowUser(int userID, int followingID);

        bool CheckDidIBlockUser(int userID, int blockedID);

        bool SaveChanges();
    }
}
