using ForumService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Repository
{
    public interface IForumMessageRepository
    {
        List<ForumMessage> GetAllForumMessages();
        ForumMessage GetForumMessageByID(int forumMessageID);
        List<ForumMessage> GetForumMessagesByForumID(int forumId);
        ForumMessage GetForumMessageByTitle(String title);

        List<ForumMessage> GetForumMessagesBySender(int senderId);

        ForumMessage CreateForumMessage(ForumMessage newForumMessage);

        void UpdateForumMessage(ForumMessage updatedForumMessage);

        void DeleteForumMessage(int forumMessageID);

        bool SaveChanges();
    }
}
