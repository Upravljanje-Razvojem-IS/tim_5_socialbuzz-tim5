using ForumService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Service
{
    public interface IForumMessageService
    {
        List<ForumMessageDTO> GetAllForumMessages();
        ForumMessageDTO GetForumMessageByID(int forumMessageID);
        List<ForumMessageDTO> GetForumMessagesByForumID(int forumId);
        ForumMessageDTO GetForumMessageByTitle(String title);

        List<ForumMessageDTO> GetForumMessagesBySender(int senderId);

        ForumMessageDTO CreateForumMessage(ForumMessageDTO newForumMessage);

        void UpdateForumMessage(ForumMessageDTO updatedForumMessage);

        void DeleteForumMessage(int forumMessageID);
    }
}
