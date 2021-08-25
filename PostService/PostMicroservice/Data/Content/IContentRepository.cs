using PostMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Data.ContentRepository
{
    public interface IContentRepository
    {


        List<Content> GetContents(string title = null);
        Content GetContentById(Guid contentId);
        void CreateContent(Content content);
        void UpdateContent(Content content);
        void DeleteContent(Guid contentId);
        bool SaveChanges();
     
    }
}
