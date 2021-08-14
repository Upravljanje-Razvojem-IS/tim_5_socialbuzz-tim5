using ForumService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Service
{
    public interface IForumService
    {
        List<ForumDTO> GetAllForums();
        ForumDTO GetForumByID(int forumID);

        List<ForumDTO> GetForumsByOwner(int ownerID);

        ForumDTO CreateForum(ForumDTO newForum);

        void UpdateForum(ForumDTO updatedForum);

        void DeleteForum(int forumID);
    }
}
