using ForumService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Repository
{
    public class ForumMessageRepository : IForumMessageRepository
    {
        private readonly DataContext contextDB;

        public ForumMessageRepository(DataContext context)
        {
            this.contextDB = context;
        }


        public ForumMessage CreateForumMessage(ForumMessage newForumMessage)
        {
            var forumMess = contextDB.ForumMessages.Add(newForumMessage);
            return forumMess.Entity;
        }

        public void DeleteForumMessage(int forumMessageID)
        {
            var toDelete = GetForumMessageByID(forumMessageID);
            contextDB.ForumMessages.Remove(toDelete);
        }

        public List<ForumMessage> GetAllForumMessages()
        {
            return contextDB.ForumMessages.ToList();
        }

        public List<ForumMessage> GetForumMessagesByForumID(int forumId)
        {
            return contextDB.ForumMessages.ToList()
           .Select(x => new ForumMessage
           {
               ForumMessageID = x.ForumMessageID,
               ForumID = x.ForumID,
               SenderID = x.SenderID,
               Body = x.Body,
               Title = x.Title
           })
           .Where(x => x.ForumID == forumId).ToList();
        }

        public ForumMessage GetForumMessageByID(int forumMessageID)
        {
            return contextDB.ForumMessages.FirstOrDefault(e => e.ForumMessageID == forumMessageID);

        }

        public ForumMessage GetForumMessageByTitle(string title)
        {
            return contextDB.ForumMessages.ToList()
              .Select(x => new ForumMessage
              {
                  ForumMessageID = x.ForumMessageID,
                  ForumID = x.ForumID,
                  SenderID = x.SenderID,
                  Body = x.Body,
                  Title = x.Title
              })
              .Where(x => x.Title == title).First();
        }

        public List<ForumMessage> GetForumMessagesBySender(int senderId)
        {
            return contextDB.ForumMessages.ToList()
             .Select(x => new ForumMessage
             {
                 ForumMessageID = x.ForumMessageID,
                 ForumID = x.ForumID,
                 SenderID = x.SenderID,
                 Body = x.Body,
                 Title = x.Title
             })
             .Where(x => x.SenderID == senderId).ToList();
        }

        public bool SaveChanges()
        {
            return contextDB.SaveChanges() > 0;
        }

        public void UpdateForumMessage(ForumMessage updatedForumMessage)
        {
           //
        }
    }
}
