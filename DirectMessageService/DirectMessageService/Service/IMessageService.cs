using DirectMessageService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectMessageService.Service
{
    public interface IMessageService
    {
        List<MessageDTO> GetAllMessages();
        MessageDTO GetMessageByID(int messageID);

        List<MessageDTO> GetMessagesBySender(int senderID);
        List<MessageDTO> GetMessagesByReceiver(int receiverID);

        List<MessageDTO> GetMessagesForUser(int userID);

        MessageDTO CreateMessage(MessageCreateDTO newMessage, int senderID);

        void UpdateMessage(MessageDTO updatedMessage);

        void DeleteMessage(int messageID);

        bool CheckDoIFollowUser(int userID, int followingID);

        bool CheckDidIBlockUser(int userID, int blockedID);

        
    }
}
