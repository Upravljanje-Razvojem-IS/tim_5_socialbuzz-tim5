using Microsoft.EntityFrameworkCore;
using FriendsService.Entities;
using FriendsService.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FriendsService.Repositories
{
    public class FriendListRepository : IFriendListRepository
    {
        private readonly AppDbContext _context;

        public FriendListRepository(AppDbContext context)
        {
            _context = context;
        }

        public FriendList Create(FriendList entity)
        {
            _context.FriendLists.Add(entity);

            _context.SaveChanges();

            return entity;
        }

        public IEnumerable<FriendList> Get()
        {
            return _context.FriendLists.Include(e => e.Friends);
        }

        public FriendList Get(int id)
        {
            return _context.FriendLists.Include(e => e.Friends).FirstOrDefault(e => e.Id == id);
        }

        public FriendList Update(FriendList entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            _context.SaveChanges();

            return entity;
        }

        public FriendList Delete(int id)
        {
            FriendList entity = _context.FriendLists.FirstOrDefault(e => e.Id == id);

            if (entity != null)
            {
                _context.FriendLists.Remove(entity);
                _context.SaveChanges();
            }

            return entity;
        }
    }
}
