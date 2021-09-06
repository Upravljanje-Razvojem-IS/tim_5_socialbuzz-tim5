using IsporukaService.DTOs.LokacijaDTOs;
using System;
using System.Collections.Generic;

namespace IsporukaService.Interfaces
{
    public interface ILokacijaRepository
    {
        List<LokacijaReadDto> Get();
        LokacijaReadDto Get(Guid id);
        LokacijaConfirmationDto Create(LokacijaCreateDto dto);
        LokacijaConfirmationDto Update(Guid id, LokacijaCreateDto dto);
        void Delete(Guid id);
    }
}
