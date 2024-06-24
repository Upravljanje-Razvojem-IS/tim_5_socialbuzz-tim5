using CommentService.Data.Interfaces;
using CommentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommentService.Data.Repositories
{
    public class MockAdRepository : IAdRepository
    {
        private static List<Ad> posts = new List<Ad>
        {
            new Ad
            {
                AdId = 0,
                Name = "For you",
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
            },
             new Ad
            {
                AdId = 1,
                Name = "For everyone",
                Content = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. "
            },

        };

        public Ad Get(int adId)
        {
            return posts.SingleOrDefault(a => a.AdId == adId);
        }
    }
}
