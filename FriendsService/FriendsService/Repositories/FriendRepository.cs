using Microsoft.EntityFrameworkCore;
using FriendsService.Entities;
using FriendsService.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FriendsService.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private readonly AppDbContext _context;

        public FriendRepository(AppDbContext context)
        {
            _context = context;
        }

        public Friend Create(Friend entity)
        {
            _context.Friends.Add(entity);

            _context.SaveChanges();

            return entity;
        }

        public IEnumerable<Friend> Get()
        {
            return _context.Friends;
        }

        public Friend Get(int id)
        {
            return _context.Friends.FirstOrDefault(e => e.Id == id);
        }

        public Friend Update(Friend entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            _context.SaveChanges();

            return entity;
        }

        public Friend Delete(int id)
        {
            Friend entity = _context.Friends.FirstOrDefault(e => e.Id == id);

            if (entity != null)
            {
                _context.Friends.Remove(entity);
                _context.SaveChanges();
            }

            return entity;
        }
    }
}
