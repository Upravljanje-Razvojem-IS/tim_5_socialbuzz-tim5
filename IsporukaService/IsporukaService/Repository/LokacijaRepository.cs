using AutoMapper;
using IsporukaService.Database;
using IsporukaService.DTOs.LokacijaDTOs;
using IsporukaService.Entities;
using IsporukaService.Interfaces;
using IsporukaService.Logger;
using IsporukaService.ServiceException;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IsporukaService.Repository
{
    public class LokacijaRepository : ILokacijaRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly MockLogger _logger;

        public LokacijaRepository(MockLogger logger, IMapper mapper, DatabaseContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public LokacijaConfirmationDto Create(LokacijaCreateDto dto)
        {
            Lokacija kreiranaLokacija = new Lokacija()
            {
                Id = Guid.NewGuid(),
                Drzava = dto.Drzava,
                Grad = dto.Grad,
                Adresa = dto.Adresa,
                Ptt = dto.Ptt
            };

            _context.Lokacije.Add(kreiranaLokacija);

            _context.SaveChanges();

            _logger.Log("Kreiranje lokacije!");

            return _mapper.Map<LokacijaConfirmationDto>(kreiranaLokacija);
        }

        public List<LokacijaReadDto> Get()
        {
            var list = _context.Lokacije.ToList();

            _logger.Log("Preuzimanje svih lokacija!");

            return _mapper.Map<List<LokacijaReadDto>>(list);
        }

        public LokacijaReadDto Get(Guid id)
        {
            var entity = _context.Lokacije.FirstOrDefault(e => e.Id == id);

            _logger.Log("Preuzimanje lokacije po Id-u!");

            return _mapper.Map<LokacijaReadDto>(entity);
        }

        public LokacijaConfirmationDto Update(Guid id, LokacijaCreateDto dto)
        {
            var lokacija = _context.Lokacije.FirstOrDefault(e => e.Id == id);

            if (lokacija == null)
                throw new IsporukaServiceException("Lokacija ne postoji");

            lokacija.Grad = dto.Grad;
            lokacija.Adresa = dto.Adresa;
            lokacija.Drzava = dto.Drzava;
            lokacija.Ptt = dto.Ptt;

            _context.SaveChanges();

            _logger.Log("Lokacija azurirana!");

            return _mapper.Map<LokacijaConfirmationDto>(lokacija);
        }

        public void Delete(Guid id)
        {
            var lokacija = _context.Lokacije.FirstOrDefault(e => e.Id == id);

            if (lokacija == null)
                throw new IsporukaServiceException("Lokacija ne postoji");

            _context.Lokacije.Remove(lokacija);

            _logger.Log("Lokacija obrisana!");

            _context.SaveChanges();
        }
    }
}
