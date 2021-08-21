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
            throw new NotImplementedException();
        }

        public Entities.Content GetContentById(Guid contentId)
        {
            throw new NotImplementedException();
        }

        public List<Entities.Content> GetContents()
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;

        }

        public void UpdateContent(Entities.Content oldContent, Entities.Content newContent)
        {
            throw new NotImplementedException();
        }
    }
}
