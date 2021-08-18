using ForumService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Repository
{
    public interface IForumRepository
    {
        List<Forum> GetAllForums();
        Forum GetForumByID(int forumID);

        List<Forum> GetForumsByOwner(int ownerID);

        Forum CreateForum(Forum newForum);

        void UpdateForum(Forum updatedForum);

        void DeleteForum(int forumID);

        bool SaveChanges();
    }
}
