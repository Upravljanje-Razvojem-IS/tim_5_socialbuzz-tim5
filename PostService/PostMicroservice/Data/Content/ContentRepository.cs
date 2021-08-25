using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostMicroservice.Database;
using PostMicroservice.Entities;


namespace PostMicroservice.Data.ContentRepository

{
    public class ContentRepository : IContentRepository
    {

        private readonly AppDbContext context;

        public ContentRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void CreateContent(Content content)
        {
            context.Contents.Add(content);
        }

       

        public void DeleteContent(Guid contentId)
        {
            var content = GetContentById(contentId);
            context.Remove(content);
        }

        public Entities.Content GetContentById(Guid contentId)
        {
            return context.Contents.FirstOrDefault(e => e.ContentId == contentId);

        }

        public List<Entities.Content> GetContents(string title = null)
        {
            return context.Contents.Where(e => (title == null || e.Title == title)).ToList();

        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;

        }

        public void UpdateContent(Content content)
        {
        }
    }
}
