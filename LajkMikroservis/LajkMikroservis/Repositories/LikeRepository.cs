using AutoMapper;
using LajkMikroservis.Database;
using LajkMikroservis.DTOs.LikeDTO;
using LajkMikroservis.Entities;
using LajkMikroservis.Interfaces;
using LajkMikroservis.Logger;
using LajkMikroservis.MockedData;
using LajkMikroservis.ServiceException;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LajkMikroservis.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly MockLogger _logger;

        public LikeRepository(MockLogger logger, IMapper mapper, DatabaseContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public LikeConfirmationDto Create(LikeCreateDto dto)
        {
            var post = MockData.Posts.FirstOrDefault(e => e.Id == dto.PostId);
            var user = MockData.Users.FirstOrDefault(e => e.Id == dto.UserId);

            if (post == null || user == null)
                throw new LikeServiceException("User ili Post ne postoje");

            Like newEntity = new Like()
            {
                Id = Guid.NewGuid(),
                LikeTipId = dto.LikeTipId,
                PostId = dto.PostId,
                UserId = dto.UserId
            };

            _context.Likes.Add(newEntity);
            _context.SaveChanges();

            _logger.Log("Lajk kreiran");

            return _mapper.Map<LikeConfirmationDto>(newEntity);
        }

        public void Delete(Guid id)
        {
            var entity = _context.Likes.FirstOrDefault(e => e.Id == id);

            if (entity == null)
                throw new LikeServiceException("Like ne posotji");

            _context.Likes.Remove(entity);
            _context.SaveChanges();

            _logger.Log("Brisanje lajka");
        }

        public List<LikeReadDto> Get()
        {
            var entities = _context.Likes
               .ToList();

            _logger.Log("Get Messages");

            return _mapper.Map<List<LikeReadDto>>(entities);
        }

        public LikeReadDto Get(Guid id)
        {
            var entity = _context.Likes
               .FirstOrDefault(e => e.Id == id);

            _logger.Log("Get Message");

            return _mapper.Map<LikeReadDto>(entity);
        }

        public LikeConfirmationDto Update(Guid id, LikeCreateDto dto)
        {
            var post = MockData.Posts.FirstOrDefault(e => e.Id == dto.PostId);
            var user = MockData.Users.FirstOrDefault(e => e.Id == dto.UserId);

            if (post == null || user == null)
                throw new LikeServiceException("User ili Post ne postoje");

            var entity = _context.Likes.FirstOrDefault(e => e.Id == id);

            if (entity == null)
                throw new LikeServiceException("Lajk ne postoji");

            entity.LikeTipId = dto.LikeTipId;
            entity.PostId = dto.PostId;
            entity.UserId = dto.UserId;

            _context.SaveChanges();

            _logger.Log("Update Message");

            return _mapper.Map<LikeConfirmationDto>(entity);
        }

        public List<LikeReadDto> GetAllByPostId(int postId)
        {
            var list = _context.Likes.Where(e => e.PostId == postId);

            return _mapper.Map<List<LikeReadDto>>(list);
        }

        public List<LikeReadDto> GetAllByUserId(int userId)
        {
            var list = _context.Likes.Where(e => e.UserId == userId);

            return _mapper.Map<List<LikeReadDto>>(list);
        }
    }
}
