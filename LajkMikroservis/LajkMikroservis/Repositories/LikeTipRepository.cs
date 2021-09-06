using AutoMapper;
using LajkMikroservis.Database;
using LajkMikroservis.DTOs.LikeTipDTO;
using LajkMikroservis.Entities;
using LajkMikroservis.Interfaces;
using LajkMikroservis.Logger;
using LajkMikroservis.ServiceException;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LajkMikroservis.Repositories
{
    public class LikeTipRepository : ILikeTipRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly MockLogger _logger;

        public LikeTipRepository(MockLogger logger, IMapper mapper, DatabaseContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public LikeTipConfirmationDto Create(LikeTipCreateDto dto)
        {
            LikeTip newEntity = new LikeTip()
            {
                Id = Guid.NewGuid(),
                Tip = dto.Tip
            };

            _context.LikeTipovi.Add(newEntity);
            _context.SaveChanges();

            _logger.Log("Tip lajka kreiran");

            return _mapper.Map<LikeTipConfirmationDto>(newEntity);
        }

        public void Delete(Guid id)
        {
            var entity = _context.LikeTipovi.FirstOrDefault(e => e.Id == id);

            if (entity == null)
                throw new LikeServiceException("Tip lajka ne posotji");

            _context.LikeTipovi.Remove(entity);
            _context.SaveChanges();

            _logger.Log("Brisanje tipa lajka");
        }

        public List<LikeTipReadDto> Get()
        {
            var entities = _context.LikeTipovi
               .ToList();

            _logger.Log("Preuzimanje svih tipova");

            return _mapper.Map<List<LikeTipReadDto>>(entities);
        }

        public LikeTipReadDto Get(Guid id)
        {
            var entity = _context.LikeTipovi
              .FirstOrDefault(e => e.Id == id);

            _logger.Log("Preuzimanje tipa lajka po id-u");

            return _mapper.Map<LikeTipReadDto>(entity);
        }

        public LikeTipConfirmationDto Update(Guid id, LikeTipCreateDto dto)
        {
            var entity = _context.LikeTipovi.FirstOrDefault(e => e.Id == id);

            if (entity == null)
                throw new LikeServiceException("Tip lajka ne postoji");

            entity.Tip = dto.Tip;

            _context.SaveChanges();

            _logger.Log("Update Message");

            return _mapper.Map<LikeTipConfirmationDto>(entity);
        }
    }
}
