using ForumService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Repository
{
    public class ForumRepository : IForumRepository
    {
        private readonly DataContext contextDB;

        public ForumRepository(DataContext context) {
            this.contextDB = context;
        }

    
        public Forum CreateForum(Forum newForum)
        {
            var forum = contextDB.Forums.Add(newForum);
            return forum.Entity;
        }

        public void DeleteForum(int forumID)
        {
            var toDelete = GetForumByID(forumID);
            contextDB.Forums.Remove(toDelete);
        }

        public List<Forum> GetAllForums()
        {
            return contextDB.Forums.ToList();
        }

        public Forum GetForumByID(int forumID)
        {
            return contextDB.Forums.FirstOrDefault(e => e.ForumID == forumID);
        }

        public List<Forum> GetForumsByOwner(int ownerID)
        {
            return  contextDB.Forums.ToList()
            .Select(x => new Forum
            {
                ForumID=x.ForumID,
                ForumName= x.ForumName,
                UserID=x.UserID,
                ForumDescription=x.ForumDescription,
                LogoLink=x.LogoLink,
                IsOpen=x.IsOpen
            })
            .Where(x => x.UserID == ownerID).ToList();
        }

        public bool SaveChanges()
        {
            return contextDB.SaveChanges() >0;
        }

        public void UpdateForum(Forum updatedForum)
        {
            //contextDB.Forums.Attach(updatedForum);
            //contextDB.Entry(updatedForum).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //contextDB.SaveChanges();
        }
    }
}
