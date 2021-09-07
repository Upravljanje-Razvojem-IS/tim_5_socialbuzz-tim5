using AutoMapper;
using IsporukaService.Database;
using IsporukaService.DTOs.IsporukaDTOs;
using IsporukaService.Entities;
using IsporukaService.Interfaces;
using IsporukaService.Logger;
using IsporukaService.MockedData;
using IsporukaService.ServiceException;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IsporukaService.Repository
{
    public class IsporukaRepository : IIsporukaRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly MockLogger _logger;

        public IsporukaRepository(MockLogger logger, IMapper mapper, DatabaseContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public IsporukaConfirmationDto Create(IsporukaCreateDto dto)
        {
            User kupac = UserData.Users.FirstOrDefault(e => e.Id == dto.KupacId);

            if (kupac == null)
                throw new IsporukaServiceException("Kupac ne postoji");

            User prodavac = UserData.Users.FirstOrDefault(e => e.Id == dto.ProdavacId);

            if (prodavac == null)
                throw new IsporukaServiceException("Prodavac ne postoji");

            Isporuka kreiranaIsporuka = new Isporuka()
            {
                Id = Guid.NewGuid(),
                DatumPorudzbine = dto.DatumPorudzbine,
                DatumIsporuke = dto.DatumIsporuke,
                Firma = dto.Firma,
                Trosak = dto.Trosak,
                ProdavacId = dto.ProdavacId,
                KupacId = dto.KupacId,
                LokacijaId = dto.LokacijaId
            };

            _context.Isporuke.Add(kreiranaIsporuka);

            _context.SaveChanges();

            _logger.Log("Kreiranje isporuke!");

            return _mapper.Map<IsporukaConfirmationDto>(kreiranaIsporuka);
        }

        public List<IsporukaReadDto> Get()
        {
            var list = _context.Isporuke.ToList();

            _logger.Log("Preuzimanje svih Isporuka!");

            return _mapper.Map<List<IsporukaReadDto>>(list);
        }

        public IsporukaReadDto Get(Guid id)
        {
            var entity = _context.Isporuke.FirstOrDefault(e => e.Id == id);

            _logger.Log("Preuzimanje isporuke po Id-u!");

            return _mapper.Map<IsporukaReadDto>(entity);
        }

        public IsporukaConfirmationDto Update(Guid id, IsporukaCreateDto dto)
        {
            User kupac = UserData.Users.FirstOrDefault(e => e.Id == dto.KupacId);

            if (kupac == null)
                throw new IsporukaServiceException("Kupac ne postoji");

            User prodavac = UserData.Users.FirstOrDefault(e => e.Id == dto.ProdavacId);

            if (prodavac == null)
                throw new IsporukaServiceException("Prodavac ne postoji");

            var isporuka = _context.Isporuke.FirstOrDefault(e => e.Id == id);

            if (isporuka == null)
                throw new IsporukaServiceException("Isporuka ne postoji");

            isporuka.DatumPorudzbine = dto.DatumPorudzbine;
            isporuka.DatumIsporuke = dto.DatumIsporuke;
            isporuka.Firma = dto.Firma;
            isporuka.Trosak = dto.Trosak;
            isporuka.ProdavacId = dto.ProdavacId;
            isporuka.KupacId = dto.KupacId;
            isporuka.LokacijaId = dto.LokacijaId;

            _context.SaveChanges();

            _logger.Log("Isporuka azurirana!");

            return _mapper.Map<IsporukaConfirmationDto>(isporuka);
        }

        public void Delete(Guid id)
        {
            var Isporuka = _context.Isporuke.FirstOrDefault(e => e.Id == id);

            if (Isporuka == null)
                throw new IsporukaServiceException("Isporuka ne postoji");

            _context.Isporuke.Remove(Isporuka);

            _logger.Log("Isporuka obrisana!");

            _context.SaveChanges();
        }
    }
}
