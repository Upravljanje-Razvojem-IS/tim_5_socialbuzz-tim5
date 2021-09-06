using IsporukaService.DTOs.IsporukaDTOs;
using System;
using System.Collections.Generic;

namespace IsporukaService.Interfaces
{
    public interface IIsporukaRepository
    {
        List<IsporukaReadDto> Get();
        IsporukaReadDto Get(Guid id);
        IsporukaConfirmationDto Create(IsporukaCreateDto dto);
        IsporukaConfirmationDto Update(Guid id, IsporukaCreateDto dto);
        void Delete(Guid id);
    }
}
